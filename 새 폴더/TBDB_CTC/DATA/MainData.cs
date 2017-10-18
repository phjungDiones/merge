using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TBDB_CTC.Data.UnitInfo;
using TBDB_Handler;
using TBDB_Handler.DATA.UnitInfo;

namespace TBDB_CTC.Data
{
	public class MainData
	{
		#region 싱글톤 멤버
		private static MainData _Instance = null;
		private static object _LockObj = new object();

		public static MainData Instance
		{
			get
			{
				if (_Instance == null)
				{
					lock (_LockObj)
					{
						if (_Instance == null)
						{
							_Instance = new MainData();
						}
					}
				}
				return _Instance;
			}
		}
		#endregion
		public MainData()
		{

		}

		public void Init()
		{
			LoadConfig();
			EQP_All_Unitinfo = new EquipmentInfo();

            ProcessMgr.Inst.Load();
            RecipeMgr.Inst.Load();
		}

		private EquipmentInfo EQP_All_Unitinfo = null;
		public EquipmentInfo GetEqpUnitInfo()
		{
			return EQP_All_Unitinfo;
		}
		public LoaderInfo GetLoaderData()
		{
			return GetEqpUnitInfo().GetLoaderData();
		}

		public AtmTmInfo GetAtmTmData()
		{
			return GetEqpUnitInfo().GetAtmTmData();
		}

		public VaccumTmInfo GetVaccumTmData()
		{
			return GetEqpUnitInfo().GetVaccumTmData();
		}

        public LoadlockInfo GetLoadlockData()
        {
            return GetEqpUnitInfo().GetLoadlockData();
        
        }
        public LaminatorInfo GetLaminatorData()
        {
            return GetEqpUnitInfo().GetLamiDaata();
        }

        public PmcInfo GetPmcData()
        {
            return GetEqpUnitInfo().GetPmcData();
        }


		#region Config
		private ConfigData _ConfigMgr = new ConfigData();
		public ConfigData ConfigMgr
		{
			get { return _ConfigMgr; }
			set { _ConfigMgr = value; }
		}
		public bool LoadConfig()
		{
			bool bRtn = false;
			string strDir = "Set";
			string strFilePath = strDir + "\\Config.xml";
			try
			{
				if (!Directory.Exists(strDir) && strDir != string.Empty)
				{
					Directory.CreateDirectory(strDir);
				}

				using (FileStream fs = new FileStream(strFilePath, FileMode.Open))
				{
					XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
					_ConfigMgr = xs.Deserialize(fs) as ConfigData;
					bRtn = true;
				}
			}
			catch
			{
				bRtn = false;
			}
			return bRtn;
		}
		public bool SaveConfig()
		{
			bool bRtn = false;
			string strDir = "Set";
			string strFilePath = strDir + "\\Config.xml";
			try
			{
				if (!Directory.Exists(strDir) && strDir != string.Empty)
				{
					Directory.CreateDirectory(strDir);
				}

				using (FileStream fs = new FileStream(strFilePath, FileMode.Create))
				{
					XmlSerializer xs = new XmlSerializer(typeof(ConfigData));
					xs.Serialize(fs, _ConfigMgr);
					bRtn = true;
				}
			}
			catch
			{
				bRtn = false;
			}
			return bRtn;
		}
		#endregion

		private UNIT_STATUS _EqpStatus = UNIT_STATUS.IDLE;
		public UNIT_STATUS EqpStatus
		{
			get { return _EqpStatus; }
			set { _EqpStatus = value; }
		}

		public void Delay(int nMillisecond)
		{
			DateTime ThisMoment = DateTime.Now;
			TimeSpan duration = new TimeSpan(0, 0, 0, 0, nMillisecond);
			DateTime AfterWards = ThisMoment.Add(duration);
			while (AfterWards >= ThisMoment)
			{
				System.Windows.Forms.Application.DoEvents();
				ThisMoment = DateTime.Now;
			}
		}
	}
}
