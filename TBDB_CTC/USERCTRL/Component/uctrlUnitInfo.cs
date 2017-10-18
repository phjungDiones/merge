using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBDB_CTC.UserCtrl
{
    public partial class uctrlUnitInfo : UserControl
    {
        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string UnitName
        {
            get { return lbUnitName.Text; }
            set
            {
                if (lbUnitName.Text == value) return;

                lbUnitName.Text = value;
                this.Invalidate();
            }
        }

        public uctrlUnitInfo()
        {
            InitializeComponent();
        }
    }
}
