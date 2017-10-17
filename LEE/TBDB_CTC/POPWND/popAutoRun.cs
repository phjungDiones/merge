using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.POPWND
{
    public partial class popAutoRun : Form
    {
        //Start, Stop Event
        public delegate void startStopDelegate(int nState);
        public event startStopDelegate RunStopEvent;

        public popAutoRun()
        {
            InitializeComponent();
        }

        private void popAutoRun_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GlobalVariable.mcState.isRdy = true;
            GlobalVariable.mcState.isRun = true;             
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            GlobalVariable.mcState.isRun = false;   
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            GlobalVariable.mcState.isRun = false;   
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            GlobalVariable.mcState.isRun = false;   
        }

        void Autorun(int nRunMode)
        {
            if (nRunMode == MCDF.CMD_RUN)
            {
                StartProcess();
            }
            else if (nRunMode == MCDF.CMD_STOP)
            {
                StopProcess();
            }
        }

        void StartProcess()
        {
            if (!GlobalVariable.mcState.isRdy) return;
            if (GlobalVariable.mcState.isErr) return;
            if (GlobalVariable.mcState.isInitializing) return;
            if (GlobalVariable.mcState.isWateForStop) return;
            //if (GlobalVariable.g_nScreenNo != DF.SCREEN_AUTO) return;
            if (GlobalVariable.mcState.isMovingStopPos) return;

            GlobalSeq.autoRun.ResetCmd();

            //통신 연결 상태 확인
            //로봇 알람, Status 확인
            
            GlobalVariable.mcState.isRun = true;
            //FrmMain.logAdd(DF.EVENT_LOG, "Machine start [Recipe = " + txtCurRecipe.Text + "]");
        }

        void StopProcess()
        {
            GlobalVariable.mcState.isRun = false;
            GlobalVariable.mcState.isManualRun = false;

            //정지될때 까지 기다린다.
            WaitStop();
            GlobalVariable.mcState.isCheckStopPos = true;
        }

        void ResetEvent()
        {
            if (!GlobalVariable.mcState.isErr) return;
            //errWidnow.resetError();
        }

        public void WaitStop()
        {
 
        }
    }
}
    