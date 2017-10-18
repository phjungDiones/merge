using CJ_Controls.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBDB_Handler.DATA.UnitInfo
{
    public class PmcInfo
    {
        public PmcInfo()
        {

        }

        private COM_Melsec _Pmc = new COM_Melsec();
        public COM_Melsec Pmc
		{
            get { return _Pmc; }
		}


    }
}
