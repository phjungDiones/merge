using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBDB_CTC.POPWND
{
    public partial class popConfirm : Form
    {
        public popConfirm()
        {
            InitializeComponent();
        }

        private void win_GlassButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void win_GlassButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void popConfirm_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
        }
    }
}
