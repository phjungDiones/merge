using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;

namespace CJ_Controls.Communication.Accura2300
{
	public class VoltageModule
	{
		#region 싱글톤 멤버

		private static VoltageModule _Instance = null;
		private static object lockObj = new object();

		public static VoltageModule Instance
		{
			get
			{
				if (_Instance == null)
				{
					lock (lockObj)
					{
						if (_Instance == null)
						{
							_Instance = new VoltageModule();
						}
					}
				}
				return _Instance;
			}
		}

		private VoltageModule()
		{
			//_Accura2300Item.Add(new VoltageItemInfo("10.10.10.100", 502));
			//foreach (VoltageItemInfo item in _Accura2300Item)
			//{
			//    item.Start();
			//}
		}
		#endregion

		public VoltageItemInfo AddVoltageItemInfo(string ip, int port)
		{
			VoltageItemInfo voltageItemInfo = new VoltageItemInfo(ip, port);
			_Accura2300Item.Add(voltageItemInfo);
			return voltageItemInfo;
		}
		public void Start()
		{
			foreach (VoltageItemInfo item in _Accura2300Item)
			{
				item.Start();
			}
		}

		private List<VoltageItemInfo> _Accura2300Item = new List<VoltageItemInfo>();

		public List<VoltageItemInfo> Accura2300Item
		{
			get { return _Accura2300Item; }
			private set { _Accura2300Item = value; }
		}
	}
}
