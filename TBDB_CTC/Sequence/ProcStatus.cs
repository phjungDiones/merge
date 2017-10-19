using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;

namespace TBDB_CTC.Sequence
{


    public class ProcStatus
    {
        private bool isCycleRun = false;
        public int nSeqNo = 0;
        private int nPreSeqNo = 0;

        public delegate void procAddMsgEvent(int nSeq, int nCaseNo, string strAddMsg);
        public event procAddMsgEvent procMsgEvent;

        public delegate void procFMErrorStopEvent(int nErrSafty);
        public event procFMErrorStopEvent errSaftyStopEvent;

        int nSeq = 0;

        public ProcStatus()
        {

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


        //VTM Vacuum 상태 확인 후 PMC Interface
        public void CheckStatus_VtmVacuum()

        {
            alwaysCheck();


            if(GlobalVariable.Status_Inter.VacuumVtmStatus == VTM_VACUUM_STATUS_VALUE.VENTING )
            {
                //벤팅 동작 중일 경우 체크 안함
                return;
            }
            else if(GlobalVariable.Status_Inter.VacuumVtmStatus == VTM_VACUUM_STATUS_VALUE.PUMPING)
            {
                //벤팅 동작 중일 경우 체크 안함
                return;
            }
            else
            {
                //현재 Pumping 및 Venting 동작이 아닐 경우 상태 체크

                //현재 VTM Vacuum 상태를 확인
                if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_2) == false
                    && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_1) == false
                    && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTM_ATMSwtich1) == true)
                {
                    GlobalVariable.Status_Inter.VacuumVtmStatus = VTM_VACUUM_STATUS_VALUE.ATM;
                }
                else if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_2) == true
                    && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_1) == true
                    && GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTM_ATMSwtich1) == false)
                {
                    GlobalVariable.Status_Inter.VacuumVtmStatus = VTM_VACUUM_STATUS_VALUE.VTM;
                }
                else
                {
                    //상태 알수 없음
                    return;
                }

                //Send to Pmc
                GlobalSeq.autoRun.prcVTM.Pmc.SetStatus(CTC_STATUS.VTMStatus, (short)GlobalVariable.Status_Inter.VacuumVtmStatus);
            }

            if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_TM_MainPump_ValueOpen) == true
                && GlobalVariable.io.ReadInput(GlobalVariable.io.I_TM_MainPump_ValueClose) == false)
            {
                GlobalSeq.autoRun.prcVTM.Pmc.SetStatusSig(CTC_StatusSig.FastPumpStatus, true);
            }
            else
            {
                GlobalSeq.autoRun.prcVTM.Pmc.SetStatusSig(CTC_StatusSig.FastPumpStatus, false);
            }

            if (GlobalSeq.autoRun.prcVTM.Pmc.GetInterlock(PMC_INTERLOCK.FastPumpChangeReq) == 1 && GlobalVariable.Status_Inter.VacuumVtmStatus == VTM_VACUUM_STATUS_VALUE.VTM)
            {
                bool bSetValue = GlobalSeq.autoRun.prcVTM.Pmc.GetInterlock(PMC_INTERLOCK.FastPumpOpenClose) == 1 ? true : false;
                GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VacuumTM_MainPump_VV, bSetValue);
            }

        }
    }
}
