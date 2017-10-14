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
using TBDB_CTC.GLOBAL;
using TBDB_CTC.Data;
using CJ_Controls;

namespace TBDB_CTC.UserCtrl.SubForm.MainSub
{
    public partial class subMainStatus : UserControl
    {
        private MainData _Main = MainData.Instance;

        private int nLogCount = 0;

        public subMainStatus()
        {
            InitializeComponent();

            //Com log
            
        }

        private void subMainStatus_Load(object sender, EventArgs e)
        {
            GlobalSeq.autoRun.prcATM.procMsgEvent += AddProcMessage;
            GlobalSeq.autoRun.prcVTM.procMsgEvent += AddProcMessage;
            GlobalSeq.autoRun.prcFM.procMsgEvent += AddProcMessage;
            GlobalSeq.autoRun.procLoadlock.procMsgEvent += AddProcMessage;
            GlobalSeq.autoRun.prcAL.procMsgEvent += AddProcMessage;

            //_Main.GetLoaderData().Aligner.procMsgEvent += AddComMessage;
            //_Main.GetLoaderData().Robot.MessageEvent += AddComMessage; //FM
        }

        //Com Message
        void AddComMessage(string strAddMessage)
        {
            string strSection = "COM";

            int nSeq = 0;

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    GlobalFunction.WriteDebugging(strSection, nSeq.ToString(), strAddMessage);
                });
            }
            else
            {
                GlobalFunction.WriteDebugging(strSection, nSeq.ToString(), strAddMessage);
            }
        }

        void AddProcMessage(int nProc, int nSeq, string sAddMessage)
        {
            string strSection = " ";

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    dgvSeqLog.RowCount++;
                    dgvSeqLog.Rows[nLogCount].Cells[0].Value = DateTime.Now.ToString("yy-MM-dd [HH:mm:ss.fff]");
                    dgvSeqLog.Rows[nLogCount].Cells[1].Value = nProc.ToString();
                    dgvSeqLog.Rows[nLogCount].Cells[2].Value = nSeq.ToString();
                    dgvSeqLog.Rows[nLogCount].Cells[3].Value = sAddMessage;
                    GlobalFunction.WriteDebugging(strSection, nSeq.ToString(), sAddMessage);

                    nLogCount++;
                });
            }
            else
            {
                GlobalFunction.WriteDebugging(strSection, nSeq.ToString(), sAddMessage);

                dgvSeqLog.RowCount++;
                dgvSeqLog.Rows[nLogCount].Cells[0].Value = DateTime.Now.ToString("yy-MM-dd [HH:mm:ss.fff]");
                dgvSeqLog.Rows[nLogCount].Cells[1].Value = nProc.ToString();
                dgvSeqLog.Rows[nLogCount].Cells[2].Value = nSeq.ToString();
                dgvSeqLog.Rows[nLogCount].Cells[3].Value = sAddMessage;
            }
        }

    }
}
