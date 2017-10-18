using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ_Controls.Communication.Error.Global
{
    public class GlobalErrorMessage
    {
        private static volatile GlobalErrorMessage instance;
        private static object syncRoot = new Object();
        public Dictionary<int, Cause_action> _CyborgRobot_HTR = new Dictionary<int, Cause_action>();


         public Dictionary<int, Cause_action> _Nano300 = new Dictionary<int, Cause_action>();

        public Dictionary<int, Cause_action> _AlignerPA300C = new Dictionary<int, Cause_action>();

        public Dictionary<int, Cause_action> _CyMechsRobot = new Dictionary<int, Cause_action>();

        private GlobalErrorMessage() {
            _CyborgRobot_HTR.Add(1, new Cause_action("Illegal command", " 로봇 운영프로그램에 정의되지 않은 Command가 수신되는 경우 발생합니다.","올바른커맨드및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(2, new Cause_action("Wrong number of stage", "입력된 Stage Argument값이 올바르지 않은 경우 발생합니다.", "올바른커맨드및 통신을 확인합니다."));
            _CyborgRobot_HTR.Add(3, new Cause_action("Wrong number of arm", "입력된 Arm Argument값이 올바르지 않은 경우 발생합니다.","올바른커맨드및 통신을 확인합니다."));
        }
        public static GlobalErrorMessage Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GlobalErrorMessage();
                    }
                }

                return instance;
            }
        }











    }
}
