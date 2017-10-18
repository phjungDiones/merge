using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ_Controls.Vision.BaseData
{
	public class XPos
	{
		protected int[] p;

		public static int AX = 0;
		public static int AY = 1;
		public static int MAX_POS = 2;

		public XPos()
		{
			p = new int[2] { 0, 0 };
		}
		public XPos(int x, int y)
		{
			p = new int[2] { x, y };
		}
		public void Set(int x, int y)
		{
			p[AX] = x;
			p[AY] = y;
		}
		public int[] Get()
		{
			return p;
		}
		public int X
		{
			get
			{
				return p[AX];
			}
			set
			{
				p[AX] = value;
			}
		}
		public int Y
		{
			get
			{
				return p[AY];
			}
			set
			{
				p[AY] = value;
			}
		}

		public override string ToString()
		{
			return p[AX].ToString() + "," + p[AY].ToString();
		}
		public string ToString(string formatX, string formatY)
		{
			return p[AX].ToString(formatX) + "," + p[AY].ToString(formatY);
		}
	}

	public class XSize
	{
		protected uint[] s;
		public static int AW = 0;
		public static int AH = 1;
		public static int MAX_SIZE = 2;
		public XSize()
		{
			s = new uint[2] { 0, 0 };
		}
		public XSize(uint w, uint h)
		{
			s = new uint[2] { w, h };
		}
		public void Set(uint w, uint h)
		{
			s[AW] = w;
			s[AH] = h;
		}
		public uint[] Get()
		{
			return s;
		}
		public uint W
		{
			get
			{
				return s[AW];
			}
			set
			{
				s[AW] = value;
			}
		}
		public uint H
		{
			get
			{
				return s[AH];
			}
			set
			{
				s[AH] = value;
			}
		}
		public override string ToString()
		{
			return s[AW].ToString() + "," + s[AH].ToString();
		}
		public string ToString(string formatW, string formatH)
		{
			return s[AW].ToString(formatW) + "," + s[AH].ToString(formatH);
		}
	}

	public class XRect : XPos
	{
		protected XSize s;
		public XRect()
			: base(0, 0)
		{
			s = new XSize(0, 0);
		}
		public XRect(int x, int y, uint w, uint h)
			: base(x, y)
		{
			s = new XSize(w, h);
		}
		public void Set(int x, int y, uint w, uint h)
		{
			base.Set(x, y);
			s.Set(w, h);
		}
		public void Set(XRect r)
		{
			base.Set(r.X, r.Y);
			s.Set(r.W, r.H);
		}
		public void Get(ref int[] Pos, ref uint[] Size)
		{
			Pos = base.Get();
			Size = s.Get();
		}
		public uint W
		{
			get
			{
				return s.W;
			}
			set
			{
				s.W = value;
			}
		}
		public uint H
		{
			get
			{
				return s.H;
			}
			set
			{
				s.H = value;
			}
		}
		public override string ToString()
		{
			return base.ToString() + "," + s.ToString();
		}
		public string ToString(string formatX, string formatY, string formatW, string formatH)
		{
			return base.ToString(formatX, formatY) + "," + s.ToString(formatW, formatH);
		}
	}

	public class XRange
	{
		protected double[] r;
		public static int AL = 0;
		public static int AH = 1;
		public static int MAX_RANGE = 2;

		public XRange()
		{
			r = new double[2] { 0, 0 };
		}
		public XRange(double l, double h)
		{
			r = new double[2] { l, h };
		}
		public void Set(XRange x)
		{
			r[AL] = x.L;
			r[AH] = x.H;
		}
		public void Set(double l, double h)
		{
			r[AL] = l;
			r[AH] = h;
		}
		public double[] Get()
		{
			return r;
		}
		public double L
		{
			get
			{
				return r[AL];
			}
			set
			{
				r[AL] = value;
			}
		}
		public double H
		{
			get
			{
				return r[AH];
			}
			set
			{
				r[AH] = value;
			}
		}
		public override string ToString()
		{
			return r[AL].ToString() + "," + r[AH].ToString();
		}
		public string ToString(string formatX, string formatY)
		{
			return r[AL].ToString(formatX) + "," + r[AH].ToString(formatY);
		}
	}

	public class XScalar
	{
		protected double[] p;
		public static int AX = 0;
		public static int AY = 1;
		public static int MAX_SCALAR = 2;
		public XScalar()
		{
			p = new double[2] { 0, 0 };
		}
		public XScalar(XScalar s)
		{
			p = new double[2] { s.X, s.Y };
		}
		public XScalar(double x, double y)
		{
			p = new double[2] { x, y };
		}
		public void Set(double x, double y)
		{
			p[AX] = x;
			p[AY] = y;
		}
		public void Set(XScalar s)
		{
			p[AX] = s.X;
			p[AY] = s.Y;
		}
		public double[] Get()
		{
			return p;
		}
		public double X
		{
			get
			{
				return p[AX];
			}
			set
			{
				p[AX] = value;
		}
			}
		public double Y
		{
			get
			{
				return p[AY];
			}
			set
			{
				p[AY] = value;
			}
		}

		public static XScalar operator +(XScalar a, XScalar b)
		{
			return new XScalar(a.X + b.X, a.Y + b.Y);
		}
		public static XScalar operator -(XScalar a, XScalar b)
		{
			return new XScalar(a.X - b.X, a.Y - b.Y);
		}
		public static XScalar operator *(XScalar a, XScalar b)
		{
			return new XScalar(a.X * b.X, a.Y * b.Y);
		}
		public static XScalar operator *(XScalar a, double b)
		{
			return new XScalar(a.X * b, a.Y * b);
		}
		public static XScalar operator *(double a, XScalar b)
		{
			return new XScalar(a * b.X, a * b.Y);
		}
		public static XScalar operator /(XScalar a, XScalar b)
		{
			return new XScalar(a.X / b.X, a.Y / b.Y);
		}
		public static XScalar operator /(XScalar a, double b)
		{
			return new XScalar(a.X / b, a.Y / b);
		}
		public static XScalar operator /(double a, XScalar b)
		{
			return new XScalar(a / b.X, a / b.Y);
		}
		static public explicit operator double(XScalar i)
		{
			return Math.Sqrt(i.X * i.X + i.Y * i.Y);
		}
		public double Angle()
		{
			return Math.Atan(Y / X);
		}
		public void Rotate(double dAngle)
		{
			double rx, ry;
			rx = p[AX] * Math.Cos(dAngle) + p[AY] * Math.Sin(dAngle);
			ry = -p[AX] * Math.Sin(dAngle) + p[AY] * Math.Cos(dAngle);
			p[AX] = rx;
			p[AY] = ry;
		}
		public override string ToString()
		{
			return p[AX].ToString() + "," + p[AY].ToString();
		}
		public string ToString(string formatX, string formatY)
		{
			return p[AX].ToString(formatX) + "," + p[AY].ToString(formatY);
		}
	}

	public class XVector : XScalar
	{
		public static int AT = 2;
		public static int MAX_VECTIR = 3;
		public XVector()
		{
			p = new double[3] { 0, 0, 0 };
		}
		public XVector(XVector v)
		{
			p = new double[3] { v.X, v.Y, v.T };
		}
		public XVector(double x, double y, double t)
		{
			p = new double[3] { x, y, t };
		}
		public double T
		{
			get
			{
				return p[AT];
			}
			set
			{
				p[AT] = value;
			}
		}
		public new double[] Get()
		{
			return p;
		}
		public void Set(double x, double y, double t)
		{
			p[AX] = x;
			p[AY] = y;
			p[AT] = t;
		}
		public void Set(XVector v)
		{
			p[AX] = v.X;
			p[AY] = v.Y;
			p[AT] = v.T;
		}
		public static XVector operator +(XVector a, XVector b)
		{
			return new XVector(a.X + b.X, a.Y + b.Y, a.T + b.T);
		}
		public static XVector operator -(XVector a, XVector b)
		{
			return new XVector(a.X - b.X, a.Y - b.Y, a.T - b.T);
		}
		public static XVector operator *(XVector a, XVector b)
		{
			return new XVector(a.X * b.X, a.Y * b.Y, a.T * b.T);
		}
		public static XVector operator /(XVector a, XVector b)
		{
			return new XVector(a.X / b.X, a.Y / b.Y, a.T / b.T);
		}
		public static XVector operator *(XVector a, double b)
		{
			return new XVector(a.X * b, a.Y * b, a.T * b);
		}
		public static XVector operator /(XVector a, double b)
		{
			return new XVector(a.X / b, a.Y / b, a.T / b);
		}
		public XScalar Transpose()
		{
			double tx = p[AX] * Math.Cos(p[AT]) + p[AY] * Math.Sin(p[AT]);
			double ty = -p[AX] * Math.Sin(p[AT]) + p[AY] * Math.Cos(p[AT]);
			return new XScalar(p[AX] + tx, p[AY] + ty);
		}
		public override string ToString()
		{
			return p[AX].ToString() + "," + p[AY].ToString() + "," + p[AT].ToString();
		}
		public string ToString(string formatX, string formatY, string formatT)
		{
			return p[AX].ToString(formatX) + "," + p[AY].ToString(formatY) + "," + p[AT].ToString(formatT);
		}
	}
}
