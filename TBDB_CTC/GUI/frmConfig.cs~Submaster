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
using TBDB_CTC.Data;
using TBDB_CTC.UserCtrl.SubForm.ConfigSub;
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.GUI
{
    public partial class frmConfig : Form
    {
        private MainData _Main = null;
        public UserControl[] wndCfgSubMenu = new UserControl[4];

        public frmConfig()
        {
            InitializeComponent();

            _Main = MainData.Instance;

            wndCfgSubMenu[0] = new subConfigParam();
            wndCfgSubMenu[1] = new subConfigCTC();
            wndCfgSubMenu[2] = new subConfigUser();
            wndCfgSubMenu[3] = new subConfigFA();

            panConfigSubClient.Controls.Add(wndCfgSubMenu[0]);
            panConfigSubClient.Controls.Add(wndCfgSubMenu[1]);
            panConfigSubClient.Controls.Add(wndCfgSubMenu[2]);
            panConfigSubClient.Controls.Add(wndCfgSubMenu[3]);
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            _Main = MainData.Instance;
            //LoadConfig();

            wndCfgSubMenu[0].Show();
        }

        public void LoadConfig()
        {
            PortOpenAll(); //Port Open
        }

        public void CloseConfig()
        {
            CloseAll();
        }

        private void PortOpenAll()
        {
            bool bRet = false;

            _Main.ConfigMgr.Port1.Comport = "COM19";
            _Main.ConfigMgr.Port1.Baudrate = 9600;
            _Main.ConfigMgr.Port2.Comport = "COM20";
            _Main.ConfigMgr.Port2.Baudrate = 9600;
            _Main.ConfigMgr.Port3.Comport = "COM21";
            _Main.ConfigMgr.Port3.Baudrate = 9600;
            _Main.ConfigMgr.Port4.Comport = "COM22";
            _Main.ConfigMgr.Port4.Baudrate = 9600;

            _Main.ConfigMgr.FM_Robot_Com.Comport = "COM4";
            _Main.ConfigMgr.FM_Robot_Com.Baudrate = 19200;

            _Main.ConfigMgr.AtmTM_Robot_Com.Comport = "COM3";
            _Main.ConfigMgr.AtmTM_Robot_Com.Baudrate = 19200;

            _Main.ConfigMgr.VacTM_Robot_Com.Comport = "COM28";
            _Main.ConfigMgr.VacTM_Robot_Com.Baudrate = 9600;

            //_Main.ConfigMgr.Aligner_Com.Comport = "COM34";
            _Main.ConfigMgr.Aligner_Com.Comport = "COM29";
            _Main.ConfigMgr.Aligner_Com.Baudrate = 9600;

            //Loadlock
            _Main.ConfigMgr.Loadlock_Com.Comport = "COM27";
            _Main.ConfigMgr.Loadlock_Com.Baudrate = 9600;

            //Laminator
            //Pmc

            _Main.GetLoaderData().GetPortData(0).GetNano300().Open(_Main.ConfigMgr.Port1.Comport, _Main.ConfigMgr.Port1.Baudrate);
            _Main.GetLoaderData().GetPortData(1).GetNano300().Open(_Main.ConfigMgr.Port2.Comport, _Main.ConfigMgr.Port2.Baudrate);
            _Main.GetLoaderData().GetPortData(2).GetNano300().Open(_Main.ConfigMgr.Port3.Comport, _Main.ConfigMgr.Port3.Baudrate);
            _Main.GetLoaderData().GetPortData(3).GetNano300().Open(_Main.ConfigMgr.Port4.Comport, _Main.ConfigMgr.Port4.Baudrate);

            _Main.GetLoaderData().Robot.Open(_Main.ConfigMgr.FM_Robot_Com.Comport, _Main.ConfigMgr.FM_Robot_Com.Baudrate);
            _Main.GetAtmTmData().Robot.Open(_Main.ConfigMgr.AtmTM_Robot_Com.Comport, _Main.ConfigMgr.AtmTM_Robot_Com.Baudrate);
            _Main.GetVaccumTmData().Robot.Open(_Main.ConfigMgr.VacTM_Robot_Com.Comport, _Main.ConfigMgr.VacTM_Robot_Com.Baudrate);

            //Alinger
            _Main.GetLoaderData().Aligner.Open(_Main.ConfigMgr.Aligner_Com.Comport, _Main.ConfigMgr.Aligner_Com.Baudrate);
            
            //Loadlock
            //_Main.GetLoadlockData().Loadlock.OpenComm(_Main.ConfigMgr.Loadlock_Com.Comport, _Main.ConfigMgr.Loadlock_Com.Baudrate);
            bRet = GlobalSeq.autoRun.procLoadlock.Loadlock.ComOpen(_Main.ConfigMgr.Loadlock_Com.Comport, _Main.ConfigMgr.Loadlock_Com.Baudrate);
            if( !bRet)
            {
                MessageBox.Show("Loadlock Open Fail..");
            }

            //Laminator 
            string strIp = "192.168.3.100";
            _Main.GetLaminatorData().Laminator.Init_MXComp(strIp);

            //Pmc Meslec
            _Main.GetPmcData().Pmc.Open();

        }

        private void CloseAll()
        {
            _Main.GetLoaderData().GetPortData(0).GetNano300().Close();
            _Main.GetLoaderData().GetPortData(1).GetNano300().Close();
            _Main.GetLoaderData().GetPortData(2).GetNano300().Close();
            _Main.GetLoaderData().GetPortData(3).GetNano300().Close();

            _Main.GetLoaderData().Robot.Close();
            _Main.GetAtmTmData().Robot.Close();
            _Main.GetVaccumTmData().Robot.Close();
            _Main.GetLoaderData().Aligner.Close();

            GlobalSeq.autoRun.procLoadlock.Loadlock.ComClose(_Main.ConfigMgr.Loadlock_Com.Comport);
            _Main.GetLaminatorData().Laminator.Close_Com();
            _Main.GetPmcData().Pmc.Close();
        }

        private void frmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseAll(); //Port Close
        }

        private void OnCfgSubMenuButtonClick(object sender, EventArgs e)
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

            for (int nCfgSubMenuCount = 0; nCfgSubMenuCount < 4; nCfgSubMenuCount++)
            {
                wndCfgSubMenu[nCfgSubMenuCount].Hide();
            }

            wndCfgSubMenu[nPageNo].Show();
        }
    }
}
