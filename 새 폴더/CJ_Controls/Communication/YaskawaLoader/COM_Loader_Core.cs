using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.ComponentModel;
using CJ_Controls;

// 2014. 07. 16 ChangJin.Jeong
// 심천 Century CIM PC에 들어갈 때 적용함.
namespace CJ_Controls.Communication.YaskawaLoader
{
	public class COM_Loader_Core : Component
	{
		#region Event Message
		public delegate void SendEventHandler(object sender, SendRcvEventArgs args);
		public event SendEventHandler SendEvent;
		public void Event_Send(byte[] _RcvData, uint nLength)
		{
			if (SendEvent != null)
				SendEvent(this, new SendRcvEventArgs(_RcvData, nLength));
		}

		public delegate void ReceiveEventHandler(object sender, SendRcvEventArgs args);
		public event ReceiveEventHandler ReceiveEvent;
		public void Event_Rcv(byte[] _RcvData, uint nLength)
		{
			if (ReceiveEvent != null)
				ReceiveEvent(this, new SendRcvEventArgs(_RcvData, nLength));
		}
		#endregion

		public COM_Loader_Core()
		{
			_RcvThread = new Thread(new ThreadStart(Run));
			_RcvThread.IsBackground = true;
			_RcvThread.Start();

			//동시에 오면 뒤에꺼는 이벤트를 발생을 안시켜줄때가 간혹 있다..스레드로 계속 체크 하자..
			//_Serial.DataReceived += new SerialDataReceivedEventHandler(OnSerialRcv);
		}

		private Thread _RcvThread;

		private string m_strComPort = "COM1";
		[Category("Loader Com Port"), Description("Port Name"), DefaultValue(false)]
		public string ComPort
		{
			get { return m_strComPort; }
			set { m_strComPort = value; }
		}

		private int m_nBaudRate = 9600;
		[Category("Loader Com Port"), Description("BaudRate"), DefaultValue(false)]
		public int BaudRate
		{
			get { return m_nBaudRate; }
			set { m_nBaudRate = value; }
		}

		private bool m_bYAlignmentUse = true;
		[Category("Loader Y Alignment"), Description("Use / NotUse"), DefaultValue(false)]
		public bool YAlignmentUse
		{
			get { return m_bYAlignmentUse; }
			set { m_bYAlignmentUse = value; }
		}

		private string m_strSendCommand = "";
		public string SendCommand
		{
			get { return m_strSendCommand; }
		}

		private string COMMAND_EQPTOLOADER = "1"; // Loader로 보내는 메세지
		//private string COMMAND_LOADERTOEQP = "2"; // Loader로 보내는 메세지

		private const int MAX_BUFFER_SIZE = 1024;
		private SerialPort _Serial = new SerialPort();
		private Queue<byte[]> _RcvQ = new Queue<byte[]>();
		private byte[] byRcvBuff = new byte[MAX_BUFFER_SIZE]; //남은 데이터 담아지는 공간.
		private uint nRcvBytes = 0;

		public bool IsOpen()
		{
			return _Serial.IsOpen;
		}
		private bool Send(string strSend)
		{
			if (IsOpen().Equals(false))
				return false;

			byte[] _Data = new byte[strSend.Length];
			for (int i = 0; i < _Data.Length; i++)
			{
				_Data[i] = (byte)strSend[i];
			}

			return Send(_Data, 0, _Data.Length);
		}
		private bool Send(byte[] buffer, int offset, int count)
		{
			if (IsOpen().Equals(false))
				return false;

			_Serial.Write(buffer, offset, count);

			return true;
		}
		private bool Send(byte strSend)
		{
			if (IsOpen().Equals(false))
				return false;
			byte[] Snd = new byte[2];
			Snd[0] = strSend;
			_Serial.Write(Snd, 0, 1);
			return true;
		}
		public bool Open()
		{
			if (IsOpen())  //열렸냐?
			{
				Close(); // 닫아라.
				Thread.Sleep(100);
			}

			_Serial.PortName = m_strComPort;
			_Serial.BaudRate = m_nBaudRate;
			_Serial.Encoding = Encoding.Default;
			_Serial.Parity = Parity.Even;
			_Serial.DataBits = 8;
			_Serial.StopBits = StopBits.One;

			try
			{
				_RcvQ.Clear();
				ResetBuff();
				_Serial.Open();
				
			}
			catch// (System.Exception ex)
			{
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
		private void ResetBuff()
		{
			nRcvBytes = 0;
			m_strSendCommand = "";
			Array.Clear(byRcvBuff, 0, byRcvBuff.Length);
		}
		void OnSerialRcv(object sender, SerialDataReceivedEventArgs e)
		{
			ReadSerialData(sender);
		}
		private void ReadSerialData(object sender)
		{
			SerialPort sp = (SerialPort)sender;
			if (sp.IsOpen == false)
				return;

			int nBytes = sp.BytesToRead;
			if (nBytes > 0)
			{
				byte[] strBuff = new byte[nBytes];
				sp.Read(strBuff, 0, nBytes);
#if DEBUG
				Debug_Console_Write(strBuff);
#endif
				if (nBytes <= 3)
				{//메뉴얼상에 ACK는 1자리, NAK는 3자리씩(에러코드포함) 온다.
					if (strBuff[0] == (char)ASCII.ACK
						|| strBuff[0] == (char)ASCII.NAK)
					{
						m_strSendCommand = "";
						Q_Enqueue(strBuff);
					}
					else
					{//그런 경우는 없겠지만,, 끝에 몇바이트가 짤려서 들어올 경우...ACK, NAK가 아니면 넣는다.
						Process_OneData(strBuff, (uint)nBytes);
					}
				}
				else
				{
					int nEnq = Array.IndexOf(strBuff, (byte)ASCII.ENQ, 0);
					if (nEnq == -1)
					{
						//시작 값이 없으므로 그냥 넣는다..
						Process_OneData(strBuff, (uint)strBuff.Length);
					}
					else
					{
						if (nEnq > 0)
						{//ENQ데이터가  ACK,NAK 바로 뒤에 붙어 오는 경우.
							if (nEnq <= 3)
							{
								if (strBuff[0] == (char)ASCII.ACK
									|| strBuff[0] == (char)ASCII.NAK)
								{
									byte[] strAckNak = new byte[nEnq];
									Array.Copy(strBuff, 0, strAckNak, 0, nEnq);
									m_strSendCommand = "";
									Q_Enqueue(strAckNak);
								}
							}
						}

						int nDataLen = nEnq + 1 + 2; //ENQ+DataLength
						nDataLen += (strBuff[nEnq + 1]); //데이터 Low값 뿔라스
						nDataLen += ((strBuff[nEnq + 2] & 0xFF00) >> 8);//데이터 High값 뿔라스.
						nDataLen += 2; //BCC
						int nEnq2 = 0;
						if (strBuff.Length > nDataLen)
							nEnq2 = Array.IndexOf(strBuff, (byte)ASCII.ENQ, nDataLen);
						if (nEnq2 > 0)
						{//ENQ가 하나 더있고, 뒤에 더있을지 모르니,,, do while...
							do
							{
								byte[] strRcv = new byte[nEnq2 - nEnq];
								Array.Copy(strBuff, nEnq, strRcv, 0, strRcv.Length);
								Process_OneData(strRcv, (uint)strRcv.Length);

								nEnq = nEnq2;
								nDataLen = nEnq + 1 + 2; //ENQ+DataLength
								nDataLen += (strBuff[nEnq + 1]); //데이터 Low값 뿔라스
								nDataLen += ((strBuff[nEnq + 2] & 0xFF00) >> 8);//데이터 High값 뿔라스.
								nDataLen += 2; //BCC
								nEnq2 = 0;
								if (strBuff.Length > nDataLen)
									nEnq2 = Array.IndexOf(strBuff, (byte)ASCII.ENQ, nEnq + 1);
							} while (nEnq2 > 0);

							byte[] strZZaTuRi = new byte[strBuff.Length - nEnq];
							Array.Copy(strBuff, nEnq, strZZaTuRi, 0, strZZaTuRi.Length);
							Process_OneData(strZZaTuRi, (uint)strZZaTuRi.Length);
						}
						else
						{
							//실제 처리해야 할 데이터..
							byte[] strRcv = new byte[strBuff.Length - nEnq];
							Array.Copy(strBuff, nEnq, strRcv, 0, strRcv.Length);
							Process_OneData(strRcv, (uint)strRcv.Length);
						}
					}
				}
			}
		}
		private void Debug_Console_Write(byte[] strBuff)
		{
			string strConsole = null;
			for (int i = 0; i < strBuff.Length; i++)
			{
				string strChar = null;
				strChar += (char)strBuff[i];
				if (strBuff[i] == (byte)ASCII.ACK)
					strChar = "[A]";
				else if (strBuff[i] == (byte)ASCII.NAK)
					strChar = "[N]";
				else if (strBuff[i] == (byte)ASCII.ENQ)
					strChar = "[E]";
				else if (strBuff[i] == (byte)ASCII.NULL)
					strChar = "[0]";
				else if (strBuff[i] == 42)
					strChar = "*";
				else
				{
					if ((strBuff[i] >= 48 && strBuff[i] <= 57) //숫자
						|| (strBuff[i] >= 65 && strBuff[i] <= 90) //대문자
						|| (strBuff[i] >= 97 && strBuff[i] <= 122)) //소문자
					{
						//걍찍고.
					}
					else
					{
						strChar = string.Format("[{0}]", strBuff[i]);
					}
				}
				strConsole += strChar;
			}
			Console.WriteLine("RCV_DATA:" + strConsole);
		}

		private void Process_OneData(byte[] strRcv, uint nBytes)
		{
			if ((nRcvBytes + nBytes) > MAX_BUFFER_SIZE)
			{
				ResetBuff();
				byRcvBuff = strRcv;
				nRcvBytes = nBytes;
				return;
			}

			//일단 담아.
			Array.Copy(strRcv, 0, byRcvBuff, nRcvBytes, nBytes);
			nRcvBytes += nBytes;

			//파싱해...
			bool bReset = false;
			int nStartPos = Array.IndexOf(byRcvBuff, (byte)ASCII.ENQ, 0);
			if (nStartPos != -1)
			{
				int nLength = nStartPos + 1 + 2; //ENQ+DataLength
				nLength += (byRcvBuff[nStartPos + 1]); //데이터 Low값 뿔라스
				nLength += ((byRcvBuff[nStartPos + 2] & 0xFF00) >> 8);//데이터 High값 뿔라스.

				if (nRcvBytes >= nLength)
				{
					byte[] _Data = new byte[nLength + 2]; //BCC 영역까지 더 복사.
					Array.Copy(byRcvBuff, nStartPos, _Data, 0, _Data.Length);
					Q_Enqueue(_Data);
					//데이터 정리 다 됏으면 지워.
					ResetBuff();
				}
				else
				{ //데이터가 덜 왔으면,, 300 msec 만 기다려 보고 지울까? 보여줄까...
					Thread _DelayThread = new Thread(new ThreadStart(Process_AbnormalData));
					_DelayThread.IsBackground = true;
					_DelayThread.Start();
				}
			}
			else
			{ //ENQ가 없으면, 그게 데이터냐??
				bReset = true;
			}

			if(bReset == true)
			{
				ResetBuff();
			}
		}
		private void Process_AbnormalData()
		{
			Thread.Sleep(300); //어브노말메세지... 일단 300 mSec 기다려보고.... 그래,,파싱해라.. 거지같넹;ㅋ
			int nStartPos = Array.IndexOf(byRcvBuff, (byte)ASCII.ENQ, 0);
			if (nStartPos != -1)
			{
				int nLength = nStartPos + 1 + 2; //ENQ+DataLength
				nLength += (byRcvBuff[nStartPos + 1]); //데이터 Low값 뿔라스
				nLength += ((byRcvBuff[nStartPos + 2] & 0xFF00) >> 8);//데이터 High값 뿔라스.

				if (nRcvBytes >= nLength)
				{//어브노말이어도... 길이가 이전하고 달라졌다면???
					return;
				}

				byte[] _Data = new byte[nLength + 2]; //BCC 영역까지 더 복사.
				Array.Copy(byRcvBuff, nStartPos, _Data, 0, _Data.Length);
				Q_Enqueue(_Data);
				//데이터 정리 다 됏으면 지워.
				ResetBuff();
			}
		}
		private void Q_Enqueue(byte[] _Data)
		{
			lock (_RcvQ)
			{
				_RcvQ.Enqueue(_Data);
			}
		}
		private void Run()
		{
			while (true)
			{
				Thread.Sleep(100);
				if (_RcvQ.Count > 0)
				{
					byte[] _Data = null;
					lock (_RcvQ)
					{
						_Data = _RcvQ.Dequeue();
					}
					if (_Data != null)
					{
						Event_Rcv(_Data, (uint)_Data.Length);
					}
				}

				try
				{
					ReadSerialData(_Serial);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		//SendMessage
		public bool ACK_Send()
		{
			bool bRtn = false;
			byte[] _Send = new byte[2];
			_Send[0] = (byte)ASCII.ACK;
			if (Send(_Send[0]))
			{
				Event_Send(_Send, 1);
				bRtn = true;
			}
			return bRtn;
		}
		public bool NAK_Send()
		{
			bool bRtn = false;
			byte[] _Send = new byte[2];
			_Send[0] = (byte)ASCII.NAK;
			if (Send(_Send[0]))
			{
				Event_Send(_Send, 1);
				bRtn = true;
			}
			return bRtn;
		}
		public bool SendRequestCommand(string strCode)
		{
			string strCommand = null;
			strCommand += (char)ASCII.ENQ;
			strCommand += (char)(strCode.Length % 256);
			strCommand += (char)(strCode.Length / 256);
			strCommand += strCode;
			MakeBCC(ref strCommand);

			bool bRtn = false;
			try
			{
				if (Send(strCommand))
				{
					byte[] _Send = new byte[strCommand.Length];
					Array.Copy(Encoding.Default.GetBytes(strCommand), 0, _Send, 0, strCommand.Length);
					Event_Send(_Send, (uint)_Send.Length);
					bRtn = true;
				}
			}
			catch
			{
				bRtn = false;
			}

			return bRtn;
		}

		public void MakeBCC(ref string strCmd)
		{
			int bcc = 0;
			int length = 0;

			length = strCmd.Length;

			for (int i = 1; i < length; i++) // 1부터 시작하는 이유 : ENQ 제외
			{
				bcc += strCmd[i];
			}
			strCmd += (char)(bcc & 0x00FF);
			strCmd += (char)((bcc & 0xFF00) >> 8);
		}

		private string MakeString(string str, int nCnt)
		{
			string strRtn = "";
			for (int i = 0; i < nCnt; i++)
			{
				strRtn += str;
			}
			return strRtn;
		}

		#region EQP To LOADER
		//6.1 Remote Status Confirm
		public bool SendRemoteStatusConfirm()
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R00" + MakeString("*", 8);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.2 Confirm cassette present status
		public bool SendConfirmCassettePresentStatus()
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R01" + MakeString("*", 8);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.3 Confirm Port status
		public bool SendConfirmPortStatus()
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R02" + MakeString("*", 8);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.7 Request for return to origin
		public bool SendRequestForReturnToOrigin(ORIGIN_UNIT_NO nRequestOriginUnitNo)
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R32" + (int)nRequestOriginUnitNo + MakeString("*", 7);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.8 Request for return to initial status
		public bool SendRequestForReturnToInitialStatus()
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R33" + MakeString("*", 8);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.9 Request for return to initial status
		public bool SendRequestConfirmAlarmStatus()
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R35" + MakeString("*", 8);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.10 AGV/MGV mode change request
		public bool SendRequestPortModeChange(int nPortNo, bool bAGV_Mode)
		{
			string strMode = (bAGV_Mode == true) ? "A" : "M";
			m_strSendCommand = COMMAND_EQPTOLOADER + "R37" + nPortNo + strMode + MakeString("*", 6);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.11 Mode setting of cassette station ( Enable/Disable)
		public bool SendRequestPortEnable(int nPort, bool bEnable)
		{
			string strEnable = (bEnable == true) ? "E" : "D";
			m_strSendCommand = COMMAND_EQPTOLOADER + "R38" + nPort + strEnable + MakeString("*", 6);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.12 Request for start preparation
		public bool SendRequestForStartPreparation(int nRequestPortNo, int[] nGlassSlot)
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R60" + nRequestPortNo;

			byte[] _SlotData = SetTransferGlassSlot(nGlassSlot);
			for (int i = 0; i < _SlotData.Length; i++)
				m_strSendCommand += (char)_SlotData[i];

			m_strSendCommand += MakeString("*", 3);
			return SendRequestCommand(m_strSendCommand);
		}
		public byte[] SetTransferGlassSlot(int[] GlassSlot)
		{
			byte[] _SlotData = { 0, 0, 0, 0 };

			for (int i = 0; i < 4; i++)
			{
				_SlotData[i] += (1 << 7); //고정
				int nStartSlot = i * 7;
				if (nStartSlot < GlassSlot.Length && GlassSlot[nStartSlot++] == 1) { _SlotData[i] += (1 << 6); }
				if (nStartSlot < GlassSlot.Length && GlassSlot[nStartSlot++] == 1) { _SlotData[i] += (1 << 5); }
				if (nStartSlot < GlassSlot.Length && GlassSlot[nStartSlot++] == 1) { _SlotData[i] += (1 << 4); }
				if (nStartSlot < GlassSlot.Length && GlassSlot[nStartSlot++] == 1) { _SlotData[i] += (1 << 3); }
				if (nStartSlot < GlassSlot.Length && GlassSlot[nStartSlot++] == 1) { _SlotData[i] += (1 << 2); }
				if (nStartSlot < GlassSlot.Length && GlassSlot[nStartSlot++] == 1) { _SlotData[i] += (1 << 1); }
				if (nStartSlot < GlassSlot.Length && GlassSlot[nStartSlot++] == 1) { _SlotData[i] += (1 << 0); }
			}
			return _SlotData;
		}

		//6.13 Request substrate transfer
		public bool SendRequestEQPForSubstrateTransfer(TransferInfo _TransInfo)
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R61" + MakeManualSubstrateCommand(_TransInfo) + MakeString("*", 3);
			return SendRequestCommand(m_strSendCommand);
		}
		private string MakeManualSubstrateCommand(TransferInfo _TransInfo)
		{
			string strCmd = "*9";
			strCmd += (int)_TransInfo.OperationType;
			strCmd += (int)_TransInfo.RobotHand;

			if ((int)_TransInfo.OperationType == 2)
			{
				strCmd += string.Format("7");
				strCmd += string.Format("01");
			}
			else
			{
				strCmd += string.Format("{0:X}", (int)_TransInfo.Stage);
				strCmd += string.Format("{0:d02}", _TransInfo.Slot);
			}
			strCmd += (int)_TransInfo.Thickness;
			return strCmd;
		}

		//6.14 Request to stop to transfer the substrate
		public bool SendRequestRobotPauseResume(char PauseCmd)
		{
			string strPause = null;
			strPause += (char)PauseCmd;
			m_strSendCommand = COMMAND_EQPTOLOADER + "R631" + strPause + MakeString("*", 6);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.16 Inform cassette is available to moved out
		public bool SendRequestPortUnclamp(int nPort)
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R73" + nPort + MakeString("*", 6);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.17 Request for rechuck
		public bool SendRequestPortRechuck(int nPortNo)
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R81" + nPortNo + MakeString("*", 7);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.18 Request for mapping
		public bool SendRequestForMapping(int nPortNo)
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R82" + nPortNo + MakeString("*", 7);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.20 System Date Setting
		public bool SendRequestSystemDateSetting()
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R87" + MakeSystemDateSetting() + "*";
			return SendRequestCommand(m_strSendCommand);
		}
		private string MakeSystemDateSetting()
		{
			DateTime _dtNow = DateTime.Now;
			string strCommand = "";
			strCommand += String.Format("{0:d04}", _dtNow.Year);   // 16진수로 변환
			strCommand += String.Format("{0:d02}", _dtNow.Month);  // 16진수로 변환
			strCommand += String.Format("{0:d02}", _dtNow.Day);    // 16진수로 변환
			strCommand += String.Format("{0:d02}", _dtNow.Hour);   // 16진수로 변환
			strCommand += String.Format("{0:d02}", _dtNow.Minute); // 16진수로 변환
			strCommand += String.Format("{0:d02}", _dtNow.Second); // 16진수로 변환

			switch (_dtNow.DayOfWeek)
			{
				case DayOfWeek.Monday:		strCommand += "1"; break;
				case DayOfWeek.Tuesday:		strCommand += "2"; break;
				case DayOfWeek.Wednesday:	strCommand += "3"; break;
				case DayOfWeek.Thursday:	strCommand += "4"; break;
				case DayOfWeek.Friday:		strCommand += "5"; break;
				case DayOfWeek.Saturday:	strCommand += "6"; break;
				case DayOfWeek.Sunday:		strCommand += "0"; break;
				default:
					strCommand += "1";
				break;
			}

			return strCommand;
		}

		//6.22 Alarm Reset
		public bool SendRequestAlarmReset()
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "R910" + MakeString("*", 7);
			return SendRequestCommand(m_strSendCommand);
		}
		#endregion

		/* 이거는 로더일때만 사용하거라..
		#region LOADER To EQP
		//6.1 Informremote confirmation end
		public bool RcvRemoteStatusConfirm(bool bRemote, int nVer, bool[] bAGV)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A00";
			m_strSendCommand += "Gn11"; //고정.
			m_strSendCommand += (bRemote == true) ? "1" : "2";
			m_strSendCommand += string.Format("{0:d03}", nVer);
			for (int i = 0; i < bAGV.Length; i++)
			{
				if (i < 6)
					m_strSendCommand += (bAGV[i] == true) ? "A" : "M";
				else
					break;
			}
			if (bAGV.Length < 6)
			{
				for(int i = 0; i < (6-bAGV.Length); i++)
					m_strSendCommand += "*";
			}

			return SendRequestCommand(m_strSendCommand);
		}

		//6.2 Reply cassette present status
		public bool RcvConfirmCassettePresentStatus(bool bRemote, CASSETTE_STATUS[] _Status)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A01";
			m_strSendCommand += (bRemote == true) ? "S" : "D";
			for (int i = 0; i < _Status.Length; i++)
			{
				if (i < 6)
				{
					switch (_Status[i])
					{
						case CASSETTE_STATUS.NO_CASSETTE:   m_strSendCommand += "0"; break;
						case CASSETTE_STATUS.HAVE_CASSETTE: m_strSendCommand += "1"; break;
						case CASSETTE_STATUS.ERROR_STATUS:  m_strSendCommand += "E"; break;
						case CASSETTE_STATUS.NOT_DEFINED:   m_strSendCommand += "*"; break;
					}
				}
				else
					break;
			}
			if (_Status.Length < 6)
			{
				for (int i = 0; i < (6 - _Status.Length); i++)
					m_strSendCommand += "*";
			}
			m_strSendCommand += "*"; //마지막 문자.

			return SendRequestCommand(m_strSendCommand);
		}

		//6.3 Reply cassette present status
		public bool RcvConfirmPortStatus(bool bRemote, PORT_STATUS[] _Status)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A02";
			m_strSendCommand += (bRemote == true) ? "S" : "D";
			for (int i = 0; i < _Status.Length; i++)
			{
				if (i < 6)
				{
					m_strSendCommand += string.Format("{0}", (int)_Status[i]);
				}
				else
					break;
			}
			if (_Status.Length < 6)
			{
				for (int i = 0; i < (6 - _Status.Length); i++)
					m_strSendCommand += "0";
			}
			m_strSendCommand += "*"; //마지막 문자.

			return SendRequestCommand(m_strSendCommand);
		}

		//6.4 Inform cassette status change
		public bool RcvInformCassetteStatusChange(CASSETTE_STATUS[] _Status)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A11";
			for (int i = 0; i < _Status.Length; i++)
			{
				if (i < 6)
				{
					switch (_Status[i])
					{
						case CASSETTE_STATUS.NO_CASSETTE: m_strSendCommand += "0"; break;
						case CASSETTE_STATUS.HAVE_CASSETTE: m_strSendCommand += "1"; break;
						case CASSETTE_STATUS.ERROR_STATUS: m_strSendCommand += "E"; break;
						case CASSETTE_STATUS.NOT_DEFINED: m_strSendCommand += "*"; break;
					}
				}
				else
					break;
			}
			if (_Status.Length < 6)
			{
				for (int i = 0; i < (6 - _Status.Length); i++)
					m_strSendCommand += "*";
			}
			m_strSendCommand += "*"; //마지막 문자.

			return SendRequestCommand(m_strSendCommand);
		}

		//6.5 Inform loader mode change
		public bool RcvInformLoaderModeChange(bool bRemote, bool bPause, bool[] bAGV)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A12";
			m_strSendCommand += (bRemote == true) ? "1" : "2";
			m_strSendCommand += "0"; //Fixed
			m_strSendCommand += (bPause == true) ? "1" : "0";
			for (int i = 0; i < bAGV.Length; i++)
			{
				if (i < 6)
					m_strSendCommand += (bAGV[i] == true) ? "A" : "M";
				else
					break;
			}
			if (bAGV.Length < 6)
			{
				for (int i = 0; i < (6 - bAGV.Length); i++)
					m_strSendCommand += "*";
			}

			return SendRequestCommand(m_strSendCommand);
		}

		//6.7 Request for return to origin
		public bool RcvRequestForReturnToOrigin(string strData, RETURN_ORIGIN_UNIT_STATUS _Status)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A32";
			m_strSendCommand += strData;
			switch (_Status)
			{
				case RETURN_ORIGIN_UNIT_STATUS.FINISH: m_strSendCommand += "S"; break;
				case RETURN_ORIGIN_UNIT_STATUS.ROBOT_ON_MOVE: m_strSendCommand += "B"; break;
				case RETURN_ORIGIN_UNIT_STATUS.ERROR: m_strSendCommand += "E"; break;
				case RETURN_ORIGIN_UNIT_STATUS.ACCEPTANCE_REJECTED: m_strSendCommand += "N"; break;
				case RETURN_ORIGIN_UNIT_STATUS.NOT_REMOTE_MODE: m_strSendCommand += "D"; break;
				case RETURN_ORIGIN_UNIT_STATUS.PARAMETER_ERROR: m_strSendCommand += "Z"; break;
			}
			m_strSendCommand += MakeString("*", 6);
			
			return SendRequestCommand(m_strSendCommand);
		}

		//6.8 Request for return to initial status
		public bool RcvRequestForReturnToInitialStatus(INITIALIZE_STATUS _Status)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A33";
			switch (_Status)
			{
				case INITIALIZE_STATUS.SUCCESS: m_strSendCommand += "S"; break;
				case INITIALIZE_STATUS.ERROR: m_strSendCommand += "E"; break;
				case INITIALIZE_STATUS.NOT_REMOTE_MODE: m_strSendCommand += "D"; break;
				case INITIALIZE_STATUS.ROBOT_ON_MOVE: m_strSendCommand += "B"; break;
				case INITIALIZE_STATUS.REFUSE_MESSAGE: m_strSendCommand += "N"; break;
			}
			m_strSendCommand += MakeString("*", 7);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.9 Request for return to initial status
		public bool RcvRequestConfirmAlarmStatus(ALARM_STATUS_RESULT _LoaderResult, ALARM_STATUS _Status,
													ALARM_STATUS_RESULT _UpHand, ALARM_STATUS_RESULT _LoHand)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A35";
			switch (_LoaderResult)
			{
				case ALARM_STATUS_RESULT.HAVE_NO_GLASS: m_strSendCommand += "S"; break;
				case ALARM_STATUS_RESULT.HAVE_GLASS: m_strSendCommand += "E"; break;
				case ALARM_STATUS_RESULT.NO_REMOTE_MODE: m_strSendCommand += "D"; break;
			}
			switch (_Status)
			{
				case ALARM_STATUS.NO_ALARM: m_strSendCommand += "E"; break;
				case ALARM_STATUS.ALARM_STATUS: m_strSendCommand += "D"; break;
			}
			switch (_UpHand)
			{
				case ALARM_STATUS_RESULT.HAVE_NO_GLASS: m_strSendCommand += "S"; break;
				case ALARM_STATUS_RESULT.HAVE_GLASS: m_strSendCommand += "E"; break;
			}
			switch (_LoHand)
			{
				case ALARM_STATUS_RESULT.HAVE_NO_GLASS: m_strSendCommand += "S"; break;
				case ALARM_STATUS_RESULT.HAVE_GLASS: m_strSendCommand += "E"; break;
			}
			m_strSendCommand += MakeString("*", 4);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.10 AGV/MGV mode change request
		public bool RcvRequestPortModeChange(AGV_MODE_CHANGE_RESULT _Result, bool[] bAGV)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A37";
			switch (_Result)
			{
				case AGV_MODE_CHANGE_RESULT.RECEIVE_ON_NORMAL: m_strSendCommand += "S"; break;
				case AGV_MODE_CHANGE_RESULT.BUSY: m_strSendCommand += "B"; break;
				case AGV_MODE_CHANGE_RESULT.REFUSE_MESSAGE: m_strSendCommand += "N"; break;
				case AGV_MODE_CHANGE_RESULT.PARAMETER_ERROR: m_strSendCommand += "Z"; break;
			}
			for (int i = 0; i < bAGV.Length; i++)
			{
				if (i < 6)
					m_strSendCommand += (bAGV[i] == true) ? "A" : "M";
				else
					break;
			}
			if (bAGV.Length < 6)
			{
				for (int i = 0; i < (6 - bAGV.Length); i++)
					m_strSendCommand += "*";
			}
			m_strSendCommand += "*";

			return SendRequestCommand(m_strSendCommand);
		}

		//6.11 Reply cassette station mode
		public bool RcvRequestPortEnable(CASSETTE_ENABLE_MODE _EnableMode, bool[] bEnable)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A38";
			switch (_EnableMode)
			{
				case CASSETTE_ENABLE_MODE.RECEIVE_ON_NORMAL: m_strSendCommand += "S"; break;
				case CASSETTE_ENABLE_MODE.NOT_REMOTE_MODE: m_strSendCommand += "D"; break;
				case CASSETTE_ENABLE_MODE.BUSY: m_strSendCommand += "B"; break;
				case CASSETTE_ENABLE_MODE.APPOINTED_PORT_HAS_NOT_BEEN_REGISTERED: m_strSendCommand += "U"; break;
				case CASSETTE_ENABLE_MODE.PARAMETER_ERROR: m_strSendCommand += "Z"; break;
			}
			for (int i = 0; i < bEnable.Length; i++)
			{
				if (i < 6)
					m_strSendCommand += (bEnable[i] == true) ? "E" : "D";
				else
					break;
			}
			if (bEnable.Length < 6)
			{
				for (int i = 0; i < (6 - bEnable.Length); i++)
					m_strSendCommand += "*";
			}
			m_strSendCommand += "*";
			return SendRequestCommand(m_strSendCommand);
		}

		//6.12 Request for start preparation
		public bool RcvRequestForStartPreparation(string strRcvData, PORT_STARTPREPARATION_RESULT _Result)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A60";
			m_strSendCommand += strRcvData;
			switch (_Result)
			{
				case PORT_STARTPREPARATION_RESULT.REQUEST_IS_ON_ACCEPTING: m_strSendCommand += "A"; break;
				case PORT_STARTPREPARATION_RESULT.ON_ERROR: m_strSendCommand += "E"; break;
				case PORT_STARTPREPARATION_RESULT.ON_WORKING: m_strSendCommand += "B"; break;
				case PORT_STARTPREPARATION_RESULT.NOT_REMOTE_MODE: m_strSendCommand += "D"; break;
				case PORT_STARTPREPARATION_RESULT.HAVE_NOT_RETURN_TO_ORIGIN: m_strSendCommand += "H"; break;
				case PORT_STARTPREPARATION_RESULT.THE_PORT_OF_APPOINT_HAS_NOT_CASSETTE: m_strSendCommand += "K"; break;
				case PORT_STARTPREPARATION_RESULT.ACCEPTANCE_REFUSAL: m_strSendCommand += "N"; break;
				case PORT_STARTPREPARATION_RESULT.THE_PORT_OF_APPOINT_HAS_NOT_BEEN_REGISTERED: m_strSendCommand += "U"; break;
				case PORT_STARTPREPARATION_RESULT.THE_PORT_OF_APPOINT_IS_ON_DISABLE: m_strSendCommand += "P"; break;
				case PORT_STARTPREPARATION_RESULT.PARAMETER_ERROR: m_strSendCommand += "Z"; break;
			}
			m_strSendCommand += "**";
			return SendRequestCommand(m_strSendCommand);
		}

		//6.13 Request substrate transfer
		public bool RcvRequestEQPForSubstrateTransfer(string strData, INFORM_THE_SUBSTRATE_TRANSFER _Result)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A61";
			m_strSendCommand += strData;

			switch (_Result)
			{
				case INFORM_THE_SUBSTRATE_TRANSFER.REQUEST_ACCEPTED: m_strSendCommand += "A"; break;
				case INFORM_THE_SUBSTRATE_TRANSFER.ERROR_HAPPENED: m_strSendCommand += "E"; break;
				case INFORM_THE_SUBSTRATE_TRANSFER.BUSY: m_strSendCommand += "B"; break;
				case INFORM_THE_SUBSTRATE_TRANSFER.REMOTE_MODE_NOT_AVAILABLE: m_strSendCommand += "D"; break;
				case INFORM_THE_SUBSTRATE_TRANSFER.RETURN_TO_ORIGINAL_WAS_NOT_EXECUTED: m_strSendCommand += "H"; break;
				case INFORM_THE_SUBSTRATE_TRANSFER.NO_SUBSTRATE_EXISTING_AT_THE_LOADING_POSITION: m_strSendCommand += "C"; break;
				case INFORM_THE_SUBSTRATE_TRANSFER.SUBSTRATE_EXISTING_AT_THE_LOADING_POSITION: m_strSendCommand += "X"; break;
				case INFORM_THE_SUBSTRATE_TRANSFER.PAUSING: m_strSendCommand += "P"; break;
				case INFORM_THE_SUBSTRATE_TRANSFER.THE_INDICATED_STAGE_IS_NOT_REGISTERED: m_strSendCommand += "U"; break;
				case INFORM_THE_SUBSTRATE_TRANSFER.MESSAGE_REJECTED: m_strSendCommand += "N"; break;
				case INFORM_THE_SUBSTRATE_TRANSFER.PARAMETER_ERROR: m_strSendCommand += "Z"; break;
			}
			m_strSendCommand += MakeString("*", 5);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.13 Inform the substrate transportation finished.
		public bool RcvRequestEQPForSubstrateTransferFinished(string strStageAndType, SUBSTRATE_TRANSPORT_RESULTS _Result, string strData)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A62";
			m_strSendCommand += strStageAndType;
			m_strSendCommand += "*";

			switch (_Result)
			{
				case SUBSTRATE_TRANSPORT_RESULTS.NORMAL_FINISHED: m_strSendCommand += "S"; break;
				case SUBSTRATE_TRANSPORT_RESULTS.ABNORMAL_FINISHED: m_strSendCommand += "E"; break;
				case SUBSTRATE_TRANSPORT_RESULTS.SUBSTRATE_CHUCKING_ERROR: m_strSendCommand += "V"; break;
			}
			m_strSendCommand += strData;
			m_strSendCommand += MakeString("*", 3);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.14 Request to stop to transfer the substrate
		public bool RcvRequestRobotPauseResume(string strData, ROBOT_PAUSE_RESUME_RESULTS _Result)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A63";
			m_strSendCommand += strData;
			switch (_Result)
			{
				case ROBOT_PAUSE_RESUME_RESULTS.FINISHED_NORMALLY: m_strSendCommand += "S"; break;
				case ROBOT_PAUSE_RESUME_RESULTS.NOT_PAUSE_CONDITION: m_strSendCommand += "R"; break;
				case ROBOT_PAUSE_RESUME_RESULTS.ERROR_OCCURRED: m_strSendCommand += "E"; break;
				case ROBOT_PAUSE_RESUME_RESULTS.NOT_REMOTE_MODE: m_strSendCommand += "D"; break;
				case ROBOT_PAUSE_RESUME_RESULTS.IN_FORCED_STOPPING: m_strSendCommand += "N"; break;
			}
			m_strSendCommand += MakeString("*", 5);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.15 Inform cassette move in/out finished
		public bool RcvInformCassetteMoveFinished(PORT_CARRYING_IN_OUT _InOut, int nPort, bool bAGV,
													PORT_CARRYING_IN_OUT_RESULT _Result, byte[] _Mapping)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A72";
			switch (_InOut)
			{
				case PORT_CARRYING_IN_OUT.MOVE_IN_CASSETTE: m_strSendCommand += "I"; break;
				case PORT_CARRYING_IN_OUT.MOVE_OUT_CASSETTE: m_strSendCommand += "O"; break;
			}
			m_strSendCommand += nPort;
			m_strSendCommand += (bAGV == true) ? "1" : "0";
			switch (_Result)
			{
				case PORT_CARRYING_IN_OUT_RESULT.FINISHED: m_strSendCommand += "A"; break;
				case PORT_CARRYING_IN_OUT_RESULT.BCR_MALFUNCTION: m_strSendCommand += "B"; break;
			}
			m_strSendCommand += MakeString("*", 12);

			for (int i = 0; i < _Mapping.Length; i++)
			{
				m_strSendCommand += (char)_Mapping[i];
			}

			return SendRequestCommand(m_strSendCommand);
		}

		//6.16 Inform cassette is available to moved out
		public bool RcvRequestPortUnclamp(string strData, PORT_UNCLAMP_RESULT _Result)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A73";
			m_strSendCommand += strData;
			switch (_Result)
			{
				case PORT_UNCLAMP_RESULT.ACTION_FINISHED: m_strSendCommand += "A"; break;
				case PORT_UNCLAMP_RESULT.ERROR_HAPPENED: m_strSendCommand += "E"; break;
				case PORT_UNCLAMP_RESULT.NOT_UNDER_REMOTE_MODE: m_strSendCommand += "D"; break;
				case PORT_UNCLAMP_RESULT.REJECTED: m_strSendCommand += "N"; break;
				case PORT_UNCLAMP_RESULT.PARAMETER_ERROR: m_strSendCommand += "Z"; break;
			}
			m_strSendCommand += MakeString("*", 6);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.17 Request for rechuck
		public bool RcvRequestPortRechuck(string strData, REPLY_PORT_RECHUCK _Result)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A81";
			m_strSendCommand += strData;
			switch (_Result)
			{
				case REPLY_PORT_RECHUCK.REQUEST_ACCEPTED: m_strSendCommand += "A"; break;
				case REPLY_PORT_RECHUCK.ERROR_HAPPENED: m_strSendCommand += "E"; break;
				case REPLY_PORT_RECHUCK.MOVING: m_strSendCommand += "B"; break;
				case REPLY_PORT_RECHUCK.REMOTE_MODE_NOT_AVAILABLE: m_strSendCommand += "D"; break;
				case REPLY_PORT_RECHUCK.RETURN_TO_ORIGINAL_NOT_BEEN_EXECUTED: m_strSendCommand += "H"; break;
				case REPLY_PORT_RECHUCK.THERE_IS_NO_CASSETTE_ON_THE_INDICATE_PORT: m_strSendCommand += "K"; break;
				case REPLY_PORT_RECHUCK.THE_INDICATED_PORT_IS_NOT_REGISTER: m_strSendCommand += "U"; break;
				case REPLY_PORT_RECHUCK.THE_COMMAND_WAS_REJECTED: m_strSendCommand += "N"; break;
				case REPLY_PORT_RECHUCK.PARAMETER_ERROR: m_strSendCommand += "Z"; break;
			}
			m_strSendCommand += MakeString("*", 6);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.18 Request for mapping
		public bool RcvRequestForMapping(string strData, REQUEST_MAPPING_PORT_RESULTS _Result)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A82";
			m_strSendCommand += strData;
			switch (_Result)
			{
				case REQUEST_MAPPING_PORT_RESULTS.REQUEST_ACCEPTED: m_strSendCommand += "A"; break;
				case REQUEST_MAPPING_PORT_RESULTS.MOVING: m_strSendCommand += "B"; break;
				case REQUEST_MAPPING_PORT_RESULTS.NOT_REMOTE_MODE: m_strSendCommand += "D"; break;
				case REQUEST_MAPPING_PORT_RESULTS.REQUEST_BEEN_REJECTED: m_strSendCommand += "N"; break;
				case REQUEST_MAPPING_PORT_RESULTS.PARAMETER_ERROR: m_strSendCommand += "Z"; break;
			}
			m_strSendCommand += MakeString("*", 6);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.18 Request for mapping
		public bool RcvRequestForMappingFinished(string strData, bool bFinished, byte[] _Mapping)
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A83";
			m_strSendCommand += "*";
			m_strSendCommand += strData;
			m_strSendCommand += "*";
			m_strSendCommand += (bFinished == true) ? "A" : "B";
			m_strSendCommand += MakeString("*", 12);
			for (int i = 0; i < _Mapping.Length; i++)
			{
				m_strSendCommand += (char)_Mapping[i];
			}
			return SendRequestCommand(m_strSendCommand);
		}

		//6.20 System Date Setting
		public bool RcvRequestSystemDateSetting()
		{
			m_strSendCommand = COMMAND_LOADERTOEQP + "A87S" + MakeSystemDateSetting() + "*";
			return SendRequestCommand(m_strSendCommand);
		}

		//6.21 Inform Alarm happened
		public bool RcvInformAlarmHappened(ALARM_HAPPENED_LEVEL _Level, ALARM_HAPPENED_EQP_DIVISION _Division, int nPort, int nCode)
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "A90";
			switch (_Level)
			{
				case ALARM_HAPPENED_LEVEL.ALL_THE_ERROR_BEEN_CANCELLED: m_strSendCommand += "0"; break;
				case ALARM_HAPPENED_LEVEL.ERROR_CAN_RETRY: m_strSendCommand += "1"; break;
				case ALARM_HAPPENED_LEVEL.ERROR_CAN_NOT_RETRY: m_strSendCommand += "2"; break;
				case ALARM_HAPPENED_LEVEL.DIRECT_TO_OFF_THE_BUZZER_IGNORE_ALL_THE_OTHER: m_strSendCommand += "3"; break;
				case ALARM_HAPPENED_LEVEL.WARNING_OPERATION_IS_CONTINUTING: m_strSendCommand += "4"; break;
			}
			switch (_Division)
			{
				case ALARM_HAPPENED_EQP_DIVISION.CONTROLLER: m_strSendCommand += "0"; break;
				case ALARM_HAPPENED_EQP_DIVISION.CASSETTE_ID: m_strSendCommand += "1"; break;
				case ALARM_HAPPENED_EQP_DIVISION.ROBOT: m_strSendCommand += "2"; break;
				case ALARM_HAPPENED_EQP_DIVISION.MAPPING_UNIT: m_strSendCommand += "3"; break;
				case ALARM_HAPPENED_EQP_DIVISION.PORT: m_strSendCommand += "4"; break;
				case ALARM_HAPPENED_EQP_DIVISION.TURN_TABLE: m_strSendCommand += "6"; break;
			}
			m_strSendCommand += nPort;
			m_strSendCommand += string.Format("{0:d03}", nCode);
			m_strSendCommand += MakeString("*", 3);
			return SendRequestCommand(m_strSendCommand);
		}

		//6.22 Alarm Reset
		public bool SendRequestAlarmReset(ALARM_RESET_RESULTS _Result)
		{
			m_strSendCommand = COMMAND_EQPTOLOADER + "A91";
			switch (_Result)
			{
				case ALARM_RESET_RESULTS.REQUEST_ACCEPTED: m_strSendCommand += "S"; break;
				case ALARM_RESET_RESULTS.NOT_REMOTE_MODE: m_strSendCommand += "D"; break;
				case ALARM_RESET_RESULTS.ALARM_IS_NOT_LEVEL_1: m_strSendCommand += "N"; break;
				case ALARM_RESET_RESULTS.PARAMETER_ERROR: m_strSendCommand += "Z"; break;
			}
			m_strSendCommand += MakeString("*", 7);
			return SendRequestCommand(m_strSendCommand);
		}
		#endregion
		*/
	}

	public class TransferInfo
	{
		public TransferInfo()
		{
		}

		public void Clear()
		{
			OperationType = OPERATION_TYPE.NONE;
			Thickness = THICKNESS.THICKNESS_0_5;
			Stage = STAGE.PORT_1;
			RobotHand = ROBOT_HAND.HAND_LOWER;
			Slot = 0;
		}
		public OPERATION_TYPE OperationType = OPERATION_TYPE.NONE;
		public THICKNESS Thickness = THICKNESS.THICKNESS_0_5;
		public STAGE Stage = STAGE.PORT_1;
		public ROBOT_HAND RobotHand = ROBOT_HAND.HAND_LOWER;
		public int Slot = 0;
	}

	public class SendRcvEventArgs : EventArgs
	{
		private byte[] m_SendRcv;
		private uint m_nLength = 0;
		public SendRcvEventArgs(byte[] _SendRcv, uint nLength)
		{
			this.m_SendRcv = _SendRcv;
			this.m_nLength = nLength;
		}

		public byte[] Data
		{
			get { return this.m_SendRcv; }
		}
		public uint DataLength
		{
			get { return this.m_nLength; }
		}
	}
}
