using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBDB_CTC.GUI
{
    public partial class frmLogIn : Form
    {
        string strUserLevel = "OPERATOR";
        public delegate void UserLogin(int nScrNo);
        public event UserLogin userLogin = null;

        public frmLogIn()
        {
            InitializeComponent();

            //GlobalVariable.Login.nLoginLevel = MCDF.LEVEL_OP;
            btnOp.BackColor = Color.Gold;
            txtPass.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (userLogin != null) userLogin(1);
        }

        private void OnChangeUserLevelButtonClick(object sender, EventArgs e)
        {
            Button pBtn = (Button)sender;
            int nTag = 0;
            if (!int.TryParse(pBtn.Tag.ToString(), out nTag)) return;

            btnOp.BackColor = SystemColors.Control;
            btnEng.BackColor = SystemColors.Control;
            btnMaker.BackColor = SystemColors.Control;

            txtPass.Text = "";
            if (nTag == 0)
            {
                //GlobalVariable.Login.nLoginLevel = MCDF.LEVEL_OP;
                btnOp.BackColor = Color.Gold;
                txtPass.Focus();
            }
            else if (nTag == 1)
            {
                //GlobalVariable.Login.nLoginLevel = MCDF.LEVEL_ENG;
                btnEng.BackColor = Color.Gold;
                txtPass.Focus();
            }
            else
            {
                //GlobalVariable.Login.nLoginLevel = MCDF.LEVEL_MAK;
                btnMaker.BackColor = Color.Gold;
                txtPass.Focus();
            }
        }
    }
}
