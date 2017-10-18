using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CJ_Controls.Communication.Global;
using System.Collections.Generic;
using TBDB_CTC.Data;

namespace TBDB_CTC.GUI
{
    public partial class frmTestForm : Form
    {
        MainData _Main = null;

        public frmTestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            switch ((int)comboBox1.SelectedValue)
            {
                case 0:
                    GlobalFunction.Instance.SetErr(textBox1.Text, (GlobalDefine.Eidentify_error)(int)comboBox1.SelectedValue);
                    break;
                case 1:
                    MessageBox.Show("1");
                    GlobalFunction.Instance.SetErr(textBox1.Text, (GlobalDefine.Eidentify_error)(int)comboBox1.SelectedValue);
                    break;
                case 2:
                    MessageBox.Show("2");
                    GlobalFunction.Instance.SetErr(textBox1.Text, (GlobalDefine.Eidentify_error)(int)comboBox1.SelectedValue);
                    break;
                case 3:
                    MessageBox.Show("3");
                    GlobalFunction.Instance.SetErr(textBox1.Text, (GlobalDefine.Eidentify_error)(int)comboBox1.SelectedValue);
                    break;

            }
            
   

            GlobalEvent.Instance.SetGetErrorEvent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Dictionary<int ,string> comboSource = new Dictionary<int, string>();
            comboSource.Add(0, "CyborgRobot_HTR");
            comboSource.Add(1, "Nano300");
            comboSource.Add(2, "Aligner_PA300C");
            comboSource.Add(3, "CyMechsRobot");

            comboBox1.DataSource = new BindingSource(comboSource, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

            comboBox2.DataSource = new BindingSource(comboSource, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";


        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            SendCommand_Test((int)comboBox2.SelectedValue, textBox2.Text);
        }
        public void SendCommand_Test(int index, string content)
        {
            
            
            bool ret;
            switch (index)
            {
                case 0:
                    ret = _Main.GetAtmTmData().Robot.SendData_test(content);
                    MessageBox.Show(ret.ToString());
                   
                    break;
                case 1:
                    ret = _Main.GetLoaderData().GetPortData(3).GetNano300().SendData_Test(content);
                    MessageBox.Show(ret.ToString());
                    break;
                case 2:
                    ret = _Main.GetLoaderData().Aligner.SendData_Test(content);
                    MessageBox.Show(ret.ToString());
                    break;
                case 3: 
                    ret = _Main.GetVaccumTmData().Robot.SendData_Test(content);
                    MessageBox.Show(ret.ToString());
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label15.BackColor = _Main.GetAtmTmData().Robot.IsOpen() ? Color.Green : Color.Red;
            label16.BackColor = _Main.GetLoaderData().GetPortData(3).GetNano300().IsOpen() ? Color.Green : Color.Red;
            label17.BackColor = _Main.GetLoaderData().Aligner.IsOpen() ? Color.Green : Color.Red;
            label18.BackColor = _Main.GetVaccumTmData().Robot.IsOpen() ? Color.Green : Color.Red;

            Cmd_Read_RobotStatus();

            label26.BackColor = _Main.GetLoaderData().GetPortData(0).GetNano300().LPM_Data.StatusData[(int)CJ_Controls.Communication.Nano300.STATUS_DATA.STS_ALARM_OCCURED] == 1 ? Color.Green : Color.Red;
            label27.BackColor = _Main.GetLoaderData().GetPortData(1).GetNano300().LPM_Data.StatusData[(int)CJ_Controls.Communication.Nano300.STATUS_DATA.STS_ALARM_OCCURED] == 1 ? Color.Green : Color.Red;
            label28.BackColor = _Main.GetLoaderData().GetPortData(2).GetNano300().LPM_Data.StatusData[(int)CJ_Controls.Communication.Nano300.STATUS_DATA.STS_ALARM_OCCURED] == 1 ? Color.Green : Color.Red;
            label29.BackColor = _Main.GetLoaderData().GetPortData(3).GetNano300().LPM_Data.StatusData[(int)CJ_Controls.Communication.Nano300.STATUS_DATA.STS_ALARM_OCCURED] == 1 ? Color.Green : Color.Red;

            Cmd_Read_STAT();

        }

        public async void Cmd_Read_STAT()
        {//This command returns current system state.
            string str = "";
            while (_Main.GetLoaderData().Aligner.Seq_ReadCommand_Test("STAT", ref str) == 0)
            {
                await Task.Delay(100);
            }

            if (str.Length < 1) return;


            label30.BackColor = str.Contains("#08") == true ? Color.Green : Color.Red;

        }

        public async void Cmd_Read_RobotStatus()
        {// Robot Status
            
            string str = "";
            while (_Main.GetAtmTmData().Robot.Seq_ReadCommand_Test("R_RSTAT", ref str) == 0)
            {
                await Task.Delay(100);
            }

            List<string> result = new List<string>();
            result.AddRange(str.Split(','));

            try
            {
                //int nVal = 0;
                //int.TryParse(result[0], out nVal);
                //RobotData.RunStatus = nVal == 1 ? true : false;
                //int.TryParse(result[1], out nVal);
                //RobotData.AbnormalStatus = nVal == 1 ? true : false;
                //int.TryParse(result[2], out nVal);
                //RobotData.ServoOnStatus = nVal == 1 ? true : false;
                //int.TryParse(result[3], out nVal);
                //RobotData.ArmFoldStatus = nVal == 1 ? true : false;
                //int.TryParse(result[6], out nVal);
                //RobotData.BetteryStatus = nVal == 1 ? true : false;

                int nVal = 0;
                int.TryParse(result[0].Substring(0, 1), out nVal);
                label19.BackColor = nVal == 1 ? Color.Green : Color.Red;
                int.TryParse(result[0].Substring(1, 1), out nVal);
                label20.BackColor = nVal == 1 ? Color.Green : Color.Red;
                int.TryParse(result[0].Substring(2, 1), out nVal);
                label21.BackColor = nVal == 1 ? Color.Green : Color.Red;
                int.TryParse(result[0].Substring(3, 1), out nVal);
                label22.BackColor = nVal == 1 ? Color.Green : Color.Red;
                int.TryParse(result[0].Substring(6, 1), out nVal);
                label23.BackColor = nVal == 1 ? Color.Green : Color.Red;

                //추가
                int.TryParse(result[0].Substring(14, 1), out nVal);
                label24.BackColor = nVal == 1 ? Color.Green : Color.Red;
                int.TryParse(result[0].Substring(15, 1), out nVal);
                label25.BackColor = nVal == 1 ? Color.Green : Color.Red;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
        public async void Cmd_Send_Status()
        {
            string strCmd = "STATUS";
            while (_Main.GetLoaderData().GetPortData(3).GetNano300().Seq_Wait_Command_Test(strCmd, CJ_Controls.Communication.Nano300.ACK_MODE.STATUS_DATA) == 0)
            {
                await Task.Delay(100);
            }
        }
        private void frmTestForm_Load(object sender, EventArgs e)
        {
            _Main = MainData.Instance;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void sendRestCommand(int index)
        {
            bool ret;
            switch (index)
            {
                case 0:
                    ret = _Main.GetAtmTmData().Robot.SendData_test("R_RESET");
                    MessageBox.Show(ret.ToString());
                    break;
                case 1:
                    ret = _Main.GetLoaderData().GetPortData(3).GetNano300().SendData_Test("RESET");
                    MessageBox.Show(ret.ToString());
                    break;
                case 2:
                    ret = _Main.GetLoaderData().Aligner.SendData_Test("#ECLR");
                    MessageBox.Show(ret.ToString());
                    break;
                case 3:
                    ret = _Main.GetVaccumTmData().Robot.SendData_Test("CLEAR");
                    MessageBox.Show(ret.ToString());
                    break;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
