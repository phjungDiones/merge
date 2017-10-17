using System;
using System.Text;
using System.IO.Ports;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace CJ_Controls.Communication.SRZ
{
	public enum ASCII : byte
	{
		NULL = 0x00, SOH = 0x01, STX = 0x02, ETX = 0x03, EOT = 0x04, ENQ = 0x05, ACK = 0x06, BELL = 0x07,
		BS = 0x08, HT = 0x09, LF = 0x0A, VT = 0x0B, FF = 0x0C, CR = 0x0D, SO = 0x0E, SI = 0x0F, DC1 = 0x11,
		DC2 = 0x12, DC3 = 0x13, DC4 = 0x14, NAK = 0x15, SYN = 0x16, ETB = 0x17, CAN = 0x18, EM = 0x19,
		SUB = 0x1A, ESC = 0x1B, FS = 0x1C, GS = 0x1D, RS = 0x1E, US = 0x1F, SP = 0x20, DEL = 0x7F
	}

	public class Class_XRKC
	{
		#region Log 보내기
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public event MessageEventHandler MessageEvent;
		public event MessageEventHandler Communication_Event;
		private void LogTextOut(string message)
		{
			if (MessageEvent != null)
				MessageEvent(this, new MessageEventArgs(message));
		}
		private void Log_Communication(string message)
		{
			if (Communication_Event != null)
				Communication_Event(this, new MessageEventArgs(message));
		}
		#endregion

		public SerialPort _Serial = new SerialPort();

		protected const int MAX_BUFFER_SIZE = 4096;
		protected const int COMMUNICATION_DELAY_TIME = 3000;

		internal const int RETRY_NO = 3;

		/*
		 * For Sequence No
		*/
		internal int nReadSeqNo = 0;
		internal int nWriteSeqNo = 0;

		/*
		 * For Retry Count
		*/
		internal int nReadRetryNo = 0;
		internal int nWriteRetryNo = 0;

		/*
		 * For Wait Time
		*/
		internal DateTime dtReadTime;
		internal DateTime dtWriteTime;

		/*
		 * For Receive Buffer
		*/
		private uint nRcvBytes = 0;
		private byte[] byRcvBuff = new byte[MAX_BUFFER_SIZE];

		/*
		 * For Log
		*/
		//internal XLog logFile = null;
		internal bool bLog = true;
		internal string strPortNo = "";

		/*
		 * For Processing Flags
		*/
		internal bool bProcessing = false;
		internal bool bOverWrited = false;

		internal enum WAITTIME
		{
			READ = 10000000,
			WRITE = 10000000
		};

		public Class_XRKC()
		{
			//이쪽으로 리시브해라..
			_Serial.DataReceived += new SerialDataReceivedEventHandler(OnSerialRcv);
		}
		public bool IsOpen()
		{
			return _Serial.IsOpen;
		}

		public bool Open(string strPort, int nBaudRate)
		{
			if (IsOpen())  //열렸냐?
			{
				Close(); // 닫아라.
				Thread.Sleep(100);
			}

			bOverWrited = false;
			nReadSeqNo = 0;
			nWriteSeqNo = 0;
			nReadRetryNo = 0;
			nWriteRetryNo = 0;
			bProcessing = false;

			_Serial.PortName = strPort;
			_Serial.BaudRate = nBaudRate;
			_Serial.Encoding = Encoding.Default;
			_Serial.Parity = Parity.None;
			_Serial.DataBits = 8;
			_Serial.StopBits = StopBits.One;

			try
			{
				ResetBuff();
				_Serial.Open();
			}
			catch (System.Exception ex)
			{
				LogTextOut("SRZ Open Error! "+ex.Message);
				return false;
			}
			return true;
		}

		public void Close()
		{
			if (IsOpen())
			{
				_Serial.Close();
			}
		}
		public bool Send(string strSend)
		{
			if (IsOpen().Equals(false))
				return false;

			_Serial.Write(strSend);

			return true;
		}
		public bool Send(byte[] buffer,int offset, int count)
		{
			if (IsOpen().Equals(false))
				return false;

			_Serial.Write(buffer, offset, count);

			return true;
		}
		
		public bool Send(byte strSend)
		{
			if (IsOpen().Equals(false))
				return false;
			byte[] Snd = new byte[2];
			Snd[0] = strSend;
			_Serial.Write(Snd, 0, 1);

			//cjinnnn:한바이트 보내는 것은, 다음 전송할 커맨드가 너무 빨리 전송될 경우, 응답받는 쪽에서 합쳐져서 받는다. (시뮬레이터)
			Thread.Sleep(100);
			return true;
		}

		private long GetElapseTime(long dtTicks)
		{
			return ((DateTime.Now.Ticks - dtTicks) / 10000);
		}

		private long GetElapseTime(DateTime dtTime)
		{
			return ((DateTime.Now.Ticks - dtTime.Ticks) / 10000);
		}

		/// <summary>
		/// 받은 Data를 확인하는 함수 이다.
		/// STX 다음부터 ETX까지를 Exclusive OR한 값과 BCC byte의 값을 비교한다.
		/// 주의 : ETX, ETB를 받은 후에 호출 되어 져야 한다.
		/// </summary>
		/// <returns>계산된 값과 받은 bcc의 값이 같으면 true, 아니면 false</returns>
		public bool IsValidBCC(byte endChar)
		{
			byte bcc;
			int nStartPos = Array.IndexOf(byRcvBuff, (byte)ASCII.STX, 0);
			int nEndPos = Array.IndexOf(byRcvBuff, endChar, 0);

			bcc = byRcvBuff[nStartPos + 1];
			for (int i = nStartPos + 2; i <= nEndPos; i++)
			{
				bcc ^= byRcvBuff[i];
			}
			if (bcc == byRcvBuff[nEndPos + 1])
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 받은 Data가 잘못되었을 경우에 receivedBuff를 Clear하고 Count를 Reset한다.
		/// </summary>
		public void ResetBuff()
		{
			nRcvBytes = 0;
			Array.Clear(byRcvBuff, 0, byRcvBuff.Length);
		}

		/// <summary>
		/// 받은 Data중에 parameter와 같은 data가 있는지를 확인하고
		/// 또한 parameter 다음의 BCC도 받았는지를 확인한다.
		/// </summary>
		/// <param name="ch">찾고자 하는 ASCII byte code</param>
		/// <returns>찾았으면 true, 아니면 false</returns>
		public bool IsReceivedByte(byte ch)
		{
			if (nRcvBytes == 0)
			{
				return false;
			}
			int nNo = Array.IndexOf(byRcvBuff, ch, 0);
			if ((ch == (byte)ASCII.ETB) || (ch == (byte)ASCII.ETX))
			{
				if ((nNo != -1) && (nRcvBytes > nNo + 1))
				{
					return true;
				}
			}
			else
			{
				if (nNo != -1)
				{
					return true;
				}
			}
			return false;
		}

		void OnSerialRcv(object sender, SerialDataReceivedEventArgs e)
		{
			SerialPort sp = (SerialPort)sender;
			if (sp.BytesToRead >= 0)
			{
				string strBuff = sp.ReadExisting();
				uint nBytes = Convert.ToUInt32(strBuff.Length);
				if ((nRcvBytes + nBytes) > MAX_BUFFER_SIZE)
				{
					bOverWrited = true;
					byRcvBuff = Encoding.Default.GetBytes(strBuff);
					nRcvBytes = nBytes;
					return;
				}
				//Buffer.BlockCopy(Encoding.Default.GetBytes(strBuff), 0, byRcvBuff, nRcvBytes, nBytes);
				Array.Copy(Encoding.Default.GetBytes(strBuff), 0, byRcvBuff, nRcvBytes, nBytes);
				nRcvBytes += nBytes;
			}
		}

		public bool SendReadCommand(UInt16 devId, string strCode)
		{
			byte[] command = new byte[6];
			char[] ch = new char[2];

			command[0] = (byte)ASCII.EOT;
			command[1] = (byte)((devId / 10) + 0x30);  // High Address
			command[2] = (byte)((devId % 10) + 0x30);

			ch = strCode.ToCharArray(0, 2);
			command[3] = (byte)ch[0];
			command[4] = (byte)ch[1];
			command[5] = (byte)ASCII.ENQ;

			if (Send(command,0,6))
			{
				return true;
			}
			return false;
		}

		public char MakeBCC(string strCmd)
		{
			char bcc;
			int length = -1;

			do { length++; } while (strCmd[length] != (char)ASCII.STX);

			bcc = (char)(strCmd[length + 1] ^ strCmd[length + 2]);
			length += 2;

			do
			{
				bcc ^= strCmd[++length];
			} while (strCmd[length] != (char)ASCII.ETX);

			return bcc;
		}

		public int SendWriteCommand(UInt16 devID, string strCode, int[] nValues, int nLen, int nStartNo, int nDataDigits)
		{
			string strCommand = null;
			strCommand += (char)ASCII.EOT;
			strCommand += String.Format("{0:00}", devID);

			strCommand += (char)ASCII.STX;
			strCommand += strCode;

			for (int i = 0; i < nLen; i++)
			{
				if (nDataDigits == 7)
				{
					strCommand += String.Format("{0:00} {1,7},", nStartNo + i, nValues[i]);
				}
				else if (nDataDigits == 1)
				{
					strCommand += String.Format("{0:00} {1,1},", nStartNo + i, nValues[i]);
				}
			}
			strCommand = strCommand.TrimEnd(',');
			strCommand += (char)ASCII.ETX;
			strCommand += MakeBCC(strCommand);

			if (Send(strCommand))
			{
				return 1;
			}
			return -1;
		}

		public int SendWriteCommandForSRZ(UInt16 devID, string strCode, int[] nValues, int nLen, int nStartNo, int nDataDigits)
		{
			string strCommand = null;
			strCommand += (char)ASCII.EOT;
			strCommand += String.Format("{0:00}", devID);

			strCommand += (char)ASCII.STX;
			strCommand += strCode;

			for (int i = 0; i < nLen; i++)
			{
				if (nDataDigits == 7)
				{
					strCommand += String.Format("{0:000} {1,7},", nStartNo + i, nValues[i]);
				}
				else if (nDataDigits == 1)
				{
					strCommand += String.Format("{0:000} {1,1},", nStartNo + i, nValues[i]);
				}
			}
			strCommand = strCommand.TrimEnd(',');
			strCommand += (char)ASCII.ETX;
			strCommand += MakeBCC(strCommand);

			if (Send(strCommand))
			{
				return 1;
			}
			return -1;
		}
		public int SendWriteCommandForSRZRUN(UInt16 devID, bool isRun)
		{
			string strCommand = null;
			strCommand += (char)ASCII.EOT;
			strCommand += String.Format("{0:00}", devID);

			strCommand += (char)ASCII.STX;
			strCommand += "SR";
			strCommand += isRun ? "1" : "0";

			strCommand += (char)ASCII.ETX;
			strCommand += MakeBCC(strCommand);

			if (Send(strCommand))
			{
				return 1;
			}
			return -1;
		}

		public int SendWriteCommandForSRZDIO(UInt16 devID, string strCode, int[] nValues, int nLen, int nChannelNo, int nDataDigits)
		{
			string strCommand = null;
			strCommand += (char)ASCII.EOT;
			strCommand += String.Format("{0:00}", devID);

			strCommand += (char)ASCII.STX;
			strCommand += strCode;

			strCommand += String.Format("{0:000}{1}", nChannelNo, " 000");
			for (int i = 0; i < 4; i++)
			{
				strCommand += nValues[i];
			}

			strCommand += (char)ASCII.ETX;
			strCommand += MakeBCC(strCommand);

			if (Send(strCommand))
			{
				return 1;
			}
			return -1;
		}

		public bool SendReadCommandForSRZDIO(UInt16 devId, string strCode, int nChannelNo)
		{
			string strCommand = null;
			strCommand += (char)ASCII.EOT;
			strCommand += String.Format("{0:00}", devId);

			strCommand += (char)ASCII.STX;
			strCommand += strCode;

			strCommand += String.Format("{0:000}", nChannelNo);

			strCommand += (char)ASCII.ETX;
			strCommand += MakeBCC(strCommand);

			if (Send(strCommand))
			{
				return true;
			}
			return false;
		}


		private string DeleteSpace(string strData)
		{
			string strRemovedData = "";

			for (int i = 0; i < strData.Length; i++)
			{
				if (strData[i] != ' ')
				{
					strRemovedData += strData[i];
				}
			}
			return strRemovedData;
		}

		public int SeqReadData(UInt16 devID, string strCommand, ref int[] readData)
		{
			int nRet = 0;
			int nSeqNo = nReadSeqNo;

			switch (nSeqNo)
			{
				case 0:
					if (bProcessing) { break; }
					if (SendReadCommand(devID, strCommand))
					{
						nSeqNo = 100;
						bProcessing = true;
						dtReadTime = DateTime.Now;
					}
					else
					{
						nReadRetryNo++;

						Log_Communication("SeqReadData(1) Retry Count : " + nReadRetryNo.ToString());
						if (nReadRetryNo > RETRY_NO)
						{
							nSeqNo = 0;
							bProcessing = false;
							nReadRetryNo = 0;
							nRet = -1;
						}
					}
					break;
				case 100:
					if (IsReceivedByte((byte)ASCII.ETB))
					{
						if (!IsValidBCC((byte)ASCII.ETB))
						{
							// Log를 남긴다.
							if (bLog)
							{
								Log_Communication(Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
							}
							ResetBuff();
							if (Send((byte)ASCII.NAK))
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(2) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nSeqNo = 0;
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -102;
								}
								break;
							}
							else
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(3) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nSeqNo = 0;
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -103;
									//break;
								}
								break;
							}
						}

						try
						{
							string strRcvedString = Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes);
							int nStart = strRcvedString.IndexOf((char)ASCII.STX);
							int nEnd = strRcvedString.IndexOf((char)ASCII.ETB);
							strRcvedString = strRcvedString.Substring(++nStart, nEnd - nStart);
							strRcvedString = strRcvedString.TrimStart(strCommand.ToCharArray());

							char[] separator = new char[1];
							separator[0] = ',';
							string[] strData = strRcvedString.Split(separator);
							separator[0] = ' ';

							foreach (string s in strData)
							{
								string[] strAtom = s.Split(separator, 2);
								if (strAtom.Length < 2) break;
								Int32 No = Convert.ToInt32(strAtom[0]);
								//Int32 Val = Convert.ToInt32(strAtom[1]);
								string strRemovedData = DeleteSpace(strAtom[1]);
								Int32 Val = (int)Convert.ToDouble(strRemovedData);
								if (No > 0 && No <= readData.Length)
								{
									readData[No - 1] = Val;
								}
							}
							ResetBuff();
							nSeqNo = 200;
						}
						catch (System.Exception ex)
						{
							string strMSg = "";
							strMSg = string.Format("SRZ Module ETB Process Error! {0} [RCV] {1}", ex.Message, Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
							LogTextOut(strMSg);

							ResetBuff();
							if (Send((byte)ASCII.NAK))
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(4) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nSeqNo = 0;
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -104;
								}
								break;
							}
							else
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(5) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nSeqNo = 0;
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -105;
								}
								break;
							}
						}
					}
					else if (IsReceivedByte((byte)ASCII.ETX))
					{
						if (!IsValidBCC((byte)ASCII.ETX))
						{
							// Log를 남긴다.
							if (bLog)
							{
								Log_Communication(Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
							}

							ResetBuff();
							if (Send((byte)ASCII.NAK))
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(6) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -104;
								}
								break;
							}
							else
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(7) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -105;
								}
								break;
							}
						}
						
						try
						{
							string strRcvedString = Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes);
							int nStart = strRcvedString.IndexOf((char)ASCII.STX);
							int nEnd = strRcvedString.IndexOf((char)ASCII.ETX);
							strRcvedString = strRcvedString.Substring(++nStart, nEnd - nStart);
							strRcvedString = strRcvedString.TrimStart(strCommand.ToCharArray());

							char[] separator = new char[1];
							separator[0] = ',';
							string[] strData = strRcvedString.Split(separator);
							separator[0] = ' ';
							foreach (string s in strData)
							{
								string[] strAtom = s.Split(separator, 2);
								if (strAtom.Length < 2) break;
								Int32 No = Convert.ToInt32(strAtom[0]);
								//Int32 Val = Convert.ToInt32(strAtom[1]);
								string strRemovedData = DeleteSpace(strAtom[1]);
								Int32 Val = (int)Convert.ToDouble(strRemovedData);
								if (No > 0 && No <= readData.Length)
								{
									readData[No - 1] = Val;
								}
							}
							nSeqNo = 0;
							bProcessing = false;
							ResetBuff();

							if (Send((byte)ASCII.EOT))
							{
								nReadRetryNo = 0;
								nRet = 1;
							}
							else
							{
								nReadRetryNo = 0;
								nRet = -106;
							}
						}
						catch (System.Exception ex)
						{
							string strMSg = "";
							strMSg = string.Format("SRZ Module ETX Process Error! {0} [RCV] {1}", ex.Message, Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
							LogTextOut(strMSg);
							ResetBuff();
						}
						
					}
					else if (IsReceivedByte((byte)ASCII.EOT))
					{
						if (bLog)
						{
							Log_Communication(Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
						}

						nReadRetryNo++;
						Log_Communication("SeqReadData(8) Retry Count : " + nReadRetryNo.ToString());
						if (nReadRetryNo > RETRY_NO)
						{
							nReadRetryNo = 0;
							nRet = -121;
						}
						ResetBuff();
						nSeqNo = 0;
						bProcessing = false;
					}
					else if (GetElapseTime(dtReadTime) > COMMUNICATION_DELAY_TIME)
					{
						if (Send((byte)ASCII.EOT))
						{
							nReadRetryNo++;
							Log_Communication("SeqReadData(9) Retry Count : " + nReadRetryNo.ToString());
							if (nReadRetryNo > RETRY_NO)
							{
								nReadRetryNo = 0;
								nRet = -108;
							}
							ResetBuff();
							nSeqNo = 0;
							bProcessing = false;
						}
						else
						{
							nReadRetryNo = 0;
							nRet = -106;
						}
					}
					break;
				case 200:
					if (Send((byte)ASCII.ACK))
					{
						nSeqNo = 100;
						dtReadTime = DateTime.Now;
					}
					else
					{
						nSeqNo = 0;
						nReadRetryNo = 0;
						nRet = -203;
						bProcessing = false;
						ResetBuff();
					}
					break;
				default:
					nSeqNo = 0;
					nReadRetryNo = 0;
					nRet = -4;
					bProcessing = false;
					ResetBuff();
					break;
			}

			nReadSeqNo = nSeqNo;

			return nRet;
		}

		public int SeqReadData(UInt16 devID, string strCommand, ref float[] readData)
		{
			int nRet = 0;
			int nSeqNo = nReadSeqNo;

			switch (nSeqNo)
			{
				case 0:
					if (bProcessing) { break; }
					if (SendReadCommand(devID, strCommand))
					{
						nSeqNo = 100;
						bProcessing = true;
						dtReadTime = DateTime.Now;
					}
					else
					{
						nReadRetryNo++;
						Log_Communication("SeqReadData(10) Retry Count : " + nReadRetryNo.ToString());
						if (nReadRetryNo > RETRY_NO)
						{
							nSeqNo = 0;
							bProcessing = false;
							nReadRetryNo = 0;
							nRet = -1;
						}
					}
					break;
				case 100:
					if (IsReceivedByte((byte)ASCII.ETB))
					{
						if (!IsValidBCC((byte)ASCII.ETB))
						{
							// Log를 남긴다.
							if (bLog)
							{
								Log_Communication(Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
							}

							ResetBuff();
							if (Send((byte)ASCII.NAK))
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(11) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nSeqNo = 0;
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -102;
								}
								break;
							}
							else
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(12) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nSeqNo = 0;
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -103;
									//break;
								}
								break;
							}
						}

						try
						{
							string strRcvedString = Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes);
							int nStart = strRcvedString.IndexOf((char)ASCII.STX);
							int nEnd = strRcvedString.IndexOf((char)ASCII.ETB);
							strRcvedString = strRcvedString.Substring(++nStart, nEnd - nStart);
							strRcvedString = strRcvedString.TrimStart(strCommand.ToCharArray());

							char[] separator = new char[1];
							separator[0] = ',';
							string[] strData = strRcvedString.Split(separator);
							separator[0] = ' ';

							foreach (string s in strData)
							{
								string[] strAtom = s.Split(separator, 2);
								if (strAtom.Length < 2) break;
								Int32 No = Convert.ToInt32(strAtom[0]);
								string strRemovedData = DeleteSpace(strAtom[1]);
								double dVal = Convert.ToDouble(strRemovedData);
								if (No > 0 && No <= readData.Length)
								{
									readData[No - 1] = (float)dVal;
								}
							}
							ResetBuff();
							nSeqNo = 200;
						}
						catch
						{
							ResetBuff();
							if (Send((byte)ASCII.NAK))
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(13) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nSeqNo = 0;
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -104;
								}
								break;
							}
							else
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(14) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nSeqNo = 0;
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -105;
								}
								break;
							}
						}
					}
					else if (IsReceivedByte((byte)ASCII.ETX))
					{
						if (!IsValidBCC((byte)ASCII.ETX))
						{
							// Log를 남긴다.
							if (bLog)
							{
								Log_Communication(Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
							}

							ResetBuff();
							if (Send((byte)ASCII.NAK))
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(15) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -104;
								}
								break;
							}
							else
							{
								nReadRetryNo++;
								Log_Communication("SeqReadData(16) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -105;
								}
								break;
							}
						}

						try
						{
							//OK RCV
							string strRcvedString = Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes);
							int nStart = strRcvedString.IndexOf((char)ASCII.STX);
							int nEnd = strRcvedString.IndexOf((char)ASCII.ETX);
							strRcvedString = strRcvedString.Substring(++nStart, nEnd - nStart);
							strRcvedString = strRcvedString.TrimStart(strCommand.ToCharArray());

							char[] separator = new char[1];
							separator[0] = ',';
							string[] strData = strRcvedString.Split(separator);
							separator[0] = ' ';
							foreach (string s in strData)
							{
								string[] strAtom = s.Split(separator, 2);
								if (strAtom.Length < 2) break;
								Int32 No = Convert.ToInt32(strAtom[0]);
								string strRemovedData = DeleteSpace(strAtom[1]);
								double dVal = Convert.ToDouble(strRemovedData);
								if (No > 0 && No <= readData.Length)
								{
									readData[No - 1] = (float)dVal;
								}
							}
							nSeqNo = 0;
							bProcessing = false;
							ResetBuff();

							if (Send((byte)ASCII.EOT))
							{
								nReadRetryNo = 0;
								nRet = 1;
							}
							else
							{
								nReadRetryNo = 0;
								nRet = -106;
							}
						}
						catch (System.Exception ex)
						{
							string strMSg = "";
							strMSg = string.Format("SRZ Module ETX Process Error! {0} [RCV] {1}", ex.Message, Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));

							ResetBuff();
							LogTextOut(strMSg);
						}
						
					}
					else if (IsReceivedByte((byte)ASCII.EOT))
					{
						// Log를 남긴다.
						if (bLog)
						{
							Log_Communication(Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
						}

						nReadRetryNo++;
						Log_Communication("SeqReadData(17) Retry Count : " + nReadRetryNo.ToString());
						if (nReadRetryNo > RETRY_NO)
						{
							nReadRetryNo = 0;
							nRet = -121;
						}
						ResetBuff();
						nSeqNo = 0;
						bProcessing = false;
					}
					else if (GetElapseTime(dtReadTime) > COMMUNICATION_DELAY_TIME)
					{
						if (Send((byte)ASCII.EOT))
						{
							nReadRetryNo++;
							Log_Communication("SeqReadData(18) Retry Count : " + nReadRetryNo.ToString());
							if (nReadRetryNo > RETRY_NO)
							{
								nReadRetryNo = 0;
								nRet = -108;
							}
							ResetBuff();
							nSeqNo = 0;
							bProcessing = false;
						}
						else
						{
							nReadRetryNo = 0;
							nRet = -106;
						}
					}
					break;
				case 200:
					if (Send((byte)ASCII.ACK))
					{
						nSeqNo = 100;
						dtReadTime = DateTime.Now;
					}
					else
					{
						nSeqNo = 0;
						nReadRetryNo = 0;
						nRet = -203;
						bProcessing = false;
						ResetBuff();
					}
					break;
				default:
					nSeqNo = 0;
					nReadRetryNo = 0;
					nRet = -4;
					bProcessing = false;
					ResetBuff();
					break;
			}

			nReadSeqNo = nSeqNo;
			return nRet;
		}
		public int SeqSRZRun(UInt16 devID, bool isRun)
		{	// [2012-07-24] by Changjin.Jeong : 스타트 모드가 핫스타트 일 때, 비정상 종료 시 아웃값이 풀값이더라도,
			// [2012-07-24] by Changjin.Jeong : 다시시작 할 때 시작값을 0부터 시작한다.
			int nRet = 0;
			int nSeqNo = nWriteSeqNo;

			switch (nSeqNo)
			{
				case 0:
					if (bProcessing) { break; }
					if (SendWriteCommandForSRZRUN(devID, isRun) > 0)
					{
						nSeqNo = 100;
						bProcessing = true;
						dtWriteTime = DateTime.Now;
						ResetBuff(); // 2010.08.30
					}
					else
					{
						nSeqNo = 0;
						bProcessing = false;
						nWriteRetryNo = 0;
						nRet = -1;
					}
					break;
				case 100:
					if (IsReceivedByte((byte)ASCII.ACK))
					{
						bProcessing = false;
						nSeqNo = 0;
						nRet = 1;
						nWriteRetryNo = 0;
						ResetBuff(); // 2010.08.30
					}
					else if (IsReceivedByte((byte)ASCII.NAK))
					{
						bProcessing = false;
						nSeqNo = 0;
						nWriteRetryNo++;
						if (nWriteRetryNo > RETRY_NO)
						{
							nWriteRetryNo = 0;
							nRet = -2;
						}
					}
					else if (GetElapseTime(dtWriteTime) > COMMUNICATION_DELAY_TIME)
					{
						bProcessing = false;
						nSeqNo = 0;
						nWriteRetryNo++;
						if (nWriteRetryNo > RETRY_NO)
						{
							nWriteRetryNo = 0;
							nRet = -3;
						}
					}
					break;
				default:
					nRet = -4;
					nWriteRetryNo = 0;
					bProcessing = false;
					nSeqNo = 0;
					break;
			}

			nWriteSeqNo = nSeqNo;

			return nRet;
		}
		public int SeqWriteData(UInt16 devID, string strCode, ref int[] nValues, int nLen, int nStartNo, int nDataDigits)
		{
			int nRet = 0;
			int nSeqNo = nWriteSeqNo;

			switch (nSeqNo)
			{
				case 0:
					if (bProcessing) { break; }
					if (SendWriteCommandForSRZ(devID, strCode, nValues, nLen, nStartNo, nDataDigits) > 0)
					{
						nSeqNo = 100;
						bProcessing = true;
						dtWriteTime = DateTime.Now;

						ResetBuff(); // 2010.08.30
					}
					else
					{
						nSeqNo = 0;
						bProcessing = false;
						nWriteRetryNo = 0;
						nRet = -1;
					}
					break;
				case 100:
					if (IsReceivedByte((byte)ASCII.ACK))
					{
						bProcessing = false;
						nSeqNo = 0;
						nRet = 1;
						nWriteRetryNo = 0;
						ResetBuff(); // 2010.08.30
					}
					else if (IsReceivedByte((byte)ASCII.NAK))
					{
						bProcessing = false;
						nSeqNo = 0;
						nWriteRetryNo++;
						if (nWriteRetryNo > RETRY_NO)
						{
							nWriteRetryNo = 0;
							nRet = -2;
						}
					}
					else if (GetElapseTime(dtWriteTime) > COMMUNICATION_DELAY_TIME)
					{
						bProcessing = false;
						nSeqNo = 0;
						nWriteRetryNo++;
						if (nWriteRetryNo > RETRY_NO)
						{
							nWriteRetryNo = 0;
							nRet = -3;
						}
					}
					break;
				default:
					nRet = -4;
					nWriteRetryNo = 0;
					bProcessing = false;
					nSeqNo = 0;
					break;
			}

			nWriteSeqNo = nSeqNo;

			return nRet;
		}

		public int SeqWriteDataForDIO(UInt16 devID, string strCode, ref int[] nValues, int nLen, int nChannelNo, int nDataDigits)
		{
			int nRet = 0;
			int nSeqNo = nWriteSeqNo;

			switch (nSeqNo)
			{
				case 0:
					if (bProcessing) { break; }
					if (SendWriteCommandForSRZDIO(devID, strCode, nValues, nLen, nChannelNo, nDataDigits) > 0)
					{
						nSeqNo = 100;
						bProcessing = true;
						dtWriteTime = DateTime.Now;
						ResetBuff(); // 2010.08.30
					}
					else
					{
						nSeqNo = 0;
						bProcessing = false;
						nWriteRetryNo = 0;
						nRet = -1;
					}
					break;
				case 100:
					if (IsReceivedByte((byte)ASCII.ACK))
					{
						bProcessing = false;
						nSeqNo = 0;
						nRet = 1;
						nWriteRetryNo = 0;
						ResetBuff(); // 2010.08.30
					}
					else if (IsReceivedByte((byte)ASCII.NAK))
					{
						bProcessing = false;
						nSeqNo = 0;
						nWriteRetryNo++;
						if (nWriteRetryNo > RETRY_NO)
						{
							nWriteRetryNo = 0;
							nRet = -2;
						}
					}
					else if (GetElapseTime(dtWriteTime) > COMMUNICATION_DELAY_TIME)
					{
						bProcessing = false;
						nSeqNo = 0;
						nWriteRetryNo++;
						if (nWriteRetryNo > RETRY_NO)
						{
							nWriteRetryNo = 0;
							nRet = -3;
						}
					}
					break;
				default:
					nRet = -4;
					nWriteRetryNo = 0;
					bProcessing = false;
					nSeqNo = 0;
					break;
			}

			nWriteSeqNo = nSeqNo;

			return nRet;
		}

		public int SeqReadDataForDIO(UInt16 devID, string strCommand, ref int[] readData, int nChannelNo)
		{
			int nRet = 0;
			int nSeqNo = nReadSeqNo;

			switch (nSeqNo)
			{
				case 0:
					if (bProcessing) { break; }
					if (SendReadCommandForSRZDIO(devID, strCommand, nChannelNo))
					{
						nSeqNo = 100;
						bProcessing = true;
						dtReadTime = DateTime.Now;
					}
					else
					{
						nReadRetryNo++;
						LogTextOut("SeqReadDataForDIO(1) Retry Count : " + nReadRetryNo.ToString());
						if (nReadRetryNo > RETRY_NO)
						{
							nSeqNo = 0;
							bProcessing = false;
							nReadRetryNo = 0;
							nRet = -1;
						}
					}
					break;
				case 100:
					if (IsReceivedByte((byte)ASCII.ETB))
					{
						if (!IsValidBCC((byte)ASCII.ETB))
						{
							// Log를 남긴다.
							if (bLog)
							{
								Log_Communication(Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
							}

							ResetBuff();
							if (Send((byte)ASCII.NAK))
							{
								nReadRetryNo++;
								Log_Communication("SeqReadDataForDIO(2) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nSeqNo = 0;
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -102;
								}
								break;
							}
							else
							{
								nReadRetryNo++;
								Log_Communication("SeqReadDataForDIO(3) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nSeqNo = 0;
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -103;
									//break;
								}
								break;
							}
						}

						string strRcvedString = Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes);
						int nStart = strRcvedString.IndexOf((char)ASCII.STX);
						int nEnd = strRcvedString.IndexOf((char)ASCII.ETB);
						strRcvedString = strRcvedString.Substring(++nStart, nEnd - nStart);
						strRcvedString = strRcvedString.TrimStart(strCommand.ToCharArray());

						char[] separator = new char[1];
						separator[0] = ',';
						string[] strData = strRcvedString.Split(separator);
						separator[0] = ' ';
						foreach (string s in strData)
						{
							string[] strAtom = s.Split(separator, 2);
							if (strAtom.Length < 2) break;
							Int32 No = Convert.ToInt32(strAtom[0]);
							//Int32 Val = Convert.ToInt32(strAtom[1]);
							string strRemovedData = DeleteSpace(strAtom[1]);
							Int32 Val = (int)Convert.ToDouble(strRemovedData);
							if (No > 0 && No <= readData.Length)
							{
								readData[No - 1] = Val;
							}
						}
						ResetBuff();
						nSeqNo = 200;
					}
					else if (IsReceivedByte((byte)ASCII.ETX))
					{
						if (!IsValidBCC((byte)ASCII.ETX))
						{
							// Log를 남긴다.
							if (bLog)
							{
								Log_Communication(Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
							}

							ResetBuff();
							if (Send((byte)ASCII.NAK))
							{
								nReadRetryNo++;
								Log_Communication("SeqReadDataForDIO(4) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -104;
								}
								break;
							}
							else
							{
								nReadRetryNo++;
								Log_Communication("SeqReadDataForDIO(5) Retry Count : " + nReadRetryNo.ToString());
								if (nReadRetryNo > RETRY_NO)
								{
									nReadRetryNo = 0;
									bProcessing = false;
									nRet = -105;
								}
								break;
							}
						}

						string strRcvedString = Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes);
						int nStart = strRcvedString.IndexOf((char)ASCII.STX);
						int nEnd = strRcvedString.IndexOf((char)ASCII.ETX);
						strRcvedString = strRcvedString.Substring(++nStart, nEnd - nStart);
						strRcvedString = strRcvedString.TrimStart(strCommand.ToCharArray());

						char[] separator = new char[1];
						separator[0] = ',';
						string[] strData = strRcvedString.Split(separator);
						separator[0] = ' ';
						foreach (string s in strData)
						{
							string[] strAtom = s.Split(separator, 2);
							if (strAtom.Length < 2) break;
							Int32 No = Convert.ToInt32(strAtom[0]);
							//Int32 Val = Convert.ToInt32(strAtom[1]);
							string strRemovedData = DeleteSpace(strAtom[1]);
							Int32 Val = (int)Convert.ToDouble(strRemovedData);
							if (No > 0 && No <= readData.Length)
							{
								readData[No - 1] = Val;
							}
						}
						nSeqNo = 0;
						bProcessing = false;
						ResetBuff();

						if (Send((byte)ASCII.EOT))
						{
							nRet = 1;
							nReadRetryNo = 0;
						}
						else
						{
							nReadRetryNo = 0;
							nRet = -106;
						}
					}
					else if (IsReceivedByte((byte)ASCII.EOT))
					{
						// Log를 남긴다.
						if (bLog)
						{
							Log_Communication(Encoding.ASCII.GetString(byRcvBuff, 0, (int)nRcvBytes));
						}

						nReadRetryNo++;
						Log_Communication("SeqReadDataForDIO(6) Retry Count : " + nReadRetryNo.ToString());
						if (nReadRetryNo > RETRY_NO)
						{
							nReadRetryNo = 0;
							nRet = -121;
						}
						ResetBuff();
						nSeqNo = 0;
						bProcessing = false;
					}
					else if (GetElapseTime(dtReadTime) > COMMUNICATION_DELAY_TIME)
					{
						if (Send((byte)ASCII.EOT))
						{
							nReadRetryNo++;
							Log_Communication("SeqReadDataForDIO(7) Retry Count : " + nReadRetryNo.ToString());
							if (nReadRetryNo > RETRY_NO)
							{
								nReadRetryNo = 0;
								nRet = -108;
							}
							ResetBuff();
							nSeqNo = 0;
							bProcessing = false;
						}
						else
						{
							nReadRetryNo = 0;
							nRet = -106;
						}
					}
					break;
				case 200:
					if (Send((byte)ASCII.ACK))
					{
						nSeqNo = 100;
						dtReadTime = DateTime.Now;
					}
					else
					{
						nSeqNo = 0;
						nReadRetryNo = 0;
						nRet = -203;
						bProcessing = false;
						ResetBuff();
					}
					break;
				default:
					nReadRetryNo = 0;
					nSeqNo = 0;
					nRet = -4;
					bProcessing = false;
					ResetBuff();
					break;
			}
			nReadSeqNo = nSeqNo;
			return nRet;
		}
	}
}
