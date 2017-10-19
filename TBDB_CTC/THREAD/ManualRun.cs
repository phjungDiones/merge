using CJ_Controls.Communication;
using CJ_Controls.Communication.QuadraVTM4;
using CJ_Controls.Communication.CybogRobot_HTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_CTC.Sequence;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;
using TBDB_Handler.SEQ;

namespace TBDB_Handler.THREAD
{
    public class ManualRun
    {

        public enum MANUAL_SEQ
        {
            NONE,

            FMRobot_Pickup_LPM,
            FMRobot_Place_LPM,
            FMRobot_Pickup_Buffer,
            FMRobot_Place_Buffer,

            ATMRobot_Pickup_Buffer,
            ATMRobot_Place_Buffer,
            ATMRobot_Pickup_Loadlock,
            ATMRobot_Place_Loadlock,       
            ATMRobot_Load_Lami,
            ATMRobot_Unload_Lami,

            //Lami Process
            ATMRobot_ProcStart_Lami,
            ATMRobot_ProcPause_Lami,
            ATMRobot_ProcResume_Lami,
           
            //Lami Manual Cmd
            ATMRobot_Init_Lami,
            ATMRobot_Standby_Lami,
            ATMRobot_RcpChange_Lami,


            VTMRobot_Pickup_Loadlock,
            VTMRobot_Place_Loadlock,

            VTMRobot_Load_PMC,
            VTMRobot_Unload_PMC,

            VTMRobot_Init_PMC,
            VTMRobot_Standby_PMC,
            VTMRobot_Process_PMC,
            VTMRobot_SetRecipe_PMC,
            VTMRobot_SetRcpStandby_PMC,

            //VTM, Loadlock Pumping,Venting

            VTM_Pumping,
            VTM_Venting,
            Loadlock_Pumping,
            Loadlock_Venting,

            FINISH,
        }

        Thread threadRun;

        MANUAL_SEQ manualSeq = MANUAL_SEQ.NONE;
        bool flagThreadAlive = false;

        public int nSeqNo = 0;
        private int nPreSeqNo = 0;
        private bool isCycleRun = false;

        CJ_Controls.Communication.CybogRobot_HTR.ARM FmRobot_Arm;
        CJ_Controls.Communication.CybogRobot_HTR.ARM AtmRobot_Arm;
        CJ_Controls.Communication.QuadraVTM4.ARM VtmRobot_Arm;


        public ManualRun()
        {
            StartStopManualThread(true);
        }

        ~ManualRun()
        {
            //
        }

        public void Dispose()
        {
            StartStopManualThread(false);
        }

        void StartStopManualThread(bool bStart)
        {
            if (threadRun != null)
            {
                flagThreadAlive = false;
                threadRun.Join(500);
                threadRun.Abort();
                threadRun = null;
            }

            if (bStart)
            {
                flagThreadAlive = true;
                threadRun = new Thread(new ParameterizedThreadStart(ThreadRun));
                threadRun.Name = "Manual Run THREAD";
                if (threadRun.IsAlive == false)
                    threadRun.Start(this);
            }
        }



        void ThreadRun(object obj)
        {
            //
            ManualRun manualRun = obj as ManualRun;

            while (manualRun.flagThreadAlive)
            {
                System.Threading.Thread.Sleep(10);

                switch (manualRun.manualSeq)
                {
                    case MANUAL_SEQ.NONE:
                        if (nSeqNo != 0)
                        {
                            nSeqNo = 0;
                        }
                        break;
                    case MANUAL_SEQ.FMRobot_Pickup_LPM:
                        if (manualRun.FmRobot_Pickup_LPM() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.FMRobot_Place_LPM:
                        if (manualRun.FmRobot_Place_LPM() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.FMRobot_Pickup_Buffer:
                        if (manualRun.FmRobot_Pickup_Buffer() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.FMRobot_Place_Buffer:
                        if (manualRun.FmRobot_Place_Buffer() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_Pickup_Buffer:
                        if (manualRun.AtmRobot_Pickup_Buffer() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_Place_Buffer:
                        if (manualRun.AtmRobot_Place_Buffer() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_Pickup_Loadlock:
                        if (manualRun.AtmRobot_Pickup_Loadlock() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_Place_Loadlock:
                        if (manualRun.AtmRobot_Place_Loadlock() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_Load_Lami:
                        if (manualRun.AtmRobot_Load_Lami() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_Unload_Lami:
                        if (manualRun.AtmRobot_Unload_Lami() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_ProcStart_Lami:
                        if (manualRun.AtmRobot_ProcStart_Lami() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_ProcPause_Lami:
                        if (manualRun.AtmRobot_ProcPause_Lami() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_ProcResume_Lami:
                        if (manualRun.AtmRobot_ProcResume_Lami() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_Init_Lami:
                        if (manualRun.AtmRobot_Init_Lami() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_Standby_Lami:
                        if (manualRun.AtmRobot_Standby_Lami() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.ATMRobot_RcpChange_Lami:
                        if (manualRun.AtmRobot_RcpChange_Lami() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTMRobot_Pickup_Loadlock:
                        if (manualRun.VtmRobot_Pickup_Loadlock() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTMRobot_Place_Loadlock:
                        if (manualRun.VtmRobot_Place_Loadlock() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTMRobot_Load_PMC:
                        if (manualRun.VtmRobot_Load_PMC() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTMRobot_Unload_PMC:
                        if (manualRun.VtmRobot_Unload_PMC() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTMRobot_Init_PMC:
                        if (manualRun.VtmRobot_Init_PMC() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTMRobot_Standby_PMC:
                        if (manualRun.VtmRobot_Standby_PMC() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTMRobot_Process_PMC:
                        if (manualRun.VtmRobot_Process_PMC() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTMRobot_SetRecipe_PMC:
                        if (manualRun.VtmRobot_SetRecipe_PMC() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTMRobot_SetRcpStandby_PMC:
                        if (manualRun.VtmRobot_SetRcp_Standby_PMC() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTM_Pumping:
                        if (manualRun.Vtm_Pumping() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.VTM_Venting:
                        if (manualRun.Vtm_Venting() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.Loadlock_Pumping:
                        if (manualRun.Loadlock_Pumping() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;

                    case MANUAL_SEQ.Loadlock_Venting:
                        if (manualRun.Loadlock_Venting() != fn.success) continue;
                        manualRun.manualSeq = MANUAL_SEQ.FINISH;
                        break;


                    case MANUAL_SEQ.FINISH:
                        manualRun.manualSeq = MANUAL_SEQ.NONE;

//                         if (OnFinishSeq != null)
//                             OnFinishSeq((int)fn.success);
//                         OnFinishSeq = null;
                        GlobalVariable.mcState.isManualRun = false;
                        break;

                    default:
                        break;
                }

                System.Threading.Thread.Sleep(10);
            }
        }


        public bool StartManualSeq( MANUAL_SEQ Seq)
        {
            if (manualSeq != MANUAL_SEQ.NONE)
                return false;

            manualSeq = Seq;
            GlobalVariable.mcState.isManualRun = true;
            return true;
        }



        #region Common Function
        private bool isAcceptRun()
        {
            if (!GlobalVariable.mcState.isManualRun) return false;
            //if (GlobalVariable.mcState.isRun || !GlobalVariable.mcState.isManualRun || isCycleRun) return false;
            //if (!GlobalVariable.mcState.isRdy) return false;
            return true;
        }

        private void nextSeq(int nSeq)
        {
            if (nSeq >= 0) { nSeqNo = nSeq; }
        }

        #endregion Common Function


        #region FMRobot Func
        public fn FmRobot_Pickup_LPM()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            FMStage stage = GlobalVariable.manualInfo.mnlStageFM; //매뉴얼에서 선택한 Stage 연결
            int nSlot = GlobalVariable.manualInfo.nSelectSource;
            FmRobot_Arm = (CJ_Controls.Communication.CybogRobot_HTR.ARM)GlobalVariable.manualInfo.SelArmFM;
            EFEM Lpm = EFEM.LPMA;
            FMStatus fmStatus = FMStatus.LPM_LOAD;

            //LPM 구분
            switch(stage)
            {
                case FMStage.LPMA: Lpm = EFEM.LPMA; break;
                case FMStage.LPMB: Lpm = EFEM.LPMB; break;
                case FMStage.LPMC: Lpm = EFEM.LPMC; break;
                case FMStage.LPMD: Lpm = EFEM.LPMD; break;
                default:
                    //LPM Index Error
                    return fn.err;
            }

            switch (nSeqNo)
            {
                case 0:
                    //GlobalSeq.autoRun.prcFM.LoadLPM(Lpm);
                    break;
                case 10:
                    if (GlobalSeq.autoRun.prcFM.CheckLoad_LPM(Lpm) != fn.success) return fn.busy;
                    break;
                case 15:
                    //Door Open Check

                    break;
                case 20:
                    GlobalSeq.autoRun.prcFM.MoveFmRobot(stage, nSlot, FmRobot_Arm, fmStatus);
                    break;

                case 30:
                    if (GlobalSeq.autoRun.prcFM.CheckCompl_FM() != fn.success) return fn.busy;
                    break;

                case 35:
                    GlobalSeq.autoRun.prcFM.MoveFmRobot(stage, nSlot, FmRobot_Arm, fmStatus, true);
                    break;

                case 40:
                    if (GlobalSeq.autoRun.prcFM.CheckCompl_FM() != fn.success) return fn.busy;
                    break;
 
                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn FmRobot_Place_LPM()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            FMStage stage = GlobalVariable.manualInfo.mnlStageFM; //매뉴얼에서 선택한 Stage 연결
            int nSlot = GlobalVariable.manualInfo.nSelectSource;
            FmRobot_Arm = (CJ_Controls.Communication.CybogRobot_HTR.ARM)GlobalVariable.manualInfo.SelArmFM;
            EFEM Lpm = EFEM.LPMA;
            FMStatus fmStatus = FMStatus.LPM_UNLOAD;

            //LPM 구분
            switch (stage)
            {
                case FMStage.LPMA: Lpm = EFEM.LPMA; break;
                case FMStage.LPMB: Lpm = EFEM.LPMB; break;
                case FMStage.LPMC: Lpm = EFEM.LPMC; break;
                case FMStage.LPMD: Lpm = EFEM.LPMD; break;
                default:
                    //LPM Index Error
                    return fn.err;
            }

            switch (nSeqNo)
            {
                case 0:
                    break;
                case 10:
                    if (GlobalSeq.autoRun.prcFM.CheckUnload_LPM(Lpm) != fn.success) return fn.busy;
                    break;
                case 15:
                    //Door Open Check

                    break;
                case 20:
                    GlobalSeq.autoRun.prcFM.MoveFmRobot(stage, nSlot, FmRobot_Arm, fmStatus);
                    break;

                case 30:
                    if (GlobalSeq.autoRun.prcFM.CheckCompl_FM() != fn.success) return fn.busy;
                    break;

                case 35:
                    GlobalSeq.autoRun.prcFM.MoveFmRobot(stage, nSlot, FmRobot_Arm, fmStatus, true);
                    break;

                case 40:
                    if (GlobalSeq.autoRun.prcFM.CheckCompl_FM() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn FmRobot_Pickup_Buffer()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            FMStage stage = GlobalVariable.manualInfo.mnlStageFM; //매뉴얼에서 선택한 Stage 연결
            int nSlot = GlobalVariable.manualInfo.nSelectSource;
            FmRobot_Arm = (CJ_Controls.Communication.CybogRobot_HTR.ARM)GlobalVariable.manualInfo.SelArmFM;
            FMStatus fmStatus = FMStatus.BUFFER_LOAD;


            switch (nSeqNo)
            {
                case 0:
                    break;
                case 10:
                    break;
                case 15:
                    //Door Open Check

                    break;
                case 20:
                    GlobalSeq.autoRun.prcFM.MoveFmRobot(stage, nSlot, FmRobot_Arm, fmStatus);
                    break;

                case 30:
                    if (GlobalSeq.autoRun.prcFM.CheckCompl_FM() != fn.success) return fn.busy;
                    break;

                case 35:
                    GlobalSeq.autoRun.prcFM.MoveFmRobot(stage, nSlot, FmRobot_Arm, fmStatus, true);
                    break;

                case 40:
                    if (GlobalSeq.autoRun.prcFM.CheckCompl_FM() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn FmRobot_Place_Buffer()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            FMStage stage = GlobalVariable.manualInfo.mnlStageFM; //매뉴얼에서 선택한 Stage 연결
            int nSlot = GlobalVariable.manualInfo.nSelectSource;
            FmRobot_Arm = (CJ_Controls.Communication.CybogRobot_HTR.ARM)GlobalVariable.manualInfo.SelArmFM;
            FMStatus fmStatus = FMStatus.BUFFER_UNLOAD;

            switch (nSeqNo)
            {
                case 0:
                    break;
                case 10:
                    break;
                case 15:
                    //Door Open Check

                    break;
                case 20:
                    GlobalSeq.autoRun.prcFM.MoveFmRobot(stage, nSlot, FmRobot_Arm, fmStatus);
                    break;

                case 30:
                    if (GlobalSeq.autoRun.prcFM.CheckCompl_FM() != fn.success) return fn.busy;
                    break;

                case 35:
                    GlobalSeq.autoRun.prcFM.MoveFmRobot(stage, nSlot, FmRobot_Arm, fmStatus, true);
                    break;

                case 40:
                    if (GlobalSeq.autoRun.prcFM.CheckCompl_FM() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        #endregion

        #region ATMRobot Func

        public fn AtmRobot_Pickup_Buffer()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            AtmStage stage = GlobalVariable.manualInfo.mnlStageATM; //매뉴얼에서 선택한 Stage 연결
            int nSlot = GlobalVariable.manualInfo.nSelectSource;
            AtmRobot_Arm = (CJ_Controls.Communication.CybogRobot_HTR.ARM)GlobalVariable.manualInfo.SelArmATM;

            switch (nSeqNo)
            {
                case 0:
                    break;
                case 10:
                    break;
                case 15:

                    break;
                case 20:
                    GlobalSeq.autoRun.prcATM.MoveRobot(stage, AtmRobot_Arm);
                    break;

                case 30:
                    if (GlobalSeq.autoRun.prcATM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 35:
                    GlobalSeq.autoRun.prcATM.MoveRobot(stage, AtmRobot_Arm, true);
                    break;

                case 40:
                    if (GlobalSeq.autoRun.prcATM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_Place_Buffer()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            AtmStage stage = GlobalVariable.manualInfo.mnlStageATM; //매뉴얼에서 선택한 Stage 연결
            int nSlot = GlobalVariable.manualInfo.nSelectSource;
            AtmRobot_Arm = (CJ_Controls.Communication.CybogRobot_HTR.ARM)GlobalVariable.manualInfo.SelArmATM;

            switch (nSeqNo)
            {
                case 0:
                    break;
                case 10:
                    break;
                case 15:
                    break;
                case 20:
                    GlobalSeq.autoRun.prcATM.MoveRobot(stage, AtmRobot_Arm);
                    break;

                case 30:
                    if (GlobalSeq.autoRun.prcATM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 35:
                    GlobalSeq.autoRun.prcATM.MoveRobot(stage, AtmRobot_Arm, true);
                    break;

                case 40:
                    if (GlobalSeq.autoRun.prcATM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_Pickup_Loadlock()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            AtmStage stage = GlobalVariable.manualInfo.mnlStageATM; //매뉴얼에서 선택한 Stage 연결
            int nSlot = GlobalVariable.manualInfo.nSelectSource;
            AtmRobot_Arm = (CJ_Controls.Communication.CybogRobot_HTR.ARM)GlobalVariable.manualInfo.SelArmATM;

            switch (nSeqNo)
            {
                case 0:
                    break;
                case 10:
                    break;
                case 15:

                    break;
                case 20:
                    GlobalSeq.autoRun.prcATM.MoveRobot(stage, AtmRobot_Arm);
                    break;

                case 30:
                    if (GlobalSeq.autoRun.prcATM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 35:
                    GlobalSeq.autoRun.prcATM.MoveRobot(stage, AtmRobot_Arm, true);
                    break;

                case 40:
                    if (GlobalSeq.autoRun.prcATM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_Place_Loadlock()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            AtmStage stage = GlobalVariable.manualInfo.mnlStageATM; //매뉴얼에서 선택한 Stage 연결
            int nSlot = GlobalVariable.manualInfo.nSelectSource;
            AtmRobot_Arm = (CJ_Controls.Communication.CybogRobot_HTR.ARM)GlobalVariable.manualInfo.SelArmATM;

            switch (nSeqNo)
            {
                case 0:
                    break;
                case 10:
                    break;
                case 15:
                    break;
                case 20:
                    GlobalSeq.autoRun.prcATM.MoveRobot(stage, AtmRobot_Arm);
                    break;

                case 30:
                    if (GlobalSeq.autoRun.prcATM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 35:
                    GlobalSeq.autoRun.prcATM.MoveRobot(stage, AtmRobot_Arm, true);
                    break;

                case 40:
                    if (GlobalSeq.autoRun.prcATM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_Load_Lami()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            AtmStage stage = GlobalVariable.manualInfo.mnlStageATM; //매뉴얼에서 선택한 Stage 연결
            int nSlot = GlobalVariable.manualInfo.nSelectSource;
            AtmRobot_Arm = (CJ_Controls.Communication.CybogRobot_HTR.ARM)GlobalVariable.manualInfo.SelArmATM;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 10:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcATM.Load_Lami(AtmRobot_Arm) != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_Unload_Lami()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            AtmStage stage = GlobalVariable.manualInfo.mnlStageATM; //매뉴얼에서 선택한 Stage 연결
            int nSlot = GlobalVariable.manualInfo.nSelectSource;
            AtmRobot_Arm = (CJ_Controls.Communication.CybogRobot_HTR.ARM)GlobalVariable.manualInfo.SelArmATM;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 10:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcATM.Unload_Lami(AtmRobot_Arm) != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_ProcStart_Lami()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcATM.Process_Start()!= fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_ProcPause_Lami()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcATM.Process_Pause() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_ProcResume_Lami()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcATM.Process_Resume() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_Init_Lami()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcATM.Init_Lami() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_Standby_Lami()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcATM.Standby_Lami() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn AtmRobot_RcpChange_Lami()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcATM.RcpChange_Lami() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }
        #endregion

        #region VTMRobot Func

        public fn VtmRobot_Pickup_Loadlock()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            //VTM은 Up핸드로만 LL픽업 고정
            VtmRobot_Arm = CJ_Controls.Communication.QuadraVTM4.ARM.UPPER;
            VtmStage stage = GlobalVariable.manualInfo.mnlStageVTM; //매뉴얼에서 선택한 Stage 연결
            
            switch (nSeqNo)
            {
                case 0:
                    break;
                case 10:
                    //GlobalSeq.autoRun.procLoadlock.Move(MotionPos.Pos3);
                    GlobalSeq.autoRun.procLoadlock.MoveExit((int)MotionPos.Pos3);
                    break;

                case 15:
                    //if (GlobalSeq.autoRun.procLoadlock.CheckComplete() != fn.success) return fn.busy;
                    if (GlobalSeq.autoRun.procLoadlock.CheckMoveDone((int)MotionPos.Pos3) == false) return fn.busy;
                    break;

                case 20:
                    GlobalSeq.autoRun.prcVTM.MoveRobot(stage, VtmRobot_Arm);
                    break;

                case 30:
                    if (GlobalSeq.autoRun.prcVTM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 35:
                    GlobalSeq.autoRun.prcVTM.MoveRobot(stage, VtmRobot_Arm, true);
                    break;

                case 40:
                    if (GlobalSeq.autoRun.prcVTM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn VtmRobot_Place_Loadlock()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            //VTM은 Up핸드로만 LL플레이스 고정
            VtmRobot_Arm = CJ_Controls.Communication.QuadraVTM4.ARM.LOWER;
            VtmStage stage = GlobalVariable.manualInfo.mnlStageVTM; //매뉴얼에서 선택한 Stage 연결

            switch (nSeqNo)
            {
                case 0:
                    break;
                case 10:
                    //BD Place -> Unload
                    //GlobalSeq.autoRun.procLoadlock.Move(MotionPos.Pos1);
                    GlobalSeq.autoRun.procLoadlock.MoveExit((int)MotionPos.Pos1);
                    break;

                case 15:
                    //if (GlobalSeq.autoRun.procLoadlock.CheckComplete() != fn.success) return fn.busy;
                    if (GlobalSeq.autoRun.procLoadlock.CheckMoveDone((int)MotionPos.Pos1) == false) return fn.busy;
                    break;

                case 20:
                    GlobalSeq.autoRun.prcVTM.MoveRobot(stage, VtmRobot_Arm);
                    break;

                case 30:
                    if (GlobalSeq.autoRun.prcVTM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 35:
                    GlobalSeq.autoRun.prcVTM.MoveRobot(stage, VtmRobot_Arm, true);
                    break;

                case 40:
                    if (GlobalSeq.autoRun.prcVTM.CheckComplete() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn VtmRobot_Load_PMC()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            //PMC Load시 Up핸드 고정
            VtmRobot_Arm = CJ_Controls.Communication.QuadraVTM4.ARM.UPPER; 

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 10:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcVTM.Load_PMC(VtmRobot_Arm) != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn VtmRobot_Unload_PMC()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            //PMC Load시 Low핸드 고정
            VtmRobot_Arm = CJ_Controls.Communication.QuadraVTM4.ARM.LOWER;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 10:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcVTM.Unload_PMC(VtmRobot_Arm) != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn VtmRobot_Init_PMC()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.procPMC.Init_PMC() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn VtmRobot_Standby_PMC()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.procPMC.Standby_PMC() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn VtmRobot_Process_PMC()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.procPMC.Process_PMC() != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn VtmRobot_SetRecipe_PMC()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    if (GlobalSeq.autoRun.procPMC.SetRecipe(GlobalSeq.autoRun.procPMC.pmc_Rcp) != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn VtmRobot_SetRcp_Standby_PMC()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            PMC_RECIPE pmc_Rcp = new PMC_RECIPE();
            pmc_Rcp.dStandby_LowTemp = 1; //데이터 연결 하자
            pmc_Rcp.dStandby_UpTemp = 1;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 20:
                    //if (GlobalSeq.autoRun.prcVTM.SetRecipe_Standby(pmc_Rcp) != fn.success) return fn.busy;
                    break;

                case 50:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }
        #endregion


        #region Pumping Func

        public fn Vtm_Pumping()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            fn fRet = fn.busy;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 10:
                    break;

                case 20:
                    fRet = GlobalSeq.autoRun.procLoadlock.VTM_Pumping();
                    if (fRet == fn.success) break;
                    else
                    {
                        if( fRet == fn.err)
                        {
                            return fn.err;
                        }
                    }
                    return fn.busy;
                case 30:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn Vtm_Venting()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            fn fRet = fn.busy;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 10:
                    break;

                case 20:
                    fRet = GlobalSeq.autoRun.procLoadlock.VTM_Venting();
                    if (fRet == fn.success) break;
                    else
                    {
                        if (fRet == fn.err)
                        {
                            return fn.err;
                        }
                    }
                    return fn.busy;
                case 30:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }

        public fn Loadlock_Pumping()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            fn fRet = fn.busy;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 10:
                    break;

                case 20:
                    fRet = GlobalSeq.autoRun.procLoadlock.Loadlock_Pumping();
                    if (fRet == fn.success) break;
                    else
                    {
                        if (fRet == fn.err)
                        {
                            return fn.err;
                        }
                    }
                    return fn.busy;
                case 30:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }


        public fn Loadlock_Venting()
        {
            if (!isAcceptRun()) { return fn.busy; }
            nPreSeqNo = nSeqNo;

            fn fRet = fn.busy;

            switch (nSeqNo)
            {
                case 0:
                    break;

                case 10:
                    break;

                case 20:
                    fRet = GlobalSeq.autoRun.procLoadlock.Loadlock_Venting();
                    if (fRet == fn.success) break;
                    else
                    {
                        if (fRet == fn.err)
                        {
                            //에러 일경우 Output 출력을 Off하자

                            return fn.err;
                        }
                    }
                    return fn.busy;
                case 30:
                    nSeqNo = 0;
                    return fn.success;
            }

            //wrong seq check
            if (nSeqNo > 10000)
            {
                //error occur
                return fn.err;
            }
            nSeqNo++;

            return fn.busy;
        }
        #endregion
    }


}
