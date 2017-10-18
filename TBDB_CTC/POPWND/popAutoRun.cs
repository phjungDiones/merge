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

namespace TBDB_CTC.POPWND
{
    public partial class popAutoRun : Form
    {
        public popAutoRun()
        {
            InitializeComponent();
        }

        private void popAutoRun_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GlobalVariable.mcState.isRdy = true;
            GlobalVariable.mcState.isRun = true;             
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            GlobalVariable.mcState.isRun = false;   
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            GlobalVariable.mcState.isRun = false;   
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            GlobalVariable.mcState.isRun = false;   
        }
    }
}
    