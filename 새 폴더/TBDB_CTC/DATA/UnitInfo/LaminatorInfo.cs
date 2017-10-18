using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBDB_CTC.Comm.Lami_PLC;

namespace TBDB_Handler.DATA.UnitInfo
{
    public class LaminatorInfo
    {
        public LaminatorInfo()
		{
		}

        private Lami_Plc_Ethernet _Laminator = new Lami_Plc_Ethernet();
        public Lami_Plc_Ethernet Laminator
        {
            get { return _Laminator; }
        }
    }

}
