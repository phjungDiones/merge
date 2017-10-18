using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_CTC.Data;
using TBDB_Handler.GLOBAL;
using CJ_Controls.Communication.PA300C;

namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    public partial class subManualAligner : UserControl
    {
        POPWND.popConfirm popConfirm = new POPWND.popConfirm();
        POPWND.popCancel popCancel = new POPWND.popCancel();
        POPWND.popKeyPad _popKeyPad = TBDB_Handler.GLOBAL.GlobalForm._popKeyPad;
        MainData _Main = null;
        int nTmrStatus = 0;

        public subManualAligner()
        {
            InitializeComponent();
        }

        private bool ShowDialog()
        {
            bool flag = false;
            popConfirm.ShowDialog();
            if (popConfirm.DialogResult == DialogResult.OK)
            {
                flag = true;
            }
            else
            {

            }
            return flag;
        }

       
        private void subManualAligner_Load(object sender, EventArgs e)
        {
            _Main = MainData.Instance;
            tbAlngeOffset.Text = "0";
            tbStartAlign.Text = "0";
            tbRotateVal.Text = "0";
            tmrStatus.Start();
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Aligner.Cmd_Send_HOME();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Aligner.Cmd_Send_REST();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Aligner.Cmd_Send_ECLR();
        }

        private void btnRotateWafer_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
        }

        private void btnStartAl_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            double dVal = Convert.ToDouble(tbStartAlign.Text);
            _Main.GetLoaderData().Aligner.Cmd_Send_STAL((float)dVal);
        }

        private void btnSetAlignOffset_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            double dVal = Convert.ToDouble(tbAlngeOffset.Text);
            _Main.GetLoaderData().Aligner.Cmd_Send_ANOF((float)dVal);
        }

        private void btnVacOn_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Aligner.Cmd_Send_VAON();
            
        }

        private void btnVacOff_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Aligner.Cmd_Send_VAOF();
        }

        private void btnChuckUp_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Aligner.Cmd_Send_CKUP();
        }

        private void btnChuckDown_Click(object sender, EventArgs e)
        {
            if (!ShowDialog()) return;
            _Main.GetLoaderData().Aligner.Cmd_Send_CKDN();
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            //return;

            if (this.Visible == false) return;

            switch (nTmrStatus)
            {
                case 0:
                    if (_Main.GetLoaderData().Aligner.BeforeAck == ACK_MODE.COMPLETE)
                        _Main.GetLoaderData().Aligner.Cmd_Read_STAT();
                    lbStatus.Text = _Main.GetLoaderData().Aligner.Pa300C_Data.AlignerStatus.ToString();
                    nTmrStatus++;
                    break;

                case 1:
                    if (_Main.GetLoaderData().Aligner.BeforeAck == ACK_MODE.COMPLETE)
                        _Main.GetLoaderData().Aligner.Cmd_Read_VACH();
                    lbVacStatus.Text = _Main.GetLoaderData().Aligner.Pa300C_Data.Vaccum_Sens.ToString();
                    nTmrStatus++;
                    break;

                case 2:
                    if (_Main.GetLoaderData().Aligner.BeforeAck == ACK_MODE.COMPLETE)
                        _Main.GetLoaderData().Aligner.Cmd_Read_CPOS();
                    lbChuckPos.Text = _Main.GetLoaderData().Aligner.Pa300C_Data.ChuckPosition.ToString();
                    nTmrStatus++;
                    break;

                case 3:
                    if (_Main.GetLoaderData().Aligner.BeforeAck == ACK_MODE.COMPLETE)
                    _Main.GetLoaderData().Aligner.Cmd_Read_STAL();
                    lbCurAngle.Text = _Main.GetLoaderData().Aligner.Pa300C_Data.CurAngleParam.ToString();
                    nTmrStatus++;
                    break;

                default:
                    nTmrStatus = 0;
                    //nTmrStatus = 1;
                    break;
            }

          
        }
        private bool ShowDialog_popkey()
        {
            bool flag = false;
            _popKeyPad.ShowDialog();
            if (_popKeyPad.DialogResult == DialogResult.OK)
            {
                flag = true;
            }
            else
            {

            }
            return flag;
        }
        private void tbAlngeOffset_Click(object sender, EventArgs e)
        {
            TextBox tb = ((TextBox)sender);
            if (ShowDialog_popkey())
            {
                tb.Text = ReturnNumber.retNum.ToString();
            }
        }

        private void tbAlngeOffset_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
