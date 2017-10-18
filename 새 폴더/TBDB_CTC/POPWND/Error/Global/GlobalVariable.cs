using System;
using System.Windows.Forms;
using System.IO;
using TBDB_Handler.GLOBAL;
using System.Data;


namespace TBDB_CTC.POPWND.Error.Global
{
    public sealed class GlobalVariable
    {
        public delegate void MyEventHandler();
        public event MyEventHandler Click;
        public void ErrMessageFormShowEvent() { Click(); }
        public ErrorMessageStruct[] ErrorList;

        private static volatile GlobalVariable instance;
        private static object syncRoot = new Object();


        public static GlobalVariable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GlobalVariable();
                    }
                }

                return instance;
            }
        }
        public int ErrorCount { get; private set; }
        public void SetErr(Eidentify_error e, string sErrorNumber)
        {


            ErrorMessageStruct temp = new ErrorMessageStruct(e, sErrorNumber);
            ErrorList[ErrorCount] = temp;
            //언젠간 데이터 닐라고 리셋해야야됨 기본셋 1000개
            ErrorCount++;
            SetTable(temp);
            ErrMessageFormShowEvent();

        }

        private void SetTable(ErrorMessageStruct temp)
        {
            //올클리어 버튼 누르는 순간 다날리자 배열에있는거 다날리자
            if(GlobalDataSet.dataset2.Tables.Count==0)GlobalDataSet.dataset2.Tables.Add("default");
            DataRow newDr = GlobalDataSet.dataset2.Tables[0].NewRow();
            if (GlobalDataSet.dataset2.Tables[0].Columns.Count == 0)
            {
                GlobalDataSet.dataset2.Tables[0].Columns.Add("index");
                GlobalDataSet.dataset2.Tables[0].Columns.Add("Model");
                GlobalDataSet.dataset2.Tables[0].Columns.Add("No");
                GlobalDataSet.dataset2.Tables[0].Columns.Add("AlarmDescription");
                GlobalDataSet.dataset2.Tables[0].Columns.Add("DateTime");
            }

            newDr["index"] = GlobalError.nIndex;
            GlobalError.nIndex++;
            newDr["Model"] = temp.mErrorTarget;
            newDr["No"] = temp.mErrorNumber;
            newDr["AlarmDescription"] = temp.mErrorCause;
            newDr["DateTime"] = temp.mErrorCauseTime;
            GlobalDataSet.dataset2.Tables[0].Rows.Add(newDr);

            GlobalDataSet.dataset2.WriteXml("AllErrorTables.xml");



        }

        private GlobalVariable() { ErrorCount = 0; ErrorList = new ErrorMessageStruct[1000]; }//에러이벤트 

















    }


}
