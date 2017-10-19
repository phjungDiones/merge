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
using System.IO;

namespace TBDB_CTC.UserCtrl.SubForm.RecipeSub
{
    public partial class subRecipe : UserControl
    {
        // ComboBox 리스트를 갱신하는 동안은 TempRcp에 값을 넣으면 안됨
        bool isUpdate = false;

        public subRecipe()
        {
            InitializeComponent();
        }

        private void subRecipe_Load(object sender, EventArgs e)
        {
            //lbRcpName.Text = RecipeMgr.Inst.LAST_MODEL_NAME;
        }

        private void subRecipe_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                RecipeMgr.Inst.CopyTempRcp();
                RefreshUI();
            }
        }

        void RefreshUI()
        {
            isUpdate = true;

            #region ComboBox List
            cbbMaxSlot.Items.Clear();
            for (int nCnt = 0; nCnt < MCDF.MAX_SLOT_COUNT; nCnt++)
            {
                cbbMaxSlot.Items.Add(string.Format("{0}", nCnt + 1));
            }

            cbbRunMode.Items.Clear();
            cbbRunMode.Items.Add("FULL");
            cbbRunMode.Items.Add("ONLY LAMI");
            cbbRunMode.Items.Add("ONLY BOND");

            ComboBox cbb = cbbPreAlignCarrier;
            cbb.DataSource = null;
            cbb.Items.Clear();
            if (ProcessMgr.Inst.Pinfo.listProcAlign.Count > 0)
            {
                cbb.DataSource = new List<ProcInfoAlign>(ProcessMgr.Inst.Pinfo.listProcAlign);
                cbb.ValueMember = "strProcName";
                cbb.DisplayMember = "strProcName";
            }

            cbb = cbbPreAlignDevice;
            cbb.DataSource = null;
            cbb.Items.Clear();
            if (ProcessMgr.Inst.Pinfo.listProcAlign.Count > 0)
            {
                cbb.DataSource = new List<ProcInfoAlign>(ProcessMgr.Inst.Pinfo.listProcAlign);
                cbb.ValueMember = "strProcName";
                cbb.DisplayMember = "strProcName";
            }

            cbb = cbbPostAlignCarrier;
            cbb.DataSource = null;
            cbb.Items.Clear();
            if (ProcessMgr.Inst.Pinfo.listProcAlign.Count > 0)
            {
                cbb.DataSource = new List<ProcInfoAlign>(ProcessMgr.Inst.Pinfo.listProcAlign);
                cbb.ValueMember = "strProcName";
                cbb.DisplayMember = "strProcName";
            }

            cbb = cbbLami;
            cbb.DataSource = null;
            cbb.Items.Clear();
            if (ProcessMgr.Inst.Pinfo.listProcLami.Count > 0)
            {
                cbb.DataSource = new List<ProcInfoLami>(ProcessMgr.Inst.Pinfo.listProcLami);
                cbb.ValueMember = "strProcName";
                cbb.DisplayMember = "strProcName";
            }

            cbb = cbbBonder;
            cbb.DataSource = null;
            cbb.Items.Clear();
            if (ProcessMgr.Inst.Pinfo.listProcBond.Count > 0)
            {
                cbb.DataSource = new List<ProcInfoBond>(ProcessMgr.Inst.Pinfo.listProcBond);
                cbb.ValueMember = "strProcName";
                cbb.DisplayMember = "strProcName";
            } 
            #endregion

            cbbMaxSlot.SelectedIndex = RecipeMgr.Inst.TempRcp.nMaxUseSlot;
            cbbRunMode.SelectedIndex = (int)RecipeMgr.Inst.TempRcp.eRunMode;

            cbbPreAlignCarrier.Text = RecipeMgr.Inst.TempRcp.strPreAlignCarrier;
            cbbPreAlignDevice.Text = RecipeMgr.Inst.TempRcp.strPreAlignDevice;
            cbbPostAlignCarrier.Text = RecipeMgr.Inst.TempRcp.strPostAlignCarrier;
            cbbLami.Text = RecipeMgr.Inst.TempRcp.strLamiCondition;
            cbbBonder.Text = RecipeMgr.Inst.TempRcp.strBondCondition;
            txtHpTime.Text = RecipeMgr.Inst.TempRcp.dHpTime.ToString("0.000");

            chkUsePreAlignCarrier.Checked = RecipeMgr.Inst.TempRcp.bUsePreAlignCarrier;
            chkUsePreAlignDevice.Checked = RecipeMgr.Inst.TempRcp.bUsePreAlignDevice;
            chkUsePostAlignCarrier.Checked = RecipeMgr.Inst.TempRcp.bUsePostAlignCarrier;
            chkUseHP.Checked = RecipeMgr.Inst.TempRcp.bUseHP;

            isUpdate = false;

            ShowModelFileList(); //List Add

            lbRcpName.Text = RecipeMgr.Inst.LAST_MODEL_NAME;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //ProcInfoLami a = new ProcInfoLami();
            //a.strProcName = "ASDF";
            //ProcessMgr.Inst.TempPinfo.listProcLami.Add(a);

            ProcessMgr.Inst.SaveTempRcp();
            ProcessMgr.Inst.Save();
            ProcessMgr.Inst.CopyTempRcp();

            RecipeMgr.Inst.SaveTempRcp();
            RecipeMgr.Inst.Save();
            RecipeMgr.Inst.CopyTempRcp();
        }

        private void cbbMaxSlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdate) return;

            int nTag = 0;
            ComboBox cbb = sender as ComboBox;
            int.TryParse(cbb.Tag.ToString(), out nTag);

            switch (nTag)
            {
                    // Max Use Slot
                case 0:
                    RecipeMgr.Inst.TempRcp.nMaxUseSlot = cbb.SelectedIndex;
                    break;
                    // Run Mode
                case 1:
                    RecipeMgr.Inst.TempRcp.eRunMode = (RUN_MODE)cbb.SelectedIndex;
                    break;
                    // Pre-Align Carrier
                case 2:
                    RecipeMgr.Inst.TempRcp.strPreAlignCarrier = cbb.Text;
                    break;
                    // Pre-Align Device
                case 3:
                    RecipeMgr.Inst.TempRcp.strPreAlignDevice = cbb.Text;
                    break;
                    // Post-Align Carrier
                case 4:
                    RecipeMgr.Inst.TempRcp.strPostAlignCarrier = cbb.Text;
                    break;
                    // Lami Condition
                case 5:
                    RecipeMgr.Inst.TempRcp.strLamiCondition = cbb.Text;
                    break;
                    // Bonder Condition
                case 6:
                    RecipeMgr.Inst.TempRcp.strBondCondition = cbb.Text;
                    break;
                default:
                    break;
            }
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
            txtHpTime_TextChanged();
        }

        private void txtHpTime_TextChanged()
        {
            if (isUpdate) return;

            double dTemp;
            double.TryParse(txtHpTime.Text, out dTemp);

            RecipeMgr.Inst.TempRcp.dHpTime = dTemp;
        }

        private void listRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnModelNew_Click(object sender, EventArgs e)
        {
            string target = tbxRcpTarget.Text;
            if (target.Length <= 0)
            {
                MessageBox.Show("모델 이름을 입력하세요.");
                return;
            }

            if (!target.Contains(".rcp"))
                target = target + ".rcp";

            string pathRcp = PathManager.Instance[PathManager.ePathInfo.eWorkFile];

            string[] files = Directory.GetFiles(pathRcp);

            foreach (string file in files)
            {
                if (!file.Contains(".rcp")) continue; //확장자 rcp만 찾는다

                //string fn = Path.GetFileNameWithoutExtension(file);
                string fn = Path.GetFileName(file);
               
                //대소문자 비교
                if(string.Equals(target, fn, StringComparison.CurrentCultureIgnoreCase) == true)
                {
                    MessageBox.Show("Already exist target model");
                    return;
                }
            }

            try
            {
                RecipeMgr.Inst.CopyToModel(target);
                MessageBox.Show("Success to model created");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to model create : " + ex.Message);
            }

            RefreshUI();
        }

        private void btnModelChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridModel.CurrentCell == null) return;
                if (gridModel.Rows[gridModel.CurrentCell.RowIndex].Cells[0].Value == null) return;
                string strChangeId = gridModel.Rows[gridModel.CurrentCell.RowIndex].Cells[0].Value.ToString();

                DialogResult result = MessageBox.Show("Do you want to change recipe? [" + strChangeId + "]",
                                                        "Confirm!",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button2);
                if (result != System.Windows.Forms.DialogResult.Yes)
                {
                    tbxRcpTarget.Text = "";
                    return;
                }

                tbxRcpSoruce.Text = strChangeId;
                RecipeMgr.Inst.ChangeModelFile(tbxRcpSoruce.Text + ".rcp");
                RefreshUI();

//                 if (deleChangeModel != null) deleChangeModel();
//                 MessageBox.Show("Model changed.");
            }
            catch (System.Exception ex)
            {
                //
            }
        }

        private void btnModelDel_Click(object sender, EventArgs e)
        {

        }

        private void ShowModelFileList()
        {
            string pathExeFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\WorkFile";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(pathExeFile);
            gridModel.RowCount = 0;

            foreach (FileInfo f in dir.GetFiles("*.rcp"))
            {
                string[] arr = new string[3];
                arr[0] = f.Name.Replace(".rcp", "");
                arr[1] = Convert.ToString(f.CreationTime);
                arr[2] = Convert.ToString(f.LastWriteTime);

                int nRecipeId = 0;
                //if (int.TryParse(arr[0], out nRecipeId))
                {
                    //RecipeMgr.Inst.nRegRecipeCnt++;
                    gridModel.Rows.Add(arr);

                }
            }
        }

        private void gridModel_SelectionChanged(object sender, EventArgs e)
        {
            if (gridModel.SelectedRows.Count <= 0)
                return;

            tbxRcpSoruce.Text = (string)gridModel.SelectedRows[0].Cells[0].Value;
        }
    }
}
