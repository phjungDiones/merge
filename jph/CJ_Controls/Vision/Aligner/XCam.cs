using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJ_Controls.Vision.BaseData;

namespace CJ_Controls.Vision.Aligner
{
	public class XCam
	{
		public const int MAX_FIND_COUNT = 4;
		public const int TARGET = 0;
		public const int OBJECT = 1;
		public XVector Coordinate;
		public XVector Transpose;
		public XVector Differ;
		public XVector[] Find = new XVector[MAX_FIND_COUNT];
		public XVector Cal;
		public XVector Mark;
		public XVector Pivot;
		public XVector Scale;
		public XVector Dist;
		public XCam()
		{

			Coordinate = new XVector();
			Transpose = new XVector();
			Differ = new XVector();
			Cal = new XVector();
			Mark = new XVector();
			Pivot = new XVector();
			Scale = new XVector();
			Dist = new XVector();

			for (int n = 0; n < MAX_FIND_COUNT; n++)
			{
				Find[n] = new XVector();
			}
		}
		public void Set(XCam xCam)
		{
			Coordinate.Set(xCam.Coordinate);
			Transpose.Set(xCam.Transpose);
			Differ.Set(xCam.Differ);
			Cal.Set(xCam.Cal);
			Mark.Set(xCam.Mark);
			Pivot.Set(xCam.Pivot);
			Scale.Set(xCam.Scale);
			Dist.Set(xCam.Dist);

			for (int n = 0; n < MAX_FIND_COUNT; n++)
			{
				Find[n].Set(xCam.Find[n]);
			}
		}
		public void CalPoint(bool bCourse)
		{
			if (bCourse == true)
			{
				Differ.Set(Find[OBJECT] - Find[TARGET]);
			}
			else
			{
				Differ.Set(Find[TARGET] - Find[OBJECT]);
			}
			Dist.Set(Differ * Cal);
			Coordinate.Set(Mark + Dist);
		}

		public void CalScale(XVector xDeviation)
		{
			XScalar xCoor = new XScalar(Coordinate);
			xCoor.Rotate(xDeviation.T);
			Scale.Set(xCoor - xDeviation - Mark);
		}
	}
}
