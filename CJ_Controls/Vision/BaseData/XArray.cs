using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ_Controls.Vision.BaseData
{
	public enum X_POS
	{
		XP_FIRST,
		XP_THIS,
		XP_LAST
	}

	public class XArray : X
	{

		protected int m_nID;
		protected uint m_nCount;
		protected X m_xFirst;
		protected X m_xThis;
		protected X m_xLast;

		public XArray()
			: base()
		{
			m_nID = 0;
			m_nCount = 0;
			m_xFirst = null;
			m_xThis = null;
			m_xLast = null;

		}

		public X This
		{
			get
			{
				return m_xThis;
			}
		}

		public X BoF
		{
			get
			{
				return m_xFirst;
			}

		}
		public X EoF
		{
			get
			{
				return m_xLast;
			}

		}
		public X First()
		{
			if (m_xFirst != null)
			{
				if (m_xFirst != m_xThis)
				{
					m_xThis = m_xFirst;
				}
				return m_xThis;
			}
			return null;
		}

		public new X Prev()
		{
			if (m_xThis != null)
			{
				X xPrev = m_xThis.Prev;
				if (xPrev != null)
				{
					m_xThis = xPrev;
					return m_xThis;
				}
			}
			return null;
		}

		public new X Next()
		{
			if (m_xThis != null)
			{
				X xNext = m_xThis.Next;
				if (xNext != null)
				{
					m_xThis = xNext;
					return m_xThis;
				}
			}
			return null;
		}

		public X Last()
		{
			if (m_xLast != null)
			{
				if (m_xLast != m_xThis)
				{
					m_xThis = m_xLast;
				}
				return m_xThis;
			}
			return null;
		}

		public bool Add(X x)
		{
			if (x != null)
			{
				if (m_xLast != null)
				{
					m_xLast.Next = x;
					x.Prev = m_xLast;
					m_xThis = m_xLast = x;
					++m_nCount;
					x.ID = ++m_nID;
				}
				else
				{
					m_xLast = m_xFirst = m_xThis = x;
					m_nCount = 1;
					x.ID = ++m_nID;
				}
				return true;
			}
			return false;
		}
		public bool Del(int lID)
		{
			X x = Find(lID);
			if (x != null)
			{
				m_xThis = x;
				return Del();
			}
			return false;
		}
		public bool Del()
		{
			if (m_xThis != null)
			{
				if (m_xLast == m_xFirst)
				{
					m_xFirst = m_xThis = m_xLast = null;
					m_nCount = 0;
					m_nID = 0;
				}
				else if (m_xFirst == m_xThis)
				{
					X xNext = m_xThis.Next;
					if (xNext != null)
					{
						xNext.Prev = null;
						m_xFirst = m_xThis = xNext;
						--m_nCount;
					}
				}
				else if (m_xLast == m_xThis)
				{
					X xPrev = m_xThis.Prev;
					if (xPrev != null)
					{
						xPrev.Next = null;
						m_xLast = m_xThis = xPrev;
						--m_nCount;
					}
				}
				else
				{
					X xPrev = m_xThis.Prev;
					X xNext = m_xThis.Next;
					if (xPrev != null && xNext != null)
					{
						m_xThis = null;
						xPrev.Next = xNext;
						xNext.Prev = xPrev;
						m_xThis = xNext;
						--m_nCount;
					}
				}
				return true;
			}

			return false;
		}

		public bool Insert(X x)
		{
			if (x != null)
			{
				if (m_xLast == m_xFirst || m_xLast == m_xThis) return Add(x);

				if (m_xFirst == m_xThis)
				{
					x.Prev = null;
					x.Next = m_xThis;
					m_xThis.Prev = x;
					m_xFirst = m_xThis = x;
					x.ID = ++m_nID;
					++m_nCount;
				}
				else
				{
					X xPrev = m_xThis.Prev;
					xPrev.Next = x;
					x.Prev = xPrev;
					x.Next = m_xThis;
					m_xThis.Prev = x;
					x.ID = ++m_nID;
					++m_nCount;
				}
				return true;
			}
			return false;
		}

		public X Back()
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

		public bool Empty()
		{
			if (m_xFirst == m_xLast) return Del();

			if (m_xFirst != null)
			{
				m_xThis = m_xFirst;
			}

			while (m_xThis != null && m_nCount > 0)
			{
				m_xThis.Prev = null;
				m_nCount--;
				m_xThis = m_xThis.Next;
			}
			m_nID = 0;
			m_nCount = 0;
			m_xFirst = m_xThis = m_xLast = null;
			return true;
		}

		public bool Exchange(X iDst, X iSoc)
		{
			if (iDst == null || iSoc == null) return false;
			if (iDst != iSoc)
			{

				X iDst_prev = iDst.Prev;
				X iDst_next = iDst.Next;
				X iSoc_prev = iSoc.Prev;
				X iSoc_next = iSoc.Next;

				if (iDst_prev == null)
				{
					m_xFirst = iSoc;
				}
				else
				{
					iDst_prev.Next = iSoc;
				}
				if (iDst_next == null)
				{
					m_xLast = iDst;
				}
				else
				{
					iDst_next.Prev = iSoc;
				}

				if (iSoc_prev == null)
				{
					m_xFirst = iDst;
				}
				else
				{
					iSoc_prev.Next = iDst;
				}
				if (iSoc_next == null)
				{
					m_xLast = iDst;
				}
				else
				{
					iSoc_next.Prev = iDst;
				}


				if (iDst_next == iSoc || iSoc_next == iDst)
				{
					iDst.Next = iSoc_next;
					iDst.Prev = iSoc;
					iSoc.Next = iDst;
					iSoc.Prev = iDst_prev;
				}
				else
				{
					iDst.Next = iSoc_next;
					iDst.Prev = iSoc_prev;
					iSoc.Next = iDst_next;
					iSoc.Prev = iDst_prev;
				}

			}
			return true;
		}

		public uint Count
		{
			get
			{
				return m_nCount;
			}
		}

		public int FindMaxID()
		{
			int nMaxID = 0;
			if (m_nCount > 0)
			{
				X xThis = m_xFirst;
				for (int n = 0; n < m_nCount && xThis != null; n++)
				{
					if (xThis != null)
					{
						if (xThis.ID > nMaxID)
						{
							nMaxID = xThis.ID;
						}
						xThis = xThis.Next;
					}
				}
			}
			return nMaxID;
		}

		public X Find(int lID)
		{
			if (m_nCount == 0) return null;

			if (m_xThis.ID == lID)
			{
				return m_xThis;
			}
			else if (m_xThis.ID < lID)
			{
				X xThis = m_xThis;
				for (int n = 0; n < m_nCount && xThis != null; n++)
				{
					if (xThis.ID == lID)
					{
						return xThis;
					}
					xThis = xThis.Next;
				}
			}
			else if (m_xThis.ID > lID)
			{
				X xThis = m_xThis;
				for (int n = 0; n < m_nCount && xThis != null; n++)
				{
					if (xThis.ID == lID)
					{
						return xThis;
					}
					xThis = xThis.Prev;
				}
			}

			{
				X xThis = m_xFirst;
				for (int n = 0; n < m_nCount && xThis != null; n++)
				{
					if (xThis.ID == lID)
					{
						return xThis;
					}
					xThis = xThis.Next;
				}
			}
			return null;
		}
		public X FindPos(int lID)
		{
			if (m_nCount == 0) return null;

			if (m_xThis.ID == lID)
			{
				return m_xThis;
			}
			else if (m_xThis.ID < lID)
			{
				X xThis = m_xThis;
				for (int n = 0; n < m_nCount && xThis != null; n++)
				{
					if (xThis.ID == lID)
					{
						m_xThis = xThis;
						return xThis;
					}
					xThis = xThis.Next;
				}
			}
			else if (m_xThis.ID > lID)
			{
				X xThis = m_xThis;
				for (int n = 0; n < m_nCount && xThis != null; n++)
				{
					if (xThis.ID == lID)
					{
						m_xThis = xThis;
						return xThis;
					}
					xThis = xThis.Prev;
				}
			}

			{
				X xThis = m_xFirst;
				for (int n = 0; n < m_nCount && xThis != null; n++)
				{
					if (xThis.ID == lID)
					{
						m_xThis = xThis;
						return xThis;
					}
					xThis = xThis.Next;
				}
			}

			return null;
		}


		public X Get(X_POS xp)
		{
			switch (xp)
			{
				case X_POS.XP_FIRST: return m_xFirst;
				case X_POS.XP_THIS: return m_xThis;
				case X_POS.XP_LAST: return m_xLast;
			}
			return null;
		}

	}
}
