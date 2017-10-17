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

namespace TBDB_CTC.UserCtrl
{
    public partial class uctrlPortSlot : UserControl
    {
        public uctrlPortSlot()
        {
            InitializeComponent();
        }

        private void uctrlPortSlot_Load(object sender, EventArgs e)
        {
            //tmrStatus.Start();
            //bStatus = new bool[25];
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {

        }

        private int nSlot = 0;
        public int Slot
        {
            get { return nSlot; }
            set
            {
                nSlot = value;               
            }
        }


        private LPM_Wafer[] _WaferStatus = new LPM_Wafer[25];

        public LPM_Wafer LpmWaferStatus
        {
            get { return _WaferStatus[nSlot];  }
            set
            {
                if (value == null) return;

                _WaferStatus[nSlot] = value;

                Label fndLb;
                int nLabel = nSlot + 1; //Label Index

                fndLb = this.Controls.Find("lbWafer_" + nSlot.ToString(), true).FirstOrDefault() as Label;
                if (fndLb != null)
                {
                    //fndLb.BackColor = (_bStatus[nSlot] == true ? Color.LightGreen : Color.Gray);
                    //fndLb.Text = nLabel.ToString();
                    fndLb.TextAlign = ContentAlignment.MiddleCenter;
                    fndLb.Font = new System.Drawing.Font("맑은 고딕", 7.5F, FontStyle.Bold);

                    if (_WaferStatus[nSlot] == LPM_Wafer.Exist)
                    {
                        fndLb.Text = nLabel.ToString();
                        fndLb.ForeColor = Color.Black;
                        fndLb.BackColor = Color.LightGreen;
                    }
                    else if(_WaferStatus[nSlot] == LPM_Wafer.Unload)
                    {
                        fndLb.Text = nLabel.ToString();
                        fndLb.ForeColor = Color.Black;
                        fndLb.BackColor = Color.Orange;
                    }
                    else
                    {
                        fndLb.Text = "";
                        fndLb.BackColor = Color.Gray;
                        fndLb.ForeColor = Color.White;
                    }
                }
            }
        }


        private bool[] _bStatus = new bool[25];

        public bool bStatus
        {
            get { return _bStatus[nSlot]; }
            set
            {
                if (value == null) return;

                _bStatus[nSlot] = value;

                Label fndLb;
                int nLabel = nSlot + 1 ; //Label Index

                fndLb = this.Controls.Find("lbWafer_" + nSlot.ToString(), true).FirstOrDefault() as Label;
                if(fndLb != null)
                {
                    //fndLb.BackColor = (_bStatus[nSlot] == true ? Color.LightGreen : Color.Gray);
                    //fndLb.Text = nLabel.ToString();
                    fndLb.TextAlign = ContentAlignment.MiddleCenter;
                    fndLb.Font = new System.Drawing.Font("맑은 고딕", 7.5F, FontStyle.Bold);

                    if (_bStatus[nSlot])
                    {
                        fndLb.Text = nLabel.ToString();
                        fndLb.ForeColor = Color.Black;
                        fndLb.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        fndLb.Text = "";
                        fndLb.BackColor = Color.Gray;
                        fndLb.ForeColor = Color.White;
                    }                                                                    
                }
            }
        }

        public string strName
        {
            set
            {
                if (value == null) return;
                lbUnitName.Text = value;
            }
        }

    }
}
