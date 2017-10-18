using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBDB_CTC.Data.UnitInfo
{
	public class BaseUnitInfo
	{
		private int _UnitNo = 0;
		private UNIT_STATUS _UnitStatus = UNIT_STATUS.IDLE;
		public event EventHandler UnitStatusChanged = delegate { };
		public int UnitNo
		{
			get { return _UnitNo; }
			set { _UnitNo = value; }
		}
		public UNIT_STATUS UnitStatus
		{
			get { return _UnitStatus; }
			set
			{
				if (_UnitStatus != value)
				{
					_UnitStatus = value;
					if (UnitStatusChanged != null)
					{
						UnitStatusChanged(this, EventArgs.Empty);
					}
				}
			}
		}
		public BaseUnitInfo(int unitNo)
		{
			_UnitNo = unitNo;
		}
	}
}
