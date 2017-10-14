using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBDB_CTC.Data.UnitInfo
{
	[Serializable]
	public class WaferAPDInfo
	{
		public WaferAPDInfo()
		{

		}

		private string _ProcessStartTime = "";
		private string _ProcessEndTime = "";

		private List<MovingTime> _UnitProcessTimeList = new List<MovingTime>();

		private List<APD_Info> _ApdInfoList = new List<APD_Info>();
		public List<APD_Info> APDInfoList
		{
			get { return _ApdInfoList; }
			set { _ApdInfoList = value; }
		}

		public string ProcessStartTime
		{
			get { return _ProcessStartTime; }
			set { _ProcessStartTime = value; }
		}
		public string ProcessEndTime
		{
			get { return _ProcessEndTime; }
			set { _ProcessEndTime = value; }
		}
		public List<MovingTime> UnitProcessTimeList
		{
			get { return _UnitProcessTimeList; }
			set { _UnitProcessTimeList = value; }
		}

		public void SetUnitStartTime(UNIT_NO _UnitNo, DateTime _Time)
		{
			MovingTime _Moving = new MovingTime();
			_Moving.UnitNo = _UnitNo;
			_Moving.StartTime = _Time;
			UnitProcessTimeList.Add(_Moving);
		}

		public void SetUnitEndTime(UNIT_NO _UnitNo, DateTime _Time)
		{
			for (int i = 0; i < UnitProcessTimeList.Count; i++)
			{
				if (UnitProcessTimeList[i].UnitNo == _UnitNo)
				{
					if (UnitProcessTimeList[i].EndTime == DateTime.MaxValue)
					{
						UnitProcessTimeList[i].EndTime = _Time;
						break;
					}
				}
			}
		}

		public void Clear()
		{
			_UnitProcessTimeList.Clear();
			_ApdInfoList.Clear();
		}
	}

	[Serializable]
	public class MovingTime
	{
		private UNIT_NO m_UnitNo;
		public UNIT_NO UnitNo
		{
			get { return m_UnitNo; }
			set { m_UnitNo = value; }
		}

		private DateTime m_StartTime = DateTime.MaxValue;
		public DateTime StartTime
		{
			get { return m_StartTime; }
			set { m_StartTime = value; }
		}

		private DateTime m_EndTime = DateTime.MaxValue;
		public DateTime EndTime
		{
			get { return m_EndTime; }
			set { m_EndTime = value; }
		}

		public MovingTime()
		{
		}
	}

	[Serializable]
	public class APD_Info
	{
		public APD_Info()
		{
		}

		private string m_strApdName = "";
		public string APD_Name
		{
			get { return m_strApdName; }
			set { m_strApdName = value; }
		}

		private string m_strApdVal = "";
		public string APD_Val
		{
			get { return m_strApdVal; }
			set { m_strApdVal = value; }
		}
	}
}
