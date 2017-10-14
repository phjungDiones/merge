using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_CTC.Data;
using TBDB_Handler.THREAD;
using TBDB_Handler.GLOBAL;
using TBDB_CTC.Comm.Lami_PLC;

namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    public partial class subProcLami : UserControl
    {
        POPWND.popConfirm popConfirm = new POPWND.popConfirm();
        POPWND.popCancel popCancel = new POPWND.popCancel();
        private bool ShowDialog()
        {
            bool flag = false;
            popConfirm.ShowDialog();
            if (popConfirm.DialogResult == DialogResult.OK)
            {
                flag = true;
            }
            else
            {
                //popCancel.ShowDialog();
            }
            return flag;
        }

        MainData _Main = null;

        public subProcLami()
        {
            InitializeComponent();
        }

        private void subProcLami_Load(object sender, EventArgs e)
        {
            _Main = MainData.Instance;
            tmrStatus.Start();
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_Init_Lami;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnStandby_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_Standby_Lami;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_ProcStart_Lami;
            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
        }

        private void CtcOut_0_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
        }

        private void CtcOut_0_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox btn = sender as CheckBox;
            int nTag = Convert.ToInt16(btn.Tag);

            bool bAct = btn.Checked;

            switch (nTag)
            {
                case 0:
                    GlobalSeq.autoRun.prcATM.Laminator.WriteAddrData(CTC_PLC_ADDR.INIT_REQUEST, bAct);
                    break;
                case 1:
                    GlobalSeq.autoRun.prcATM.Laminator.WriteAddrData(CTC_PLC_ADDR.STANDBY_REQUEST, bAct);
                    break;
                case 2:
                    GlobalSeq.autoRun.prcATM.Laminator.WriteAddrData(CTC_PLC_ADDR.REMOTE_START_REQUEST, bAct);
                    break;
                case 3:
                    GlobalSeq.autoRun.prcATM.Laminator.WriteAddrData(CTC_PLC_ADDR.REMOTE_STOP_REQUEST, bAct);
                    break;
                case 4:
                    GlobalSeq.autoRun.prcATM.Laminator.WriteAddrData(CTC_PLC_ADDR.PROCESS_PAUSE_REQUEST, bAct);
                    break;
                case 5:
                    GlobalSeq.autoRun.prcATM.Laminator.WriteAddrData(CTC_PLC_ADDR.PROCESS_RESUME_REQUEST, bAct);
                    break;
                case 6:
                    GlobalSeq.autoRun.prcATM.Laminator.WriteAddrData(CTC_PLC_ADDR.PROCESS_ABORT_REQUEST, bAct);
                    break;
                case 7:
                    GlobalSeq.autoRun.prcATM.Laminator.WriteAddrData(CTC_PLC_ADDR.RECIPE_CHANGE_REQUEST, bAct);
                    break;
            }
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            if( this.Visible == false) return;

            MainStatus_0.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.LAMINATOR_READY) == 1) ? Color.LightGreen : Color.Gray;

            LamiInput_0.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.INIT_ACK) == 1) ? Color.LightGreen : Color.Gray;
            LamiInput_1.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.STANDBY_ACK) == 1) ? Color.LightGreen : Color.Gray;
            LamiInput_2.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.LOAD_REQUEST) == 1) ? Color.LightGreen : Color.Gray;

            LamiInput_4.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.UNLOAD_REQUEST) == 1) ? Color.LightGreen : Color.Gray;
            LamiInput_5.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.LOAD_COMPLETE_ACK) == 1) ? Color.LightGreen : Color.Gray;
            LamiInput_6.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_START_ACK) == 1) ? Color.LightGreen : Color.Gray;
            LamiInput_7.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_STOP_ACK) == 1) ? Color.LightGreen : Color.Gray;
            LamiInput_8.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_PAUSE_ACK) == 1) ? Color.LightGreen : Color.Gray;
            LamiInput_9.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_RESUME_ACK) == 1) ? Color.LightGreen : Color.Gray;
            LamiInput_10.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.PROCESS_RESUME_NAK) == 1) ? Color.LightGreen : Color.Gray;
            LamiInput_11.BackColor = (GlobalSeq.autoRun.prcATM.Laminator.ReadAddrData(LAMI_PLC_ADDR.RECIPE_CHANGE_ACK) == 1) ? Color.LightGreen : Color.Gray;
        }

    }
}
