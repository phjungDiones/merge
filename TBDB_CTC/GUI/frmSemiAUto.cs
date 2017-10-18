using CJ_Controls.Communication.QuadraVTM4;
using CJ_Controls.Communication.CybogRobot_HTR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.SEQ;
using TBDB_Handler.MOTION;
using TBDB_Handler.THREAD;
using System.Threading;
using TBDB_CTC.Data;

namespace TBDB_CTC.GUI
{

    public enum ManualUnit
    {
        LPMA = 0,
        LPMB,
        LPMC,
        LPMD,
        AL,
        CP1,
        CP2,
        CP3,
        HP,
        LL2_CARR,
        LL1_DIV,
        LL_BD,
        LAMI,
        PMC,
        MAX,
    }

    public partial class frmSemiAUto : Form
    {

        private int m_nWorkingStage = 0;
        private int m_nFmRobotSlotNo = 0;

        private bool m_bStopFmRobot = false;
        private bool m_bStopAtmRobot = false;
        private bool m_bStopVtmRobot = false;

        private int m_nSelectSource = 0;

        CJ_Controls.Communication.CybogRobot_HTR.ARM FmRobot_Arm;
        CJ_Controls.Communication.CybogRobot_HTR.ARM AtmRobot_Arm;
        CJ_Controls.Communication.QuadraVTM4.ARM VtmRobot_Arm;

        public frmSemiAUto()
        {
            InitializeComponent();
        }

        private void frmSemiAUto_Load(object sender, EventArgs e)
        {

            InitControl();
            //SetButtonEnable(false); //초기값 disable
        }

        public void InitControl()
        {
            for (int i = 1; i <= COUNT.MAX_PORT_SLOT; i++ )
            {
                cbSlotSource.Items.Add(i.ToString());
                cbSlotDest.Items.Add(i.ToString());
            }

            cbUnitSel.SelectedIndex = 0;
            cbArmSel.SelectedIndex = 0;
            m_nSelectSource = -1;
        }


        private void btnLoadLami_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_Load_Lami;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnUnloadLami_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_Unload_Lami;

            GlobalVariable.manualInfo.SelArmATM = (HAND)cbAtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnProcLami_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_ProcStart_Lami;

            GlobalVariable.manualInfo.SelArmATM = (HAND)cbAtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnAtmLoadLL_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_Place_Loadlock;

            GlobalVariable.manualInfo.SelArmATM = (HAND)cbAtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnUnloadLL_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_Pickup_Loadlock;

            GlobalVariable.manualInfo.SelArmATM = (HAND)cbAtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnVtmLoadLL1_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Pickup_Loadlock;

            GlobalVariable.manualInfo.SelArmVTM = (HAND)cbVtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnVtmLoadLL2_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Pickup_Loadlock;

            GlobalVariable.manualInfo.SelArmVTM = (HAND)cbVtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }


        private void btnVtmUnloadBD_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Place_Loadlock;

            GlobalVariable.manualInfo.SelArmVTM = (HAND)cbVtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnVtmProcPMC_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Process_PMC;

            GlobalVariable.manualInfo.SelArmVTM = (HAND)cbVtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnFmStop_Click(object sender, EventArgs e)
        {

        }

        private void btnAtmStop_Click(object sender, EventArgs e)
        {

        }

        private void btnVtmStop_Click(object sender, EventArgs e)
        {

        }

        private void btnSetRcp_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_RcpChange_Lami;


            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnVtmLoadPmc_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Load_PMC;

            GlobalVariable.manualInfo.SelArmVTM = (HAND)cbVtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }

        private void btnVtmUnloadPmc_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Unload_PMC;

            GlobalVariable.manualInfo.SelArmVTM = (HAND)cbVtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }


        private void SetButtonEnable(bool bEnable)
        {
            CheckBox[] ChbSource = new CheckBox[(int)ManualUnit.MAX];
            CheckBox[] ChbDest = new CheckBox[(int)ManualUnit.MAX];
            CheckBox fndSource;
            CheckBox fndDest;

            for (int i = 0; i < ChbSource.Length; i++)
            {
                //체크박스 배열 처리
                fndSource = this.Controls.Find("chkSource_" + i.ToString(), true).FirstOrDefault() as CheckBox;
                if (fndSource != null)
                {
                    ChbSource[i] = fndSource;
                    ChbSource[i].Enabled = bEnable;
                }
            }
            for (int i = 0; i < ChbDest.Length; i++)
            {
                fndDest = this.Controls.Find("chkDest_" + i.ToString(), true).FirstOrDefault() as CheckBox;
                if (fndDest != null)
                {
                    ChbDest[i] = fndDest;
                    ChbDest[i].Enabled = bEnable;
                }
            }
        }

        private void SetUnitSelect()
        {
            if (cbUnitSel.SelectedIndex < 0) return;

            CheckBox[] ChbSource = new CheckBox[(int)ManualUnit.MAX];
            CheckBox[] ChbDest = new CheckBox[(int)ManualUnit.MAX];
            CheckBox fndSource;
            CheckBox fndDest;

            for (int i = 0; i < ChbSource.Length; i++)
            {
                //체크박스 배열 처리
                fndSource = this.Controls.Find("chkSource_" + i.ToString(), true).FirstOrDefault() as CheckBox;
                if (fndSource != null)
                {
                    ChbSource[i] = fndSource;
                    ChbSource[i].Checked = false;
                    ChbSource[i].Enabled = false;
                    ChbSource[i].BackColor = Color.Silver;
                }
            }
            for (int i = 0; i < ChbDest.Length; i++)
            {
                fndDest = this.Controls.Find("chkDest_" + i.ToString(), true).FirstOrDefault() as CheckBox;
                if (fndDest != null)
                {
                    ChbDest[i] = fndDest;
                    ChbDest[i].Checked = false;
                    ChbDest[i].Enabled = false;
                    ChbDest[i].BackColor = Color.Silver;
                }
            }

            switch (cbUnitSel.SelectedIndex)
            {
                case 0:
                    //FM Robot
                    for (int i = 0; i < (int)ManualUnit.MAX; i++ )
                    {
                        if (i == (int)ManualUnit.LPMA 
                            || i == (int)ManualUnit.LPMB
                            || i == (int)ManualUnit.LPMC 
                            || i == (int)ManualUnit.LPMD
                            /*|| i == (int)ManualUnit.AL*/ 
                            || i == (int)ManualUnit.CP1
                            || i == (int)ManualUnit.CP2 
                            || i == (int)ManualUnit.CP3)
                        {
                            //continue;
                            //ChbSource[i].Enabled = ChbDest[i].Enabled = true;
                            //ChbSource[i].BackColor = ChbDest[i].BackColor = Color.White;

                            ChbSource[i].Enabled  = true;
                            ChbSource[i].BackColor  = Color.White;
                        }
                    }
                    break;

                case 1:
                    //ATM Robot
                    for (int i = 0; i < (int)ManualUnit.MAX; i++)
                    {
                        if (i == (int)ManualUnit.HP 
                            || i == (int)ManualUnit.LAMI 
                            || i == (int)ManualUnit.LL_BD)
                        {
                            //continue;
                            ChbSource[i].Enabled = true;
                            ChbSource[i].BackColor = Color.White;
                        }

                    }
                    break;

                case 2:
                    //VTM Robot
                    for (int i = 0; i < (int)ManualUnit.MAX; i++)
                    {
                        if (i == (int)ManualUnit.PMC 
                            || i == (int)ManualUnit.LL1_DIV
                            || i == (int)ManualUnit.LL2_CARR 
                            || i == (int)ManualUnit.LL1_DIV)
                        {
                            //continue;
                            ChbSource[i].Enabled  = true;
                            ChbSource[i].BackColor  = Color.White;
                        }
                    }
                    break;

                default:
                    break;

            }

            lbSourceName.Text = "Select Unit";
            lbDestName.Text = "Select Unit";
        }

        private void cbUnitSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetUnitSelect();             
        }

        private void chkSource_0_CheckedChanged(object sender, EventArgs e)
        {
            //Source Check Box
            string[] res = ((CheckBox)sender).Name.ToString().Split('_');
            int nIndex = Convert.ToInt32(res[1]); //Index Parse
            m_nSelectSource = nIndex; //작업 할 유닛 인덱스

            CheckBox[] ChbSource = new CheckBox[(int)ManualUnit.MAX];
            CheckBox fndSource;
            CheckBox[] ChbDest = new CheckBox[(int)ManualUnit.MAX];
            CheckBox fndDest;

            for (int i = 0; i < ChbSource.Length; i++)
            {
                //체크박스 배열 처리
                fndSource = this.Controls.Find("chkSource_" + i.ToString(), true).FirstOrDefault() as CheckBox;
                if (fndSource != null)
                {
                    ChbSource[i] = fndSource;
                    ChbSource[i].BackColor = (ChbSource[i].Enabled == true) ?  Color.White :  Color.Silver;
                }
            }
            lbSourceName.Text = ChbSource[nIndex].Text;
            ChbSource[nIndex].BackColor = Color.Aqua;

            //Dest
            for (int i = 0; i < ChbDest.Length; i++)
            {
                //체크박스 배열 처리
                fndDest = this.Controls.Find("chkDest_" + i.ToString(), true).FirstOrDefault() as CheckBox;
                if (fndDest != null)
                {
                    ChbDest[i] = fndDest;
                    ChbDest[i].Enabled = false;
                    ChbDest[i].BackColor = Color.Silver;
                    //ChbDest[i].BackColor = (ChbDest[i].Enabled == true) ? Color.White : Color.Silver;
                }
            }

            //
            ManualUnit Unit =  (ManualUnit)nIndex; //현재 선택한 유닛

            switch(cbUnitSel.SelectedIndex)
            {
                case 0:
                    //FM Robot
                    switch (Unit)
                    {
                        case ManualUnit.LPMA:
                        case ManualUnit.LPMB:
                        case ManualUnit.LPMC:
                        case ManualUnit.LPMD:
                            ChbDest[(int)ManualUnit.AL].Enabled = true;
                            ChbDest[(int)ManualUnit.AL].BackColor = Color.White;
                            break;

                        case ManualUnit.CP1:
                        case ManualUnit.CP2:
                        case ManualUnit.CP3:
                            ChbDest[(int)ManualUnit.LPMA].Enabled = true;
                            ChbDest[(int)ManualUnit.LPMB].Enabled = true;
                            ChbDest[(int)ManualUnit.LPMC].Enabled = true;
                            ChbDest[(int)ManualUnit.LPMD].Enabled = true;
                            ChbDest[(int)ManualUnit.LPMA].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.LPMB].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.LPMC].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.LPMD].BackColor = Color.White;
                            break;
                            
                        default:
                            break;
                    }
                    break;

                case 1:
                    //ATM Robot
                    switch (Unit)
                    {
                        case ManualUnit.AL:
                            ChbDest[(int)ManualUnit.LAMI].Enabled = true;
                            ChbDest[(int)ManualUnit.LL1_DIV].Enabled = true;
                            ChbDest[(int)ManualUnit.LL2_CARR].Enabled = true;

                            ChbDest[(int)ManualUnit.LAMI].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.LL1_DIV].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.LL2_CARR].BackColor = Color.White;
                            break;

                        case ManualUnit.LAMI:
                            ChbDest[(int)ManualUnit.LL2_CARR].Enabled = true;
                            //CP, HP는 매뉴얼 경우만 사용
                            ChbDest[(int)ManualUnit.CP1].Enabled = true;
                            ChbDest[(int)ManualUnit.CP2].Enabled = true;
                            ChbDest[(int)ManualUnit.CP3].Enabled = true;
                            ChbDest[(int)ManualUnit.HP].Enabled = true;

                            ChbDest[(int)ManualUnit.LL2_CARR].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.CP1].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.CP2].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.CP3].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.HP].BackColor = Color.White;
                            break;

                        case ManualUnit.HP:
                            ChbDest[(int)ManualUnit.CP1].Enabled = true;
                            ChbDest[(int)ManualUnit.CP2].Enabled = true;
                            ChbDest[(int)ManualUnit.CP3].Enabled = true;

                            ChbDest[(int)ManualUnit.CP1].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.CP2].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.CP3].BackColor = Color.White;
                            break;

                        case ManualUnit.LL_BD:
                            //CP, HP는 매뉴얼 경우만 사용
                            ChbDest[(int)ManualUnit.CP1].Enabled = true;
                            ChbDest[(int)ManualUnit.CP2].Enabled = true;
                            ChbDest[(int)ManualUnit.CP3].Enabled = true;
                            ChbDest[(int)ManualUnit.HP].Enabled = true;


                            ChbDest[(int)ManualUnit.CP1].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.CP2].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.CP3].BackColor = Color.White;
                            ChbDest[(int)ManualUnit.HP].BackColor = Color.White;
                            break;

                        default:
                            break;
                    }
                    break;

                case 2:
                    //VTM Robot
                    switch (Unit)
                    {
                        case ManualUnit.LL1_DIV:
                        case ManualUnit.LL2_CARR:
                            ChbDest[(int)ManualUnit.PMC].Enabled = true;
                            ChbDest[(int)ManualUnit.PMC].BackColor = Color.White;
                            break;

                        case ManualUnit.PMC:
                            ChbDest[(int)ManualUnit.LL_BD].Enabled = true;
                            ChbDest[(int)ManualUnit.LL_BD].BackColor = Color.White;
                            break;

                        default:
                            break;
                    }
                    break;

            }
        }

        private void chkDest_0_CheckedChanged(object sender, EventArgs e)
        {
            //Dest Check Box
            string[] res = ((CheckBox)sender).Name.ToString().Split('_');
            int nIndex = Convert.ToInt32(res[1]); //Index Parse

            CheckBox[] ChbDest = new CheckBox[(int)ManualUnit.MAX];
            CheckBox fndDest;

            for (int i = 0; i < ChbDest.Length; i++)
            {
                //체크박스 배열 처리
                fndDest = this.Controls.Find("chkDest_" + i.ToString(), true).FirstOrDefault() as CheckBox;
                if (fndDest != null)
                {
                    ChbDest[i] = fndDest;
                    ChbDest[i].BackColor = (ChbDest[i].Enabled == true) ? Color.White : Color.Silver;
                }
            }
            lbDestName.Text = ChbDest[nIndex].Text;
            ChbDest[nIndex].BackColor = Color.Aqua;
        }

        private void btnPickup_Click(object sender, EventArgs e)
        {
            if (cbUnitSel.SelectedIndex < 0 || m_nSelectSource < 0) return;

            bool bFail = false;
            ManualUnit Unit = (ManualUnit)m_nSelectSource;
            HAND arm = HAND.LOWER;
            if(cbArmSel.SelectedIndex == 0) 
                arm = HAND.LOWER;
            else
                arm = HAND.UPPER;

            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.NONE;

            switch (cbUnitSel.SelectedIndex)
            {
                case 0:
                    //FM Robot
                    GlobalVariable.manualInfo.SelArmFM = arm;
                    GlobalVariable.manualInfo.nSlotSource = Convert.ToInt32(cbSlotSource.Text);
                    switch (Unit)
                    {
                        case ManualUnit.LPMA:
                            GlobalVariable.manualInfo.mnlStageFM = FMStage.LPMA;
                            Manul_Seq = ManualRun.MANUAL_SEQ.FMRobot_Pickup_LPM;
                            break;
                        case ManualUnit.LPMB:
                            GlobalVariable.manualInfo.mnlStageFM = FMStage.LPMB;
                            Manul_Seq = ManualRun.MANUAL_SEQ.FMRobot_Pickup_LPM;
                            break;
                        case ManualUnit.LPMC:
                            GlobalVariable.manualInfo.mnlStageFM = FMStage.LPMC;
                            Manul_Seq = ManualRun.MANUAL_SEQ.FMRobot_Pickup_LPM;
                            break;
                        case ManualUnit.LPMD:
                            GlobalVariable.manualInfo.mnlStageFM = FMStage.LPMD;
                            Manul_Seq = ManualRun.MANUAL_SEQ.FMRobot_Pickup_LPM;
                            break;
                        case ManualUnit.CP1:
                            GlobalVariable.manualInfo.mnlStageFM = FMStage.CP1;
                            Manul_Seq = ManualRun.MANUAL_SEQ.FMRobot_Pickup_Buffer;
                            break;
                        case ManualUnit.CP2:
                            GlobalVariable.manualInfo.mnlStageFM = FMStage.CP2;
                            Manul_Seq = ManualRun.MANUAL_SEQ.FMRobot_Pickup_Buffer;
                            break;
                        case ManualUnit.CP3:
                            GlobalVariable.manualInfo.mnlStageFM = FMStage.CP3;
                            Manul_Seq = ManualRun.MANUAL_SEQ.FMRobot_Pickup_Buffer;
                            break;
                        default:
                            bFail = true;
                            break;                                              
                    }
                    break;

                case 1:
                    //ATM Robot
                    GlobalVariable.manualInfo.SelArmATM = arm;
                    GlobalVariable.manualInfo.nSlotSource = Convert.ToInt32(cbSlotSource.Text);
                    switch (Unit)
                    {
                        case ManualUnit.AL:
                            GlobalVariable.manualInfo.mnlStageATM = AtmStage.ALIGN;
                            Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_Pickup_Buffer;
                            break;
                        case ManualUnit.HP:
                            GlobalVariable.manualInfo.mnlStageATM = AtmStage.HP;
                            Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_Pickup_Buffer;
                            break;
                        case ManualUnit.LAMI:
                            GlobalVariable.manualInfo.mnlStageATM = AtmStage.LAMI_ULD;
                            Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_Unload_Lami;
                            break;
                        case ManualUnit.LL_BD:
                            GlobalVariable.manualInfo.mnlStageATM = AtmStage.BD;
                            Manul_Seq = ManualRun.MANUAL_SEQ.ATMRobot_Place_Loadlock;
                            break;
                        default:
                            bFail = true;
                            break;
                    }
                    break;

                case 2:
                    //VTM Robot
                    GlobalVariable.manualInfo.SelArmVTM = arm;
                    GlobalVariable.manualInfo.nSlotSource = Convert.ToInt32(cbSlotSource.Text);
                    switch (Unit)
                    {
                        case ManualUnit.LL1_DIV:
                            GlobalVariable.manualInfo.mnlStageVTM = VtmStage.LL;
                            Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Pickup_Loadlock;
                            break;
                        case ManualUnit.LL2_CARR:
                            GlobalVariable.manualInfo.mnlStageVTM = VtmStage.LL;
                            Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Pickup_Loadlock;
                            break;
                        case ManualUnit.PMC:
                            GlobalVariable.manualInfo.mnlStageVTM = VtmStage.PMC1_ULD;
                            Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_Unload_PMC;
                            break;
                        default:
                            bFail = true;
                            break;
                    }
                    break;

                default:
                    bFail = true;
                    break;

                    //매뉴얼 동작 스타트
                    GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
            }
        }

        private void btnPlace_Click(object sender, EventArgs e)
        {

        }

        private void btnCycleMove_Click(object sender, EventArgs e)
        {

        }

        private void btnPmcSetRecipe_Click(object sender, EventArgs e)
        {
            ManualRun.MANUAL_SEQ Manul_Seq = ManualRun.MANUAL_SEQ.VTMRobot_SetRecipe_PMC;

            GlobalVariable.manualInfo.SelArmVTM = (HAND)cbVtmRobotArm.SelectedIndex + 1;

            GlobalSeq.manualRun.StartManualSeq(Manul_Seq);
        }
    }
}
