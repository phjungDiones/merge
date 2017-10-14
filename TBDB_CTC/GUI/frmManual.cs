using Glass;
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
using TBDB_CTC.UserCtrl.SubForm.ManualSub;

namespace TBDB_CTC.GUI
{
    public partial class frmManual : Form
    {

        subManualAligner Aligner = null;
        subManualFM FmRobot = null;
        subManualTM AtmRobot = null;
        subManualVTM VtmRobot = null;
        subManualLoadlock Loadlock = null;
        subManualLPM Lpm = null;
        subManualETC Etc = null;

        subProcBonder Bonder = null;
        subProcLami Lami = null;

        public frmManual()
        {
            InitializeComponent();
        }


        private void tabManual_Load(object sender, EventArgs e)
        {

            Aligner = new subManualAligner();
            FmRobot = new subManualFM();
            AtmRobot = new subManualTM();
            VtmRobot = new subManualVTM();
            Loadlock = new subManualLoadlock();
            Lpm = new subManualLPM();
            Etc = new subManualETC();

            Bonder = new subProcBonder();
            Lami = new subProcLami();

            Aligner.Parent = this.panMnlSubClient;
            FmRobot.Parent = this.panMnlSubClient;
            AtmRobot.Parent = this.panMnlSubClient;
            VtmRobot.Parent = this.panMnlSubClient;
            Loadlock.Parent = this.panMnlSubClient;
            Lpm.Parent = this.panMnlSubClient;
            Etc.Parent = this.panMnlSubClient;

            Bonder.Parent = this.panProcSubClient;
            Lami.Parent = this.panProcSubClient;

            TabManual.SelectedIndex = 0;
            //btnMnlOper0.BackColor = Color.Green;

            btnProc_0.BackColor = Color.Aqua;
        }

        private void btnProc_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            Bonder.Hide();
            Lami.Hide();

            btnProc_0.BackColor = Color.DimGray;
            btnProc_1.BackColor = Color.DimGray;

            switch (nTag)
            {
                case 0:
                    //Bonder
                    Bonder.Show();
                    btnProc_0.BackColor = Color.Aqua;
                    break;

                case 1:
                    //Lami
                    Lami.Show();
                    btnProc_1.BackColor = Color.Aqua;
                    break;
            }
        }


        private void btnMnlOper0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);
          
            Button fndBtn;

            for(int i=0; i<7; i++)
            {
                string strCompName = string.Format("btnMnlOper{0}", i);

                fndBtn = this.Controls.Find(strCompName, true).FirstOrDefault() as Button;
                if (fndBtn != null)
                {
                    fndBtn.BackColor = Color.Black;
                }
            }
            btn.BackColor = Color.Green;
            ChangeManualSub(nTag);
        }

        public void ChangeManualSub(int nPage)
        {
            Aligner.Hide();
            FmRobot.Hide();
            AtmRobot.Hide();
            VtmRobot.Hide();
            Loadlock.Hide();
            Lpm.Hide();
            Etc.Hide();

            switch (nPage)
            {
                case 0:
                    FmRobot.Show();
                    break;
                case 1:
                    AtmRobot.Show();
                    break;                    
                case 2:
                    VtmRobot.Show();
                    break;
                case 3:
                    Lpm.Show();
                    break;
                case 4:
                    Aligner.Show();
                    break;
                case 5:
                    Loadlock.Show();
                    break;
                case 6:
                    Etc.Show();
                    break;
                case 7:
                    break;
                case 8:
                    break;
            }
        }

        private void OnManualSubMenuButtonClick(object sender, EventArgs e)
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

            TabManual.SelectedIndex = nPageNo;
        }
    }
}
