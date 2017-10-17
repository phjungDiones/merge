using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBDB_Handler.GLOBAL;
using TBDB_CTC.LOG.VSLogger;

namespace TBDB_CTC.GLOBAL
{
    public class GlobalFunction
    {

        public static void WriteDebugging(string strSection, string strStep, string strMsg)
        {
            FileLogger log = new FileLogger();

            string strLogWrite = "";
            string strLogin = "";
            string strSplit = ", ";

            strLogin = "OPERATOR";
            strLogWrite += DateTime.Now.ToString("hh:mm:ss.fff");
            strLogWrite += strSplit;

            //strLogWrite += strLogin;
            //strLogWrite += strSplit;

            strLogWrite += strSection;
            strLogWrite += strSplit;

            strLogWrite += strStep;
            strLogWrite += strSplit;
           
            strLogWrite += strMsg;

            //log.Log(strLogWrite, log.GetDirName(PathManager.Instance.PATH_LOG_DEBUG));
            log.Log(strLogWrite, PathManager.Instance.PATH_LOG_DEBUG);
        }

    }
}
