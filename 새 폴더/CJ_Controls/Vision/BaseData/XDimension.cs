using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJ_Controls.Vision.BaseData
{
	public class XDimension : XArray
	{
		public XDimension()
			: base()
		{
		}

		public new bool Add(X x)
		{
			X d = new X();
			d.Tag = x;
			return base.Add(d);
		}
		public new bool Insert(X x)
		{
			X d = new X();
			d.Tag = x;
			return base.Insert(d);
		}
		public new object This
		{
			get
			{
				if (m_xThis == null) return null;
				return m_xThis.Tag;
			}
		}

		public new object First()
		{
			X d = base.First();
			if (d == null) return null;
			return d.Tag;
		}

		public new object Prev()
		{
			X d = base.Prev();
			if (d == null) return null;
			return d.Tag;
		}

		public new object Next()
		{
			X d = base.Next();
			if (d == null) return null;
			return d.Tag;
		}

		public new object Last()
		{
			X d = base.Last();
			if (d == null) return null;
			return d.Tag;

		}
		public new object Back()
		{
			if (m_xThis == m_xLast)
			{
				Del();
				return This;
			}
			else
			{
				Del();
				return Prev();
			}
		}

		public new object Find(int lID)
		{
			if (Count == 0) return null;
			X xThis = m_xFirst;

			for (int n = 0; n < Count && xThis != null; n++)
			{

				if (xThis.ID == lID) return xThis.Tag;
				xThis = xThis.Next;
			}
			return null;
		}
		public object FindTag(int lID)
		{
			if (Count == 0) return null;
			X xThis = m_xFirst;

			for (int n = 0; n < Count && xThis != null; n++)
			{
				X xTag = (X)xThis.Tag;
				if (xTag != null)
				{
					if (xTag.ID == lID) return xThis.Tag;
				}
				xThis = xThis.Next;
			}
			return null;
		}

		public new object FindPos(int lID)
		{
			if (Count == 0) return null;
			X xThis = m_xFirst;

			for (int n = 0; n < Count && xThis != null; n++)
			{

				if (xThis.ID == lID)
				{
					m_xThis = xThis;
					return xThis.Tag;
				}
				xThis = xThis.Next;
			}
			return null;
		}
	}
}
