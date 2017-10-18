using System;
using System.Reflection;
using System.Windows.Forms;
using TBDB_Handler.GLOBAL;
using System.IO;

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
        public string mErrorTarget { get; private set; }




        //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")
        public ErrorMessageStruct(Eidentify_error e, string ErrorNumber)
        {
            int index = (int)e; //오류번호

            //CyborgRobot_HTR = 0,
            //Nano300 = 1,
            //Aligner_PA300C = 2,
            //CyMechsRobot = 3,


            int errNumber =0; 
            Int32.TryParse(ErrorNumber, out errNumber);

            if (errNumber == 0)
            {
                mErrorTarget = e.ToString();
                mbClearError = false;
                mErrorCount = Global.GlobalVariable.Instance.ErrorCount;
                
                mErrorCauseTime = DateTime.Now.ToString("yyyy-MM-dd")+ " "+DateTime.Now.ToString("HH:mm:ss");
                mErrorNumber = ErrorNumber;

                Global.ErrorPopUpmaMassage.Instance.mErrorNumber = ErrorNumber;
                Global.ErrorPopUpmaMassage.Instance.mbClearError = mbClearError;
                Global.ErrorPopUpmaMassage.Instance.mErrorAction = "알수없는 에러";
                Global.ErrorPopUpmaMassage.Instance.mErrorCause = "알수없는 에러";
                Global.ErrorPopUpmaMassage.Instance.mErrorCauseTime = mErrorCauseTime;
                Global.ErrorPopUpmaMassage.Instance.mErrorCount = mErrorCount;
                Global.ErrorPopUpmaMassage.Instance.mErrorEndTime = mErrorEndTime;
                Global.ErrorPopUpmaMassage.Instance.mErrorMessage = "알수없는 에러";
                Global.ErrorPopUpmaMassage.Instance.mErrorTarget = mErrorTarget;
                return;
                //숫자변경못햇을시
                //에러남기거나 무언가해야함
            }


            string errorNumber = string.Format("{0:D5}", errNumber);

            string Path_CyborgRobot_HTR = @"ErrorList\CyborgRobot_HTR.txt"; //ok
            string Path_Nano300 = @"ErrorList\Nano300.txt";//ok
            string Path_Aligner_PA300C = @"ErrorList\Aligner_PA300C.txt";//ok
            string Path_CyMechsRobot = @"ErrorList\CyMechsRobot.txt";//ok

            string[] pathArray = { Path_CyborgRobot_HTR, Path_Nano300, Path_Aligner_PA300C, Path_CyMechsRobot };

            string errFilePath = pathArray[index]; // 사용할 파일 path

            string[] temp = new string[4];
            string message = "";
            string cause = "";
            string action = "";
            string[] StrArray_ErrorList = null;

            if (File.Exists(errFilePath)) // 파일이존재한다면
            {
                try //파읽있는중 혹시나 오류생길 수 있음
                {
                    StrArray_ErrorList = File.ReadAllLines(errFilePath);
                }
                catch (Exception ex)
                {
                    //로그남겨야함
                }

                if (StrArray_ErrorList == null) return; //파일 못읽었다면 그냥 리턴
                //로그남겨야함
                //어떤 오류창이라도 띄워야함,

                foreach (var i in StrArray_ErrorList)
                {
                    //ErrorNumber 포맷에 맞게 변경해야함 -> 00001
                    if (i.Contains(errorNumber)) // 에러번호 //ErrorString -> 번호,message,cause,action 순으로 이루어짐
                    {

                        temp = i.Split(',');
                        message = temp[1];
                        cause = temp[2];
                        action = temp[3];
                        break;
                    }
                    if (!i.Contains(errorNumber))
                    {
                        temp = i.Split(',');
                        message = "정의되지않은에러";
                        cause = "정의되지않은에러";
                        action = "정의되지않은에러";
                    }
                    if (e==0&&errNumber<0&& errNumber > -4101) //CyborgRobot_HTR 이고 해당범위라면
                    {//PA Controller Error

                        if(errNumber<= -200 && errNumber >= -299) // Standard System Errors
                        {
                            message = "Standard System Errors";
                        }
                        if (errNumber <= -300 && errNumber >= -499) // Hardware Device Related Errors
                        {
                            message = "Hardware Device Related Errors";
                        }
                        if (errNumber <= -500 && errNumber >= -699) // Input and Output Errors

                        {
                            message = "nput and Output Errors";
                        }
                        if (errNumber <= -700 && errNumber >= -999) // Language Related Errors
                        {
                            message = "Language Related Errors";
                        }
                        if (errNumber <= -1000 && errNumber >= -1499) // Robot Related Errors
                        {
                            message = "Robot Related Errors";
                        }
                        if (errNumber <= -1500 && errNumber >= -1699) // Configuration Parameter Database, Datalogger, and CPU Monitor Errors
                        {
                            message = "Configuration Parameter Database, Datalogger, and CPU Monitor Errors";
                        }
                        if (errNumber <= -1500 && errNumber >= -1699) // Controller Errors
                        {
                            message = "Controller Errors";
                        }
                        if (errNumber <= -1700 && errNumber >= -1799) // Network, Socket, and Communication Errors
                        {
                            message = "Network, Socket, and Communication Errors";
                        }
                        if (errNumber <= -3000 && errNumber >= -3999) // Servo Related Errors
                        {
                            message = "Servo Related Errors";
                        }
                        if (errNumber <= -4000 && errNumber >= -4100) // Vision Related Errors
                        {
                            message = "Vision Related Errors";
                        }
                        cause = "PA Controller 관련 에러 입니다.";
                        action = "PA Controller 를 확인하십시오.";
                        break;
                    }
                }


            }

            mErrorTarget = e.ToString();
            mbClearError = false;
            mErrorNumber = errorNumber;
            mErrorCount = Global.GlobalVariable.Instance.ErrorCount;
            mErrorCauseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");




            mErrorMessage = message;
            mErrorCause = cause;
            mErrorAction = action;


            Global.ErrorPopUpmaMassage.Instance.mbClearError = mbClearError;
            Global.ErrorPopUpmaMassage.Instance.mErrorAction = mErrorAction;
            Global.ErrorPopUpmaMassage.Instance.mErrorCause = mErrorCause;
            Global.ErrorPopUpmaMassage.Instance.mErrorCauseTime = mErrorCauseTime;
            Global.ErrorPopUpmaMassage.Instance.mErrorCount = mErrorCount;
            Global.ErrorPopUpmaMassage.Instance.mErrorEndTime = mErrorEndTime;
            Global.ErrorPopUpmaMassage.Instance.mErrorMessage = mErrorMessage;
            Global.ErrorPopUpmaMassage.Instance.mErrorNumber = mErrorNumber;
            Global.ErrorPopUpmaMassage.Instance.mErrorTarget = mErrorTarget;

        }


        public void ResetError()
        {
            mbClearError = true;
            mErrorEndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }


}
