using System;
using System.Reflection;
using System.Windows.Forms;

namespace TBDB_CTC.POPWND.Error
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
                int errorN=-77777; //이값이라면 변환을 재대로 못한거임

                Int32.TryParse(ErrorNumber, out errorN);

                if (errorN != -77777)//r값이 재대로 변환 됬다면
                {

                    if (errorN >= 1 && errorN <= 1001) // 로봇 시스템 에러 범위
                    {
                        mErrorMessage = Global.GlobalErrorMessage.Instance._CyborgRobot_HTR[Int32.Parse(mErrorNumber)].message;
                        mErrorCause = Global.GlobalErrorMessage.Instance._CyborgRobot_HTR[Int32.Parse(mErrorNumber)].cause;
                        mErrorAction = Global.GlobalErrorMessage.Instance._CyborgRobot_HTR[Int32.Parse(mErrorNumber)].action;
                    }else if(errorN<=-1&& errorN >= -4100) // PA Controller Error (Error Reset 필요)
                    {

                    }
                    //값범위체크하고
                    //범위별로 나눠서 넣을 메시지 넣어주고

                    //값이 존재하는지 확인하고
                    //존재한다면 다음코드실행
                    //존재하지 않는다면 수동으로입력
                }
                else
                {
                    //값변환 실패
                }
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
            Global.ErrorPopUpmaMassage.Instance.mErrorEndTime = mErrorEndTime;
            Global.ErrorPopUpmaMassage.Instance.mErrorMessage = mErrorMessage;
            Global.ErrorPopUpmaMassage.Instance.mErrorNumber = mErrorNumber;

        }


        public void ResetError()
        {
            mbClearError = true;
            mErrorEndTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }


}
