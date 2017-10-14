using CJ_Controls.Communication.CybogRobot_HTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_CTC.Data;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;

namespace TBDB_Handler.SEQ
{
    enum CaseFM
    {
        Initialze,
        Check_Interlock,

        //LPM Load
        Start_LPM_Load,
        Move_LPM_Load, //LPM Open 동작 ( Clamp, Fwd, latch, vac on, open door, down door )
        Compl_LPM_Load,
        End_LPM_Load,
        LPM_Mapping_Data,

        Check_Status_Load_A,
        Compl_Status_Load_A,
        End_Status_Load_A,

        Check_Status,
    
        //Wafer Load
        //FM Pickup -> LPM 
        Start_FM_Pickup_LPM,
        Move_FM_Pickup_LPM,
        Compl_FM_Pickup_LPM,
        End_FM_Pickup_LPM,

        Waiting_AL,       

        //FM Place -> AL
        Start_FM_Place_AL,
        Move_FM_Place_AL,
        Compl_FM_Place_AL,
        End_FM_Place_AL,

        //Check Unload Wafer
        Check_Unload_Wafer,

        //Wafer Unload
        //FM Pickup -> CP
        Start_FM_Pickup_CP,
        Move_FM_Pickup_CP,
        Compl_FM_Pickup_CP,
        End_FM_Pickup_cp,

        //FM Place -> LPM
        Start_FM_Place_LPM,
        Move_FM_Place_LPM,
        Compl_FM_Place_LPM,
        End_FM_Place_LPM,

        //LPM Unload
        Start_LPM_Unload,
        Move_LPM_Unload, //LPM Close 동작 ( Door Up, Door Close, vac off, unlatch, home, unclamp )
        Check_Status_Unload,
        Compl_LPM_Unload,
        End_LPM_Unload,
    }

    public enum FMStage
    {
        LPMA = 1,
        LPMB,
        LPMC,
        LPMD,
        AL,
        CP1,
        CP2,
        CP3,
    }

    public enum FMStatus
    {
        READY,
        LPM_LOAD, // LPM Load 상태
        LPM_UNLOAD,
        BUFFER_LOAD, //Buffer Load 상태
        BUFFER_UNLOAD,
    }


    public class ProcFM 
    {
        public MotionFm FmRobot = new MotionFm(); 
        public MotionLpm LpmRoot = new MotionLpm();
        

        private bool isCycleRun = false;
        public int nSeqNo = 0;
        private int nPreSeqNo = 0;

        public delegate void procAddMsgEvent(int nSeq, int nCaseNo, string strAddMsg);
        public event procAddMsgEvent procMsgEvent;

        public delegate void procFMErrorStopEvent(int nErrSafty);
        public event procFMErrorStopEvent errSaftyStopEvent;

        int nSeq = (int)UNIT.FM_ROBOT;

        FMStatus fmStatus = FMStatus.READY;
        EFEM Working_LPM = EFEM.LPMA;
        ARM Working_Arm = ARM.UPPER;
        FMStage Working_Stage = FMStage.LPMA;
        int nWorking_Slot = 1;
        EFEM_TYPE efemType = EFEM_TYPE.A_CARRIER;

        int nWaferCount = 0;
        string strLog = "";


        ALIGNER_RECIPE alRcp = new ALIGNER_RECIPE();

        //임시 추가
        bool[] bLPM_Load = new bool[4];

        public ProcFM()
        {
            alRcp.nNo = 1;
            alRcp.bUseMode = true;
            alRcp.fCarrAngle = 281.2f;
            alRcp.fCarrAngle = 10.0f;

            GlobalVariable.WaferInfo.bWaferUnloadExist = new bool[COUNT.MAX_PORT, COUNT.MAX_PORT_SLOT];
            GlobalVariable.WaferInfo.nWaferLoadSlot = new int[COUNT.MAX_PORT];
            GlobalVariable.WaferInfo.nWaferUnloadSlot = new int[COUNT.MAX_PORT];

            for (int i = 0; i < COUNT.MAX_PORT; i++ )
            {
                GlobalVariable.WaferInfo.nWaferLoadSlot[i] = 0;
                GlobalVariable.WaferInfo.nWaferUnloadSlot[i] = 0;
            }
            for (int i = 0; i < COUNT.MAX_PORT_SLOT; i++)
            {
                GlobalVariable.WaferInfo.bWaferUnloadExist[0, i] = false;
                GlobalVariable.WaferInfo.bWaferUnloadExist[1, i] = false;
                GlobalVariable.WaferInfo.bWaferUnloadExist[2, i] = false;
                GlobalVariable.WaferInfo.bWaferUnloadExist[3, i] = false;
            }
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


        public void Run()
        {
            if (!isAcceptRun()) { return; }
            if (nSeqNo != nPreSeqNo) { resetCmd(); }
            nPreSeqNo = nSeqNo;

            CaseFM seqCase = (CaseFM)nSeqNo;

            //test
            int nRetV = 0;


            alwaysCheck();

            switch (seqCase)
            {
                case CaseFM.Initialze:
                    break;

                case CaseFM.Check_Interlock:

                    //임시 테스트, 웨이퍼 2장만 진행하기 위함
                    //if (nWaferCount >= 2) return;
                    strLog = seqCase.ToString();
                    AddMessage(strLog);
                    break;

                case CaseFM.Start_LPM_Load:

                    if(bLPM_Load[(int)EFEM.LPMA] && bLPM_Load[(int)EFEM.LPMC])
                    {
                        //이미 LPM 로드 상태면 진행하지 않는다
                        nextSeq((int)CaseFM.Check_Status);
                        return;
                    }
                    break;

                case CaseFM.Move_LPM_Load:

                    if(bLPM_Load[(int)EFEM.LPMA] == false)
                    {
                        //로드 전 Flag 초기화
                        LpmRoot.InitMappingFlag(EFEM.LPMA); 
                        LoadLPM(EFEM.LPMA);
                    }
                    if (bLPM_Load[(int)EFEM.LPMC] == false)
                    {
                        LpmRoot.InitMappingFlag(EFEM.LPMC);
                        LoadLPM(EFEM.LPMC);
                    }

                    Thread.Sleep(500); //Load Delay
                    break;

                case CaseFM.Compl_LPM_Load:
                    if (CheckLoad_LPM(EFEM.LPMA) != fn.success) return;
                    break;

                case CaseFM.End_LPM_Load:
                    if (CheckLoad_LPM(EFEM.LPMC) != fn.success) return;
                    break;

                case CaseFM.LPM_Mapping_Data:

                    //로드 시 Scan한 Map Data 연결
                    break;

                case CaseFM.Check_Status_Load_A:

                    //Status를 체크 할 경우 LPM Alarm이 발생함,
                    //테스트 시 제거 후 진행 함
                    //LpmRoot.GetStatus(EFEM.LPMA);
                    break;

                case CaseFM.Compl_Status_Load_A:
                    //if (CheckLoad_LPM(EFEM.LPMA) != fn.success) return;
                    break;

                case CaseFM.End_Status_Load_A:

                    //LPM Load 후 Mapping Flag가 변경되면 완료
                    if (LpmRoot.GetMappingComplete(EFEM.LPMA) == false) return;
                    if (LpmRoot.GetMappingComplete(EFEM.LPMC) == false) return;

                    bLPM_Load[(int)EFEM.LPMA] = true;
                    bLPM_Load[(int)EFEM.LPMC] = true;
                    break;

                case CaseFM.Check_Status:

                    RUN_MODE RunMode = RecipeMgr.Inst.TempRcp.eRunMode; //현재 레시피 
                    if(RunMode == RUN_MODE.FULL || RunMode == RUN_MODE.ONLY_BOND)
                    {
                        //FULL Mode
                        if (nWaferCount % 2 == 0)
                        {
                            Working_LPM = EFEM.LPMA;
                            efemType = EFEM_TYPE.A_CARRIER;
                        }
                        else
                        {
                            Working_LPM = EFEM.LPMC;
                            efemType = EFEM_TYPE.C_DEVICE;
                        }
                    }
                    else if(RunMode == RUN_MODE.ONLY_LAMI)
                    {
                        //Only Lami 일경우 Carrier 고정 사용
                        Working_LPM = EFEM.LPMA;
                        efemType = EFEM_TYPE.A_CARRIER;
                    }
                    else
                    {
                        return;
                    }

                    //Next Wafer Slot 유/무 확인
                    GlobalVariable.WaferInfo.nWaferLoadSlot[(int)Working_LPM] = LpmRoot.GetNextWaferSlot(Working_LPM, LPMStatus.Load, COUNT.MAX_PORT_SLOT);
                    if (GlobalVariable.WaferInfo.nWaferLoadSlot[(int)Working_LPM] < 0)
                    {
                        //Wafer Empty
                        //더이상 투입할 웨이퍼가 없을경우 언로딩 시퀀스로 보낸다
                        nextSeq((int)CaseFM.Check_Unload_Wafer);                        
                        return;
                    }
                    break;

                case CaseFM.Start_FM_Pickup_LPM:
                    //FM 로봇 상태 확인
                    //FM 로봇 웨이퍼 상태 확인
                    break;

                case CaseFM.Move_FM_Pickup_LPM:

                    fmStatus = FMStatus.LPM_LOAD;
                    Working_Stage = (FMStage)Working_LPM + 1; //현재 LPM을 작업 할 Stage로 변경
                    nWorking_Slot = GlobalVariable.WaferInfo.nWaferLoadSlot[(int)Working_LPM] +1; //현재 작업할 Wafer Slot
                    Working_Arm = ARM.UPPER;
                    MoveFmRobot(Working_Stage, nWorking_Slot, Working_Arm, fmStatus);

                    strLog = string.Format("FmRobot Pickup Move Start -> {0}, {1}, {2}", Working_Stage.ToString(), nWorking_Slot, Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseFM.Compl_FM_Pickup_LPM:
                    if (CheckCompl_FM() != fn.success) return;

                    //nRetV = CheckCompl_FM();
                    //f(nRetV == err )

                    strLog = string.Format("FmRobot Pickup Move End -> {0}, {1}, {2}", Working_Stage.ToString(), nWorking_Slot, Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseFM.End_FM_Pickup_LPM:
                    GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(Working_LPM, nWorking_Slot-1, false); //웨이퍼 맵핑 데이터 갱신
                    GlobalVariable.seqShared.LoadingEfemToFm(efemType, nWorking_Slot, (HAND)Working_Arm);

                    nWaferCount++;
                    break;

                case CaseFM.Waiting_AL:

                    if (GlobalVariable.seqShared.IsInAligner()
                        || GlobalVariable.interlock.bAlignMoving == true)
                    {
                        //Alinger에 Wafer가 아직 있으면 대기 하도록 하자
                        return;
                    }
                    break;

                case CaseFM.Start_FM_Place_AL:
                    break;

                case CaseFM.Move_FM_Place_AL:
                    fmStatus = FMStatus.BUFFER_LOAD;
                    Working_Stage = FMStage.AL;
                    nWorking_Slot = 1;
                    Working_Arm = ARM.UPPER;
                    MoveFmRobot(Working_Stage, nWorking_Slot, Working_Arm, fmStatus);

                    strLog = string.Format("FmRobot Place Move Start -> {0}, {1}, {2}", Working_Stage.ToString(), nWorking_Slot, Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseFM.Compl_FM_Place_AL:
                    if (CheckCompl_FM() != fn.success) return;

                    strLog = string.Format("FmRobot Place Move End -> {0}, {1}, {2}", Working_Stage.ToString(), nWorking_Slot, Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseFM.End_FM_Place_AL:
                    GlobalVariable.seqShared.LoadingFmToAligner((HAND)Working_Arm);
                    GlobalVariable.interlock.bAlignMoving = true; //얼라인 동작
                    break;

                case CaseFM.Check_Unload_Wafer:
                    //CP에 언로드 할 웨이퍼가 있는지 확인
                    if (GlobalVariable.seqShared.IsInCP(0) == false)
                    {
                        nextSeq((int)CaseFM.Initialze);
                        return;
                    }
                    break;

//////////////////////////////////////
//Wafer Unload                 
/////////////////////////////////////
                case CaseFM.Start_FM_Pickup_CP:
                    //CP 웨이퍼 감지 센서 확인 필요!
                    break;

                case CaseFM.Move_FM_Pickup_CP:
                    fmStatus = FMStatus.BUFFER_UNLOAD;
                    Working_Stage = FMStage.CP1;
                    nWorking_Slot = 1;
                    Working_Arm = ARM.LOWER; //CP에서 언로딩 시에는 LOW ARM 사용하자
                    MoveFmRobot(Working_Stage, nWorking_Slot, Working_Arm, fmStatus);

                    strLog = string.Format("FmRobot Pickup Move Start -> {0}, {1}, {2}", Working_Stage.ToString(), nWorking_Slot, Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseFM.Compl_FM_Pickup_CP:
                    if (CheckCompl_FM() != fn.success) return;

                    strLog = string.Format("FmRobot Pickup Move End -> {0}, {1}, {2}", Working_Stage.ToString(), nWorking_Slot, Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseFM.End_FM_Pickup_cp:
                    GlobalVariable.seqShared.LoadingCpToFm(0, (HAND)Working_Arm);
                    break;

                case CaseFM.Start_FM_Place_LPM:

                    //완료 웨이퍼 언로딩 시 LPM A부터 채워넣자
                    Working_LPM = EFEM.LPMA;

                    //LPM 상태를 확인하자
                    if (CheckLoad_LPM(Working_LPM) != fn.success)
                    {
                        //로드 상태가 아니면 에러
                        return;
                    }

                    //Unload Slot Check
                    //Next Wafer Slot 유/무 확인
                    GlobalVariable.WaferInfo.nWaferUnloadSlot[(int)Working_LPM] = LpmRoot.GetNextWaferSlot(Working_LPM, LPMStatus.Unload, COUNT.MAX_PORT_SLOT);
                    if (GlobalVariable.WaferInfo.nWaferUnloadSlot[(int)Working_LPM] < 0)
                    {
                        //Wafer Empty
                        return;
                    }

                    break;

                case CaseFM.Move_FM_Place_LPM:
                    fmStatus = FMStatus.LPM_UNLOAD;
                    Working_Stage = (FMStage)Working_LPM + 1;
                    nWorking_Slot = GlobalVariable.WaferInfo.nWaferUnloadSlot[(int)Working_LPM]+ 1; //현재 작업할 Wafer Slot
                    Working_Arm = ARM.LOWER; //CP에서 언로딩 시에는 LOW ARM 사용하자
                    MoveFmRobot(Working_Stage, nWorking_Slot, Working_Arm, fmStatus);

                    strLog = string.Format("FmRobot Place Move Start -> {0}, {1}, {2}", Working_Stage.ToString(), nWorking_Slot, Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseFM.Compl_FM_Place_LPM:
                    if (CheckCompl_FM() != fn.success) return;

                    strLog = string.Format("FmRobot Place Move End -> {0}, {1}, {2}", Working_Stage.ToString(), nWorking_Slot, Working_Arm.ToString());
                    AddMessage(strLog);
                    break;

                case CaseFM.End_FM_Place_LPM:

                    GlobalVariable.seqShared.LoadingFmToEfem(EFEM_TYPE.A_CARRIER, nWorking_Slot, (HAND)Working_Arm);

                    //Wafer Unload 후 Data 변경
                    GlobalSeq.autoRun.prcFM.LpmRoot.SetUnloadSlot(Working_LPM, nWorking_Slot-1, true);
                    nextSeq((int)CaseFM.Initialze);
                    return;


///////////////////////////////////
//LPM Unload
//////////////////////////////////

                case CaseFM.Start_LPM_Unload:
                    break;
                    
                case CaseFM.Move_LPM_Unload:
                    UnloadLPM(Working_LPM);
                    break;

                case CaseFM.Check_Status_Unload:
                    LpmRoot.GetStatus(Working_LPM);
                    break;

                case CaseFM.Compl_LPM_Unload:                   
                    if (CheckUnload_LPM(Working_LPM) != fn.success) return;
                    break;

                case CaseFM.End_LPM_Unload:

                    //처음 case로
                    nextSeq((int)CaseFM.Initialze); 
                    return;
            }

            nSeqNo++;
        }



        #region LPM Robot
        public bool CheckStatusWafer(ARM _Arm)
        {
            bool bRet = false;
            if (_Arm == ARM.UPPER)
                bRet = FmRobot.IsWaferUpper();
            else
                bRet =  FmRobot.IsWaferLower();

            return bRet;
        }

        public void LoadLPM(EFEM Lpm )
        {
            LpmRoot.MoveLoad(Lpm);
        }

        public void UnloadLPM(EFEM lpm)
        {
            LpmRoot.MoveUnload(lpm);
        }

        public fn CheckLoad_LPM(EFEM lpm)
        {
            if (LpmRoot.CheckLoad(lpm))
                return fn.success;
            else
            {
                if( LpmRoot.IsAlarmCheck(lpm) ) 
                    return fn.err;
                else
                    return fn.busy;
            }
        }

        public fn CheckUnload_LPM(EFEM lpm)
        {
            if (LpmRoot.CheckUnload(lpm))
                return fn.success;
            else
            {
                if (LpmRoot.IsAlarmCheck(lpm))
                    return fn.err;
                else
                    return fn.busy;
            }
        }

        #endregion


        #region FM Robot

        public void MoveFmRobot(FMStage stage, int nSlot, ARM _Arm, FMStatus status, bool bHandFold = false)
        {
            //int nSlot = 1;

            if (bHandFold == true)
            {
                FmRobot.MoveWaitFold((int)stage, nSlot, _Arm);
            }
            else
            {
                switch(stage)
                {
                    case FMStage.CP1:
                    case FMStage.CP2:
                    case FMStage.CP3:
                        FmRobot.MovePickup((int)stage, nSlot, _Arm);
                        break;

                    case FMStage.AL:
                        FmRobot.MovePlace((int)stage, nSlot, _Arm);
                        break;

                    case FMStage.LPMA:
                    case FMStage.LPMB:
                    case FMStage.LPMC:
                    case FMStage.LPMD:

                        if (status == FMStatus.LPM_LOAD)
                        {
                            FmRobot.MovePickup((int)stage, nSlot, _Arm);
                        }
                        else if (status == FMStatus.LPM_UNLOAD)
                        {
                            FmRobot.MovePlace((int)stage, nSlot, _Arm);
                        }
                       
                        break;
                }

            }
        }

        public fn CheckCompl_FM()
        {
            if (FmRobot.CheckComplete())
                return fn.success;
            else if (FmRobot.IsAlarmCheck())
                return fn.err;
            else
            {
                //타임 아웃 , 에러 체크
                return fn.busy;
            }
        }

        #endregion


    }
}
