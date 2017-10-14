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
    public partial class subRecipeProcAlign : UserControl
    {
        public subRecipeProcAlign()
        {
            InitializeComponent();
        }

        private void subRecipeProcAlign_Load(object sender, EventArgs e)
        {

        }

        private void subRecipeProcAlign_VisibleChanged(object sender, EventArgs e)
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
            listboxData.DataSource = ProcessMgr.Inst.TempPinfo.listProcAlign;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ProcInfoAlign data = new ProcInfoAlign();
            float dAngle = 0.0f;

            data.strProcName = txtName.Text;
            float.TryParse(txtAngle.Text, out dAngle);
            data.dAngle = dAngle;

            if (data.strProcName.Trim() == "")
            {
                MessageBox.Show(new Form() { TopMost = true }, "Please insert name", "Confirm!");
                return;
            }

            int nCount = ProcessMgr.Inst.TempPinfo.listProcAlign.Where<ProcInfoAlign>(p => p.strProcName == data.strProcName).Count();
            if (nCount == 0)
            {
                // 추가
                ProcessMgr.Inst.TempPinfo.listProcAlign.Add(data);
            }
            else
            {
                // 수정
                ProcInfoAlign edit = ProcessMgr.Inst.TempPinfo.listProcAlign.Single<ProcInfoAlign>(p => p.strProcName == data.strProcName);
                edit.dAngle = dAngle;
            }

            ProcessMgr.Inst.SaveTempRcp();
            ProcessMgr.Inst.Save();
            ProcessMgr.Inst.CopyTempRcp();

            RefreshUI();
        }

        private void listboxData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nIndex = listboxData.SelectedIndex;

            ProcInfoAlign data = ProcessMgr.Inst.TempPinfo.listProcAlign[nIndex];

            txtName.Text = data.strProcName;
            txtAngle.Text = data.dAngle.ToString("0.000");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int nIndex = listboxData.SelectedIndex;
            ProcessMgr.Inst.TempPinfo.listProcAlign.RemoveAt(nIndex);

            RefreshUI();
        }
    }
}
