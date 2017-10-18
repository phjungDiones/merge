using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.GUI
{
    public partial class frmLogIn : Form
    {
        string strUserLevel = "OPERATOR";
        public delegate void UserLogin(int nScrNo);
        public event UserLogin userLogin = null;


        private void readxml()
        {
            GlobalDataSet.dataset4.ReadXml("login.xml", XmlReadMode.Auto);
            int count = GlobalDataSet.dataset4.Tables.Count;

            for (int i = 0; i < count; i++)
            {

                if (GlobalDataSet.dataset4.Tables[0].Rows[i]["mode"].ToString() == "OP")
                {
                    GlobalVariable.Login.strLoginPassword[MCDF.LEVEL_OP].Add(GlobalDataSet.dataset4.Tables[0].Rows[i]["ID"].ToString(), GlobalDataSet.dataset4.Tables[0].Rows[i]["pass"].ToString());
                }
                if (GlobalDataSet.dataset4.Tables[0].Rows[i]["mode"].ToString() == "ENG")
                {
                    GlobalVariable.Login.strLoginPassword[MCDF.LEVEL_ENG].Add(GlobalDataSet.dataset4.Tables[0].Rows[i]["ID"].ToString(), GlobalDataSet.dataset4.Tables[0].Rows[i]["pass"].ToString());
                }
                if (GlobalDataSet.dataset4.Tables[0].Rows[i]["mode"].ToString() == "MAK")
                {
                    GlobalVariable.Login.strLoginPassword[MCDF.LEVEL_MAK].Add(GlobalDataSet.dataset4.Tables[0].Rows[i]["ID"].ToString(), GlobalDataSet.dataset4.Tables[0].Rows[i]["pass"].ToString());
                }
            }
        }
        private void makexml()
        {
            GlobalDataSet.dataset4.Tables.Add("login");
            GlobalDataSet.dataset4.Tables[0].Columns.Add("mode");
            GlobalDataSet.dataset4.Tables[0].Columns.Add("ID");
            GlobalDataSet.dataset4.Tables[0].Columns.Add("pass");



            DataRow dr = GlobalDataSet.dataset4.Tables[0].NewRow();
            dr["mode"] = "OP";
            dr["ID"] = "1";
            dr["pass"] = "1";
            GlobalDataSet.dataset4.Tables[0].Rows.Add(dr);
            GlobalDataSet.dataset4.WriteXml("login.xml");
        }
        private void setLoginInfo()
        {
            //기본로그인정보
            GlobalVariable.Login.strLoginPassword = new Dictionary<string, string>[3];
            GlobalVariable.Login.strLoginPassword[MCDF.LEVEL_OP] = new Dictionary<string, string>();
            GlobalVariable.Login.strLoginPassword[MCDF.LEVEL_ENG] = new Dictionary<string, string>();
            GlobalVariable.Login.strLoginPassword[MCDF.LEVEL_MAK] = new Dictionary<string, string>();
            GlobalVariable.Login.blogin = false;

            if (System.IO.File.Exists("login.xml"))
            {
                readxml();

            }
            else
            {
                makexml();
                readxml();
            }

           
        }

        


        public frmLogIn()
        {
            InitializeComponent();


            setLoginInfo();
            //GlobalVariable.Login.nLoginLevel = MCDF.LEVEL_OP;
            btnOp.BackColor = Color.Gold;
            txtPass.Focus();
            
          
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //tbxUserID
            //txtPass
            //GlobalVariable.Login.nLoginLevel
            try
            {
                if (GlobalVariable.Login.strLoginPassword[GlobalVariable.Login.nLoginLevel][tbxUserID.Text] == txtPass.Text)
                {
                    //아이디 패스워드 매치
                    GlobalVariable.Login.ID = tbxUserID.Text;
                    GlobalVariable.Login.pass = txtPass.Text;
                    if (GlobalVariable.Login.nLoginLevel == 0)
                    {
                        GlobalVariable.Login.mode = "OP";
                    }
                    if (GlobalVariable.Login.nLoginLevel == 1)
                    {
                        GlobalVariable.Login.mode = "ENGINNER";
                    }
                    if (GlobalVariable.Login.nLoginLevel == 2)
                    {
                        GlobalVariable.Login.mode = "MAKER";
                    }
                    userLogin(GlobalVariable.Login.nLoginLevel);
                    tbxUserID.Text = "";
                    txtPass.Text = "";
                }
                else
                {
                    MessageBox.Show("패스워드 틀림");
                    txtPass.Focus();
                    //패스워드 틀림
                }
            }
            catch (Exception ex)
            {//아이디가없음
                MessageBox.Show("아이디없음");
                tbxUserID.Focus();
            }

            //if (userLogin != null) userLogin(1);
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
                GlobalVariable.Login.nLoginLevel = MCDF.LEVEL_OP;
                btnOp.BackColor = Color.Gold;
                txtPass.Focus();
            }
            else if (nTag == 1)
            {
                GlobalVariable.Login.nLoginLevel = MCDF.LEVEL_ENG;
                btnEng.BackColor = Color.Gold;
                txtPass.Focus();
            }
            else
            {
                GlobalVariable.Login.nLoginLevel = MCDF.LEVEL_MAK;
                btnMaker.BackColor = Color.Gold;
                txtPass.Focus();
            }
        }
    }
}
