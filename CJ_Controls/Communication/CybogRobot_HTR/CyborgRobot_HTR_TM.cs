using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CJ_Controls.Communication.CybogRobot_HTR
{
 
    public class CyborgRobot_HTR_TM : Component
    {
        #region Log 보내기
        public delegate void MessageEventHandler(object sender, MessageEventArgs args);
        public event MessageEventHandler MessageEvent;
        private void LogTextOut(string message)
        {
            if (MessageEvent != null)
            {
                string strInfo = string.Format("CyborgRobot_HTR({0})=>", m_SerialPort.PortName);
                MessageEvent(this, new MessageEventArgs(strInfo + message));
            }
        }
        #endregion

        Thread m_Thread_Read = null;

        public CyborgRobot_Data_TM RobotData = new CyborgRobot_Data_TM();
        public CyborgRobot_HTR_TM()
        {
            //Cjinnn: 연속 데이터 DataParsing 테스트 용.
            //string strText = string.Format("{0}GETSPEED\r\n", (char)RCV_CHAR.ACK);
            //strText += string.Format("*100\r\n");
            //strText += string.Format(">GETSPEED\r\n");
            //Array.Copy(Encoding.Default.GetBytes(strText), 0, m_ReceiveBuffer, 0, strText.Length);
            //m_ReadCount = strText.Length;

            m_Thread_Read = new Thread(new ThreadStart(SerialPort_DataReceived));
            m_Thread_Read.IsBackground = true;
            m_Thread_Read.Start();
        }

        private readonly int Loop_Delay = 100;
        //private readonly int READ_END_DELAY = 2000;
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
                    if (m_nSeqNo == 0 || m_nReadSeqNo == 0)
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
            if (IsReceived("\r\n") == true)
            {
                byte[] bRcv = GetReceiveDataByte();
                sRcvData = GetReceiveDataEx_EndCut();
                LogTextOut(string.Format("RecvData:{0}", sRcvData));
                if (bRcv[0] == (byte)RCV_CHAR.ACK)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.ACK;
                }
                else if (bRcv[0] == (byte)RCV_CHAR.DATA)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                }
                else if (bRcv[0] == (byte)RCV_CHAR.COMPLETE)
                {
                    Flush();
                    _WorkStatus = WORK_STATUS.IDLE;
                    _AckMode = ACK_MODE.COMPLETE;
                }
                else if (sRcvData.Contains("ERR") == true)
                {
                    Flush();
                    _WorkStatus = WORK_STATUS.ERROR;
                    _ErrMsg = sRcvData;
                    Global.GlobalFunction.Instance.SetErr(sRcvData, Global.GlobalDefine.Eidentify_error.CyborgRobot_HTR);
                    _AckMode = ACK_MODE.ERROR;
                }
                else if (sRcvData.Contains("WAR") == true)
                {
                    Flush();
                    _WorkStatus = WORK_STATUS.WARING;
                    _WarMsg = sRcvData;
                    Global.GlobalFunction.Instance.SetErr(sRcvData, Global.GlobalDefine.Eidentify_error.CyborgRobot_HTR);
                    _AckMode = ACK_MODE.WARNING;
                }
            }
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

        public bool SendData_test(string str)
        {
            try
            {
                if (m_SerialPort.IsOpen.Equals(true))
                {
                    m_SerialPort.Write(str+"\r\n");
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
            int nIndex = IsReceived_Ex((byte)RCV_CHAR.CR, (byte)RCV_CHAR.LF);
            return Encoding.ASCII.GetString(m_ReceiveBuffer, 0, nIndex + 2);
        }
        private byte[] GetReceiveDataByte()
        {
            return m_ReceiveBuffer;
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

        public void ResetCmd()
        {
            m_nSeqNo = 0;
        }

        private DateTime m_TimeOut = DateTime.MaxValue;
        private int m_nSeqNo = 0;
        private int m_nReadSeqNo = 0;
        private int Seq_SendCommand(string strCmd)
        {
            int nSeqNo = m_nSeqNo;
            int nRet = 0;
            switch (nSeqNo)
            {
                case 0: //상태 체크
                    {
                        nSeqNo = 100;
                        _WorkStatus = WORK_STATUS.MOVING;
                    } break;
                case 100: //명령 전송
                    {
                        if (SendData(strCmd + "\r\n") == true)
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
                            nRet = 1;
                            nSeqNo = 0;
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

        private object thislock = new object();

        private string m_strBeforeRcvData = "";
        private int Seq_ReadCommand(string strCmd, ref string sRcvData)
        {
            int nSeqNo = m_nReadSeqNo;
            int nRet = 0;
            switch (nSeqNo)
            {
                case 0: //상태 체크
                    {
                        nSeqNo = 100;
                        _WorkStatus = WORK_STATUS.READING;

                        //Console.WriteLine("Send 1->" + strCmd);
                    } break;
                case 100: //명령 전송
                    {
                        if (SendData(strCmd + "\r\n") == true)
                        {
                            //Console.WriteLine("Send 2->" + strCmd);
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
                            switch (_AckMode)
                            {
                                case ACK_MODE.ACK:
                                    {//Ack 받고.
                                        m_TimeOut = DateTime.Now;
                                        //Console.WriteLine("Ack->" + sRcvData);
                                    } break;
                                case ACK_MODE.DATA:
                                    {//데이터 받고..
                                        m_TimeOut = DateTime.Now;
                                        sRcvData = sRcvData.Replace("\r\n", "");
                                        sRcvData = sRcvData.Replace("*", "");
                                        sRcvData = sRcvData.Replace(" ", "");
                                        m_strBeforeRcvData = sRcvData;

                                        //Console.WriteLine("Data->" + sRcvData);
                                    } break;
                                case ACK_MODE.COMPLETE:
                                    {//데이터 받는거 완료 되었다..진짜끝임.
                                        m_TimeOut = DateTime.Now;
                                        sRcvData = m_strBeforeRcvData;

                                        nRet = 1;
                                        nSeqNo = 0;

                                        //Console.WriteLine("Rcv Compl->" + strCmd);
                                    } break;
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
                        Console.WriteLine("Error");
                        nSeqNo = 0;
                        nRet = -1;
                        Flush();
                    } break;
            }

            m_nReadSeqNo = nSeqNo;
            return nRet;
        }
        public int Seq_ReadCommand_Test(string strCmd, ref string sRcvData)
        {
            int nSeqNo = m_nReadSeqNo;
            int nRet = 0;
            switch (nSeqNo)
            {
                case 0: //상태 체크
                    {
                        nSeqNo = 100;
                        _WorkStatus = WORK_STATUS.READING;

                        //Console.WriteLine("Send 1->" + strCmd);
                    }
                    break;
                case 100: //명령 전송
                    {
                        if (SendData(strCmd + "\r\n") == true)
                        {
                            //Console.WriteLine("Send 2->" + strCmd);
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
                    }
                    break;
                case 200: //ACK Check.
                    {
                        ACK_MODE _AckMode = RcvCheck(ref sRcvData);
                        if ((int)_AckMode > 0)
                        {
                            switch (_AckMode)
                            {
                                case ACK_MODE.ACK:
                                    {//Ack 받고.
                                        m_TimeOut = DateTime.Now;
                                        //Console.WriteLine("Ack->" + sRcvData);
                                    }
                                    break;
                                case ACK_MODE.DATA:
                                    {//데이터 받고..
                                        m_TimeOut = DateTime.Now;
                                        sRcvData = sRcvData.Replace("\r\n", "");
                                        sRcvData = sRcvData.Replace("*", "");
                                        sRcvData = sRcvData.Replace(" ", "");
                                        m_strBeforeRcvData = sRcvData;

                                        //Console.WriteLine("Data->" + sRcvData);
                                    }
                                    break;
                                case ACK_MODE.COMPLETE:
                                    {//데이터 받는거 완료 되었다..진짜끝임.
                                        m_TimeOut = DateTime.Now;
                                        sRcvData = m_strBeforeRcvData;

                                        nRet = 1;
                                        nSeqNo = 0;

                                        //Console.WriteLine("Rcv Compl->" + strCmd);
                                    }
                                    break;
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
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Error");
                        nSeqNo = 0;
                        nRet = -1;
                        Flush();
                    }
                    break;
            }

            m_nReadSeqNo = nSeqNo;
            return nRet;
        }

        #region 데이터 읽기 명령어
        public async void Cmd_ReadSpeed()
        {// 로봇의 현재 System Speed를 읽어오는 명령

            string str = "";
            while (Seq_ReadCommand("GETSPEED", ref str) == 0)
            {
                await Task.Delay(Loop_Delay);
            }

            int nVal = 0;
            int.TryParse(str, out nVal);
            RobotData.Read_Speed = nVal;
        }
        public async void Cmd_ReadStepSpeed()
        {// 로봇의 현재 Profile Speed를 읽어오는 명령
            string str = "";
            while (Seq_ReadCommand("GETSTEPSPEED", ref str) == 0)
            {
                await Task.Delay(Loop_Delay);
            }

            //List<string> result = new List<string>();
            //result.AddRange(str.Split(','));

            RobotData.Read_StepSpeed = str;
        }
        public async void Cmd_ReadAlignerCalOffset()
        {// Aligner calibration offset
            string str = "";
            while (Seq_ReadCommand("GETALCAL", ref str) == 0)
            {
                await Task.Delay(Loop_Delay);
            }

            int nVal = 0;
            int.TryParse(str, out nVal);

            RobotData.ReadAlignerCalOffset = nVal;
        }
        public async void Cmd_Read_RobotStatus()
        {// Robot Status

            string str = "";
            while (Seq_ReadCommand("R_RSTAT", ref str) == 0)
            {
                await Task.Delay(Loop_Delay);
            }

            List<string> result = new List<string>();
            result.AddRange(str.Split(','));

            try
            {
                //int nVal = 0;
                //int.TryParse(result[0], out nVal);
                //RobotData.RunStatus = nVal == 1 ? true : false;
                //int.TryParse(result[1], out nVal);
                //RobotData.AbnormalStatus = nVal == 1 ? true : false;
                //int.TryParse(result[2], out nVal);
                //RobotData.ServoOnStatus = nVal == 1 ? true : false;
                //int.TryParse(result[3], out nVal);
                //RobotData.ArmFoldStatus = nVal == 1 ? true : false;
                //int.TryParse(result[6], out nVal);
                //RobotData.BetteryStatus = nVal == 1 ? true : false;

                int nVal = 0;
                int.TryParse(result[0].Substring(0, 1), out nVal);
                RobotData.RunStatus = nVal == 1 ? true : false;
                int.TryParse(result[0].Substring(1, 1), out nVal);
                RobotData.AbnormalStatus = nVal == 1 ? true : false;
                int.TryParse(result[0].Substring(2, 1), out nVal);
                RobotData.ServoOnStatus = nVal == 1 ? true : false;
                int.TryParse(result[0].Substring(3, 1), out nVal);
                RobotData.ArmFoldStatus = nVal == 1 ? true : false;
                int.TryParse(result[0].Substring(6, 1), out nVal);
                RobotData.BetteryStatus = nVal == 1 ? true : false;

                //추가
                int.TryParse(result[0].Substring(14, 1), out nVal);
                RobotData.LowerHand_WaferSts = nVal == 1 ? true : false;
                int.TryParse(result[0].Substring(15, 1), out nVal);
                RobotData.UpperHand_WaferSts = nVal == 1 ? true : false;
            }
            catch
            {
            }


        }
        public async void Cmd_ReadVacStatus(ARM _Hand)
        {// Vacuum Status for Arm B, 2 is Upper Arm
            string str = "";
            while (Seq_ReadCommand("VACCHEK " + (int)_Hand, ref str) == 0)
            {
                await Task.Delay(Loop_Delay);
            }

            int nVal = 0;
            int.TryParse(str, out nVal);
            if (_Hand == ARM.UPPER)
                RobotData.UpperHand_VacSts = nVal == 1 ? true : false;
            else if (_Hand == ARM.LOWER)
                RobotData.LowerHand_VacSts = nVal == 1 ? true : false;
        }
        public async void Cmd_ReadClampStatus()
        {// Clamp Status for Aligner
            string str = "";
            while (Seq_ReadCommand("GETCLAMP", ref str) == 0)
            {
                await Task.Delay(Loop_Delay);
            }

            int nVal = 0;
            int.TryParse(str, out nVal);
            RobotData.ClampStatus = nVal == 1 ? true : false;
        }
        public async void Cmd_ReadAlignerVacStatus()
        {// Vacuum Status for Aligner
            string str = "";
            while (Seq_ReadCommand("GETALVAC", ref str) == 0)
            {
                await Task.Delay(Loop_Delay);
            }

            int nVal = 0;
            int.TryParse(str, out nVal);
            RobotData.Aligner_VacSts = nVal == 1 ? true : false;
        }
        #endregion

        #region 데이터 쓰기 명령어
        public async void Cmd_WriteSpeed(int nSpeed)
        {// 로봇의 현재 System Speed를 변경하기 위한 명령
            string strCmd = string.Format("SETSPEED {0}", nSpeed);
            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_WriteStepSpeed(int nSpeed)
        {// 로봇의 현재 Profile Speed를 변경하기 위한 명령
            string strCmd = string.Format("SETSTEPSPEED {0}", nSpeed);
            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_WriteAlignerCalOffset(float fVal)
        {// Aligner calibration offset
            string strCmd = string.Format("SETALCAL {0:0.00}", fVal);
            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_WriteServoOnOff(bool bOn)
        {// Servo On/Off
            string strCmd = bOn ? "SERVOON" : "SERVOOF";
            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_WriteInitialize()
        {// 초기화
            string strCmd = "INITIAL";
            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_WriteErrorReset()
        {// Error Reset
            string strCmd = "R_RESET";
            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_Write_MotionPauseStop(PAUSE_STATUS _Status)
        {// Motion 명령
            string strCmd = "R_RSTOP";
            switch (_Status)
            {
                case PAUSE_STATUS.PAUSE: strCmd = "R_PAUSE"; break;
                case PAUSE_STATUS.RESUME: strCmd = "R_RESUM"; break;
                case PAUSE_STATUS.STOP: strCmd = "R_RSTOP"; break;
            }
            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_Write_MoveWait_FG(int nStage, int nSlot, ARM _Hand)
        {// Motion 명령 : 앞으로 이동
            string strCmd = string.Format("FGREADY {0},{1},{2}", nStage, nSlot, (int)_Hand);

            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_Write_Move_Get(int nStage, int nSlot, ARM _Hand)
        {// Motion 명령 : Get
            string strCmd = string.Format("GETFROM {0},{1},{2}", nStage, nSlot, (int)_Hand);

            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_Write_Move_Put(int nStage, int nSlot, ARM _Hand)
        {// Motion 명령 : Put
            string strCmd = string.Format("PUTINTO {0},{1},{2}", nStage, nSlot, (int)_Hand);

            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_Write_Move_Transfer(int nGetStage, int nGetSlot, ARM _GetHand, int nPutStage, int nPutSlot, ARM _PutHand)
        {// Motion 명령 : TRANSFER => 메뉴얼에는 TRANSFR로 나와있음.
            string strCmd = string.Format("TRANSFER {0},{1},{2},{3},{4},{5}\r\n"
                                            , nGetStage, nGetSlot, (int)_GetHand
                                            , nPutStage, nPutSlot, (int)_PutHand);
            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_Write_Move_Pos(int nStage, int nSlot, ARM _Hand)
        {// Motion 명령 : Teaching 위치로 이동
            string strCmd = string.Format("MOVEPOS {0},{1},{2}", nStage, nSlot, (int)_Hand);

            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_Write_Move_ArmFold()
        {// Motion 명령 : ArmFold
            string strCmd = string.Format("ARMFOLD");

            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_Write_Move_DownFold(int nStage, int nSlot, ARM _Hand)
        {// Motion 명령 : Down Fold
            string strCmd = string.Format("DOWNFOLD {0},{1},{2}", nStage, nSlot, (int)_Hand);

            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_Write_Move_FlipHand(bool bOn)
        {// Motion 명령 : Lower Hand Flip
            string strCmd = string.Format("FLIPHAND {0}", bOn ? 1 : 0);

            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        public async void Cmd_Write_Move_ArmVac(ARM _Hand, bool bOn)
        {// Motion 명령 : Down Fold
            string strCmd = string.Format("VACCTRL {0},{1}", (int)_Hand, bOn ? 1 : 0);

            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }

        public async void Cmd_Write_Move_GoReady()
        {// Motion 명령 : GOREADY
            string strCmd = string.Format("GOREADY");

            while (Seq_SendCommand(strCmd) == 0)
            {
                await Task.Delay(Loop_Delay);
            }
        }
        #endregion
    }

    public class CyborgRobot_Data_TM
    {
        public CyborgRobot_Data_TM()
        {
        }

        public int Read_Speed = 0;
        public string Read_StepSpeed = "";
        public int ReadAlignerCalOffset = 0;
        public bool RunStatus = false;
        public bool AbnormalStatus = false;
        public bool ServoOnStatus = false;
        public bool ArmFoldStatus = false;
        public bool BetteryStatus = false;
        public bool LowerHand_VacSts = false;
        public bool UpperHand_VacSts = false;
        public bool ClampStatus = false;
        public bool Aligner_VacSts = false;

        public bool LowerHand_WaferSts = false;
        public bool UpperHand_WaferSts = false;
    }
}
