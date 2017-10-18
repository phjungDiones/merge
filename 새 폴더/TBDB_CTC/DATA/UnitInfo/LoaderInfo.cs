using CJ_Controls.Communication.CybogRobot_HTR;
using CJ_Controls.Communication.PA300C;
using CJ_Controls.Communication.QuadraVTM4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBDB_CTC.Data.UnitInfo
{
	public class LoaderInfo : BaseUnitInfo
	{
		public LoaderInfo(int nUnitNo)
			: base(nUnitNo)
		{
			for (int nPort = 0; nPort < COUNT.MAX_PORT; nPort++)
			{
				_PortData[nPort] = new PortInfo(nPort + 1);
			}
		}

		private PortInfo[] _PortData = new PortInfo[COUNT.MAX_PORT];
		public PortInfo[] PortData
		{
			get { return _PortData; }
			set { _PortData = value; }
		}

		public PortInfo GetPortData(int nPort_0Base)
		{
			if (nPort_0Base < 0)
			{
				return null;
			}
			return _PortData[nPort_0Base];
		}

		private CyborgRobot_HTR m_FM_Robot = new CyborgRobot_HTR();
		public CyborgRobot_HTR Robot
		{
			get { return m_FM_Robot; }
		}


        private Aligner_PA300C m_Aligner = new Aligner_PA300C();
        public Aligner_PA300C Aligner
        {
            get { return m_Aligner;  }
        }




	}
}
