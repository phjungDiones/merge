using CJ_Controls.DeviceNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_CTC.Data;
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.GUI
{
    public partial class frmIO : Form
    {
        MainData _Main = MainData.Instance;

        //COM_DeviceNet DNet = null;

        public frmIO()
        {
            InitializeComponent();
            //DNet = COM_DeviceNet.Instance;
            //DNet.DNet_IO_List = this.DNet_Io_List;

            //this.DNet.Open();

            //IO_List_View.SetDeviceNet(DNet);
            //tmrStatus.Start();

            GlobalVariable.io.StartReadIO(IO_List_View);
        }

        private void frmIO_Load(object sender, EventArgs e)
        {

            
            //DNet.bTestFalg = true;
            tmrStatus.Start();
        }

        private void btnCreateList_Click(object sender, EventArgs e)
        {
            Form_IO_List_Maker dlg = new Form_IO_List_Maker();
            //dlg.IO_List_DataSet = this.DNet_Io_List;
            dlg.ShowDialog();
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            //DNet.ReadAll_and_Matching();
			//this.DNet.ReadAll_and_Matching();
        }


    }
}
