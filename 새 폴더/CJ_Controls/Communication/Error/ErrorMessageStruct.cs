using System;
using System.Reflection;
using System.Windows.Forms;

namespace CJ_Controls.Communication.Error
{
    public class ErrorMessageStruct
    {
        public bool mbClearError { get; set; } //에러 클리어 여부

        public string mErrorNumber { get; private set; }
        public string mErrorMessage { get; private set; }
        public string mErrorCause { get; private set; }
        public string mErrorAction { get; private set; }
        public int mErrorCount { get; private set; }
        public string mErrorCauseTime { get; private set; }
        public string mErrorEndTime { get; private set; }




        //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")
        public ErrorMessageStruct(string FuncName, string ErrorNumber)
        {
            mbClearError = false;
            mErrorNumber = ErrorNumber;
            mErrorCount = Global.GlobalVariable.Instance.ErrorCount;
            mErrorCauseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");


            if (FuncName == "CyborgRobot_HTR_TM")
            {

                mErrorMessage = Global.GlobalErrorMessage.Instance._CyborgRobot_HTR[Int32.Parse(mErrorNumber)].message;
                        mErrorCause = Global.GlobalErrorMessage.Instance._CyborgRobot_HTR[Int32.Parse(mErrorNumber)].cause;
                mErrorAction = Global.GlobalErrorMessage.Instance._CyborgRobot_HTR[Int32.Parse(mErrorNumber)].action;

            }
            if (FuncName == "Nano300")
            {
                switch (Int32.Parse(mErrorNumber))
                {
                    case 3:
                        mErrorMessage = "Nano300 Invalid Data Error";
                        mErrorCause = "Nano300 Data 형식 이상 있음";
                        mErrorAction = "Nano300 Data 확인 ( Software / Hardware )";
                        break;

                }

            }
            if (FuncName == "Aligner_PA300C")
            {
                switch (Int32.Parse(mErrorNumber))
                {
                    case 1:
                        mErrorMessage = "Aligner_PA300C Invalid command error";
                        mErrorCause = "Aligner_PA300C none";
                        mErrorAction = "Aligner_PA300C none";
                        break;

                }

            }
            if (FuncName == "CyMechsRobot")
            {
                switch (Int32.Parse(mErrorNumber))
                {
                    case 8:
                        mErrorMessage = "CyMechsRobot Command is not correct";
                        mErrorCause = "CyMechsRobot none";
                        mErrorAction = "CyMechsRobot none";
                        break;

                }

            }

            Global.ErrorPopUpmaMassage.Instance.mbClearError = mbClearError;
            Global.ErrorPopUpmaMassage.Instance.mErrorAction = mErrorAction;
            Global.ErrorPopUpmaMassage.Instance.mErrorCause = mErrorCause;
            Global.ErrorPopUpmaMassage.Instance.mErrorCauseTime = mErrorCauseTime;
            Global.ErrorPopUpmaMassage.Instance.mErrorCount = mErrorCount;
            Global.ErrorPopUpmaMassage.Instance.mErrorEndTime =mErrorEndTime;
            Global.ErrorPopUpmaMassage.Instance.mErrorMessage =mErrorMessage;
            Global.ErrorPopUpmaMassage.Instance.mErrorNumber = mErrorNumber;

        }


        public void ResetError()
        {
            mbClearError = true;
            mErrorEndTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }


}
