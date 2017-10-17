
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_CTC.FileLib;
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.POPWND
{
    public partial class popErrMessage : Form
    {
        public static int nErrNo = 0;
        private Stopwatch timeStamp = Stopwatch.StartNew();

        public popErrMessage()
        {
            InitializeComponent();
        }

        public void resetError()
        {
            GlobalVariable.mcState.isErr = false;
            
            timeStamp.Stop();
            tmrErr.Enabled = false;
            this.Hide();
        }

        public void setErrorText()
        {

            return;


            iniUtil ini = new iniUtil(PathManager.Instance.FILE_LAST_ERROR_DEF);
            if (System.IO.File.Exists(PathManager.Instance.FILE_LAST_ERROR_DEF))
            {
                ERDF.nErrHistCnt[GlobalVariable.mcState.nLastErrNo]++;
                string strLastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ini.SetIniValue("ERROR_COUNT", "COUNT" + GlobalVariable.mcState.nLastErrNo.ToString(), ERDF.nErrHistCnt[GlobalVariable.mcState.nLastErrNo].ToString());
                ini.SetIniValue("LAST_TIME", "TIME" + GlobalVariable.mcState.nLastErrNo.ToString(), strLastTime);
                ERDF.strErrHistLastTime[GlobalVariable.mcState.nLastErrNo] = strLastTime;
            }

            lblErrNo.Text = "E." + GlobalVariable.mcState.nLastErrNo.ToString();
            
            lblTitle.Text = ERDF.sErrTitle[GlobalVariable.mcState.nLastErrNo];
            lblCause.Text = ERDF.sErrCause[GlobalVariable.mcState.nLastErrNo];
            lblAct1.Text = ERDF.sErrAction[GlobalVariable.mcState.nLastErrNo];
            lblCnt.Text = ERDF.nErrHistCnt[GlobalVariable.mcState.nLastErrNo].ToString();

            timeStamp.Restart();
            tmrErr.Enabled = true;
            //FrmMain.logAdd(DF.ERROR_LOG, "[E." + lblErrNo.Text + "] " + ERDF.sErrTitle[GVAR.mcState.nLastErrNo]);
        }

        private void tmrErr_Tick(object sender, EventArgs e)
        {
            string strElapsedTime = String.Format("{0:00}", timeStamp.Elapsed.Hours) + ":";
            strElapsedTime += (String.Format("{0:00}", timeStamp.Elapsed.Minutes) + ":");
            strElapsedTime += (String.Format("{0:00}", timeStamp.Elapsed.Seconds));
            lblTime.DigitText = strElapsedTime;
            if (lblErrNo.ForeColor == Color.OrangeRed) { lblErrNo.ForeColor = Color.FromArgb(50, 50, 50); }
            else { lblErrNo.ForeColor = Color.OrangeRed; }

            //if (FrmMain.sysCfg.timeOut.lBzActiveTime != 0)
            //{
            //    if (timeStamp.ElapsedMilliseconds >= FrmMain.sysCfg.timeOut.lBzActiveTime)
            //    {
            //        motion.setOut(IODF.O_BUZZER, false);
            //    }
            //}
        }

        private void popErrMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            resetError();
        }
    }
}
