using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBDB_Handler.GLOBAL;
using TBDB_CTC.LOG.VSLogger;
using System.Data;
using System.Windows.Forms;

namespace TBDB_CTC.GLOBAL
{
    public class GlobalFunction
    {
        public delegate void MyEventHandler();
        public static event MyEventHandler DataTableChanged;
        static GlobalFunction()
        {
               DataTableChanged += new MyEventHandler(asdf);
        }
        public static void asdf() { MessageBox.Show("글로벌이벤트발생함"); }
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

        public static void SetLogToDataTable(Eidentify index, string content)
        {
            //indexform 정보를 맞게 변경해야됨; 스트링값도 받아야함
            //globaldefine 에 폼정보박아놓고 매개변수로받아 식별하게만듬
            //폼별로 받는정보가다름, 정보를 어캐처리하지 생각해야됨

            DataRow dr = GlobalDataSet.dataset.Tables[(int)index].NewRow();
            dr["No"] = "123";
            GlobalDataSet.dataset.Tables[(int)index].Rows.Add(dr);

           DataTableChanged(); //데이터 변동 이벤트
        }//DataTable에 해당Content 기록

    }
}
