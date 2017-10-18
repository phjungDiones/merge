using System;
using System.Windows.Forms;
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.POPWND
{
    public partial class popErrMessage : Form
    {
        TBDB_CTC.POPWND.Error.Global.ErrorPopUpmaMassage a = TBDB_CTC.POPWND.Error.Global.ErrorPopUpmaMassage.Instance;
        TBDB_CTC.POPWND.Error.Global.GlobalVariable b = TBDB_CTC.POPWND.Error.Global.GlobalVariable.Instance;
        
        

        public popErrMessage()
        {
            
            InitializeComponent();
        }

        private void popErrMessage_Load(object sender, EventArgs e)
        {
            this.lblErrNo.Text = a.mErrorNumber.ToString();
            this.lblTitle.Text = a.mErrorMessage;
            this.lblCause.Text = a.mErrorCause;
            this.lblAct1.Text = a.mErrorAction;
            this.lblCnt.Text = "None";
            this.lblTime.DigitText = a.mErrorCauseTime;
            this.lblTarget.Text = a.mErrorTarget;

            b.Click += delegate ()
            {
                this.lblTarget.Text = a.mErrorTarget;
                this.lblErrNo.Text = a.mErrorNumber.ToString();
                this.lblTitle.Text = a.mErrorMessage;
                this.lblCause.Text = a.mErrorCause;
                this.lblAct1.Text = a.mErrorAction;
                this.lblCnt.Text = "None";
                this.lblTime.DigitText = a.mErrorCauseTime;
            };

        }
 

        private void btnOk_Click(object sender, EventArgs e)
        {
            //b.ErrorList[b.ErrorCount-1].mbClearError = true;
            a.mbClearError = true;
            GlobalForm.fErr.Hide();

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
