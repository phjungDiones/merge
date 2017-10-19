using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ_Controls.Communication.Test
{
    public class GlobalEvent
    {
        public delegate void MyEventHandler();
        public static event MyEventHandler GetErrorEvent;
        public void SetGetErrorEvent() { GetErrorEvent(); }

        private static volatile GlobalEvent instance;
        private static object syncRoot = new Object();

        protected GlobalEvent()
        {

        }

        public static GlobalEvent Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GlobalEvent();
                    }
                }

                return instance;
            }
        }
    }
}
