using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.IO.Ports;

namespace CJ_Controls.Communication
{
	public class DL_RS1A_GT2
	{
		public DL_RS1A_GT2()
		{
			m_Thread_Read = new Thread(new ThreadStart(SerialPort_DataReceived));
			m_Thread_Read.IsBackground = true;
            m_Thread_Read.Start();
		}

		Thread m_Thread_Read = null;

		private readonly int READ_DELAY = 2000;
		private System.IO.Ports.SerialPort m_SerialPort = new System.IO.Ports.SerialPort();
		public void Open(string strCom, int nBaudRate)
		{
			try
			{
				if (IsOpen())
				{
					Close(); // 다른 포트나 속도이면,, 닫아라...
					System.Threading.Thread.Sleep(100);
				}

				m_SerialPort.PortName = strCom;
				m_SerialPort.BaudRate = nBaudRate;
				m_SerialPort.Encoding = Encoding.Default;
				m_SerialPort.StopBits = StopBits.One;
				m_SerialPort.DataBits = 8;
				m_SerialPort.Parity = Parity.None;
				m_SerialPort.Handshake = Handshake.None;

				m_SerialPort.Open();
			}
			catch// (Exception ex)
			{

			}
		}

		public bool IsOpen()
		{
			return m_SerialPort.IsOpen;
		}

		public void Close()
		{
            if (IsOpen())
			    m_SerialPort.Close();
		}

		private int m_ReadCount = 0;
		private byte[] m_ReceiveBuffer = new byte[1024];
		private object obj = new object();
		private void SerialPort_DataReceived()
		{
			while (true)
			{
				Thread.Sleep(100);
				if (IsOpen() == false)
					continue;

				try
				{
					SerialPort sp = (SerialPort)m_SerialPort;
					lock (obj)
					{
						while (sp.BytesToRead > 0)
						{
							if ((sp.BytesToRead + m_ReadCount) > 1024)
							{
								Flush();
							}
							else
							{
								m_ReadCount += sp.Read(m_ReceiveBuffer, m_ReadCount, 1024 - m_ReadCount);
							}
						}
					}
				}
				catch
				{

				}
			}
		}

		private void Flush()
		{
			if (IsOpen() == false)
				return;

			try
			{
				lock (obj)
				{
					while (m_SerialPort.BytesToRead > 0)
					{
						byte[] buf = new byte[1024];
						m_SerialPort.Read(buf, 0, buf.Length);
					}
					m_ReadCount = 0;
					Array.Clear(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length);
				}
			}
			catch
			{
			}
		}
		private DateTime m_TimeOut = DateTime.MaxValue;
		public int SubSeq_ReadGt2(ref int nSubSeqNo, ref double _ResValue)
		{
			int nRet = 0;
			switch (nSubSeqNo)
			{
				case 0:
					if (SendData("M0\r\n") == true)
					{
						nSubSeqNo = 100;
						m_TimeOut = DateTime.Now;
					}
					else
					{
						//알람
						Flush();
						nSubSeqNo = 0;
						nRet = -1;
					}
					break;
				case 100:
					if (IsReceived((char)0x0D) != -1 && IsReceived((char)0x0A) != -1)
					{
						string strBuf = GetReceiveData();
						try
						{
							List<string> result = new List<string>();
							result.AddRange(strBuf.Split(','));
                            if (result.Count >= 2)
                            {
                                _ResValue = 0;
                                double.TryParse(result[1], out _ResValue);
                            }
							nRet = 100;
							nSubSeqNo = 0;
							Flush();
						}
						catch
						{
							nSubSeqNo = 0;
							nRet = -100;
							Flush();
						}
					}
					else
					{
						if (GetElapseTime(m_TimeOut) > READ_DELAY)
						{
							nSubSeqNo = 0;
							nRet = -100;
							Flush();
						}
					}
					break;
				default:
					{
						nSubSeqNo = 0;
						nRet = -100;
						Flush();
					} break;
			}
			return nRet;
		}
		private bool SendData(string str)
		{
			try
			{
				if (m_SerialPort.IsOpen.Equals(true))
					m_SerialPort.Write(str);
				else
					return false;
			}
			catch
			{
				return false;
			}

			return true;
		}
		private long GetElapseTime(DateTime dateTime)
		{
			return ((DateTime.Now.Ticks - dateTime.Ticks) / 10000);
		}

		private bool IsReceived(string strEnd)
		{
			return GetReceiveData().Contains(strEnd);
		}
		private int IsReceived(char strCh)
		{
			int nRtn = -1;
			for (int i = 0; i < m_ReadCount; i++)
			{
				if (m_ReceiveBuffer[i] == strCh)
				{
					nRtn = i;
					break;
				}
			}
			return nRtn;
		}
		private string GetReceiveData()
		{
			return Encoding.ASCII.GetString(m_ReceiveBuffer, 0, m_ReadCount);
		}
		private byte[] GetReceiveDataByte()
		{
			return m_ReceiveBuffer;
		}
	}
}
