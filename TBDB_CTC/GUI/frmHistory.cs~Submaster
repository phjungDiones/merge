using Owf.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_CTC.UserCtrl.SubForm.HistorySub;
using TBDB_CTC.UserCtrl.SubForm.LogSub;
using System.IO;
using TBDB_Handler.GLOBAL;
using System.Xml;

namespace TBDB_CTC.GUI
{
    public partial class frmHistory : Form
    {
        public UserControl[] wndHistorySubMenu = new UserControl[7];

        public frmHistory()
        {

            InitializeComponent();

            wndHistorySubMenu[0] = new subLogEvent();
            wndHistorySubMenu[1] = new subLogLogging();
            wndHistorySubMenu[2] = new subHistoryLotRun();
            wndHistorySubMenu[3] = new subHistoryWaferRun();
            wndHistorySubMenu[4] = new subHistoryLotProc();
            wndHistorySubMenu[5] = new subHistoryWaferProc();
            wndHistorySubMenu[6] = new subLogDebug();

            panHistorySubClient.Controls.Add(wndHistorySubMenu[0]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[1]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[2]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[3]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[4]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[5]);
            panHistorySubClient.Controls.Add(wndHistorySubMenu[6]);
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            SubScreenChange(0);
//            wndHistorySubMenu[0].Show();
        }

        private void OnHistorySubMenuButtonClick(object sender, EventArgs e)
        {
            Label lbSubMn = sender as Label;

            SubScreenChange(Convert.ToInt32(lbSubMn.Tag));
        }

        public void ResetSubMenuButton()
        {
            for (int nCtrlCount = 0; nCtrlCount < 10; nCtrlCount++)
            {
                A1Panel pBtn = Controls.Find("panSubMenuC" + nCtrlCount.ToString(), true).FirstOrDefault() as A1Panel;
                if (pBtn != null)
                {
                    pBtn.GradientEndColor = SystemColors.ButtonFace;
                    pBtn.GradientStartColor = Color.DimGray;
                }

                Label Lb = Controls.Find("lbSubMenuC" + nCtrlCount.ToString(), true).FirstOrDefault() as Label;
                if (Lb != null)
                {
                    Lb.ForeColor = Color.Black;
                }
            }
        }
        private static int mnPageNo = 1;
        public void SubScreenChange(int nPageNo)
        {
            func2();
            func();
            
            mnPageNo = nPageNo;
            ResetSubMenuButton();

            A1Panel pBtn = Controls.Find("panSubMenuC" + nPageNo.ToString(), true).FirstOrDefault() as A1Panel;
            if (pBtn != null)
            {
                pBtn.GradientEndColor = Color.Black;
                pBtn.GradientStartColor = Color.DimGray;
            }

            Label Lb = Controls.Find("lbSubMenuC" + nPageNo.ToString(), true).FirstOrDefault() as Label;
            if (Lb != null)
            {
                Lb.ForeColor = Color.Gold;
            }

            for (int nCfgSubMenuCount = 0; nCfgSubMenuCount < 7; nCfgSubMenuCount++)
            {
                wndHistorySubMenu[nCfgSubMenuCount].Hide();
            }

            wndHistorySubMenu[nPageNo].Show();
        }
        DataGridView _datagridView = null;
        private void button2_Click(object sender, EventArgs e)
        {
            SetLog();

        }

        private void func2()
        {
            switch (mnPageNo)
            {
                case 0:
                    _datagridView = ((subLogEvent)wndHistorySubMenu[mnPageNo]).gridLog_Event;
                    break;
                case 1:
                    _datagridView = ((subLogLogging)wndHistorySubMenu[mnPageNo]).gridLog_Login;
                    break;
                case 2:
                    _datagridView = ((subHistoryLotRun)wndHistorySubMenu[mnPageNo]).gridLog_subHistoryLotRun;
                    break;
                case 3:
                    _datagridView = ((subHistoryWaferRun)wndHistorySubMenu[mnPageNo]).gridLog_subHistoryWaferRun;
                    break;
                case 4:
                    _datagridView = ((subHistoryLotProc)wndHistorySubMenu[mnPageNo]).gridLog_subHistoryLotProc;
                    break;
                case 5:
                    _datagridView = ((subHistoryWaferProc)wndHistorySubMenu[mnPageNo]).gridLog_subHistoryWaferProc;
                    break;
                case 6:
                    _datagridView = ((subLogDebug)wndHistorySubMenu[mnPageNo]).gridLog_Debug;
                    break;
                default:

                    return;

            }
        }

        private void SetLog()
        {
            _datagridView.Rows.Add(_datagridView.Rows.Count,"1", "1", "1", "1");
            DataGridViewRow row = new DataGridViewRow();
            saveLogFIleToXml(_datagridView);
        }

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {

            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    // You could potentially name the column based on the DGV column name (beware of dupes)
                    // or assign a type based on the data type of the data bound to this DGV column.
                    dt.Columns.Add();
                }
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;
        }
        
        private void saveLogFIleToXml(DataGridView DataGridView1)
        {
            string filepath = dtp_Log.Value.ToShortDateString() + "_" + ((EhistoryLogIndex)mnPageNo).ToString()+".xml";
            DataTable dT = GetDataTableFromDGV(DataGridView1);
            DataSet dS = new DataSet();
            dS.Tables.Add(dT);
            
            FileStream fs = File.OpenWrite(filepath);
            dS.WriteXml(filepath);
            fs.Close();

        }

        private void btn_LoadLog_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dtp_Log.Value.ToShortDateString());
        }

        private void func()
        {
            
            string filepath = dtp_Log.Value.ToShortDateString() + "_" + ((EhistoryLogIndex)mnPageNo).ToString() + ".xml";

            XmlReader xmlFile=null;
            System.IO.FileInfo fi = new System.IO.FileInfo(filepath);
            if (fi.Exists) xmlFile = XmlReader.Create(filepath, new XmlReaderSettings());
            try
            {

                DataSet ds = new DataSet();
                ds.ReadXml(xmlFile);
                _datagridView.DataSource = ds.Tables[0];
                xmlFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }




}


