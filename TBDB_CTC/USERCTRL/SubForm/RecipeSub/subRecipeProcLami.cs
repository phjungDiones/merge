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
    public partial class subRecipeProcLami : UserControl
    {
        public subRecipeProcLami()
        {
            InitializeComponent();
        }

        private void subRecipeProcLami_Load(object sender, EventArgs e)
        {

        }

        private void subRecipeProcLami_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ProcessMgr.Inst.CopyTempRcp();
                RefreshUI();
            }
        }

        void RefreshUI()
        {
            listboxData.DisplayMember = "strProcName";
            listboxData.ValueMember = "strProcName";
            listboxData.DataSource = ProcessMgr.Inst.TempPinfo.listProcLami;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ProcInfoLami data = new ProcInfoLami();
            double dValue = 0.0;

            data.strProcName = txtName.Text;
            double.TryParse(txtProcessTime.Text, out dValue);
            data.nProcTimeSec = (int)dValue;
            double.TryParse(txtPressure.Text, out dValue);
            data.dPressure = dValue;
            double.TryParse(txtUpperTemp.Text, out dValue);
            data.dUpperTemp = dValue;
            double.TryParse(txtLowerTemp.Text, out dValue);
            data.dLowerTemp = dValue;
            double.TryParse(txtPressTime.Text, out dValue);
            data.nPressingTimeSec = (UInt64)dValue;
            
            if (data.strProcName.Trim() == "")
            {
                MessageBox.Show(new Form() { TopMost = true }, "Please insert name", "Confirm!");
                return;
            }

            int nCount = ProcessMgr.Inst.TempPinfo.listProcLami.Where<ProcInfoLami>(p => p.strProcName == data.strProcName).Count();

            if (nCount == 0)
            {
                // 추가
                ProcessMgr.Inst.TempPinfo.listProcLami.Add(data);
            }
            else
            {
                // 수정
                ProcInfoLami edit = ProcessMgr.Inst.TempPinfo.listProcLami.Single<ProcInfoLami>(p => p.strProcName == data.strProcName);
                edit.nProcTimeSec = data.nProcTimeSec;
                edit.dPressure = data.dPressure;
                edit.dUpperTemp = data.dUpperTemp;
                edit.dLowerTemp = data.dLowerTemp;
                edit.nPressingTimeSec = data.nPressingTimeSec;
            }

            ProcessMgr.Inst.SaveTempRcp();
            ProcessMgr.Inst.Save();
            ProcessMgr.Inst.CopyTempRcp();

            RefreshUI();
        }

        private void listboxData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nIndex = listboxData.SelectedIndex;
            ProcInfoLami data = ProcessMgr.Inst.TempPinfo.listProcLami[nIndex];

            txtName.Text = data.strProcName;
            txtProcessTime.Text = data.nProcTimeSec.ToString();
            txtPressure.Text = data.dPressure.ToString("0.000");
            txtUpperTemp.Text = data.dUpperTemp.ToString();
            txtLowerTemp.Text = data.dLowerTemp.ToString();
            txtPressTime.Text = data.nPressingTimeSec.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int nIndex = listboxData.SelectedIndex;
            ProcessMgr.Inst.TempPinfo.listProcLami.RemoveAt(nIndex);

            RefreshUI();
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
