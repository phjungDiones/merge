using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CJ_Controls.Communication.PA300C
{
	public enum RCV_CHAR
	{
		ACK = ASCII.ACK,	//[-]
		CR = ASCII.CR,		//[\r]
		LF = ASCII.LF,		//[\n]
		DATA = 0x23,		//[#]
		COMPLETE = 0x3E,	//[>]
	}
	public enum ACK_MODE
	{
		NONE = 0,
		ACK,
		DATA,
		STATUS_DATA,
		COMPLETE,
		ERROR = -1,
		NAK = -2,
		BEFORE_CMD_PROC = -3,
	}
	public enum WORK_STATUS
	{
		IDLE = 0,
		SENDING,
		MOVING,
		ERROR,
		READING,
	}
	public enum WAFER_STATUS_AL
	{
		NO_WAFER = 0,								//No Wafer
		WAFER_EXIST = 1,							//Wafer Exist [Wafer was detected by vacuum sensor & CCD sensor]
		WAFER_DETECTED_VAC_AND_CCD_COVERED=11,		//Wafer was detected by vacuum sensor & CCD sensor was covered by wafer
		WAFER_DETECTED_VAC_AND_CCD_CANT_DETECT=12,	//Wafer was detected by vacuum sensor & CCD sensor cannot detect wafer
		WAFER_DETECTED_CCD_AND_VAC_CANT_DETECT=13,	//Wafer was detected by vacuum sensor & CCD sensor cannot detect wafer
	}
	public enum CHUCK_POS
	{
		NONE = 0,
		DOWN_POS,
		BELOW_ROD_POS,
		ABOVE_ROD_POS,
		UP_POS,
		MOVING_STS,
		NEED_HOME,
	}
	public enum ALIGNER_STATUS
	{
		NONE = 0,
		READY,
		BUSY,
		ERROR,
		NEED_HOME,
	}
	public enum ONOFF
	{
		OFF = 0,
		ON = 1,
	}
	public class Aligner_PA300C
	{
        public delegate void procAddMsgEvent(string strAddMsg);
        public event procAddMsgEvent procMsgEvent;

		#region Log 보내기
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public event MessageEventHandler MessageEvent;

		private void LogTextOut(string message)
		{
            if (procMsgEvent != null)
			{
				string strInfo = string.Format("Aligner_PA300C({0})=>", m_SerialPort.PortName);
				//procMsgEvent(this, new MessageEventArgs(strInfo+message));

//                 if (procMsgEvent != null)
//                     procMsgEvent(strInfo + message);
			}
		}
		#endregion

		Thread m_Thread_Read = null;
		public PA300C_Data Pa300C_Data = new PA300C_Data();



		public Aligner_PA300C()
		{
			m_Thread_Read = new Thread(new ThreadStart(SerialPort_DataReceived));
			m_Thread_Read.IsBackground = true;
			m_Thread_Read.Start();
		}

		private readonly int Loop_Delay = 100;
		private readonly int READ_END_DELAY = 2000;
		private readonly int MOVING_COMPLETE_DELAY = 5000;
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
					//if (WorkStatus == WORK_STATUS.IDLE)
					if(m_nSeqNo == 0)
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
			if (IsReceived("\n>") == true)
			{
				byte[] bRcv = GetReceiveDataByteEx_EndCut();
				sRcvData = GetReceiveDataEx_EndCut();
				LogTextOut(string.Format("RecvData:{0}", sRcvData));
				if (sRcvData.Contains("#ST") == true)
				{
					FlushEx_EndCheckCut();
					_AckMode = ACK_MODE.ACK;
				}
				else if (sRcvData.Contains("#OK") == true)
				{
					FlushEx_EndCheckCut();
					_WorkStatus = WORK_STATUS.IDLE;
					_AckMode = ACK_MODE.COMPLETE;
				}
				else if (sRcvData.Contains("#BU") == true)
				{
					FlushEx_EndCheckCut();
					_AckMode = ACK_MODE.BEFORE_CMD_PROC;
				}
				else if (sRcvData.Contains("#??") == true)
				{
					FlushEx_EndCheckCut();
					_WorkStatus = WORK_STATUS.IDLE;
					_AckMode = ACK_MODE.NAK;
				}
				else if (sRcvData.Contains("#ER") == true)
				{
					FlushEx_EndCheckCut();
					_WorkStatus = WORK_STATUS.ERROR;
					_ErrMsg = sRcvData;
					_AckMode = ACK_MODE.ERROR;
				}
				else
				{//위의것이 아니면, 거의 체크 데이터로 본다. 그냥..
					FlushEx_EndCheckCut();
					_AckMode = ACK_MODE.DATA;
				}
			}

            //수정
            BeforeAck = _AckMode;
			return _AckMode;
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
		{//cjinnnn: 연속으로 붙어오는 데이터 처리를 위한 함수. \r\n 단위로 자른다.
			if (IsOpen() == false)
				return;
			try
			{
				int nIndex = IsReceived_Ex((byte)RCV_CHAR.CR, (byte)RCV_CHAR.LF);
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
						Array.Copy(m_ReceiveBuffer, nIndex + 2, TempBuf, 0, m_ReadCount - (nIndex + 2));
						Array.Clear(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length);
						Array.Copy(TempBuf, 0, m_ReceiveBuffer, 0, m_ReceiveBuffer.Length);
						m_ReadCount -= (nIndex + 2);
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
			for (int i = 0; i < m_ReadCount-1; i++)
			{
				if (m_ReceiveBuffer[i] == strCh1
					&& m_ReceiveBuffer[i+1] == strCh2)
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
			int nIndex = IsReceived_Ex((byte)RCV_CHAR.LF, (byte)RCV_CHAR.COMPLETE);
			return Encoding.ASCII.GetString(m_ReceiveBuffer, 0, nIndex+2);
		}
		private byte[] GetReceiveDataByte()
		{
			return m_ReceiveBuffer;
		}
		private byte[] GetReceiveDataByteEx_EndCut()
		{
			byte[] _Rtn;
			int nIndex = IsReceived_Ex((byte)RCV_CHAR.LF, (byte)RCV_CHAR.COMPLETE);
			if (nIndex <= 0)
				_Rtn = m_ReceiveBuffer;
			else
			{
				byte[] TempBuf = new byte[nIndex+2];
				Array.Copy(m_ReceiveBuffer, 0, TempBuf, 0, TempBuf.Length);
				_Rtn = TempBuf;
			}

			return _Rtn;
		}

        public ACK_MODE BeforeAck = ACK_MODE.NONE;

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

		private DateTime m_TimeOut = DateTime.MaxValue;
		private int m_nSeqNo = 0;
		private int Seq_SendCommand(string strCmd)
		{
			int nSeqNo = m_nSeqNo;
			int nRet = 0;
            char STX = '#';

			switch (nSeqNo)
			{
				case 0: //상태 체크
					{
						nSeqNo = 100;
						_WorkStatus = WORK_STATUS.SENDING;
					}break;
				case 100: //명령 전송
					{
                        if (SendData(STX + strCmd + "\r") == true)
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
						}
					} break;
				case 200: //ACK Check.
					{
						string strRcvData = "";
						ACK_MODE _AckMode = RcvCheck(ref strRcvData);
						if ((int)_AckMode > 0)
						{
							_WorkStatus = WORK_STATUS.IDLE;
							nRet = 1;
							nSeqNo = 0;
						}
						else if ((int)_AckMode < 0)
						{
							nRet = nSeqNo*-1;
							nSeqNo = 0;
						}
						else
						{
							if (GetElapseTime(m_TimeOut) > READ_END_DELAY)
							{
								nRet = nSeqNo * -1;
								nSeqNo = 0;
								Flush();
							}
						}
					} break;
				default:
					{
						nSeqNo = 0;
						nRet = -1;
						Flush();
					} break;
			}
			m_nSeqNo = nSeqNo;
			return nRet;
		}
		private int Seq_SendCommand_WaitComplete(string strCmd)
		{
			int nSeqNo = m_nSeqNo;
			int nRet = 0;
            char STX = '#';

			switch (nSeqNo)
			{
				case 0: //상태 체크
					{
						nSeqNo = 100;
						_WorkStatus = WORK_STATUS.MOVING;
					}break;
				case 100: //명령 전송
					{
                        if (SendData(STX + strCmd + "\r") == true)
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
						}
					} break;
				case 200: //ACK Check.
					{
						string strRcvData = "";
						ACK_MODE _AckMode = RcvCheck(ref strRcvData);
						if ((int)_AckMode > 0)
						{
							m_TimeOut = DateTime.Now;
							if (_AckMode == ACK_MODE.COMPLETE)
							{
								_WorkStatus = WORK_STATUS.IDLE;
								nRet = 1;
								nSeqNo = 0;
							}
						}
						else if ((int)_AckMode < 0)
						{
							nRet = nSeqNo*-1;
							nSeqNo = 0;
						}
						else
						{
                            //if (GetElapseTime(m_TimeOut) > MOVING_COMPLETE_DELAY)
                            //{
                            //    nRet = nSeqNo * -1;
                            //    nSeqNo = 0;
                            //    Flush();
                            //}
						}
					} break;
				default:
					{
						nSeqNo = 0;
						nRet = -1;
						Flush();
					} break;
			}
			m_nSeqNo = nSeqNo;
			return nRet;
		}

		private string m_strBeforeRcvData = "";
		private int Seq_ReadCommand(string strCmd, ref string sRcvData)
		{
			int nSeqNo = m_nSeqNo;
			int nRet = 0;
            char STX = '#';

			switch (nSeqNo)
			{
				case 0: //상태 체크
					{

						nSeqNo = 100;
						_WorkStatus = WORK_STATUS.READING;
					} break;
				case 100: //명령 전송
					{
                        if (SendData(STX + strCmd + "\r") == true)
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
						}
					} break;
				case 200: //ACK Check.
					{
						ACK_MODE _AckMode = RcvCheck(ref sRcvData);
						if ((int)_AckMode > 0)
						{
							if(_AckMode == ACK_MODE.DATA)
							{
								m_strBeforeRcvData = sRcvData;
								_WorkStatus = WORK_STATUS.IDLE;
								nRet = 1;
								nSeqNo = 0;
							}
						}
						else if ((int)_AckMode < 0)
						{
							nRet = nSeqNo * -1;
							nSeqNo = 0;
						}
						else
						{
							if (GetElapseTime(m_TimeOut) > READ_END_DELAY)
							{
								nRet = nSeqNo * -1;
								nSeqNo = 0;
								Flush();
							}
						}
					} break;
				default:
					{
						nSeqNo = 0;
						nRet = -1;
						Flush();
					} break;
			}
			m_nSeqNo = nSeqNo;
			return nRet;
		}

		public string Replace_Char(string str)
		{
			str = str.Replace("#","");
			str = str.Replace("\r","");
			str = str.Replace("\n","");
			str = str.Replace(">","");
			str = str.Replace(" ","");
			return str;
		}
		#region 데이터 읽기 명령어
		public async void Cmd_Read_ANOF()
		{//This command controls the Flat/Notch alignment angle offset.
			string str = "";
			while (Seq_ReadCommand("ANOF", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			str = Replace_Char(str);

			float fVal = 0;
			float.TryParse(str, out fVal);
			Pa300C_Data.CurAngleOffset = fVal;
		}
		public async void Cmd_Read_STAL()
		{//starts wafer alignment procedure.
			string str = "";
			while (Seq_ReadCommand("STAL", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
            if (str.Length < 1) return;

			str = Replace_Char(str);

			float fVal = 0;
			float.TryParse(str, out fVal);
			Pa300C_Data.CurAngleParam = fVal;
		}
		public async void Cmd_Read_CHKW()
		{//This command checks wafer existence and returns the result.
			string str = "";
			while (Seq_ReadCommand("CHKW", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

            if (str.Length < 1) return;

			str = Replace_Char(str);

			int nVal = 0;
			int.TryParse(str, out nVal);
            Pa300C_Data.WaferStatus = (WAFER_STATUS_AL)nVal;
		}
		public async void Cmd_Read_CPOS()
		{//This command returns the current chuck position.
			string str = "";
			while (Seq_ReadCommand("CPOS", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
            if (str.Length < 1) return;

			if (str.Contains("#DN") == true)
				Pa300C_Data.ChuckPosition = CHUCK_POS.DOWN_POS;
			else if (str.Contains("#BE") == true)
				Pa300C_Data.ChuckPosition = CHUCK_POS.BELOW_ROD_POS;
			else if (str.Contains("#AB") == true)
				Pa300C_Data.ChuckPosition = CHUCK_POS.ABOVE_ROD_POS;
			else if (str.Contains("#UP") == true)
				Pa300C_Data.ChuckPosition = CHUCK_POS.UP_POS;
			else if (str.Contains("#MO") == true)
				Pa300C_Data.ChuckPosition = CHUCK_POS.MOVING_STS;
			else// if (str.Contains("#NA") == true)
				Pa300C_Data.ChuckPosition = CHUCK_POS.NEED_HOME;
		}
		public async void Cmd_Read_CVER()
		{//This command returns the controller firmware version.
			string str = "";
			while (Seq_ReadCommand("CVER", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

            if (str.Length < 1) return;

			str = Replace_Char(str);

			Pa300C_Data.Version = str;
		}
		public async void Cmd_Read_ERST()
		{//aligner error status.
			string str = "";
			while (Seq_ReadCommand("ERST", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
            if (str.Length < 1) return;

			str = Replace_Char(str);
			int nVal = 0;
			int.TryParse(str, out nVal);
			Pa300C_Data.ErrorCode = nVal;
		}
		public async void Cmd_Read_STAT()
		{//This command returns current system state.
			string str = "";
			while (Seq_ReadCommand("STAT", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

            if (str.Length < 1) return;

			if (str.Contains("#00") == true)
				Pa300C_Data.AlignerStatus = ALIGNER_STATUS.READY;
			else if (str.Contains("#01") == true)
				Pa300C_Data.AlignerStatus = ALIGNER_STATUS.BUSY;
			else if (str.Contains("#08") == true)
				Pa300C_Data.AlignerStatus = ALIGNER_STATUS.ERROR;   
			else if (str.Contains("#80") == true)
				Pa300C_Data.AlignerStatus = ALIGNER_STATUS.NEED_HOME;
			else
				Pa300C_Data.AlignerStatus = ALIGNER_STATUS.NONE;
		}
		public async void Cmd_Read_VACH()
		{//current vacuum sensor status.
			string str = "";
			while (Seq_ReadCommand("VACH", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			if (str.Contains("#01") == true)
				Pa300C_Data.Vaccum_Sens = ONOFF.ON;
			else
				Pa300C_Data.Vaccum_Sens = ONOFF.OFF;
		}
		public async void Cmd_Read_WACI()
		{//This command finds the wafer center offset. Rotates wafer 360 degrees to find center offset, and return center offset.
			string str = "";
			while (Seq_ReadCommand("WACI", ref str) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

			str = Replace_Char(str);

			List<string> result = new List<string>();
			result.AddRange(str.Split(','));

			if (result.Count == 2)
			{
				float fVal = 0;
				float.TryParse(result[0], out fVal);
				Pa300C_Data.CurCoordinate_X = fVal;

				fVal = 0;
				float.TryParse(result[1], out fVal);
				Pa300C_Data.CurCoordinate_Y = fVal;
			}
		}
		#endregion

		#region 데이터 쓰기 명령어
		public async void Cmd_Send_ANOF(float fVal)
		{//This command controls the Flat/Notch alignment angle offset.
			string strCmd = string.Format("ANOF {0:0.00}", fVal);
			while (Seq_SendCommand_WaitComplete(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_ROTW(float fVal)
		{//This command rotates the wafer to the required angle
			string strCmd = string.Format("ROTW {0:0.00}", fVal);
			while (Seq_SendCommand_WaitComplete(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_STAL(float fVal)
		{//This command rotates the wafer to the required angle
			string strCmd = string.Format("STAL {0:0.00}", fVal);
			while (Seq_SendCommand_WaitComplete(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_CKDN()
		{//This command moves the chuck to DOWN position.
			string strCmd = string.Format("CKDN");
			while (Seq_SendCommand_WaitComplete(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_CKUP()
		{//This command moves the chuck to UP position.
			string strCmd = string.Format("CKUP");
			while (Seq_SendCommand_WaitComplete(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}
		}
		public async void Cmd_Send_ECLR()
		{//clears aligner error status register.
			string strCmd = string.Format("ECLR");
			while (Seq_SendCommand_WaitComplete(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

		}
		public async void Cmd_Send_HOME()
		{//initializes aligner: Initialize All Axis and return home Position.
			string strCmd = string.Format("HOME");
			while (Seq_SendCommand_WaitComplete(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

		}
		public async void Cmd_Send_RCAL()
		{//executes system calibration.
			string strCmd = string.Format("RCAL");
			while (Seq_SendCommand_WaitComplete(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

		}
		public async void Cmd_Send_REST()
		{//resets the controller. The action is same as turning the power off and on.
			string strCmd = string.Format("REST");
			while (Seq_SendCommand(strCmd) == 0) //얘는 OK 안온다.
			{
				await Task.Delay(Loop_Delay);
			}

		}
		public async void Cmd_Send_VAOF()
		{//turns off a vacuum valve.
			string strCmd = string.Format("VAOF");
			while (Seq_SendCommand_WaitComplete(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

		}
		public async void Cmd_Send_VAON()
		{//turns on a vacuum valve.
			string strCmd = string.Format("VAON");
			while (Seq_SendCommand_WaitComplete(strCmd) == 0)
			{
				await Task.Delay(Loop_Delay);
			}

		}
		#endregion
	}

	public class PA300C_Data
	{
		public PA300C_Data()
		{
		}

		public string Version = "";
		public int ErrorCode = 0;
		public float CurAngleOffset = 0;
		public float CurAngleParam = 0;
		public float CurCoordinate_X = 0;
		public float CurCoordinate_Y = 0;
        public WAFER_STATUS_AL WaferStatus = WAFER_STATUS_AL.NO_WAFER;
		public CHUCK_POS ChuckPosition = CHUCK_POS.NONE;
		public ALIGNER_STATUS AlignerStatus = ALIGNER_STATUS.NONE;
		public ONOFF Vaccum_Sens = ONOFF.OFF;
	}
}
