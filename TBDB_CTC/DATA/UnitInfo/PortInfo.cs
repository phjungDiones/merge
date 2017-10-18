using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;
using CJ_Controls.Communication.Nano300;
using CJ_Controls;
using System.Threading;

namespace TBDB_CTC.Data.UnitInfo
{
	[Serializable]
	public class PortInfo
	{
		public PortInfo(int nPort_1Base)
		{
			PortNum = nPort_1Base;

			for (int nSlot = 0; nSlot < COUNT.MAX_PORT_SLOT; nSlot++)
			{
				_WaferList[nSlot] = new WaferInfo(PortNum, nSlot + 1);
			}

			_Nano300_Com.MappingEvent += new Nano300.MappingEventHandler(MappingEvent);
			_Thread_Seq = new Thread(new ThreadStart(Run));
			_Thread_Seq.IsBackground = true;
			_Thread_Seq.Start();
		}

		public PortInfo()
		{
			for (int nSlot = 0; nSlot < COUNT.MAX_PORT_SLOT; nSlot++)
			{
				_WaferList[nSlot] = new WaferInfo(PortNum, nSlot + 1);
			}
		}
		private void MappingEvent(object sender, MappingEventArgs args)
		{
            string str;
			for(int nSlot = 0; nSlot < COUNT.MAX_PORT_SLOT; nSlot++)
			{
				_WaferList[nSlot].WaferUse = args.Mapping_Slot[nSlot] == 1 ? true : false;
				_WaferList[nSlot].WaferCross = args.Mapping_Cross[nSlot] == 1 ? true : false;
				_WaferList[nSlot].WaferDouble = args.Mapping_Double[nSlot] == 1 ? true : false;

                str = string.Format("Slot{0} , {1}", nSlot, _WaferList[nSlot].WaferUse);
                Console.WriteLine(str);
			}
		}
		private void SeqLog(string strMsg)
		{
// 			Form_MainFrame _mainFrame = (Form_MainFrame)Class_Public.Instance.MainFrame;
// 			string strPortName = string.Format("PORT{0}", PortNum);
// 			_mainFrame.Comp_Tbdb.LogSaver.LogTextOut(strPortName, "[" +strPortName + "]" + strMsg);
		}
		
		private Thread _Thread_Seq;
		private Nano300 _Nano300_Com = new Nano300();
		public Nano300 GetNano300()
		{
			return _Nano300_Com;
		}

		public object Clone()
		{
			MemoryStream ms = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(ms, this);
			ms.Position = 0;
			object obj = bf.Deserialize(ms);
			ms.Close();
			return obj;
		}

		private WaferInfo[] _WaferList = new WaferInfo[COUNT.MAX_PORT_SLOT];
		public WaferInfo[] WaferData
		{
			get { return _WaferList; }
			set { _WaferList = value; }
		}

		public void ClearPortInfo()
		{
			FOUP_ID = "";
			PPID = "";
			LOT_ID = "";
			ProcessStartTime = DateTime.MaxValue;
			ProcessEndTime = DateTime.MaxValue;
			_PortStatusChangeTime = DateTime.MaxValue;
			_DataRcvTime = DateTime.MaxValue;
			for (int nSlot = 0; nSlot < (int)COUNT.MAX_PORT_SLOT; nSlot++)
			{
				_WaferList[nSlot].ClearInfo();
			}
		}
		private PORT_COMMAND _PortCommand = PORT_COMMAND.IDLE;
		public PORT_COMMAND PortCmd
		{
			get { return _PortCommand; }
		}
		private void CommandSet_IDLE()
		{
			_PortCommand = PORT_COMMAND.IDLE;
		}
		public bool SetCommand(PORT_COMMAND _Cmd)
		{
			bool bRtn = false;

            //switch(_Cmd)
            //{
            //    case PORT_COMMAND.CLEAR:
            //        _Nano300_Com.Cmd_Send_Reset();
            //        break;

            //    case PORT_COMMAND.HOME:
            //        _Nano300_Com.Cmd_Send_Home();
            //    break;

            //    case PORT_COMMAND.LOAD:

            //        break;

            //    case PORT_COMMAND.UNLOAD :
            //        break;

            //    case PORT_COMMAND.UNLOAD_OPTION:
            //        break;

            //    case PORT_COMMAND.CLAMP:
            //        _Nano300_Com.Cmd_Send_POD_LOCK(true);
            //        break;

            //    case PORT_COMMAND.UNCLAMP:
            //        _Nano300_Com.Cmd_Send_POD_LOCK(false);
            //        break;

            //    case PORT_COMMAND.UNCLAMP_OPTION:
            //        break;

            //    case PORT_COMMAND.FOUP_DOCK:
            //        _Nano300_Com.Cmd_Send_Dock(true);
            //        break;

            //    case PORT_COMMAND.FOUP_UNDOCK:
            //        _Nano300_Com.Cmd_Send_Dock(false);
            //        break;

            //    case PORT_COMMAND.DOOR_OPEN:
            //        _Nano300_Com.Cmd_Send_Open();
            //        break;

            //    case PORT_COMMAND.DOOR_CLOSE:
            //        _Nano300_Com.Cmd_Send_Close();
            //        break;

            //    case PORT_COMMAND.MAPPING:
            //        break;

            //    break;

            //}

            if (_PortCommand != PORT_COMMAND.IDLE)
            {
                bRtn = false;
            }
            else
            {
                _PortCommand = _Cmd;
                bRtn = true;
            }
			return bRtn;
		}

        public bool SedCmdFunc(PORT_COMMAND _Cmd)
        {
            bool bRtn = false;

            switch (_Cmd)
            {
                case PORT_COMMAND.CLEAR:
                    _Nano300_Com.Cmd_Send_Reset();
                    break;

                case PORT_COMMAND.HOME:
                    _Nano300_Com.Cmd_Send_Home();
                    break;

                case PORT_COMMAND.LOAD:
                    _Nano300_Com.Cmd_Send_Open();
                    break;

                case PORT_COMMAND.UNLOAD:
                    _Nano300_Com.Cmd_Send_Close();
                    break;

                case PORT_COMMAND.UNLOAD_OPTION:
                    break;

                case PORT_COMMAND.CLAMP:
                    _Nano300_Com.Cmd_Send_POD_LOCK(true);
                    break;

                case PORT_COMMAND.UNCLAMP:
                    _Nano300_Com.Cmd_Send_POD_LOCK(false);
                    break;

                case PORT_COMMAND.UNCLAMP_OPTION:
                    break;

                case PORT_COMMAND.FOUP_DOCK:
                    _Nano300_Com.Cmd_Send_Dock(true);
                    break;

                case PORT_COMMAND.FOUP_UNDOCK:
                    _Nano300_Com.Cmd_Send_Dock(false);
                    break;

                case PORT_COMMAND.DOOR_OPEN:
                    
                    break;

                case PORT_COMMAND.DOOR_CLOSE:
                    
                    break;

                case PORT_COMMAND.MAPPING:
                    _Nano300_Com.Cmd_Send_GetMapping();
                    break;

            }

            return bRtn;
        }

		public void ResetCommand_Seq()
		{
			m_nSeqNo = 0;
			m_nSeqNo_Sub = 0;
			CommandSet_IDLE();
			_Nano300_Com.ResetSeq();
		}
		public bool IsClamp()
		{
			bool bRtn = false;
			if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_CLAMPED] == 1
				&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNCLAMPED] == 0)
			{
				bRtn=true;
			}
			return bRtn;
		}
		public bool IsDock()
		{
			bool bRtn = false;
			if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_DOCKED] == 1
				&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNDOCKED] == 0)
			{
				bRtn=true;
			}
			return bRtn;
		}
		public bool IsDoorOpen()
		{
			bool bRtn = false;
			if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_RESERVED] == 1
				&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_OPEN] == 1
				&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_CLOSE] == 0)
			{
				bRtn=true;
			}
			return bRtn;
		}

		//Public
		private int _PortNum = 0;
		public int PortNum
		{
			get { return _PortNum; }
			set
			{ 
				if (_PortNum != value)
				{
					_PortNum = value;
					switch (_PortNum)
					{
						case 1: FOUP_ID = "LPMA"; break;
						case 2: FOUP_ID = "LPMB"; break;
						case 3: FOUP_ID = "LPMC"; break;
						case 4: FOUP_ID = "LPMD"; break;
					}
				}
			}
		}
		private string _FOUP_ID = "";
		public string FOUP_ID
		{
			get { return _FOUP_ID; }
			set
			{
				_FOUP_ID = value;
			}
		}
		public string PPID = "";
		public string LOT_ID = "";
		public WAFER_MODE WaferMode = WAFER_MODE.DEVICE;
		private PORT_STATUS _PortStatus = PORT_STATUS.EMPTY;
		public PORT_STATUS PortStatus
		{
			get { return _PortStatus; }
			set
			{
				if (_PortStatus != value)
				{
					BeforePortStatus = _PortStatus;
					_PortStatus = value;
					_PortStatusChangeTime = DateTime.Now;

					if (_PortStatus == PORT_STATUS.EMPTY)
					{
					}
					else if (_PortStatus == PORT_STATUS.RESERVE)
					{
					}
					else if (_PortStatus == PORT_STATUS.LOAD_COMPLETE)
					{
					}
					else if (_PortStatus == PORT_STATUS.BUSY)
					{
					}
					else if (_PortStatus == PORT_STATUS.UNLOAD_COMPLETE)
					{
					}
				}
			}
		}
		private PORT_STATUS _BeforePortStatus = PORT_STATUS.EMPTY;
		public PORT_STATUS BeforePortStatus
		{
			get { return _BeforePortStatus; }
			set { _BeforePortStatus = value; }
		}
		private DateTime _ProcessStartTime = DateTime.MaxValue;
		public DateTime ProcessStartTime
		{
			get { return _ProcessStartTime; }
			set { _ProcessStartTime = value; }
		}
		private DateTime _ProcessEndTime = DateTime.MaxValue;
		public DateTime ProcessEndTime
		{
			get { return _ProcessEndTime; }
			set { _ProcessEndTime = value; }
		}
		private DateTime _PortStatusChangeTime = DateTime.MaxValue;
		public DateTime PortStatusChangeTime
		{
			get { return _PortStatusChangeTime; }
			set { _PortStatusChangeTime = value; }
		}
		private DateTime _DataRcvTime = DateTime.MaxValue;
		public DateTime DataRcvTime
		{
			get { return _DataRcvTime; }
			set { _DataRcvTime = value; }
		}

		private const int TIME_OUT_DELAY = 1000 * 30;
		private int m_nSeqNo = 0;
		private int m_nSeqNo_Sub = 0;
		private DateTime m_TimeOut = DateTime.MaxValue;
		public long GetElapsed(DateTime _Time, bool bSec)
		{
			long nElapsed = ((DateTime.Now.Ticks - _Time.Ticks) / 10000);
			
			if (bSec == true)
			{
				nElapsed = nElapsed / 1000;
			}
			return nElapsed;
		}
		private void Run()
		{
			while (true)
			{
				Thread.Sleep(100);
				if (_Nano300_Com.IsOpen() == false)
					continue;

				int nSeqNo = m_nSeqNo;
				int nSeqNo_Sub = m_nSeqNo_Sub;
				string strBuf = "";

                //Test
                continue; 

				switch (nSeqNo)
				{
					case 0:
						{
							if (_Nano300_Com.IsOpen() == true)
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Port Sequence Start.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								//흠....시작안됨..
							}
						} break;
					case 100:
						{
							#region Status Check
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Status();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
								{
									//자동 홈..위험한가?
									//if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_HOME_COMP] != 1)
									//{
									//	nSeqNo = 2000;
									//	strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check OK.", m_nSeqNo, nSeqNo);
									//	SeqLog(strBuf);
									//}
									//else
									{
										nSeqNo = 1000;
										m_TimeOut = DateTime.Now;
										strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check OK.", m_nSeqNo, nSeqNo);
										SeqLog(strBuf);
									}
								}
								else if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Error ({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 1000:
						{
							switch (_PortCommand)
							{
								case PORT_COMMAND.IDLE: nSeqNo = 100; break;
								case PORT_COMMAND.HOME: nSeqNo = 2000; break;
								case PORT_COMMAND.LOAD: nSeqNo = 3000; break;
								case PORT_COMMAND.UNLOAD: nSeqNo = 4000; break;
								case PORT_COMMAND.CLAMP: nSeqNo = 5000; break;
								case PORT_COMMAND.UNCLAMP: nSeqNo = 6000; break;
								case PORT_COMMAND.FOUP_DOCK: nSeqNo = 7000; break;
								case PORT_COMMAND.FOUP_UNDOCK: nSeqNo = 8000; break;
								case PORT_COMMAND.DOOR_OPEN: nSeqNo = 9000; break;
								case PORT_COMMAND.DOOR_CLOSE: nSeqNo = 4000; break;
								case PORT_COMMAND.MAPPING: nSeqNo = 11000; break;
							}
						} break;
					case 2000:
						{
							#region HOME
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Home Send.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
                                _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_HOME_COMP] = 0; //init
								_Nano300_Com.Cmd_Send_Home();
								//if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
                                if (_Nano300_Com.WorkStatus != WORK_STATUS.ERROR)                                
								{
									nSeqNo = 2100;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Go Home Check.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
								else if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Home Error({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Home Send Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion HOME
						} break;
					case 2100:
						{
							#region Status Check (Home Check)
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Status();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Error ({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
								else
								{
									nSeqNo = 2200;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check OK.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 2200:
						{
							#region Home Check
                            if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_HOME_COMP] == 1)
                            {
                                nSeqNo = 100;
                                CommandSet_IDLE();
                                strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Home Complete.", m_nSeqNo, nSeqNo);
                                SeqLog(strBuf);
                            }
                            else
                            {
                                if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
                                {
                                    strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check.", m_nSeqNo, nSeqNo);
                                    SeqLog(strBuf);
                                    _Nano300_Com.Cmd_Send_Status();

                                    //nSeqNo = 2100;
                                }
                                if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
                                {
                                    strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Error ({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
                                    SeqLog(strBuf);
                                    _Nano300_Com.ResetSeq();
                                }
                            }
							#endregion
						} break;
					case 3000:
						{
							#region LOAD (Reserved Check)
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_RESERVED] == 1)
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 3100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Check is Reserved.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								CommandSet_IDLE();
								m_TimeOut = DateTime.Now;
								nSeqNo = 100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Check is Not Reserved.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							#endregion
						} break;
					case 3100:
						{
							#region LOAD (Clamp)
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_CLAMPED] == 1)
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 9000;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Clamped.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Clamp Send.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);

									_Nano300_Com.Cmd_Send_POD_LOCK(true);
									if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
									{
										strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Clamp Error({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
										SeqLog(strBuf);
										_Nano300_Com.ResetSeq();
									}
									else
									{
										nSeqNo = 3200;
										strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Go Clamp Check.", m_nSeqNo, nSeqNo);
										SeqLog(strBuf);
									}
								}
							}
							#endregion
						} break;
					case 3200:
						{
							#region Status Check (Clamp Check)
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Status();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
								{
									nSeqNo = 3300;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check OK.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
								else if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Error ({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 3300:
						{
							#region Clamp Check
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_CLAMPED] == 1)
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 9000;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Clamped.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 3200;
							}
							#endregion
						} break;
					case 4000:
						{
							#region UNLOAD (Reserved Check)
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_RESERVED] == 0
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_OPEN] == 0
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_CLOSE] == 1)
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Unload Check is Unload.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								CommandSet_IDLE();
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 4100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Unload Check is not Unload.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							#endregion
						} break;
					case 4100:
						{
							#region UNLOAD
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Unload Send.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Close();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Unload Error({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
								else
								{
									m_TimeOut = DateTime.Now;
									nSeqNo = 4200;

									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Go Unload Check.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Unload Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 4200:
						{
							#region Status Check (Unload Check)
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Status();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
								{
									nSeqNo = 4300;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check OK.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
								else if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Error ({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 4300:
						{
							#region UNLOAD (Reserved Check)
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_RESERVED] == 0
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_OPEN] == 0
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_CLOSE] == 1)
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Unload Check is Unload.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								CommandSet_IDLE();
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 4200;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Unload Check is not Unload.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							#endregion
						}break;
					case 5000:
						{
							#region Clamp Check
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_CLAMPED] == 1
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNCLAMPED] == 0)
							{
								CommandSet_IDLE();
								nSeqNo = 100;
								m_TimeOut = DateTime.Now;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Check is Clamped.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 5100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Check is not Clamped.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							#endregion
						} break;
					case 5100:
						{
							#region Clamp
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Clamp Send.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_POD_LOCK(true);

								if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Clamp Error({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
								else
								{
									nSeqNo = 5200;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Go Clamp Check.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Clamp Send Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 5200:
						{
							#region Status Check (Clamp Check)
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Status();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
								{
									nSeqNo = 5300;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check OK.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
								else if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Error ({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 5300:
						{
							#region Clamp Check
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_CLAMPED] == 1
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNCLAMPED] == 0)
							{
								CommandSet_IDLE();
								nSeqNo = 100;
								m_TimeOut = DateTime.Now;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Clamped.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 5200;
							}
							#endregion
						} break;
					case 6000:
						{
							#region UnClamp Check
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_CLAMPED] == 0
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNCLAMPED] == 1)
							{
								CommandSet_IDLE();
								nSeqNo = 100;
								m_TimeOut = DateTime.Now;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Check is UnClamped.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 6100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Check is not UnClamped.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							#endregion
						}break;
					case 6100:
						{
							#region UnClamp
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => UnClamp Send.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_POD_LOCK(false);

								if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => UnClamp Error({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
								else
								{
									nSeqNo = 6200;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Go UnClamp Check.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => UnClamp Send Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 6200:
						{
							#region Status Check (UnClamp Check)
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Status();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
								{
									nSeqNo = 6300;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check OK.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
								else if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Error ({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 6300:
						{
							#region Clamp Check
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_CLAMPED] == 0
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNCLAMPED] == 1)
							{
								CommandSet_IDLE();
								nSeqNo = 100;
								m_TimeOut = DateTime.Now;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Clamped.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 6200;
							}
							#endregion
						} break;

					case 7000:
						{
							#region Foup Dock Check
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_DOCKED] == 1
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNDOCKED] == 0)
							{
								CommandSet_IDLE();
								nSeqNo = 100;
								m_TimeOut = DateTime.Now;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Check is Docked.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 7100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Check is not Docked.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							#endregion
						}break;
					case 7100:
						{
							#region Foup Dock
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Dock Send.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Dock(true);
								
								if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Dock Error({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
								else
								{
									nSeqNo = 7200;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Go Foup Dock.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Dock Send Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 7200:
						{
							#region Status Check (Docked Check)
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Status();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
								{
									nSeqNo = 7300;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check OK.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
								else if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Error ({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 7300:
						{
							#region Dock Check
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_DOCKED] == 1
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNDOCKED] == 0)
							{
								CommandSet_IDLE();
								nSeqNo = 100;
								m_TimeOut = DateTime.Now;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Docked.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 7200;
							}
							#endregion
						} break;
					case 8000:
						{
							#region Foup UnDock Check
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_DOCKED] == 0
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNDOCKED] == 1)
							{
								CommandSet_IDLE();
								nSeqNo = 100;
								m_TimeOut = DateTime.Now;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Check is UnDocked.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 8100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Check is not UnDocked.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							#endregion
						}break;
					case 8100:
						{
							#region Foup UnDock
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup UnDock Send.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Dock(false);
								
								if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup UnDock Error({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
								else
								{
									nSeqNo = 100;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Go Foup UnDock Check.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup UnDock Send Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 8200:
						{
							#region Status Check (Docked Check)
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Status();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
								{
									nSeqNo = 8300;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check OK.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
								else if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Error ({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 8300:
						{
							#region Dock Check
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_DOCKED] == 0
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNDOCKED] == 1)
							{
								CommandSet_IDLE();
								nSeqNo = 100;
								m_TimeOut = DateTime.Now;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Docked.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								m_TimeOut = DateTime.Now;
								nSeqNo = 8200;
							}
							#endregion
						} break;
					case 9000:
						{
							#region DOOR_OPEN (Clamp 이후 부터 Door Open 까지.)
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Door Open Send.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Open();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Door Open Error({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
								else
								{
									m_TimeOut = DateTime.Now;
									nSeqNo = 9100;

									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Go Door Open Check.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Door Open Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 9100:
						{
							#region Status Check (DOOR_OPEN)
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_Status();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
								{
									nSeqNo = 9200;
									m_TimeOut = DateTime.Now;
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check OK.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
								else if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Error ({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Status Check Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
					case 9200:
						{
							#region Open Check
							if (_Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_RESERVED] == 1
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_OPEN] == 1
								&& _Nano300_Com.LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_CLOSE] == 0)
							{
								CommandSet_IDLE();
								m_TimeOut = DateTime.Now;
								nSeqNo = 100;
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Foup Load Check is Loaded.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
							}
							else
							{
								nSeqNo = 9100;
							}
							#endregion
						} break;
					case 11000:
						{
							#region MAPPING
							if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
							{
								strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Get Mapping Send.", m_nSeqNo, nSeqNo);
								SeqLog(strBuf);
								_Nano300_Com.Cmd_Send_GetMapping();

								if (_Nano300_Com.WorkStatus == WORK_STATUS.IDLE)
								{
									m_TimeOut = DateTime.Now;
									nSeqNo = 100;
									CommandSet_IDLE();

									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Get Mapping Complete.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
								}
								else if (_Nano300_Com.WorkStatus == WORK_STATUS.ERROR)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Get Mapping Error({2}). Reset.", m_nSeqNo, nSeqNo, _Nano300_Com.ErrorMessage);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							else
							{
								long nElapsed = GetElapsed(m_TimeOut, false);
								if (nElapsed >= TIME_OUT_DELAY)
								{
									strBuf = string.Format("SeqNo:{0}, NextNo:{1} => Get Mapping Timeout Error. Reset.", m_nSeqNo, nSeqNo);
									SeqLog(strBuf);
									_Nano300_Com.ResetSeq();
								}
							}
							#endregion
						} break;
				}

				m_nSeqNo = nSeqNo;
				m_nSeqNo_Sub = nSeqNo_Sub;
			}
		}
	}
}
