using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CJ_Controls.Communication.Nano300
{//cjinnnn: 20170417. EFEM 모듈.
	#region DEFINE Data
	public enum WORK_STATUS
	{
		IDLE = 0,
		SENDING,
		READING,
		ACK_CHECK,
		ERROR = -1
	}
	public enum ACK_MODE
	{
		NONE = 0,
		ACK,
		EVENT_DATA,
		STATUS_DATA,
		MAPPING_DATA,
		CHECK_DATA,
		ERROR = -1,
		NAK = -2,
		BEFORE_CMD_PROC = -3,
	}
	public enum EVENT_DATA
	{
		EVT_TP_CONNECT=0,
		EVT_TP_DISCONNECT,
		EVT_LOAD_BUTTON_PUSH,
		EVT_UNLOAD_BUTTON_PUSH,
		EVT_POD_IN,
		EVT_POD_OUT,
		EVT_START_RESET=8,
		EVT_END_RESET,
		EVT_UNLOAD_TIMEOUT=12,
		EVT_FOUP_START_READY=16,
		EVT_AMHS_HANDOFF_STARTED=20,
		EVT_AMHS_HANDOFF_COMPLETE,
		EVT_EXCEPTION_HANDOFF,
		EVT_EMERGENCY_STOP_KEY=25,
		MAX_COUNT=32,
	}
	public enum STATUS_DATA
	{
		STS_HOME_COMP = 0,
		STS_WSO_ON,
		STS_DOOR_OPEN,
		STS_DOOR_CLOSE,
		STS_ACTING,
		STS_CRASH_DATA,
		STS_MAINT_MODE_CONDITION,
		STS_LATCH_LOCKED,
		STS_LATCH_UNLOCKED,
		STS_FOUP_CLAMPED,
		STS_FOUP_UNCLAMPED = 10,

		STS_FOUP_DOCKED,
		STS_FOUP_UNDOCKED,
		STS_FINGER_ARM_EXTENDED,
		STS_FINGER_ARM_RETRACTED,
		STS_VACCUM_STATUS,
		STS_LATCH_SENS_USED,
		STS_PLOCK_SENS_USED,
		STS_PLACSE_SENS_USED,
		STS_VACCUM_SENS_USED,
		STS_AMHS_MODE = 20,

		STS_OPTION_USAGE,
		STS_MAPPING_FUNC_USAGE,
		STS_AUTO_MODE_CONDITION,
		STS_LOAD_UNLOAD_ID_SWITCH_USAGE,
		STS_BUZZER_USED_ON_ALARM,
		STS_DOOR_UP_POSITION,
		STS_DOOR_DOWN_POSITION,
		STS_PLACEMENT_SENS_CONDITION,
		STS_PRESENT_SENS_CONDITION,
		STS_FOSB_FOUP_PLACEMENT = 30, //STS_RESERVED 밑에꺼랑 같은데.... 이름만다름...
		STS_RESERVED = STS_FOSB_FOUP_PLACEMENT,
		STS_ALARM_OCCURED,
		MAX_COUNT,
	}
	public enum LED_STATE
	{
		OFF=0,
		ON,
		BLINK,
	}
	public enum COUNT
	{
		MAX_SLOT = 25,
	}
	#endregion
	public class Nano300
	{
		#region Log 보내기
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public event MessageEventHandler MessageEvent;
		private void LogTextOut(string message)
		{
			if (MessageEvent != null)
			{
				string strInfo = string.Format("Nano300({0})=>", m_SerialPort.PortName);
				MessageEvent(this, new MessageEventArgs(strInfo + message));
			}
		}
		#endregion

		#region Mapping Data 보내기
		public delegate void MappingEventHandler(object sender, MappingEventArgs args);
		public event MappingEventHandler MappingEvent;
		private void MappingData_Event(byte[] _Slot, byte[] _Cross, byte[] _Double)
		{
			if (MappingEvent != null)
			{
				MappingEvent(this, new MappingEventArgs(_Slot, _Cross, _Double));
			}
		}
		#endregion

		Thread m_Thread_Read = null;
		public Nano300_Data LPM_Data = new Nano300_Data();
		public Nano300()
		{
			m_Thread_Read = new Thread(new ThreadStart(SerialPort_DataReceived));
			m_Thread_Read.IsBackground = true;
			m_Thread_Read.Start();
		}

		private readonly int MaxRetryCount = 3;
		private readonly int Loop_Delay = 100;
		private readonly int READ_END_DELAY = 2000;
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

					if (m_nSeqNo == 0)
					{
						string strRcv = "";
						RcvCheck(ref strRcv);
					}
				}
				catch (Exception ex)
				{
					LogTextOut(string.Format("SerialPort_DataReceived/{0}", ex.Message));
				}
			}
		}

		private ACK_MODE RcvCheck(ref string sRcvData)
		{
			ACK_MODE _AckMode = ACK_MODE.NONE;
			if (IsReceived("\n") == true)
			{
				byte[] byRcv = GetReceiveDataByteEx_EndCut();
				sRcvData = GetReceiveDataEx_EndCut();
				LogTextOut(string.Format("RecvData:{0}", sRcvData));

				if (byRcv[0] == 'A')
				{//정상적으로 수행
					FlushEx_EndCheckCut();
					_AckMode = ACK_MODE.ACK;
				}
				else if (byRcv[0] == 'N')
				{//정상적인 커맨드가 아님.
					FlushEx_EndCheckCut();
					_ErrMsg = "NAK.";
                    Global.GlobalFunction.Instance.SetErr(_ErrMsg, Global.GlobalDefine.Eidentify_error.Nano300);
                    _AckMode = ACK_MODE.NAK;
				}
				else if (byRcv[0] == 'B')
				{//이미 명령 수행 중.
					FlushEx_EndCheckCut();
					_ErrMsg = "Before Comand Processing.";
                    Global.GlobalFunction.Instance.SetErr(_ErrMsg, Global.GlobalDefine.Eidentify_error.Nano300);
					_AckMode = ACK_MODE.BEFORE_CMD_PROC;
				}
				else if (byRcv[0] == 'E')
				{//Alarm이 발생하여 Error Code 전송.
					FlushEx_EndCheckCut();
					_ErrMsg = Encoding.ASCII.GetString(byRcv, 0, byRcv.Length);
                    Global.GlobalFunction.Instance.SetErr(_ErrMsg, Global.GlobalDefine.Eidentify_error.Nano300);
                    _AckMode = ACK_MODE.ERROR;
				}
				else if (byRcv[0] == 'C')
				{//EVENT 정보 전송.
					FlushEx_EndCheckCut();
					_AckMode = ACK_MODE.EVENT_DATA;
					#region Event Data 처리
					string strRcv = sRcvData;
					strRcv = strRcv.Trim();
					strRcv = strRcv.Trim('C');
					strRcv = strRcv.Replace("\n", "");
					
					int nDec = 0;
					int.TryParse(strRcv, System.Globalization.NumberStyles.HexNumber, null, out nDec);
					for (int nBit = 0; nBit < (int)EVENT_DATA.MAX_COUNT; nBit++)
					{
						LPM_Data.EventData[nBit] = (byte)((nDec >> nBit) & 0x00000001);
					}
					#endregion
				}
				else if (byRcv[0] == 'S')
				{//상태조회 커맨드
					FlushEx_EndCheckCut();
					_AckMode = ACK_MODE.STATUS_DATA;

					#region Status Data 처리
					string strRcv = sRcvData;
					strRcv = strRcv.Trim();
					strRcv = strRcv.Trim('S');
					strRcv = strRcv.Replace("\n", "");

					int nDec = 0;
					int.TryParse(strRcv, System.Globalization.NumberStyles.HexNumber, null, out nDec);
					for (int nBit = 0; nBit < (int)STATUS_DATA.MAX_COUNT; nBit++)
					{
						LPM_Data.StatusData[nBit] = (byte)((nDec >> nBit) & 0x00000001);
					}
					#endregion
				}
				else if (byRcv[0] == 'M')
				{//맵핑 데이터 커맨드
					FlushEx_EndCheckCut();
					Mapping_Proc(sRcvData);
					_AckMode = ACK_MODE.MAPPING_DATA;
				}
				else
				{//위의것이 아니면, 거의 체크 데이터로 본다. 그냥..
					FlushEx_EndCheckCut();
					_AckMode = ACK_MODE.CHECK_DATA;
				}
			}
			return _AckMode;
		}
		private void Mapping_Proc(string strMap)
		{
			string strRcv = strMap;
			strRcv = strRcv.Trim();
			strRcv = strRcv.Trim('M');
			strRcv = strRcv.Replace("\n", "");

			List<string> _MapList = new List<string>();
			_MapList.AddRange(strRcv.Split(','));
			if(_MapList.Count != 3)
			{
				return;
			}

			for (int i = 0; i < _MapList.Count; i++)
			{
				int nDec = 0;
				int.TryParse(_MapList[i], System.Globalization.NumberStyles.HexNumber, null, out nDec);

				switch(i)
				{
					case 0://Slot Mapping
						{
							for (int nSlot = 0; nSlot < (int)COUNT.MAX_SLOT; nSlot++)
							{
								LPM_Data.Mapping_Slot[nSlot] = (byte)((nDec >> nSlot) & 0x00000001);
							}
						}break;
					case 1://Cross Mapping
						{
							for (int nSlot = 0; nSlot < (int)COUNT.MAX_SLOT; nSlot++)
							{
								LPM_Data.Mapping_Cross[nSlot] = (byte)((nDec >> nSlot) & 0x00000001);
							}
						}break;
					case 2://Double Mapping
						{
							for (int nSlot = 0; nSlot < (int)COUNT.MAX_SLOT; nSlot++)
							{
								LPM_Data.Mapping_Double[nSlot] = (byte)((nDec >> nSlot) & 0x00000001);
							}
						}break;
				}
			}

			MappingData_Event(LPM_Data.Mapping_Slot, LPM_Data.Mapping_Cross, LPM_Data.Mapping_Double);

            //맵핑 완료 flag
            LPM_Data.bRecvMapping = true;
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
		private void FlushEx_EndCheckCut()
		{//cjinnnn: 연속으로 붙어오는 데이터 처리를 위한 함수. \r 단위로 자른다.
			if (IsOpen() == false)
				return;
			try
			{
				int nIndex = IsReceived((byte)ASCII.LF);
				if (nIndex <= 0)
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
				else
				{
					lock (obj)
					{
						byte[] TempBuf = new byte[MAX_BUFFER];
						Array.Copy(m_ReceiveBuffer, nIndex + 1, TempBuf, 0, m_ReadCount - (nIndex + 1));
						Array.Clear(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length);
						Array.Copy(TempBuf, 0, m_ReceiveBuffer, 0, m_ReceiveBuffer.Length);
						m_ReadCount -= (nIndex + 1);
					}
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
			catch
			{
				return false;
			}

			return true;
		}
        public bool SendData_Test(string str)
        {
            try
            {
                if (m_SerialPort.IsOpen.Equals(true))
                {
                    m_SerialPort.Write(str + "\n");
                    LogTextOut(string.Format("SendData:{0}", str));
                }
                else
                    return false;
            }
            catch
            {
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
			for (int i = 0; i < m_ReadCount - 1; i++)
			{
				if (m_ReceiveBuffer[i] == strCh1
					&& m_ReceiveBuffer[i + 1] == strCh2)
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
		private string GetReceiveDataEx_EndCut()
		{
			int nIndex = IsReceived((byte)ASCII.LF);
			return Encoding.ASCII.GetString(m_ReceiveBuffer, 0, nIndex + 1);
		}
		private byte[] GetReceiveDataByte()
		{
			return m_ReceiveBuffer;
		}
		private byte[] GetReceiveDataByteEx_EndCut()
		{
			byte[] _Rtn;
			int nIndex = IsReceived((byte)ASCII.LF);
			if (nIndex <= 0)
				_Rtn = m_ReceiveBuffer;
			else
			{
				byte[] TempBuf = new byte[nIndex+1];
				Array.Copy(m_ReceiveBuffer, 0, TempBuf, 0, TempBuf.Length);
				_Rtn = TempBuf;
			}

			return _Rtn;
		}
		private WORK_STATUS _WorkStatus = WORK_STATUS.IDLE;
		public WORK_STATUS WorkStatus
		{
			get { return _WorkStatus; }
		}
		private string _ErrMsg = "";
		public string ErrorMessage
		{
			get { return _ErrMsg; }
		}
		private string _WarMsg = "";
		public string WarningMessage
		{
			get { return _WarMsg; }
		}
		public void ResetSeq()
		{
			Flush();
			m_nSeqNo = 0;
			m_RetryCount = 0;
			_WorkStatus = WORK_STATUS.IDLE;
		}
		private DateTime m_TimeOut = DateTime.MaxValue;
		private int m_nSeqNo = 0;
		private int m_RetryCount = 0;
		private int Seq_SendCommand(string strCmd)
		{
			int nSeqNo = m_nSeqNo;
			int nRet = 0;
			switch (nSeqNo)
			{
				case 0: //상태 체크
					{
						nSeqNo = 100;
						_WorkStatus = WORK_STATUS.SENDING;
						m_RetryCount = 0;
					} break;
				case 100: //명령 전송
					{
						if (SendData(strCmd+"\n") == true)
						{
							nSeqNo = 200;
							m_TimeOut = DateTime.Now;
						}
						else
						{
							//알람
							Flush();
							nSeqNo = 0;
							nRet = -1;
							_ErrMsg = "Send Fail.";
							_WorkStatus = WORK_STATUS.ERROR;
						}
					} break;
				case 200: //ACK Check.
					{
						string strRcv = "";
						ACK_MODE _AckMode = RcvCheck(ref strRcv);
						if ((int)_AckMode > 0)
						{
							switch (_AckMode)
							{
								case ACK_MODE.ACK:
									{//원하는 데이터 받을때 까지..돈다..
										m_TimeOut = DateTime.Now;
										nSeqNo = 1000;
									} break;
								default:
									{
										m_TimeOut = DateTime.Now;
									} break;
							}
						}
						else if ((int)_AckMode < 0)
						{
							if (_AckMode == ACK_MODE.BEFORE_CMD_PROC)
							{//이미 명령 나가있는놈이 있어서, 방금보낸 명령이 안먹음...리트라이 해야한다.
								if (m_RetryCount < MaxRetryCount)
								{
									m_RetryCount += 1;
									nSeqNo = 100;
									m_TimeOut = DateTime.Now;
									LogTextOut(string.Format("{0}/Retry Count:{1})", strCmd, m_RetryCount));
								}
								else
								{
									nRet = nSeqNo * -1;
									nSeqNo = 0;
									LogTextOut(string.Format("{0}/RetryCount Over.", strCmd));
								}
							}
							else
							{
								_WorkStatus = WORK_STATUS.ERROR;
								nRet = nSeqNo * -1;
								nSeqNo = 0;
							}
						}
						else
						{
							if (GetElapseTime(m_TimeOut) > READ_END_DELAY)
							{
								_ErrMsg = "Receive Time Out.";
								_WorkStatus = WORK_STATUS.ERROR;
								nRet = nSeqNo * -1;
								nSeqNo = 0;
								Flush();
							}
						}
					} break;
				case 1000:
					{
						nRet = 1;
						nSeqNo = 0;
						_WorkStatus = WORK_STATUS.IDLE;
					}break;
				default:
					{
						_ErrMsg = "Seq No Error.";
						_WorkStatus = WORK_STATUS.ERROR;
						nSeqNo = 0;
						nRet = -1;
						Flush();
					} break;
			}
			m_nSeqNo = nSeqNo;
			return nRet;
		}
		private int Seq_CurVal_Command(string strCmd, ref string sRcvData)
		{
			int nSeqNo = m_nSeqNo;
			int nRet = 0;
			switch (nSeqNo)
			{
				case 0: //상태 체크
					{
						nSeqNo = 100;
						_WorkStatus = WORK_STATUS.READING;
						m_RetryCount = 0;
					} break;
				case 100: //명령 전송
					{
						if (SendData(strCmd+"\n") == true)
						{
							nSeqNo = 200;
							m_TimeOut = DateTime.Now;
						}
						else
						{
							//알람
							Flush();
							nSeqNo = 0;
							nRet = -1;
							_ErrMsg = "Send Fail.";
							_WorkStatus = WORK_STATUS.ERROR;
						}
					} break;
				case 200: //ACK Check.
					{
						ACK_MODE _AckMode = RcvCheck(ref sRcvData);
						if ((int)_AckMode > 0)
						{
							switch (_AckMode)
							{
								case ACK_MODE.CHECK_DATA:
									{//원하는 데이터 받을때 까지..돈다..
										m_TimeOut = DateTime.Now;
										nSeqNo = 1000;
									} break;
								default:
									{
										m_TimeOut = DateTime.Now;
									} break;
							}
						}
						else if ((int)_AckMode < 0)
						{
							if (_AckMode == ACK_MODE.BEFORE_CMD_PROC)
							{//이미 명령 나가있는놈이 있어서, 방금보낸 명령이 안먹음...리트라이 해야한다.
								if (m_RetryCount < MaxRetryCount)
								{
									m_RetryCount += 1;
									nSeqNo = 100;
									m_TimeOut = DateTime.Now;
									LogTextOut(string.Format("{0}/Retry Count:{1})", strCmd, m_RetryCount));
								}
								else
								{
									nRet = nSeqNo * -1;
									nSeqNo = 0;
									LogTextOut(string.Format("{0}/RetryCount Over.", strCmd));
								}
							}
							else
							{
								_WorkStatus = WORK_STATUS.ERROR;
								nRet = nSeqNo * -1;
								nSeqNo = 0;
							}
						}
						else
						{
							if (GetElapseTime(m_TimeOut) > READ_END_DELAY)
							{
								_ErrMsg = "Receive Time Out.";
								_WorkStatus = WORK_STATUS.ERROR;
								nRet = nSeqNo * -1;
								nSeqNo = 0;
								Flush();
							}
						}
					} break;
				case 1000:
					{
						nRet = 1;
						nSeqNo = 0;
						_WorkStatus = WORK_STATUS.IDLE;
					}break;
				default:
					{
						_ErrMsg = "Seq No error.";
						_WorkStatus = WORK_STATUS.ERROR;
						nSeqNo = 0;
						nRet = -1;
						Flush();
					} break;
			}
			m_nSeqNo = nSeqNo;
			return nRet;
		}
		private int Seq_Wait_Command(string strCmd, ACK_MODE _CheckMode)
		{
			int nSeqNo = m_nSeqNo;
			int nRet = 0;
			switch (nSeqNo)
			{
				case 0: //상태 체크
					{
						nSeqNo = 100;
						_WorkStatus = WORK_STATUS.ACK_CHECK;
						m_RetryCount = 0;
					} break;
				case 100: //명령 전송
					{
						if (SendData(strCmd+"\n") == true)
						{
							nSeqNo = 200;
							m_TimeOut = DateTime.Now;
						}
						else
						{
							//알람
							Flush();
							nSeqNo = 0;
							nRet = -1;
							_ErrMsg = "Send Fail.";
							_WorkStatus = WORK_STATUS.ERROR;
						}
					} break;
				case 200: //ACK Check.
					{
						string strRcv = "";
						ACK_MODE _AckMode = RcvCheck(ref strRcv);
						if ((int)_AckMode > 0)
						{
							if (_AckMode == _CheckMode)
							{
								//원하는 데이터 받을때 까지..돈다..
								m_TimeOut = DateTime.Now;
								nSeqNo = 1000;
							}
							else
							{
								m_TimeOut = DateTime.Now;
							}
						}
						else if ((int)_AckMode < 0)
						{
							if (_AckMode == ACK_MODE.BEFORE_CMD_PROC)
							{//이미 명령 나가있는놈이 있어서, 방금보낸 명령이 안먹음...리트라이 해야한다.
								if (m_RetryCount < MaxRetryCount)
								{
									m_RetryCount += 1;
									nSeqNo = 100;
									m_TimeOut = DateTime.Now;
									LogTextOut(string.Format("{0}/Retry Count:{1})", strCmd, m_RetryCount));
								}
								else
								{
									nRet = nSeqNo * -1;
									nSeqNo = 0;
									LogTextOut(string.Format("{0}/RetryCount Over.", strCmd));
								}
							}
							else
							{
								_WorkStatus = WORK_STATUS.ERROR;
								nRet = nSeqNo * -1;
								nSeqNo = 0;
							}
						}
						else
						{
                            if (strCmd == "OPEN" || strCmd == "CLOSE")
                            {
                                if (GetElapseTime(m_TimeOut) > READ_END_DELAY * 20)
                                {
                                    _ErrMsg = "Receive Time Out.";
                                    _WorkStatus = WORK_STATUS.ERROR;
                                    nRet = nSeqNo * -1;
                                    nSeqNo = 0;
                                    Flush();
                                }
                            }
                            else
                            {
                                if (GetElapseTime(m_TimeOut) > READ_END_DELAY)
                                {
                                    _ErrMsg = "Receive Time Out.";
                                    _WorkStatus = WORK_STATUS.ERROR;
                                    nRet = nSeqNo * -1;
                                    nSeqNo = 0;
                                    Flush();
                                }
                            }

						}
					} break;
				case 1000:
					{
						nRet = 1;
						nSeqNo = 0;
						_WorkStatus = WORK_STATUS.IDLE;
					}break;
				default:
					{
						_ErrMsg = "Seq No Error.";
						_WorkStatus = WORK_STATUS.ERROR;
						nSeqNo = 0;
						nRet = -1;
						Flush();
					} break;
			}
			m_nSeqNo = nSeqNo;
			return nRet;
		}
        public int Seq_Wait_Command_Test(string strCmd, ACK_MODE _CheckMode)
        {
            int nSeqNo = m_nSeqNo;
            int nRet = 0;
            switch (nSeqNo)
            {
                case 0: //상태 체크
                    {
                        nSeqNo = 100;
                        _WorkStatus = WORK_STATUS.ACK_CHECK;
                        m_RetryCount = 0;
                    }
                    break;
                case 100: //명령 전송
                    {
                        if (SendData(strCmd + "\n") == true)
                        {
                            nSeqNo = 200;
                            m_TimeOut = DateTime.Now;
                        }
                        else
                        {
                            //알람
                            Flush();
                            nSeqNo = 0;
                            nRet = -1;
                            _ErrMsg = "Send Fail.";
                            _WorkStatus = WORK_STATUS.ERROR;
                        }
                    }
                    break;
                case 200: //ACK Check.
                    {
                        string strRcv = "";
                        ACK_MODE _AckMode = RcvCheck(ref strRcv);
                        if ((int)_AckMode > 0)
                        {
                            if (_AckMode == _CheckMode)
                            {
                                //원하는 데이터 받을때 까지..돈다..
                                m_TimeOut = DateTime.Now;
                                nSeqNo = 1000;
                            }
                            else
                            {
                                m_TimeOut = DateTime.Now;
                            }
                        }
                        else if ((int)_AckMode < 0)
                        {
                            if (_AckMode == ACK_MODE.BEFORE_CMD_PROC)
                            {//이미 명령 나가있는놈이 있어서, 방금보낸 명령이 안먹음...리트라이 해야한다.
                                if (m_RetryCount < MaxRetryCount)
                                {
                                    m_RetryCount += 1;
                                    nSeqNo = 100;
                                    m_TimeOut = DateTime.Now;
                                    LogTextOut(string.Format("{0}/Retry Count:{1})", strCmd, m_RetryCount));
                                }
                                else
                                {
                                    nRet = nSeqNo * -1;
                                    nSeqNo = 0;
                                    LogTextOut(string.Format("{0}/RetryCount Over.", strCmd));
                                }
                            }
                            else
                            {
                                _WorkStatus = WORK_STATUS.ERROR;
                                nRet = nSeqNo * -1;
                                nSeqNo = 0;
                            }
                        }
                        else
                        {
                            if (strCmd == "OPEN" || strCmd == "CLOSE")
                            {
                                if (GetElapseTime(m_TimeOut) > READ_END_DELAY * 20)
                                {
                                    _ErrMsg = "Receive Time Out.";
                                    _WorkStatus = WORK_STATUS.ERROR;
                                    nRet = nSeqNo * -1;
                                    nSeqNo = 0;
                                    Flush();
                                }
                            }
                            else
                            {
                                if (GetElapseTime(m_TimeOut) > READ_END_DELAY)
                                {
                                    _ErrMsg = "Receive Time Out.";
                                    _WorkStatus = WORK_STATUS.ERROR;
                                    nRet = nSeqNo * -1;
                                    nSeqNo = 0;
                                    Flush();
                                }
                            }

                        }
                    }
                    break;
                case 1000:
                    {
                        nRet = 1;
                        nSeqNo = 0;
                        _WorkStatus = WORK_STATUS.IDLE;
                    }
                    break;
                default:
                    {
                        _ErrMsg = "Seq No Error.";
                        _WorkStatus = WORK_STATUS.ERROR;
                        nSeqNo = 0;
                        nRet = -1;
                        Flush();
                    }
                    break;
            }
            m_nSeqNo = nSeqNo;
            return nRet;
        }

        #region 명령어
        public async void Cmd_ReadVersion()
		{//버전 읽기
			string str = "";
			while (Seq_CurVal_Command("GETVER", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			LPM_Data.Version = str;
		}
		public async void Cmd_ReadErrorCode()
		{//에러코드 읽기
			string str = "";
			while (Seq_CurVal_Command("ECODE", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_ReadMaintMode()
		{
			string str = "";
			while (Seq_CurVal_Command("MAINT_MODE", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			LPM_Data.MAINT_MODE = str.Contains("ON");
		}
		public async void Cmd_ReadSenPlace()
		{
			string str = "";
			while (Seq_CurVal_Command("SEN_PLACE", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			LPM_Data.SEN_PLACE = str.Contains("ON");
		}
		public async void Cmd_ReadSenVaccum()
		{
			string str = "";
			while (Seq_CurVal_Command("SEN_VACUUM", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			LPM_Data.SEN_VACUUM = str.Contains("ON");
		}
		public async void Cmd_ReadMdoorOpen()
		{
			string str = "";
			while (Seq_CurVal_Command("MDOR_OPEN", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			LPM_Data.MDOR_OPEN = str.Contains("ON");
		}
		public async void Cmd_ReadMliftDown()
		{
			string str = "";
			while (Seq_CurVal_Command("MLIFT_DN", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			LPM_Data.MLIFT_DN = str.Contains("ON");
		}
		public async void Cmd_ReadMfinger()
		{
			string str = "";
			while (Seq_CurVal_Command("MFINGER", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			LPM_Data.MFINGER = str.Contains("ON");
		}
		public async void Cmd_ReadPodLock()
		{
			string str = "";
			while (Seq_CurVal_Command("POD_LOCK", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			LPM_Data.POD_LOCK = str.Contains("ON");
		}
		public async void Cmd_ReadCfgIdsw()
		{
			string str = "";
			while (Seq_CurVal_Command("CFG_IDSW", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			LPM_Data.CFG_IDSW = str.Contains("ON");
		}
		public async void Cmd_ReadCheckDoor()
		{
			string str = "";
			while (Seq_CurVal_Command("CHK_DOR", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			LPM_Data.CHK_DOR = str.Contains("ON");
		}

		public async void Cmd_Send_LedState_Load(LED_STATE _State)
		{
			string strCmd = "LED_STATE LOAD " + _State.ToString();
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_LedState_UnLoad(LED_STATE _State)
		{
			string strCmd = "LED_STATE UNLOAD " + _State.ToString();
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_LedState_Reserve(LED_STATE _State)
		{
			string strCmd = "LED_STATE RESERVE " + _State.ToString();
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_ReserveOnOff(bool bOn)
		{
			string strCmd = string.Format("RESERVE {0}", bOn ? "ON" : "OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_AmpOnOff(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "AMPON" : "AMPOFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_Home()
		{
			string strCmd = "HOM";
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_Open()
		{//Load ( clamp 이후부터 door open 까지 )
			string strCmd = "OPEN";
            //while (Seq_SendCommand(strCmd) == 0)
            //{
            //    await Task.Delay(Loop_Delay);
            //}

            while (Seq_Wait_Command(strCmd, ACK_MODE.MAPPING_DATA) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
		}
		public async void Cmd_Send_Close()
		{// UnLoad ( unclamp 전까지 수행 )
			string strCmd = "CLOSE";
            //while (Seq_SendCommand(strCmd) == 0)
            //{
            //    await Task.Delay(Loop_Delay);
            //}

            while (Seq_Wait_Command(strCmd, ACK_MODE.MAPPING_DATA) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
		}
		public async void Cmd_Send_Dock(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "DOCK" : "UNDOCK");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_ScanUp(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "SCAN UP" : "SCAN DN");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_Abort()
		{
			string strCmd = "ABORT";
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_AMHS(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "AMHS ON" : "AMHS OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_AutoMode(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "AUTO_MODE ON" : "AUTO_MODE OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}

        public async void Cmd_Send_MappingMode(bool bOn)
        {
            string strCmd = string.Format("{0}", bOn ? "CFG_MAP ON" : "CFG_MAP OFF");
            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }

		public async void Cmd_Send_Reset()
		{
			string strCmd = "RESET";
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_SOpen()
		{
			string strCmd = "SOPEN";
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_SClose()
		{
			string strCmd = "SCLOSE";
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_MDOR_OPN(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "MDOR_OPN ON" : "MDOR_OPN OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_MLIFT_DN(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "MLIFT_DN ON" : "MLIFT_DN OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_MFINGER(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "MFINGER ON" : "MFINGER OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_POD_LOCK(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "POD_LOCK ON" : "POD_LOCK OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_MAINT_MODE(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "MAINT_MODE ON" : "MAINT_MODE OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_SEN_PLACE(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "SEN_PLACE ON" : "SEN_PLACE OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_SEN_VACUUM(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "SEN_VACUUM ON" : "SEN_VACUUM OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_CFG_IDSW(bool bOn)
		{
			string strCmd = string.Format("{0}", bOn ? "CFG_IDSW ON" : "CFG_IDSW OFF");
			while (Seq_SendCommand(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}

		public async void Cmd_Send_Status()
		{
			string strCmd = "STATUS";
			while (Seq_Wait_Command(strCmd, ACK_MODE.STATUS_DATA) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_GetMapping()
		{
			string strCmd = "GETMAP";
			while (Seq_Wait_Command(strCmd, ACK_MODE.MAPPING_DATA) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		#endregion
	}

	public class Nano300_Data
	{
		public Nano300_Data()
		{
		}

		//이벤트 데이터 (수시로 오는것)
		public byte[] EventData = new byte[32];

		//상태 데이터 (STATUS 명령 보내고 받는 것)
		public byte[] StatusData = new byte[32];

		//Mapping 정보
		public byte[] Mapping_Slot = new byte[32];
		public byte[] Mapping_Cross = new byte[32];
		public byte[] Mapping_Double = new byte[32];

        //Mapping Flag 추가 
        public bool bRecvMapping = false;

		//기타 데이터
		public string Version = "";

		public bool MAINT_MODE = false;
		public bool SEN_PLACE = false;
		public bool SEN_VACUUM = false;
		public bool MDOR_OPEN = false;
		public bool MLIFT_DN = false;
		public bool MFINGER = false;
		public bool POD_LOCK = false;
		public bool CFG_IDSW = false;
		public bool CHK_DOR = false;

	}

	public class MappingEventArgs : EventArgs
	{
		public byte[] Mapping_Slot = new byte[32];
		public byte[] Mapping_Cross = new byte[32];
		public byte[] Mapping_Double = new byte[32];
		public MappingEventArgs(byte[] _Slot, byte[] _Closs, byte[] _Double)
		{
			Array.Copy(_Slot, 0, Mapping_Slot, 0, Mapping_Slot.Length);
			Array.Copy(_Closs, 0, Mapping_Cross, 0, Mapping_Cross.Length);
			Array.Copy(_Double, 0, Mapping_Double, 0, Mapping_Double.Length);
		}

		
	}
}
