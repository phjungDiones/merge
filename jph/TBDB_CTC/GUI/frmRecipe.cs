using Owf.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_CTC.UserCtrl.SubForm.RecipeSub;

namespace TBDB_CTC.GUI
{
    public partial class frmRecipe : Form
    {
        public UserControl[] wndRecipeSubMenu = new UserControl[7];

        public frmRecipe()
        {
            InitializeComponent();

            wndRecipeSubMenu[0] = new subRecipeLot();
            wndRecipeSubMenu[1] = new subRecipeCluster();
            wndRecipeSubMenu[2] = new subRecipeProcess();
            wndRecipeSubMenu[3] = new subRecipeProcAlign();
            wndRecipeSubMenu[4] = new subRecipeProcLami();
            wndRecipeSubMenu[5] = new subRecipeProcBond();
            wndRecipeSubMenu[6] = new subRecipe();

            foreach (UserControl uc in wndRecipeSubMenu)
            {
                panRecipeSubClient.Controls.Add(uc);
            }
        }

        private void frmRecipe_Load(object sender, EventArgs e)
        {
            //wndRecipeSubMenu[0].Show();
            RecipeSubMenuScreenChange(6);

            //if (contextProcessMenuStrip != null)
            //{
            //    lbRcp_2.ContextMenuStrip = contextProcessMenuStrip;
            //}
        }

        private void lbRcp_0_Click(object sender, EventArgs e)
        {
            Label btn = sender as Label;
            int nTag = Convert.ToInt16(btn.Tag);

            RecipeSubMenuScreenChange(nTag);
        }

        public void RecipeSubMenuScreenChange(int nScreenNo, int nSubScreenNo = 0)
        {
            int nTag = nScreenNo;

            foreach (UserControl uc in wndRecipeSubMenu)
            {
                uc.Hide();
            }

            Label[] lblArray = { lbRcp_0, lbRcp_1, lbRcp_2, lbRcp_3, lbRcp_4, lbRcp_5, lbRcp_6 };
            A1Panel[] pnlArray = { panSubMenuC0, panSubMenuC1, panSubMenuC2, panSubMenuC3, panSubMenuC4, panSubMenuC5, panSubMenuC6 };

            foreach (Label lbl in lblArray)
            {
                lbl.ForeColor = Color.Black;
            }

            foreach (A1Panel pnl in pnlArray)
            {
                pnl.GradientEndColor = SystemColors.ButtonFace;
            }

            wndRecipeSubMenu[nTag].Show();
            lblArray[nTag].ForeColor = Color.Gold;
            pnlArray[nTag].GradientEndColor = Color.Black;

            ResetProcessMenuLabel();
        }

        public void ResetProcessMenuLabel()
        {
            lbProcessSubMenuItem_0.ForeColor = Color.White;
            lbProcessSubMenuItem_1.ForeColor = Color.White;
            lbProcessSubMenuItem_2.ForeColor = Color.White;
            lbProcessSubMenuItem_3.ForeColor = Color.White;
        }

        private void contextProcessMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            (sender as ContextMenuStrip).Close();

            int nSelectSubNo = 0;
            switch (e.ClickedItem.Text)
            {
                case "Lamination":
                    nSelectSubNo = 0;
                    break;
                case "Bonder":
                    nSelectSubNo = 1;
                    break;
                case "Aligner":
                    nSelectSubNo = 2;
                    break;
                case "H.P":
                    nSelectSubNo = 3;
                    break;
                default:
                    break;
            }

            RecipeSubMenuScreenChange(2, nSelectSubNo);
            ResetProcessMenuLabel();
            
            Label lb = Controls.Find("lbProcessSubMenuItem_" + nSelectSubNo, true).FirstOrDefault() as Label;
            lb.ForeColor = Color.OrangeRed;
        }

        public void ShowContextMenu()
        {
            contextProcessMenuStrip.Show();
        }

        private void lbRcp_2_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    MethodInfo mi = typeof(Label).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            //    mi.Invoke(lbRcp_2, null);
            //}

            if (e.Button == MouseButtons.Left)
            {
                contextProcessMenuStrip.Show(MousePosition.X, MousePosition.Y);
            }
            else
            {
                contextProcessMenuStrip.Visible = false;
            }
        }
    }
}
