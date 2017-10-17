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
using CJ_Controls.Communication.QuadraVTM4;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.SEQ;
using System.Diagnostics;

namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    public partial class subManualVTM : UserControl
    {
        MainData _Main = null;

        int nStatus = 0;

        public subManualVTM()
        {
            InitializeComponent();
        }

        private void subManualVTM_Load(object sender, EventArgs e)
        {
            _Main = MainData.Instance;

            InitControl();

            tmrStatus.Start();
        }

        private void InitControl()
        {
            int nIndex = 1;

            cbStage.Items.Add(nIndex++.ToString() + ". " + VtmStage.Bonded.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + VtmStage.LL.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + VtmStage.PMC1_ULD.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + VtmStage.PMC1_LD.ToString());

            for (int i = 1; i <= COUNT.MAX_PORT_SLOT; i++)
            {
                cbSlot.Items.Add(i.ToString());
            }

            cbSlot.SelectedIndex = 0;
            cbArm.SelectedIndex = 0;
            cbExRe.SelectedIndex = 0;
            cbUpdown.SelectedIndex = 0;
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            _Main.GetVaccumTmData().Robot.Cmd_SendHomeAll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _Main.GetVaccumTmData().Robot.Cmd_SendClear();
        }

        private void btnServoOn_Click(object sender, EventArgs e)
        {

        }

        private void btnServoOff_Click(object sender, EventArgs e)
        {

        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            ARM arm;
            UPDOWN_POS updn;
            RADIAL_POS radial;

            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0) return;


            if (cbArm.SelectedIndex == 0) arm = ARM.LOWER;
            else arm = ARM.UPPER;

            if (cbExRe.SelectedIndex == 0) radial = RADIAL_POS.EXTENDED;
            else radial = RADIAL_POS.RETRACTED; 

            if (cbUpdown.SelectedIndex == 0) updn = UPDOWN_POS.UP;
            else updn = UPDOWN_POS.DOWN;

            _Main.GetVaccumTmData().Robot.Cmd_SendGoTo(nStage, nSlot, arm, radial, updn);
        }

        private void btnGetfrom_Click(object sender, EventArgs e)
        {
            ARM arm;
            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0 || cbArm.SelectedIndex < 0) return;

            if (cbArm.SelectedIndex == 0) arm = ARM.LOWER;
            else arm = ARM.UPPER;

            _Main.GetVaccumTmData().Robot.Cmd_SendPick(nStage, nSlot, arm);  
        }

        private void btnPutinto_Click(object sender, EventArgs e)
        {
            ARM arm;
            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0 || cbArm.SelectedIndex < 0) return;

            if (cbArm.SelectedIndex == 0) arm = ARM.LOWER;
            else arm = ARM.UPPER;

            _Main.GetVaccumTmData().Robot.Cmd_SendPlace(nStage, nSlot, arm);

            Debug.WriteLine("btnPutinto_Click Stage=" + nStage.ToString() + ", nSlot=" + nSlot.ToString() + ", arm=" + arm.ToString());
        }

        private void btnUpZ_Click(object sender, EventArgs e)
        {
            ARM arm;
            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0 || cbArm.SelectedIndex < 0) return;

            if (cbArm.SelectedIndex == 0) arm = ARM.LOWER;
            else arm = ARM.UPPER;

            _Main.GetVaccumTmData().Robot.Cmd_SendZAxis(nStage, nSlot, arm, UPDOWN_POS.UP);
        }

        private void btnZDown_Click(object sender, EventArgs e)
        {
            ARM arm;

            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0 || cbArm.SelectedIndex < 0) return;

            if (cbArm.SelectedIndex == 0) arm = ARM.LOWER;
            else arm = ARM.UPPER;

            _Main.GetVaccumTmData().Robot.Cmd_SendZAxis(nStage, nSlot, arm, UPDOWN_POS.DOWN); 
        }

        private void btnArmExt_Click(object sender, EventArgs e)
        {
            UPDOWN_POS updn;

            ARM arm;
            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0 || cbArm.SelectedIndex < 0) return;

            if (cbArm.SelectedIndex == 0) arm = ARM.LOWER;
            else arm = ARM.UPPER;

            if (cbUpdown.SelectedIndex == 0) updn = UPDOWN_POS.UP;
            else updn = UPDOWN_POS.DOWN;

            _Main.GetVaccumTmData().Robot.Cmd_Extend(nStage, nSlot, arm, updn);   
        }

        private void btnArmRetract_Click(object sender, EventArgs e)
        {
            ARM arm;
            UPDOWN_POS updn;

            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0 || cbArm.SelectedIndex < 0) return;

            if (cbArm.SelectedIndex == 0) arm = ARM.LOWER;
            else arm = ARM.UPPER;

            if (cbUpdown.SelectedIndex == 0) updn = UPDOWN_POS.UP;
            else updn = UPDOWN_POS.DOWN;

            _Main.GetVaccumTmData().Robot.Cmd_Retract(nStage, nSlot, arm, updn);  
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false) return;

            switch (nStatus)
            {
                case 0:
                    //_Main.GetVaccumTmData().Robot.Cmd_ReadPower();
                    break;

                case 1:
                    //_Main.GetVaccumTmData().Robot.Cmd_ReadWaferArm(ARM.LOWER);
                    break;

                case 2:
                    //_Main.GetVaccumTmData().Robot.Cmd_ReadWaferArm(ARM.UPPER);
                    break;
                default :
                    nStatus = 0;
                    break;
            }

            nStatus++;
            lbReady.BackColor = (_Main.GetVaccumTmData().Robot.WorkStatus == WORK_STATUS.IDLE) ? Color.LightGreen : Color.Gray;
            lbMoving.BackColor = (_Main.GetVaccumTmData().Robot.WorkStatus == WORK_STATUS.MOVING) ? Color.LightGreen : Color.Gray;
            lbError.BackColor = (_Main.GetVaccumTmData().Robot.WorkStatus == WORK_STATUS.ERROR) ? Color.LightGreen : Color.Gray;


            //lbWaferA.BackColor = (_Main.GetVaccumTmData().Robot.RobotData.Wafer_A_Status == WAFER_STS.PRESENT) ? Color.LightGreen : Color.Gray;
            //lbWaferB.BackColor = (_Main.GetVaccumTmData().Robot.RobotData.Wafer_B_Status == WAFER_STS.PRESENT) ? Color.LightGreen : Color.Gray;
        }

        private void btnGateOpen_Click(object sender, EventArgs e)
        {

        }

        private void btnGateClose_Click(object sender, EventArgs e)
        {

        }

    }
}
