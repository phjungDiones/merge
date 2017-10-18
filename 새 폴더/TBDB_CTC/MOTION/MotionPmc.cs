using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBDB_CTC.Data;

namespace TBDB_Handler.MOTION
{
    #region CTC_AREA

    public enum CTC_StatusSig
    {
        HeartBit = 0x0200,
    }

    public enum CTC_PIO : short
    {
        SendAble = 0x0210,
        SendStart,
        SendComplete,
        SendFail,
        RecvAble,
        RecvStart,
        RecvComplete,
        RecvFail,   
    }

    public enum CTC_VTM_ROBOT : short
    {
        RobotMoving = 0x0220,
        HandFold,
        HandStretch,
        HandDown,   
        HandUp,
    }

    public enum CTC_INTERLOCK :short
    {
        ShutterOpen = 0x0230,
        ShutterClose,
        FastPumpOpenClose,
        FastPumpChangeReq,
        DryPumpOnReq,

    }

    public enum CTC_MANUAL : short
    {
        InitReq = 0x0250,
        InitReady,
        InitCompleteAck,
        StandbyReq,
        StandbyReady,
        StandbyCompleteAck,
        ProcessReq,
        ProcessReady,
        ProcessCompleteAck,
    }   

    public enum CTC_STATUS
    {
        CTCStatus = 0x0000,
        VTMStatus,
        CTCRunMode,
    }

    public enum CTC_SATAUS_VALUE
    {
        IDLE,
        RUN,
        STOP,
        PM
    }

    public enum CTC_VTM_STATUS_VALUE
    {
        ATM,
        BENTING,
        VTM,
        PUMPING,
    }

    public enum CTC_RunMode
    {
        ModeAtm,
        ModeVtm,
    }


    public enum CTC_RECIPE
    {
        VisionUsed = 0x0400,
        Pressure,
        PressingTime,
        UpperTemp,
        LowerTemp,
        APCPosition,
        CH1Backlight,
        CH2Backlight,
        CH3Backlight,
    }

    public enum CTC_RecipeName
    {
        //ACII 20자리
        RcpName_1 = 0x0470,
        RcpName_2,
        RcpName_3,
        RcpName_4,
        RcpName_5,
        RcpName_6,
        RcpName_7,
        RcpName_8,
        RcpName_9,
        RcpName_10,
        RcpName_11,
        RcpName_12,
        RcpName_13,
        RcpName_14,
        RcpName_15,
        RcpName_16,
        RcpName_17 = 0x480,
        RcpName_18,
        RcpName_19,
        RcpName_20,

    }

    public enum CTC_STANDBY
    {
        UpperTemp,
        LowerTemp
    }


    #endregion

    #region PMC_AREA

    public enum PMC_StatusSig
    {
        HeartBit = 0x0800,
        InitStatus,
        StandbyStatus,
        LowWaferExist,
        UppWaferExist,
        DryPumpOnOffStatus,
        FastPumpStatus,
    }

    public enum PMC_PIO
    {
        RecvReq = 0x810,
        RecvReady,
        RecvComplAck,
        RecvFail,
        SendReq,
        SendReady,
        SendComplAck,
        SendFail,
    }

    public enum PMC_MOTOR
    {
        MSPDown = 0x820,
        MSPUp,
        PinDown,
        PinUp,
    }

    public enum PMC_MANUAL
    {
        InitAck = 0x0850,
        InitStart,
        InitCompl,
        InitFail,
        StandbyAck,
        StandbyStart,
        StandbyCompl,
        StandbyFail,
        ProcAck,
        ProcStart,
        ProcCompl,
        ProcFail,

    }

    public enum PMC_STATUS
    {
        PMCStatus = 0x1000,
        PMCVacuumStatus,
        PMCProcStepStatus,
        PMCRunMode,
    }

    public enum PMC_PROCDATA
    {
        OutUpperTemp = 0x1010,
        InUpperTemp,
        OutLowerTemp,
        InLowerTemp,
        Pressure,
        Probe1,
        Probe2,
        Probe3,
    }

    public enum PMC_MotorPos
    {
        DDMotorPos = 0x1020,
        MSPMotorPos,
        Pin1MotorPos,
        Pin2MotorPos,
        XAxisPos,
        YAxisPos,
        TAxisPos,
    }


    #endregion

    public class MotionPmc
    {
        private MainData _Main = null;

        public MotionPmc()
        {
            _Main = MainData.Instance;
        }


        public string ToHex( int value)
        {
            return String.Format("0x{0}", value);
        }

        public int SetBit(short addr, bool bOn)
        {
            int Ret = 0;
            switch(bOn)
            {
                case false:
                    Ret = _Main.GetPmcData().Pmc.ResetBit(addr);
                    break;

                case true:
                    Ret = _Main.GetPmcData().Pmc.SetBit(addr);
                    break;
            }

            return Ret;
        }

        public int GetBit(short addr)
        {
            int nRet = 0;
            nRet = _Main.GetPmcData().Pmc.GetBit(addr);
            return nRet;
        }


        public int SetWord(short addr, short value)
        {
            int nRet = 0;
            nRet = _Main.GetPmcData().Pmc.SetWord(addr, value);
            return nRet;
        }

        public int GetWord(short addr, ref short read)
        {
            int nRet = 0;
            nRet = _Main.GetPmcData().Pmc.GetWord(addr, ref read);
            return nRet;
        }

        #region CTC_CMD

        public int GetPIO(CTC_PIO CTCtoPMC)
        {
            int nRet = 0;
            nRet = GetBit((short)CTCtoPMC);
            if (nRet < 0)
            {
                //Error 
            }
            return nRet;
        }

        public int GetManualCmd(CTC_MANUAL CTCtoPMC)
        {
            int nRet = 0;
            nRet = GetBit((short)CTCtoPMC);
            if (nRet < 0)
            {
                //Error 
            }
            return nRet;
        }

        public int GetVTMRobot(CTC_VTM_ROBOT CTCtoPMC)
        {
            int nRet = 0;
            nRet = GetBit((short)CTCtoPMC);
            if (nRet < 0)
            {
                //Error 
            }
            return nRet;
        }

        public int GetInterlock(CTC_INTERLOCK CTCtoPMC)
        {
            int nRet = 0;
            nRet = GetBit((short)CTCtoPMC);
            if (nRet < 0)
            {
                //Error 
            }
            return nRet;
        }

        public int GetStatus(CTC_STATUS CTCtoPMC, ref short nStatus)
        {
            int nRet = 0;
            nRet = GetWord((short)CTCtoPMC, ref nStatus);
            return nRet;
        }

        public int GetStatusSig(CTC_StatusSig CTCtoPMC)
        {
            int nRet = 0;
            nRet = GetBit((short)CTCtoPMC);
            if (nRet < 0)
            {
                //Error 
            }
            return nRet;
        }

        public int GetRecipe(CTC_RECIPE CTCtoPMC, ref short nStatus)
        {
            int nRet = 0;
            nRet = GetWord((short)CTCtoPMC, ref nStatus);
            return nRet;
        }

        public int GetRecipeName(CTC_RecipeName CTCtoPMC, ref short nStatus)
        {
            int nRet = 0;
            nRet = GetWord((short)CTCtoPMC, ref nStatus);
            return nRet;
        }


        public int SetPIO(CTC_PIO CTCtoPMC, bool bOnOff)
        {
            int nRet = 0;
            nRet = SetBit((short)CTCtoPMC, bOnOff);
            return nRet;
        }

        public int SetManualCmd(CTC_MANUAL CTCtoPMC, bool bOnOff)
        {
            int nRet = 0;
            nRet = SetBit((short)CTCtoPMC, bOnOff);
            return nRet;
        }

        public int SetVTMRobot(CTC_VTM_ROBOT CTCtoPMC, bool bOnOff)
        {
            int nRet = 0;
            nRet = SetBit((short)CTCtoPMC, bOnOff);
            return nRet;
        }

        public int SetInterlock(CTC_INTERLOCK CTCtoPMC, bool bOnOff)
        {
            int nRet = 0;
            nRet = SetBit((short)CTCtoPMC, bOnOff);
            return nRet;
        }

        public int SetStatus(CTC_STATUS CTCtoPMC, short nStatus)
        {
            int nRet = 0;
            nRet = SetWord((short)CTCtoPMC, nStatus);
            return nRet;
        }

        public int SetStatusSig(CTC_StatusSig CTCtoPMC, bool bOnOff)
        {
            int nRet = 0;
            nRet = SetBit((short)CTCtoPMC, bOnOff);
            return nRet;
        }


        public int SetRecipe(CTC_RECIPE CTCtoPMC, short nStatus)
        {
            int nRet = 0;
            nRet = SetWord((short)CTCtoPMC, nStatus);
            return nRet;
        }

        public int SetRecipeName(CTC_RecipeName CTCtoPMC, short nStatus)
        {
            int nRet = 0;
            nRet = SetWord((short)CTCtoPMC, nStatus);
            return nRet;
        }

//         public int SetRecipe(CTC_STANDBY CTCtoPMC, short nStatus)
//         {
//             int nRet = 0;
//             nRet = SetWord((short)CTCtoPMC, nStatus);
//             return nRet;
//         }

        #endregion


        #region PMC_CMD

        public int GetPIO(PMC_PIO PMCtoCTC)
        {
            int nRet = 0;
            nRet = GetBit((short)PMCtoCTC);
            if( nRet < 0 )
            {
                //Error 
            }
            return nRet;
        }


        public int GetManualCmd(PMC_MANUAL PMCtoCTC)
        {
            int nRet = 0;
            nRet = GetBit((short)PMCtoCTC);
            if (nRet < 0)
            {
                //Error 
            }
            return nRet;
        }

        public int GetMotor(PMC_MOTOR PMCtoCTC)
        {
            int nRet = 0;
            nRet = GetBit((short)PMCtoCTC);
            if (nRet < 0)
            {
                //Error 
            }
            return nRet;
        }

        public int GetStatus(PMC_STATUS PMCtoCTC, ref short nStatus)
        {
            int nRet = 0;
            nRet = GetWord((short)PMCtoCTC, ref nStatus);
            return nRet;
        }

        public int GetStatusSig(PMC_StatusSig PMCtoCTC)
        {
            int nRet = 0;
            nRet = GetBit((short)PMCtoCTC);
            if (nRet < 0)
            {
                //Error 
            }
            return nRet;
        }

        public int GetProcData(PMC_PROCDATA PMCtoCTC, ref short nStatus)
        {
            int nRet = 0;
            nRet = GetWord((short)PMCtoCTC, ref nStatus);
            return nRet;
        }

        public int GeMotorPos(PMC_MotorPos PMCtoCTC, ref short nStatus)
        {
            int nRet = 0;
            nRet = GetWord((short)PMCtoCTC, ref nStatus);
            return nRet;
        }

        #endregion
    }
}
