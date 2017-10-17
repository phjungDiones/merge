using System;
using System.Windows.Forms;


namespace CJ_Controls.Communication.Error.Global
{
    public sealed class GlobalVariable
    {
        private static volatile GlobalVariable instance;
        private static object syncRoot = new Object();
        public ErrorMessageStruct[] ErrorList;

        private GlobalVariable() { ErrorCount = 0; ErrorList = new ErrorMessageStruct[1000]; }//에러이벤트 
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

        public delegate void MyEventHandler();

        public event MyEventHandler Click;

        public void ErrMessageFormShowEvent() {  Click(); }

        






    }


}
