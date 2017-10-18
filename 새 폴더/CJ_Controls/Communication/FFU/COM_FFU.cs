using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.ComponentModel;
using CJ_Controls;

namespace CJ_Controls.Communication
{
	public class COM_FFU : Component
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

		private readonly long TIME_OUT = 3000;
		private readonly int RETRY_COUNT = 3;
		private int m_SeqNum = 0;
		private int m_CmdSeqNum = 0;
		private int m_ReceiveDataCount = 0;
		private byte[] m_GetDatas = new byte[1024];
		private DateTime m_TimeOut = DateTime.MaxValue;
		private int m_RetryCount = 0;

		private Thread m_Thread;
		private SerialPort m_SerialPort = new SerialPort();
		string m_strETX = string.Format("{0}", 0x03);
		string m_strEndChar = string.Format("{0}", 0x3f);

		byte m_nCurSet = 0;
		byte m_nSetVal = 0;

		public string PortName
		{
			get { return m_SerialPort.PortName; }
			set { m_SerialPort.PortName = value; }
		}
		public int BaudRate
		{
			get { return m_SerialPort.BaudRate; }
			set { m_SerialPort.BaudRate = value; }
		}

		public List<FFU_Value> m_FFU_Data;
		public COM_FFU()
		{
			m_FFU_Data = new List<FFU_Value>();
			for (int i = 0; i < 20; i++)
				m_FFU_Data.Add(new FFU_Value());

			m_Thread = new Thread(new ThreadStart(Run));
			m_Thread.IsBackground = true;
			m_SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
		}
		public void SetSpeed(byte nVal)
		{
			m_nSetVal = nVal;
		}
		public void SetSpeed(Decimal nVal)
		{
			try
			{
				if (nVal > 150)
					m_nSetVal = 150;
				else
					m_nSetVal = (byte)Convert.ToInt32(nVal);
			}
			catch (Exception ex)
			{
				LogTextOut(ex.Message);
			}
			
		}
		public bool IsOpen()
		{
			return m_SerialPort.IsOpen;
		}
		public void Close()
		{
			if (IsOpen())
			{
				m_SerialPort.Close();
			}
		}
		public bool Open()
		{
			try
			{
				if (IsOpen())
				{
					Close();
					Thread.Sleep(100);
				}

				m_SerialPort.Encoding = Encoding.Default;
				m_SerialPort.Parity = Parity.None;
				m_SerialPort.DataBits = 8;
				m_SerialPort.StopBits = StopBits.One;
				m_SerialPort.Handshake = Handshake.None;

				Reset_FFU_Data();
				m_SerialPort.Open();
				LogTextOut("Open Success!");
			}
			catch (Exception ex)
			{
				LogTextOut(ex.Message);
				return false;
			}
			return true;
		}

		void Reset_FFU_Data()
		{
			ResetBuffer();
			m_ReceiveDataCount = 0;
			m_CmdSeqNum = 0;
			m_SeqNum = 0;
			m_RetryCount = 0;
		}

		public bool Start()
		{
			bool bRtn = Open();
			m_Thread.Start();
			return bRtn;
		}

		private void Run()
		{
			while (true)
			{
				Thread.Sleep(100);
				Sequence_FFU();
			}
		}

		private void Sequence_FFU()
		{
			int seqNum = m_SeqNum;
			int nRet = 0;
			switch (seqNum)
			{
				case 0: //Start
					{
						if (IsOpen())
						{
							if (m_nCurSet != m_nSetVal)
							{
								seqNum = 200;
							}
							else
							{
								seqNum = 100;
							}
						}
					} break;
				case 100:
					{
						// 읽어오기..
						nRet = ReadDataSeq();
						if (nRet > 0)
						{
							seqNum = 0;
						}
						else if (nRet < 0)
						{
							seqNum = 0;
							Flush();
							LogTextOut("FFU Unit Communication Error! (Read)");
						}
					}break;
				case 200:
					{ // 속도 설정.
						nRet = SetDataSeq(m_nSetVal);
						if (nRet > 0)
						{
							seqNum = 0;
						}
						else if (nRet < 0)
						{
							seqNum = 0;
							Flush();
							LogTextOut("FFU Unit Communication Error! (Write)");
						}
					} break;
				default:
					break;
			}

			m_SeqNum = seqNum;
		}

		private int ReadDataSeq()
		{// 현재구동속도, 알람, 지정속도를 읽어온다.
			int nRet = 0;
			byte[] strBuf;
			int seqNum = m_CmdSeqNum;
			switch (seqNum)
			{
				case 0: //농도와 온도를 읽어온다.
					strBuf = MakeReadCmd();
					if (SendData(strBuf,0,9))
					{
						seqNum = 100;
						m_TimeOut = DateTime.Now;
					}
					else if (GetElapseTime(m_TimeOut) > TIME_OUT)
					{
						nRet = Retry(ref seqNum);
					}
					break;
				case 100:
					if(IsReceived((char)0x02) != -1 && IsReceived((char)0x03) != -1)
					{
						strBuf = GetReceiveDataByte();
						try
						{
							int nStxIndex = 0;
							for (int i = 0; i < 1024; i++)
							{
								nStxIndex = i;
								if (strBuf[i] == 0x02)
								{
									break;
								}
							}
							nStxIndex += 5; // 데이터 까지 쩜프.

							for (int i = 0; i < 20; i++)
							{
								if (strBuf[nStxIndex] == 0x02)
									break;
								else
								{
									if (strBuf[nStxIndex] == 0x81 + i)
									{
										nStxIndex += 1;
										m_FFU_Data[i].nPV = strBuf[nStxIndex]; nStxIndex += 1;
										m_FFU_Data[i].nAlarm = strBuf[nStxIndex]; nStxIndex += 1;
										m_FFU_Data[i].nSetVal = strBuf[nStxIndex]; nStxIndex += 1;
									}
									else
									{
										nStxIndex += 4;
									}
								}
							}

							seqNum = 0;
							nRet = 1;
							m_RetryCount = 0;
							Flush();
						}
						catch (Exception ex)
						{
							seqNum = 0;
							nRet = -1;
							m_RetryCount = 0;

							LogTextOut(string.Format("FFU Unit ({0}) (Read Data Parsing Error : (DATA:{1})): {2}", m_SerialPort.PortName, strBuf, ex.Message));
						}
					}
					else if (GetElapseTime(m_TimeOut) > TIME_OUT)
					{
						nRet = Retry(ref seqNum);
					}
					break;
				default:
					break;
			}
			m_CmdSeqNum = seqNum;
			return nRet;
		}

		private int SetDataSeq(byte nSetVal)
		{// 현재구동속도, 알람, 지정속도를 읽어온다.
			int nRet = 0;
			byte[] strBuf;
			int seqNum = m_CmdSeqNum;
			switch (seqNum)
			{
				case 0:
					strBuf = MakeSetCmd(nSetVal);
					if (SendData(strBuf, 0, 10))
					{
						seqNum = 100;
						m_TimeOut = DateTime.Now;
					}
					else if (GetElapseTime(m_TimeOut) > TIME_OUT)
					{
						nRet = Retry(ref seqNum);
					}
					break;
				case 100:
					if (IsReceived((char)0x02) != -1 && IsReceived((char)0x03) != -1)
					{
						strBuf = GetReceiveDataByte();
						try
						{
							int nStxIndex = 0;
							for (int i = 0; i < 1024; i++)
							{
								nStxIndex = i;
								if (strBuf[i] == 0x02)
								{
									break;
								}
							}
							nStxIndex += 7; // OK Flag 까지 쩜프.

							if (strBuf[nStxIndex] == 0xB9)
							{ //정상적으로 세팅.
								m_nCurSet = m_nSetVal;
								seqNum = 0;
								nRet = 1;
								m_RetryCount = 0;
								Flush();
							}
							else
							{ //비정상이면 다시 세팅.
								seqNum = 0;
								nRet = 0;
								Flush();
							}
						}
						catch (Exception ex)
						{
							seqNum = 0;
							nRet = -1;
							m_RetryCount = 0;
							LogTextOut(string.Format("FFU Unit ({0}) (Set Data Error : (DATA:{1})): {2}", m_SerialPort.PortName, strBuf, ex.Message));
						}
					}
					else if (GetElapseTime(m_TimeOut) > TIME_OUT)
					{
						nRet = Retry(ref seqNum);
					}
					break;
				default:
					break;
			}
			m_CmdSeqNum = seqNum;
			return nRet;
		}
		private byte[] MakeReadCmd()
		{
			byte[] strCmd = new byte[1024];
			byte CheckSum = 0;
			int nIndex = 0;
			strCmd[nIndex] = 0x02;					nIndex++; //STX
			CheckSum += strCmd[nIndex] = 0x8A;		nIndex++; //MODE1
			CheckSum += strCmd[nIndex] = 0x87;		nIndex++; //MODE2
			CheckSum += strCmd[nIndex] = 0x81;		nIndex++; //LV32_ID
			CheckSum += strCmd[nIndex] = 0x9F;		nIndex++; //DPU ID
			CheckSum += strCmd[nIndex] = 1 | 0x80;	nIndex++; //START BL500_ID
			CheckSum += strCmd[nIndex] = 20 | 0x80;	nIndex++; //END BL500_ID
			strCmd[nIndex] = CheckSum;				nIndex++; //Check Sum
			strCmd[nIndex] = 0x03;					nIndex++; //ETX
			return strCmd;
		}
		private byte[] MakeSetCmd(byte nSV /*SV: 0-140*/)
		{
			byte[] strCmd = new byte[1024];
			byte CheckSum = 0;
			int nIndex = 0;
			strCmd[nIndex] = 0x02;					nIndex++; //STX
			CheckSum += strCmd[nIndex] = 0x89;		nIndex++; //MODE1
			CheckSum += strCmd[nIndex] = 0x84;		nIndex++; //MODE2
			CheckSum += strCmd[nIndex] = 0x81;		nIndex++; //LV32_ID
			CheckSum += strCmd[nIndex] = 0x9F;		nIndex++; //DPU ID
			CheckSum += strCmd[nIndex] = 1 | 0x80;	nIndex++; //START BL500_ID
			CheckSum += strCmd[nIndex] = 20 | 0x80;	nIndex++; //END BL500_ID
			CheckSum += strCmd[nIndex] = nSV;		nIndex++; //SV
			strCmd[nIndex] = CheckSum;				nIndex++; //Check Sum
			strCmd[nIndex] = 0x03;					nIndex++; //ETX
			return strCmd;
		}

		private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				SerialPort sp = (SerialPort)sender;
				while (sp.BytesToRead > 0)
				{
					if ((sp.BytesToRead + m_ReceiveDataCount) > 1024)
					{
						Flush();
					}
					else
					{
						m_ReceiveDataCount += sp.Read(m_GetDatas, m_ReceiveDataCount, 1024 - m_ReceiveDataCount);
					}
				}
			}
			catch (Exception ex)
			{
				LogTextOut(ex.Message);
			}
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
				ResetBuffer();
			}
			catch (Exception ex)
			{
				LogTextOut(ex.Message);
			}
		}
		private bool SendData(string data)
		{
			try
			{
				if (m_SerialPort.IsOpen.Equals(true))
					m_SerialPort.Write(data);
				else
					return false;
			}
			catch (Exception ex)
			{
				LogTextOut(ex.Message);
				return false;
			}

			return true;
		}
		public bool SendData(byte[] buffer, int offset, int count)
		{
			try
			{
				if (m_SerialPort.IsOpen.Equals(true))
					m_SerialPort.Write(buffer, offset, count);
				else
					return false;
			}
			catch (Exception ex)
			{
				LogTextOut(ex.Message);
				return false;
			}
			return true;
		}
		private bool IsReceived(string strEnd)
		{
			return GetReceiveData().Contains(strEnd);
		}
		private int IsReceived(char strCh)
		{
			int nRtn = -1;
			for (int i = 0; i < m_ReceiveDataCount; i++)
			{
				if (m_GetDatas[i] == strCh)
				{
					nRtn = i;
					break;
				}
			}
			return nRtn;
		}
		private string GetReceiveData()
		{
			return Encoding.ASCII.GetString(m_GetDatas, 0, m_ReceiveDataCount);
		}
		private byte[] GetReceiveDataByte()
		{
			return m_GetDatas;
		}
		private long GetElapseTime(DateTime dateTime)
		{
			return ((DateTime.Now.Ticks - dateTime.Ticks) / 10000);
		}

		private int Retry(ref int seqNum)
		{
			Flush();
			seqNum = 0;
			int nRet = 0;
			if (m_RetryCount < RETRY_COUNT)
			{
				m_RetryCount++;
			}
			else
			{
				m_RetryCount = 0;
				nRet = -1;
			}
			return nRet;
		}
		private void ResetBuffer()
		{
			m_ReceiveDataCount = 0;
			Array.Clear(m_GetDatas, 0, m_GetDatas.Length);
		}

		public class FFU_Value
		{
			public int nPV; // Process Value
			public int nAlarm; //Alarm
			public int nSetVal; //Setting Value

			public FFU_Value()
			{
				nPV = 0;
				nAlarm = 0;
				nSetVal = 0;
			}
		}
	}
}
