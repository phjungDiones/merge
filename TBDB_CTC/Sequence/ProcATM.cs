using CJ_Controls.Communication.CybogRobot_HTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_CTC.Comm.Lami_PLC;
using TBDB_CTC.Sequence;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;

namespace TBDB_Handler.SEQ
{
    //public enum CaseATM
    //{
    //    Initialze,
    //    Check_Interlock,
    //    Check_Status,

    //    //TM Pickup -> AL
    //    Start_TM_Pickup_AL,
    //    Check_Vacuum_AL_Send,
    //    Check_Vacuum_AL_Recv,
    //    Move_TM_Pickup_AL,
    //    Compl_TM_Pickup_AL,
    //    End_TM_Pickup_AL,

    //    //Check Device
    //    Check_Deicve,

    //    //TM -> Lami Load
    //    Start_TM_Load_Lami,
    //    Move_TM_Load_Lami,
    //    Compl_TM_Load_Lami,
    //    End_TM_Load_Lami,

    //    //TM -> Lami Unload
    //    Start_TM_Unload_Lami,
    //    Move_TM_Unload_Lami,
    //    Compl_TM_Unload_Lami,
    //    End_TM_Unload_Lami,

    //    //LL Move
    //    Check_LL_Place,
    //    Move_LL_Move_Place,
    //    Compl_LL_Move_Place,

    //    //TM Place -> LL
    //    Start_TM_Place_LL,
    //    Move_TM_Place_LL,
    //    Compl_TM_Place_LL,
    //    End_TM_Place_LL,

    //    //LL Move
    //    Check_LL_Pickup,
    //    Move_LL_Move_Pickup,
    //    Compl_LL_Move_Pickup,

    //    //TM Pickup -> LL
    //    Start_TM_Pickup_LL,
    //    Move_TM_Pickup_LL,
    //    Compl_TM_Pickup_LL,
    //    End_TM_Pickup_LL,

    //    //TM Place -> CP
    //    Start_TM_Place_CP,
    //    Move_TM_Place_CP,
    //    Compl_TM_Place_CP,
    //    End_TM_Place_CP,
    //}


    public enum CaseATM
    {
        Initialze,
        Check_Interlock,
        Check_Status,



        //TM Pickup -> AL
        Start_TM_Pickup_AL,
        Check_Vacuum_AL_Send,
        Check_Vacuum_AL_Recv,
        Move_TM_Pickup_AL,
        Compl_TM_Pickup_AL,
        End_TM_Pickup_AL,

        //Check Device
        Check_Deicve,

        //TM -> Lami Load
        Start_TM_Load_Lami,
        Move_TM_Load_Lami,
        Compl_TM_Load_Lami,
        End_TM_Load_Lami,


        //TM -> Lami Unload
        Start_TM_Unload_Lami,
        Move_TM_Unload_Lami,
        Compl_TM_Unload_Lami,
        End_TM_Unload_Lami,


        //LL Move
        Check_LL_Place,
        Move_LL_Move_Place,
        Compl_LL_Move_Place,

        //TM Place -> LL
        Start_TM_Place_LL,
        Set_Venting_Place_LL,
        Door_Open_Place_LL,
        Check_Open_Place_LL,
        Move_TM_Place_LL,
        Compl_TM_Place_LL,
        Door_Close_Place_LL,
        Check_Close_Place_LL,
        End_TM_Place_LL,

        //LL Move
        Check_LL_Pickup,
        Move_LL_Move_Pickup,
        Compl_LL_Move_Pickup,

        //TM Pickup -> LL
        Start_TM_Pickup_LL,
        Set_Venting_Pickup_LL,
        Door_Open_Pickup_LL,
        Check_Open_Pickup_LL,
        Move_TM_Pickup_LL,
        Compl_TM_Pickup_LL,
        Door_Close_Pickup_LL,
        Check_Close_Pickup_LL,
        End_TM_Pickup_LL,

        //TM Place -> CP
        Start_TM_Place_CP,
        Move_TM_Place_CP,
        Compl_TM_Place_CP,
        End_TM_Place_CP,
    }


    public enum AtmStage
    {
        ALIGN = 1,
        CP1,
        CP2,
        CP3,        
        LAMI_ULD,
        LAMI_LD,
        LL1 = 8,
        LL2,
        BD,
        HP,
    }

    public enum ReqStatus_Lami
    {
        NONE = 0,
        LOAD,
        UNLOAD,
        EXCHANGE,
        MAX,
    }

    public enum Seq_Lami
    {
        Lami_Load = 0,
        Lami_Unload,
        Lami_Init,
        Lami_Proc_Start,
        Lami_Proc_Pause,
        Lami_Proc_Resume,
        Lami_Standby,
        Lami_RcpChange,
        Lami_Max,
    }

    public enum ReqStatus_ATM
    {
        NONE = 0,
        LOAD_AL_CARRIER,
        LOAD_AL_DEVICE,
        UNLOAD_CP,
        LOAD_LAMI,
        UNLOAD_LAMI,
        LOAD_LL1_DEVICE,
        LOAD_LL2_CARRIER,
        UNLOAD_LL_BONDED,
    }


    public enum CheckWafer
    {

    }

    public class ProcATM 
    {
        public MotionAtm AtmRobot = new MotionAtm();
        public MotionLaminator Laminator = new MotionLaminator();
        public MotionAlinger aligner = new MotionAlinger();

        private bool isCycleRun = false;
        public int nSeqNo = 0;
        private int nPreSeqNo = 0;

        public delegate void procAddMsgEvent(int nSeq, int nCaseNo, string strAddMsg);
        public event procAddMsgEvent procMsgEvent;

        public delegate void procATMErrorStopEvent(int nErrSafty);
        public event procATMErrorStopEvent errSaftyStopEvent;

        int nSeq =  (int)UNIT.ATM_ROBOT;
        int[] nSeq_Lami = new int[(int)Seq_Lami.Lami_Max];
        string strLog = "";

        AtmStage Working_Stage = AtmStage.ALIGN;
        ARM Working_Arm = ARM.UPPER;
        WaferType Working_Device = WaferType.CARRIER; //레시피랑 연결하자, 구조체 만들어서

        ReqStatus_ATM ReqStatus = ReqStatus_ATM.NONE;
        //Data 레시피랑 연결하자
        ProcInfoLami LamiInfo;

        fn fRet = fn.busy;
        short status = 0;

        int nCurLamiCount = 0;

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

        private void nextSeq(CaseATM nSeq)
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

            CaseATM seqCase = (CaseATM)nSeqNo;

            alwaysCheck();

            switch (seqCase)
            {

                case CaseATM.Initialze:
                    //AddMessage("Test");
                    break;

                case CaseATM.Check_Interlock:
                    break;

                case CaseATM.Check_Status:

                    //현재 상태를 확인하자
                    ReqStatus = CheckReqStatus();                   
                    if (ReqStatus == ReqStatus_ATM.LOAD_AL_CARRIER)
                    {
                        //Upper Arm -> AL Carrier
                        Working_Arm = ARM.UPPER;
                        nextSeq(CaseATM.Move_TM_Pickup_AL);
                    }
                    if (ReqStatus == ReqStatus_ATM.LOAD_AL_DEVICE)
                    {
                        //Low Arm -> AL Device
                        Working_Arm = ARM.LOWER;
                        nextSeq(CaseATM.Move_TM_Pickup_AL);
                    }
                    if (ReqStatus == ReqStatus_ATM.LOAD_LAMI)
                    {
                        //Upper Arm -> Lami
                        Working_Arm = ARM.UPPER;
                        nextSeq(CaseATM.Start_TM_Load_Lami);
                    }
                    if (ReqStatus == ReqStatus_ATM.UNLOAD_LAMI)
                    {
                        //Lami -> Upper Arm
                        Working_Arm = ARM.UPPER;
                        nextSeq(CaseATM.Start_TM_Unload_Lami);
                    }
                    if (ReqStatus == ReqStatus_ATM.LOAD_LL2_CARRIER)
                    {
                        //Upper Arm -> Loadlock2 Carrier
                        Working_Stage = AtmStage.LL2;
                        Working_Arm = ARM.UPPER;
                        nextSeq(CaseATM.Check_LL_Place);
                    }
                    if (ReqStatus == ReqStatus_ATM.LOAD_LL1_DEVICE)
                    {
                        //Low Arm -> Loadlock1 Device
                        Working_Stage = AtmStage.LL1;
                        Working_Arm = ARM.LOWER ;
                        nextSeq(CaseATM.Check_LL_Place);
                    }
                    if (ReqStatus == ReqStatus_ATM.UNLOAD_LL_BONDED)
                    {
                        //Upper Arm -> Loadlock1 Device
                        Working_Stage = AtmStage.BD;
                        Working_Arm = ARM.UPPER;
                        nextSeq(CaseATM.Check_LL_Pickup);
                    }
                    if (ReqStatus == ReqStatus_ATM.UNLOAD_CP)
                    {
                        //Upper Arm -> CP
                        Working_Arm = ARM.UPPER;
                        nextSeq(CaseATM.Start_TM_Place_CP);
                    }
                    else
                    {
                        //대기
                        return;
                    }
                    return;

                /////////////////////////////////
                //TM Pickup AL
                /////////////////////////////////
                case CaseATM.Start_TM_Pickup_AL:
                    //GlobalVariable.seqShared.LoadingAlignerToAtm();
                    break;

                case CaseATM.Check_Vacuum_AL_Send:
                    aligner.GetVacuumStatus();
                    break;

                case CaseATM.Check_Vacuum_AL_Recv:
                    //Aligner Vacuum Off 확인
                    if (aligner.IsVacuumCheck() == true)
                    {
                        //에러
                        return;
                    }
                    break;


                case CaseATM.Move_TM_Pickup_AL:
                    Working_Stage = AtmStage.ALIGN;
                    MoveRobot(Working_Stage, Working_Arm);

                    strLog = string.Format("ATM Robot Pickup Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.Compl_TM_Pickup_AL:
                    if (CheckComplete() != fn.success) return;

                    strLog = string.Format("ATM Robot Pickup Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.End_TM_Pickup_AL:
                    //데이터 이동
                    GlobalVariable.seqShared.PreAlign();
                    GlobalVariable.seqShared.LoadingAlignerToAtm((HAND)Working_Arm);

                    nextSeq(CaseATM.Initialze);
                    return;

                case CaseATM.Check_Deicve:

                    break;

                /////////////////////////////////
                //TM Load Lami
                /////////////////////////////////
                case CaseATM.Start_TM_Load_Lami:

                    //Lami 상태 확인
                    //TM 상태 확인

                    //Set Rcp
                    if (RcpChange_Lami() != fn.success) return;

                    strLog = string.Format("ATM Robot Lami Load Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.Move_TM_Load_Lami:
                    //임시 스킵
                    if (Load_Lami(Working_Arm) != fn.success) return;

                    strLog = string.Format("ATM Robot Lami Load End-> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.Compl_TM_Load_Lami:
                    //라미 프로세스 
                    if (Process_Start() != fn.success) return;
                    break;

                case CaseATM.End_TM_Load_Lami:
                    //데이터 이동
                    GlobalVariable.seqShared.LoadingAtmToLami((HAND)Working_Arm, nCurLamiCount);
                    nCurLamiCount++;
                    nextSeq((int)CaseATM.Initialze);
                    return;

                /////////////////////////////////
                //TM Unload Lami
                /////////////////////////////////
                case CaseATM.Start_TM_Unload_Lami:
                    break;

                case CaseATM.Move_TM_Unload_Lami:
                    //임시 스킵
                    if (Unload_Lami(Working_Arm) != fn.success) return;

                    nCurLamiCount--;
                    GlobalVariable.seqShared.Laminate(nCurLamiCount); //라미 완료 정보
                    GlobalVariable.seqShared.LoadingLamiToAtm((HAND)Working_Arm, nCurLamiCount);
                    
                    break;

                case CaseATM.Compl_TM_Unload_Lami:
                    break;

                case CaseATM.End_TM_Unload_Lami:

                    nextSeq(CaseATM.Initialze);
                    return;

                case CaseATM.Check_LL_Place:
                    //Loadlock이 동작 중인지 확인
                    if (GlobalVariable.interlock.bLLMoving) return;
                    GlobalVariable.interlock.bLLMoving = true;
                    GlobalVariable.interlock.bLLUsed_ATM = true;

                    break;

                case CaseATM.Move_LL_Move_Place:
                    //GlobalSeq.autoRun.procLoadlock.Move(MotionPos.Pos2);
                    
                    GlobalSeq.autoRun.procLoadlock.MoveExit((int)MotionPos.Pos2);
                   
                    strLog = string.Format("Loadlock ATM Place Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.Compl_LL_Move_Place:
                    //if (GlobalSeq.autoRun.procLoadlock.CheckComplete() != fn.success) return;
                    if (GlobalSeq.autoRun.procLoadlock.CheckMoveDone((int)MotionPos.Pos2) == false) return;
                    

                    strLog = string.Format("Loadlock ATM Place Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                /////////////////////////////////
                //TM Place LL
                /////////////////////////////////
                case CaseATM.Start_TM_Place_LL:
                    break;

                case CaseATM.Set_Venting_Place_LL:

                    GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(CTC_STATUS.CTCRunMode, ref status);
                    if (status == (short)CTC_RunMode.ModeAtm) break;

                    //Loadlock Pumping 상태로 만든다.
                    fRet = GlobalSeq.autoRun.procLoadlock.Loadlock_Venting();
                    if (fRet == fn.success) break;
                    else if (fRet == fn.busy) return;
                    else
                    {
                        //Error
                        return;
                    }

                    break;


                case CaseATM.Door_Open_Place_LL:

                    //ATM Door Open
                    GlobalVariable.io.ATM_Door_Open();
                    break;

                case CaseATM.Check_Open_Place_LL:

                    //ATM Door Open Check
                    if (GlobalVariable.io.Check_ATM_Door_Open() == false)
                    {
                        //타임아웃 체크
                        return;
                    }

                    break;

                   

                case CaseATM.Move_TM_Place_LL:

                    if (GlobalSeq.autoRun.procLoadlock.SetBlock() == false) return;
                    MoveRobot(Working_Stage, Working_Arm);
                    
                    strLog = string.Format("ATM Robot Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.Compl_TM_Place_LL:
                    if (CheckComplete() != fn.success) return;
                    GlobalSeq.autoRun.procLoadlock.UnBlock();

                    strLog = string.Format("ATM Robot Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.Door_Close_Place_LL:

                    //ATM Door Close
                    GlobalVariable.io.ATM_Door_Close();
                    break;

                case CaseATM.Check_Close_Place_LL:

                    //ATM Door Close Check
                    if (GlobalVariable.io.Check_ATM_Door_Close() == false)
                    {
                        //타임아웃 체크
                        return;
                    }
                    break;

                case CaseATM.End_TM_Place_LL:

                    //데이터 이동
                    if (Working_Stage == AtmStage.LL1)
                        GlobalVariable.seqShared.LoadingAtmToLoadLock((int)WaferType.DEVICE, (HAND)Working_Arm);
                    else if (Working_Stage == AtmStage.LL2)
                        GlobalVariable.seqShared.LoadingAtmToLoadLock((int)WaferType.CARRIER, (HAND)Working_Arm);

                    GlobalVariable.interlock.bLLMoving = false;


                    //로드락 본드 완료 된 웨이퍼가 있을경우 언로딩 시킨다
                    if (GlobalVariable.seqShared.IsInATM(HAND.UPPER) == false
                        && GlobalVariable.seqShared.IsInLoadLock((int)WaferType.BONDED) == true
                        && GlobalVariable.interlock.bLLUsed_VTM == false
                        && GlobalVariable.seqShared.IsInCP(0) == false)  //조건 확인필요
                    {
                        //본딩 완료 된 웨이퍼가 있을경우 픽업한다.

                        Working_Arm = ARM.UPPER;
                        GlobalVariable.interlock.bLLUsed_ATM = true; 
                        nextSeq(CaseATM.Check_LL_Pickup);
                        return;
                    }
                    else
                    {
                        GlobalVariable.interlock.bLLUsed_ATM = false;
                        nextSeq(CaseATM.Initialze);
                        return;
                    }
                    return;

                case CaseATM.Check_LL_Pickup:
                    //Loadlock이 동작 중인지 확인
                    if (GlobalVariable.interlock.bLLMoving) return;
                    GlobalVariable.interlock.bLLMoving = true;
                    GlobalVariable.interlock.bLLUsed_ATM = true;
                    break;

                case CaseATM.Move_LL_Move_Pickup:
                    //BD Pickup -> Unload CP
                    //GlobalSeq.autoRun.procLoadlock.Move(MotionPos.Pos1);
                    GlobalSeq.autoRun.procLoadlock.MoveExit((int)MotionPos.Pos1);

                    strLog = string.Format("Loadlock ATM Pickup Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.Compl_LL_Move_Pickup:
                    //if (GlobalSeq.autoRun.procLoadlock.CheckComplete() != fn.success) return;
                    if (GlobalSeq.autoRun.procLoadlock.CheckMoveDone((int)MotionPos.Pos1) == false) return;

                    strLog = string.Format("Loadlock ATM Pickup Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                /////////////////////////////////
                //TM Pickup LL
                /////////////////////////////////
                case CaseATM.Start_TM_Pickup_LL:
                    break;

                case CaseATM.Set_Venting_Pickup_LL:

                    GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(CTC_STATUS.CTCRunMode, ref status);
                    if (status == (short)CTC_RunMode.ModeAtm) break;

                    //Loadlock Pumping 상태로 만든다.
                    fRet = GlobalSeq.autoRun.procLoadlock.Loadlock_Venting();
                    if (fRet == fn.success) break;
                    else if (fRet == fn.busy) return;
                    else
                    {
                        //Error
                        return;
                    }

                    break;

                case CaseATM.Door_Open_Pickup_LL:

                    //ATM Door Open
                    GlobalVariable.io.ATM_Door_Open();
                    break;

                case CaseATM.Check_Open_Pickup_LL:

                    //ATM Door Open Check
                    if (GlobalVariable.io.Check_ATM_Door_Open() == false)
                    {
                        //타임아웃 체크
                        return;
                    }
                    break;

                case CaseATM.Move_TM_Pickup_LL:
                    Working_Stage = AtmStage.BD;

                    if (GlobalSeq.autoRun.procLoadlock.SetBlock() == false) return;
                    MoveRobot(Working_Stage, Working_Arm);

                    strLog = string.Format("ATM Robot Pickup Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.Compl_TM_Pickup_LL:
                    if (CheckComplete() != fn.success) return;
                    GlobalSeq.autoRun.procLoadlock.UnBlock();

                    strLog = string.Format("ATM Robot Pickup Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.Door_Close_Pickup_LL:

                    //Door Close
                    GlobalVariable.io.ATM_Door_Close();
                    break;

                case CaseATM.Check_Close_Pickup_LL:

                    //Door Close Check
                    if (GlobalVariable.io.Check_ATM_Door_Close() == false)
                    {
                        //타임아웃 체크
                        return;
                    }
                    break;

                case CaseATM.End_TM_Pickup_LL:
                    //데이터 이동
                    GlobalVariable.seqShared.LoadingLoadlockToAtm((int)WaferType.BONDED, (HAND)Working_Arm);
                    GlobalVariable.interlock.bLLMoving = false;
                    GlobalVariable.interlock.bLLUsed_ATM = false;

                    nextSeq(CaseATM.Initialze);
                    return;

                /////////////////////////////////
                //TM Place CP
                /////////////////////////////////
                case CaseATM.Start_TM_Place_CP:
                    break;

                case CaseATM.Move_TM_Place_CP:
                    Working_Stage = AtmStage.CP1;

                    if (GlobalSeq.autoRun.procLoadlock.SetBlock() == false) return;
                    MoveRobot(Working_Stage, Working_Arm);

                    strLog = string.Format("ATM Robot Place Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.Compl_TM_Place_CP:
                    if (CheckComplete() != fn.success) return;
                    GlobalSeq.autoRun.procLoadlock.UnBlock();

                    strLog = string.Format("ATM Robot Place Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseATM.End_TM_Place_CP:
                    //데이터 이동
                    GlobalVariable.seqShared.LoadingAtmToCp(0, (HAND)Working_Arm);

                    nextSeq(CaseATM.Check_Interlock);
                    return;
            }

            nSeqNo++;
        }


        //public void Run()
        //{
        //    if (!isAcceptRun()) { return; }
        //    if (nSeqNo != nPreSeqNo) { resetCmd(); }
        //    nPreSeqNo = nSeqNo;

        //    CaseATM seqCase = (CaseATM)nSeqNo;


        //    alwaysCheck();


        //    switch (seqCase)
        //    {

        //        case CaseATM.Initialze:
        //            //AddMessage("Test");
        //            break;

        //        case CaseATM.Check_Interlock:
        //            break;

        //        case CaseATM.Check_Status:

        //            //현재 상태를 확인하자
        //            ReqStatus = CheckReqStatus();
        //            if (ReqStatus == ReqStatus_ATM.LOAD_LAMI)
        //            {
        //                //Load AL -> Load Lami
        //                Working_Arm = ARM.UPPER;
        //                nextSeq(CaseATM.Move_TM_Pickup_AL);
        //            }
        //            else if (ReqStatus == ReqStatus_ATM.UNLOAD_LAMI_LL2_CARR)
        //            {
        //                //Unload Lami -> Load LL2 ( Carr )
        //                //Working_Arm = ARM.UPPER;
        //                Working_Arm = ARM.LOWER;
        //                nextSeq(CaseATM.Start_TM_Unload_Lami);
        //            }
        //            else if (ReqStatus == ReqStatus_ATM.LOAD_LL1_DIV)
        //            {
        //                //Load AL -> Load LL1 ( DEVI )
        //                Working_Arm = ARM.LOWER;
        //                nextSeq(CaseATM.Start_TM_Pickup_AL);
        //            }
        //            else if (ReqStatus == ReqStatus_ATM.UNLOAD_CP)
        //            {
        //                //Load BD -> Unload CP
        //                nextSeq(CaseATM.Check_LL_Pickup);
        //            }
        //            else if( ReqStatus == ReqStatus_ATM.LOAD_LL2_CARR)
        //            {
        //                //Only Bond 시퀀스
        //                //Load AL -> LL2 Carr
        //                Working_Arm = ARM.UPPER;
        //                nextSeq(CaseATM.Start_TM_Pickup_AL);
        //            }
        //            else if(ReqStatus == ReqStatus_ATM.UNLOAD_LAMI_CP)
        //            {
        //                //Unload Lami -> CP
        //                Working_Arm = ARM.LOWER;
        //                nextSeq(CaseATM.Start_TM_Unload_Lami);
        //            }
        //            else
        //            {
        //                //대기
        //                return;
        //            }
        //            return;

        //        /////////////////////////////////
        //        //TM Pickup AL
        //        /////////////////////////////////
        //        case CaseATM.Start_TM_Pickup_AL:
        //            //GlobalVariable.seqShared.LoadingAlignerToAtm();
        //            break;

        //        case CaseATM.Check_Vacuum_AL_Send:
        //            aligner.GetVacuumStatus();
        //            break;

        //        case CaseATM.Check_Vacuum_AL_Recv:
        //            //Aligner Vacuum Off 확인
        //            if (aligner.IsVacuumCheck() == true)
        //            {
        //                //에러
        //                return;
        //            }
        //            break;


        //        case CaseATM.Move_TM_Pickup_AL:
        //            Working_Stage = AtmStage.ALIGN;
        //            MoveRobot(Working_Stage, Working_Arm);

        //            strLog = string.Format("ATM Robot Pickup Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.Compl_TM_Pickup_AL:
        //            if (CheckComplete() != fn.success) return;

        //            strLog = string.Format("ATM Robot Pickup Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.End_TM_Pickup_AL:
        //            //데이터 이동
        //            GlobalVariable.seqShared.LoadingAlignerToAtm((HAND)Working_Arm);
        //            break;

        //        case CaseATM.Check_Deicve:

        //            //현재 웨이퍼가 Carrier일 경우 -> Laminator
        //            //Device일 경우 Loadlock으로 이동하자                   
        //            if (GlobalVariable.seqShared.robotATM[(int)Working_Arm].waferType == WaferType.DEVICE)
        //            {
        //                //Device -> Loadlock
        //                Working_Arm = ARM.LOWER;
        //                Working_Stage = AtmStage.LL1;
        //                nextSeq(CaseATM.Check_LL_Place);
        //                return;
        //            }
        //            else
        //            {
        //                if(ReqStatus == ReqStatus_ATM.LOAD_LL2_CARR)
        //                {
        //                    //Only Bond 모드일 경우
        //                    Working_Arm = ARM.UPPER;
        //                    Working_Stage = AtmStage.LL2;
        //                    nextSeq(CaseATM.Check_LL_Place);
        //                    return;
        //                }
        //                else
        //                {
        //                    //Carrier -> Lami
        //                    //임시 스킵
        //                    nextSeq(CaseATM.Start_TM_Load_Lami);
        //                    return;
        //                }
        //            }
        //            break;

        //        //Lami Load
        //        case CaseATM.Start_TM_Load_Lami:

        //            //Lami 상태 확인
        //            //TM 상태 확인

        //            //Set Rcp

        //            if (RcpChange_Lami() != fn.success) return;

        //            strLog = string.Format("ATM Robot Lami Load Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.Move_TM_Load_Lami:
        //            //임시 스킵
        //            //if (Load_Lami(Working_Arm) != fn.success) return;

        //            strLog = string.Format("ATM Robot Lami Load End-> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.Compl_TM_Load_Lami:
        //            break;

        //        case CaseATM.End_TM_Load_Lami:
        //            //데이터 이동
        //            GlobalVariable.seqShared.LoadingAtmToLami((HAND)Working_Arm);
        //            nextSeq((int)CaseATM.Initialze);
        //            return;

        //        //Lami Unload 후 Loadlock Carrier 로드
        //        case CaseATM.Start_TM_Unload_Lami:
        //            break;

        //        case CaseATM.Move_TM_Unload_Lami:
        //            //임시 스킵
        //            //if (Unload_Lami(Working_Arm) != fn.success) return;
        //            break;

        //        case CaseATM.Compl_TM_Unload_Lami:
        //            break;

        //        case CaseATM.End_TM_Unload_Lami:
        //            //데이터 이동
        //            GlobalVariable.seqShared.LoadingLamiToAtm((HAND)Working_Arm);

        //            if(ReqStatus == ReqStatus_ATM.UNLOAD_LAMI_CP)
        //            {
        //                //Only Lami 모드일 경우 
        //                //라미 언로드 후 CP로 언로딩
        //            }
        //            else
        //            {
        //                //언로드 후 LL2으로 Stage변경
        //                Working_Stage = AtmStage.LL2;
        //            }

        //            break;

        //        case CaseATM.Check_LL_Place:
        //            //Loadlock이 동작 중인지 확인
        //            if (GlobalVariable.interlock.bLLMoving) return;
        //            GlobalVariable.interlock.bLLMoving = true;
        //            break;

        //        case CaseATM.Move_LL_Move_Place:
        //            //Place 
        //            GlobalSeq.autoRun.procLoadlock.Move(MotionPos.Pos2);

        //            strLog = string.Format("Loadlock ATM Place Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.Compl_LL_Move_Place:
        //            if (GlobalSeq.autoRun.procLoadlock.CheckComplete() != fn.success) return;

        //            strLog = string.Format("Loadlock ATM Place Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        /////////////////////////////////
        //        //TM Place LL
        //        /////////////////////////////////
        //        case CaseATM.Start_TM_Place_LL:
        //            break;

        //        case CaseATM.Move_TM_Place_LL:
        //            MoveRobot(Working_Stage, Working_Arm);

        //            strLog = string.Format("ATM Robot Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.Compl_TM_Place_LL:
        //            if (CheckComplete() != fn.success) return;

        //            strLog = string.Format("ATM Robot Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.End_TM_Place_LL:

        //            //데이터 이동
        //            if(Working_Stage == AtmStage.LL1)
        //                GlobalVariable.seqShared.LoadingAtmToLoadLock((int)WaferType.DEVICE, (HAND)Working_Arm);
        //            else if (Working_Stage == AtmStage.LL2)
        //                GlobalVariable.seqShared.LoadingAtmToLoadLock((int)WaferType.CARRIER, (HAND)Working_Arm);

        //            GlobalVariable.interlock.bLLMoving = false;
        //            nextSeq((int)CaseATM.Initialze);
        //            return;


        //        case CaseATM.Check_LL_Pickup:
        //            //Loadlock이 동작 중인지 확인
        //            if (GlobalVariable.interlock.bLLMoving) return;
        //            GlobalVariable.interlock.bLLMoving = true;
        //            break;

        //        case CaseATM.Move_LL_Move_Pickup:
        //            //BD Pickup -> Unload CP
        //            GlobalSeq.autoRun.procLoadlock.Move(MotionPos.Pos1);

        //            strLog = string.Format("Loadlock ATM Pickup Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.Compl_LL_Move_Pickup:
        //            if (GlobalSeq.autoRun.procLoadlock.CheckComplete() != fn.success) return;

        //            strLog = string.Format("Loadlock ATM Pickup Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        /////////////////////////////////
        //        //TM Pickup LL
        //        /////////////////////////////////
        //        case CaseATM.Start_TM_Pickup_LL:
        //            break;

        //        case CaseATM.Move_TM_Pickup_LL:
        //            Working_Stage = AtmStage.BD;
        //            MoveRobot(Working_Stage, Working_Arm);

        //            strLog = string.Format("ATM Robot Pickup Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.Compl_TM_Pickup_LL:
        //            if (CheckComplete() != fn.success) return;

        //            strLog = string.Format("ATM Robot Pickup Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.End_TM_Pickup_LL:
        //            //데이터 이동
        //            GlobalVariable.seqShared.LoadingLoadlockToAtm((int)WaferType.BONDED, (HAND)Working_Arm);
        //            GlobalVariable.interlock.bLLMoving = false;
        //            break;

        //        /////////////////////////////////
        //        //TM Place CP
        //        /////////////////////////////////
        //        case CaseATM.Start_TM_Place_CP:
        //            break;

        //        case CaseATM.Move_TM_Place_CP:
        //            Working_Stage = AtmStage.CP1;
        //            MoveRobot(Working_Stage, Working_Arm);

        //            strLog = string.Format("ATM Robot Place Move Start -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.Compl_TM_Place_CP:
        //            if (CheckComplete() != fn.success) return;

        //            strLog = string.Format("ATM Robot Place Move End -> {0}, {1}", Working_Stage.ToString(), Working_Arm.ToString());
        //            AddMessage(strLog);
        //            break;

        //        case CaseATM.End_TM_Place_CP:
        //            //데이터 이동
        //            GlobalVariable.seqShared.LoadingAtmToCp(0, (HAND)Working_Arm);
        //            nextSeq(CaseATM.Check_Interlock);
        //            return;
        //    }

        //    nSeqNo++;
        //}



        public void MoveRobot(AtmStage stage, ARM _Arm, bool bHandFold = false)
        {
            int nSlot = 1;

            if (bHandFold == true)
            {
                AtmRobot.MoveWaitFold((int)stage, nSlot, _Arm );
            }
            else
            {
                if (stage == AtmStage.ALIGN || stage == AtmStage.LAMI_ULD || stage == AtmStage.BD)
                {
                    AtmRobot.MovePickup((int)stage, nSlot, _Arm);
                }
                else
                {
                    AtmRobot.MovePlace((int)stage, nSlot, _Arm);
                }
            }
        }

        public fn CheckComplete()
        {      
            if (AtmRobot.CheckComplete())
                return fn.success;
            else if (AtmRobot.IsAlarmCheck())
                return fn.err;
            else
            {
                //타임 아웃 , 에러 체크
                return fn.busy;
            }
        }

        public bool CheckStatusWafer(ARM _Arm)
        {
            bool bRet = false;
            if (_Arm == ARM.UPPER)
                bRet = AtmRobot.IsWaferUpper();
            else
                bRet = AtmRobot.IsWaferLower();

            return bRet;
        }


        //기존 시퀀스용
        //public ReqStatus_ATM CheckReqStatus()
        //{

        //    ReqStatus_ATM ReqStatus = ReqStatus_ATM.NONE;

        //    if( CheckWaferAL() == true
        //        && GlobalVariable.WaferInfo.bWaferTypeLL == WaferType.DECIVE
        //        && GlobalVariable.WaferInfo.bWaferLL1 == false)
        //    {
        //        //Load AL -> Load LL1
        //        ReqStatus = ReqStatus_ATM.LOAD_LL1;
        //    }
        //    else if (CheckWaferAL() == true
        //        && GlobalVariable.WaferInfo.bWaferTypeLL == WaferType.CARRIER
        //        && GlobalVariable.WaferInfo.bWaferLL2 == false)
        //    {
        //        //Unload Lami -> Load LL2
        //        ReqStatus = ReqStatus_ATM.LOAD_LL2;
        //    }
        //    else if (GlobalVariable.WaferInfo.bWaferBD == true
        //        && GlobalVariable.WaferInfo.bWaferCP1 == false)
        //    {
        //        //Unload LL -> Unload CP
        //        ReqStatus = ReqStatus_ATM.UNLOAD_CP;
        //    }
        //    else
        //    {
        //        ReqStatus = ReqStatus_ATM.NONE;
        //    }
        //    return ReqStatus;
        //}

        public bool CheckAligner()
        {
            //웨이퍼 유무
            if (GlobalVariable.seqShared.IsInAligner() == false)
                return false;

            //Align 완료
            //if (GlobalVariable.seqShared.aligner.Finish_PreAlign() == false)
                //return false;

            //동작 중인지 
            if (GlobalVariable.interlock.bAlignMoving == true)
                return false;

            return true;
        }

        public ReqStatus_ATM CheckReqStatus()
        {
            ReqStatus_ATM ReqStatus = ReqStatus_ATM.NONE;
            RUN_MODE RunMode = RecipeMgr.Inst.TempRcp.eRunMode; //현재 레시피 
          
            switch(RunMode)
            {
                case RUN_MODE.FULL:

                    //FULL 시퀀스
                    if (CheckAligner()
                        && GlobalVariable.seqShared.aligner.waferType == WaferType.CARRIER
                        && GlobalVariable.seqShared.IsInATM(HAND.UPPER) == false
                        && ( GlobalVariable.seqShared.IsInLami(0) == false || GlobalVariable.seqShared.IsInLami(1) == false )
                        && (CheckReqStatus_Lami() == ReqStatus_Lami.LOAD || CheckReqStatus_Lami() == ReqStatus_Lami.EXCHANGE))
                    {
                        //Load AL Carrier -> Upper Arm
                        ReqStatus = ReqStatus_ATM.LOAD_AL_CARRIER;
                        return ReqStatus;
                    }
                    if (CheckAligner()
                        && GlobalVariable.seqShared.aligner.waferType == WaferType.DEVICE
                        && GlobalVariable.seqShared.IsInATM(HAND.LOWER) == false
                        && GlobalVariable.seqShared.IsInATM(HAND.UPPER) == false)
                    {
                        //Load AL Device -> Low Arm 
                        ReqStatus = ReqStatus_ATM.LOAD_AL_DEVICE;
                        return ReqStatus;
                    }
                    if ( GlobalVariable.seqShared.IsInATM(HAND.UPPER) == true
                        && (CheckReqStatus_Lami() == ReqStatus_Lami.LOAD || CheckReqStatus_Lami() == ReqStatus_Lami.EXCHANGE))
                    {
                        if(GlobalVariable.seqShared.robotATM[(int)HAND.UPPER].bIsPreAlign == true
                            && GlobalVariable.seqShared.robotATM[(int)HAND.UPPER].bIsLami == false
                            && GlobalVariable.seqShared.robotATM[(int)HAND.UPPER].waferType == WaferType.CARRIER
                            && (GlobalVariable.seqShared.IsInLami(0) == false || GlobalVariable.seqShared.IsInLami(1) == false))
                        {
                            //Upper Arm -> Load Lami
                            ReqStatus = ReqStatus_ATM.LOAD_LAMI;
                            return ReqStatus;
                        }

                        
                    }
                    if (GlobalVariable.seqShared.IsInATM(HAND.UPPER) == false
                        && GlobalVariable.seqShared.IsInLoadLock((int)WaferType.CARRIER) == false
                        && (CheckReqStatus_Lami() == ReqStatus_Lami.UNLOAD || CheckReqStatus_Lami() == ReqStatus_Lami.EXCHANGE))
                    {
                        //Upper Arm -> Unload Lami
                        ReqStatus = ReqStatus_ATM.UNLOAD_LAMI;
                        return ReqStatus;
                    }

                    if (GlobalVariable.seqShared.IsInATM(HAND.LOWER) == true
                        && GlobalVariable.seqShared.IsInLoadLock((int)WaferType.DEVICE) == false
                        && GlobalVariable.interlock.bLLUsed_VTM == false)
                    {
                        if(GlobalVariable.seqShared.robotATM[(int)HAND.LOWER].bIsPreAlign == true
                            && GlobalVariable.seqShared.robotATM[(int)HAND.LOWER].waferType == WaferType.DEVICE)
                        {
                            //Low Arm -> LL Device
                            ReqStatus = ReqStatus_ATM.LOAD_LL1_DEVICE;
                            return ReqStatus;
                        }
                    }
                    if (GlobalVariable.seqShared.IsInATM(HAND.UPPER) == true
                        && GlobalVariable.seqShared.IsInLoadLock((int)WaferType.CARRIER) == false
                        && GlobalVariable.interlock.bLLUsed_VTM == false)
                    {
                        if(GlobalVariable.seqShared.robotATM[(int)HAND.UPPER].waferType == WaferType.CARRIER
                            && GlobalVariable.seqShared.robotATM[(int)HAND.UPPER].bIsLami == true)
                        {
                            //Upper Arm -> LL Carrier
                            ReqStatus = ReqStatus_ATM.LOAD_LL2_CARRIER;
                            return ReqStatus;
                        }

                    }
                    if (GlobalVariable.seqShared.IsInATM(HAND.UPPER) == false
                        && GlobalVariable.seqShared.IsInLoadLock((int)WaferType.BONDED) == true
                        && GlobalVariable.interlock.bLLUsed_VTM == false //조건 확인필요
                        && GlobalVariable.seqShared.IsInCP(0) == false)  
                    {
                        //LL Bonded -> Upper Arm
                        ReqStatus = ReqStatus_ATM.UNLOAD_LL_BONDED;
                        return ReqStatus;
                    }
                    if (GlobalVariable.seqShared.IsInATM(HAND.UPPER) == true
                        && GlobalVariable.seqShared.IsInCP(0) == false) 
                    {
                        if(GlobalVariable.seqShared.robotATM[(int)HAND.UPPER].bIsBond == true)
                        {
                            //LL Bonded -> Upper Arm
                            ReqStatus = ReqStatus_ATM.UNLOAD_CP;
                            return ReqStatus;
                        }
                    }

                    break;

//                 case RUN_MODE.ONLY_LAMI:
// 
//                     //Only Lami
// 
//                     if (CheckReqStatus_Lami() == ReqStatus_Lami.LOAD
//                         && GlobalVariable.seqShared.IsInAligner()
//                         && GlobalVariable.seqShared.aligner.Finish_PreAlign()
//                         && GlobalVariable.seqShared.aligner.waferType == WaferType.CARRIER
//                         && GlobalVariable.seqShared.IsInATM(HAND.UPPER) == false)
//                     {
//                         //AL에서 캐리어 로드 후 라미 로딩
//                         ReqStatus = ReqStatus_ATM.LOAD_LAMI;
//                     }
//                     else if(CheckReqStatus_Lami() == ReqStatus_Lami.UNLOAD
//                         && GlobalVariable.seqShared.IsInLami()
//                         && GlobalVariable.seqShared.IsInCP(0) == false
//                         && GlobalVariable.seqShared.IsInATM(HAND.LOWER) == false)
//                     {
//                         //Lami에서 CP언로딩
//                         ReqStatus = ReqStatus_ATM.UNLOAD_LAMI_CP;                        
//                     }
//                     else if (CheckReqStatus_Lami() == ReqStatus_Lami.EXCHANGE
//                         && GlobalVariable.seqShared.IsInLami()
//                         && GlobalVariable.seqShared.IsInCP(0) == false
//                         && GlobalVariable.seqShared.IsInATM(HAND.LOWER) == false)
//                     {
//                         if(GlobalVariable.seqShared.IsInATM(HAND.UPPER) == false)
//                         {
//                             //바로 CP로 언로딩 
//                             ReqStatus = ReqStatus_ATM.UNLOAD_LAMI_CP;                                                
//                         }
//                         else if (GlobalVariable.seqShared.IsInATM(HAND.UPPER) == true)
//                         {
// 
//                         }
// 
//                     }
//                     else
//                     {
//                         ReqStatus = ReqStatus_ATM.NONE;
//                     }
//                     break;
// 
//                 case RUN_MODE.ONLY_BOND:
// 
//                     //Only Bond
// 
//                     if (GlobalVariable.seqShared.IsInLoadLock((int)WaferType.DEVICE) == false
//                         && GlobalVariable.seqShared.IsInAligner()
//                         && GlobalVariable.seqShared.aligner.Finish_PreAlign()
//                         && GlobalVariable.seqShared.aligner.waferType == WaferType.DEVICE)
//                     {
//                         //AL에서 디바이스 로드 후 LL에 로딩 Device
//                         ReqStatus = ReqStatus_ATM.LOAD_LL1_DIV;
//                     }
//                     else if(GlobalVariable.seqShared.IsInLoadLock((int)WaferType.CARRIER) == false
//                         && GlobalVariable.seqShared.IsInAligner()
//                         && GlobalVariable.seqShared.aligner.Finish_PreAlign()
//                         && GlobalVariable.seqShared.aligner.waferType == WaferType.CARRIER)
//                     {
//                         //AL에서 디바이스 로드 후 LL에 로딩 Carrier
//                         ReqStatus = ReqStatus_ATM.LOAD_LL2_CARR;
//                     }
//                     else if (GlobalVariable.seqShared.IsInLoadLock((int)WaferType.BONDED) == true
//                         && GlobalVariable.seqShared.IsInCP(0) == false)
//                     {
//                         //LL에서 본딩 완료 웨이퍼 로드 후 CP에 언로딩
//                         ReqStatus = ReqStatus_ATM.UNLOAD_CP;
//                     }
//                     else
//                     {
//                         ReqStatus = ReqStatus_ATM.NONE;
//                     }
//                     break;

                default:
                    //에러
                    break;

            }

            return ReqStatus;
        }


        public ReqStatus_Lami CheckReqStatus_Lami()
        {
            ReqStatus_Lami Status;

#if !_REAL_MC

            if (GlobalVariable.WaferInfo.bLamiLoad
                && GlobalVariable.WaferInfo.bLamiUnload)
            {
                Status = ReqStatus_Lami.EXCHANGE;
            }
            else if (GlobalVariable.WaferInfo.bLamiLoad)
            {
                Status = ReqStatus_Lami.LOAD;
            }
            else if (GlobalVariable.WaferInfo.bLamiUnload)
            {
                Status = ReqStatus_Lami.UNLOAD;
            }
            else
            {
                Status = ReqStatus_Lami.NONE;
            }

            return Status;
#endif


            if (Laminator.ReadAddrData(LAMI_PLC_ADDR.LAMINATOR_READY) == 1)
            {
                if( Laminator.ReadAddrData(LAMI_PLC_ADDR.LOAD_REQUEST) == 1
                    && Laminator.ReadAddrData(LAMI_PLC_ADDR.UNLOAD_REQUEST) == 1)
                {
                    //Wafer Exchange Req
                    Status = ReqStatus_Lami.EXCHANGE;
                }
                else if( Laminator.ReadAddrData(LAMI_PLC_ADDR.LOAD_REQUEST) == 1)
                {
                    //Wafer Load Req
                    Status = ReqStatus_Lami.LOAD;
                }
                else if( Laminator.ReadAddrData(LAMI_PLC_ADDR.UNLOAD_REQUEST) == 1)
                {
                    //Wafer Unload Req
                    Status = ReqStatus_Lami.UNLOAD;
                }
                else
                {
                    //요청이 없을 경우
                    Status = ReqStatus_Lami.NONE;
                }
            }
            else
            {
                //준비 상태가 아닌 경우
                Status = ReqStatus_Lami.NONE;
            }
            return Status;
        }


        #region Interface_Load

        public fn Load_Lami(ARM arm)
        {

#if !_REAL_MC
            return fn.success;
#endif

            int nSeq = (int)Seq_Lami.Lami_Load;

            arm = ARM.UPPER;

            switch (nSeq_Lami[nSeq])
            {
                case 0 :
                    break;

                //&& Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_STOP_ACK) == 1
                case 10:

                    if( Laminator.ReadAddrData(LAMI_PLC_ADDR.LAMINATOR_READY) == 1                        
                        && Laminator.ReadAddrData(LAMI_PLC_ADDR.LOAD_REQUEST) == 1)
                    {
                        //Ack 신호 추가
                        Laminator.WriteAddrData(CTC_PLC_ADDR.TM_LOAD_COMPLETE, true);
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }

                    return fn.busy;

                case 15:
                    if (Laminator.ReadAddrData(LAMI_PLC_ADDR.LOAD_COMPLETE_ACK) == 1) //웨이퍼 반입 완료 ack 신호
                    {                    
                        Laminator.WriteAddrData(CTC_PLC_ADDR.TM_LOAD_COMPLETE, false);
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                    break;

                case 20:
                    // ATM 로봇 웨이퍼 로드 동작
                    MoveRobot(AtmStage.LAMI_LD, arm);
                    break;

                case 30:
                    if (CheckComplete() != fn.success) return fn.busy;                 
                    //웨이퍼 상태 확인필요!
                    break;

                case 40:
                    // 웨이퍼 로드 후 Arm Fold
                    //MoveRobot(AtmStage.LAMI_LD, arm, true);
                    break;

                case 50:
                    //if (CheckComplete() != fn.success) return fn.busy;
                    break;

                case 60:
                    //웨이퍼 반입 완료 시그널 온
                    //Laminator.WriteAddrData(CTC_PLC_ADDR.TM_LOAD_COMPLETE, true);
                    break;

                case 70:
                    break;

                case 80:

                    //동작 완료
                    nSeq_Lami[nSeq] = 0;
                    return fn.success;
            }

            nSeq_Lami[nSeq]++;
            return fn.busy;
        }

        #endregion


        #region Interface_Unload

        public fn Unload_Lami(ARM arm)
        {
#if !_REAL_MC
            return fn.success;
#endif

            int nSeq = (int)Seq_Lami.Lami_Unload;

            switch (nSeq_Lami[nSeq])
            {
                case 0:
                    break;
                //Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_START_ACK) == 1
                case 10:
                    if(  Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_START_ACK) == 0
                        && Laminator.ReadAddrData(LAMI_PLC_ADDR.UNLOAD_REQUEST) == 1)
                    {
                        //웨이퍼 언로드 시그널 확인 
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    return fn.busy;

                case 20:
                    //ATM 로봇 웨이퍼 언로드 동작
                    MoveRobot(AtmStage.LAMI_ULD, arm);
                    break;

                case 30:
                    if (CheckComplete() != fn.success) return fn.busy;                 
                    //웨이퍼 상태 확인필요!
                    break;

                case 40:
                    //웨이퍼 반출 완료 시그널 ON
                    Laminator.WriteAddrData(CTC_PLC_ADDR.TM_UNLOAD_COMPLETE, true);
                    break;

                case 50:
                    if (Laminator.ReadAddrData(LAMI_PLC_ADDR.UNLOAD_REQUEST) == 0)
                    {
                        //언로드 요청신호가 꺼진 후 완료 신호를 끄자
                        Laminator.WriteAddrData(CTC_PLC_ADDR.TM_UNLOAD_COMPLETE, false);
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    return fn.busy;

                case 60:

                    //동작 완료
                    nSeq_Lami[nSeq] = 0;
                    return fn.success;
            }

            nSeq_Lami[nSeq]++;
            return fn.busy;
        }

#endregion

        #region Interface_Process
        public fn Process_Start()
        {
#if !_REAL_MC
            return fn.success;
#endif

            int nSeq = (int)Seq_Lami.Lami_Proc_Start;

            switch (nSeq_Lami[nSeq])
            {
                case 0:
                    break;

                case 10:
                    if( Laminator.ReadAddrData(LAMI_PLC_ADDR.LAMINATOR_READY) == 1
                        && Laminator.ReadAddrData(LAMI_PLC_ADDR.LOAD_COMPLETE_ACK) == 1)
                    {
                        //프로세스 스타트 시그널 ON
                        Laminator.WriteAddrData(CTC_PLC_ADDR.REMOTE_START_REQUEST, true); 
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    return fn.busy;

                case 20:

                    if (Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_START_ACK) == 1)
                    {
                        //프로세스 스타트 ACK 확인
                        Laminator.WriteAddrData(CTC_PLC_ADDR.REMOTE_START_REQUEST, false); //Off
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }

                    return fn.busy;

                case 30:
                    //동작 완료
                    nSeq_Lami[nSeq] = 0;
                    return fn.success;
            }
            nSeq_Lami[nSeq]++;
            return fn.busy;
        }

        public fn Process_Pause()
        {
            int nSeq = (int)Seq_Lami.Lami_Proc_Pause;

            switch (nSeq_Lami[nSeq])
            {
                case 0:
                    break;

                case 10:
                    if (Laminator.ReadAddrData(LAMI_PLC_ADDR.LAMINATOR_READY) == 1
                        && Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_START_ACK) == 1)
                    {
                        Laminator.WriteAddrData(CTC_PLC_ADDR.PROCESS_PAUSE_REQUEST, true);
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    return fn.busy;

                case 20:

                    if (Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_PAUSE_ACK) == 1)
                    {
                        Laminator.WriteAddrData(CTC_PLC_ADDR.PROCESS_PAUSE_REQUEST, false); //Off
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }

                    return fn.busy;

                case 30:
                    //동작 완료
                    return fn.success;
            }
            nSeq_Lami[nSeq]++;
            return fn.busy;
        }


        public fn Process_Resume()
        {
            int nSeq = (int)Seq_Lami.Lami_Proc_Resume;

            switch (nSeq_Lami[nSeq])
            {
                case 0:
                    break;

                case 10:
                    if (Laminator.ReadAddrData(LAMI_PLC_ADDR.LAMINATOR_READY) == 1
                        && Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_START_ACK) == 1)
                    {
                        Laminator.WriteAddrData(CTC_PLC_ADDR.PROCESS_RESUME_REQUEST, true);
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    return fn.busy;

                case 20:

                    if (Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_RESUME_ACK) == 1)
                    {
                        Laminator.WriteAddrData(CTC_PLC_ADDR.PROCESS_RESUME_REQUEST, false); //Off
                        break;
                    }
                    else if( Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_RESUME_NAK) == 1)
                    {
                        //Resume NAK
                        Laminator.WriteAddrData(CTC_PLC_ADDR.PROCESS_RESUME_REQUEST, false); //Off
                        return fn.err;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    return fn.busy;

                case 30:
                    //동작 완료
                    return fn.success;
            }
            nSeq_Lami[nSeq]++;
            return fn.busy;
        }


        #endregion


        #region Interface_Manual
        public fn Init_Lami()
        {
            int nSeq = (int)Seq_Lami.Lami_Init;

            switch (nSeq_Lami[nSeq])
            {
                case 0:
                    break;

                case 5:
                    //Lami 초기화 요청
                    Laminator.WriteAddrData(CTC_PLC_ADDR.INIT_REQUEST, true); 
                    break;

                case 10:
                    if (Laminator.ReadAddrData(LAMI_PLC_ADDR.INIT_ACK) == 1)
                    {
                        //INIT ACK ON 시그널 확인
                        Laminator.WriteAddrData(CTC_PLC_ADDR.INIT_REQUEST, false); //Off 
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    return fn.busy;                   

                case 20:
                    //동작 완료
                    nSeq_Lami[nSeq] = 0;
                    return fn.success;
            }

            nSeq_Lami[nSeq]++;
            return fn.busy;
        }

        public fn Standby_Lami()
        {
            int nSeq = (int)Seq_Lami.Lami_Standby;

            switch (nSeq_Lami[nSeq])
            {
                case 0:
                    break;

                case 10:
                    //Lami 초기화 요청
                    Laminator.WriteAddrData(CTC_PLC_ADDR.STANDBY_REQUEST, true);
                    break;

                case 20:
                    if (Laminator.ReadAddrData(LAMI_PLC_ADDR.STANDBY_ACK) == 1)
                    {
                        //INIT ACK ON 시그널 확인
                        Laminator.WriteAddrData(CTC_PLC_ADDR.STANDBY_REQUEST, false); //Off 
                        break;
                    }
                    else
                    {
                        //타임 아웃
                    }
                    return fn.busy;

                case 30:
                    //동작 완료
                    nSeq_Lami[nSeq] = 0;
                    return fn.success;
            }

            nSeq_Lami[nSeq]++;
            return fn.busy;
        }


        public fn RcpChange_Lami()
        {
#if !_REAL_MC
            return fn.success;
#endif

            int nSeq = (int)Seq_Lami.Lami_RcpChange;

            switch (nSeq_Lami[nSeq])
            {
                case 0:
                    break;

                case 10:
                    //Lami 초기화 요청
                    Laminator.WriteAddrData(CTC_PLC_ADDR.RECIPE_CHANGE_REQUEST, true);

                    //레시피 연결 필요 !!!
                    //Recipe Write

                    LamiInfo = ProcessMgr.Inst.TempPinfo.listProcLami.Single<ProcInfoLami>(p => p.strProcName == RecipeMgr.Inst.TempRcp.strLamiCondition);

                    Laminator.WriteRecipeData(RECIPE_PLC_ADDR.CHAMBER_LO_TEMP, Convert.ToInt16(LamiInfo.dLowerTemp * 10));
                    Laminator.WriteRecipeData(RECIPE_PLC_ADDR.CHAMBER_UP_TEMP , Convert.ToInt16(LamiInfo.dUpperTemp * 10));
                    Laminator.WriteRecipeData(RECIPE_PLC_ADDR.PRESS_TON, Convert.ToInt16(LamiInfo.dPressure * 100));
                    Laminator.WriteRecipeData(RECIPE_PLC_ADDR.CHAMBER_LO_MOTOR_PRESS_WAIT_TIME, Convert.ToInt16(LamiInfo.nPressingTimeSec * 10));
                    Laminator.SendRcp(true);
                    break;

                case 20:
                    if (Laminator.ReadAddrData(LAMI_PLC_ADDR.RECIPE_CHANGE_ACK) == 1)
                    {
                        //ACK
                        Laminator.WriteAddrData(CTC_PLC_ADDR.RECIPE_CHANGE_REQUEST, false);
                        break;
                    }
                    else if( Laminator.ReadAddrData(LAMI_PLC_ADDR.RECIPE_CHANGE_ACK) == 2)
                    {
                        //NAK
                        Laminator.WriteAddrData(CTC_PLC_ADDR.RECIPE_CHANGE_REQUEST, false);
                        return fn.err;
                    }
                    else
                    {
                        //타임 아웃
                    }

                    Laminator.SendRcp(false);
                    return fn.busy;

                case 30:
                    //동작 완료
                    nSeq_Lami[nSeq] = 0;
                    return fn.success;
            }

            nSeq_Lami[nSeq]++;
            return fn.busy;
        }


        #endregion
    }
}
