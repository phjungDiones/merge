using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJ_Controls.Communication.CybogRobot_HTR;
using TBDB_Handler.DATA.UnitInfo;

namespace TBDB_CTC.Data.UnitInfo
{
	public class EquipmentInfo : BaseUnitInfo
	{
		public EquipmentInfo()
			: base(0)
		{

		}

		//LOADER Unit
		private LoaderInfo loaderData = new LoaderInfo((int)UNIT_NO.LOADER);
		public LoaderInfo GetLoaderData()
		{
			return loaderData;
		}

		private AtmTmInfo _AtmTmData = new AtmTmInfo();
		public AtmTmInfo GetAtmTmData()
		{
			return _AtmTmData;
		}

		private VaccumTmInfo _VacTmData = new VaccumTmInfo();
		public VaccumTmInfo GetVaccumTmData()
		{
			return _VacTmData;
		}

        private LoadlockInfo _Loadlock = new LoadlockInfo();
        public LoadlockInfo GetLoadlockData()
        {
            return _Loadlock;
        }

        private PmcInfo _Pmc = new PmcInfo();
        public PmcInfo GetPmcData()
        {
            return _Pmc;
        }

        private LaminatorInfo _LaminatorData = new LaminatorInfo();
        public LaminatorInfo GetLamiDaata()
        {
            return _LaminatorData;
        }
	}
}
