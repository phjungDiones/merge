using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBDB_CTC.Data
{
	[Serializable]
	public class ConfigData
	{
		public ConfigData()
		{

		}

		public ComPortData FM_Robot_Com = new ComPortData();
		public ComPortData AtmTM_Robot_Com = new ComPortData();
		public ComPortData VacTM_Robot_Com = new ComPortData();

        public ComPortData Aligner_Com = new ComPortData();
        public ComPortData Loadlock_Com = new ComPortData();

		public ComPortData Port1 = new ComPortData();
		public ComPortData Port2 = new ComPortData();
		public ComPortData Port3 = new ComPortData();
		public ComPortData Port4 = new ComPortData();

        public ComPortData Laminator_Com = new ComPortData();
        public ComPortData Pmc_Com = new ComPortData();

    }

	[Serializable]
	public class ComPortData
	{
		public ComPortData()
		{
		}

		public string Comport = "COM3";
		public int Baudrate = 9600;
	}
}
