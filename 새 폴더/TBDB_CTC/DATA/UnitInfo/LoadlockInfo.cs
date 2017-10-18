using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJ_Controls.Communication.EDB2000;

namespace TBDB_Handler.DATA.UnitInfo
{
    public class LoadlockInfo
    {
        public LoadlockInfo()
        {

        }
        private Edb2000 _Loadlock = new Edb2000();
        public Edb2000 Loadlock
        {
            get { return _Loadlock; }
        }
    }


}
