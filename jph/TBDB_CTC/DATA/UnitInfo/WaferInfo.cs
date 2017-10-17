using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBDB_CTC.Data.UnitInfo
{
	[Serializable]
	public class WaferInfo
	{
		public WaferInfo()
		{
		}
		public WaferInfo(int nPort_1base, int nSlot_1Base)
		{
			PortNum_1Base = nPort_1base;
			PortSlotNum_1Base = nSlot_1Base;
		}

		public void ClearInfo()
		{
			PortNum_1Base = 0;
			PortSlotNum_1Base = 0;
			WaferStatus = WAFER_STATUS.IDLE;
			WaferMode = WAFER_MODE.DEVICE;
			WaferCurrentPos = 0;
			SelectSlot = false;
			WaferUse = false;
			WaferCross = false;
			WaferDouble = false;
			WaferID = "";
			PPID = "";
			LOT_ID = "";
			PROCESS_FLAG = "";
			JUDGEMENT = "";
			WaferAPD.Clear();
			
		}
		public void SetNextPath()
		{
			if (OnlyLami == true
				&& WaferMode == WAFER_MODE.CARRIER)
			{
				if (WaferCurrentPos == (int)WAFER_PATH_CARRIER.ATM_TM_2)
				{//바로 쿨링 플레이트로 건너 뛸 것...
					WaferCurrentPos = (int)WAFER_PATH_CARRIER.COOLING_PLATE;
				}
				else
				{
					WaferCurrentPos += 1;
				}
			}
			else
			{
				WaferCurrentPos += 1;
			}
		}
		public string GetWaferPos_Text()
		{
			string strWaferPos = "";
			switch(WaferMode)
			{
				case WAFER_MODE.DEVICE:
					{
						strWaferPos = ((WAFER_PATH_DEVICE)WaferCurrentPos).ToString();
					}break;
				case WAFER_MODE.CARRIER:
					{
						strWaferPos = ((WAFER_PATH_CARRIER)WaferCurrentPos).ToString();
					} break;
				default:
					break;
			}
			return strWaferPos;
		}

		public string GetWaferName()
		{
			string strWaferName = "";
			switch(PortNum_1Base)
			{
				case 1: strWaferName = "A"; break;
				case 2: strWaferName = "B"; break;
				case 3: strWaferName = "C"; break;
				case 4: strWaferName = "D"; break;
			}

			strWaferName += PortSlotNum_1Base.ToString("00");
			return strWaferName;
		}

		//Public
		public bool OnlyLami = false;
		public int PortNum_1Base = 0;
		public int PortSlotNum_1Base = 0;
		public WAFER_STATUS WaferStatus = WAFER_STATUS.IDLE;
		public WAFER_MODE WaferMode = WAFER_MODE.DEVICE;
		public int WaferCurrentPos = 0;
		public bool Lamination_Complete = false;
		public bool SelectSlot = false;
		public bool WaferUse = false;
		private bool _WaferCross = false;
		public bool WaferCross
		{
			get { return _WaferCross; }
			set
			{
				if (_WaferCross != value)
				{
					_WaferCross = value;
					if (_WaferCross == true)
					{
						WaferStatus = WAFER_STATUS.CROSS;
					}
				}
			}
		}
		private bool _WaferDouble = false;
		public bool WaferDouble
		{
			get { return _WaferDouble; }
			set
			{
				if (_WaferDouble != value)
				{
					_WaferDouble = value;
					if (_WaferDouble == true)
					{
						WaferStatus = WAFER_STATUS.DOUBLE;
					}
				}
			}
		}
		private string _WaferID = "";
		public string WaferID
		{
			get
			{
				if (_WaferID == "")
				{
					_WaferID = GetWaferName();
				}
				return _WaferID;
			}
			set { _WaferID = value; }
		}
		public string PPID = "";
		public string LOT_ID = "";
		public string PROCESS_FLAG = "";
		public string JUDGEMENT = "";

		private WaferAPDInfo _WaferAPDInfo = new WaferAPDInfo();
		public WaferAPDInfo WaferAPD
		{
			get { return _WaferAPDInfo; }
			set { _WaferAPDInfo = value; }
		}

		public short WaferCode
		{
			get
			{
				int nPort = PortNum_1Base;
				int nSlot = PortSlotNum_1Base;
				short nGlassCode = (short)((nPort << 8) | nSlot);
				return nGlassCode;
			}
		}

		private string _CassetteID = "";
		public string CassetteID
		{
			get { return _CassetteID; }
			set { _CassetteID = value; }
		}
	}
}
