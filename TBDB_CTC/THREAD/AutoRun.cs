using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_CTC.Sequence;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.SEQ;

namespace TBDB_Handler.THREAD
{
    public class AutoRun : IDisposable
    {
        public ProcATM prcATM = new ProcATM();
        public ProcFM prcFM = new ProcFM();
        public ProcVTM prcVTM = new ProcVTM();
        public ProcLoadlock procLoadlock = new ProcLoadlock();
        public ProcPmc procPMC = new ProcPmc();
        public ProcAligner prcAL = new ProcAligner();
        public ProcStatus procStatus = new ProcStatus();

        public enum Sequence
        {
            SeqFM,
            SeqATM,
            SeqVTM,
            SeqAL,
            SeqLami,
            SeqLoadlock,
            SeqPmc,
            SeqBuffer,
            SeqReadStatus,
            SeqMax,
        }

        enum AUTO_MODE
        {
            NONE,
            AUTO_RUN,
            DRY_RUN,
            FINISH,
        }

        Thread[] threadRun = new Thread[(int)Sequence.SeqMax];
        bool[] flagThreadAlive = new bool[(int)Sequence.SeqMax];

        public AutoRun()
        {
            //for (int i = 0; i < (int)Sequence.SeqMax; i++ )
            //{
            //    StartStopAutoRunThread((Sequence)i, true);
            //}

            StartStopAutoRunThread(Sequence.SeqFM, true);
            StartStopAutoRunThread(Sequence.SeqATM, true);
            StartStopAutoRunThread(Sequence.SeqVTM, true);
            StartStopAutoRunThread(Sequence.SeqAL, true);
            StartStopAutoRunThread(Sequence.SeqLoadlock, true); //Loadlock 
            StartStopAutoRunThread(Sequence.SeqPmc, true);

            //PMC, VTM, Loadlock Vacuum Status 확인
            StartStopAutoRunThread(Sequence.SeqReadStatus, true);
        }

        ~AutoRun()
        {
            //
        }

        public void Dispose()
        {
            //for (int i = 0; i < (int)Sequence.SeqMax; i++ )
            //{
            //    StartStopAutoRunThread((Sequence)i, false);
            //}

            StartStopAutoRunThread(Sequence.SeqFM, false);
            StartStopAutoRunThread(Sequence.SeqATM, false);
            StartStopAutoRunThread(Sequence.SeqVTM, false);
            StartStopAutoRunThread(Sequence.SeqAL, false);
            StartStopAutoRunThread(Sequence.SeqLoadlock, false); //Loadlock 
            StartStopAutoRunThread(Sequence.SeqPmc, false);

            StartStopAutoRunThread(Sequence.SeqReadStatus, false);
        }

        public void ResetCmd()
        {
            prcATM.resetCmd();
            prcFM.resetCmd();
            prcVTM.resetCmd();
            prcAL.resetCmd();
        }

        public void InitSeqNum()
        {
            prcATM.nSeqNo = 0;
            prcFM.nSeqNo = 0;
            prcVTM.nSeqNo = 0;
            prcAL.nSeqNo = 0;
        }

        void StartStopAutoRunThread(Sequence sequence, bool bStart)
        {
            int nSeq = (int)sequence;

            if (threadRun[nSeq] != null)
            {
                flagThreadAlive[nSeq] = false;
                threadRun[nSeq].Join(500);
                threadRun[nSeq].Abort();
                threadRun[nSeq] = null;
            }

            if (bStart)
            {
                flagThreadAlive[nSeq] = true;
                switch (sequence)
                {
                    case Sequence.SeqFM:
                        threadRun[nSeq] = new Thread(new ParameterizedThreadStart(ThreadRun_FM));
                        threadRun[nSeq].Name = "FM Run THREAD";
                        break;

                    case Sequence.SeqATM:
                        threadRun[nSeq] = new Thread(new ParameterizedThreadStart(ThreadRun_ATM));
                        threadRun[nSeq].Name = "ATM Run THREAD";
                        break;

                    case Sequence.SeqVTM:
                        threadRun[nSeq] = new Thread(new ParameterizedThreadStart(ThreadRun_VTM));
                        threadRun[nSeq].Name = "VTM Run THREAD";
                        break;

                    case Sequence.SeqAL:
                        threadRun[nSeq] = new Thread(new ParameterizedThreadStart(ThreadRun_AL));
                        threadRun[nSeq].Name = "AL Run THREAD";
                        break;

                    case Sequence.SeqLoadlock:
                        threadRun[nSeq] = new Thread(new ParameterizedThreadStart(ThreadRun_Loadlock));
                        threadRun[nSeq].Name = "AL Run THREAD";
                        break;

                    case Sequence.SeqPmc:
                        threadRun[nSeq] = new Thread(new ParameterizedThreadStart(ThreadRun_PMC));
                        threadRun[nSeq].Name = "Pmc Run THREAD";
                        break;

                    case Sequence.SeqReadStatus:
                        threadRun[nSeq] = new Thread(new ParameterizedThreadStart(ThreadRun_Vacuum));
                        threadRun[nSeq].Name = "Vacuum Status THREAD";
                        break;

                    default:
                        break;
                }

                if (threadRun[nSeq].IsAlive == false)
                    threadRun[nSeq].Start(this);
            }
        }

        void ThreadRun_Vacuum(object obj)
        {
            AutoRun autoRun = obj as AutoRun;
            while (autoRun.flagThreadAlive[(int)Sequence.SeqPmc])
            {
                Thread.Sleep(10);
                //if (!GlobalVariable.mcState.isRdy) continue;

                autoRun.procStatus.CheckStatus_VtmVacuum();
            }
            autoRun.flagThreadAlive[(int)Sequence.SeqPmc] = false;
        }

        void ThreadRun_PMC(object obj)
        {
            AutoRun autoRun = obj as AutoRun;
            while (autoRun.flagThreadAlive[(int)Sequence.SeqPmc])
            {
                Thread.Sleep(10);
                if (!GlobalVariable.mcState.isRdy) continue;

                autoRun.procPMC.Run();
            }
            autoRun.flagThreadAlive[(int)Sequence.SeqPmc] = false;
        }

        void ThreadRun_Loadlock(object obj)
        {
            AutoRun autoRun = obj as AutoRun;
            while (autoRun.flagThreadAlive[(int)Sequence.SeqLoadlock])
            {
                Thread.Sleep(10);

                autoRun.procLoadlock.Run();
            }
            autoRun.flagThreadAlive[(int)Sequence.SeqLoadlock] = false;
        }

        void ThreadRun_AL(object obj)
        {
            AutoRun autoRun = obj as AutoRun;
            while (autoRun.flagThreadAlive[(int)Sequence.SeqAL])
            {
                Thread.Sleep(10);
                if (!GlobalVariable.mcState.isRdy) continue;

                //Thread.Sleep(50);
                autoRun.prcAL.Run();
            }
            autoRun.flagThreadAlive[(int)Sequence.SeqAL] = false;
        }

        void ThreadRun_FM(object obj)
        {
            AutoRun autoRun = obj as AutoRun;
            while (autoRun.flagThreadAlive[(int)Sequence.SeqFM])
            {
                Thread.Sleep(10);
                if (!GlobalVariable.mcState.isRdy) continue;

                //Thread.Sleep(50);
                autoRun.prcFM.Run();
            }
            autoRun.flagThreadAlive[(int)Sequence.SeqFM] = false;
        }

        void ThreadRun_ATM(object obj)
        {
            AutoRun autoRun = obj as AutoRun;
            while (autoRun.flagThreadAlive[(int)Sequence.SeqATM])
            {
                Thread.Sleep(10);
                if (!GlobalVariable.mcState.isRdy) continue;

                //Thread.Sleep(50);
                autoRun.prcATM.Run();
            }
            autoRun.flagThreadAlive[(int)Sequence.SeqATM] = false;
        }

        void ThreadRun_VTM(object obj)
        {
            AutoRun autoRun = obj as AutoRun;
            while (autoRun.flagThreadAlive[(int)Sequence.SeqVTM])
            {
                Thread.Sleep(10);
                if (!GlobalVariable.mcState.isRdy) continue;

                //Thread.Sleep(50);
                autoRun.prcVTM.Run();
            }
            autoRun.flagThreadAlive[(int)Sequence.SeqVTM] = false;
        }
    }
}
