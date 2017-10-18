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
    public partial class subManualFM : UserControl
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

        public subManualFM()
        {
            InitializeComponent();
        }

        private void subManualFM_Load(object sender, EventArgs e)
        {
            _Main = MainData.Instance;

            InitControl();

            tmrStatus.Start();
        }

        private void InitControl()
        {
            int nIndex = 1;
            cbStage.Items.Add(nIndex++.ToString() + ". " + FMStage.LPMA.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + FMStage.LPMB.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + FMStage.LPMC.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + FMStage.LPMD.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + FMStage.AL.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + FMStage.CP1.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + FMStage.CP2.ToString());
            cbStage.Items.Add(nIndex++.ToString() + ". " + FMStage.CP3.ToString());

            for(int i=1; i <= COUNT.MAX_PORT_SLOT; i++)
            {
                cbSlot.Items.Add(i.ToString());
            }

            cbSlot.SelectedIndex = 0;
            cbArm.SelectedIndex = 0;
            cbStage.SelectedIndex = 0;            
        }


        private void btnInit_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Robot.Cmd_WriteInitialize();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Robot.Cmd_WriteErrorReset();
        }

        private void btnServoOn_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Robot.Cmd_WriteServoOnOff(true);
        }

        private void btnServoOff_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Robot.Cmd_WriteServoOnOff(false);
        }

        private void btnFGReady_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            ARM arm;
            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0) return;

            if (cbArm.SelectedIndex == 0)
                arm = ARM.LOWER;
            else
                arm = ARM.UPPER;

            _Main.GetLoaderData().Robot.Cmd_Write_MoveWait_FG(nStage, nSlot, arm);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Robot.Cmd_Write_MotionPauseStop(PAUSE_STATUS.STOP);
        }

        private void btnGetfrom_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            ARM arm;
            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0) return;

            if (cbArm.SelectedIndex == 0)
                arm = ARM.LOWER;
            else
                arm = ARM.UPPER;

            _Main.GetLoaderData().Robot.Cmd_Write_Move_Get(nStage, nSlot, arm);   
        }

        private void btnPutinto_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            ARM arm;
            int nStage = (int)cbStage.SelectedIndex + 1;
            int nSlot = (int)cbSlot.SelectedIndex + 1;
            if (nStage < 0 || nSlot < 0) return;

            if (cbArm.SelectedIndex == 0)
                arm = ARM.LOWER;
            else
                arm = ARM.UPPER;

            _Main.GetLoaderData().Robot.Cmd_Write_Move_Put(nStage, nSlot, arm);
        }

        private void btnLowVacOn_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Robot.Cmd_Write_Move_ArmVac(ARM.LOWER, true);
        }

        private void btnLowVacOff_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Robot.Cmd_Write_Move_ArmVac(ARM.LOWER, false);
        }

        private void btnUpVacOn_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Robot.Cmd_Write_Move_ArmVac(ARM.UPPER, true);
        }

        private void btnUpVacOff_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Robot.Cmd_Write_Move_ArmVac(ARM.UPPER, false);
        }

        private void btnChangeSpeed_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            int nSpeed = Convert.ToInt32(tbSpeed.Text);
            _Main.GetLoaderData().Robot.Cmd_WriteSpeed(nSpeed); 
        }

        public bool CompleteCheck()
        {
            if (_Main.GetLoaderData().Robot.WorkStatus == WORK_STATUS.IDLE)
            {
                return true;
            }
            else 
                return false;
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false) return;


                lbServo.BackColor = (_Main.GetLoaderData().Robot.RobotData.ServoOnStatus == true) ? Color.LightGreen : Color.Gray;
                lbRun.BackColor = (_Main.GetLoaderData().Robot.RobotData.RunStatus == true) ? Color.LightGreen : Color.Gray;
                lbArmFold.BackColor = (_Main.GetLoaderData().Robot.RobotData.ArmFoldStatus == true) ? Color.LightGreen : Color.Gray;
                lbAbnormal.BackColor = (_Main.GetLoaderData().Robot.RobotData.AbnormalStatus == true) ? Color.LightGreen : Color.Gray;
                lbLowHand.BackColor = (_Main.GetLoaderData().Robot.RobotData.LowerHand_VacSts == true) ? Color.LightGreen : Color.Gray;
                lbUpHand.BackColor = (_Main.GetLoaderData().Robot.RobotData.UpperHand_VacSts == true) ? Color.LightGreen : Color.Gray;

                tbReadSpeed.Text = _Main.GetLoaderData().Robot.RobotData.Read_Speed.ToString();

                lbCompl.BackColor = (CompleteCheck() == true) ? Color.LightGreen : Color.Gray;
        }
    }
}
