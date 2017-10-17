using System;
using System.Collections.Generic;
using System.Text;

namespace TBDB_CTC.Data.Alarm
{
	#region ALARM List
	public enum EQP_ALARM : int
	{
		START = 10000,
		err_Timeout,
		err_Ems,
		err_Door,
		END,
	}
	#endregion

	public enum ALARM_TYPE : int
	{
		WARNING = 0,
		ALARM,
		CLEAR,
	}

	public class AlarmListStruct
	{
		int _AlarmID;
		int _Index;
		int _UnitID;
		string _AlarmText;
		string _AlarmLevel;

		public int Index
		{
			get { return _Index; }
			set { _Index = value; }
		}
		public int AlarmID
		{
			get { return _AlarmID; }
			set { _AlarmID = value; }
		}
		public int UnitID
		{
			get { return _UnitID; }
			set { _UnitID = value; }
		}
		public string AlarmText
		{
			get { return _AlarmText; }
			set { _AlarmText = value; }
		}
		public string AlarmLevel
		{
			get { return _AlarmLevel; }
			set { _AlarmLevel = value; }
		}
		public AlarmListStruct()
		{
		}
		public AlarmListStruct(int index, int id, int unitid, string alarmtext, string alarmlevel)
		{
			_Index = index;
			_AlarmID = id;
			_UnitID = unitid;
			_AlarmText = alarmtext;
			_AlarmLevel = alarmlevel;
		}
	}
}
