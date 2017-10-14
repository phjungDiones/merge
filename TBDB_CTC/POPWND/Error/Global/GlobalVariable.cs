using System;
using System.Windows.Forms;


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
        public void addErrors(string FuncName, string sErrorNumber)
        {


            ErrorMessageStruct temp = new ErrorMessageStruct(FuncName, sErrorNumber);
            //ErrorList[ErrorCount] = temp;
            ErrorCount++;
            ErrMessageFormShowEvent();

        }

        private GlobalVariable() { ErrorCount = 0; ErrorList = new ErrorMessageStruct[1000]; }//에러이벤트 
















    }


}
