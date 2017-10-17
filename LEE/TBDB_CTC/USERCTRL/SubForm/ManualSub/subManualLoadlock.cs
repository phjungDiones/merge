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
using CJ_Controls.Communication.EDB2000;
using System.Threading;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;
using EraeMotionApi;
using System.IO;
using System.Xml.Serialization;
using TBDB_CTC.Sequence;


namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    public partial class subManualLoadlock : UserControl
    {
        MotionLoadlock Loadlock = new MotionLoadlock();


        byte[] RxBuffer = new byte[9];
        int nRpmSpeed = 0;
        bool bStopTest = false;
        const int MAX_MOTOR = 2;
        const int UNIT_RPM = 50;
        int nCurposX = 0, nCurposY = 0;

        MainData _Main = null;

        int nActPos = 0;
        int nEncPos = 0;
        bool bMoving = false;
        bool bInpos = false;
        bool bLimitP = false; 
        bool bLmitN = false;
        bool bHome = false;
        bool bAlarm = false;
        bool bServo = false;
        int nHomeStatus = 0;

        int nModuleID = 1;
        int nStatusCase = 0;

        int nTargetPos1 = 0;
        int nTargetPos2 = 0;
        int nTargetPos3 = 0;

        bool bStatusFlag = false;

        private object lockData = new object();

        Thread m_Thread_Monitor;
        EMCL.MotorStatus moStatus = new EMCL.MotorStatus();
        public subManualLoadlock()
        {
            InitializeComponent();

//             m_Thread_Monitor = new Thread(new ThreadStart(Monitoring));
//             m_Thread_Monitor.IsBackground = true;
//             m_Thread_Monitor.Start();
        }

        private void subManualLoadlock_Load(object sender, EventArgs e)
        {
            ModelManager.Instance.LoadLoadlockData();

            tbPos1.Text = GlobalVariable.model.nLoadlockPos[0].ToString();
            tbPos2.Text = GlobalVariable.model.nLoadlockPos[1].ToString();
            tbPos3.Text = GlobalVariable.model.nLoadlockPos[2].ToString();

            tbSpeed.Text = GlobalVariable.model.nLLSpeed.ToString();
            tbAcc.Text = GlobalVariable.model.nLLAcc.ToString();

            tmrStatus.Start();
        }


        private object Read = new object();
        private void Monitoring()
        {
            while (true)
            {
                Thread.Sleep(100);
                //Application.DoEvents();

                //lock (xx)
                //{
                //    nActPos = Loadlock.GetActPos();
                //    nEncPos = Loadlock.GetEncoderPos();
                //    bMoving = Loadlock.IsMoving();
                //    bInpos = Loadlock.Inposition();
                //    bLimitP = Loadlock.IsLimitP();
                //    bLmitN = Loadlock.IsLimitN();
                //    bHome = Loadlock.IsHome();
                //    nHomeStatus = Loadlock.SearchStatus();
                //}
                lock (Read)
                {
                    Loadlock.GetAllStatus(ref moStatus);
                    GlobalVariable.LoadlockMotor.nActPos = moStatus.nActualPos;
                    GlobalVariable.LoadlockMotor.nEncPos = moStatus.nPosError;
                    GlobalVariable.LoadlockMotor.bMoving = Convert.ToBoolean(moStatus.nIsBusy);
                    GlobalVariable.LoadlockMotor.bInpos = Convert.ToBoolean(moStatus.nInPosition);
                    GlobalVariable.LoadlockMotor.bLimitP = Convert.ToBoolean(moStatus.nRightLimitStatus);
                    GlobalVariable.LoadlockMotor.bLmitN = Convert.ToBoolean(moStatus.nLeftLimitStatus);
                    GlobalVariable.LoadlockMotor.bHome = Convert.ToBoolean(moStatus.nHomeStatus);
                    GlobalVariable.LoadlockMotor.bServo = Convert.ToBoolean(moStatus.nIsServoOn);
                    GlobalVariable.LoadlockMotor.nHomeStatus = Convert.ToInt32(moStatus.nHomeStatus);
                }
            }
        }



        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            labelSpeed.Text = (trackBarSpeed.Value * UNIT_RPM).ToString();
            nRpmSpeed = (trackBarSpeed.Value * UNIT_RPM) / 60 * 51200;
        }
        private int GetRpm()
        {
            return (trackBarSpeed.Value * UNIT_RPM) / 60 * 51200;
        }


        private ReplyCode GetReply()
        {
            int nValue = 0;
            bool bVersion = false;

            return GetReply(ref nValue, bVersion);
        }
        private ReplyCode GetReply(ref int nValue, bool bVersion)
        {
            byte[] RxBuffer = new byte[9];
            byte uchReplyAddr = 0, ucModuleAdr = 0, uchStatus = 0, ucCmdNumber = 0;
            ReplyCode ret = _Main.GetLoadlockData().Loadlock.GetReply(ref uchReplyAddr, ref ucModuleAdr, ref uchStatus, ref ucCmdNumber, ref nValue, ref RxBuffer, bVersion);

            UpdateRoundTick(RxBuffer);
            return ret;
        }
        private void UpdateRoundTick(byte[] buffer)
        {
            int nCount = listViewLog.Items.Count;
            listViewLog.Items[nCount - 1].SubItems.Add(BitConverter.ToString(buffer));
            listViewLog.Items[nCount - 1].SubItems.Add(((DateTime.Now.Ticks - Convert.ToInt64(listViewLog.Items[nCount - 1].SubItems[1].Text)) / 10000).ToString());
            listViewLog.EnsureVisible(nCount - 1);
        }


        private void SendCmd(byte nMotor, byte nCmd, byte nType, byte nBank, int nValue)
        {
            byte[] buffer = new byte[9];

            int nCount = listViewLog.Items.Count;
            listViewLog.Items.Add(listViewLog.Items.Count.ToString());
            listViewLog.Items[nCount].SubItems.Add(DateTime.Now.Ticks.ToString());

            _Main.GetLoadlockData().Loadlock.SendCmd(nMotor, nCmd, nType, nBank, nValue, ref buffer);
            listViewLog.Items[nCount].SubItems.Add(BitConverter.ToString(buffer));
        }


        private void SendCmdToAll(byte nCmd, byte nType, byte nBank, int nValue)
        {
            for (int i = 1; i <= MAX_MOTOR; ++i)
            {
                SendCmd((byte)i, nCmd, nType, nBank, nValue);
                GetReply();
            }
        }


        private void buttonUp_MouseDown(object sender, MouseEventArgs e)
        {
            //int Acc = int.Parse(tbAcc.Text);
            //int Speed = int.Parse(tbSpeed.Text);

            //Loadlock.JogMove_P(Speed, Acc);
            GlobalSeq.autoRun.procLoadlock.JogMoveP();
        }

        private void buttonUp_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalSeq.autoRun.procLoadlock.MoveStop();
            //int Speed = int.Parse(tbSpeed.Text);
            //Loadlock.MoveStop(Speed);


        }

        private void buttonBottom_MouseDown(object sender, MouseEventArgs e)
        {
            //int Acc = int.Parse(tbAcc.Text);
            //int Speed = int.Parse(tbSpeed.Text);

            GlobalSeq.autoRun.procLoadlock.JogMoveN();
            //Loadlock.JogMove_N(Speed, Acc);
        }

        private void buttonBottom_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalSeq.autoRun.procLoadlock.MoveStop();
            //int Speed = int.Parse(tbSpeed.Text);
            //Loadlock.MoveStop(Speed);
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            bStatusFlag = true;
            int nHomeSpeed = int.Parse(tbSpeed.Text) / 2;

            GlobalSeq.autoRun.procLoadlock.HomeMove();

            //Loadlock.SetHomeRef(HomeRefMode.HomeRef_8);
            //Loadlock.MoveHome(nHomeSpeed);

            bStatusFlag = false;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            int Speed = int.Parse(tbSpeed.Text);
            //Loadlock.MoveStop(Speed);
            GlobalSeq.autoRun.procLoadlock.MoveStop();
        }

        private void buttonSetCoord1_Click(object sender, EventArgs e)
        {
            nTargetPos1 = nEncPos;
            tbPos1.Text = nTargetPos1.ToString();
            GlobalVariable.model.nLoadlockPos[0] = nTargetPos1;
        }

        private void buttonSetCoord2_Click(object sender, EventArgs e)
        {
            nTargetPos2 = nEncPos;
            tbPos2.Text = nTargetPos2.ToString();
            GlobalVariable.model.nLoadlockPos[1] = nTargetPos2;
        }

        private void buttonResCoord1_Click(object sender, EventArgs e)
        {
            bStatusFlag = true;
            int Pos = int.Parse(tbPos1.Text);
            int Speed = int.Parse(tbSpeed.Text);
            //lock(Read)
            //{
            GlobalSeq.autoRun.procLoadlock.MoveExit((int)MotionPos.Pos1);
            //}

            bStatusFlag = false;
            //GlobalSeq.autoRun.procLoadlock.Move(MotionPos.Pos1);
        }

        private void buttonResCoord2_Click(object sender, EventArgs e)
        {
            bStatusFlag = true;
            int Pos = int.Parse(tbPos2.Text);
            int Speed = int.Parse(tbSpeed.Text);
            GlobalSeq.autoRun.procLoadlock.MoveExit((int)MotionPos.Pos2);
            bStatusFlag = false;
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            int Pos = int.Parse(tbRelPos.Text);
            int Speed = int.Parse(tbSpeed.Text);
            //Loadlock.MoveStart(Pos, Speed, MoveMode.REL_MODE);

            GlobalSeq.autoRun.procLoadlock.MoveExit_Target(Pos, MoveMode.REL_MODE);
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                return;
            }
            else
            {
            }

//             lbSensorH.BackColor = (Loadlock.IsHome() == true) ? Color.LightGreen : Color.Gray;
//             lbSensorLmP.BackColor = (Loadlock.IsLimitP() == true) ? Color.LightGreen : Color.Gray;
//             lbSensorLmN.BackColor = (Loadlock.IsLimitP() == true) ? Color.LightGreen : Color.Gray;
//             lbMoving.BackColor = (Loadlock.IsMoving() == true) ? Color.LightGreen : Color.Gray;
//             lbInpos.BackColor = (Loadlock.Inposition() == true) ? Color.LightGreen : Color.Gray;
//             lbActPos.Text = Loadlock.GetActPos().ToString();
//             lbEncodPos.Text = Loadlock.GetEncoderPos().ToString();        
//             lbHomeStatus.Text = Loadlock.SearchStatus().ToString();

                lbSensorH.BackColor = (GlobalVariable.LoadlockMotor.bHome == true) ? Color.LightGreen : Color.Gray;
                lbSensorLmP.BackColor = (GlobalVariable.LoadlockMotor.bLimitP == true) ? Color.LightGreen : Color.Gray;
                lbSensorLmN.BackColor = (GlobalVariable.LoadlockMotor.bLmitN == true) ? Color.LightGreen : Color.Gray;
                lbMoving.BackColor = (GlobalVariable.LoadlockMotor.bMoving == true) ? Color.LightGreen : Color.Gray;
                lbInpos.BackColor = (GlobalVariable.LoadlockMotor.bInpos == true) ? Color.LightGreen : Color.Gray;
                lbActPos.Text = GlobalVariable.LoadlockMotor.nActPos.ToString();
                lbEncodPos.Text = GlobalVariable.LoadlockMotor.nEncPos.ToString();
                lbHomeStatus.Text = GlobalVariable.LoadlockMotor.nHomeStatus.ToString();
                lbAlarm.BackColor = (GlobalVariable.LoadlockMotor.bAlarm == true) ? Color.LightGreen : Color.Gray;
                lbServo.BackColor = (GlobalVariable.LoadlockMotor.bServo == true) ? Color.LightGreen : Color.Gray;
        }


        private void JogMode_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnHomeStop_Click(object sender, EventArgs e)
        {
            GlobalSeq.autoRun.procLoadlock.HomStop();
            //Loadlock.HomeStop(nModuleID);
        }

        private void subManualLoadlock_Leave(object sender, EventArgs e)
        {
            
        }

        private void btnAlarmReset_Click(object sender, EventArgs e)
        {
            Loadlock.AlarmReset();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnServoOn_Click(object sender, EventArgs e)
        {
            Loadlock.ServoOn();
        }

        private void btnServoOff_Click(object sender, EventArgs e)
        {
            Loadlock.ServoOff();
        }


        private void glassButton3_Click(object sender, EventArgs e)
        {
            int Speed = int.Parse(tbSpeed.Text);
            //Loadlock.MoveStop(Speed);
            GlobalSeq.autoRun.procLoadlock.MoveStop();
        }

        private void btnSetZero_Click(object sender, EventArgs e)
        {
            Loadlock.SetZero();
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            GlobalVariable.model.nLoadlockPos[0] = int.Parse(tbPos1.Text);
            GlobalVariable.model.nLoadlockPos[1] = int.Parse(tbPos2.Text);
            GlobalVariable.model.nLoadlockPos[2] = int.Parse(tbPos3.Text);
            GlobalVariable.model.nLLSpeed = int.Parse(tbSpeed.Text);
            GlobalVariable.model.nLLAcc = int.Parse(tbAcc.Text);

            ModelManager.Instance.SaveLoadlockData();
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            nTargetPos3 = nEncPos;
            tbPos3.Text = nTargetPos3.ToString();
            GlobalVariable.model.nLoadlockPos[2] = nTargetPos3;
        }

        private void btnStop2_Click(object sender, EventArgs e)
        {
            int Speed = int.Parse(tbSpeed.Text);
            //Loadlock.MoveStop(Speed);
            GlobalSeq.autoRun.procLoadlock.MoveStop();
        }

        private void btnAbsMove_Click(object sender, EventArgs e)
        {
            int Pos = int.Parse(lbAbsPos.Text);
            int Speed = int.Parse(tbSpeed.Text);
            GlobalSeq.autoRun.procLoadlock.MoveExit_Target(Pos, MoveMode.ABS_MODE);
        }

        private void btnSetRes_Click(object sender, EventArgs e)
        {
            if (tbRes.Text == "" || tbRes.Text == null)
            {
                return;
            }
            Loadlock.SetResolution(Convert.ToInt32(tbRes.Text));
        }


        private void glassButton2_Click(object sender, EventArgs e)
        {
            bStatusFlag = true;
            int Pos = int.Parse(tbPos3.Text);
            int Speed = int.Parse(tbSpeed.Text);
            GlobalSeq.autoRun.procLoadlock.MoveExit((int)MotionPos.Pos3);
            bStatusFlag = false;
        }

    }
}
