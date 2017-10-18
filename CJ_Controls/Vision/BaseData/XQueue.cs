using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJ_Controls.Vision.BaseData
{
	public class XQueue
	{
		private int MAX_COUNT;
		private int pos;
		private int pull;
		private object[] queue;
		public XQueue(int counts)
		{
			pos = 0;
			MAX_COUNT = counts;
			queue = new object[MAX_COUNT];
		}
		public int Counts
		{
			get
			{
				return MAX_COUNT;
			}
		}
		public void Push(object obj)
		{
			queue[pos++] = obj;
			if (pos >= MAX_COUNT) pos = 0;
		}
		public object Pop()
		{
			return queue[pos--];
		}
		public object Pull()
		{
			if (pull >= MAX_COUNT) pull = 0;
			if (pos == pull) return null;
			return queue[pull++];
		}
	}
}
