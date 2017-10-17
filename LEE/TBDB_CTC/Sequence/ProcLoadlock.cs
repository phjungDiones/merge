using CJ_Controls;
using EraeMotionApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;

namespace TBDB_CTC.Sequence
{

    public enum MotionPos
    {
        Pos1 = 0,
        Pos2,
        Pos3
    }

    public enum Vacuum_Seq
    {
        VTM_Pumping,
        VTM_Venting,
        Loadlock_Pumping,
        Loadlock_Venting,
        max,
    }

    public enum LoadlockCmd
    {
        GetStatus = 0,
        Move,
        CheckMoveDone,
        AlarmReset,
    }

    public class ProcLoadlock
    {
        public MotionLoadlock Loadlock = new MotionLoadlock();

        private bool isCycleRun = false;
        public int nSeqNo = 0;
        private int nPreSeqNo = 0;

        public delegate void procAddMsgEvent(int nSeq, int nCaseNo, string strAddMsg);
        public event procAddMsgEvent procMsgEvent;

        public delegate void procFMErrorStopEvent(int nErrSafty);
        public event procFMErrorStopEvent errSaftyStopEvent;

        int nSeq = (int)UNIT.LOADLOCK;
        int[] nSeqVacuum = new int[(int)Vacuum_Seq.max];
        public bool bMoving = false;

        EMCL.MotorStatus moStatus = new EMCL.MotorStatus();


        //TimeOut 및 Delay Parameter
        //Config Data와 연결하자
        //임시
        private long lTimeOut = 10000;
        private long lLL_VentOpenTime = 20000;
        private long lLL_VentCloseTime = 20000;

        private long lLL_PumpingTime = 20000;

        //VtmPumping
        private long lTime_VTM_PumpConRy1 = 20000;
        private long lTime_VTM_PumpConRy2 = 20000;

        //VtmVenting
        private long lTime_VTM_VentConRy1 = 20000;
        private long lTime_VTM_VentConRy2 = 20000;

        //Loadlock Pumping
        private long lTime_LL_PumpConRy1 = 20000;
        private long lTime_LL_PumpConRy2 = 20000;

        //Loadlock Venting
        private long lTime_LL_VentOpen = 20000;
        private long lTime_LL_VentClose = 20000;


        //TimeOut Check
        private Stopwatch timeVtmPump = Stopwatch.StartNew();
        private Stopwatch timeVtmVent = Stopwatch.StartNew();

        private Stopwatch timeLLPump = Stopwatch.StartNew();
        private Stopwatch timeLLVent = Stopwatch.StartNew();


        void AddMessage(string msg)
        {
            if (procMsgEvent != null)
                procMsgEvent(nSeq, nSeqNo, msg);
        }

        #region Common Function
        private bool isAcceptRun()
        {
            if (!GlobalVariable.mcState.isRun || GlobalVariable.mcState.isManualRun || isCycleRun) return false;
            if (!GlobalVariable.mcState.isRdy) return false;
            if (GlobalVariable.mcState.isWaitPopup) return false;
            return true;
        }

        public void resetCmd()
        {
            //             rLib.resetCmd();
            //             rLib.setTimeBegin();
        }

        private void nextSeq(int nSeq)
        {
            /*            rLib.resetCmd();*/
            if (nSeq >= 0) { nSeqNo = nSeq; }
        }

        #endregion Common Function


        #region Safety Check

        /// <summary>
        /// </summary>
        /// <returns></returns>

        public fn getSafetyAxis(int nAxis)
        {
            return fn.success;
        }

        public fn getSafetyAxisPos(int nAxis, double dPos)
        {
            return fn.success;
        }

        public void alwaysCheck()
        {

        }

        #endregion Safety Check



        private int m_nCmd = 0;
        private object m_objLock = new object();
        private int m_nPosi = 0;
        private int m_nAcc = 0;


//         GlobalVariable.model.nLoadlockPos[0]
//         GlobalVariable.model.nLoadlockPos[1]
//         GlobalVariable.model.nLoadlockPos[2]  
//         GlobalVariable.model.nLLSpeed
//         GlobalVariable.model.nLLAcc 

        MoveMode Mode = MoveMode.ABS_MODE;

        public int m_nEncpos = 0;
        public int m_nCmdpos = 0;

        public void Run()
        {
            if (GlobalVariable.LoadlockMotor.bIsOpen == false)
            {
                //
                return;
            }

            switch (m_nCmd)
            {
                case 0: //idle - No Move
                    Loadlock.GetAllStatus(ref moStatus);
                    GlobalVariable.LoadlockMotor.nActPos = moStatus.nActualPos;
                    GlobalVariable.LoadlockMotor.nEncPos = moStatus.nPosError;
                    GlobalVariable.LoadlockMotor.bMoving = Convert.ToBoolean(moStatus.nIsBusy);
                    GlobalVariable.LoadlockMotor.bInpos = Convert.ToBoolean(moStatus.nInPosition);
                    GlobalVariable.LoadlockMotor.bLimitP = Convert.ToBoolean(moStatus.nRightLimitStatus);
                    GlobalVariable.LoadlockMotor.bLmitN = Convert.ToBoolean(moStatus.nLeftLimitStatus);
                    GlobalVariable.LoadlockMotor.bHome = Convert.ToBoolean(moStatus.nHomeStatus);
                    GlobalVariable.LoadlockMotor.bServo = Convert.ToBoolean(moStatus.nIsServoOn);
                    GlobalVariable.LoadlockMotor.nHomeStatus = Convert.ToInt32(moStatus.nHomeStatus);

                    break;

                case 10: //Move -NonBlock
                    Loadlock.MoveStart(m_nPosi, GlobalVariable.model.nLLAcc, MoveMode.ABS_MODE);
                    m_nCmd = 0;
                    break;

                case 11: //Move -Block
                    Loadlock.MoveStart(m_nPosi, GlobalVariable.model.nLLAcc, MoveMode.ABS_MODE);
                    if (Loadlock.GetActPos() == m_nPosi)
                    {
                        m_nCmd = 0;
                        break;
                    }
                    while (Loadlock.GetActPos() != m_nPosi)
                    {
                        // timeout check

//                         if (timeover)
//                         {
//                             Loadlock.MoveStop();
// 
//                             //setError
//                             m_nCmd = 0;
//                             break;
//                         }
                    }

                    m_nCmd = 0;
                    break;


                case 20: //Reset alarm
                    Loadlock.AlarmReset();
                    m_nCmd = 0;
                    break;

                case 30: // Homming
                    Loadlock.SetHomeRef(HomeRefMode.HomeRef_8);
                    Thread.Sleep(100);
                    Loadlock.MoveHome(GlobalVariable.model.nLLSpeed);

                    while (true)
                    {
                        Loadlock.GetAllStatus(ref moStatus);
                        if (moStatus.nHomeStatus == 1)
                        {
                            break;
                        }

                        Thread.Sleep(100);
                        // timeout check

                            //                         if (timeover)
                            //                         {
                            //                             Loadlock.MoveStop();
                            // 
                            //                             //setError
                            //                             m_nCmd = 0;
                            //                             break;
                            //                         }
                    }
                    m_nCmd = 0;
                    break;

                case 40:

                    Loadlock.MoveStart(m_nPosi, m_nAcc, Mode);
                    m_nCmd = 0;
                    break;

                case 50:
                    //JogMove -
                    Loadlock.JogMove_P(GlobalVariable.model.nLLSpeed, GlobalVariable.model.nLLAcc);
                    m_nCmd = 0;
                    break;

                case 51:
                    //Move Stop
                    //Loadlock.MoveStop(GlobalVariable.model.nLLAcc);
                    //m_nCmd = 0;
                    break;

                case 60:
                    //JogMove +
                    Loadlock.JogMove_N(GlobalVariable.model.nLLSpeed, GlobalVariable.model.nLLAcc);
                    m_nCmd = 0;
                    break;

                case 61:
                    //Move Stop
                    //Loadlock.MoveStop(GlobalVariable.model.nLLAcc);
                    //m_nCmd = 0;
                    break;

                case 70:
                    //Move Stop
                    Loadlock.MoveStop(GlobalVariable.model.nLLAcc);
                    m_nCmd = 0;
                    break;

                case 80:
                    //Home Stop
                    Loadlock.HomeStop();
                    m_nCmd = 0;
                    break;



                case 100: //block
                    break;

                default:
                    m_nCmd = 0;
                    break;
            }
        }


        /// <summary>
        /// Motor 고정 - 사용예약
        /// </summary>
        /// <returns></returns>
        public bool SetBlock()
        {
            lock (m_objLock)
            {
                if (m_nCmd != 0) { return false; }
                m_nCmd = 100; // Move
                return true;
            }
        }

        /// <summary>
        ///  Motor 사용가능토록
        /// </summary>
        /// <returns></returns>
        public bool UnBlock()
        {
            lock (m_objLock)
            {
                if (m_nCmd != 100) { return false; }
                m_nCmd = 0;
                return true;
            }
        }

        /// <summary>
        /// return 0 : ok, -value : fail, +value : working
        /// </summary>
        /// <param name="nPos"></param>
        /// <returns></returns>
        public int MoveExit(int nPos)
        {
#if !_REAL_MC
            return 0;
#endif
            lock (m_objLock)
            {
                if (m_nCmd != 0) { return m_nCmd; }

                //인터락 체크
                //if (CheckATMStatus() == false) { return -1; }
                //if (CheckVTMStatus() == false) { return -1; }

                m_nAcc = GlobalVariable.model.nLLAcc;
                m_nPosi = GlobalVariable.model.nLoadlockPos[nPos];

                m_nCmd = 10; // Move
            }

            while (m_nCmd != 0) { Thread.Sleep(10); }
            return 0;
        }

        public int MoveExit_Target(int nPos, MoveMode moveMode)
        {
            lock (m_objLock)
            {
                if (m_nCmd != 0) { return m_nCmd; }

                //인터락 체크
                //if (CheckATMStatus() == false) { return -1; }
                //if (CheckVTMStatus() == false) { return -1; }

                m_nAcc = GlobalVariable.model.nLLAcc;
                m_nPosi = nPos;
                Mode = moveMode;
                m_nCmd = 40; // Move
            }

            while (m_nCmd != 0) { Thread.Sleep(10); }
            return 0;
        }

        public int JogMoveP()
        {
            lock (m_objLock)
            {
                if (m_nCmd != 0) { return m_nCmd; }

                //인터락 체크
                //if (CheckATMStatus() == false) { return -1; }
                //if (CheckVTMStatus() == false) { return -1; }

                m_nCmd = 50; // Move
            }
            return 0;
        }

        public int JogMoveN()
        {
            lock (m_objLock)
            {
                if (m_nCmd != 0) { return m_nCmd; }

                //인터락 체크
                //if (CheckATMStatus() == false) { return -1; }
                //if (CheckVTMStatus() == false) { return -1; }

                m_nCmd = 60; // Move
            }
            return 0;
        }

        public int MoveStop()
        {

            lock (m_objLock)
            {
                //if (m_nCmd == 51 || m_nCmd == 61 /* || m_nCmd = 50 || m_nCmd = 60*/)
                //{
                //    m_nCmd = 70; //Stop
                //    return 0;
                //}
                if (m_nCmd != 0) { return m_nCmd; }
                m_nCmd = 70; //Stop
            }
            return 0;
        }

        public int HomStop()
        {
            lock (m_objLock)
            {
                if (m_nCmd != 0) { return m_nCmd; }

                m_nCmd = 80;
            }
            return 0;
        }

        public int HomeMove()
        {
            lock (m_objLock)
            {
                if (m_nCmd != 0) { return m_nCmd; }

                //인터락 체크
                //if (CheckATMStatus() == false) { return -1; }
                //if (CheckVTMStatus() == false) { return -1; }

                m_nCmd = 30; 
            }
            return 0;
        }

        public bool CheckMoveDone(int nTargetPos)
        {
#if !_REAL_MC
            return true;
#endif
            lock (m_objLock)
            {
                if (m_nCmd != 0) { return false; }

                if (GlobalVariable.LoadlockMotor.nActPos != GlobalVariable.model.nLoadlockPos[nTargetPos])
                {
                    return false;
                }

                if(GlobalVariable.LoadlockMotor.bMoving == true )
                {
                    return false;
                }

                if (GlobalVariable.LoadlockMotor.bInpos == false)
                {
                    return false;
                }
            }
            return true;
        }


        public void Move(MotionPos Pos)
        {
            //int nSpeed = GlobalVariable.model.nLLSpeed;
            int nAcc = GlobalVariable.model.nLLAcc;
            int nPos = GlobalVariable.model.nLoadlockPos[(int)Pos];

            bMoving = true;
            Loadlock.MoveStart(nPos, nAcc, MoveMode.ABS_MODE);
        }

        public fn CheckComplete()
        {
#if !_REAL_MC
            return fn.success;
#endif

            if ( !Loadlock.IsMoving() && Loadlock.Inposition() == true)
            {
                bMoving = false;
                return fn.success;
            }
            else
            {
                //Alarm Check
                if (Loadlock.IsLimitN() || Loadlock.IsLimitP())
                    return fn.err;
                
            }
            return fn.busy;
        }


        public fn VTM_Pumping()
        {
            int nSeq = (int)Vacuum_Seq.VTM_Pumping;
            short nStatus = 0;

            switch (nSeqVacuum[nSeq])
            {
                case 0:

                    // Pumping Status Check

                    //Convectron Sensor On Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_1) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_2) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_TmMainPump_ValueOpen) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_TmMainPump_ValueClose) == false)
                    {
                        //위 조건 맞으면 진행하지 않는다
                        return fn.success;
                    }
                    else
                    {
                        break;
                    }

                case 5:
                    //Interlock Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_BonderGate_OpenStatus) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_BonderGate_CloseStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadlockTmGate_OpenStatus) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadlockTmGate_CloseStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_ChamberTopLID_OpenStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTM_ATMSwtich1) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_TmMainPump_ValueOpen) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_TmMainPump_ValueClose) == true) //체크
                    {
                        break;
                    }
                    else
                    {
                        return fn.err;
                    }

                case 10:
                    //All Valve Close
                    //EQUAL_VV Close

                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VacuumTmVent_VV, false);
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_ChamberEqualPressure_VV, false);
                    break;

                case 12:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.DryPumpOnOffStatus) == 0)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.DryPumpOnReq, true);
                    }
                    timeVtmPump.Restart();
                    break;

                case 13:
                    if(GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.DryPumpOnOffStatus) == 1)
                    {

                        GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.DryPumpOnReq, false);
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeVtmPump.ElapsedMilliseconds > lTimeOut)
                        {
                            return fn.err;
                        }
                    }
                    return fn.busy;

                case 15:
                    //Set FastPump Close
                    //Fast Pump변경 전 ChangeReq Bit 변경
                    nStatus = 0;
                    if(GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(PMC_STATUS.PMCVacuumStatus, ref nStatus) == 2 
                        || GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.FastPumpStatus) == 1)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.FastPumpOpenClose, false);
                        GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.FastPumpChangeReq, true);
                    }
                    timeVtmPump.Restart();
                    break;

                case 17:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.FastPumpStatus) == 0)
                    {
                        //Fast Pump상태 확인 후 Change Req Bit 꺼준다
                        GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.FastPumpChangeReq, false);

                        //Slow Pump VV Open
                        GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VacuumTMSlowPump_VV, true);
                        timeVtmPump.Restart();
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeVtmPump.ElapsedMilliseconds > lTimeOut)
                        {
                            return fn.err;
                        }
                   }
                   return fn.busy;

                case 20:
                    //Low Pump Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTM_ATMSwtich1) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_1) == true)
                    {
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeVtmPump.ElapsedMilliseconds > lTime_VTM_PumpConRy1)
                        {
                            //LowPump Time Out Err
                            return fn.err;
                        }
                    }
                    return fn.busy;

                case 25:
                    //Fast Pum VV Open
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VacuumTmmainPump_VV, true);

                    Thread.Sleep(500);

                    timeVtmPump.Restart();
                    break;

                case 30:
                    //Pumping VV Open Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_TmMainPump_ValueOpen) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_TmMainPump_ValueClose) == false)
                    {
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeVtmPump.ElapsedMilliseconds > lTimeOut)
                        {
                            return fn.err;
                        }
                    }
                    return fn.busy;
                case 35:
                    //Slow Pump VV Close
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VacuumTMSlowPump_VV, false);
                    timeVtmPump.Restart();
                    break;

                case 40:
                    //High Pump Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_2) == true)
                    {

                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeVtmPump.ElapsedMilliseconds > lTime_VTM_PumpConRy2)
                        {
                            return fn.err;
                        }                     
                    }
                    return fn.busy;

                case 45:

                    //PMC(Bonder)Fast Pump VV On
                    //Fast Pump변경 전 ChangeReq Bit 변경
                    nStatus = 0;
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(PMC_STATUS.PMCVacuumStatus, ref nStatus) == 2)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.FastPumpChangeReq, true);
                        GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.FastPumpOpenClose, true);
                    }
                    timeVtmPump.Restart();
                    break;

                case 50:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(PMC_STATUS.PMCVacuumStatus, ref nStatus) == 2)
                    {
                        if (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.FastPumpStatus) == 1)
                        {
                            GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.FastPumpChangeReq, false);
                            break;
                        }
                        else
                        {
                            //타임아웃 체크
                            if (timeVtmPump.ElapsedMilliseconds > lTimeOut)
                            {
                                GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.FastPumpChangeReq, true);

                                GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.FastPumpOpenClose, false);
                                Thread.Sleep(500);
                                GlobalSeq.autoRun.prcVTM.Pmc.SetInterlock(CTC_INTERLOCK.FastPumpChangeReq, false);
                                return fn.err;
                            }
                        }

                    }
                    else
                    {
                        break;
                    }
                    
                    return fn.busy;

                case 60:
                    //동작 완료
                    nSeqVacuum[nSeq] = 0;
                    return fn.success;
            }
            nSeqVacuum[nSeq]++;
            return fn.busy;
        }

        public fn VTM_Venting()
        {
            int nSeq = (int)Vacuum_Seq.VTM_Venting;

            switch (nSeqVacuum[nSeq])
            {
                case 0:
                    //Venting Status Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTM_ATMSwtich1) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_1) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_2) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_TmMainPump_ValueOpen) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_TmMainPump_ValueClose) == true)
                    {
                        return fn.success;
                    }
                    else
                    {
                        break;
                    }

                case 5:
                    //Interlock Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_SlowVentGN2_PressureSwitch_1) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_BonderGate_OpenStatus) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_BonderGate_CloseStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadlockTmGate_OpenStatus) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadlockTmGate_CloseStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_ChamberTopLID_OpenStatus) == true)
                    {
                        break;
                    }
                    else
                    {
                        //VTM Vent Interlock Err
                        return fn.err;
                    }

                case 10:
                    //All Valve Close
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VacuumTMSlowPump_VV, false);
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VacuumTmmainPump_VV, false);
                    timeVtmVent.Restart();
                    break;

                case 15:
                    //Pumping V/v Close Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_TmMainPump_ValueOpen) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_TmMainPump_ValueClose) == true)
                    {
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeVtmVent.ElapsedMilliseconds > lTimeOut)
                        {
                            //VTM Vent Pumping VV Close Timeout Err
                            return fn.err;
                        }
                    }
                    return fn.busy;

                case 20:
                    //Vent VV Open
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VacuumTmVent_VV, true);
                    timeVtmVent.Restart();
                    break;

                case 25:
                    //Convectron Sensor 2 Off Check
                    //Convectron Sensor 1 Off Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_2) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_1) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTM_ATMSwtich1) == true)
                    {
                        break;
                    } 
                    else
                    {
                        //타임아웃 체크
                        if (timeVtmVent.ElapsedMilliseconds > lTime_VTM_VentConRy2)
                        {
                            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VacuumTmVent_VV, false);
                            //VTM Vent ConRy2 TimeOut
                            return fn.err;
                        }

                        //타임아웃 체크
                        if (timeVtmVent.ElapsedMilliseconds > lTime_VTM_VentConRy1)
                        {
                            //VTM Vent ConRy1 TimeOut
                            return fn.err;
                        }
                    }
                    return fn.busy;

                case 30:

                    //if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_1) == true
                    //    && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTM_ATMSwtich1) == true)
                    //{
                    //    break;
                    //}
                    //else
                    //{
                    //    //타임아웃
                    //}
                    //return fn.busy;

                    break;
                case 35:
                    //Vent VV Close
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VacuumTmVent_VV, false);
                    break;

                case 40:
                    //Config Time 대기 후 완료
                    Thread.Sleep(1000); //타임아웃 연결
                    break;

                case 50:
                    //동작 완료
                    nSeqVacuum[nSeq] = 0;
                    return fn.success;
            }
            nSeqVacuum[nSeq]++;
            return fn.busy;
        }

        public fn Loadlock_Pumping()
        {
            int nSeq = (int)Vacuum_Seq.Loadlock_Pumping;

            switch (nSeqVacuum[nSeq])
            {
                case 0:
                    //Pumping Status Check

                    //Convectron Sensor On Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_1) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_2) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockMainPUMP_ValveOpen) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockMainPUMP_ValveClose) == false)
                    {
                        //위 조건 만족시 마지막 스탭으로 이동
                        return fn.success;
                    }
                    else
                    {
                        break;
                    }

                case 5:
                    //Interlock Check
                    //Load lock Door Close
                    //Loadlock Gate Close
                    //L/L Cover Close
                    //L/L ATM S/W 상태
                    //L/L 진공 V/v 상태
                    //L/L Dry Pump Alarm 상태 Check

                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockSlotValve_OpenStatus) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockSlotValve_CloseStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadlockTmGate_OpenStatus) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadlockTmGate_CloseStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockTopLID_OpenStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLock_ATMSwtich_1) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockMainPUMP_ValveOpen) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockMainPUMP_ValveClose) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockDryPump_AlarmStatus) == false)
                    {
                        break;
                    }
                    else
                    {
                        //Loadlock Pumping Interlock Error
                        return fn.err;
                    }

                case 10:

                    //Loadlok Vent V/V Close
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockVent_VV, false);
                    break;

                case 15:
                    //L/L Dry Pump On
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockDry_pumpOn, true);
                    Thread.Sleep(1000);
                    timeLLPump.Restart();
                    break;

                case 20:
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockDryPump_RunStatus) == true)
                    {
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeLLPump.ElapsedMilliseconds > lTimeOut)
                        {
                            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockDry_pumpOn, false);
                            //Loadlock DryPump RunStatus On TimeOut Error
                            return fn.err;
                        }
                    }
                    return fn.busy;

                case 25:              
                    //L/L Slow Pump VV Open
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockSlowPumping_VV, true);
                    Thread.Sleep(500);
                    timeLLPump.Restart();
                    break;

                case 30:
                    break;

                case 35:
                    //Low Pump Check
                    //ATM Switch Sensor OFF Check
                    //Convectron Sensor 1 On Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLock_ATMSwtich_1) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockConvectronRy_1) == true)
                    {
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeLLPump.ElapsedMilliseconds > lTime_LL_PumpConRy1)
                        {
                            //Output Off
                            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockDry_pumpOn, false);
                            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockSlowPumping_VV, false);

                            //lTime_LL_PumpConRy1 TimeOut
                            return fn.err;
                        }
                    }
                    return fn.busy;

                case 40:
                    //L/L Slow Pump VV Open
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockFastPumping_VV, true);
                    Thread.Sleep(500);
                    timeLLPump.Restart();
                    break;

                case 45:
                    //Pumping VV Open Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockMainPUMP_ValveOpen) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockMainPUMP_ValveClose) == false)
                    {
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeLLPump.ElapsedMilliseconds > lTimeOut)
                        {
                            //Output Off
                            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockDry_pumpOn, false);
                            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockSlowPumping_VV, false);
                            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockFastPumping_VV, false);

                            //timeLLPump TimeOut
                            return fn.err;
                        }
                    }
                    return fn.busy;

                case 50:
                    //Slow Pump VV Close
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockSlowPumping_VV, false);
                    timeLLPump.Restart();
                    break;

                case 55:                   
                    break;

                case 60:
                    //High Pump Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockConvectrionRy_2) == true)
                    {
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeLLPump.ElapsedMilliseconds > lTime_LL_PumpConRy2)
                        {
                            //lTime_LL_PumpConRy2 TimeOut
                            return fn.err;
                        }
                    }
                    return fn.busy;

                case 65:
                    //Pumping Time Wait(Config에서 관리)
                    //어떻게 처리 할지 ?
                    break;

                case 100:
                    //동작 완료
                    nSeqVacuum[nSeq] = 0;
                    return fn.success;
            }
            nSeqVacuum[nSeq]++;
            return fn.busy;
        }

        public fn Loadlock_Venting()
        {
            int nSeq = (int)Vacuum_Seq.Loadlock_Venting;

            switch (nSeqVacuum[nSeq])
            {
                case 0:
                    //Venting Status Check

                    //Convectron Sensor On Check
                    //L/L ATM S/W 상태
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockConvectronRy_1) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockConvectronRy_1) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLock_ATMSwtich_1) == true)
                    {
                        //위 조건 만족시 마지막 스탭으로 이동
                        return fn.busy;
                    }
                    else
                    {
                        break;
                    }
                   
                case 5:

                    //Interlock Check

                    //Load lock Door Close
                    //Loadlock Gate Close
                    //L/L Cover Close
                    //L/L ATM S/W 상태
                    //L/L 진공 V/v 상태
                    //GN2 Pressure On Check

                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockSlotValve_OpenStatus) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockSlotValve_CloseStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadlockTmGate_OpenStatus) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadlockTmGate_CloseStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockTopLID_OpenStatus) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLock_ATMSwtich_1) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockMainPUMP_ValveOpen) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockMainPUMP_ValveClose) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_SlowVentGN2_PressureSwitch_1) == false)
                    {
                        break;
                    }
                    else
                    {
                        //Loadlock Vent Interlock Error
                        return fn.err;
                    }

                case 10:
                    //Loadlok Pump V/V Close
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockSlowPumping_VV, false);
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockFastPumping_VV, false);
                    timeLLVent.Restart();
                    break;

                case 15:
                    //Pumping V/ v Close Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockMainPUMP_ValveOpen) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockMainPUMP_ValveClose) == false)
                    {
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeLLVent.ElapsedMilliseconds > lTimeOut)
                        {
                            //Loadlock Pump Close TimeOut Error
                            return fn.err;
                        }
                    }
                    break;

                case 20:
                    //L/L Vent VV Open
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockVent_VV, true);
                    timeLLVent.Restart();
                    break;

                case 25:
                    //Vent Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLock_ATMSwtich_1) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockConvectronRy_1) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockConvectrionRy_2) == false)
                    {
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeLLVent.ElapsedMilliseconds > lTime_LL_VentOpen)
                        {
                            //Output Off
                            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockVent_VV, false);

                            //Loadlock Vent Open TimeOut
                            return fn.err;
                        }
                    }
                    break;

                case 30:

                    //L/L Vent VV Close
                    GlobalVariable.io.WriteOutput(GlobalVariable.io.O_LoadLockVent_VV, false);
                    timeLLVent.Restart();
                    break;

                case 35:

                    //Vent VV Close & Vent Check
                    if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLock_ATMSwtich_1) == true
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockConvectronRy_1) == false
                        && GlobalVariable.io.ReadInput(GlobalVariable.io.I_LoadLockConvectrionRy_2) == false)
                    {
                        break;
                    }
                    else
                    {
                        //타임아웃 체크
                        if (timeLLVent.ElapsedMilliseconds > lTime_LL_VentClose)
                        {
                            //Loadlock Vent Close TimeOut
                            return fn.err;
                        }
                    }
                    break;

                case 40:

                    //Vent Time Wait(Config에서 관리) 어떻게 ?
                    break;

                case 45:
                    break;

                case 50:
                    //동작 완료
                    nSeqVacuum[nSeq] = 0;
                    return fn.success;
            }
            nSeqVacuum[nSeq]++;
            return fn.busy;
        }
    }
}
