using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CJ_Controls.Communication.Accura2300
{
	public class VoltageItemInfo
	{
		private Accura2300 _Accura2300Info = new Accura2300();
		private Thread _Thread;
		public VoltageValue GetVoltageValue()
		{
			return _Accura2300Info.VoltageValueStruct;
		}

		public int GetAllVoltageValue()
		{
			return _Accura2300Info.AllVoltageValueStruct.AllVoltageValueData;
		}

		public VoltageItemInfo(string ip, int port)
		{
			m_strIP = ip;
			m_nPort = port;
			_Thread = new Thread(new ThreadStart(Run));
			_Thread.IsBackground = true;
		}
		private string m_strIP = "10.10.10.100";
		private int m_nPort = 502;

		public void Start()
		{
			if (_Thread.IsAlive == true)
			{
				return;
			}
			_Thread.Start();
		}

		private DateTime dtConnectTime = DateTime.MaxValue;
		private void Run()
		{
			while (true)
			{
				Thread.Sleep(500);
				if (_Accura2300Info.IsConnect)
				{
					_Accura2300Info.ReadVoltageValue();
				}
				else
				{
					if (dtConnectTime == DateTime.MaxValue)
					{
						dtConnectTime = DateTime.Now;
						_Accura2300Info.ConnectSocket(m_strIP, m_nPort);
					}
					else
					{
						long nElapsed = ((DateTime.Now.Ticks - dtConnectTime.Ticks) / 10000); //mSec
						if (nElapsed > (10 * 1000))
						{
							dtConnectTime = DateTime.MaxValue;
						}
					}
				}
			}
		}
	}
}
