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

namespace TBDB_CTC.GUI
{
    public partial class frmMainView : Form
    {
        public UserControl[] wndMainSubMenu = new UserControl[2];

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
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //AddProcMessage(0, 1, "AAAA");
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

    }
}
