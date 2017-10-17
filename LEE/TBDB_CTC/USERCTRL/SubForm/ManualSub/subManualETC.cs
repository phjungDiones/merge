using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.THREAD;

namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    public partial class subManualETC : UserControl
    {
        public subManualETC()
        {
            InitializeComponent();
        }

        private void subManualETC_Load(object sender, EventArgs e)
        {
            //tmrStatus.Start();
        }

        private void btnGateOpen_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.Open_ChamberBDGateValve();
        }

        private void btnGateClose_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.Close_ChamberBDGateValve();
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false) return;

//             lbBDOpen.BackColor = (GlobalVariable.io.is_ChamberBDSlitGate_Open() ==  true) ? Color.LightGreen : Color.Gray;
//             lbBDClose.BackColor = (GlobalVariable.io.is_ChamberBDSlitGate_Close() == true) ? Color.LightGreen : Color.Gray;
//             lbTMOpen.BackColor = (GlobalVariable.io.is_ChamberTMSlitGate_Open() == true) ? Color.LightGreen : Color.Gray;
//             lbTMClose.BackColor = (GlobalVariable.io.is_ChamberTMSlitGate_Close() == true) ? Color.LightGreen : Color.Gray;
//             lbLLOpen.BackColor = (GlobalVariable.io.is_LLSlot_Open() == true) ? Color.LightGreen : Color.Gray;
//             lbLLClose.BackColor = (GlobalVariable.io.is_LLSlot_Close() == true) ? Color.LightGreen : Color.Gray;            
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.Open_ChamberTMGateValve();
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.Close_ChamberTMGateValve();
        }

        private void glassButton4_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.Open_LoadLockGateSlotValve();
        }

        private void glassButton3_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.Close_LoadLockGateSlotValve();
        }

        private void btnVtmArmExt1_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.VtmArmExt(0, true);
        }

        private void glassButton11_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.VtmArmExt(0, false);
        }

        private void glassButton5_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.VtmArmExt(1, true);
        }

        private void glassButton10_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.VtmArmExt(1, false);
        }

        private void glassButton6_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.VtmArmExt(2, true);
        }

        private void glassButton9_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.VtmArmExt(2, false);
        }

        private void glassButton7_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.VtmArmExt(3, true);
        }

        private void glassButton8_Click(object sender, EventArgs e)
        {
            //GlobalVariable.io.VtmArmExt(3, false);
        }

        private void btnVtmPump_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTM_Pumping;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnVtmVent_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTM_Venting;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnLoadlockPump_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.Loadlock_Pumping;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnLoadlockVent_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.Loadlock_Venting;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }
    }
}
