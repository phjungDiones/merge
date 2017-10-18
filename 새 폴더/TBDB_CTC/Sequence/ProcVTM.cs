using CJ_Controls.Communication.QuadraVTM4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_CTC.Sequence;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;

namespace TBDB_Handler.SEQ
{
    public enum CaseVTM
    {
        Initialze,
        Check_Interlock,
        Check_Status,

        //VTM Pickup -> LL
        Check_WaferStatus,

        Check_LL_Pickup,
        Move_LL_Move_Pickup,
        Compl_LL_Move_Pickup,

        Start_VTM_Pickup_LL,
        Move_VTM_Pickup_LL,
        Move_VTM_Pickup_LL_Stretch,
        Move_VTM_Pickup_LL_Stretch_Check,
        Move_VTM_Pickup_LL_HandUp,
        Move_VTM_Pickup_LL_HandUp_Check,
        Move_VTM_Pickup_LL_Fold,
        Move_VTM_Pickup_LL_Fold_Check,
        Compl_VTM_Pickup_LL,
        End_VTM_Pickup_LL,

        //VTM Load -> PMC
        Start_VTM_Load_PMC,
        Move_VTM_Load_PMC,
        Compl_VTM_Load_PMC,
        End_VTM_Load_PMC,

        //Set Process
        Set_Recipe,
        Start_Process,

        //VTM Unload -> PMC
        Start_VTM_Unload_PMC,
        Move_VTM_Unload_PMC,
        Compl_VTM_Unload_PMC,
        End_VTM_Unload_PMC,

        //Unload LL
        Check_LL_Place,
        Move_LL_Move_Place,
        Compl_LL_Move_Place,

        Start_VTM_Place_LL,
        Move_VTM_Place_LL,
        Move_VTM_Place_LL_Stretch,
        Move_VTM_Place_LL_Stretch_Check,
        Move_VTM_Place_LL_HandDown,
        Move_VTM_Place_LL_HandDown_Check,
        Move_VTM_Place_LL_Fold,
        Move_VTM_Place_LL_Fold_Check,
        Compl_VTM_Place_LL,
        End_VTM_Place_LL,
    }

    public enum VtmStage
    {
        Bonded = 1,
        LL,
        PMC1_ULD = 4,
        PMC1_LD,
        PM2_LD,
        PMC2_ULD,
    }

    public enum Seq_PMC
    {
        PMC_Load = 0,
        PMC_Unload,
        PMC_Init,
        PMC_Standby,
        PMC_Process,    
        PMC_Max,
    }

    public enum WaferLoad
    {
        LL1,
        LL2,
        BD,
        PMC
    }

    public enum ReqStatus_VTM
    {
        NONE = 0,
        LOAD_PMC,
        UNLOAD_PMC,
        EXCHANGE_PMC,
        LOADLOCK_LOAD,        
        MAX,
    }

    public enum ArmLoad
    {
        NONE,
        LOWER,
        UPPER,
        FULL,
        MAX,
    }
    
    public class ProcVTM 
    {
        public MotionVtm VtmRobot = new MotionVtm();
        public MotionPmc Pmc = new MotionPmc();
       
        private bool isCycleRun = false;
        public int nSeqNo = 0;
        private int nPreSeqNo = 0;

        public delegate void procAddMsgEvent(int nSeq, int nCaseNo, string strAddMsg);
        public event procAddMsgEvent procMsgEvent;

        public delegate void procVTMErrorStopEvent(int nErrSafty);
        public event procVTMErrorStopEvent errSaftyStopEvent;

        public PMC_RECIPE pmc_Rcp = new PMC_RECIPE(); //레시피 연결하자

        int nSeq = (int)UNIT.VTM_ROBOT;
        //매뉴얼 Seq Case
        int[] nSeq_PMC = new int[(int)Seq_PMC.PMC_Max];

        VtmStage Working_Stage = VtmStage.Bonded;
        ARM Working_Arm = ARM.UPPER;

        string strLog = "";

        ReqStatus_VTM ReqStatus = ReqStatus_VTM.NONE;
        ArmLoad ArmStatus = ArmLoad.NONE;
        ProcInfoBond PmcInfo;

        public ProcVTM()
        {
            pmc_Rcp.bUseVision = true;
            pmc_Rcp.dPressure = 400;
            pmc_Rcp.dPressingTime = 1000;
            pmc_Rcp.dUpperTemp = 1700;
            pmc_Rcp.dLowerTemp = 1000;
            pmc_Rcp.dAPC_Pos = 1000;
            pmc_Rcp.dCh_1 = 640;
            pmc_Rcp.dCh_2 = 560;
            pmc_Rcp.dCh_3 = 800;
        }


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

        private void nextSeq(CaseVTM nSeq)
        {
/*            rLib.resetCmd();*/
            if (nSeq >= 0) { nSeqNo = (int)nSeq; }
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



        public void Run()
        {
            if (!isAcceptRun()) { return; }
            if (nSeqNo != nPreSeqNo) { resetCmd(); }
            nPreSeqNo = nSeqNo;

            CaseVTM seqCase = (CaseVTM)nSeqNo;

            alwaysCheck();

            switch (seqCase)
            {
                case CaseVTM.Initialze:
                    break;

                case CaseVTM.Check_Interlock:
                    break;

                case CaseVTM.Check_Status:
                    
                    //웨이퍼 로딩 상태 확인
                    ReqStatus = CheckStatusReqeust();
                    if(ReqStatus == ReqStatus_VTM.LOADLOCK_LOAD)
                    {
                        //Pickup Loadlock
                        Working_Arm = ARM.UPPER;
                        nextSeq(CaseVTM.Check_LL_Pickup);
                    }
                    else if (ReqStatus == ReqStatus_VTM.LOAD_PMC)
                    {
                        //Load PMC Seq
                        Working_Arm = ARM.UPPER;
                        nextSeq(CaseVTM.Start_VTM_Load_PMC);                      
                    }
                    else if (ReqStatus == ReqStatus_VTM.UNLOAD_PMC)
                    {
                        //Unload Seq
                        Working_Arm = ARM.LOWER;
                        nextSeq(CaseVTM.Start_VTM_Unload_PMC);
                    }
                    else
                    {
                        //요청이 없을 경우 대기하자
                    }
                    return;

                /////////////////////////////////
                //VTM Pickup LL -> Load PMC
                /////////////////////////////////

                case CaseVTM.Check_WaferStatus:
                    break;


                case CaseVTM.Check_LL_Pickup:
                    //Loadlock이 동작 중인지 확인
                    if (GlobalVariable.interlock.bLLMoving) return;
                    GlobalVariable.interlock.bLLMoving = true;
                    break;

                case CaseVTM.Move_LL_Move_Pickup:
                    //Pickup LL
                    GlobalSeq.autoRun.procLoadlock.Move(MotionPos.Pos3);

                    strLog = string.Format("Loadlock VTM Pickup Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseVTM.Compl_LL_Move_Pickup:
                    if (GlobalSeq.autoRun.procLoadlock.CheckComplete() != fn.success) return;

                    strLog = string.Format("Loadlock VTM Pickup Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;


                case CaseVTM.Start_VTM_Pickup_LL:
                    break;

                case CaseVTM.Move_VTM_Pickup_LL:

                    Working_Stage = VtmStage.LL;
                    Working_Arm = ARM.UPPER;

                    //VTM Pickup/Place 시 부분동작으로 변경 함 
                    MoveRobot(Working_Stage, Working_Arm, true); //Arm Fold 이동
                    
                    strLog = string.Format("VTM Robot Pickup Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;
                  
                case CaseVTM.Move_VTM_Pickup_LL_Stretch:
                    MoveRobot_Stretch(Working_Stage, Working_Arm, UPDOWN_POS.DOWN);
                    break;

                case CaseVTM.Move_VTM_Pickup_LL_Stretch_Check:
                    if (CheckComplete() != fn.success) return;
                    break;

                case CaseVTM.Move_VTM_Pickup_LL_HandUp:
                    MoveRobot_HandUp(Working_Stage, Working_Arm);
                    break;

                case CaseVTM.Move_VTM_Pickup_LL_HandUp_Check:
                    if (CheckComplete() != fn.success) return;
                    break;

                case CaseVTM.Move_VTM_Pickup_LL_Fold:
                    MoveRobot_Fold(Working_Stage, Working_Arm, UPDOWN_POS.UP);
                    break;

                case CaseVTM.Move_VTM_Pickup_LL_Fold_Check:
                    if (CheckComplete() != fn.success) return;
                    break;
        
                case CaseVTM.Compl_VTM_Pickup_LL:

                    strLog = string.Format("VTM Robot Pickup Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseVTM.End_VTM_Pickup_LL:               
                    GlobalVariable.interlock.bLLMoving = false;
                    GlobalVariable.seqShared.LoadingLoadLockToVtm(WaferType.CARRIER, (HAND)Working_Arm);
                    //GlobalVariable.seqShared.LoadingLoadLockToVtm(WaferType.DEVICE, (HAND)Working_Arm);

                    //Loadlock 픽업 후 리턴
                    nextSeq(CaseVTM.Check_Interlock);
                    break;

                /////////////////////////////////
                //VTM Load PMC
                /////////////////////////////////
                case CaseVTM.Start_VTM_Load_PMC:
                    break;

                case CaseVTM.Move_VTM_Load_PMC:
                    if (Load_PMC(Working_Arm) != fn.success) return;
                    break;

                case CaseVTM.Compl_VTM_Load_PMC:
                    break;

                case CaseVTM.End_VTM_Load_PMC:
                    GlobalVariable.seqShared.LoadingVtmToBonder((HAND)Working_Arm);
                    break;

                case CaseVTM.Set_Recipe:

                    if (SetRecipe(pmc_Rcp) != fn.success) return;

                    strLog = string.Format("PMC Set Recipe -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseVTM.Start_Process:
                    if (Process_PMC() != fn.success) return;

                    strLog = string.Format("PMC Process End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);

                    nextSeq(CaseVTM.Check_Interlock);
                    return;  


                /////////////////////////////////
                //VTM Unload PMC
                /////////////////////////////////
                case CaseVTM.Start_VTM_Unload_PMC:

                    strLog = string.Format("VTM PMC Unload Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseVTM.Move_VTM_Unload_PMC:
                    Working_Arm = ARM.LOWER; //언로드 시 Lower Hand
                    if (Unload_PMC(Working_Arm) != fn.success) return;

                    strLog = string.Format("VTM PMC Unload Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseVTM.Compl_VTM_Unload_PMC:
                    break;

                case CaseVTM.End_VTM_Unload_PMC:
                    
                    //데이터 이동
                    GlobalVariable.seqShared.LoadingBonderToVtm((HAND)Working_Arm);

                    if (ReqStatus == ReqStatus_VTM.EXCHANGE_PMC)
                    {
                        //웨이퍼 교체일 경우 언로드 후 다시 PMC로드 케이스로 보낸다.
                        nextSeq(CaseVTM.Start_VTM_Load_PMC);
                        return;
                    }
                    break;

                case CaseVTM.Check_LL_Place:
                    //Loadlock이 동작 중인지 확인
                    if (GlobalVariable.interlock.bLLMoving) return;
                    GlobalVariable.interlock.bLLMoving = true;
                    break;

                case CaseVTM.Move_LL_Move_Place:
                    //BD Place -> Unload
                    GlobalSeq.autoRun.procLoadlock.Move(MotionPos.Pos1);

                    strLog = string.Format("Loadlock VTM Place Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseVTM.Compl_LL_Move_Place:
                    if (GlobalSeq.autoRun.procLoadlock.CheckComplete() != fn.success) return;

                    strLog = string.Format("Loadlock VTM Place Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseVTM.Start_VTM_Place_LL:
                    if(Working_Arm != ARM.LOWER)
                    {
                        //Hand Fail
                        return;
                    }
                    break;

                case CaseVTM.Move_VTM_Place_LL:
                    Working_Stage = VtmStage.Bonded;
                    MoveRobot(Working_Stage, Working_Arm, true); //Hand Fold 이동

                    strLog = string.Format("VTM Robot Place Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseVTM.Move_VTM_Place_LL_Stretch:
                    MoveRobot_Stretch(Working_Stage, Working_Arm, UPDOWN_POS.UP);
                    break;

                case CaseVTM.Move_VTM_Place_LL_Stretch_Check:
                    if (CheckComplete() != fn.success) return;
                    break;

                case CaseVTM.Move_VTM_Place_LL_HandDown:
                    MoveRobot_HandDown(Working_Stage, Working_Arm);                   
                    break;

                case CaseVTM.Move_VTM_Place_LL_HandDown_Check:
                    if (CheckComplete() != fn.success) return;
                    break;

                case CaseVTM.Move_VTM_Place_LL_Fold:
                    MoveRobot_Fold(Working_Stage, Working_Arm, UPDOWN_POS.DOWN);
                    break;

                case CaseVTM.Move_VTM_Place_LL_Fold_Check:
                    if (CheckComplete() != fn.success) return;
                    break;

                case CaseVTM.Compl_VTM_Place_LL:                   
                    strLog = string.Format("VTM Robot Place Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseVTM.End_VTM_Place_LL:
                    //데이터 이동
                    GlobalVariable.seqShared.LoadingVtmToLoadlock(WaferType.BONDED, (HAND)Working_Arm);
                    GlobalVariable.interlock.bLLMoving = false;
                    nextSeq(CaseVTM.Check_Interlock);
                    return;
            }
            nSeqNo++;
        }


        public void MoveRobot(VtmStage stage, ARM _Arm, bool bHandFold = false)
        {
            int nSlot = 1;

            if (bHandFold == true)
            {
                VtmRobot.MoveStage((int)stage, nSlot, _Arm, RADIAL_POS.RETRACTED, UPDOWN_POS.DOWN);
            }
            else
            {
                if (stage == VtmStage.LL || stage == VtmStage.PMC1_ULD)
                {
                    VtmRobot.MovePickup((int)stage, nSlot, _Arm);   
                }
                else
                {
                    VtmRobot.MovePlace((int)stage, nSlot, _Arm);
                }    
            }   
        }

        public void MoveRobot_Fold(VtmStage stage, ARM _Arm, UPDOWN_POS _UpDnPos)
        {
            int nSlot = 1;
            VtmRobot.MoveRetract((int)stage, nSlot, _Arm, _UpDnPos);  
        }

        public void MoveRobot_Stretch(VtmStage stage, ARM _Arm, UPDOWN_POS _UpDnPos)
        {
            int nSlot = 1;
            VtmRobot.MoveExtend((int)stage, nSlot, _Arm, _UpDnPos);
        }

        public void MoveRobot_HandUp(VtmStage stage, ARM _Arm )
        {
            int nSlot = 1;
            VtmRobot.MoveZAxisUp((int)stage, nSlot, _Arm);
        }

        public void MoveRobot_HandDown(VtmStage stage, ARM _Arm)
        {
            int nSlot = 1;
            VtmRobot.MoveZAxisDown((int)stage, nSlot, _Arm);
        }


        public fn CheckComplete()
        {
            if( VtmRobot.CheckComplete() )
                return fn.success;
            else if (VtmRobot.IsAlarmCheck())
                return fn.err;
            else
            {
                //타임 아웃 , 에러 체크
                return fn.busy;
            }
        }

        public bool CheckStatusWafer(ARM _Arm)
        {
            return VtmRobot.IsWaferCheck(_Arm);
        }

        public ReqStatus_VTM CheckStatusReqeust()
        {
            ReqStatus_VTM Status;

#if !_REAL_MC

            if (GlobalVariable.seqShared.IsInLoadLock((int)WaferType.DEVICE) == true
                && GlobalVariable.seqShared.IsInLoadLock((int)WaferType.CARRIER) == true
                && GlobalVariable.seqShared.IsInVTM(HAND.UPPER) == false)
            {
                //Upper Arm에 웨이퍼가 없고 loadlock에 웨이퍼 로드 상태일 경우 
                //Loadlock Pickup
                Status = ReqStatus_VTM.LOADLOCK_LOAD;
            }
            else if (GlobalVariable.WaferInfo.bPmcUnload
                && GlobalVariable.seqShared.IsInLoadLock((int)WaferType.BONDED) == false
                && GlobalVariable.seqShared.IsInBonder() == true
                && GlobalVariable.seqShared.IsInVTM(HAND.LOWER) == false)
            {
                //Wafer Unload Req
                Status = ReqStatus_VTM.UNLOAD_PMC;
            }
            else if (GlobalVariable.WaferInfo.bPmcLoad
                && GlobalVariable.seqShared.IsInBonder() == false
                && GlobalVariable.seqShared.IsInVTM(HAND.UPPER) == true)
            {
                //Upper Arm에 웨이퍼 로드 상태일경우 
                Status = ReqStatus_VTM.LOAD_PMC;
            }
            else
            {
                //요청이 없을 경우
                Status = ReqStatus_VTM.NONE;
            }

            return Status;
#endif

            if(GlobalVariable.seqShared.IsInLoadLock((int)WaferType.DEVICE) == true
                && GlobalVariable.seqShared.IsInLoadLock((int)WaferType.CARRIER) == true
                && GlobalVariable.seqShared.IsInVTM(HAND.UPPER) == false)
            {
                //Upper Arm에 웨이퍼가 없고 loadlock에 웨이퍼 로드 상태일 경우 
                //Loadlock Pickup
                Status = ReqStatus_VTM.LOADLOCK_LOAD;
            }
            else if (Pmc.GetPIO(PMC_PIO.SendReq) == 1 
                && GlobalVariable.seqShared.IsInLoadLock((int)WaferType.BONDED) == false
                && GlobalVariable.seqShared.IsInBonder() == true
                && GlobalVariable.seqShared.IsInVTM(HAND.UPPER) == false)
            {
                //Wafer Unload Req
                Status = ReqStatus_VTM.UNLOAD_PMC;
            }
            else if(Pmc.GetPIO(PMC_PIO.RecvReq) == 1
                && GlobalVariable.seqShared.IsInBonder() == false
                && GlobalVariable.seqShared.IsInVTM(HAND.UPPER) == true)
            {
                //Upper Arm에 웨이퍼 로드 상태일경우 
                Status = ReqStatus_VTM.LOAD_PMC;
            }
            else
            {
                //요청이 없을 경우
                Status = ReqStatus_VTM.NONE;
            }

            return Status;
        }

        //기존 시퀀스
        //        public ReqStatus_VTM CheckStatusReqeust()
        //        {
        //            ReqStatus_VTM Status;

        //#if !_REAL_MC

        //            if (GlobalVariable.WaferInfo.bPmcLoad
        //                && GlobalVariable.WaferInfo.bWaferLL1 == true
        //                && GlobalVariable.WaferInfo.bWaferLL2 == true
        //                && GlobalVariable.WaferInfo.bWaferPmc == false)
        //            {
        //                Status = ReqStatus_VTM.LOAD_PMC;
        //            }
        //            else if (GlobalVariable.WaferInfo.bPmcUnload
        //                && GlobalVariable.WaferInfo.bWaferBD == false
        //                && GlobalVariable.WaferInfo.bWaferPmc == true)
        //            {
        //                Status = ReqStatus_VTM.UNLOAD_PMC;
        //            }
        //            else
        //            {
        //                Status = ReqStatus_VTM.NONE;
        //            }

        //            return Status;
        //#endif

        //            if (Pmc.GetPIO(PMC_PIO.RecvReq) == 1
        //                && GlobalVariable.WaferInfo.bWaferLL1 == true
        //                && GlobalVariable.WaferInfo.bWaferLL2 == true)
        //            {
        //                Wafer Load Seq
        //                Status = ReqStatus_VTM.LOAD_PMC;
        //            }
        //            else if (Pmc.GetPIO(PMC_PIO.SendReq) == 1 && GlobalVariable.WaferInfo.bWaferBD == false)
        //            {
        //                Wafer Unload Req
        //                Status = ReqStatus_VTM.UNLOAD_PMC;
        //            }
        //            else
        //            {
        //                요청이 없을 경우
        //                Status = ReqStatus_VTM.NONE;
        //            }

        //            return Status;
        //        }


        #region Interface_Load

        public void InitLoadSignal()
        {
            Pmc.SetPIO(CTC_PIO.SendAble, false);
            Pmc.SetPIO(CTC_PIO.SendStart, false);
            Pmc.SetPIO(CTC_PIO.SendComplete, false);
            Pmc.SetPIO(CTC_PIO.SendFail, false);
            Pmc.SetVTMRobot(CTC_VTM_ROBOT.RobotMoving, false);
            Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandFold, false);
            Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandStretch, false);
            Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandDown, false);
            Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandUp, false);
        }

        public void InitUnloadSignal()
        {
            Pmc.SetPIO(CTC_PIO.RecvAble, false);
            Pmc.SetPIO(CTC_PIO.RecvStart, false);
            Pmc.SetPIO(CTC_PIO.RecvComplete, false);
            Pmc.SetPIO(CTC_PIO.RecvFail, false);
            Pmc.SetVTMRobot(CTC_VTM_ROBOT.RobotMoving, false);
            Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandFold, false);
            Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandStretch, false);
            Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandDown, false);
            Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandUp, false);
        }

        public fn  Load_PMC(ARM arm)
        {

#if !_REAL_MC
            return fn.success;
#endif

            int nSeq = (int)Seq_PMC.PMC_Load;

            switch (nSeq_PMC[nSeq])
            {
                case 0:
                    //전체 초기화
                    InitLoadSignal();
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandFold, true);
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandUp, true);
                    break;

                case 10:
                    if (Pmc.GetPIO(PMC_PIO.RecvReq) == 1)
                    {
                        // 웨이퍼 로드
                        Pmc.SetPIO(CTC_PIO.SendAble, true); //Send Able On
                        Pmc.SetPIO(CTC_PIO.SendStart, false);
                        Pmc.SetPIO(CTC_PIO.SendComplete, false);
                        Pmc.SetPIO(CTC_PIO.SendFail, false);
                        Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandFold, true);
                        Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandStretch, false);
                        Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandDown, false);
                        Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandUp, true);
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 20:
                    if (Pmc.GetPIO(PMC_PIO.RecvReady) == 1)
                    {
                        Pmc.SetPIO(CTC_PIO.SendStart, true); // Start On
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 30:
                    //ARM FOLD 후 로봇 이동 
                    MoveRobot(VtmStage.PMC1_LD, arm, true);
                    break;

                case 40:
                    if (CheckComplete() != fn.success) return fn.busy;
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandFold, false); //Hand fold off
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.RobotMoving, true); //Robot Moving On
                    break;

                case 50:
                    //ARM Stretch , Up
                    MoveRobot_Stretch(VtmStage.PMC1_LD, arm, UPDOWN_POS.UP);

                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandStretch, true); //Hand Stretch On
                    break;
                    
                case 60:
                    if (CheckComplete() != fn.success) return fn.busy;
                    break;

                case 70:
                    //Hand Down Pos
                    //MoveRobot_Stretch(VtmStage.PMC1_LD, arm, UPDOWN_POS.DOWN);
                    MoveRobot_HandDown(VtmStage.PMC1_LD, arm);
                    break;
                case 80:
                    if (CheckComplete() != fn.success) return fn.busy;
                    break;

                case 90:
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandUp, false); //Hand Up Off
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandDown, true); //Hand Down On
                    break;

                case 100:
                    // MSP Down Check
                    if (Pmc.GetMotor(PMC_MOTOR.MSPDown) == 1)
                    {
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    return fn.busy;

                case 110:
                    //MSP Up & Pin Up 확인
                    if (Pmc.GetMotor(PMC_MOTOR.MSPUp) == 1 && Pmc.GetMotor(PMC_MOTOR.PinUp) == 1)
                    {
                        MoveRobot_HandUp(VtmStage.PMC1_LD, arm); //Hand Up
                        break;
                    }
                    else
                    {
                         //타임 아웃
                    }
                    return fn.busy;
                case 115:
                    if (CheckComplete() != fn.success) return fn.busy;
                    break;

                case 120:
                    //Hand Fold 동작
                    MoveRobot_Fold(VtmStage.PMC1_LD, arm, UPDOWN_POS.DOWN);
                    break;

                case 130:
                    if (CheckComplete() != fn.success) return fn.busy;
                    break;

                case 140:                
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.RobotMoving, false); //Robot Moving Off
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandFold, true);
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandStretch, false);

                    //Complete 전 Start 신호 Off
                    Pmc.SetPIO(CTC_PIO.SendAble, false);
                    Pmc.SetPIO(CTC_PIO.SendStart, false);
                    Pmc.SetPIO(CTC_PIO.SendComplete, true); //Send Compl On
                    break;

                case 150:
                    if (Pmc.GetPIO(PMC_PIO.RecvComplAck) == 1)
                    {
                        Pmc.SetPIO(CTC_PIO.SendComplete, false); //Send Compl Off
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    return fn.busy;                  

                case 160:
                    if (Pmc.GetPIO(PMC_PIO.RecvComplAck) == 0)
                    {
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    break;
                case 170:
                    //동작 완료
                    nSeq_PMC[nSeq] = 0;
                    return fn.success;
            }

            nSeq_PMC[nSeq]++;
            return fn.busy;
        }

        public fn Unload_PMC(ARM arm)
        {

#if !_REAL_MC
            return fn.success;
#endif
            int nSeq = (int)Seq_PMC.PMC_Unload;

            switch (nSeq_PMC[nSeq])
            {
                case 0:
                    InitUnloadSignal();
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandFold, true);
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandDown, true);
                    break;

                case 10:
                    if (Pmc.GetPIO(PMC_PIO.SendReq) == 1)
                    {
                        //초기값 setting
                        Pmc.SetPIO(CTC_PIO.RecvStart, false);
                        Pmc.SetPIO(CTC_PIO.SendComplete, false);
                        Pmc.SetVTMRobot(CTC_VTM_ROBOT.RobotMoving, false);
                        Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandStretch, false);
                        Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandUp, false);

                        Pmc.SetPIO(CTC_PIO.RecvAble, true); //Recive Able On
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 20:
                    if (Pmc.GetPIO(PMC_PIO.SendReady) == 1)
                    {                    
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 30:
                    //MSP Up & Pin Up 상태 확인
                    if (Pmc.GetMotor(PMC_MOTOR.MSPUp) == 1 && Pmc.GetMotor(PMC_MOTOR.PinUp) == 1)
                    {
                        Pmc.SetPIO(CTC_PIO.RecvStart, true); // Start On
                        Pmc.SetVTMRobot(CTC_VTM_ROBOT.RobotMoving, true);
                        Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandFold, false);
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 40:
                    //로봇 이동 후 Stretch
                    //Hand Down Pos
                    MoveRobot_Stretch(VtmStage.PMC1_ULD, arm, UPDOWN_POS.DOWN);
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandStretch, true); //Stretch On

                    //Pickup
                    //GlobalSeq.autoRun.prcVTM.MoveRobot(VtmStage.PMC1_ULD, arm);
                    break;

                case 50:
                    if (CheckComplete() != fn.success) return fn.busy;
                    break;

                case 60:
                    //Hand Up
                    MoveRobot_HandUp(VtmStage.PMC1_ULD, arm);
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandDown, false); //Hand Down off
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandUp, true); // Hnad Up On
                    break;

                case 70:
                    if (CheckComplete() != fn.success) return fn.busy;
                    break;

                case 80:
                    //여기서 확인 필요!! 문서에는 PIN 상태를 확인 필요한지 ?

                    //Arm Fold
                    MoveRobot_Fold(VtmStage.PMC1_ULD, arm, UPDOWN_POS.UP);
                    break;

                case 90:
                    if (CheckComplete() != fn.success) return fn.busy;
                    break;

                case 100:
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandStretch, false);
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.HandFold, true);
                    Pmc.SetVTMRobot(CTC_VTM_ROBOT.RobotMoving, false);

                    Pmc.SetPIO(CTC_PIO.RecvStart, false);
                    Pmc.SetPIO(CTC_PIO.SendComplete, false);
                    Pmc.SetPIO(CTC_PIO.RecvComplete, true); //Recive Compl On
                    break;

                case 110:
                    if (Pmc.GetPIO(PMC_PIO.SendComplAck) == 1)
                    {
                        Pmc.SetPIO(CTC_PIO.RecvComplete, false); //Recive Compl Off
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 120:
                    if (Pmc.GetPIO(PMC_PIO.SendComplAck) == 0)
                    {
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 170:
                    //동작 완료
                    nSeq_PMC[nSeq] = 0;
                    return fn.success;
            }

            nSeq_PMC[nSeq]++;
            return fn.busy;
        }

        #endregion

        #region Interface_Manual

        public fn Init_PMC()
        {
            int nSeq = (int)Seq_PMC.PMC_Init;

            switch (nSeq_PMC[nSeq])
            {
                case 0:
                    break;

                case 10:
                    Pmc.SetManualCmd(CTC_MANUAL.InitReady, false);
                    Pmc.SetManualCmd(CTC_MANUAL.InitCompleteAck, false);
                    Pmc.SetManualCmd(CTC_MANUAL.InitReq, true); //Init Req On
                    return fn.busy;

                case 20:
                    if (Pmc.GetManualCmd(PMC_MANUAL.InitAck) == 1)
                    {
                        Pmc.SetManualCmd(CTC_MANUAL.InitReady, true); //Ready On
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 30:
                    if (Pmc.GetManualCmd(PMC_MANUAL.InitStart) == 1)
                    {
                        break;
                    }
                    else
                    { 
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 40:
                    if (Pmc.GetManualCmd(PMC_MANUAL.InitCompl) == 1
                        && Pmc.GetManualCmd(PMC_MANUAL.InitAck) == 0
                        && Pmc.GetManualCmd(PMC_MANUAL.InitStart) == 0 )
                    {
                        Pmc.SetManualCmd(CTC_MANUAL.InitReq, false); //Init Req Off
                        Pmc.SetManualCmd(CTC_MANUAL.InitReady, false); //Ready Off
                        Pmc.SetManualCmd(CTC_MANUAL.InitCompleteAck, true); //Compl Ack On
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 50:
                    if (Pmc.GetManualCmd(PMC_MANUAL.InitCompl) == 0)
                    {
                        Pmc.SetManualCmd(CTC_MANUAL.InitCompleteAck, false); //Compl Ack Off
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 60:
                    break;


                case 70:
                    //동작 완료
                    return fn.success;
            }

            nSeq_PMC[nSeq]++;
            return fn.busy;
        }
        
        public fn Standby_PMC()
        {
            int nSeq = (int)Seq_PMC.PMC_Standby;

            switch (nSeq_PMC[nSeq])
            {
                case 0:
                    break;

                case 10:
                    Pmc.SetManualCmd(CTC_MANUAL.StandbyReady, false);
                    Pmc.SetManualCmd(CTC_MANUAL.StandbyCompleteAck, false);
                    Pmc.SetManualCmd(CTC_MANUAL.StandbyReq, true);
                    return fn.busy;

                case 20:
                    if (Pmc.GetManualCmd(PMC_MANUAL.StandbyAck) == 1)
                    {
                        Pmc.SetManualCmd(CTC_MANUAL.StandbyReady, true); 
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 30:
                    if (Pmc.GetManualCmd(PMC_MANUAL.StandbyStart) == 1)
                    {
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 40:
                    if (Pmc.GetManualCmd(PMC_MANUAL.StandbyCompl) == 1
                        && Pmc.GetManualCmd(PMC_MANUAL.StandbyAck) == 0
                        && Pmc.GetManualCmd(PMC_MANUAL.StandbyStart) == 0)
                    {
                        Pmc.SetManualCmd(CTC_MANUAL.StandbyReq, false); 
                        Pmc.SetManualCmd(CTC_MANUAL.StandbyReady, false); //Ready Off
                        Pmc.SetManualCmd(CTC_MANUAL.StandbyCompleteAck, true); //Compl Ack On
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 50:
                    if (Pmc.GetManualCmd(PMC_MANUAL.StandbyCompl) == 0)
                    {
                        Pmc.SetManualCmd(CTC_MANUAL.StandbyCompleteAck, false); //Compl Ack Off
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 60:
                    break;


                case 70:
                    //동작 완료
                    return fn.success;
            }

            nSeq_PMC[nSeq]++;
            return fn.busy;
        }


        public fn Process_PMC()
        {
#if !_REAL_MC
            return fn.success;
#endif

            int nSeq = (int)Seq_PMC.PMC_Process;

            switch (nSeq_PMC[nSeq])
            {
                case 0:
                    break;

                case 10:
                    Pmc.SetManualCmd(CTC_MANUAL.ProcessReady, false);
                    Pmc.SetManualCmd(CTC_MANUAL.ProcessCompleteAck, false);
                    Pmc.SetManualCmd(CTC_MANUAL.ProcessReq, true);
                    return fn.busy;

                case 20:
                    if (Pmc.GetManualCmd(PMC_MANUAL.ProcAck) == 1)
                    {
                        Pmc.SetManualCmd(CTC_MANUAL.ProcessReady, true);
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 30:
                    if (Pmc.GetManualCmd(PMC_MANUAL.ProcStart) == 1)
                    {
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 40:
                    if (Pmc.GetManualCmd(PMC_MANUAL.ProcCompl) == 1
                        && Pmc.GetManualCmd(PMC_MANUAL.ProcAck) == 0
                        && Pmc.GetManualCmd(PMC_MANUAL.ProcStart) == 0)
                    {
                        Pmc.SetManualCmd(CTC_MANUAL.ProcessReq, false);
                        Pmc.SetManualCmd(CTC_MANUAL.ProcessReady, false); //Ready Off
                        Pmc.SetManualCmd(CTC_MANUAL.ProcessCompleteAck, true); //Compl Ack On
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 50:
                    if (Pmc.GetManualCmd(PMC_MANUAL.ProcCompl) == 0)
                    {
                        Pmc.SetManualCmd(CTC_MANUAL.ProcessCompleteAck, false); //Compl Ack Off
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 60:
                    break;


                case 70:
                    //동작 완료
                    return fn.success;
            }

            nSeq_PMC[nSeq]++;
            return fn.busy;
        }

        public fn SetRecipe(PMC_RECIPE pmc_Rcp)
        {
            //레시피 데이터 연결
            PmcInfo = ProcessMgr.Inst.TempPinfo.listProcBond.Single<ProcInfoBond>(p => p.strProcName == RecipeMgr.Inst.TempRcp.strBondCondition);
            if (PmcInfo == null) return fn.err;

            Pmc.SetRecipe(CTC_RECIPE.VisionUsed, Convert.ToInt16(PmcInfo.bVisionUse));
            Pmc.SetRecipe(CTC_RECIPE.Pressure, Convert.ToInt16(PmcInfo.dPressure));
            Pmc.SetRecipe(CTC_RECIPE.PressingTime, Convert.ToInt16(PmcInfo.dPressTimeSec));
            Pmc.SetRecipe(CTC_RECIPE.UpperTemp, Convert.ToInt16(PmcInfo.dUpperTemp));
            Pmc.SetRecipe(CTC_RECIPE.LowerTemp, Convert.ToInt16(PmcInfo.dLowerTemp));
            Pmc.SetRecipe(CTC_RECIPE.APCPosition, Convert.ToInt16(PmcInfo.dAPCPos));
            Pmc.SetRecipe(CTC_RECIPE.CH1Backlight, Convert.ToInt16(PmcInfo.dBacklightCH1));
            Pmc.SetRecipe(CTC_RECIPE.CH2Backlight, Convert.ToInt16(PmcInfo.dBacklightCH2));
            Pmc.SetRecipe(CTC_RECIPE.CH3Backlight, Convert.ToInt16(PmcInfo.dBacklightCH3));

            //Pmc.SetRecipe(CTC_RECIPE.VisionUsed, Convert.ToInt16(pmc_Rcp.bUseVision));
            //Pmc.SetRecipe(CTC_RECIPE.Pressure, Convert.ToInt16(pmc_Rcp.dPressure));
            //Pmc.SetRecipe(CTC_RECIPE.PressingTime, Convert.ToInt16(pmc_Rcp.dPressingTime));
            //Pmc.SetRecipe(CTC_RECIPE.UpperTemp, Convert.ToInt16(pmc_Rcp.dUpperTemp));
            //Pmc.SetRecipe(CTC_RECIPE.LowerTemp, Convert.ToInt16(pmc_Rcp.dLowerTemp));
            //Pmc.SetRecipe(CTC_RECIPE.APCPosition, Convert.ToInt16(pmc_Rcp.dAPC_Pos));
            //Pmc.SetRecipe(CTC_RECIPE.CH1Backlight, Convert.ToInt16(pmc_Rcp.dCh_1));
            //Pmc.SetRecipe(CTC_RECIPE.CH2Backlight, Convert.ToInt16(pmc_Rcp.dCh_2));
            //Pmc.SetRecipe(CTC_RECIPE.CH3Backlight, Convert.ToInt16(pmc_Rcp.dCh_3));

            return fn.success;
        }

        public fn SetRecipe_Standby(PMC_RECIPE pmc_Rcp)
        {
            //Pmc.SetRecipe(CTC_STANDBY.LowerTemp, Convert.ToInt16(pmc_Rcp.dStandby_LowTemp));
            //Pmc.SetRecipe(CTC_STANDBY.UpperTemp, Convert.ToInt16(pmc_Rcp.dStandby_UpTemp));

            return fn.success;
        }

        #endregion
    }
}
