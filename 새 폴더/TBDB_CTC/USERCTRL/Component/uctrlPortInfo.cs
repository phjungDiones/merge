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
    public partial class uctrlPortInfo : UserControl
    {
        bool bPortLockStatus = false;

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string PortName
        {
            get { return lbPortName.Text; }
            set
            {
                if (lbPortName.Text == value) return;

                lbPortName.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string PortStatus
        {
            get { return lbPortStatus.Text; }
            set
            {
                if (lbPortStatus.Text == value) return;

                lbPortStatus.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string PortPath
        {
            get { return lbPortPath.Text; }
            set
            {
                if (lbPortPath.Text == value) return;

                lbPortPath.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string PortPPID
        {
            get { return lbPortPPID.Text; }
            set
            {
                if (lbPortPPID.Text == value) return;

                lbPortPPID.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string PortLotID
        {
            get { return lbPortLotID.Text; }
            set
            {
                if (lbPortLotID.Text == value) return;

                lbPortLotID.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string PortMID
        {
            get { return lbPortMID.Text; }
            set
            {
                if (lbPortMID.Text == value) return;

                lbPortMID.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool PortLockStatus
        {
            get { return bPortLockStatus; }
            set
            {
                if (bPortLockStatus == value) return;

                if (!value)
                {
                    lbPortLockStatus.BackColor = Color.FromArgb(30, 30, 30);
                    lbPortLockStatus.ForeColor = Color.White;
                }
                else
                {
                    lbPortLockStatus.BackColor = Color.Yellow;
                    lbPortLockStatus.ForeColor = Color.Black;
                }

                bPortLockStatus = value;
                this.Invalidate();
            }
        }

        public uctrlPortInfo()
        {
            InitializeComponent();
        }
    }
}
