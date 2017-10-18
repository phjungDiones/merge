using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CJ_Controls.Communication.Global
{
    public class GlobalFunction
    {
        private static volatile GlobalFunction instance;
        private static object syncRoot = new Object();
        protected GlobalFunction()
    {
            
    }


     public static GlobalFunction Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GlobalFunction();
                    }
                }

                return instance;
            }
        }



        public void GetErrNumber_0(string content,GlobalDefine.Eidentify_error e)
        {
            string ret;
            if (content.Contains(':'))
            {
                string[] s = content.Split(':');
                ret = s[0];
            }
            else
            {
                ret = content;
            }
            Global.GlobalDefine.Instance.sRcvData = ret;
            Global.GlobalDefine.Instance.robot_index = (int)e;
            //CyborgRobot_HTR = 0,
            //Nano300 = 1,
            //Aligner_PA300C = 2,
            //CyMechsRobot = 3,

          //  GlobalEvent.Instance.SetGetErrorEvent(); // 이벤트셋
        }
        public void GetErrNumber_1(string content, GlobalDefine.Eidentify_error e)
        {
            string ret;
            if (content.Contains('E'))
            {
                string[] f = content.Split('E');
                ret = f[1];
            }
            else
            {
                ret = content;
            }
            ret = content;
            Global.GlobalDefine.Instance.sRcvData = ret;
            Global.GlobalDefine.Instance.robot_index = (int)e;
           // GlobalEvent.Instance.SetGetErrorEvent();
        }
        public void GetErrNumber_2(string content, GlobalDefine.Eidentify_error e)
        {
            string ret;
            if (content.Contains('#'))
            {
                string[] f = content.Split('#');
                ret = f[1];
            }
            else
            {
                ret = content;
            }
            ret = content;
            Global.GlobalDefine.Instance.sRcvData = ret;
            Global.GlobalDefine.Instance.robot_index = (int)e;
         //   GlobalEvent.Instance.SetGetErrorEvent();
        }
        public void GetErrNumber_3(string content, GlobalDefine.Eidentify_error e)
        {
            string ret;
            if (content.Contains("_ERR"))
            {
                string[] f = content.Split(' ');
                ret = f[1];
            }
            else
            {
                ret = content;
            }
            ret = content;
            Global.GlobalDefine.Instance.sRcvData = ret;
            Global.GlobalDefine.Instance.robot_index = (int)e;
         //   GlobalEvent.Instance.SetGetErrorEvent();
        }

      
    }
}
