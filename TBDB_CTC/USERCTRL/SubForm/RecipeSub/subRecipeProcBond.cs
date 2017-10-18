using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_Handler;
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.UserCtrl.SubForm.RecipeSub
{
    public partial class subRecipeProcBond : UserControl
    {
        public subRecipeProcBond()
        {
            InitializeComponent();
        }

        private void subRecipeProcBond_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ProcessMgr.Inst.CopyTempRcp();
                RefreshUI();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ProcInfoBond data = new ProcInfoBond();
            bool bValue = false;
            double dValue = 0.0;

            data.strProcName = txtName.Text;
            data.bVisionUse = swVisionUse.Checked;
            double.TryParse(txtTotalTime.Text, out dValue);
            data.dTotalTimeSec = dValue;
            double.TryParse(txtTotalTime.Text, out dValue);
            data.dTotalTimeSec = dValue;
            double.TryParse(txtPressure.Text, out dValue);
            data.dPressure = dValue;
            double.TryParse(txtPressTime.Text, out dValue);
            data.dPressTimeSec = dValue;
            double.TryParse(txtUpperTemp.Text, out dValue);
            data.dUpperTemp = dValue;
            double.TryParse(txtLowerTemp.Text, out dValue);
            data.dLowerTemp = dValue;
            double.TryParse(txtApcPosition.Text, out dValue);
            data.dAPCPos = dValue;
            double.TryParse(txtBackLight1.Text, out dValue);
            data.dBacklightCH1 = dValue;
            double.TryParse(txtBackLight2.Text, out dValue);
            data.dBacklightCH2 = dValue;
            double.TryParse(txtBackLight3.Text, out dValue);
            data.dBacklightCH3 = dValue;


            if (data.strProcName.Trim() == "")
            {
                MessageBox.Show(new Form() { TopMost = true }, "Please insert name", "Confirm!");
                return;
            }

            int nCount = ProcessMgr.Inst.TempPinfo.listProcBond.Where<ProcInfoBond>(p => p.strProcName == data.strProcName).Count();
            if (nCount == 0)
            {
                // 추가
                ProcessMgr.Inst.TempPinfo.listProcBond.Add(data);
            }
            else
            {
                // 수정
                ProcInfoBond edit = ProcessMgr.Inst.TempPinfo.listProcBond.Single<ProcInfoBond>(p => p.strProcName == data.strProcName);
                edit.dTotalTimeSec = data.dTotalTimeSec;
                edit.bVisionUse = data.bVisionUse;
                edit.dPressure = data.dPressure;
                edit.dPressTimeSec = data.dPressTimeSec;
                edit.dUpperTemp = data.dUpperTemp;
                edit.dLowerTemp = data.dLowerTemp;
                edit.dAPCPos = data.dAPCPos;
                edit.dBacklightCH1 = data.dBacklightCH1;
                edit.dBacklightCH2 = data.dBacklightCH2;
                edit.dBacklightCH3 = data.dBacklightCH3;
            }

            ProcessMgr.Inst.SaveTempRcp();
            ProcessMgr.Inst.Save();
            ProcessMgr.Inst.CopyTempRcp();

            RefreshUI();
        }

        void RefreshUI()
        {
            listboxData.DisplayMember = "strProcName";
            listboxData.ValueMember = "strProcName";
            listboxData.DataSource = ProcessMgr.Inst.TempPinfo.listProcBond;
        }

        private void listboxData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nIndex = listboxData.SelectedIndex;
            ProcInfoBond data = ProcessMgr.Inst.TempPinfo.listProcBond[nIndex];

            txtName.Text = data.strProcName;
            txtTotalTime.Text = data.dTotalTimeSec.ToString();
            swVisionUse.Checked = data.bVisionUse;
            txtPressure.Text = data.dPressure.ToString("0.000");
            txtPressTime.Text = data.dPressTimeSec.ToString("0.000");
            txtUpperTemp.Text = data.dUpperTemp.ToString("0.000");
            txtLowerTemp.Text = data.dLowerTemp.ToString("0.000");
            txtApcPosition.Text = data.dAPCPos.ToString();
            txtBackLight1.Text = data.dBacklightCH1.ToString();
            txtBackLight2.Text = data.dBacklightCH2.ToString();
            txtBackLight3.Text = data.dBacklightCH3.ToString();
        }
        private bool ShowDialog_popkey()
        {
            bool flag = false;
            TBDB_Handler.GLOBAL.GlobalForm._popKeyPad.ShowDialog();
            if (TBDB_Handler.GLOBAL.GlobalForm._popKeyPad.DialogResult == DialogResult.OK)
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
    }
}
