using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJ_Controls.Vision.BaseData;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CJ_Controls.Vision.Viewer
{
	public enum CONTROL_POSITION
	{
		TL,
		TR,
		BR,
		BL,
		TC,
		MR,
		BC,
		ML,
		MC,
		TT,
		MAX
	}
	public enum SHAPE
	{
		RECTANGLE,
		ELLIPSE,
		LINE,
		CLOSE,
		MAX
	}
	public class XShape : X
	{
		protected SHAPE m_eRShape;
		protected float[] p;

		public XShape()
		{
			m_eRShape = SHAPE.RECTANGLE;
			p = new float[5] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
		}

		public XShape(float x, float y, float w, float h)
		{
			m_eRShape = SHAPE.RECTANGLE;
			p = new float[5] { x, y, w, h, 0.0f };
		}

		public XShape(float x, float y, float w, float h, float t)
		{
			m_eRShape = SHAPE.RECTANGLE;
			p = new float[5] { x, y, w, h, t };
		}

		public XShape(SHAPE eRShape, float x, float y, float w, float h, float t)
		{
			m_eRShape = eRShape;
			p = new float[5] { x, y, w, h, t };
		}

		public XShape(XShape Shape)
		{
			m_eRShape = Shape.m_eRShape;
			p = new float[5];
			Array.Copy(Shape.Get(), p, 5);
		}

		public void Set(XShape Shape)
		{
			m_eRShape = Shape.m_eRShape;
			Array.Copy(Shape.Get(), p, 5);
		}

		public SHAPE RegionShape
		{
			get
			{
				return m_eRShape;
			}
			set
			{
				m_eRShape = value;
			}
		}

		public float Left
		{
			get
			{
				return p[0] - p[2];
			}
		}

		public float Top
		{
			get
			{
				return p[1] - p[3];
			}
		}

		public float Right
		{
			get
			{
				return p[0] + p[2];
			}
		}

		public float Bottom
		{
			get
			{
				return p[1] + p[3];
			}
		}

		public float X
		{
			get
			{
				return p[0];
			}
			set
			{
				p[0] = value;
			}
		}
		public float Y
		{
			get
			{
				return p[1];
			}
			set
			{
				p[1] = value;
			}
		}
		public float W
		{
			get
			{
				return p[2];
			}
			set
			{
				p[2] = value;
			}
		}
		public float H
		{
			get
			{
				return p[3];
			}
			set
			{
				p[3] = value;
			}
		}
		public float T
		{
			get
			{
				return p[4];
			}
			set
			{
				p[4] = value;
			}
		}

		public float[] Get()
		{
			return p;
		}

		public void Set(float x, float y, float w, float h, float t)
		{
			p[0] = x;
			p[1] = y;
			p[2] = w;
			p[3] = h;
			p[4] = t;
		}

		public RectangleF Rect()
		{
			PointF[] cp = GetCP();
			float minX = p[0], maxX = p[0];
			float minY = p[1], maxY = p[1];
			for (int n = 0; n < (int)CONTROL_POSITION.MAX - 1; n++)
			{
				if (minX > cp[n].X) minX = cp[n].X;
				if (minY > cp[n].Y) minY = cp[n].Y;
				if (maxX < cp[n].X) maxX = cp[n].X;
				if (maxY < cp[n].Y) maxY = cp[n].Y;
			}
			return new RectangleF(minX, minY, maxX - minX, maxY - minY);
		}

		public PointF[] GetCP()
		{
			PointF[] cp = new PointF[(int)CONTROL_POSITION.MAX];
			double t = p[4];
			double Cos = Math.Cos(t);
			double Sin = Math.Sin(t);
			float x = p[0];
			float y = p[1];
			float w = p[2];
			float h = p[3];
			cp[(int)CONTROL_POSITION.MC].X = x;
			cp[(int)CONTROL_POSITION.MC].Y = y;

			cp[(int)CONTROL_POSITION.TL].X = (float)(x - w * Cos - h * Sin);
			cp[(int)CONTROL_POSITION.TL].Y = (float)(y + w * Sin - h * Cos);
			cp[(int)CONTROL_POSITION.TR].X = (float)(x + w * Cos - h * Sin);
			cp[(int)CONTROL_POSITION.TR].Y = (float)(y - w * Sin - h * Cos);
			cp[(int)CONTROL_POSITION.BR].X = (float)(x + w * Cos + h * Sin);
			cp[(int)CONTROL_POSITION.BR].Y = (float)(y - w * Sin + h * Cos);
			cp[(int)CONTROL_POSITION.BL].X = (float)(x - w * Cos + h * Sin);
			cp[(int)CONTROL_POSITION.BL].Y = (float)(y + w * Sin + h * Cos);

			cp[(int)CONTROL_POSITION.TC].X = (cp[(int)CONTROL_POSITION.TL].X + cp[(int)CONTROL_POSITION.TR].X) / 2.0f;
			cp[(int)CONTROL_POSITION.TC].Y = (cp[(int)CONTROL_POSITION.TL].Y + cp[(int)CONTROL_POSITION.TR].Y) / 2.0f;
			cp[(int)CONTROL_POSITION.MR].X = (cp[(int)CONTROL_POSITION.TR].X + cp[(int)CONTROL_POSITION.BR].X) / 2.0f;
			cp[(int)CONTROL_POSITION.MR].Y = (cp[(int)CONTROL_POSITION.TR].Y + cp[(int)CONTROL_POSITION.BR].Y) / 2.0f;
			cp[(int)CONTROL_POSITION.BC].X = (cp[(int)CONTROL_POSITION.BL].X + cp[(int)CONTROL_POSITION.BR].X) / 2.0f;
			cp[(int)CONTROL_POSITION.BC].Y = (cp[(int)CONTROL_POSITION.BL].Y + cp[(int)CONTROL_POSITION.BR].Y) / 2.0f;
			cp[(int)CONTROL_POSITION.ML].X = (cp[(int)CONTROL_POSITION.TL].X + cp[(int)CONTROL_POSITION.BL].X) / 2.0f;
			cp[(int)CONTROL_POSITION.ML].Y = (cp[(int)CONTROL_POSITION.TL].Y + cp[(int)CONTROL_POSITION.BL].Y) / 2.0f;
			cp[(int)CONTROL_POSITION.TT].X = (cp[(int)CONTROL_POSITION.MC].X + cp[(int)CONTROL_POSITION.MR].X) / 2.0f;
			cp[(int)CONTROL_POSITION.TT].Y = (cp[(int)CONTROL_POSITION.MC].Y + cp[(int)CONTROL_POSITION.MR].Y) / 2.0f;

			if (m_eRShape == SHAPE.ELLIPSE)
			{
				for (int n = 0; n < 4; n++)
				{
					double dt = Math.PI / 4 + Math.PI * n / 2.0;
					double tx = w * Math.Cos(dt);
					double ty = h * Math.Sin(dt);
					cp[n].X = (long)(x + tx * Cos + ty * Sin);
					cp[n].Y = (long)(y - tx * Sin + ty * Cos);
				}
			}
			return cp;
		}
	}

	public enum REGION_CONTROL_STYLES
	{
		NONE = 0,

		TL = 0x0001,
		TR = 0x0002,
		BR = 0x0004,
		BL = 0x0008,
		TC = 0x0010,
		MR = 0x0020,
		BC = 0x0040,
		ML = 0x0080,
		MC = 0x0100,
		TT = 0x0200,
		SELECT = 0x0300,

		ALL = 0xFFFF
	}

	public class XRegion : XShape
	{
		private bool bHitOn;
		private REGION_CONTROL_STYLES m_ObjectAlignRCS;

		private Color m_cBorderColor;
		private Color m_cFillColor;
		private Bitmap m_cFillImage;
		private DashStyle m_dBorderStyle;
		private REGION_CONTROL_STYLES m_RCS;
		private REGION_CONTROL_STYLES m_SelectRCS;
		private string m_sCaption;

		private float m_fSpaces;
		private PointF fix = new PointF();
		private int m_Param;
		private RegionAlign m_RegionAlign = new RegionAlign();

		float m_fOx = 0, m_fOy = 0;

		public XRegion()
		{
			bHitOn = false;
			m_ObjectAlignRCS = REGION_CONTROL_STYLES.NONE;
			m_fSpaces = 5.0f;
			m_cBorderColor = Color.Blue;
			m_RCS = REGION_CONTROL_STYLES.ALL;
			m_SelectRCS = REGION_CONTROL_STYLES.NONE;
			m_dBorderStyle = DashStyle.Solid;
			m_cFillImage = null;
		}

		public XRegion(float _x, float _y, float _w, float _h)
			: base(_x, _y, _w, _h)
		{
			InitRegion();
		}

		public XRegion(float _x, float _y, float _w, float _h, float _t)
			: base(_x, _y, _w, _h, _t)
		{
			InitRegion();
		}
		public XRegion(SHAPE eRShape, float _x, float _y, float _w, float _h, float _t)
			: base(eRShape, _x, _y, _w, _h, _t)
		{
			InitRegion();
		}

		private void Set(XRegion xRegion)
		{
			base.Set(xRegion);

			m_cBorderColor = xRegion.BorderColor;
			m_cFillColor = xRegion.FillColor;
			m_cFillImage = new Bitmap(xRegion.FillImage);
			m_dBorderStyle = xRegion.BorderStyle;
			m_RCS = xRegion.ControlStyle;
			m_SelectRCS = xRegion.ControlSelect;
			m_sCaption = xRegion.Caption;
		}

		private void InitRegion()
		{
			m_fSpaces = 5.0f;
			m_cBorderColor = Color.Blue;
			m_RCS = REGION_CONTROL_STYLES.ALL;
			m_SelectRCS = REGION_CONTROL_STYLES.NONE;
			m_dBorderStyle = DashStyle.Solid;
			m_cFillImage = null;
		}

		public float Spaces
		{
			get
			{
				return m_fSpaces;
			}
			set
			{
				m_fSpaces = value;
			}
		}

		public RegionAlign RegionAlign
		{
			get
			{
				return m_RegionAlign;
			}
			set
			{
				m_RegionAlign = value;
			}
		}

		public int Param
		{
			get
			{
				return m_Param;
			}
			set
			{
				m_Param = value;
			}
		}

		public string Caption
		{
			get
			{
				return m_sCaption;
			}
			set
			{
				m_sCaption = value;
			}
		}

		public Color BorderColor
		{
			get
			{
				return m_cBorderColor;
			}
			set
			{
				m_cBorderColor = value;
			}
		}

		public Color FillColor
		{
			get
			{
				return m_cFillColor;
			}
			set
			{
				m_cFillColor = value;
			}
		}

		public Bitmap FillImage
		{
			get
			{
				return m_cFillImage;
			}
			set
			{
				m_cFillImage = value;
			}
		}

		public DashStyle BorderStyle
		{
			get
			{
				return m_dBorderStyle;
			}
			set
			{
				m_dBorderStyle = value;
			}
		}

		public PointF Fix
		{
			get
			{
				return fix;
			}
			set
			{
				fix = value;
			}
		}

		public REGION_CONTROL_STYLES ControlStyle
		{
			get
			{
				return m_RCS;
			}
			set
			{
				m_RCS = value;
			}
		}

		public REGION_CONTROL_STYLES ControlSelect
		{
			get
			{
				return m_SelectRCS;
			}
			set
			{
				m_SelectRCS = value;
			}
		}

		public bool InnerRegion(float _x, float _y, float _w, float _h)
		{
			float spx = _x - _w;
			float spy = _y - _h;
			float epx = _x + _w;
			float epy = _y + _h;
			PointF[] cp = GetCP();
			for (int n = 0; n < (int)CONTROL_POSITION.MAX; n++)
			{
				if (spx > cp[n].X || cp[n].X > epx ||
					spy > cp[n].Y || cp[n].Y > epy)
				{
					return false;
				}
			}
			return true;
		}

		public bool InnerRegion(XShape xShape)
		{
			float fX = xShape.X;
			float fY = xShape.Y;
			float spx = fX - xShape.W;
			float spy = fY - xShape.H;
			float epx = fX + xShape.W;
			float epy = fY + xShape.H;

			PointF[] cp = GetCP();
			double t = p[4];
			for (int n = 0; n < (int)CONTROL_POSITION.MAX; n++)
			{
				double dx = cp[n].X - fX;
				double dy = cp[n].Y - fY;
				double px = dx * Math.Cos(t) + dy * Math.Sin(t);
				double py = dx * Math.Sin(t) + dy * Math.Cos(t);
				px += fX;
				py += fY;

				if (spx > px || px > epx ||
					spy > py || py > epy)
				{
					return false;
				}
			}
			return true;
		}

		public REGION_CONTROL_STYLES HitTestBolder(float fX, float fY, float fSpaces, Keys nFlags)//Graphics gp, float fOx, float fOy, float fZoomX, float fZoomY)
		{
			REGION_CONTROL_STYLES SelectRCS = REGION_CONTROL_STYLES.NONE;
			PointF[] dp = GetCP();
			float x = p[0];
			float y = p[1];
			float w = p[2];
			float h = p[3];
			double t = p[4];

			switch (RegionShape)
			{
				case SHAPE.CLOSE:
					{

					} break;
				case SHAPE.ELLIPSE:
					{
						double nt = 0;
						double cost = Math.Cos(t);
						double sint = Math.Sin(t);
						for (long n = 0; n < 6284; n++)
						{
							nt += 0.001;
							double tx = w * Math.Cos(nt);
							double ty = h * Math.Sin(nt);
							float drawX = (float)(x + tx * cost + ty * sint);
							float drawY = (float)(y - tx * sint + ty * cost);

							if (Math.Abs(fX - drawX) < fSpaces && Math.Abs(fY - drawY) < fSpaces)
							{
								SelectRCS = REGION_CONTROL_STYLES.SELECT;
								break;
							}
						}
					} break;
				case SHAPE.RECTANGLE:
				default:
					{
						PointF[] pDraw = new PointF[5];
						for (int n = 0; n < 4; n++)
						{
							pDraw[n].X = dp[n].X;
							pDraw[n].Y = dp[n].Y;
						}
						pDraw[4] = pDraw[0];

						for (int n = 0; n < 4; n++)
						{
							float Lx = pDraw[n].X - pDraw[n + 1].X;
							float Ly = pDraw[n].Y - pDraw[n + 1].Y;

							float Fx = pDraw[n].X - fX;
							float Fy = pDraw[n].Y - fY;

							if (Lx == 0 && Math.Abs(Fx) < fSpaces)
							{
								if ((pDraw[n].Y < fY && fY < pDraw[n + 1].Y) || (pDraw[n + 1].Y < fY && fY < pDraw[n].Y))
								{
									SelectRCS = REGION_CONTROL_STYLES.SELECT;
									break;
								}
							}
							else if (Ly == 0 && Math.Abs(Fy) < fSpaces)
							{
								if ((pDraw[n].X < fX && fX < pDraw[n + 1].X) || (pDraw[n + 1].X < fX && fX < pDraw[n].X))
								{
									SelectRCS = REGION_CONTROL_STYLES.SELECT;
									break;
								}
							}
							else
							{
								if (Math.Abs((Ly / Lx) * Fx - Fy) < fSpaces)
								{
									SelectRCS = REGION_CONTROL_STYLES.SELECT;
									break;
								}
							}
						}
					} break;
			}

			if (SelectRCS != REGION_CONTROL_STYLES.NONE) bHitOn = true;
			return SelectRCS;
		}

		public REGION_CONTROL_STYLES HitTest(float fX, float fY, float fSpaces, Keys nFlags)
		{
			PointF[] cp = GetCP();
			REGION_CONTROL_STYLES SelectRCS = REGION_CONTROL_STYLES.NONE;
			for (int n = 0; n < (int)CONTROL_POSITION.MAX; n++)
			{
				float ax = fSpaces - Math.Abs(cp[n].X - fX); // fZoomX 
				float ay = fSpaces - Math.Abs(cp[n].Y - fY); // fZoomY 
				if (ax > 0 && ay > 0)
				{
					SelectRCS = (REGION_CONTROL_STYLES)(1 << n);
					if ((m_RCS & SelectRCS) != 0 || nFlags == Keys.Alt)
					{
						if (SelectRCS != REGION_CONTROL_STYLES.NONE)
							break;
					}
					else
					{
						SelectRCS = REGION_CONTROL_STYLES.NONE;
					}
				}
			}

			if (SelectRCS != REGION_CONTROL_STYLES.NONE) bHitOn = true;
			return SelectRCS;
		}

		public REGION_CONTROL_STYLES SelectControl(float fX, float fY, float fSpaces, Keys nFlags)
		{
			REGION_CONTROL_STYLES RetRCS = HitTest(fX, fY, fSpaces, nFlags);
			if (RetRCS == REGION_CONTROL_STYLES.NONE)
			{
				RetRCS = HitTestBolder(fX, fY, fSpaces, nFlags);
			}

			m_SelectRCS = RetRCS;
			PointF[] cp = GetCP();
			float x = p[0];
			float y = p[1];
			float w = p[2];
			float h = p[3];
			double t = p[4];
			if (nFlags == Keys.Alt)
			{
				if (m_SelectRCS != REGION_CONTROL_STYLES.NONE)
				{
					fix.X = x - fX;
					fix.Y = y - fY;
					if ((m_SelectRCS & m_RCS) != 0)
					{
						m_ObjectAlignRCS = m_SelectRCS;
						m_RegionAlign.TargetRegion = null;
						m_SelectRCS = REGION_CONTROL_STYLES.MC;
					}
					else
					{
						m_ObjectAlignRCS = REGION_CONTROL_STYLES.NONE;
					}
				}
			}
			else
			{
				switch (m_SelectRCS)
				{
					case REGION_CONTROL_STYLES.TL:
						fix = cp[(int)CONTROL_POSITION.BR]; break;
					case REGION_CONTROL_STYLES.TR:
						fix = cp[(int)CONTROL_POSITION.BL]; break;
					case REGION_CONTROL_STYLES.BR:
						fix = cp[(int)CONTROL_POSITION.TL]; break;
					case REGION_CONTROL_STYLES.BL:
						fix = cp[(int)CONTROL_POSITION.TR]; break;
					case REGION_CONTROL_STYLES.TC:
						fix = cp[(int)CONTROL_POSITION.BC]; break;
					case REGION_CONTROL_STYLES.MR:
						fix = cp[(int)CONTROL_POSITION.ML]; break;
					case REGION_CONTROL_STYLES.BC:
						fix = cp[(int)CONTROL_POSITION.TC]; break;
					case REGION_CONTROL_STYLES.ML:
						fix = cp[(int)CONTROL_POSITION.MR]; break;
					case REGION_CONTROL_STYLES.MC:
					case REGION_CONTROL_STYLES.TT:
						fix.X = 0;
						fix.Y = 0;
						break;
				}

				if ((m_RCS & REGION_CONTROL_STYLES.ALL) != 0 && m_SelectRCS == REGION_CONTROL_STYLES.NONE)
				{
					double dx = fX - x;
					double dy = fY - y;
					double dw = dx * Math.Cos(-t) + dy * Math.Sin(-t);
					double dh = -dx * Math.Sin(-t) + dy * Math.Cos(-t);

					if ((w - Math.Abs(dw)) > 0 && (h - Math.Abs(dh)) > 0)
					{
						if (m_eRShape == SHAPE.ELLIPSE)
						{
							if (dx != 0 && dy != 0)
							{
								double dt = Math.Atan(dy / dx);
								double l = Math.Sqrt(w * w + h * h);
								double tx = l * Math.Cos(dt);
								double ty = l * Math.Sin(dt);

								if ((dx * dx) <= (tx * tx) && (dy * dy) <= (ty * ty))
								{
									fix.X = x - fX;
									fix.Y = y - fY;
									m_SelectRCS = REGION_CONTROL_STYLES.ALL;
								}
							}
							else
							{
								fix.X = x - fX;
								fix.Y = y - fY;
								m_SelectRCS = REGION_CONTROL_STYLES.ALL;
							}
						}
						else
						{
							fix.X = x - fX;
							fix.Y = y - fY;
							m_SelectRCS = REGION_CONTROL_STYLES.ALL;
						}
					}
				}
			}
			return RetRCS;
		}

		public void DrawFill(Graphics gp, int nTransparency)
		{
			try
			{
				float x = p[0];
				float y = p[1];
				float w = p[2];
				float h = p[3];
				double t = p[4];

				PointF[] dp = GetCP();
				Brush BrushFill;
				if (m_cFillImage == null)
				{
					BrushFill = new SolidBrush(Color.FromArgb(FillColor.A & nTransparency, FillColor.R, FillColor.G, FillColor.B));
				}
				else
				{
					double a = -t;
					TextureBrush TextureFill = new TextureBrush(m_cFillImage);
					TextureFill.RotateTransform((float)(a * 180.0 / Math.PI));
					double dx = w + x * Math.Cos(a) + y * Math.Sin(a);
					double dy = h - x * Math.Sin(a) + y * Math.Cos(a);
					TextureFill.TranslateTransform((float)dx, (float)dy);
					TextureFill.ScaleTransform(w / m_cFillImage.Width * 2.0f, h / m_cFillImage.Height * 2.0f);
					BrushFill = TextureFill;
				}

				switch (RegionShape)
				{
					case SHAPE.ELLIPSE:
						{
							double nt = 0;
							double cost = Math.Cos(t);
							double sint = Math.Sin(t);
							Point[] pDraw = new Point[6285];
							for (long n = 0; n < 6284; n++)
							{
								nt += 0.001;
								double tx = w * Math.Cos(nt);
								double ty = h * Math.Sin(nt);
								pDraw[n].X = (int)(x + tx * cost + ty * sint + 0.5);
								pDraw[n].Y = (int)(y - tx * sint + ty * cost + 0.5);
							}

							pDraw[6284] = pDraw[0];
							gp.FillPolygon(BrushFill, pDraw);
						} break;
					case SHAPE.RECTANGLE:
					default:
						{
							Point[] pDraw = new Point[5];
							for (int n = 0; n < 4; n++)
							{
								pDraw[n].X = (int)(dp[n].X + 0.5);
								pDraw[n].Y = (int)(dp[n].Y + 0.5);
							}
							pDraw[4] = pDraw[0];
							gp.FillPolygon(BrushFill, pDraw);
						} break;
				}

				BrushFill.Dispose();
			}
			catch (Exception pe)
			{
				string sMsg = pe.ToString();
			}
		}

		public void DrawFill(Graphics gp, float fOx, float fOy, float fZoomX, float fZoomY, int nTransparency)
		{
			m_fOx = fOx;
			m_fOy = fOy;

			try
			{
				PointF[] dp = GetCP();
				for (int n = 0; n < (int)CONTROL_POSITION.MAX; n++)
				{
					dp[n].X *= fZoomX;
					dp[n].Y *= fZoomY;
				}

				float x = p[0] * fZoomX;
				float y = p[1] * fZoomY;
				float w = p[2] * fZoomX;
				float h = p[3] * fZoomY;
				double t = p[4];

				Brush BrushFill;
				if (m_cFillImage == null)
				{
					BrushFill = new SolidBrush(Color.FromArgb(FillColor.A & nTransparency, FillColor.R, FillColor.G, FillColor.B));
				}
				else
				{
					double a = -t;
					double ox = x + fOx;
					double oy = y + fOy;
					TextureBrush TextureFill = new TextureBrush(m_cFillImage);
					TextureFill.RotateTransform((float)(a * 180.0 / Math.PI));
					double dx = w + ox * Math.Cos(a) + oy * Math.Sin(a);
					double dy = h - ox * Math.Sin(a) + oy * Math.Cos(a);
					TextureFill.TranslateTransform((float)dx, (float)dy);
					TextureFill.ScaleTransform(w / m_cFillImage.Width * 2.0f, h / m_cFillImage.Height * 2.0f);
					BrushFill = TextureFill;
				}

				switch (RegionShape)
				{
					case SHAPE.ELLIPSE:
						{
							double nt = 0;
							double cost = Math.Cos(t);
							double sint = Math.Sin(t);
							Point[] pDraw = new Point[6285];
							for (long n = 0; n < 6284; n++)
							{
								nt += 0.001;
								double tx = w * Math.Cos(nt);
								double ty = h * Math.Sin(nt);
								pDraw[n].X = (int)(x + tx * cost + ty * sint + fOx + 0.5);
								pDraw[n].Y = (int)(y - tx * sint + ty * cost + fOy + 0.5);
							}
							pDraw[6284] = pDraw[0];
							gp.FillPolygon(BrushFill, pDraw);
						} break;
					case SHAPE.RECTANGLE:
					default:
						{
							Point[] pDraw = new Point[5];
							for (int n = 0; n < 4; n++)
							{
								pDraw[n].X = (int)(dp[n].X + fOx + 0.5);
								pDraw[n].Y = (int)(dp[n].Y + fOy + 0.5);
							}
							pDraw[4] = pDraw[0];
							gp.FillPolygon(BrushFill, pDraw);
						} break;
				}

				BrushFill.Dispose();
			}
			catch (Exception pe)
			{
				string sMsg = pe.ToString();
			}
		}

		public void Draw(Graphics gp, float fOx, float fOy, float fZoomX, float fZoomY)
		{
			m_fOx = fOx;
			m_fOy = fOy;

			Pen BolderPen = new Pen(m_cBorderColor);
			BolderPen.DashStyle = m_dBorderStyle;

			PointF[] dp = GetCP();
			for (int n = 0; n < (int)CONTROL_POSITION.MAX; n++)
			{
				dp[n].X *= fZoomX;
				dp[n].Y *= fZoomY;
			}

			float x = p[0] * fZoomX;
			float y = p[1] * fZoomY;
			float w = p[2] * fZoomX;
			float h = p[3] * fZoomY;
			double t = p[4];

			switch (RegionShape)
			{
				case SHAPE.CLOSE:
					{
						PointF[] pDraw = new PointF[2];
						pDraw[0].X = dp[(int)CONTROL_POSITION.ML].X + fOx;
						pDraw[0].Y = dp[(int)CONTROL_POSITION.ML].Y + fOy;
						pDraw[1].X = dp[(int)CONTROL_POSITION.MR].X + fOx;
						pDraw[1].Y = dp[(int)CONTROL_POSITION.MR].Y + fOy;
						gp.DrawLine(BolderPen, pDraw[0], pDraw[1]);

						pDraw[0].X = dp[(int)CONTROL_POSITION.TC].X + fOx;
						pDraw[0].Y = dp[(int)CONTROL_POSITION.TC].Y + fOy;
						pDraw[1].X = dp[(int)CONTROL_POSITION.BC].X + fOx;
						pDraw[1].Y = dp[(int)CONTROL_POSITION.BC].Y + fOy;
						gp.DrawLine(BolderPen, pDraw[0], pDraw[1]);
					} break;
				case SHAPE.ELLIPSE:
					{
						double nt = 0;
						double cost = Math.Cos(t);
						double sint = Math.Sin(t);
						PointF[] pDraw = new PointF[6285];
						for (long n = 0; n < 6284; n++)
						{
							nt += 0.001;
							double tx = w * Math.Cos(nt);
							double ty = h * Math.Sin(nt);
							pDraw[n].X = (float)(x + tx * cost + ty * sint + fOx);
							pDraw[n].Y = (float)(y - tx * sint + ty * cost + fOy);
						}
						pDraw[6284] = pDraw[0];
						gp.DrawPolygon(BolderPen, pDraw);
					} break;
				case SHAPE.RECTANGLE:
				default:
					{
						PointF[] pDraw = new PointF[5];
						for (int n = 0; n < 4; n++)
						{
							pDraw[n].X = dp[n].X + fOx;
							pDraw[n].Y = dp[n].Y + fOy;
						}
						pDraw[4] = pDraw[0];
						gp.DrawPolygon(BolderPen, pDraw);
					} break;
			}

			if (m_SelectRCS != REGION_CONTROL_STYLES.NONE || bHitOn == true)
			{
				for (int n = 0; n < (int)CONTROL_POSITION.MAX - 1; n++)
				{
					if (((int)m_RCS & (1 << n)) != 0)
					{
						gp.DrawRectangle(BolderPen, dp[n].X - m_fSpaces + fOx, dp[n].Y - m_fSpaces + fOy, m_fSpaces + m_fSpaces, m_fSpaces + m_fSpaces);
					}
				}

				if ((m_RCS & REGION_CONTROL_STYLES.TT) != 0)
				{
					gp.DrawEllipse(BolderPen, dp[(int)CONTROL_POSITION.TT].X - m_fSpaces + fOx, dp[(int)CONTROL_POSITION.TT].Y - m_fSpaces + fOy, m_fSpaces + m_fSpaces, m_fSpaces + m_fSpaces);
					PointF[] pDraw = new PointF[2];
					pDraw[0].X = x + fOx;
					pDraw[0].Y = y + fOy;
					pDraw[1].X = dp[(int)CONTROL_POSITION.TT].X + fOx;
					pDraw[1].Y = dp[(int)CONTROL_POSITION.TT].Y + fOy;
					gp.DrawPolygon(BolderPen, pDraw);
				}

				if ((m_RCS & REGION_CONTROL_STYLES.MC) != 0)
				{
					PointF[] pDraw = new PointF[5];
					PointF mc = dp[(int)CONTROL_POSITION.MC];
					mc.X += fOx;
					mc.Y += fOy;
					float fSpaces = m_fSpaces * 1.8f;
					pDraw[0].X = mc.X;
					pDraw[0].Y = mc.Y - fSpaces;

					pDraw[1].X = mc.X + fSpaces;
					pDraw[1].Y = mc.Y;

					pDraw[2].X = mc.X;
					pDraw[2].Y = mc.Y + fSpaces;

					pDraw[3].X = mc.X - fSpaces;
					pDraw[3].Y = mc.Y;
					pDraw[4] = pDraw[0];
					gp.DrawLines(BolderPen, pDraw);
				}
				bHitOn = false;
			}

			if ((m_RCS & REGION_CONTROL_STYLES.MC) != 0)
			{
				{
					PointF[] pDraw = new PointF[2];
					float fMCx = x + fOx;
					float fMCy = y + fOy;

					pDraw[0].X = fMCx - 16.0f;
					pDraw[1].X = fMCx + 16.0f;
					pDraw[0].Y = pDraw[1].Y = fMCy;
					gp.DrawLine(BolderPen, pDraw[0], pDraw[1]);

					pDraw[0].X = pDraw[1].X = fMCx;
					pDraw[0].Y = fMCy - 16.0f;
					pDraw[1].Y = fMCy + 16.0f;
					gp.DrawLine(BolderPen, pDraw[0], pDraw[1]);
				}
			}
			BolderPen.Dispose();
		}

		public void Control(float _x, float _y, float fZoomX, float fZoomY)
		{
			if (fZoomX == 0 || fZoomY == 0)
				return;

			float fX = _x / fZoomX;
			float fY = _y / fZoomY;
			float dw = fX - fix.X;
			float dh = fY - fix.Y;

			REGION_CONTROL_STYLES ControlRCS = m_SelectRCS & m_RCS;

			double t = p[4];

			switch (ControlRCS)
			{

				case REGION_CONTROL_STYLES.MC:
					{
						p[0] = fix.X + fX;
						p[1] = fix.Y + fY;

					} break;

				case REGION_CONTROL_STYLES.TT:
					{
						double dx = fX - p[0];
						double dy = p[1] - fY;
						p[4] = (float)Math.Atan(dy / dx);
						if (dx < 0) p[4] = (float)(Math.PI + p[4]);
						if (t < 0) p[4] = (float)(2.0 * Math.PI + p[4]);
					} break;


				case REGION_CONTROL_STYLES.TC:
					{
						p[3] = (float)Math.Abs(-dw * Math.Sin(-t) + dh * Math.Cos(-t)) / 2.0f;
						p[0] = fix.X - p[3] * (float)Math.Sin(t);
						p[1] = fix.Y - p[3] * (float)Math.Cos(t);
					} break;
				case REGION_CONTROL_STYLES.BC:
					{
						p[3] = (float)Math.Abs(-dw * Math.Sin(-t) + dh * Math.Cos(-t)) / 2.0f;
						p[0] = fix.X + p[3] * (float)Math.Sin(t);
						p[1] = fix.Y + p[3] * (float)Math.Cos(t);
					} break;

				case REGION_CONTROL_STYLES.ML:
					{
						p[2] = (float)Math.Abs(dw * Math.Cos(-t) + dh * Math.Sin(-t)) / 2.0f;
						p[0] = fix.X - p[2] * (float)Math.Cos(t);
						p[1] = fix.Y + p[2] * (float)Math.Sin(t);

					} break;
				case REGION_CONTROL_STYLES.MR:
					{
						p[2] = (float)Math.Abs(dw * Math.Cos(-t) + dh * Math.Sin(-t)) / 2.0f;
						p[0] = fix.X + p[2] * (float)Math.Cos(t);
						p[1] = fix.Y - p[2] * (float)Math.Sin(t);

					} break;
				case REGION_CONTROL_STYLES.TL:
				case REGION_CONTROL_STYLES.TR:
				case REGION_CONTROL_STYLES.BL:
				case REGION_CONTROL_STYLES.BR:
					{
						p[0] = (fix.X + fX) / 2.0f;
						p[1] = (fix.Y + fY) / 2.0f;
						p[2] = (float)Math.Abs(dw * Math.Cos(-t) + dh * Math.Sin(-t)) / 2.0f;
						p[3] = (float)Math.Abs(-dw * Math.Sin(-t) + dh * Math.Cos(-t)) / 2.0f;
						if (m_eRShape == SHAPE.ELLIPSE)
						{
							dw = (float)(dw * Math.Cos(Math.PI / 4));
							dh = (float)(dh * Math.Sin(Math.PI / 4));

							p[2] = (float)Math.Abs(dw * Math.Cos(-t) + dh * Math.Sin(-t));
							p[3] = (float)Math.Abs(-dw * Math.Sin(-t) + dh * Math.Cos(-t));
						}
					} break;
			}
		}

		public void SetAlign()
		{
			m_RegionAlign.SetAlign();
		}

		public void SetAlign(XRegion TargetRegion, CONTROL_POSITION AlignTarget, CONTROL_POSITION AlignObject)
		{
			if (m_RCS != REGION_CONTROL_STYLES.NONE)
			{
				if (TargetRegion != null)
				{
					if (TargetRegion.m_RegionAlign.TargetRegion != this)
					{
						m_RegionAlign.SetAlign(TargetRegion, AlignTarget, AlignObject);
						Align();
					}
				}
				else
				{
					m_RegionAlign.SetAlign();
				}
			}
		}

		public CONTROL_POSITION GetControlPositon(REGION_CONTROL_STYLES ControlStyle)
		{
			switch (ControlStyle)
			{
				case REGION_CONTROL_STYLES.TL: return CONTROL_POSITION.TL;
				case REGION_CONTROL_STYLES.TR: return CONTROL_POSITION.TR;
				case REGION_CONTROL_STYLES.BR: return CONTROL_POSITION.BR;
				case REGION_CONTROL_STYLES.BL: return CONTROL_POSITION.BL;
				case REGION_CONTROL_STYLES.TC: return CONTROL_POSITION.TC;
				case REGION_CONTROL_STYLES.MR: return CONTROL_POSITION.MR;
				case REGION_CONTROL_STYLES.BC: return CONTROL_POSITION.BC;
				case REGION_CONTROL_STYLES.ML: return CONTROL_POSITION.ML;
				case REGION_CONTROL_STYLES.MC: return CONTROL_POSITION.MC;
				case REGION_CONTROL_STYLES.TT: return CONTROL_POSITION.TT;
			}
			return CONTROL_POSITION.MC;
		}

		public void SetAlign(XRegion TargetRegion, REGION_CONTROL_STYLES AlignTargetStyles)
		{
			if ((m_RCS & AlignTargetStyles) != 0)
			{
				if (TargetRegion.m_RegionAlign.TargetRegion != this)
				{
					m_RegionAlign.SetAlign(TargetRegion, GetControlPositon(AlignTargetStyles), GetControlPositon(m_ObjectAlignRCS));
					Align();
				}
			}
			else
			{
				m_RegionAlign.SetAlign(null, CONTROL_POSITION.TT, CONTROL_POSITION.TT);
			}
		}

		public void Align()
		{
			if (m_RegionAlign != null && m_RegionAlign.TargetRegion != null)
			{
				m_RegionAlign.TargetRegion.Align();

				PointF[] TargetCp = m_RegionAlign.TargetRegion.GetCP();
				PointF[] ObjectCp = GetCP();

				if (TargetCp != null)
				{
					PointF tCp = TargetCp[(int)m_RegionAlign.AlignTarget];
					PointF oCp = ObjectCp[(int)m_RegionAlign.AlignObject];
					XVector Offset = m_RegionAlign.Offset;
					p[0] = (float)(p[0] - oCp.X + tCp.X + Offset.X);
					p[1] = (float)(p[1] - oCp.Y + tCp.Y + Offset.Y);
				}
			}
		}
	}

	public class RegionAlign
	{
		public XVector Offset = new XVector();
		public CONTROL_POSITION AlignTarget;
		public CONTROL_POSITION AlignObject;
		public XRegion TargetRegion;
		public RegionAlign()
		{
			AlignTarget = CONTROL_POSITION.TT;
			AlignObject = CONTROL_POSITION.TT;
			TargetRegion = null;
		}
		public void SetAlign()
		{
			AlignTarget = CONTROL_POSITION.TT;
			AlignObject = CONTROL_POSITION.TT;
			TargetRegion = null;
		}
		public void SetAlign(XRegion iTargetRegion, CONTROL_POSITION iAlignTarget, CONTROL_POSITION iAlignObject)
		{
			AlignTarget = iAlignTarget;
			AlignObject = iAlignObject;
			TargetRegion = iTargetRegion;
		}
	}

	public class TRegion
	{
		public int m_nID;
		public SHAPE m_eRShape;
		public float[] p;
		public int m_cBorderColor;
		public int m_cFillColor;
		public string m_sFillImagePath;
		public DashStyle m_dBorderStyle;
		public uint m_RCS;
		public string m_sCaption;
		public CONTROL_POSITION m_eAlignTarget;
		public CONTROL_POSITION m_eAlignObject;
		public int m_TargetRegionID;

		public TRegion()
		{
			p = new float[5] { 0, 0, 0, 0, 0 };
		}

		public void Copy(XRegion Item)
		{
			m_nID = Item.ID;
			m_eRShape = Item.RegionShape;
			Array.Copy(Item.Get(), p, 5);
			m_cBorderColor = Item.BorderColor.ToArgb();
			m_cFillColor = Item.FillColor.ToArgb();
			m_RCS = (uint)Item.ControlStyle;
			m_dBorderStyle = Item.BorderStyle;
			m_sCaption = Item.Caption;

			RegionAlign RAlign = Item.RegionAlign;
			if (RAlign != null)
			{
				m_eAlignTarget = RAlign.AlignTarget;
				m_eAlignObject = RAlign.AlignObject;
				if (RAlign.TargetRegion != null)
				{
					m_TargetRegionID = RAlign.TargetRegion.ID;
				}
			}
		}
	}
}
