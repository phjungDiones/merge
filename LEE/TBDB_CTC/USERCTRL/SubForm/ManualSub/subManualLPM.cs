using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_CTC.Data;

namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    public partial class subManualLPM : UserControl
    {
        MainData _Main = null;

        public subManualLPM()
        {
            InitializeComponent();
        }

        private void subManualLPM_Load(object sender, EventArgs e)
        {
            _Main = MainData.Instance;
        }


        private void btnInit_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).SedCmdFunc(PORT_COMMAND.HOME);
        }

        private void btnMapping_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).SedCmdFunc(PORT_COMMAND.MAPPING);
        }

        private void btnClamp_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).SedCmdFunc(PORT_COMMAND.CLAMP);
        }

        private void btnUnClamp_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).SedCmdFunc(PORT_COMMAND.UNCLAMP);
        }

        private void btnDock_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).SedCmdFunc(PORT_COMMAND.FOUP_DOCK);
        }

        private void btnUndock_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).SedCmdFunc(PORT_COMMAND.FOUP_UNDOCK);
        }

        private void btnAlarmCl_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).SedCmdFunc(PORT_COMMAND.CLEAR);
        }

        private void btnCmdReset_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).ResetCommand_Seq();
        }

        private void btnLoad_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).SedCmdFunc(PORT_COMMAND.LOAD);
        }

        private void btn_Unload_0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).SedCmdFunc(PORT_COMMAND.UNLOAD);
        }

        private void btnAutoModeOn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).GetNano300().Cmd_Send_AutoMode(true);
        }

        private void btnAutoModeOff_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).GetNano300().Cmd_Send_AutoMode(false);
        }

        private void btnMaintModeOn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).GetNano300().Cmd_Send_MAINT_MODE(true);
        }

        private void btnMaintModeOff_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).GetNano300().Cmd_Send_MAINT_MODE(false);
        }

        private void btnMappingmodeOn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).GetNano300().Cmd_Send_MappingMode(true);
        }

        private void btnMappingmodeOff_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int nTag = Convert.ToInt16(btn.Tag);

            _Main.GetLoaderData().GetPortData(nTag).GetNano300().Cmd_Send_MappingMode(false);
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false) return;

            //btnDock_0.BackColor = (_Main.GetLoaderData().GetPortData(0).GetNano300().Read== true) ? Color.LightGreen : Color.Gray;

        }

   
    }
}
