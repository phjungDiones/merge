using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DionesTool.UTIL;
using TBDB_CTC.GUI;
using System.IO;
using TBDB_CTC.GLOBAL;
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.POPWND
{
    public partial class popAppLoading : Form
    {
        int nRetryCount = 0;

        public popAppLoading()
        {
            InitializeComponent();
        }

        private void popAppLoading_Shown(object sender, EventArgs e)
        {
            Util.Delay(100);

            GlobalForm.fLogin = new frmLogIn();
            prgLoading.Value = 10;
            GlobalForm.fMain = new frmMainView();
            prgLoading.Value = 20;
            GlobalForm.fAuto = new frmSemiAUto();
            prgLoading.Value = 30;
            GlobalForm.fRcp = new frmRecipe();
            prgLoading.Value = 40;
            GlobalForm.fManual = new frmManual();
            prgLoading.Value = 50;

            GlobalForm.fCfg = new frmConfig();
            prgLoading.Value = 60;
            GlobalForm.fIO = new frmIO();
            prgLoading.Value = 70;
            GlobalForm.fAlarm = new frmAlarm();
            prgLoading.Value = 80;
            GlobalForm.fHistory = new frmHistory();
            prgLoading.Value = 90;
            GlobalForm.fErr = new popErrMessage();
            prgLoading.Value = 95;
            GlobalForm.fTest = new frmTestForm();
            prgLoading.Value = 96;
            GlobalForm.fmAlarm = new frmAlarm();
            prgLoading.Value = 97;
            GlobalForm._popKeyPad = new popKeyPad();

            GlobalForm.fErr.Hide(); // 에러폼숨기기





            Util.Delay(100);
            prgLoading.Value = 100;

            lbLoadingStatus.Text = "TBDB-CTC program start";
            Util.Delay(100);
            lbLoadingStatus.Text = "TBDB-CTC program start.";
            Util.Delay(100);
            lbLoadingStatus.Text = "TBDB-CTC program start..";
            Util.Delay(100);
            lbLoadingStatus.Text = "TBDB-CTC program start...";
            Util.Delay(100);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
