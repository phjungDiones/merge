using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBDB_CTC.Data
{
	public class COUNT
	{
		public const int MAX_PORT = 4;
		public const int MAX_PORT_SLOT = 25;
		public const int MAX_RETRY_COUNT = 3;
		public const int RETRY_TIME_SEC = 3;
	}

	public enum UNIT_STATUS
	{
		IDLE = 0,
		RUN,
		DOWN,
		END,
	}

	public enum EQP_CONCEPT
	{
		LAMI_ONLY = 0,
		BONDER_ONLY,
		FULL_AUTO,
	}

	public enum WAFER_STATUS
	{
		IDLE = 0,
		WAIT,
		RUN,
		END,
		SCRAP,
		CROSS,
		DOUBLE,
	}

	public enum WAFER_MODE
	{
		DEVICE = 0,
		CARRIER = 1,
	}
	public enum UNIT_NO : int
	{
		EQUIPMENT = 0,
		LOADER = 1, //EFEM / Loader
		ATM_TM = 2,
		LAMINATOR = 3,
		HOT_PLATE = 4,
		VACCUM_TM = 5,
		BONDER = 6,
	}

	public enum WAFER_PATH_DEVICE : int
	{//디바이스 웨이퍼 순서.
		START = 0,
		FM_1,
		ALIGNER,
		ATM_TM_1,
		BUFFER,
		VAC_TM_START_BOND,
		BONDER,
		VAC_TM_END_BOND,
		HOT_PLATE,
		ATM_TM_2,
		COOLING_PLATE,
		FM_2,
		END,
	}

	public enum WAFER_PATH_CARRIER : int
	{//캐리어 웨이퍼 순서.
		START = 0,
		FM_1,
		ALIGNER_LAMI,
		ATM_TM_1,
		LAMINATER,
		ATM_TM_2,
		ALIGNER_BOND,
		ATM_TM_3,
		BUFFER,
		VAC_TM_START_BOND,
		BONDER,
		VAC_TM_END_BOND,
		HOT_PLATE,
		ATM_TM_4,
		COOLING_PLATE,
		FM_2,
		END,
	}

#region Port 관련
	public enum PORT_STATUS : int
	{
		EMPTY = 0,			//초기상태
		RESERVE,			//클램프까지만
		LOAD_COMPLETE,		//Open까지.
		BUSY,				//작업중.
		UNLOAD_COMPLETE,	//Close까지.
	}

	public enum PORT_COMMAND
	{
		IDLE = 0,
		HOME,
		LOAD,
		UNLOAD,
		UNLOAD_OPTION,
		CLAMP,
		UNCLAMP,
		UNCLAMP_OPTION,
		FOUP_DOCK,
		FOUP_UNDOCK,
		DOOR_OPEN,
		DOOR_CLOSE,
		MAPPING,
        CLEAR,  
	}
#endregion
}
