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
using CJ_Controls.Communication.CybogRobot_HTR;
using TBDB_Handler.SEQ;

namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    public partial class subManualTM : UserControl
    {
        MainData _Main = null;

        public subManualTM()
        {
            InitializeComponent();
        }

        private void subManualTM_Load(object sender, EventArgs e)
        {
            _Main = MainData.Instance;

            InitControl();

            tmrStatus.Start();
        }

        private void InitControl()
        {
            int nIndex = 1;
            cbStage.Items.Add(nIndex++.ToString() + ". " + AtmStage.ALIGN.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + AtmStage.CP1.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + AtmStage.CP2.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + AtmStage.CP3.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + AtmStage.LAMI_LD.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + AtmStage.LAMI_ULD.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + AtmStage.LL1.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + AtmStage.LL2.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + AtmStage.BD.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + AtmStage.HP.ToString());

            for (int i = 1; i <= COUNT.MAX_PORT_SLOT; i++)
            {
                cbSlot.Items.Add(i.ToString());
            }

            cbSlot.SelectedIndex = 0;
            cbArm.SelectedIndex = 0;
            cbStage.SelectedIndex = 0;
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            _Main.GetAtmTmData().Robot.Cmd_WriteInitialize();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _Main.GetAtmTmData().Robot.Cmd_WriteErrorReset();
        }

        private void btnServoOn_Click(object sender, EventArgs e)
        {
            _Main.GetAtmTmData().Robot.Cmd_WriteServoOnOff(true);
        }

        private void btnServoOff_Click(object sender, EventArgs e)
        {
            _Main.GetAtmTmData().Robot.Cmd_WriteServoOnOff(false);
        }

        private void btnFGReady_Click(object sender, EventArgs e)
        {
            ARM arm;
            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0) return;

            if (cbArm.SelectedIndex == 0)
                arm = ARM.LOWER;
            else
                arm = ARM.UPPER;

            _Main.GetAtmTmData().Robot.Cmd_Write_MoveWait_FG(nStage, nSlot, arm);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_MotionPauseStop(PAUSE_STATUS.STOP);
        }

        private void btnGetfrom_Click(object sender, EventArgs e)
        {
            ARM arm;
            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0) return;

            if (cbArm.SelectedIndex == 0)
                arm = ARM.LOWER;
            else
                arm = ARM.UPPER;

            _Main.GetAtmTmData().Robot.Cmd_Write_Move_Get(nStage, nSlot, arm); 
        }

        private void btnPutinto_Click(object sender, EventArgs e)
        {
            ARM arm;
            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0) return;

            if (cbArm.SelectedIndex == 0)
                arm = ARM.LOWER;
            else
                arm = ARM.UPPER;

            _Main.GetAtmTmData().Robot.Cmd_Write_Move_Put(nStage, nSlot, arm);
        }

        private void btnLowVacOn_Click(object sender, EventArgs e)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_ArmVac(ARM.LOWER, true);
        }

        private void btnLowVacOff_Click(object sender, EventArgs e)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_ArmVac(ARM.LOWER, false);
        }

        private void btnUpVacOn_Click(object sender, EventArgs e)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_ArmVac(ARM.UPPER, true);
        }

        private void btnUpVacOff_Click(object sender, EventArgs e)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_ArmVac(ARM.UPPER, false);
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false) return;

            //lbSending.BackColor = (_Main.GetLoaderData().Robot.bWorkingStatus == true) ? Color.LightGreen : Color.Gray;
            //lbSending.Text = "Send" + m_TmrCase.ToString();

            lbServo.BackColor = (_Main.GetAtmTmData().Robot.RobotData.ServoOnStatus == true) ? Color.LightGreen : Color.Gray;
            lbRun.BackColor = (_Main.GetAtmTmData().Robot.RobotData.RunStatus == true) ? Color.LightGreen : Color.Gray;
            lbArmFold.BackColor = (_Main.GetAtmTmData().Robot.RobotData.ArmFoldStatus == true) ? Color.LightGreen : Color.Gray;
            lbAbnormal.BackColor = (_Main.GetAtmTmData().Robot.RobotData.AbnormalStatus == true) ? Color.LightGreen : Color.Gray;
            lbLowHand.BackColor = (_Main.GetAtmTmData().Robot.RobotData.LowerHand_VacSts == true) ? Color.LightGreen : Color.Gray;
            lbUpHand.BackColor = (_Main.GetAtmTmData().Robot.RobotData.UpperHand_VacSts == true) ? Color.LightGreen : Color.Gray;

            tbReadSpeed.Text = _Main.GetAtmTmData().Robot.RobotData.Read_Speed.ToString();

        }

        private void btnChangeSpeed_Click(object sender, EventArgs e)
        {
            int nSpeed = Convert.ToInt32(tbSpeed.Text);
            _Main.GetAtmTmData().Robot.Cmd_WriteSpeed(nSpeed);
        }
    }
}
