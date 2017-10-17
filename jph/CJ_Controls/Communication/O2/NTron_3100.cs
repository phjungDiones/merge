using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.IO.Ports;

namespace CJ_Controls.Communication
{
	class NTron_3100 : Component
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

		public NTron_3100()
		{
			m_SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
			m_Thread = new Thread(new ThreadStart(Run));
			m_Thread.IsBackground = true;
		}

		private readonly int RETRY_NO = 5;
		private readonly int RETRY_DELAY = 2000;
		private System.IO.Ports.SerialPort m_SerialPort = new System.IO.Ports.SerialPort();

		Thread m_Thread = null;
		[Category("O2 Port Setting"), Description("Port Name"), DefaultValue(false)]
		public string PortName
		{
			get { return m_SerialPort.PortName; }
			set { m_SerialPort.PortName = value; }
		}
		[Category("O2 Port Setting"), Description("Baudrate"), DefaultValue(false)]
		public int BaudRate
		{
			get { return m_SerialPort.BaudRate; }
			set { m_SerialPort.BaudRate = value; }
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
		public void Start()
		{
			m_Thread.Start();
		}

		private int m_ReadCount = 0;
		private byte[] m_ReceiveBuffer = new byte[1024];
		private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				SerialPort sp = (SerialPort)sender;
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
		private void ResetBuffer()
		{
			m_ReadCount = 0;
			Array.Clear(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length);
		}
		private int m_nSeqNum = 0;
		private DateTime m_TimeOut = DateTime.MaxValue;
		private int m_RetryCount = 0;
		private void SeqReset()
		{
			Flush();
			m_nSeqNum = 0;
			m_TimeOut = DateTime.MaxValue;
			m_RetryCount = 0;
		}
		private void Run()
		{
			while (true)
			{
				Thread.Sleep(100);
				Sequence();
			}
		}
		
		private void Sequence()
		{
			switch (m_nSeqNum)
			{
				case 0:
					if (SendData("d\r") == true)
					{
						m_nSeqNum = 100;
					}
					else
					{
						//알람
						SeqReset();
					}
					break;
				case 100:
					if (GetElapseTime(m_TimeOut) > RETRY_DELAY)
					{
						int nRet = RetryCount();
						if (nRet >= 0)
						{
							m_nSeqNum = 0;
							Flush();
						}
						else
						{
							//알람
							SeqReset();
						}
					}
					else
					{
						//응답처리

						//정상이면 200번으로
						Flush();
						m_nSeqNum = 200;
					}
					break;
				case 200:
					if (SendData("d\r") == true)
					{
						m_nSeqNum = 300;
					}
					else
					{
						//알람.
						SeqReset();
					}
					break;
				case 300:
					if (GetElapseTime(m_TimeOut) > RETRY_DELAY)
					{
						int nRet = RetryCount();
						if (nRet > 0)
						{
							m_nSeqNum = 200;
							Flush();
						}
						else
						{
							//알람
							SeqReset();
						}
					}
					else
					{
						//응답처리

						//정상이면 0번으로
						Flush();
						m_nSeqNum = 0;
					}
					break;
				default:
					break;
			}
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
		private int RetryCount()
		{
			Flush();
			int nRet = 0;
			if (m_RetryCount <= RETRY_NO)
			{
				nRet = m_RetryCount++;
			}
			else
			{
				m_RetryCount = 0;
				nRet = -1;
			}
			return nRet;
		}
	}
}
