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
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.GUI
{
    public partial class frmHistory : Form
    {

        public delegate void MyEventHandler();
        public event MyEventHandler DataTableChanged;//DataTable 데이터 변동 이벤트;
        private int IndexForm = 0;
        public UserControl[] wndHistorySubMenu = new UserControl[7];

        private void func(DataGridView dgv, DataTable dt)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dt.Columns[dgv.Columns[i].HeaderText] == null)
                    dt.Columns.Add(dgv.Columns[i].HeaderText);
            }

        }

        private void test()
        {
            //MessageBox.Show("11");
        }
        DataGridView DGVsubLogEvent;
        DataGridView DGVsubLogLogging;
        DataGridView DGVsubHistoryLotRun;
        DataGridView DGVsubHistoryWaferRun;
        DataGridView DGVsubHistoryLotProc;
        DataGridView DGVsubHistoryWaferProc;
        DataGridView DGVsubLogDebug;

        public void clearAllDGVData()
        {
            int a = DGVsubLogEvent.Rows.Count;
            for (int i = 0; i < a; i++)
            {
                DGVsubLogEvent.Rows.RemoveAt(0);
            }


            int b = DGVsubHistoryLotRun.Rows.Count;
            for (int i = 0; i < b; i++)
            {
                DGVsubHistoryLotRun.Rows.RemoveAt(0);
            }


            int c = DGVsubHistoryWaferRun.Rows.Count;
            for (int i = 0; i < c; i++)
            {
                DGVsubHistoryWaferRun.Rows.RemoveAt(0);
            }



            int d = DGVsubHistoryLotProc.Rows.Count;
            for (int i = 0; i < d; i++)
            {
                DGVsubHistoryLotProc.Rows.RemoveAt(0);
            }

            int g = DGVsubHistoryWaferProc.Rows.Count;
            for (int i = 0; i < g; i++)
            {
                DGVsubHistoryWaferProc.Rows.RemoveAt(0);
            }

            int f = DGVsubLogDebug.Rows.Count;
            for (int i = 0; i < f; i++)
            {
                DGVsubLogDebug.Rows.RemoveAt(0);
            }

            int hf = DGVsubLogLogging.Rows.Count;
            for (int i = 0; i < hf; i++)
            {
                DGVsubLogLogging.Rows.RemoveAt(0);
            }








        }


        public frmHistory()
        {

            DataTableChanged += new MyEventHandler(SaveXml); // 데이터 변동되면 저장메소드 실행
            InitializeComponent();
            wndHistorySubMenu[0] = new subLogEvent();
            wndHistorySubMenu[1] = new subLogLogging();
            wndHistorySubMenu[2] = new subHistoryLotRun();
            wndHistorySubMenu[3] = new subHistoryWaferRun();
            wndHistorySubMenu[4] = new subHistoryLotProc();
            wndHistorySubMenu[5] = new subHistoryWaferProc();
            wndHistorySubMenu[6] = new subLogDebug();

            //DGV == DataGridView
            DGVsubLogEvent = ((subLogEvent)wndHistorySubMenu[0]).gridLog_subLogEvent;
            DGVsubLogLogging = ((subLogLogging)wndHistorySubMenu[1]).gridLog_subLogLogging;
            DGVsubHistoryLotRun = ((subHistoryLotRun)wndHistorySubMenu[2]).gridLog_subHistoryLotRun;
            DGVsubHistoryWaferRun = ((subHistoryWaferRun)wndHistorySubMenu[3]).gridLog_subHistoryWaferRun;
            DGVsubHistoryLotProc = ((subHistoryLotProc)wndHistorySubMenu[4]).gridLog_subHistoryLotProc;
            DGVsubHistoryWaferProc = ((subHistoryWaferProc)wndHistorySubMenu[5]).gridLog_subHistoryWaferProc;
            DGVsubLogDebug = ((subLogDebug)wndHistorySubMenu[6]).gridLog_subLogDebug;

            DGVsubLogEvent.DataSource = GlobalDataSet.dataset;
            DataTable subLogEvent = GlobalDataSet.dataset.Tables.Add("subLogEvent");
            DGVsubLogEvent.DataMember = "subLogEvent"; //dataset에 이테이블이 sublogevent 라고 등록
            func(DGVsubLogEvent, subLogEvent);







            DGVsubLogLogging.DataSource = GlobalDataSet.dataset;

            DataTable subLogLogging = GlobalDataSet.dataset.Tables.Add("subLogLogging");
            DGVsubLogLogging.DataMember = "subLogLogging";
            func(DGVsubLogLogging, subLogLogging);

            DGVsubHistoryLotRun.DataSource = GlobalDataSet.dataset;

            DataTable subHistoryLotRun = GlobalDataSet.dataset.Tables.Add("subHistoryLotRun");
            DGVsubHistoryLotRun.DataMember = "subHistoryLotRun";
            func(DGVsubHistoryLotRun, subHistoryLotRun);

            DGVsubHistoryWaferRun.DataSource = GlobalDataSet.dataset;

            DataTable subHistoryWaferRun = GlobalDataSet.dataset.Tables.Add("subHistoryWaferRun");
            DGVsubHistoryWaferRun.DataMember = "subHistoryWaferRun";
            func(DGVsubHistoryWaferRun, subHistoryWaferRun);

            DGVsubHistoryLotProc.DataSource = GlobalDataSet.dataset;

            DataTable subHistoryLotProc = GlobalDataSet.dataset.Tables.Add("subHistoryLotProc");
            DGVsubHistoryLotProc.DataMember = "subHistoryLotProc";
            func(DGVsubHistoryLotProc, subHistoryLotProc);

            DGVsubHistoryWaferProc.DataSource = GlobalDataSet.dataset;

            DataTable subHistoryWaferProc = GlobalDataSet.dataset.Tables.Add("subHistoryWaferProc");
            DGVsubHistoryWaferProc.DataMember = "subHistoryWaferProc";
            func(DGVsubHistoryWaferProc, subHistoryWaferProc);


            DGVsubLogDebug.DataSource = GlobalDataSet.dataset;

            DataTable subLogDebug = GlobalDataSet.dataset.Tables.Add("subLogDebug");
            DGVsubLogDebug.DataMember = "subLogDebug";
            func(DGVsubLogDebug, subLogDebug);

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
            GetCurrentDateLogXml();
            SetFirstXML();

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

        public void SubScreenChange(int nPageNo)
        {
            IndexForm = nPageNo;
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

        
        private void SetLogToDataTable(Eidentify index, string content)
        {
            //indexform 정보를 맞게 변경해야됨; 스트링값도 받아야함
            //globaldefine 에 폼정보박아놓고 매개변수로받아 식별하게만듬
            //폼별로 받는정보가다름, 정보를 어캐처리하지 생각해야됨

            DataRow dr = GlobalDataSet.dataset.Tables[(int)index].NewRow();
            dr["No"] = "123";
            GlobalDataSet.dataset.Tables[(int)index].Rows.Add(dr);

            DataTableChanged(); //데이터 변동 이벤트
        }//DataTable에 해당Content 기록


        private void button1_Click(object sender, EventArgs e)
        {

            DataRow dr = GlobalDataSet.dataset.Tables[IndexForm].NewRow();
            dr["No"] = "123";
            GlobalDataSet.dataset.Tables[IndexForm].Rows.Add(dr);

            DataTableChanged();

        }

        private string GetFormName(int i)
        {
            string ret = "";
            switch (i)
            {
                case 0:
                    ret = "subLogEvent";
                    break;
                case 1:
                    ret = "subLogLogging";
                    break;
                case 2:
                    ret = "subHistoryLotRun";
                    break;
                case 3:
                    ret = "subHistoryWaferRun";
                    break;
                case 4:
                    ret = "subHistoryLotProc";
                    break;
                case 5:
                    ret = "subHistoryWaferProc";
                    break;
                case 6:
                    ret = "subLogDebug";
                    break;
                default:
                    MessageBox.Show("GetFormName 불가");
                    break;

            }
            return ret;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        public string GetDatasetXML()
        {


            return dtp_Log.Value.ToShortDateString() + "-dataset.xml";

        }

        private void SaveXml()
        {
            string filePath = GetDatasetXML();

            GlobalDataSet.dataset.WriteXml(filePath);
        } //데이터를 현재 날짜에 맞춰 저장함

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = GetDatasetXML();

            GlobalDataSet.dataset.WriteXml(filePath);

        }

        private void GetCurrentDateLogXml()
        {
            if (System.IO.File.Exists(GetDatasetXML()))
            {
                GlobalDataSet.dataset.ReadXml(GetDatasetXML(), XmlReadMode.ReadSchema);
            }
            else
            {
                MessageBox.Show("오늘날짜파일없음");
                clearAllDGVData();
            }
        }//폼로드후 getXml;
        private void SetXml()
        {
            string filePath = GetDatasetXML();

            GlobalDataSet.dataset.WriteXml(filePath);
        }//xml파일에 저장함


        private void SetFirstXML()
        {
            if (System.IO.File.Exists(GetDatasetXML()))
            {
                clearAllDGVData();
                GlobalDataSet.dataset.ReadXml(GetDatasetXML(), XmlReadMode.ReadSchema);
            }
            else
            {
                MessageBox.Show("파일존재하지않음");
                clearAllDGVData();
            }
        } // 폼로드시 오늘 로그 데이터를 dataset에 대입 폼 로드시 한번만 실행해야됨


        private void button5_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(GetDatasetXML()))
            {
                GlobalDataSet.dataset.ReadXml(GetDatasetXML(), XmlReadMode.ReadSchema);
            }
            else
            {
                MessageBox.Show("오늘날짜파일없음");
                clearAllDGVData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            GLOBAL.GlobalFunction.SetLogToDataTable(Eidentify.subLogEvent, "1");
        }


        private void btn_LoadLog_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(GetDatasetXML()))
            {
                clearAllDGVData();
                GlobalDataSet.dataset.ReadXml(GetDatasetXML(), XmlReadMode.ReadSchema);
            }
            else
            {
                MessageBox.Show("파일존재하지않음");
                clearAllDGVData();
            }
        }
    }
}
