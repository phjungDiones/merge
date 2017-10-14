using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.UserCtrl.SubForm.ConfigSub
{
    public partial class subConfigParam : UserControl
    {
        public subConfigParam()
        {
            InitializeComponent();
        }

        private void subConfigParam_Load(object sender, EventArgs e)
        {
            DisplayData();
        }


        public void DisplayData()
        {
            tbRobotMove.Text = GlobalVariable.cfg.nRobotMoveTimeOut.ToString();
            tbRobotStatus.Text = GlobalVariable.cfg.nRobotStatusTimeOut.ToString();
        }

        private void btnParamSave_Click(object sender, EventArgs e)
        {
            GlobalVariable.cfg.nRobotMoveTimeOut = Convert.ToInt32(tbRobotMove.Text);
            GlobalVariable.cfg.nRobotStatusTimeOut = Convert.ToInt32(tbRobotStatus.Text);

            //Save Config Data
            CfgManager.Instance.SaveConfigFile();
        }
    }
}
