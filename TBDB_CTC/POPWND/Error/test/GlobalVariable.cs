using System;
using System.Windows.Forms;
using System.IO;
using TBDB_Handler.GLOBAL;
using System.Data;


namespace TBDB_CTC.POPWND.Error.Test
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
           

            DataRow newDr;
            MessageBox.Show(temp.mErrorCauseTime.ToString());
           
            newDr = GlobalDataSet.dataset2.Tables[0].NewRow();

            newDr["index"] = GlobalError.nIndex;
            GlobalError.nIndex++;
            newDr["Model"] = temp.mErrorTarget;
            newDr["No"] = temp.mErrorNumber;
            newDr["AlarmDescription"] = temp.mErrorCause;
            newDr["DateTime"] = temp.mErrorCauseTime;
            
            GlobalDataSet.dataset2.Tables[0].Rows.Add(newDr);
            GlobalDataSet.dataset2.WriteXml("AllErrorTables.xml");

            newDr = GlobalDataSet.dataset3.Tables[0].NewRow();

            newDr["index"] = GlobalError.nIndex;
            GlobalError.nIndex++;
            newDr["Model"] = temp.mErrorTarget;
            newDr["No"] = temp.mErrorNumber;
            newDr["AlarmDescription"] = temp.mErrorCause;
            newDr["DateTime"] = temp.mErrorCauseTime;

            GlobalDataSet.dataset3.Tables[0].Rows.Add(newDr);
            GlobalDataSet.dataset3.WriteXml("AllErrorTables_clone.xml");



        }

        private GlobalVariable() { ErrorCount = 0; ErrorList = new ErrorMessageStruct[1000]; }//에러이벤트 

















    }


}
