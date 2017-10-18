using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CJ_Controls.Communication.Yamatake_SDC15
{
	public class Yamatake_SDC15
	{
		#region Log 보내기
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public event MessageEventHandler MessageEvent;
		private void LogTextOut(string message)
		{
			if (MessageEvent != null)
			{
				string strInfo = string.Format("Yamatake_SDC15({0})=>", m_SerialPort.PortName);
				MessageEvent(this, new MessageEventArgs(strInfo+message));
			}
		}
		#endregion

		Thread m_Thread_Read = null;
		public Yamatake_SDC15()
		{
			m_Thread_Read = new Thread(new ThreadStart(SerialPort_DataReceived));
			m_Thread_Read.IsBackground = true;
            m_Thread_Read.Start();
		}

		private SerialPort m_SerialPort = new SerialPort();
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
				LogTextOut(string.Format("Open Success. ({0}/{1})", strCom, nBaudRate));
			}
			catch (Exception ex)
			{
				LogTextOut(string.Format("Open Fail. ({0}/{1}) : {2}", strCom, nBaudRate, ex.Message));
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

		private const int MAX_BUFFER = 256;
		private int m_ReadCount = 0;
		private byte[] m_ReceiveBuffer = new byte[MAX_BUFFER];
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
							if ((sp.BytesToRead + m_ReadCount) > MAX_BUFFER)
							{
								Flush();
							}
							else
							{
								m_ReadCount += sp.Read(m_ReceiveBuffer, m_ReadCount, MAX_BUFFER - m_ReadCount);
							}
						}
					}
				}
				catch (Exception ex)
				{
					LogTextOut(string.Format("SerialPort_DataReceived/{0}", ex.Message));
				}
			}
		}
		private void GetRcvData(ref string sRcvData)
		{
			sRcvData = GetReceiveData();
			LogTextOut(string.Format("RecvData:{0}", sRcvData));

			Flush();
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
						byte[] buf = new byte[MAX_BUFFER];
						m_SerialPort.Read(buf, 0, buf.Length);
					}
					m_ReadCount = 0;
					Array.Clear(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length);
				}
			}
			catch (Exception ex)
			{
				LogTextOut(string.Format("Flush/{0}", ex.Message));
			}
		}

		private bool SendData(string str)
		{
			try
			{
				if (m_SerialPort.IsOpen.Equals(true))
				{
					m_SerialPort.Write(str);
					LogTextOut(string.Format("SendData:{0}", str));
				}
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
		private long GetElapseTime(DateTime dateTime)
		{
			return ((DateTime.Now.Ticks - dateTime.Ticks) / 10000);
		}
		private bool IsReceived(string strEnd)
		{
			return GetReceiveData().Contains(strEnd);
		}
		private int IsReceived(byte strCh)
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
		private int IsReceived_Ex(byte strCh1, byte strCh2)
		{
			int nRtn = -1;
			for (int i = 0; i < m_ReadCount-1; i++)
			{
				if (m_ReceiveBuffer[i] == strCh1
					&& m_ReceiveBuffer[i+1] == strCh2)
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

		private const int READ_ADDR = 9101;		//PV
		private const int WRITE_ADDR = 9102;	//SP
		private byte[] MakeCmdHeader(byte _DeviceID)
		{
			byte[] strHeader = new byte[6];
			int nIndex = 0;
			strHeader[nIndex] = 0x02;						nIndex++; //STX
			strHeader[nIndex] = 0x30;						nIndex++; //기기어드레스 첫번째자리
			strHeader[nIndex] = (byte)(_DeviceID | 0x30);	nIndex++; //기기어드레스 두번째자리
			strHeader[nIndex] = 0x30;						nIndex++; //서브어드레스 첫번째자리
			strHeader[nIndex] = 0x30;						nIndex++; //서브어드레스 두번째자리
			strHeader[nIndex] = 0x58;						nIndex++; //디바이드 구별 코드
			return strHeader;
		}

		private async void RcvCheckDelay(int _Delay_ms)
		{
			DateTime dt = DateTime.Now;
			bool bLoop = true;
			int _Before_Data_Count = -1;
			do
			{
				await Task.Delay(100);

				if (_Before_Data_Count == m_ReadCount && m_ReadCount > 0)
					bLoop = false;
				else
					_Before_Data_Count = m_ReadCount;

				long nElapsed = ((DateTime.Now.Ticks - dt.Ticks) / 10000);
				if (nElapsed >= _Delay_ms) //타임아웃..
				{
					bLoop = false;
					LogTextOut("Receive Check Time Out...Time(ms):" + nElapsed.ToString());
				}

			} while (bLoop);
		}
		
		//읽기
		private float Read_Memory_PV(byte nDeviceID)
		{
			int nReadSize = 1;
			string strCmd = string.Format("{0}RS,{1:0000}W,{2}{3}", MakeCmdHeader(nDeviceID), READ_ADDR, nReadSize, 0x03);
			SendData(strCmd);

			RcvCheckDelay(2000); //2초안에 데이터가 오면..

			string strRcv = "";
			GetRcvData(ref strRcv);
			
			List<string> result = new List<string>();
			result.AddRange(strRcv.Split(','));

			float fRtn = 0;
			if(result.Count >= 2)
			{
				float fGet = 0;
				float.TryParse(result[1], out fGet);
				fRtn = fGet / 10;
			}

			return fRtn;
		}

		//쓰기
		private bool Write_Memory_SP(byte nDeviceID, float fTempVal)
		{
			string strCmd = string.Format("{0}WS,{1:0000}W,{2}{3}", MakeCmdHeader(nDeviceID), WRITE_ADDR, fTempVal, 0x03);
			SendData(strCmd);

			RcvCheckDelay(2000); //2초안에 데이터가 오면..

			string strRcv = "";
			GetRcvData(ref strRcv);

			bool bRtn = false;
			if (strRcv.Length > 0)
				bRtn = true;
			return bRtn;
		}
	}
}
