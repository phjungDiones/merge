using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_CTC.Data;
using TBDB_CTC.GLOBAL;
using TBDB_CTC.POPWND;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;
using TBDB_Handler.THREAD;
namespace TBDB_CTC.GUI
{
    public partial class frmMainFrame : Form
    {
        MainData _Main = null;

        frmLogIn fLogin;
        frmMainView fMain;
        frmSemiAUto fAuto;
        frmManual fManual;
        frmConfig fCfg;
        frmIO fIO;
        frmAlarm fAlarm;
        frmHistory fHistory;
        frmRecipe fRcp;
        popErrMessage ferr;
        frmAlarm fmAlarm;
        popKeyPad _popKeyPad;
        //frmLog fLog;

        popErrMessage errWidnow;

        popAutoRun autoRun;

        public frmMainFrame()
        {
            InitializeComponent();
        }
        public void ErrMessageFormShow()
        {
            ferr.ShowDialog();
        }

















        private void LoginLevel(int index)
        {
            if (index.Equals(MCDF.LEVEL_NOT_LOGIN))
            {//로그인안된상태
                btnToolIO.Enabled = false;
                btnToolMain.Enabled = false;
                btnToolAuto.Enabled = false;

                btnToolRecipe.Enabled = false;

                btnToolManual.Enabled = false;

                btnToolConfig.Enabled = false;

                btnToolAlarm.Enabled = false;


                btnToolHistory.Enabled = false;
                if (!(fmAlarm == null))
                {
                    ScreenChange(0);
                }

            }
            if (index.Equals(MCDF.LEVEL_OP))
            {//OP
                btnToolIO.Enabled = true;
                btnToolMain.Enabled = true;
                btnToolAuto.Enabled = true;

                btnToolRecipe.Enabled = true;

                btnToolManual.Enabled = true;

                btnToolConfig.Enabled = true;

                btnToolAlarm.Enabled = true;


                btnToolHistory.Enabled = true;
                if (!(fmAlarm == null))
                {
                    label5.Text = GlobalVariable.Login.ID;
                    label4.Text = GlobalVariable.Login.mode;
                    ScreenChange(1);
                    btnLogin.Text = "LogOut";
                    GlobalVariable.Login.blogin = true;
                }
            }
            if (index.Equals(MCDF.LEVEL_ENG))
            {//ENG
                if (!(fmAlarm == null))
                {
                    label5.Text = GlobalVariable.Login.ID;
                    label4.Text = GlobalVariable.Login.mode;
                    ScreenChange(1);
                    btnLogin.Text = "LogOut";
                    GlobalVariable.Login.blogin = true;
                }
            }
            if (index.Equals(MCDF.LEVEL_MAK))
            {//MAK
                if (!(fmAlarm == null))
                {
                    label5.Text = GlobalVariable.Login.ID;
                    label4.Text = GlobalVariable.Login.mode;
                    ScreenChange(1);
                    btnLogin.Text = "LogOut";
                    GlobalVariable.Login.blogin = true;
                }

            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoginLevel(MCDF.LEVEL_NOT_LOGIN);

            if (System.IO.File.Exists("AllErrorTables.xml"))
            {

                GlobalDataSet.dataset3.ReadXml("AllErrorTables_clone.xml", XmlReadMode.Auto);
                GlobalDataSet.dataset2.ReadXml("AllErrorTables.xml", XmlReadMode.Auto);
                if (GlobalDataSet.dataset3.Tables.Count == 0)
                {
                    GlobalDataSet.dataset3.Tables.Add("default");
                    DataRow newDr;
                    newDr = GlobalDataSet.dataset3.Tables[0].NewRow();

                    GlobalDataSet.dataset3.Tables[0].Columns.Add("AlarmDescription");
                    GlobalDataSet.dataset3.Tables[0].Columns.Add("index");
                    GlobalDataSet.dataset3.Tables[0].Columns.Add("Model");
                    GlobalDataSet.dataset3.Tables[0].Columns.Add("No");
                    GlobalDataSet.dataset3.Tables[0].Columns.Add("DateTime");
                    GlobalDataSet.dataset3.WriteXml("AllErrorTables_clone.xml");
                    GlobalDataSet.dataset3.ReadXml("AllErrorTables_clone.xml", XmlReadMode.Auto);
                }


            }
            else
            {


                DataRow newDr;
                GlobalDataSet.dataset2.Tables.Add("default");
                GlobalDataSet.dataset3.Tables.Add("default");
                if (GlobalDataSet.dataset2.Tables[0].Columns.Count == 0)
                {
                    newDr = GlobalDataSet.dataset2.Tables[0].NewRow();

                    GlobalDataSet.dataset2.Tables[0].Columns.Add("AlarmDescription");
                    GlobalDataSet.dataset2.Tables[0].Columns.Add("index");
                    GlobalDataSet.dataset2.Tables[0].Columns.Add("Model");
                    GlobalDataSet.dataset2.Tables[0].Columns.Add("No");
                    GlobalDataSet.dataset2.Tables[0].Columns.Add("DateTime");
                    GlobalDataSet.dataset2.WriteXml("AllErrorTables.xml");
                    GlobalDataSet.dataset2.ReadXml("AllErrorTables.xml", XmlReadMode.Auto);


                    newDr = GlobalDataSet.dataset3.Tables[0].NewRow();

                    GlobalDataSet.dataset3.Tables[0].Columns.Add("AlarmDescription");
                    GlobalDataSet.dataset3.Tables[0].Columns.Add("index");
                    GlobalDataSet.dataset3.Tables[0].Columns.Add("Model");
                    GlobalDataSet.dataset3.Tables[0].Columns.Add("No");
                    GlobalDataSet.dataset3.Tables[0].Columns.Add("DateTime");
                    GlobalDataSet.dataset3.WriteXml("AllErrorTables_clone.xml");
                    GlobalDataSet.dataset3.ReadXml("AllErrorTables_clone.xml", XmlReadMode.Auto);

                }
            }
            CJ_Controls.Communication.Test.GlobalEvent.GetErrorEvent += delegate { POPWND.Error.Test.GlobalVariable.Instance.SetErr((Eidentify_error)CJ_Controls.Communication.Test.GlobalDefine.Instance.robot_index, CJ_Controls.Communication.Test.GlobalDefine.Instance.sRcvData); };

            TBDB_CTC.POPWND.Error.Test.GlobalVariable.Instance.Click += ErrMessageFormShow;
            popAppLoading popLoading = new popAppLoading();
            DialogResult result = popLoading.ShowDialog();

            GlobalSeq.autoRun = new TBDB_Handler.THREAD.AutoRun();
            GlobalSeq.manualRun = new TBDB_Handler.THREAD.ManualRun();

            GlobalSeq.autoRun.prcATM.errSaftyStopEvent += ShowSafetyAlarm;
            GlobalSeq.autoRun.prcVTM.errSaftyStopEvent += ShowSafetyAlarm;
            GlobalSeq.autoRun.prcFM.errSaftyStopEvent += ShowSafetyAlarm;

            autoRun = new popAutoRun();

            _Main = MainData.Instance;
            _Main.Init();

            fLogin = GlobalForm.fLogin;
            fMain = GlobalForm.fMain;
            fAuto = GlobalForm.fAuto;
            fManual = GlobalForm.fManual;
            fCfg = GlobalForm.fCfg;
            fIO = GlobalForm.fIO;
            fAlarm = GlobalForm.fAlarm;
            fHistory = GlobalForm.fHistory;
            fRcp = GlobalForm.fRcp;
 errWidnow = new popErrMessage();//겹침
            ferr = GlobalForm.fErr;
            fmAlarm = GlobalForm.fmAlarm;
            _popKeyPad = GlobalForm._popKeyPad;



            fLogin.MdiParent = this;
            fLogin.Parent = this.panClientView;

            fMain.MdiParent = this;
            fMain.Parent = this.panClientView;

            fAuto.MdiParent = this;
            fAuto.Parent = this.panClientView;

            fManual.MdiParent = this;
            fManual.Parent = this.panClientView;

            fCfg.MdiParent = this;
            fCfg.Parent = this.panClientView;

            fIO.MdiParent = this;
            fIO.Parent = this.panClientView;

            fAlarm.MdiParent = this;
            fAlarm.Parent = this.panClientView;

            fHistory.MdiParent = this;
            fHistory.Parent = this.panClientView;

            fRcp.MdiParent = this;
            fRcp.Parent = this.panClientView;

            fmAlarm.MdiParent = this;
            fmAlarm.Parent = this.panClientView;


            fLogin.Show();


            fLogin.userLogin += LoginLevel;
            autoRun.RunStopEvent += Autorun;
            autoRun.ResetEvent += ResetEvent;

            fCfg.LoadConfig(); //ComPort Open
            CfgManager.Instance.LoadConfigFile(); //Config Data Load
            tmrRefresh.Enabled = true;

            //GlobalVariable.io.StartReadIO(); //DeviceNet Open Read

            GlobalVariable.seqShared.Init(COUNT.MAX_PORT_SLOT);

            //프로그램 로딩 후 초기 셋팅
            InitSetting();

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
#if !_REAL_MC
            //전체 홈 시퀀스 이후에 따로 연결해야 함
            GlobalVariable.mcState.isRdy = true;
            GlobalVariable.mcState.isRun = true;
            return;
#endif

            short status = 0;
            if (GlobalVariable.mcState.isErr) return;
            //if (!GlobalVariable.mcState.isRdy) return;
//             if (GlobalVariable.mcState.isInitializing) return;
//             if (GlobalVariable.mcState.isWateForStop) return;
//             if (GlobalVariable.mcState.isMovingStopPos) return;

            GlobalSeq.autoRun.ResetCmd();

            //통신 연결 상태 확인
            //로봇 알람, Status 확인

            //PMC Vacuum Status 확인
            GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(PMC_STATUS.PMCVacuumStatus, ref status);
            if (status != (short)VTM_VACUUM_STATUS_VALUE.VTM)
            {
                MessageBox.Show("PMC가 VTM상태가 아닙니다. 확인하세요.");
                return;
            }

            if( GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(CTC_STATUS.CTCRunMode, ref status)
                != GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(PMC_STATUS.PMCRunMode, ref status) )
            {
                MessageBox.Show("CTC와 PMC의 RunMode 상태가 다릅니다. 확인하세요.");
                return;
            }

            //RunMode가 VTM일경우, VTM Pumping상태가 아닐경우 한번 실행
            if (GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_1) == false
                || GlobalVariable.io.ReadInput(GlobalVariable.io.I_VacuumTm_ConvectronRy_2) == false )
            {
                //VTM 상태가 아닐경우 VTM Pumping

//                 fn fRet = GlobalSeq.autoRun.procLoadlock.VTM_Pumping();
//                 if (fRet == fn.success) break;
//                 else if (fRet == fn.busy) return;
//                 else
//                 {
//                     //Error
//                     return;
//                 }

            }

            //전체 홈 시퀀스 이후에 따로 연결해야 함
                GlobalVariable.mcState.isRdy = true; 
            GlobalVariable.mcState.isRun = true;

            GlobalSeq.autoRun.prcVTM.Pmc.SetStatus(CTC_STATUS.CTCStatus, (short)CTC_STATUS_VALUE.RUN);
        }

        void StopProcess()
        {
            GlobalVariable.mcState.isRun = false;
            GlobalVariable.mcState.isManualRun = false;

            //정지될때 까지 기다린다.
            WaitStop();
            GlobalVariable.mcState.isCheckStopPos = true;

            GlobalSeq.autoRun.prcVTM.Pmc.SetStatus(CTC_STATUS.CTCStatus, (short)CTC_STATUS_VALUE.STOP);
        }

        void ResetEvent()
        {
            if (!GlobalVariable.mcState.isErr) return;
            //errWidnow.resetError();
        }

        public void WaitStop()
        {

        }

        void InitSetting()
        {
            //VTM Robot Extend 동작 IO on
            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VtmArmExt_1, true);
            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VtmArmExt_2, true);
            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VtmArmExt_4, true);
            GlobalVariable.io.WriteOutput(GlobalVariable.io.O_VtmArmExt_5, true);


        }

        void ShowSafetyAlarm(int nSafetyErrNo)
        {
            //LogManager.Instance.WriteI(LOG_TYPE.ERROR, "{0} : ShowSafetyAlarm [{1}]",this.GetType().Name, nSafetyErrNo);

            // AutoRun인지 확인
            bool bIsAutoRun = GlobalVariable.mcState.isRun;

            //StopProcess();

            if (GlobalVariable.mcState.isErr) return;
            GlobalVariable.mcState.isErr = true;
            GlobalVariable.mcState.nLastErrNo = nSafetyErrNo;

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(ShowErrorWin));
            }
            else
            {
                ShowErrorWin();
            }

        }

        void ShowErrorWin()
        {
         //  errWidnow.setErrorText();
           errWidnow.TopMost = true;
           errWidnow.Show();
        }

        private void ResetToolButton()
        {
            btnToolMain.BackColor = Color.Black;
            btnToolAuto.BackColor = Color.Black;
            btnToolManual.BackColor = Color.Black;
            btnToolRecipe.BackColor = Color.Black;
            btnToolIO.BackColor = Color.Black;
            btnToolAlarm.BackColor = Color.Black;
            btnToolHistory.BackColor = Color.Black;
            btnToolConfig.BackColor = Color.Black;

            btnToolMain.ForeColor = Color.White;
            btnToolAuto.ForeColor = Color.White;
            btnToolManual.ForeColor = Color.White;
            btnToolRecipe.ForeColor = Color.White;
            btnToolIO.ForeColor = Color.White;
            btnToolAlarm.ForeColor = Color.White;
            btnToolHistory.ForeColor = Color.White;
            btnToolConfig.ForeColor = Color.White;
        }

        public void ScreenChange(int nScreenNo)
        {
            ResetToolButton();

            // VisibleChanged Event를 받기 위해 추가
            fLogin.Hide();
            fMain.Hide();
            fAuto.Hide();
            fManual.Hide();
            fCfg.Hide();
            fIO.Hide();
            fAlarm.Hide();
            fHistory.Hide();
            fRcp.Hide();
            fmAlarm.Hide();

            //Show Screen
            switch (nScreenNo)
            {
                case 0://DF.SCREEN_LOGIN:

                    //Logout 상태로 만듬.
                    //GlobalVariable.Login.nLoginLevel = MCDF.LEVEL_NOT_LOGIN;

                    //SetEnableToolBtn(false);
                    fLogin.Show();
                    fLogin.BringToFront();
                    btnLogin.BackColor = Color.Orange;
                    //setAllScreenDisabel();
                    //GlobalFunction.WriteLogging("User Log Out");
                    //GlobalVariable.nActiveScreen = MCDF.SCREEN_LOGIN;
                    break;

                case 1://DF.SCREEN_AUTO:
                    fMain.Show();
                    fMain.BringToFront();
                    btnToolMain.BackColor = Color.FromArgb(64, 64, 64);
                    btnToolMain.ForeColor = Color.Orange;
                    //GlobalVariable.nActiveScreen = MCDF.SCREEN_AUTO;
                    break;

                case 2://DF.SCREEN_LOG:
                    fAuto.Show();
                    fAuto.BringToFront();
                    btnToolAuto.BackColor = Color.FromArgb(64, 64, 64);
                    btnToolAuto.ForeColor = Color.Orange;
                    //GlobalVariable.nActiveScreen = MCDF.SCREEN_LOG;
                    break;

                case 3://DF.SCREEN_FUNC:
                    fRcp.Show();
                    fRcp.BringToFront();
                    btnToolRecipe.BackColor = Color.FromArgb(64, 64, 64);
                    btnToolRecipe.ForeColor = Color.Orange;
                    //GlobalVariable.nActiveScreen = MCDF.SCREEN_FUNC;
                    break;

                case 4://DF.SCREEN_TEACH:
                    fManual.Show();
                    fManual.BringToFront();
                    btnToolManual.BackColor = Color.FromArgb(64, 64, 64);
                    btnToolManual.ForeColor = Color.Orange;
                    //GlobalVariable.nActiveScreen = MCDF.SCREEN_TEACH;
                    break;

                case 5://DF.SCREEN_CONFIG:
                    fCfg.Show();
                    fCfg.BringToFront();
                    btnToolConfig.BackColor = Color.FromArgb(64, 64, 64);
                    btnToolConfig.ForeColor = Color.Orange;
                    //GlobalVariable.nActiveScreen = MCDF.SCREEN_CONFIG;
                    break;

                case 6://DF.SCREEN_REPORT:
                    fIO.Show();
                    fIO.BringToFront();
                    btnToolIO.BackColor = Color.FromArgb(64, 64, 64);
                    btnToolIO.ForeColor = Color.Orange;
                    //GlobalVariable.nActiveScreen = MCDF.SCREEN_ERROR;
                    break;
                case 7://DF.SCREEN_REPORT:
                    fAlarm.Show();
                    fAlarm.BringToFront();
                    btnToolAlarm.BackColor = Color.FromArgb(64, 64, 64);
                    btnToolAlarm.ForeColor = Color.Orange;
                    //GlobalVariable.nActiveScreen = MCDF.SCREEN_ERROR;
                    break;
                case 8://DF.SCREEN_REPORT:
                    fHistory.Show();
                    fHistory.BringToFront();
                    btnToolHistory.BackColor = Color.FromArgb(64, 64, 64);
                    btnToolHistory.ForeColor = Color.Orange;
                    //GlobalVariable.nActiveScreen = MCDF.SCREEN_ERROR;
                    break;

                default: break;
            }
        }

        void fLogin_userLogin(int nScrNo)
        {
            //btnToolMain,btnToolAuto,btnToolRecipe,btnToolManual,btnToolConfig,btnToolIO,btnToolAlarm,btnToolHistory
            btnLogin.Text = "LOGOUT";
            btnToolMain.Enabled = true;
            btnToolAuto.Enabled = true;

            btnToolRecipe.Enabled = true;

            btnToolManual.Enabled = true;

            btnToolConfig.Enabled = true;

            btnToolAlarm.Enabled = true;
            btnToolIO.Enabled = true;

            btnToolHistory.Enabled = true;

            //btnLogin.Text = "LOGOUT";
            ScreenChange(nScrNo);
        }

        private void OnToolButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ScreenChange(Convert.ToInt16(btn.Tag));
          //  fmAlarm.resetindex();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to quit", "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes) 
            {
                tmrRefresh.Enabled = false;
                this.Close(); /*Application.Exit();*/ 
            }
        }

        private void frmMainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalSeq.autoRun.Dispose();
            GlobalSeq.manualRun.Dispose();
            fCfg.CloseConfig();
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            dtxDay.DigitText = DateTime.Now.ToString("yyyy-MM-dd");
            dtxTime.DigitText = DateTime.Now.ToString("HH:mm:ss tt");
        }

        private void btnAutoRun_Click(object sender, EventArgs e)
        {
            if (autoRun.IsDisposed) autoRun = new popAutoRun();
            autoRun.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {//label5 ID
            //label4 LEVEL
            if (GlobalVariable.Login.blogin)
            {
                GlobalVariable.Login.blogin = false;
                LoginLevel(MCDF.LEVEL_NOT_LOGIN);
                btnLogin.Text = "LOG-IN";
                btnLogin.BackColor = Color.DimGray;
                label5.Text = "";
                label4.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
