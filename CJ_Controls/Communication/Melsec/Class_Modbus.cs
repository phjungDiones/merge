using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using CJ_Controls;

namespace CJ_Controls.Communication
{
	public class Class_Modbus
	{
		#region Log 보내기
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public event MessageEventHandler MessageEvent;

		private void LogTextOut(string message)
		{
			if (MessageEvent != null)
				MessageEvent(this, new MessageEventArgs(message));
		}
		#endregion

		private readonly int RETRY_NO = 5;
		private readonly int WAITTIME = 500;
		private System.IO.Ports.SerialPort m_SerialPort = new System.IO.Ports.SerialPort();

		private byte[] m_ReceiveBuffer = new byte[1024];

		public SerialPort ModbusPort
		{
			get { return m_SerialPort; }
		}
		public Class_Modbus()
		{
			m_SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
		}

		private int m_ReadCount = 0;
		private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
 			while (m_SerialPort.BytesToRead > 0)
 			{
				SerialPort sp = (SerialPort)sender;
 				m_ReadCount += m_SerialPort.Read(m_ReceiveBuffer, m_ReadCount, 1024 - m_ReadCount);
 			}
		}
		public bool IsOpen()
		{
			return m_SerialPort.IsOpen;
		}
		public void Open()
		{
			try
			{
				if (IsOpen())
				{
					Close(); // 다른 포트나 속도이면,, 닫아라...
					System.Threading.Thread.Sleep(100);
				}

				m_SerialPort.Encoding = Encoding.Default;
				m_SerialPort.StopBits = StopBits.One;
				m_SerialPort.DataBits = 8;
				m_SerialPort.Parity = Parity.None;
				m_SerialPort.Handshake = Handshake.None;

				m_SerialPort.Open();
				LogTextOut("Open Success!");
			}
			catch (Exception ex)
			{
				LogTextOut(ex.Message);
			}
		}

		public void Close()
		{
			m_SerialPort.Close();
		}
		private int nReadSeqNo = 0;
		private DateTime dtReadTime = DateTime.Now;
		private int nRetryCount = 0;
		public int SeqReadData(byte devID, UInt16 address, UInt16 length, ref UInt16[] readData)
		{
			int nRet = 0;
			int nSeqNo = nReadSeqNo;

			switch (nSeqNo)
			{
				case 0:
					if (SendReadCommand(devID, address, length))
					{
						dtReadTime = DateTime.Now;
						nSeqNo = 100;
					}
					else
					{
						nRetryCount++;
						if (nRetryCount >= RETRY_NO)
						{
							nRet = -1;
							nRetryCount = 0;
						}
						nSeqNo = 0;

						LogTextOut("Send Error = " + nRetryCount);
					}
					break;
				case 100:
					if (IsReceiveCompleted((UInt16)(length * 2 + 5)))
					{
						if (IsValidCRC(m_ReceiveBuffer, (UInt16)(length * 2 + 3)))
						{
							for (int i = 0; i < length; i++)
							{
								readData[i] = (UInt16)((m_ReceiveBuffer[3 + (i * 2)] << 8) + m_ReceiveBuffer[4 + (i * 2)]);
							}
							nRet = 1;
							nSeqNo = 0;
							nRetryCount = 0;
							Flush();

						}
						else
						{
							nRetryCount++;
							if (nRetryCount >= RETRY_NO)
							{
								nRet = -2;
								nRetryCount = 0;
							}
							nSeqNo = 0;
							Flush();
							LogTextOut("EURO, CRC Error = " + nRetryCount);
						}
					}
					else if ((DateTime.Now - dtReadTime).TotalMilliseconds > WAITTIME)
					{
						nRetryCount++;
						if (nRetryCount >= RETRY_NO)
						{
							nRet = -2;
							nRetryCount = 0;
						}
						nSeqNo = 0;
						Flush();
						LogTextOut("Time Out Error = " + nRetryCount);
					}
					break;
				default:
					nRet = -3;
					nSeqNo = 0;
					nRetryCount = 0;
					Flush();
					break;
			}
			nReadSeqNo = nSeqNo;
			return nRet;
		}
		private bool IsReceiveCompleted(UInt16 length)
		{
			if (length == m_ReadCount)
			{
				return true;
			}
			return false;
		}
		public int SeqReadData_ASCII(byte devID, UInt16 address, UInt16 length, ref UInt16[] readData)
		{
			int nRet = 0;
			int nSeqNo = nReadSeqNo;

			switch (nSeqNo)
			{
				case 0:
					if (SendReadCommand_ASCII(devID, address, length))
					{
						dtReadTime = DateTime.Now;
						nSeqNo = 100;
					}
					else
					{
						nRetryCount++;
						if (nRetryCount >= RETRY_NO)
						{
							nRet = -1;
							nRetryCount = 0;
						}
						nSeqNo = 0;

						LogTextOut("Send Error = " + nRetryCount);
					}
					break;
				case 100:
					if (IsReceivedByte((byte)':') //시작문자
						&& IsReceivedByte((byte)ASCII.CR)
						&& IsReceivedByte((byte)ASCII.LF))
					{
						try
						{
							string strRcvedString = Encoding.ASCII.GetString(m_ReceiveBuffer, 0, (int)m_ReadCount);

							int nStartNo = strRcvedString.IndexOf(":", 0);
							int nEndNo = strRcvedString.IndexOf("\r\n", nStartNo);

							//SubString Start+7 : 시작부터 데이터 갯수까지 7바이트
							//SubString End-2 : CR,LR 이전에 LRC 있음..2바이트
							string strProcessData = strRcvedString.Substring(nStartNo + 7, nEndNo - 2 - (nStartNo + 7));

							if (strProcessData.Length == (length * 4))
							{
								for (int i = 0; i < length; i++)
								{
									string strData = strProcessData.Substring(4 * i, 4);
									readData[i] = (ushort)int.Parse(strData, System.Globalization.NumberStyles.HexNumber);
								}
							}
						}
						catch (System.Exception ex)
						{
							LogTextOut("Data Error -> " + ex.Message);
						}

						nRet = 1;
						nSeqNo = 0;
						nRetryCount = 0;
						Flush();
					}
					else if ((DateTime.Now - dtReadTime).TotalMilliseconds > WAITTIME)
					{
						nRetryCount++;
						if (nRetryCount >= RETRY_NO)
						{
							nRet = -2;
							nRetryCount = 0;
						}
						nSeqNo = 0;
						Flush();
						LogTextOut("Time Out Error = " + nRetryCount);
					}
					break;
				default:
					nRet = -3;
					nSeqNo = 0;
					nRetryCount = 0;
					Flush();
					break;
			}
			nReadSeqNo = nSeqNo;
			return nRet;
		}
		public bool IsReceivedByte(byte ch)
		{
			int nNo = Array.IndexOf(m_ReceiveBuffer, ch, 0);
			if (nNo != -1)
			{
				return true;
			}

			return false;
		}

		private void ResetBuff()
		{
			m_ReadCount = 0;
			Array.Clear(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length);
		}
		private void Flush()
		{
			try
			{
				while (m_SerialPort.BytesToRead > 0)
				{
					byte[] buf = new byte[1024];
					m_SerialPort.Read(buf, 0, buf.Length);
				}
				ResetBuff();
			}
			catch
			{

			}
		}

		private bool SendReadCommand(byte devID, UInt16 address, UInt16 length)
		{
			UInt16 CRC;
			byte[] command = new byte[9];
			command[0] = devID;
			command[1] = (byte)COMMAND.READ;

			command[2] = (byte)(address / 256);
			command[3] = (byte)(address % 256);
			command[4] = (byte)(length / 256);
			command[5] = (byte)(length % 256);

			CRC = MakeCRC(command, 6);

			command[6] = (byte)(CRC / 256);
			command[7] = (byte)(CRC % 256);
			command[8] = (byte)ASCII.EOT;
			try
			{
				m_SerialPort.Write(command, 0, command.Length);
			}
			catch
			{
				return false;
			}
			return true;
		}

		private UInt16 MakeCRC(byte[] data, UInt16 length)
		{
			UInt16 CRC = 0xffff;
			UInt16 next, carry, n;

			UInt16 p = 0;
			while (p < length)
			{
				next = (UInt16)data[p++];
				CRC ^= next;
				for (n = 0; n < 8; n++)
				{
					carry = (UInt16)(CRC & 1);
					CRC >>= 1;
					if (carry != 0) CRC ^= 0xA001;
				}
			}
			return CRC;
		}

		private bool SendReadCommand_ASCII(byte devID, UInt16 address, UInt16 length)
		{
			string strCmd = string.Format(":{0:X02}{1:X02}{2:X02}{3:X02}{4:X02}{5:X02}"
									, devID
									, (byte)COMMAND.READ
									, (byte)(address / 256)
									, (byte)(address % 256)
									, (byte)(length / 256)
									, (byte)(length % 256));

			UInt16 LRC;
			LRC = MakeLRC(strCmd);

			strCmd += string.Format("{0:X02}\r\n", (byte)LRC);

			try
			{
				m_SerialPort.Write(strCmd);
			}
			catch
			{
				return false;
			}
			return true;
		}
		
		private UInt16 MakeLRC(byte[] data, UInt16 length)
		{
			int i;
			byte ndat = 0x00, tdat = 0x00;
			for (i = 1; i < length; i = i + 2)
			{
				tdat += data[i];
			}
			ndat = (byte)((0xFF - (tdat & 0xFF)) + 1);
			return ndat;
		}
		private UInt16 MakeLRC(string strData)
		{
			int i;
			char ndat = (char)0, tdat = (char)0;
			for (i = 1; i < strData.Length; i = i + 2)
			{
				tdat += strData[i];
			}
			ndat = (char)((0xFF - (tdat & 0xFF)) + 1);
			return ndat;
		}

		public bool IsValidCRC(byte[] data, UInt16 length)
		{
			UInt16 CRC = MakeCRC(data, length);
			UInt16 rCRC = (UInt16)(data[length + 1] << 8);
			rCRC += data[length];

			if (CRC == rCRC)
			{
				return true;
			}
			return false;
		}
		private enum COMMAND
		{
			READ = 0x03,
			WRITE = 0x06,
			FASTREAD = 0x07,
			LOOP = 0x08
		};

		private enum ASCII : byte
		{
			NULL = 0x00, SOH = 0x01, STX = 0x02, ETX = 0x03, EOT = 0x04, ENQ = 0x05, ACK = 0x06, BELL = 0x07,
			BS = 0x08, HT = 0x09, LF = 0x0A, VT = 0x0B, FF = 0x0C, CR = 0x0D, SO = 0x0E, SI = 0x0F, DC1 = 0x11,
			DC2 = 0x12, DC3 = 0x13, DC4 = 0x14, NAK = 0x15, SYN = 0x16, ETB = 0x17, CAN = 0x18, EM = 0x19,
			SUB = 0x1A, ESC = 0x1B, FS = 0x1C, GS = 0x1D, RS = 0x1E, US = 0x1F, SP = 0x20, DEL = 0x7F
		};
	}
}
