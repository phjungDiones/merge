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

        //에러셋
        public void SetErr(string content, GlobalDefine.Eidentify_error e)
        {
            string ret ="";
            if (GlobalDefine.Eidentify_error.CyborgRobot_HTR == e)
            {
                if (content.Contains(':'))
                {
                    string[] s = content.Split(':');
                    string[] ff = s[1].Split('*');
                    ret = ff[0];
                }
                else
                {
                    ret = content;
                }
            }
            if (GlobalDefine.Eidentify_error.Nano300 == e)
            {
                if (content.Contains('E'))
                {
                    string[] f = content.Split('E');
                    ret = f[1];
                }
                else if (content.Contains("NAK"))
                {
                    content = "3";
                }
                else if (content.Contains("Before Comand Processing."))
                {
                    content = "130";
                }
            }
            if(GlobalDefine.Eidentify_error.Aligner_PA300C == e)
            {
                if (content.Contains('#'))
                {
                    string[] f = content.Split('#');
                    ret = f[1];
                }
                else
                {
                    ret = content;
                }
            }
            if (GlobalDefine.Eidentify_error.Nano300 == e)
            {
                if (content.Contains("_ERR"))
                {
                    string[] f = content.Split(' ');
                    ret = f[1];
                }
                else if (content.Contains("_NAK"))
                {
                    content = "13";
                }

                ret = content;
            }
            Global.GlobalDefine.Instance.sRcvData = ret;
            Global.GlobalDefine.Instance.robot_index = (int)e;
            //CyborgRobot_HTR = 0,
            //Nano300 = 1,
            //Aligner_PA300C = 2,
            //CyMechsRobot = 3,

            GlobalEvent.Instance.SetGetErrorEvent(); // 이벤트셋
        }



      
    }
}
