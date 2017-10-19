using System;

namespace TBDB_CTC.POPWND.Error.Test
{
    public class ErrorPopUpmaMassage
    {
        private static volatile ErrorPopUpmaMassage instance;
        private static object syncRoot = new Object();

        private ErrorPopUpmaMassage() {}
        public static ErrorPopUpmaMassage Instance
        {
            get
            {
               
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ErrorPopUpmaMassage();
                    }
                }

                return instance;
            }
        }

        public bool mbClearError;
        public string mErrorNumber;
        public string mErrorMessage;
        public string mErrorCause;
        public string mErrorAction;
        public int mErrorCount;
        public string mErrorCauseTime;
        public string mErrorEndTime;
        public string mErrorTarget;
    }
}


