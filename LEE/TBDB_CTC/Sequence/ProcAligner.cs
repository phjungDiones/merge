using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBDB_Handler;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;

namespace TBDB_CTC.Sequence
{
    public class ProcAligner
    {
        public MotionAlinger aligner = new MotionAlinger();

        private bool isCycleRun = false;
        public int nSeqNo = 0;
        private int nPreSeqNo = 0;

        public delegate void procAddMsgEvent(int nSeq, int nCaseNo, string strAddMsg);
        public event procAddMsgEvent procMsgEvent;

        public delegate void procFMErrorStopEvent(int nErrSafty);
        public event procFMErrorStopEvent errSaftyStopEvent;

        ProcInfoAlign AlignInfo;

        int nSeq = (int)UNIT.ALINGER;

        float m_AlignAngle = 0.0f;

        public ProcAligner()
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


        public void Run()
        {
            if (!isAcceptRun()) { return; }
            if (nSeqNo != nPreSeqNo) { resetCmd(); }
            nPreSeqNo = nSeqNo;

            alwaysCheck();
            switch (nSeqNo)
            {
                case 0:
                    if (GlobalVariable.interlock.bAlignMoving == false) return;
                    break;

                case 5:
                    if (GlobalVariable.seqShared.IsInAligner() == false) return;
                    break;

                case 10:
                    //웨이퍼 확인                  
                    break;

                case 15:
                    
                    if (GlobalVariable.seqShared.aligner.waferType == WaferType.CARRIER)
                    {
                        //현재 웨이퍼가 Carrier일 경우
                        AlignInfo = ProcessMgr.Inst.TempPinfo.listProcAlign.Single<ProcInfoAlign>(p => p.strProcName == RecipeMgr.Inst.TempRcp.strPreAlignCarrier);                       
                        m_AlignAngle = AlignInfo.dAngle; 
                    }
                    else if(GlobalVariable.seqShared.aligner.waferType == WaferType.DEVICE)
                    {
                        //Device
                        AlignInfo = ProcessMgr.Inst.TempPinfo.listProcAlign.Single<ProcInfoAlign>(p => p.strProcName == RecipeMgr.Inst.TempRcp.strPreAlignDevice);
                        m_AlignAngle = AlignInfo.dAngle;
                    }
                    else
                    {
                        //Type Error
                        return;
                    }

                    MoveAlign(m_AlignAngle);
                    break;

                case 20:
                    if (CheckCompl_Align() != fn.success) return;

                    break;

                case 25:
                    //Vacuum Off
                    aligner.VacuumOff();
                    break;

                case 30:
                    if (CheckCompl_Align() != fn.success) return;
                    break;

                case 35:                 
                    //얼라인 완료
                    
                    GlobalVariable.interlock.bAlignMoving = false; //완료
                    nextSeq(0);
                    return;

                default:
                    break;
            }
            nSeqNo++;
        }

        #region Aligner
        public void MoveAlign(float fAngle)
        {
            aligner.MoveAlign(fAngle);
        }

        public fn CheckCompl_Align()
        {
            if (aligner.CheckAlign())
                return fn.success;
            else if (aligner.IsAlarmCheck())
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
