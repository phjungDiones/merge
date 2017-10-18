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
using System.Data;
using TBDB_CTC.Data;


namespace TBDB_CTC.GUI
{
    public partial class frmAlarm : Form
    {
        MainData _Main = null;
        public frmAlarm()
        {
            InitializeComponent();
        }

        private void a1Panel6_Paint(object sender, PaintEventArgs e)
        {

        }
        public void GetCurrentDateLogXml()
        {
            //if (System.IO.File.Exists("AllErrorTables.xml"))
            //{
            //    GlobalDataSet.dataset2.ReadXml("AllErrorTables.xml", XmlReadMode.Auto);
            gridLog_alarm.DataSource = GlobalDataSet.dataset3;
                   gridLog_alarm.DataMember = "default";
                
                
            //}
            //else
            //{
            //    GlobalDataSet.dataset2.Tables.Add("default");
            //    DataRow newDr = GlobalDataSet.dataset2.Tables[0].NewRow();
            //    if (GlobalDataSet.dataset2.Tables[0].Columns.Count == 0)
            //    {
            //        GlobalDataSet.dataset2.Tables[0].Columns.Add("index");
            //        GlobalDataSet.dataset2.Tables[0].Columns.Add("Model");
            //        GlobalDataSet.dataset2.Tables[0].Columns.Add("No");
            //        GlobalDataSet.dataset2.Tables[0].Columns.Add("AlarmDescription");
            //        GlobalDataSet.dataset2.Tables[0].Columns.Add("DateTime");

            //        GlobalDataSet.dataset2.WriteXml("AllErrorTables.xml");

            //        GlobalDataSet.dataset2.ReadXml("AllErrorTables.xml", XmlReadMode.Auto);
            //        MessageBox.Show(GlobalDataSet.dataset2.GetXml().ToString());
            //        gridLog_alarm.DataSource = GlobalDataSet.dataset2;
            //        gridLog_alarm.DataMember = "default";
            //    }
            //    MessageBox.Show("파일 존재 하지 않음;");

            //}
        }//폼로드후 getXml;

        private void frmAlarm_Load(object sender, EventArgs e)
        {
            _Main = MainData.Instance;

            GetCurrentDateLogXml();
            gridLog_alarm.CellValueChanged += delegate { setDes(); };
            resetindex();
        }


   




        public void clearAllDGVData()
        {
            int a = gridLog_alarm.Rows.Count;
            for (int i = 0; i < a; i++)
            {
                gridLog_alarm.Rows.RemoveAt(0);
            }


           








        }


        private void gridLog_alarm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Svaexml()
        {
            if (System.IO.File.Exists("AllErrorTables.xml"))
            {
                
                GlobalDataSet.dataset3.WriteXml("AllErrorTables_clone.xml");
            }
        }
        private void setDes()
        {
            int x = gridLog_alarm.CurrentCell.RowIndex;
            GlobalDataSet.dataset3.Tables[0].Rows[x]["AlarmDescription"] = gridLog_alarm.Rows[x].Cells["AlarmDescription"].Value.ToString();
            Svaexml();
        }

        private void setxml()
        {
            if (System.IO.File.Exists("AllErrorTables_clone.xml"))
            {
                GlobalDataSet.dataset3.ReadXml("AllErrorTables_clone.xml", XmlReadMode.Auto);
            }
            //MessageBox.Show(GlobalDataSet.dataset2.GetXml());
        }
        private void clearM_Error(string target)
        {

        }
        private void sendRestCommand(int index)
        {
            bool ret;
            switch (index)
            {
                case 0:
                    ret = _Main.GetAtmTmData().Robot.SendData_test("R_RESET");
                  //  MessageBox.Show(ret.ToString());
                    break;
                case 1:
                    ret = _Main.GetLoaderData().GetPortData(3).GetNano300().SendData_Test("RESET");
                   // MessageBox.Show(ret.ToString());
                    break;
                case 2:
                    ret = _Main.GetLoaderData().Aligner.SendData_Test("#ECLR");
                   // MessageBox.Show(ret.ToString());
                    break;
                case 3:
                    ret = _Main.GetVaccumTmData().Robot.SendData_Test("CLEAR");
                  //  MessageBox.Show(ret.ToString());
                    break;
            }
        }
        private void win_GlassButton3_Click(object sender, EventArgs e)
        {
            if (GlobalDataSet.dataset3.Tables[0].Rows.Count == 0) return;



            //MessageBox.Show(GlobalDataSet.dataset2.Tables[0].Rows[0]["Model"].ToString());

            int x = gridLog_alarm.CurrentCell.RowIndex;
            string targetModel = gridLog_alarm.Rows[x].Cells["Model"].Value.ToString();



            if (targetModel == "CyborgRobot_HTR")
            {
                sendRestCommand(0);
            }
            
            if(targetModel == "Nano300")
            {
                sendRestCommand(1);
            }
            if (targetModel == "Aligner_PA300C")
            {
                sendRestCommand(2);
            }
            if(targetModel == "CyMechsRobot")
            {
                sendRestCommand(3);
            }
            GlobalDataSet.dataset3.Tables[0].Rows[x].Delete();
            resetindex();
            Svaexml();
        }

        private void win_GlassButton1_Click(object sender, EventArgs e)
        {
            GlobalError.nIndex = 0;
            int count = GlobalDataSet.dataset3.Tables[0].Rows.Count;
            for (int i=0;i< count; i++)
            {
                GlobalDataSet.dataset3.Tables[0].Rows[0].Delete();
            }
            resetindex();
            Svaexml();
            sendRestCommand(1);
            sendRestCommand(2);
            sendRestCommand(3);
            sendRestCommand(4);
            
        }

        private void win_GlassButton2_Click(object sender, EventArgs e)
        {
            //리셋동작
            GlobalDataSet.dataset2.WriteXml("AllErrorTables.xml");
            GlobalDataSet.dataset3.WriteXml("AllErrorTables_clone.xml");
           // removeAllGrid();
            GlobalDataSet.dataset3.Clear();
            GlobalDataSet.dataset3 = GlobalDataSet.dataset2.Clone();
            GlobalDataSet.dataset3 = GlobalDataSet.dataset2.Copy();

            gridLog_alarm.DataSource = GlobalDataSet.dataset3;

            gridLog_alarm.DataMember = "default";

            resetindex();
        }

        private void removeAllGrid()
        {
                 int a = gridLog_alarm.Rows.Count;
            for (int i = 0; i < a; i++)
            {
                gridLog_alarm.Rows.RemoveAt(0);
            }
        }

        public void resetindex()
        {
            for (int i = 0; i < GlobalDataSet.dataset3.Tables[0].Rows.Count; i++)
            {
                GlobalDataSet.dataset3.Tables[0].Rows[i]["index"] = i.ToString();
            }
            Svaexml();
        }
    }
}
