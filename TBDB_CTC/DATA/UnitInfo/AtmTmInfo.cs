using CJ_Controls.Communication.CybogRobot_HTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TBDB_CTC.Data.UnitInfo
{
	public class AtmTmInfo
	{

		public AtmTmInfo()
		{
		}

        private CyborgRobot_HTR_TM _Robot = new CyborgRobot_HTR_TM();
        public CyborgRobot_HTR_TM Robot
		{
			get { return _Robot; }
		}

		//private Lami_Plc_Ethernet _Lami = new Lami_Plc_Ethernet();

	}

}
