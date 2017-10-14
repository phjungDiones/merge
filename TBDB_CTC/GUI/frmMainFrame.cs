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
        //frmLog fLog;

        popAutoRun autoRun;

        public frmMainFrame()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
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

            fLogin.Show();

            fLogin.userLogin += fLogin_userLogin;

            fCfg.LoadConfig(); //ComPort Open
            CfgManager.Instance.LoadConfigFile(); //Config Data Load
            tmrRefresh.Enabled = true;

            //GlobalVariable.io.StartReadIO(); //DeviceNet Open Read

            GlobalVariable.seqShared.Init(COUNT.MAX_PORT_SLOT);
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
            //if (nErrNo != ERDF.LOTEND)
            //{
            //    GVAR.mcState.isErr = true;
            //}
            //isErrStop = true;

            if (this.InvokeRequired)
            {
                //this.Invoke(new MethodInvoker(ShowErrorWin));
            }
            else
            {
                //ShowErrorWin();
            }

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
            //btnLogin.Text = "LOGOUT";
            //btnAuto.Enabled = true;
            //btnManual.Enabled = true;
            //btnLog.Enabled = true;
            //btnIo.Enabled = true;
            //btnModel.Enabled = true;
            //btnCfg.Enabled = true;

            //btnLogin.Text = "LOGOUT";
            ScreenChange(nScrNo);
        }

        private void OnToolButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ScreenChange(Convert.ToInt16(btn.Tag));
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
    }
}
