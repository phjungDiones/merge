using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ_Controls.Communication.Global
{
    public class GlobalDefine
    {

        public enum Eidentify_error
        {
            CyborgRobot_HTR = 0,
            Nano300 = 1,
            Aligner_PA300C = 2,
            CyMechsRobot = 3,
        }

        public string sRcvData;
        public int robot_index;

        public bool Status_Aligner_PA300C_b = false;

        private static volatile GlobalDefine instance;
        private static object syncRoot = new Object();

        public static GlobalDefine Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GlobalDefine();
                    }
                }

                return instance;
            }
        }


    }
}
