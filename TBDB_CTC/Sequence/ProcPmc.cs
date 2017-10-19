using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_Handler;
using TBDB_Handler.DATA.UnitInfo;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;
using TBDB_Handler.SEQ;

namespace TBDB_CTC.Sequence
{
    public class ProcPmc
    {
        private bool isCycleRun = false;
        public int nSeqNo = 0;
        private int nPreSeqNo = 0;

        public delegate void procAddMsgEvent(int nSeq, int nCaseNo, string strAddMsg);
        public event procAddMsgEvent procMsgEvent;

        public delegate void procFMErrorStopEvent(int nErrSafty);
        public event procFMErrorStopEvent errSaftyStopEvent;

        int nSeq = (int)UNIT.PMC;
        int[] nSeq_PMC = new int[(int)Seq_PMC.PMC_Max];

        public PMC_RECIPE pmc_Rcp = new PMC_RECIPE(); //레시피 연결하자

        ProcInfoBond PmcInfo;

        string strLog;

        public ProcPmc()
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

        void AddProcMessage(string msg)
        {
            if (procMsgEvent != null)
                procMsgEvent(0, nSeqNo, msg);
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


            alwaysCheck();
            switch (nSeqNo)
            {

                case 0:
                    //프로세스 flag
                    if (GlobalVariable.interlock.bBondingProcess == false) return;

                    if (GlobalVariable.seqShared.IsInBonder() == false) return;

                    GlobalVariable.seqShared.stopWatch[(int)UNIT.PMC].Restart(); //시간 측정 시작

#if !_REAL_MC
                    Thread.Sleep(5000);
#endif
                    break;

                case 2:

                    if (SetRecipe(pmc_Rcp) != fn.success) return;
                    strLog = string.Format("PMC Set Recipe");
                    AddProcMessage(strLog);
                    break;

                case 4:
                    if (Process_PMC() != fn.success) return;
                    GlobalVariable.seqShared.Bonding();

                    //시간측정 종료
                    GlobalVariable.seqShared.stopWatch[(int)UNIT.PMC].Stop();
                    GlobalVariable.seqShared.aligner.strBondingTime = GlobalVariable.seqShared.stopWatch[(int)UNIT.PMC].Elapsed.ToString(@"hh\:mm\:ss");

                    strLog = string.Format("PMC Process End");
                    AddProcMessage(strLog);
                    break;

                case 6:
                    GlobalVariable.interlock.bBondingProcess = false; //Process 완료          
                    nextSeq(0);
                    return;

                default:
                    break;
            }
            nSeqNo++;
        }


        #region Interface_Manual

        public fn Init_PMC()
        {
            int nSeq = (int)Seq_PMC.PMC_Init;

            switch (nSeq_PMC[nSeq])
            {
                case 0:
                    break;

                case 10:
                    GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.InitReady, false);
                    GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.InitCompleteAck, false);
                    GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.InitReq, true); //Init Req On
                    return fn.busy;

                case 20:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.InitAck) == 1)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.InitReady, true); //Ready On
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 30:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.InitStart) == 1)
                    {
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 40:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.InitCompl) == 1
                        && GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.InitAck) == 0
                        && GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.InitStart) == 0)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.InitReq, false); //Init Req Off
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.InitReady, false); //Ready Off
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.InitCompleteAck, true); //Compl Ack On
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 50:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.InitCompl) == 0)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.InitCompleteAck, false); //Compl Ack Off
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
                    GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.StandbyReady, false);
                    GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.StandbyCompleteAck, false);
                    GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.StandbyReq, true);
                    return fn.busy;

                case 20:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.StandbyAck) == 1)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.StandbyReady, true);
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 30:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.StandbyStart) == 1)
                    {
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 40:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.StandbyCompl) == 1
                        && GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.StandbyAck) == 0
                        && GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.StandbyStart) == 0)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.StandbyReq, false);
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.StandbyReady, false); //Ready Off
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.StandbyCompleteAck, true); //Compl Ack On
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 50:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.StandbyCompl) == 0)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.StandbyCompleteAck, false); //Compl Ack Off
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
                    nSeq_PMC[nSeq]= 0;
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
                    GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.ProcessReady, false);
                    GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.ProcessCompleteAck, false);
                    GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.ProcessReq, true);
                    break;

                case 20:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.ProcAck) == 1)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.ProcessReady, true);
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 30:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.ProcStart) == 1)
                    {
                        break;
                    }
                    else
                    {
                        //타임 아웃 체크
                    }
                    return fn.busy;

                case 40:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.ProcCompl) == 1
                        && GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.ProcAck) == 0
                        && GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.ProcStart) == 0)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.ProcessReq, false);
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.ProcessReady, false); //Ready Off
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.ProcessCompleteAck, true); //Compl Ack On
                        break;
                    }
                    else
                    {

                    }
                    return fn.busy;

                case 50:
                    if (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.ProcCompl) == 0)
                    {
                        GlobalSeq.autoRun.prcVTM.Pmc.SetManualCmd(CTC_MANUAL.ProcessCompleteAck, false); //Compl Ack Off
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
                    nSeq_PMC[nSeq] = 0;
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

            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.VisionUsed, Convert.ToInt16(PmcInfo.bVisionUse));
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.Pressure, Convert.ToInt16(PmcInfo.dPressure * 100));
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.PressingTime, Convert.ToInt16(PmcInfo.dPressTimeSec * 10));
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.UpperTemp, Convert.ToInt16(PmcInfo.dUpperTemp * 10));
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.LowerTemp, Convert.ToInt16(PmcInfo.dLowerTemp * 10));
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.APCPosition, Convert.ToInt16(PmcInfo.dAPCPos * 10));
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.CH1Backlight, Convert.ToInt16(PmcInfo.dBacklightCH1 * 10));
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.CH2Backlight, Convert.ToInt16(PmcInfo.dBacklightCH2 * 10));
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.CH3Backlight, Convert.ToInt16(PmcInfo.dBacklightCH3 * 10));

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
    }
        #endregion
}
