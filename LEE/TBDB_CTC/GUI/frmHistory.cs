using Owf.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_CTC.UserCtrl.SubForm.HistorySub;
using TBDB_CTC.UserCtrl.SubForm.LogSub;

namespace TBDB_CTC.GUI
{
    public partial class frmHistory : Form
    {
        public UserControl[] wndHistorySubMenu = new UserControl[7];

        public frmHistory()
        {
            InitializeComponent();

            wndHistorySubMenu[0] = new subLogEvent();
            wndHistorySubMenu[1] = new subLogLogging();
            wndHistorySubMenu[2] = new subHistoryLotRun();
            wndHistorySubMenu[3] = new subHistoryWaferRun();
            wndHistorySubMenu[4] = new subHistoryLotProc();
            wndHistorySubMenu[5] = new subHistoryWaferProc();
            wndHistorySubMenu[6] = new subLogDebug();

            panHistorySubClient.Controls.Add(wndHistorySubMenu[0]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[1]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[2]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[3]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[4]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[5]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[6]);
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            wndHistorySubMenu[0].Show();
        }

        private void OnHistorySubMenuButtonClick(object sender, EventArgs e)
        {
            Label lbSubMn = sender as Label;

            SubScreenChange(Convert.ToInt32(lbSubMn.Tag));
        }

        public void ResetSubMenuButton()
        {
            for (int nCtrlCount = 0; nCtrlCount < 10; nCtrlCount++)
            {
                A1Panel pBtn = Controls.Find("panSubMenuC" + nCtrlCount.ToString(), true).FirstOrDefault() as A1Panel;
                if (pBtn != null)
                {
                    pBtn.GradientEndColor = SystemColors.ButtonFace;
                    pBtn.GradientStartColor = Color.DimGray;
                }

                Label Lb = Controls.Find("lbSubMenuC" + nCtrlCount.ToString(), true).FirstOrDefault() as Label;
                if (Lb != null)
                {
                    Lb.ForeColor = Color.Black;
                }
            }
        }

        public void SubScreenChange(int nPageNo)
        {
            ResetSubMenuButton();

            A1Panel pBtn = Controls.Find("panSubMenuC" + nPageNo.ToString(), true).FirstOrDefault() as A1Panel;
            if (pBtn != null)
            {
                pBtn.GradientEndColor = Color.Black;
                pBtn.GradientStartColor = Color.DimGray;
            }

            Label Lb = Controls.Find("lbSubMenuC" + nPageNo.ToString(), true).FirstOrDefault() as Label;
            if (Lb != null)
            {
                Lb.ForeColor = Color.Gold;
            }

            for (int nCfgSubMenuCount = 0; nCfgSubMenuCount < 7; nCfgSubMenuCount++)
            {
                wndHistorySubMenu[nCfgSubMenuCount].Hide();
            }

            wndHistorySubMenu[nPageNo].Show();
        }
    }
}
