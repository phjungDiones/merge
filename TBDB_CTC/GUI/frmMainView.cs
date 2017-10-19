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
using TBDB_CTC.GLOBAL;
using TBDB_CTC.UserCtrl.SubForm;
using TBDB_CTC.UserCtrl.SubForm.MainSub;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;

namespace TBDB_CTC.GUI
{
    public partial class frmMainView : Form
    {
        public UserControl[] wndMainSubMenu = new UserControl[2];

        short status = 0;

        public frmMainView()
        {
            InitializeComponent();

            wndMainSubMenu[0] = new subMainScreen();
            wndMainSubMenu[1] = new subMainStatus();

            panMainSubClient.Controls.Add(wndMainSubMenu[0]);
            panMainSubClient.Controls.Add(wndMainSubMenu[1]);
        }

        private void frmMainView_Load(object sender, EventArgs e)
        {
            SubScreenChange(0);

            cbRunMode.SelectedIndex = 0;
            SetCtcRunMode();
        }

        private void SetCtcRunMode()
        {
            if (cbRunMode.SelectedIndex == 1)
            {
                lbCtcRunMode.Text = "RunMode : VTM";
                lbCtcRunMode.ForeColor = Color.LimeGreen;
                GlobalSeq.autoRun.prcVTM.Pmc.SetStatus(CTC_STATUS.CTCRunMode, (short)CTC_RUN_MODE_VALUE.VTM_MODE);
            }
            else
            {
                lbCtcRunMode.Text = "RunMode : ATM";
                lbCtcRunMode.ForeColor = Color.White;
                GlobalSeq.autoRun.prcVTM.Pmc.SetStatus(CTC_STATUS.CTCRunMode, (short)CTC_RUN_MODE_VALUE.ATM_MODE);
            }
        }

        private void OnManualSubMenuButtonClick(object sender, EventArgs e)
        {
            Label lbSubMn = sender as Label;
            SubScreenChange(Convert.ToInt32(lbSubMn.Tag));
        }

        public void ChangeManualSub(int nPage)
        {
            wndMainSubMenu[0].Hide();
            wndMainSubMenu[1].Hide();

            switch (nPage)
            {
                case 0:
                    wndMainSubMenu[nPage].Show();
                    break;
                case 1:
                    wndMainSubMenu[nPage].Show();
                    break;

                default :
                    break;
            }
        }

        public void SubScreenChange(int nPageNo)
        {
            ChangeManualSub(nPageNo);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!this.Visible) return;

            GlobalSeq.autoRun.prcVTM.Pmc.GetStatus(CTC_STATUS.CTCRunMode, ref status);

            if(status == (short)CTC_RUN_MODE_VALUE.ATM_MODE)
            {
                ledCtcModeATM.Color = Color.LightBlue;
                ledCtcModeVTM.Color = Color.LightSlateGray;
            }
            else
            {
                ledCtcModeATM.Color = Color.LightSlateGray;
                ledCtcModeVTM.Color = Color.LightBlue;
            }
            
        }

        private void cbRunMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCtcRunMode();

//             DialogResult result = MessageBox.Show("모드를 변경 하시겠습니까?", "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
//             if (result == DialogResult.Yes)
//             {
//                 SetCtcRunMode();
//             }
//             else
//             {
//                 return;
//             }
        }
    }
}
