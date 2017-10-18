using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CJ_Controls.Communication.QuadraVTM4
{
    #region DEFINE Data
    public enum ACK_MODE
    {
        NONE = 0,
        ACK,
        DATA,
        COMPLETE,
        ERROR = -1,
        WARNING = -2,
    }
    public enum WORK_STATUS
    {
        IDLE = 0,
        MOVING,
        READING,
        ERROR,
    }
    public enum ARM
    {
        LOWER = 1,
        UPPER = 2,
    }
    public enum RADIAL_POS
    {
        EXTENDED = 0,
        RETRACTED = 1,
    }
    public enum UPDOWN_POS
    {
        UP = 0,
        DOWN = 1,
    }
    public enum PAUSE_STATUS
    {
        PAUSE = 1,
        RESUME,
        STOP,
    }
    public enum ONOFF
    {
        OFF = 0,
        ON = 1,
    }
    public enum WAFER_STS
    {
        UNKNOWN = 0,
        ABSENT = 1,
        PRESENT = 2,
    }
    public enum CONTROL_MODE
    {
        NONE = 0,
        CDM = 1,
        HOST = 2,
    }
    #endregion

    public class CyMechsRobot
    {
        #region Log 보내기
        public delegate void MessageEventHandler(object sender, MessageEventArgs args);
        public event MessageEventHandler MessageEvent;
        private void LogTextOut(string message)
        {
            if (MessageEvent != null)
            {
                string strInfo = string.Format("CyMechsRobot({0})=>", m_SerialPort.PortName);
                MessageEvent(this, new MessageEventArgs(strInfo + message));
            }
        }
        #endregion

        Thread m_Thread_Read = null;
        public CyMechsRobot_Data RobotData = new CyMechsRobot_Data();
        public CyMechsRobot()
        {
            m_Thread_Read = new Thread(new ThreadStart(SerialPort_DataReceived));
            m_Thread_Read.IsBackground = true;
            m_Thread_Read.Start();
        }

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
                //m_SerialPort.Encoding = Encoding.ASCII;
                m_SerialPort.StopBits = StopBits.One;
                m_SerialPort.DataBits = 8;
                m_SerialPort.Parity = Parity.None;
                m_SerialPort.Handshake = Handshake.None;

                m_SerialPort.DtrEnable = true;
                //m_SerialPort.DiscardNull = true;
                //m_SerialPort.RtsEnable = true;

                //m_SerialPort.NewLine = "\r";
                m_SerialPort.ReadTimeout = 2000;
                m_SerialPort.WriteTimeout = 2000;

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

                    string strRcv = "";
                    RcvCheck(ref strRcv);
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
            if (IsReceived("\r") == true)
            {
                byte[] byRcv = GetReceiveDataByteEx_EndCut();
                sRcvData = GetReceiveDataEx_EndCut();
                LogTextOut(string.Format("RecvData:{0}", sRcvData));

                if (sRcvData.Contains("_ACK") == true)
                {//정상적으로 수행
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.ACK;
                    _WorkStatus = WORK_STATUS.MOVING;
                }
                else if (sRcvData.Contains("_RDY") == true)
                {//동작완료.
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.COMPLETE;
                    _WorkStatus = WORK_STATUS.IDLE;
                }
                else if (sRcvData.Contains("_NAK") == true)
                {//명령이 잘못됨.
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.ERROR;
                    _WorkStatus = WORK_STATUS.ERROR;
                    Global.GlobalFunction.Instance.GetErrNumber_3(_ErrMsg, Global.GlobalDefine.Eidentify_error.CyMechsRobot);
                }
                else if (sRcvData.Contains("_ERR") == true || sRcvData.Contains("ERR") == true)
                {//Alarm이 발생하여 Error Code 전송.
                    FlushEx_EndCheckCut();
                    _ErrMsg = Encoding.ASCII.GetString(byRcv, 0, byRcv.Length);
                    Global.GlobalFunction.Instance.GetErrNumber_3(_ErrMsg, Global.GlobalDefine.Eidentify_error.CyMechsRobot);
                    _AckMode = ACK_MODE.ERROR;
                    _WorkStatus = WORK_STATUS.ERROR;
                }
                else if (sRcvData.Contains("POWER OFF") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.PowerStatus = ONOFF.OFF;
                }
                else if (sRcvData.Contains("POWER ON") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.PowerStatus = ONOFF.ON;
                }
                else if (sRcvData.Contains("WAFER A Y") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Wafer_A_Status = WAFER_STS.PRESENT;
                }
                else if (sRcvData.Contains("WAFER A N") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Wafer_A_Status = WAFER_STS.ABSENT;
                }
                else if (sRcvData.Contains("WAFER A ERR") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Wafer_A_Status = WAFER_STS.UNKNOWN;
                }
                else if (sRcvData.Contains("WAFER B Y") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Wafer_B_Status = WAFER_STS.PRESENT;
                }
                else if (sRcvData.Contains("WAFER B N") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Wafer_B_Status = WAFER_STS.ABSENT;
                }
                else if (sRcvData.Contains("WAFER B ERR") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Wafer_B_Status = WAFER_STS.UNKNOWN;
                }
                else if (sRcvData.Contains("VAC A N") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Vaccum_A_Status = ONOFF.OFF;
                }
                else if (sRcvData.Contains("VAC A Y") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Vaccum_A_Status = ONOFF.ON;
                }
                else if (sRcvData.Contains("VAC B N") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Vaccum_B_Status = ONOFF.OFF;
                }
                else if (sRcvData.Contains("VAC B Y") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Vaccum_B_Status = ONOFF.ON;
                }
                else if (sRcvData.Contains("CDM") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.HostControlMode = CONTROL_MODE.CDM;
                }
                else if (sRcvData.Contains("HOST") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.HostControlMode = CONTROL_MODE.HOST;
                }
                else if (sRcvData.Contains("VER") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.Version = sRcvData;
                }
                else if (sRcvData.Contains("ENC A") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.nEncA = Convert.ToInt32(sRcvData.Substring(4));
                }
                else if (sRcvData.Contains("ENC B") == true)
                {
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
                    RobotData.nEncB = Convert.ToInt32(sRcvData.Substring(4));
                }
                else
                {//위의것이 아니면, 거의 체크 데이터로 본다. 그냥..
                    FlushEx_EndCheckCut();
                    _AckMode = ACK_MODE.DATA;
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
        {//cjinnnn: 연속으로 붙어오는 데이터 처리를 위한 함수. \r 단위로 자른다.
            if (IsOpen() == false)
                return;

            try
            {
                int nIndex = IsReceived((byte)ASCII.CR);
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

        private bool SendData(char[] str)
        {
            try
            {
                if (m_SerialPort.IsOpen.Equals(true))
                {
                    //byte[] data1 = System.Text.Encoding.ASCII.GetBytes(str);
                    //char[] ch = new char[data1.Length];

                    //ch = System.Text.Encoding.ASCII.GetChars(data1);
                    //m_SerialPort.Write(ch, 0, ch.Length);
                    //m_SerialPort.Write(data1, 0, data1.Length);
                    m_SerialPort.Write(str, 0, str.Length);

                    Thread.Sleep(2000);

                    string strff = m_SerialPort.ReadLine();
                    LogTextOut(string.Format("SendData:{0}", str));
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
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
            int nIndex = IsReceived((byte)ASCII.CR);
            return Encoding.ASCII.GetString(m_ReceiveBuffer, 0, nIndex + 1);
        }
        private byte[] GetReceiveDataByte()
        {
            return m_ReceiveBuffer;
        }
        private byte[] GetReceiveDataByteEx_EndCut()
        {
            byte[] _Rtn;
            int nIndex = IsReceived((byte)ASCII.CR);
            if (nIndex <= 0)
                _Rtn = m_ReceiveBuffer;
            else
            {
                byte[] TempBuf = new byte[nIndex + 1];
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

        #region 명령어
        public bool Cmd_ReadOpMode()
        {
            string strCmd = "RQ OPMODE\r";
            return SendData(strCmd);
        }
        public bool Cmd_ReadVersion()
        {
            string strCmd = "RQ VERSION\r";
            return SendData(strCmd);
        }
        public bool Cmd_ReadPower()
        {
            string strCmd = "RQ POWER\r";
            return SendData(strCmd);
        }
        public bool Cmd_ReadWaferArm(ARM _Arm)
        {
            string strCmd = string.Format("RQ WAFER ARM {0}\r", _Arm == ARM.LOWER ? "A" : "B");
            return SendData(strCmd);
        }
        public bool Cmd_ReadError()
        {
            string strCmd = "RQ ERROR\r";
            return SendData(strCmd);
        }

        public bool Cmd_SendHello()
        {
            string strCmd = "HLLO";
            strCmd += Convert.ToString((char)13);
            //strCmd += Convert.ToString((char)0);
            return SendData(strCmd);
            //return SendData(ch);
        }
        public bool Cmd_SendClear()
        {
            string strCmd = "CLEAR\r";
            return SendData(strCmd);
        }
        public bool Cmd_SendHomeAll()
        {
            _WorkStatus = WORK_STATUS.MOVING;

            char[] CH = new char[3];
            CH[0] = (char)0x0d;
            CH[1] = (char)0x00;
            CH[1] = (char)0x00;
            StringBuilder st = new StringBuilder("HOME ALL");
            st.Append(CH[0].ToString());
            //string strCmd = st.ToString();//"HOME ALL\r";
            //strCmd = strCmd + CH[0].ToString() + CH[1].ToString() + CH[2].ToString();
            string strCmd = "HOME ALL\r";
            return SendData(strCmd);
        }
        public bool Cmd_SendHome_R()
        {
            _WorkStatus = WORK_STATUS.MOVING;
            string strCmd = "HOME R\r";
            return SendData(strCmd);
        }
        public bool Cmd_SendHome_T()
        {
            _WorkStatus = WORK_STATUS.MOVING;
            string strCmd = "HOME T\r";
            return SendData(strCmd);
        }
        public bool Cmd_SendHome_Z()
        {
            _WorkStatus = WORK_STATUS.MOVING;
            string strCmd = "HOME Z\r";
            return SendData(strCmd);
        }
        public bool Cmd_SendRelease()
        {
            string strCmd = string.Format("RELEASE");
            return SendData(strCmd);
        }
        public bool Cmd_SendGoTo(int nStage, int nSlot, ARM _Arm, RADIAL_POS _RadialPos, UPDOWN_POS _UpDnPos)
        {
            _WorkStatus = WORK_STATUS.MOVING;
            string strCmd = string.Format("GOTO N {0} R {1} Z {2} SLOT {3} ARM {4}\r"
                                            , nStage
                                            , _RadialPos == RADIAL_POS.EXTENDED ? "EX" : "RE"
                                            , _UpDnPos == UPDOWN_POS.UP ? "UP" : "DN"
                                            , nSlot
                                            , _Arm == ARM.LOWER ? "A" : "B");
            return SendData(strCmd);
        }
        public bool Cmd_SendPick(int nStage, int nSlot, ARM _Arm)
        {
            _WorkStatus = WORK_STATUS.MOVING;
            string strCmd = string.Format("PICK {0} SLOT {1} ARM {2}\r"
                                            , nStage
                                            , nSlot
                                            , _Arm == ARM.LOWER ? "A" : "B");
            return SendData(strCmd);
        }
        public bool Cmd_SendPlace(int nStage, int nSlot, ARM _Arm)
        {
            _WorkStatus = WORK_STATUS.MOVING;
            string strCmd = string.Format("PLACE {0} SLOT {1} ARM {2}\r"
                                            , nStage
                                            , nSlot
                                            , _Arm == ARM.LOWER ? "A" : "B");
            return SendData(strCmd);
        }
        public bool Cmd_SendZAxis(int nStage, int nSlot, ARM _Arm, UPDOWN_POS _UpDnPos)
        {
            _WorkStatus = WORK_STATUS.MOVING;
            string strCmd = string.Format("ZAXIS {0} SLOT {1} {2} ARM {3}\r"
                                            , nStage
                                            , nSlot
                                            , _UpDnPos == UPDOWN_POS.UP ? "UP" : "DN"
                                            , _Arm == ARM.LOWER ? "A" : "B");
            return SendData(strCmd);
        }

        public bool Cmd_Extend(int nStage, int nSlot, ARM _Arm, UPDOWN_POS _UpDnPos)
        {
            _WorkStatus = WORK_STATUS.MOVING;
            string strCmd = string.Format("EXTEND {0} {1} {2} {3}\r"
                                            , nStage
                                            , _UpDnPos == UPDOWN_POS.UP ? "UP" : "DN"
                                            , nSlot
                                            , _Arm == ARM.LOWER ? "A" : "B");
            return SendData(strCmd);
        }

        public bool Cmd_Retract(int nStage, int nSlot, ARM _Arm, UPDOWN_POS _UpDnPos)
        {
            _WorkStatus = WORK_STATUS.MOVING;
            string strCmd = string.Format("RETRACT {0} {1} {2} {3}\r"
                                            , nStage
                                            , _UpDnPos == UPDOWN_POS.UP ? "UP" : "DN"
                                            , nSlot
                                            , _Arm == ARM.LOWER ? "A" : "B");
            return SendData(strCmd);
        }

        public bool Cmd_ReadEnc(ARM _Arm)
        {
            string strCmd = string.Format("RQ ENC ARM {0}\r"
                                            , _Arm == ARM.LOWER ? "A" : "B");
            return SendData(strCmd);
        }

        #endregion
    }

    public class CyMechsRobot_Data
    {
        public CyMechsRobot_Data()
        {
        }

        public string Version = "";
        public ONOFF PowerStatus = ONOFF.OFF;
        public WAFER_STS Wafer_A_Status = WAFER_STS.UNKNOWN;
        public WAFER_STS Wafer_B_Status = WAFER_STS.UNKNOWN;
        public ONOFF Vaccum_A_Status = ONOFF.OFF;
        public ONOFF Vaccum_B_Status = ONOFF.OFF;
        public CONTROL_MODE HostControlMode = CONTROL_MODE.NONE;
        public int nEncA = 0;
        public int nEncB = 0;
    }
}
