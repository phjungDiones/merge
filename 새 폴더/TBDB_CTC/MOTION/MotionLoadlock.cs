using CJ_Controls.Communication.EDB2000;
using EraeMotionApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_CTC.Data;
using TBDB_Handler.GLOBAL;

namespace TBDB_Handler.MOTION
{
    public enum MoveMode
    {
        ABS_MODE,
        REL_MODE,
        COORD_MODE, //좌표 모드
    }

    //Home Ref 확인 필요
    public enum HomeRefMode
    {
        HomeRef_1 = 1,
        HomeRef_2,
        HomeRef_3,
        HomeRef_4,
        HomeRef_5,
        HomeRef_6,
        HomeRef_7,
        HomeRef_8,
    }


    public class MotionLoadlock
    {
        private MainData _Main = null;
        private byte nMotor = 0;

        const int UNIT_RPM = 50;
        byte[] RxBuffer = new byte[9];
        byte[] buffer = new byte[9];
        int nRpmSpeed = 0;
        const int MAX_MOTOR = 1;

        int nSpeedRate = 5; //10 = 100%

        int m_nPort = 27;
        int m_nRetValue = 0;
        int m_nModuleID = 1;

        int m_nSpeed = 0;
        int m_nAcc = 0;

        EMCL.MotorStatus moStatus = new EMCL.MotorStatus();

        public MotionLoadlock()
        {
            _Main = MainData.Instance;
        }

        #region Protocol Cmd
        public bool Initialize()
        {
            return _Main.GetLoadlockData().Loadlock.OpenComm(_Main.ConfigMgr.Loadlock_Com.Comport, _Main.ConfigMgr.Loadlock_Com.Baudrate);
        }

        private int GetRpm()
        {
            return (nSpeedRate * UNIT_RPM) / 60 * 51200;
        }

        private void SendCmd(byte nMotor, byte nCmd, byte nType, byte nBank, int nValue)
        {
            byte[] buffer = new byte[9];
            _Main.GetLoadlockData().Loadlock.SendCmd(nMotor, nCmd, nType, nBank, nValue, ref buffer);
        }
        private ReplyCode GetReply()
        {
            int nValue = 0;
            bool bVersion = false;

            return GetReply(ref nValue, bVersion);
        }
        private ReplyCode GetReply(ref int nValue, bool bVersion)
        {
            byte[] RxBuffer = new byte[9];
            byte uchReplyAddr = 0, ucModuleAdr = 0, uchStatus = 0, ucCmdNumber = 0;
            ReplyCode ret = _Main.GetLoadlockData().Loadlock.GetReply(ref uchReplyAddr, ref ucModuleAdr, ref uchStatus, ref ucCmdNumber, ref nValue, ref RxBuffer, bVersion);
            return ret;
        }

        private void SendCmdToAll(byte nCmd, byte nType, byte nBank, int nValue)
        {
            for (int i = 0; i < MAX_MOTOR; ++i)
            {
                SendCmd((byte)i, nCmd, nType, nBank, nValue);
                GetReply();
            }
        }



        #endregion


        #region Move Func

        public void GetConfigData()
        {
           m_nModuleID = GlobalVariable.Loadlock.nModule;
           m_nSpeed = GlobalVariable.Loadlock.nSpeed;
           m_nAcc = GlobalVariable.Loadlock.nAcc;
        }

        public void SetConfigData()
        {
            GlobalVariable.Loadlock.nModule = m_nModuleID;
            GlobalVariable.Loadlock.nSpeed = m_nSpeed;
            GlobalVariable.Loadlock.nAcc = m_nAcc;
        }


        public bool ComOpen(string strCom, int nBaud)
        {
            string sPortNum = strCom.Replace("COM", "");
            m_nPort = Convert.ToInt32(sPortNum);
            EMCL.ERAETech_EMCL_OpenComm(m_nPort, Convert.ToInt32(nBaud));

            return EMCL.ERAETech_EMCL_IsPortOpen(m_nPort);
        }

        public bool ComClose(string strCom)
        {
            EMCL.ERAETech_EMCL_CloseComm(-1);
            return true;
        }

        public void SetZero()
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_SetZeroPosition(m_nPort, Convert.ToByte(GlobalVariable.Loadlock.nModule));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);

            // send command and read recv buffer
            EMCL.ERAETech_EMCL_SetEncoderZeroPosition(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
        }

        public void JogMove_P(int nSpeed, int nAcc)
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_SetMaxAcceleration(m_nPort, Convert.ToByte(m_nModuleID), Convert.ToInt32(nAcc));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);

            // send command and read recv buffer
            EMCL.ERAETech_EMCL_RotateRight(m_nPort, Convert.ToByte(m_nModuleID), Convert.ToInt32(nSpeed));
            byte nModuleId = 0, nCmd = 0;
            EMCL.ERAETech_EMCL_WaitAndGetReply3(m_nPort, ref nModuleId, ref nCmd, ref m_nRetValue);
        }

        public void JogMove_N(int nSpeed, int nAcc)
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_SetMaxAcceleration(m_nPort, Convert.ToByte(m_nModuleID), Convert.ToInt32(nAcc));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);

            // send command and read recv buffer
            EMCL.ERAETech_EMCL_RotateLeft(m_nPort, Convert.ToByte(m_nModuleID), Convert.ToInt32(nSpeed));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
        }

        public void MoveStop(int nAcc)
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_SetMaxAcceleration(m_nPort, Convert.ToByte(m_nModuleID), Convert.ToInt32(nAcc));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);

            // send command and read recv buffer
            EMCL.ERAETech_EMCL_Stop(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
        }

        public void MoveStart(int nTargetPos, int nAcc, MoveMode mode)
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_SetMaxPositionSpeed(m_nPort, Convert.ToByte(m_nModuleID), Convert.ToInt32(nAcc));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);

            // send command and read recv buffer
            EMCL.ERAETech_EMCL_MoveToPosition(m_nPort, Convert.ToByte(m_nModuleID), (byte)mode, Convert.ToInt32(nTargetPos));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
        }

        public void SetHomeRef(HomeRefMode RefMode)
        {
            EMCL.ERAETech_EMCL_SetSearchMode(m_nPort, Convert.ToByte(m_nModuleID), Convert.ToInt32(RefMode));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
        }

        public void AlarmReset()
        {
            int nVal = 0;
            EMCL.ERAETech_EMCL_Sync_ResetPosError(m_nPort, Convert.ToByte(m_nModuleID), ref nVal);
            EMCL.ERAETech_EMCL_ResetAlarm(m_nPort, Convert.ToByte(m_nModuleID));
        }
        public void ServoOn()
        {
            EMCL.ERAETech_EMCL_SetServoOn(m_nPort, Convert.ToByte(m_nModuleID), 1);            
        }

        public void ServoOff()
        {
            EMCL.ERAETech_EMCL_SetServoOn(m_nPort, Convert.ToByte(m_nModuleID), 0);
        }

        public void SetResolution(int nValue)
        {
            EMCL.ERAETech_EMCL_SetMicroStep(m_nPort, Convert.ToByte(m_nModuleID), nValue);
        }

        public void MoveHome(int nRefSpeed)
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_SetRefSearchSpeed(m_nPort, Convert.ToByte(m_nModuleID), Convert.ToInt32(nRefSpeed));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);

            // send command and read recv buffer
            EMCL.ERAETech_EMCL_SetRefSwitchSpeed(m_nPort, Convert.ToByte(m_nModuleID), Convert.ToInt32(nRefSpeed));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);

            // send command and read recv buffer
            EMCL.ERAETech_EMCL_StartRefSearch(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
        }

        public void HomeStop(int m_nModuleID)
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_StopRefSearch(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
        }
        #endregion

        #region Status Func

        public bool IsMoving()
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_IsMoving(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
            return (0 < m_nRetValue ? true : false);
        }

        public bool Inposition()
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_IsPositionReached(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
            return (0 < m_nRetValue ? true : false);
        }

        public int GetActPos()
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_GetActualPos(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
            return (m_nRetValue);
        }

        public int GetEncoderPos()
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_GetEncoderPos(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
            return (m_nRetValue);
        }

        public int GetActSpeed()
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_GetActualSpeed(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
            return (m_nRetValue);
        }

        public int SearchStatus()
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_GetRefSearchStatus(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
            return (m_nRetValue);
        }

        public bool IsLimitP()
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_GetRightLimitStatus(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
            return (0 < m_nRetValue ? true : false);
        }

        public bool IsLimitN()
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_GetLeftLimitStatus(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
            return (0 < m_nRetValue ? true : false);
        }

        public bool IsHome()
        {
            // send command and read recv buffer
            EMCL.ERAETech_EMCL_GetHomeSwitchStatus(m_nPort, Convert.ToByte(m_nModuleID));
            EMCL.ERAETech_EMCL_WaitAndGetReply(m_nPort, ref m_nRetValue);
            return (0 < m_nRetValue ? true : false);
        }

        public void GetAllStatus(ref EMCL.MotorStatus moStatus)
        {
            EMCL.ERAETech_EMCL_GetAllStatus(m_nPort, Convert.ToByte(m_nModuleID), ref moStatus);
        }


        #endregion

    }
}
