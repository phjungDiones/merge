using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ_Controls.Vision.BaseData
{
	public class X : object
	{
		public int ID;
		public int Type;
		public X Prev;
		public X Next;
		public object Tag;
		public X()
		{
			ID = 0;
			Type = 0;
			Prev = null;
			Next = null;
			Tag = null;
		}
	}
}
