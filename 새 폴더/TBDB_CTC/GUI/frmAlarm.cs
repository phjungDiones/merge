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


namespace TBDB_CTC.GUI
{
    public partial class frmAlarm : Form
    {

        public frmAlarm()
        {
            InitializeComponent();
        }

        private void a1Panel6_Paint(object sender, PaintEventArgs e)
        {

        }
        public void GetCurrentDateLogXml()
        {
            if (System.IO.File.Exists("AllErrorTables.xml"))
            {
                GlobalDataSet.dataset2.ReadXml("AllErrorTables.xml", XmlReadMode.Auto);
                MessageBox.Show(GlobalDataSet.dataset2.GetXml().ToString());
                gridLog_alarm.DataSource = GlobalDataSet.dataset2;
                gridLog_alarm.DataMember = "default";
            }
            else
            {
                GlobalDataSet.dataset2.Tables.Add("default");
                DataRow newDr = GlobalDataSet.dataset2.Tables[0].NewRow();
                if (GlobalDataSet.dataset2.Tables[0].Columns.Count == 0)
                {
                    GlobalDataSet.dataset2.Tables[0].Columns.Add("index");
                    GlobalDataSet.dataset2.Tables[0].Columns.Add("Model");
                    GlobalDataSet.dataset2.Tables[0].Columns.Add("No");
                    GlobalDataSet.dataset2.Tables[0].Columns.Add("AlarmDescription");
                    GlobalDataSet.dataset2.Tables[0].Columns.Add("DateTime");

                    GlobalDataSet.dataset2.WriteXml("AllErrorTables.xml");

                    GlobalDataSet.dataset2.ReadXml("AllErrorTables.xml", XmlReadMode.Auto);
                    MessageBox.Show(GlobalDataSet.dataset2.GetXml().ToString());
                    gridLog_alarm.DataSource = GlobalDataSet.dataset2;
                    gridLog_alarm.DataMember = "default";
                }
                MessageBox.Show("파일 존재 하지 않음;");

            }
        }//폼로드후 getXml;

        private void frmAlarm_Load(object sender, EventArgs e)
        {


            GetCurrentDateLogXml();

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

        private void setxml()
        {
            if (System.IO.File.Exists("AllErrorTables.xml"))
            {
                GlobalDataSet.dataset2.ReadXml("AllErrorTables.xml", XmlReadMode.Auto);
            }
            MessageBox.Show(GlobalDataSet.dataset2.GetXml());
        }

      


    }
}
