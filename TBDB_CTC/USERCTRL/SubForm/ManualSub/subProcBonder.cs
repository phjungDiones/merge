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
using TBDB_Handler.MOTION;
using TBDB_CTC.GLOBAL;

namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    public partial class subProcBonder : UserControl
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


        MotionPmc instance = new MotionPmc();

        MainData _Main = null;

        public subProcBonder()
        {
            InitializeComponent();
        }

        private void subProcBonder_Load(object sender, EventArgs e)
        {
//             _Main = MainData.Instance;
//             var i = jph_class.Instance.GetallSpecificTypeOfForm(this, typeof(Glass.GlassButton));
//             foreach (var f in i)
//             {
//                 GetPmcValue((Glass.GlassButton)f);
//             }

            tmrStatus.Start();
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Init_PMC;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnStandby_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Standby_PMC;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Process_PMC;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void glassButton10_Click(object sender, EventArgs e)
        {

        }

        private void glassButton59_Click(object sender, EventArgs e)
        {

        }

        private void CtcOut_0_Click(object sender, EventArgs e)
        {
            Setvalue(sender, e);
        }







        private bool Setvalue(object sender, EventArgs e)
        {
            int tag = 0;
            Int32.TryParse(((CheckBox)sender).Tag.ToString(), out tag);
            int tag_r = tag / 10;
            bool bChk = ((CheckBox)sender).Checked;
            int r = 0;

            switch (tag_r)
            {
                case (10): // CTC_PIO
                    r = tag - 100;
                    instance.SetPIO((CTC_PIO)r, bChk);
                    switch (r)
                    {
                        case 0: instance.SetPIO(CTC_PIO.SendAble, bChk); break;
                        case 1: instance.SetPIO(CTC_PIO.SendStart, bChk); break;
                        case 2: instance.SetPIO(CTC_PIO.SendComplete, bChk); break;
                        case 3: instance.SetPIO(CTC_PIO.SendFail, bChk); break;
                        case 4: instance.SetPIO(CTC_PIO.RecvAble, bChk); break;
                        case 5: instance.SetPIO(CTC_PIO.RecvStart, bChk); break;
                        case 6: instance.SetPIO(CTC_PIO.RecvComplete, bChk); break;
                        case 7: instance.SetPIO(CTC_PIO.RecvFail, bChk); break;
                    } 
                    break;

                case (110)://CTC_VTM_ROBOT
                    r = tag - 1100;
                    switch (r)
                    {
                        case 0: instance.SetVTMRobot(CTC_VTM_ROBOT.RobotMoving, bChk); break;
                        case 1: instance.SetVTMRobot(CTC_VTM_ROBOT.HandFold, bChk); break;
                        case 2: instance.SetVTMRobot(CTC_VTM_ROBOT.HandStretch, bChk); break;
                        case 3: instance.SetVTMRobot(CTC_VTM_ROBOT.HandDown, bChk); break;
                        case 4: instance.SetVTMRobot(CTC_VTM_ROBOT.HandUp, bChk); break;
                    } 
                    break;


                case (12): //CTC_MANUAL
                    r = tag - 120;
                    switch (r)
                    {
                        case 0: instance.SetManualCmd(CTC_MANUAL.InitReq, bChk); break;
                        case 1: instance.SetManualCmd(CTC_MANUAL.InitReady, bChk); break;
                        case 2: instance.SetManualCmd(CTC_MANUAL.InitCompleteAck, bChk); break;
                        case 3: instance.SetManualCmd(CTC_MANUAL.StandbyReq, bChk); break;
                        case 4: instance.SetManualCmd(CTC_MANUAL.StandbyReady, bChk); break;
                        case 5: instance.SetManualCmd(CTC_MANUAL.StandbyCompleteAck, bChk); break;
                        case 6: instance.SetManualCmd(CTC_MANUAL.ProcessReq, bChk); break;
                        case 7: instance.SetManualCmd(CTC_MANUAL.ProcessReady, bChk); break;
                        case 8: instance.SetManualCmd(CTC_MANUAL.ProcessCompleteAck, bChk); break;
                    }  
                    break;

                case (20):
                    r = tag - 200;
                    instance.SetStatusSig(CTC_StatusSig.HeartBit, bChk);
                    break;

                case (40):
                    r = tag - 400;
                    switch (r)
                    {
                        case 0: instance.SetInterlock(CTC_INTERLOCK.ShutterOpen, bChk); break;
                        case 1: instance.SetInterlock(CTC_INTERLOCK.ShutterClose, bChk); break;
                        case 2: instance.SetInterlock(CTC_INTERLOCK.FastPumpOpenClose, bChk); break;
                        case 3: instance.SetInterlock(CTC_INTERLOCK.FastPumpChangeReq, bChk); break;
                        case 4: instance.SetInterlock(CTC_INTERLOCK.DryPumpOnReq, bChk); break;
                    }                  
                    break;
            }


            return false;
        }

        private void glassButton76_Click(object sender, EventArgs e)
        {
            var i = jph_class.Instance.GetallSpecificTypeOfForm(this, typeof(Glass.GlassButton));
            foreach(var f in i)
            {
                GetPmcValue((Glass.GlassButton)f);
            }
        }
        private void GetPmcValue(Glass.GlassButton button)
        {
            int tag = 0;
            Int32.TryParse(button.Tag.ToString(), out tag);
            int tag_r = tag / 10;
            int result = 0;


            int r = 0;


//             switch (tag_r)
//             {
//                 case (10): // CTC_PIO
//                     r = tag - 100;
//                     result = instance.GetPIO((CTC_PIO)r);//0== 논셋           1== 셋        음수값 == 에러;
//                     break;
//                 case (110): // CTC_VTM_ROBOT
//                     r = tag - 1100;
//                     result = instance.GetVTMRobot((CTC_VTM_ROBOT)r);
//                     break;
//                 case (12): // CTC_MANUAL
//                     r = tag - 120;
//                     result = instance.GetManualCmd((CTC_MANUAL)r);
//                     break;
// 
//                 case (20): // PMC_PIO
//                     r = tag - 200;
//                     result = instance.GetPIO((PMC_PIO)r);//0== 논셋           1== 셋        음수값 == 에러;
//                     break;
//                 case (21)://PMC_MOTOR
//                     r = tag - 210;
//                     result = instance.GetMotor((PMC_MOTOR)r);
//                     break;
//                 case (22)://PMC_MANUAL
//                     r = tag - 220;
//                     result = instance.GetManualCmd((PMC_MANUAL)r);
//                     break;
//                 case (23)://PMC_MANUAL
//                     r = tag - 220;
//                     result = instance.GetManualCmd((PMC_MANUAL)r);
//                     break;
// 
//             }
//             if (result == 1)
//             {
//                 button.BackColor = System.Drawing.Color.Lime;
//             }
//             if (result == 0)
//             {
//                 button.BackColor = System.Drawing.Color.DimGray;
//             }
//             if(result < 0)//통신오류
//             {
//                 //MessageBox.Show("통신오류 통신확인");
//             }

        }

        LogManager logManager = new LogManager();
        private void glassButton39_Click(object sender, EventArgs e)
        {
            //logManager.Save_LogToFile(dataGridView1);
        }

        private void glassButton40_Click(object sender, EventArgs e)
        {
            //logManager.Load_LogToFile(dataGridView2);
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false) return;

            short nStatus = 0;

            switch (tabStatus.SelectedIndex)
            {
                case 0:
                    //Interface
                    //Load Pmc
                    LoadPmc_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(PMC_PIO.RecvReq) == 1) ? Color.LightGreen : Color.Gray;
                    LoadPmc_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(PMC_PIO.RecvReady) == 1) ? Color.LightGreen : Color.Gray;
                    LoadPmc_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(PMC_PIO.RecvComplAck) == 1) ? Color.LightGreen : Color.Gray;
                    LoadPmc_3.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetMotor(PMC_MOTOR.MSPDown) == 1) ? Color.LightGreen : Color.Gray;
                    LoadPmc_4.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetMotor(PMC_MOTOR.MSPUp) == 1) ? Color.LightGreen : Color.Gray;
                    LoadPmc_5.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetMotor(PMC_MOTOR.PinDown) == 1) ? Color.LightGreen : Color.Gray;
                    LoadPmc_6.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetMotor(PMC_MOTOR.PinUp) == 1) ? Color.LightGreen : Color.Gray;
                    LoadPmc_7.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(PMC_PIO.RecvFail) == 1) ? Color.LightGreen : Color.Gray;

                    UnloadPmc_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(PMC_PIO.SendReq) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadPmc_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(PMC_PIO.SendReady) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadPmc_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(PMC_PIO.SendComplAck) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadPmc_3.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetMotor(PMC_MOTOR.MSPDown) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadPmc_4.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetMotor(PMC_MOTOR.MSPUp) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadPmc_5.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetMotor(PMC_MOTOR.PinDown) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadPmc_6.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetMotor(PMC_MOTOR.PinUp) == 1) ? Color.LightGreen : Color.Gray;

                    InitPmc_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.InitAck) == 1) ? Color.LightGreen : Color.Gray;
                    InitPmc_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.InitStart) == 1) ? Color.LightGreen : Color.Gray;
                    InitPmc_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.InitCompl) == 1) ? Color.LightGreen : Color.Gray;
                    InitPmc_3.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.InitFail) == 1) ? Color.LightGreen : Color.Gray;

                    StandPmc_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.StandbyAck) == 1) ? Color.LightGreen : Color.Gray;
                    StandPmc_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.StandbyStart) == 1) ? Color.LightGreen : Color.Gray;
                    StandPmc_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.StandbyCompl) == 1) ? Color.LightGreen : Color.Gray;
                    StandPmc_3.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.StandbyFail) == 1) ? Color.LightGreen : Color.Gray;

                    ProcPmc_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.ProcAck) == 1) ? Color.LightGreen : Color.Gray;
                    ProcPmc_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.ProcStart) == 1) ? Color.LightGreen : Color.Gray;
                    ProcPmc_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.ProcCompl) == 1) ? Color.LightGreen : Color.Gray;
                    ProcPmc_3.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(PMC_MANUAL.ProcFail) == 1) ? Color.LightGreen : Color.Gray;

                    //CTC Cmd
                    LoadCtc_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(CTC_PIO.SendAble) == 1) ? Color.LightGreen : Color.Gray;
                    LoadCtc_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(CTC_PIO.SendStart) == 1) ? Color.LightGreen : Color.Gray;
                    LoadCtc_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(CTC_PIO.SendComplete) == 1) ? Color.LightGreen : Color.Gray;
                    LoadCtc_3.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetVTMRobot(CTC_VTM_ROBOT.RobotMoving) == 1) ? Color.LightGreen : Color.Gray;
                    LoadCtc_4.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetVTMRobot(CTC_VTM_ROBOT.HandFold) == 1) ? Color.LightGreen : Color.Gray;
                    LoadCtc_5.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetVTMRobot(CTC_VTM_ROBOT.HandStretch) == 1) ? Color.LightGreen : Color.Gray;
                    LoadCtc_6.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetVTMRobot(CTC_VTM_ROBOT.HandDown) == 1) ? Color.LightGreen : Color.Gray;
                    LoadCtc_7.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetVTMRobot(CTC_VTM_ROBOT.HandUp) == 1) ? Color.LightGreen : Color.Gray;
                    LoadCtc_8.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(CTC_PIO.SendFail) == 1) ? Color.LightGreen : Color.Gray;

                    UnloadCtc_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(CTC_PIO.RecvAble) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadCtc_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(CTC_PIO.RecvStart) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadCtc_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetPIO(CTC_PIO.RecvComplete) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadCtc_3.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetVTMRobot(CTC_VTM_ROBOT.RobotMoving) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadCtc_4.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetVTMRobot(CTC_VTM_ROBOT.HandFold) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadCtc_5.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetVTMRobot(CTC_VTM_ROBOT.HandStretch) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadCtc_6.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetVTMRobot(CTC_VTM_ROBOT.HandDown) == 1) ? Color.LightGreen : Color.Gray;
                    UnloadCtc_7.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetVTMRobot(CTC_VTM_ROBOT.HandUp) == 1) ? Color.LightGreen : Color.Gray;

                    InitCtc_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(CTC_MANUAL.InitReq) == 1) ? Color.LightGreen : Color.Gray;
                    InitCtc_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(CTC_MANUAL.InitReady) == 1) ? Color.LightGreen : Color.Gray;
                    InitCtc_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(CTC_MANUAL.InitCompleteAck) == 1) ? Color.LightGreen : Color.Gray;

                    StandCtc_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(CTC_MANUAL.StandbyReq) == 1) ? Color.LightGreen : Color.Gray;
                    StandCtc_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(CTC_MANUAL.StandbyReady) == 1) ? Color.LightGreen : Color.Gray;
                    StandCtc_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(CTC_MANUAL.StandbyCompleteAck) == 1) ? Color.LightGreen : Color.Gray;

                    ProcCtc_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(CTC_MANUAL.ProcessReq) == 1) ? Color.LightGreen : Color.Gray;
                    ProcCtc_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(CTC_MANUAL.ProcessReady) == 1) ? Color.LightGreen : Color.Gray;
                    ProcCtc_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(CTC_MANUAL.ProcessCompleteAck) == 1) ? Color.LightGreen : Color.Gray;
                    //ProcCtc_3.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetManualCmd(CTC_MANUAL.ProcessCompleteAck) == 1) ? Color.LightGreen : Color.Gray;     

                    //CTC Status
                    CtcStatusSig_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(CTC_StatusSig.HeartBit) == 1) ? Color.LightGreen : Color.Gray;
                    CtcInterlock_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetInterlock(CTC_INTERLOCK.ShutterOpen) == 1) ? Color.LightGreen : Color.Gray;
                    CtcInterlock_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetInterlock(CTC_INTERLOCK.ShutterClose) == 1) ? Color.LightGreen : Color.Gray;
                    CtcInterlock_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetInterlock(CTC_INTERLOCK.FastPumpOpenClose) == 1) ? Color.LightGreen : Color.Gray;
                    CtcInterlock_3.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetInterlock(CTC_INTERLOCK.FastPumpChangeReq) == 1) ? Color.LightGreen : Color.Gray;
                    CtcInterlock_4.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetInterlock(CTC_INTERLOCK.DryPumpOnReq) == 1) ? Color.LightGreen : Color.Gray;

                    //PMC Status 
                    PmcStatusSig_0.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.HeartBit) == 1) ? Color.LightGreen : Color.Gray;
                    PmcStatusSig_1.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.InitStatus) == 1) ? Color.LightGreen : Color.Gray;
                    PmcStatusSig_2.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.StandbyStatus) == 1) ? Color.LightGreen : Color.Gray;
                    PmcStatusSig_3.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.LowWaferExist) == 1) ? Color.LightGreen : Color.Gray;
                    PmcStatusSig_4.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.UppWaferExist) == 1) ? Color.LightGreen : Color.Gray;
                    PmcStatusSig_5.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.DryPumpOnOffStatus) == 1) ? Color.LightGreen : Color.Gray;
                    PmcStatusSig_6.BackColor = (GlobalSeq.autoRun.prcVTM.Pmc.GetStatusSig(PMC_StatusSig.FastPumpStatus) == 1) ? Color.LightGreen : Color.Gray;

                    break;

                case 1:

                    //Ctc status
                    GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(CTC_STATUS.CTCStatus, ref nStatus);
                    CtcStatus_0.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(CTC_STATUS.VTMStatus, ref nStatus);
                    CtcStatus_1.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(CTC_STATUS.CTCRunMode, ref nStatus);
                    CtcStatus_2.Text = nStatus.ToString();

                    //CTC Recipe
                    GlobalSeq.autoRun.prcVTM.Pmc.GetRecipe(CTC_RECIPE.VisionUsed, ref nStatus);
                    CtcRecipe_0.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetRecipe(CTC_RECIPE.Pressure, ref nStatus);
                    CtcRecipe_1.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetRecipe(CTC_RECIPE.PressingTime, ref nStatus);
                    CtcRecipe_2.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetRecipe(CTC_RECIPE.UpperTemp, ref nStatus);
                    CtcRecipe_3.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetRecipe(CTC_RECIPE.LowerTemp, ref nStatus);
                    CtcRecipe_4.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetRecipe(CTC_RECIPE.APCPosition, ref nStatus);
                    CtcRecipe_5.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetRecipe(CTC_RECIPE.CH1Backlight, ref nStatus);
                    CtcRecipe_6.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetRecipe(CTC_RECIPE.CH2Backlight, ref nStatus);
                    CtcRecipe_7.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetRecipe(CTC_RECIPE.CH3Backlight, ref nStatus);
                    CtcRecipe_8.Text = nStatus.ToString();

                    //PMC Status
                    GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(PMC_STATUS.PMCStatus, ref nStatus);
                    PmcStatus_0.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(PMC_STATUS.PMCVacuumStatus, ref nStatus);
                    PmcStatus_1.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(PMC_STATUS.PMCProcStepStatus, ref nStatus);
                    PmcStatus_2.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(PMC_STATUS.PMCRunMode, ref nStatus);
                    PmcStatus_3.Text = nStatus.ToString();

                    //PMC Process Data
                    GlobalSeq.autoRun.prcVTM.Pmc.GetProcData(PMC_PROCDATA.OutUpperTemp, ref nStatus);
                    PmcProcData_0.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetProcData(PMC_PROCDATA.InUpperTemp, ref nStatus);
                    PmcProcData_1.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetProcData(PMC_PROCDATA.OutLowerTemp, ref nStatus);
                    PmcProcData_2.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetProcData(PMC_PROCDATA.InLowerTemp, ref nStatus);
                    PmcProcData_3.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GetProcData(PMC_PROCDATA.Pressure, ref nStatus);
                    PmcProcData_4.Text = nStatus.ToString();                   
                    GlobalSeq.autoRun.prcVTM.Pmc.GetProcData(PMC_PROCDATA.Probe1, ref nStatus);
                    PmcProcData_5.Text = nStatus.ToString();                   
                     GlobalSeq.autoRun.prcVTM.Pmc.GetProcData(PMC_PROCDATA.Probe2, ref nStatus);
                    PmcProcData_6.Text = nStatus.ToString();                                          
                     GlobalSeq.autoRun.prcVTM.Pmc.GetProcData(PMC_PROCDATA.Probe3, ref nStatus);
                    PmcProcData_7.Text = nStatus.ToString();                                          
                                        
                    //PMC Motor Pos
                    GlobalSeq.autoRun.prcVTM.Pmc.GeMotorPos(PMC_MotorPos.DDMotorPos, ref nStatus);
                    PmcMotor_0.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GeMotorPos(PMC_MotorPos.MSPMotorPos, ref nStatus);
                    PmcMotor_1.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GeMotorPos(PMC_MotorPos.Pin1MotorPos, ref nStatus);
                    PmcMotor_2.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GeMotorPos(PMC_MotorPos.Pin2MotorPos, ref nStatus);
                    PmcMotor_3.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GeMotorPos(PMC_MotorPos.XAxisPos, ref nStatus);
                    PmcMotor_4.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GeMotorPos(PMC_MotorPos.YAxisPos, ref nStatus);
                    PmcMotor_5.Text = nStatus.ToString();
                    GlobalSeq.autoRun.prcVTM.Pmc.GeMotorPos(PMC_MotorPos.TAxisPos, ref nStatus);
                    PmcMotor_6.Text = nStatus.ToString();
                    break;

                default :
                    break;

            }
 
        }

        private void btnSetRcpName_Click(object sender, EventArgs e)
        {
            int nMinLen = 20;

            if (tbRcpName.TextLength < nMinLen)
            {
                MessageBox.Show("Please Check Recipe Length ");
                return;
            }

            int nLen = tbRcpName.TextLength;
            short[] rcpName = new short[nLen];
            string strRcpName = tbRcpName.Text;
            int j = 0;

            //아스키 변환
            byte[] name = Encoding.ASCII.GetBytes(strRcpName);

            for (int i = 0; i < nLen; i++)
            {
                rcpName[i] = name[i];
                //rcpName[i] = short.Parse(strRcpName.Substring(0, 1));
            }

            j = 0;

            //Send Recipe Name, 20 Len
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_1, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_2, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_3, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_4, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_5, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_6, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_7, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_8, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_9, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_10, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_11, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_12, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_13, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_14, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_15, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_16, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_17, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_18, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_19, rcpName[j++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipeName(CTC_RecipeName.RcpName_20, rcpName[j++]);            
        }



        private void btnSetRcp_Click(object sender, EventArgs e)
        {
            short[] rcpData = new short[10];
            int i = 0;

            //GlobalVariable.pmc_Rcp
            rcpData[i++] = Convert.ToInt16(double.Parse(tb_CtcRecipe_0.Text));
            rcpData[i++] = Convert.ToInt16(double.Parse(tb_CtcRecipe_1.Text) * 100);
            rcpData[i++] = Convert.ToInt16(double.Parse(tb_CtcRecipe_2.Text) * 10);
            rcpData[i++] = Convert.ToInt16(double.Parse(tb_CtcRecipe_3.Text) * 10);
            rcpData[i++] = Convert.ToInt16(double.Parse(tb_CtcRecipe_4.Text) * 10);
            rcpData[i++] = Convert.ToInt16(double.Parse(tb_CtcRecipe_5.Text) * 10);
            rcpData[i++] = Convert.ToInt16(double.Parse(tb_CtcRecipe_6.Text) * 10);
            rcpData[i++] = Convert.ToInt16(double.Parse(tb_CtcRecipe_7.Text) * 10);
            rcpData[i++] = Convert.ToInt16(double.Parse(tb_CtcRecipe_8.Text) * 10);

            i = 0;

            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.VisionUsed, rcpData[i++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.Pressure, rcpData[i++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.PressingTime, rcpData[i++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.UpperTemp, rcpData[i++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.LowerTemp, rcpData[i++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.APCPosition, rcpData[i++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.CH1Backlight, rcpData[i++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.CH2Backlight, rcpData[i++]);
            GlobalSeq.autoRun.prcVTM.Pmc.SetRecipe(CTC_RECIPE.CH3Backlight, rcpData[i++]);
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
        }
    }

    class jph_class
    {




        private static volatile jph_class instance;
        private static object syncRoot = new Object();

        private jph_class() { }
        public static jph_class Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new jph_class();
                    }
                }

                return instance;
            }
        }







        public void SetFormToMaximized(Form _form)
        {

            _form.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Size.Width, Screen.PrimaryScreen.WorkingArea.Size.Height);
            _form.Location = new System.Drawing.Point(0, 0);
            _form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        public void SetObjectToForm(Form _form, Control _control, string name, int x, int y, int width, int height)
        {
            _control.Name = name;
            _control.SetBounds(x, y, width, height);
            _form.Controls.Add(_control);
        }


        public IEnumerable<Control> GetallSpecificTypeOfForm(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetallSpecificTypeOfForm(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }












    }
}
