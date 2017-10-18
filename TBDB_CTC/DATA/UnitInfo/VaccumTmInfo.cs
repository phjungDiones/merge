using CJ_Controls.Communication.CybogRobot_HTR;
using CJ_Controls.Communication.QuadraVTM4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBDB_CTC.Data.UnitInfo
{
	public class VaccumTmInfo
	{
		public VaccumTmInfo()
		{
		}

        private CyMechsRobot _Robot = new CyMechsRobot();
        public CyMechsRobot Robot
		{
			get { return _Robot; }
		}
	}
}
