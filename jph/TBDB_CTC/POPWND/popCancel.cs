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
    public partial class popCancel : Form
    {
        public popCancel()
        {
            InitializeComponent();
        }

        private void win_GlassButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void popCancel_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
        }
    }
}
