using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_Handler.GLOBAL;
using TBDB_Handler.MOTION;
using System.Threading;

namespace TBDB_CTC.UserCtrl.SubForm
{
    public partial class subMainScreen : UserControl
    { 
        public subMainScreen()
        {
            InitializeComponent();
        }

        private void subMainScreen_Load(object sender, EventArgs e)
        {
            tmrStatus.Start();
            
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            if (!this.Visible) return;

            //lbWaferLL1.BackColor = (GlobalVariable.WaferInfo.bWaferLL1 == true) ? Color.Blue : Color.Black;
            //lbWaferLL2.BackColor = (GlobalVariable.WaferInfo.bWaferLL2 == true) ? Color.Blue : Color.Black;
            //lbWaferBD.BackColor = (GlobalVariable.WaferInfo.bWaferBD == true) ? Color.Blue : Color.Black;
            //lbWaferAL.BackColor = (GlobalVariable.WaferInfo.bWaferAL == true) ? Color.Blue : Color.Black;
            //lbWaferCP.BackColor = (GlobalVariable.WaferInfo.bWaferCP1 == true) ? Color.Blue : Color.Black;
            //lbWaferLami.BackColor = (GlobalVariable.WaferInfo.bWaferLami == true) ? Color.Blue : Color.Black;
            //lbWaferPMC.BackColor = (GlobalVariable.WaferInfo.bWaferPmc == true) ? Color.Blue : Color.Black;
            //
            ////Robot
            //lbFmUp.BackColor = (GlobalVariable.WaferInfo.bWaferFmUp == true) ? Color.LightGreen : Color.Gray;
            //lbFmLow.BackColor = (GlobalVariable.WaferInfo.bWaferFmLow == true) ? Color.LightGreen : Color.Gray;
            //
            //lbAtmUp.BackColor = (GlobalVariable.WaferInfo.bWaferAtmUp == true) ? Color.LightGreen : Color.Gray;
            //lbAtmLow.BackColor = (GlobalVariable.WaferInfo.bWaferAtmLow == true) ? Color.LightGreen : Color.Gray;
            //
            //lbVtmUp.BackColor = (GlobalVariable.WaferInfo.bWaferVtmUp == true) ? Color.LightGreen : Color.Gray;
            //lbVtmLow.BackColor = (GlobalVariable.WaferInfo.bWaferVtmLow == true) ? Color.LightGreen : Color.Gray;

            

            lbWaferLL1.BackColor = (GlobalVariable.seqShared.IsInLoadLock((int)WaferType.DEVICE) == true) ? Color.Blue : Color.Black;
            lbWaferLL2.BackColor = (GlobalVariable.seqShared.IsInLoadLock((int)WaferType.CARRIER) == true) ? Color.Blue : Color.Black;
            lbWaferBD.BackColor = (GlobalVariable.seqShared.IsInLoadLock((int)WaferType.BONDED) == true) ? Color.Blue : Color.Black;
            lbWaferAL.BackColor = (GlobalVariable.seqShared.IsInAligner() == true) ? Color.Blue : Color.Black;
            lbWaferCP.BackColor = (GlobalVariable.seqShared.IsInCP(0) == true) ? Color.Blue : Color.Black;
            lbWaferLami.BackColor = (GlobalVariable.seqShared.IsInLami() == true) ? Color.Blue : Color.Black;
            lbWaferPMC.BackColor = (GlobalVariable.seqShared.IsInBonder() == true) ? Color.Blue : Color.Black;

            //Robot
            lbFmUp.BackColor = (GlobalVariable.seqShared.IsInFM(HAND.UPPER) == true) ? Color.LightGreen : Color.Gray;
            lbFmLow.BackColor = (GlobalVariable.seqShared.IsInFM(HAND.LOWER) == true) ? Color.LightGreen : Color.Gray;

            lbAtmUp.BackColor = (GlobalVariable.seqShared.IsInATM(HAND.UPPER) == true) ? Color.LightGreen : Color.Gray;
            lbAtmLow.BackColor = (GlobalVariable.seqShared.IsInATM(HAND.LOWER) == true) ? Color.LightGreen : Color.Gray;

            lbVtmUp.BackColor = (GlobalVariable.seqShared.IsInVTM(HAND.UPPER) == true) ? Color.LightGreen : Color.Gray;
            lbVtmLow.BackColor = (GlobalVariable.seqShared.IsInVTM(HAND.LOWER) == true) ? Color.LightGreen : Color.Gray;


            DispWaferInfo(); //Wafer Mapping Data


            ////GlobalVariable.seqShared.Init(20);
            //GlobalVariable.seqShared.LoadingCarrierToFm(EFEM_TYPE.A_CARRIER, 0);
            //GlobalVariable.seqShared.robotFM.PreAlign();
            //GlobalVariable.seqShared.Save();
        }

        public void DispWaferInfo()
        {
            uctrlPortSlot1.strName= "LPM A";
            uctrlPortSlot2.strName = "LPM B";
            uctrlPortSlot3.strName = "LPM C";
            uctrlPortSlot4.strName = "LPM D";

//             for(int i=0; i<25; i++)
//             {
//                 uctrlPortSlot1.Slot = i;
//                 uctrlPortSlot1.bStatus = GlobalSeq.autoRun.prcFM.LpmRoot.GetWaferInfo(EFEM.LPMA, i, ScanMode.Exist);
// 
//                 uctrlPortSlot2.Slot = i;
//                 uctrlPortSlot2.bStatus = GlobalSeq.autoRun.prcFM.LpmRoot.GetWaferInfo(EFEM.LPMB, i, ScanMode.Exist);
// 
//                 uctrlPortSlot3.Slot = i;
//                 uctrlPortSlot3.bStatus = GlobalSeq.autoRun.prcFM.LpmRoot.GetWaferInfo(EFEM.LPMC, i, ScanMode.Exist);
// 
//                 uctrlPortSlot4.Slot = i;
//                 uctrlPortSlot4.bStatus = GlobalSeq.autoRun.prcFM.LpmRoot.GetWaferInfo(EFEM.LPMD, i, ScanMode.Exist);
//             }

            for (int i = 0; i < 25; i++)
            {
                uctrlPortSlot1.Slot = i;
                uctrlPortSlot1.LpmWaferStatus = GlobalSeq.autoRun.prcFM.LpmRoot.GetLPMWaferStauts(EFEM.LPMA, i);

                uctrlPortSlot2.Slot = i;
                uctrlPortSlot2.LpmWaferStatus = GlobalSeq.autoRun.prcFM.LpmRoot.GetLPMWaferStauts(EFEM.LPMB, i);

                uctrlPortSlot3.Slot = i;
                uctrlPortSlot3.LpmWaferStatus = GlobalSeq.autoRun.prcFM.LpmRoot.GetLPMWaferStauts(EFEM.LPMC, i);

                uctrlPortSlot4.Slot = i;
                uctrlPortSlot4.LpmWaferStatus = GlobalSeq.autoRun.prcFM.LpmRoot.GetLPMWaferStauts(EFEM.LPMD, i);
            }


            
        }

        private void btnMapping_Click(object sender, EventArgs e)
        {
            int nMaxLpm = 4;
            int nMaxCount = 25;
            bool bWafer = false;

            //테스트
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMA, 0, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMA, 4, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMA, 9, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMA, 19, true);
// 
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMB, 1, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMB, 6, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMB, 11, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMB, 21, true);
// 
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMC, 3, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMC, 7, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMC, 13, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMC, 23, true);
// 
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMD, 8, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMD, 9, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMD, 17, true);
//             GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo(EFEM.LPMD, 18, true);

            for (int lpm = 0; lpm < nMaxLpm; lpm++) 
            {
                for (int i = 0; i < nMaxCount; i++)
                {
                    GlobalSeq.autoRun.prcFM.LpmRoot.SetWaferInfo((EFEM)lpm, i, true);

                    bWafer = GlobalSeq.autoRun.prcFM.LpmRoot.GetWaferInfo((EFEM)lpm, i, ScanMode.Exist);
                    Console.WriteLine(string.Format("LPM : {0}, Slot : {1},  {2}", lpm, i, bWafer));
                }
            }
        }

        private void btnLamiLoad_Click(object sender, EventArgs e)
        {
            GlobalVariable.WaferInfo.bLamiUnload = false;
            GlobalVariable.WaferInfo.bLamiLoad = true;

            Thread.Sleep(500);

            GlobalVariable.WaferInfo.bLamiLoad = false;
        }

        private void btnLamiUnload_Click(object sender, EventArgs e)
        {
            GlobalVariable.WaferInfo.bLamiLoad = false;
            GlobalVariable.WaferInfo.bLamiUnload = true;
            Thread.Sleep(500);
            GlobalVariable.WaferInfo.bLamiUnload = false;
        }

        private void btnLamiReset_Click(object sender, EventArgs e)
        {
            GlobalVariable.WaferInfo.bLamiLoad = false;
            GlobalVariable.WaferInfo.bLamiUnload = false;
        }

        private void btnPmcLoad_Click(object sender, EventArgs e)
        {
            GlobalVariable.WaferInfo.bPmcUnload = false;
            GlobalVariable.WaferInfo.bPmcLoad = true;
            Thread.Sleep(500);
            GlobalVariable.WaferInfo.bPmcLoad = false;
        }

        private void btnPmcUnload_Click(object sender, EventArgs e)
        {
            GlobalVariable.WaferInfo.bPmcLoad = false;
            GlobalVariable.WaferInfo.bPmcUnload = true;
            Thread.Sleep(500);
            GlobalVariable.WaferInfo.bPmcUnload = false;
        }

        private void btnPmcReset_Click(object sender, EventArgs e)
        {
            GlobalVariable.WaferInfo.bPmcLoad = false;
            GlobalVariable.WaferInfo.bPmcUnload = false;
        }
    }
}
