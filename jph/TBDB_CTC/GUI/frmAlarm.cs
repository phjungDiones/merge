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

        private void frmAlarm_Load(object sender, EventArgs e)
        {

           
           



        }


       
        private void gridLog_alarm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void setxml()
        {
            if (System.IO.File.Exists("AllDataSet.xml"))
            {
                GlobalDataSet.dataset2.ReadXml("AllDataSet.xml", XmlReadMode.Auto);
            }
            MessageBox.Show(GlobalDataSet.dataset2.GetXml());
        }

      


    }
}
