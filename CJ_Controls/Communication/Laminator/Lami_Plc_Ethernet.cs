using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TBDB_CTC.Comm.Lami_PLC
{
	public enum CTC_PLC_ADDR
	{ //시작 번지 : D10020
		D10020 = 0,
		REMOTE_START_REQUEST,
		REMOTE_STOP_REQUEST,
		D10023,
		TM_LOAD_COMPLETE,
		DUAL_ARM_WAFER_PLACE_ACK,
		TM_UNLOAD_COMPLETE,
		PROCESS_PAUSE_REQUEST,
		PROCESS_RESUME_REQUEST,
	}
	public enum LAMI_PLC_ADDR
	{ //시작 번지 : D10000
		LAMINATOR_READY = 0, //1 or 0
		PROCESS_START_ACK,
		PROCESS_STOP_ACK,
		LOAD_REQUEST,
		D10004,
		UNLOAD_REQUEST,
		DUAL_ARM_WAFER_PLACE, // 안씀
		PROCESS_PAUSE_ACK,
		PROCESS_RESUME_ACK,
		PROCESS_RESUME_NAK,
		ALARM_LIGHT_LAMP,
		ALARM_HIGH_LAMP,
		ALARM_LIGHT_RESET_LAMP,
		ALARM_HIGH_RESET_LAMP,
		WAFER_EJECTION_POSSIBILITY_CHECK,
		WAFER_EJECTION_RUN,
		D10016,
		WAFER_POSITION,		// 0-6
		FILM_POSITION,		// 0-5
		RECIPE_CHANGE_ACK,	//1-2
	}
	public enum RECIPE_PLC_ADDR
	{//D4800 부터 시작.
		CHAMBER_UP_TEMP = 0,
		CHAMBER_LO_TEMP,
		MAX_TORQUE = 10,
		TORQUE_OFFSET = 12,
		PRESS_TON = 14,
		CHAMBER_LO_MOTOR_PRESS_WAIT_TIME = 16,
		PUMP_OPEN_PRESURE = 18,
		PUMP_COMPLETE_PRESURE = 20,
		VENT_VALVE_OPEN_PRESURE = 22,
		VENT_COMPETE_PRESURE = 24,
		MAX_SIZE,
	}
	public enum WAFER_POS
	{
		Empty = 0,
		Lifter_In,
		Handler_In,
		Chamber,
		Handler_Out,
		Third_Remover,
		Lifter_Out,
	}
	public enum FILM_POS
	{
		Empty = 0,
		Pinnacle_Cutter,
		Line_Cutter,
		Second_Remover,
		Handler,
		Chamber,
	}
	public class Lami_Plc_Ethernet
	{
		public Lami_Plc_Ethernet()
		{
			Init_MXComp();

			m_Thread = new Thread(new ThreadStart(Run));
			m_Thread.IsBackground = true;
			m_Thread.Start();
		}
		~Lami_Plc_Ethernet()
		{
			try
			{
				_ActTcp.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private Thread m_Thread;
		private ACTETHERLib.ActQJ71E71TCP _ActTcp = new ACTETHERLib.ActQJ71E71TCP();
		private bool Init_MXComp()
		{
			bool bConnect = false;
			_ActTcp.ActHostAddress = "192.168.3.100";
			int nRet = _ActTcp.Open();
			if (nRet == 0)
			{
				bConnect = true;
			}
			return bConnect;
		}

		private const int _Delay = 100;
		private short[] m_Lamination_Map = new short[20];
		private short[] m_CTC_Map = new short[20];
		private short[] m_LamiRecipeMap = new short[(int)RECIPE_PLC_ADDR.MAX_SIZE];
		private void Run()
		{
			while (true)
			{
				Thread.Sleep(_Delay);
				Read_Lami_All();
				Thread.Sleep(_Delay);
				Write_CTC_All();

				if (m_WriteRecipe_Start == true)
				{
					Thread.Sleep(_Delay);
					if (Write_Recipe_All() == true)
					{
						m_WriteRecipe_Start = false;
					}
				}
			}
		}

		private void Read_Lami_All()
		{
			short[] _Value = null;
			int nReadSize = 20;
			if (ReadPlc_Block("D10000", nReadSize, out _Value) == true)
			{
				if (_Value == null)
					return;
				if (_Value.Length != nReadSize)
					return;

				for (int i = 0; i < _Value.Length; i++)
				{
					m_Lamination_Map[i] = _Value[i];
				}
			}
		}
		private bool Write_CTC_All()
		{
			return WritePlc_Block("D10020", m_CTC_Map);
		}
		private bool Write_Recipe_All()
		{
			return WritePlc_Block("D4800", m_LamiRecipeMap);
		}

		private bool ReadPlc_Block(string _Addr, int nSize, out short[] _Value)
		{
			_Value = new short[nSize];
			int nRet = _ActTcp.ReadDeviceBlock2(_Addr, nSize, out _Value[0]);
			return nRet == 0 ? true : false;
		}
		private bool WritePlc_Block(string _Addr, short[] _Value)
		{
			int nRet = _ActTcp.WriteDeviceBlock2(_Addr, _Value.Length, ref _Value[0]);
			return nRet == 0 ? true : false;
		}

		public short Read_Lami_Bit(LAMI_PLC_ADDR _Addr)
		{
			return m_Lamination_Map[(int)_Addr];
		}
		public void Send_CTC_To_Lami(CTC_PLC_ADDR _Addr, bool bOn)
		{
			m_CTC_Map[(int)_Addr] = bOn ? (short)1 : (short)0;
		}
		public void SetRecipe(RECIPE_PLC_ADDR _Addr, short sVal)
		{
			m_LamiRecipeMap[(int)_Addr] = sVal;
		}

		//인터페이스
		private bool m_WriteRecipe_Start = false;
		public bool WriteRecipe_Start
		{
			get { return m_WriteRecipe_Start; }
			set { m_WriteRecipe_Start = value; }
		}
		
	}
}
