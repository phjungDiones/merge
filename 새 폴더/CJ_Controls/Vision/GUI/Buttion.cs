using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CJ_Controls.Vision.BaseData;
namespace CJ_Controls.Vision.GUI
{
	public partial class Button : Control
	{
		public enum STATE
		{
			OFF = 0,
			ON = 1,
			MAX_BUTTON_STATE
		};
		public enum SHAPE
		{
			RECTANGLE = 0,	//Rectangle■
			ROUND = 7,//ROUND
			MAX_BUTTON_SHAPE
		};
		public enum STYLE
		{
			DRAW = 0x0,
			IMAGE = 0x1,
			PUSH = 0x10,
			TOGGLE = 0x20,
			MOUSE_UP = 0x100,

			DRAW_PUSH = PUSH,
			DRAW_PUSH_MOUSE_UP = PUSH + MOUSE_UP,

			DRAW_TOGGLE = TOGGLE,
			DRAW_TOGGLE_MOUSE_UP = TOGGLE + MOUSE_UP,

			IMAGE_PUSH = IMAGE + PUSH,
			IMAGE_PUSH_MOUSE_UP = IMAGE + PUSH + MOUSE_UP,

			IMAGE_TOGGLE = IMAGE + TOGGLE,
			IMAGE_TOGGLE_MOUSE_UP = IMAGE + TOGGLE + MOUSE_UP,

			MAX_BUTTON_STYLE
		};

		private StringFormat m_TextAlign = new StringFormat();
		private STATE m_eMouseUp;
		private int m_nRound;
		private STYLE m_eStyle;
		private SHAPE m_eShape;
		private STATE m_eValue;
		private STATE m_eState;

		private Color m_BorderColor;

		private Color m_MouseUpColor;
		private Color m_FillColor;
		private Color m_PushColor;

		private Image m_MouseUpImage;
		private Image m_FillImage;

		private Image m_StateOnImage;
		private Image m_StateOffImage;

		private Point m_pState;

		public Button()
		{
			m_TextAlign.Alignment = StringAlignment.Center;
			m_TextAlign.LineAlignment = StringAlignment.Center;
			m_eMouseUp = STATE.OFF;
			m_eShape = SHAPE.RECTANGLE;
			m_eStyle = STYLE.DRAW;
			m_eValue = STATE.OFF;
			m_nRound = 9;
			m_MouseUpColor = Color.OrangeRed;
			m_FillColor = Color.AliceBlue;
			m_BorderColor = Color.Blue;
			m_PushColor = Color.LightGray;

			InitializeComponent();
		}



		public Image MouseUpImage
		{
			get
			{
				return m_MouseUpImage;
			}
			set
			{
				m_MouseUpImage = value;
				Refresh();
			}
		}
		public Image FillImage
		{
			get
			{
				return m_FillImage;
			}
			set
			{
				m_FillImage = value;
				Refresh();
			}
		}

		public Image StateOnImage
		{
			get
			{
				return m_StateOnImage;
			}
			set
			{
				m_StateOnImage = value;
				Refresh();
			}
		}
		public Image StateOffImage
		{
			get
			{
				return m_StateOffImage;
			}
			set
			{
				m_StateOffImage = value;
				Refresh();
			}
		}

		public Point StatePos
		{
			get
			{
				return m_pState;
			}
			set
			{
				m_pState = value;
				Refresh();
			}
		}

		public int Round
		{
			get
			{
				return m_nRound;
			}
			set
			{
				m_nRound = value;
				Refresh();
			}
		}

		public Color PushColor
		{
			get
			{
				return m_PushColor;
			}
			set
			{
				m_PushColor = value;
				Refresh();
			}
		}

		public Color MouseUpColor
		{
			get
			{
				return m_MouseUpColor;
			}
			set
			{
				m_MouseUpColor = value;
				Refresh();
			}
		}
		public Color FillColor
		{
			get
			{
				return m_FillColor;
			}
			set
			{
				m_FillColor = value;
				Refresh();
			}
		}

		public Color BorderColor
		{
			get
			{
				return m_BorderColor;
			}
			set
			{
				m_BorderColor = value;
				Refresh();
			}
		}


		public STATE Value
		{
			get
			{
				return m_eValue;
			}
			set
			{
				m_eValue = value;
				Refresh();
			}
		}

		public STYLE Style
		{
			get
			{
				return m_eStyle;
			}
			set
			{
				m_eStyle = value;
				Refresh();
			}
		}
		public SHAPE Shape
		{
			get
			{
				return m_eShape;
			}
			set
			{
				m_eShape = value;
				Refresh();
			}
		}
		public STATE State
		{
			get
			{
				return m_eState;
			}
			set
			{
				m_eState = value;
				Refresh();
			}
		}



		public ContentAlignment TextAlign
		{
			get
			{
				return (ContentAlignment)((1 << ((int)m_TextAlign.LineAlignment * 4)) * (1 << (int)m_TextAlign.Alignment));
			}
			set
			{
				switch (value)
				{
					case ContentAlignment.TopLeft: m_TextAlign.LineAlignment = StringAlignment.Near; m_TextAlign.Alignment = StringAlignment.Near; break;
					case ContentAlignment.TopCenter: m_TextAlign.LineAlignment = StringAlignment.Near; m_TextAlign.Alignment = StringAlignment.Center; break;
					case ContentAlignment.TopRight: m_TextAlign.LineAlignment = StringAlignment.Near; m_TextAlign.Alignment = StringAlignment.Far; break;
					case ContentAlignment.MiddleLeft: m_TextAlign.LineAlignment = StringAlignment.Center; m_TextAlign.Alignment = StringAlignment.Near; break;
					case ContentAlignment.MiddleCenter: m_TextAlign.LineAlignment = StringAlignment.Center; m_TextAlign.Alignment = StringAlignment.Center; break;
					case ContentAlignment.MiddleRight: m_TextAlign.LineAlignment = StringAlignment.Center; m_TextAlign.Alignment = StringAlignment.Far; break;
					case ContentAlignment.BottomLeft: m_TextAlign.LineAlignment = StringAlignment.Far; m_TextAlign.Alignment = StringAlignment.Near; break;
					case ContentAlignment.BottomCenter: m_TextAlign.LineAlignment = StringAlignment.Far; m_TextAlign.Alignment = StringAlignment.Center; break;
					case ContentAlignment.BottomRight: m_TextAlign.LineAlignment = StringAlignment.Far; m_TextAlign.Alignment = StringAlignment.Far; break;
				}
				Refresh();
			}
		}

		protected override void OnTextChanged(EventArgs e)
		{
			Refresh();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Rectangle drawRect = pe.ClipRectangle;// new Rectangle(0, 0, Width, Height);

			if ((m_eStyle & STYLE.IMAGE) != 0)
			{

				OnPaintImage(pe.Graphics, drawRect);
			}
			else
			{
				switch (m_eShape)
				{

					case SHAPE.RECTANGLE: OnPaintDraw(pe.Graphics, drawRect); break;
					case SHAPE.ROUND: OnPaintRound(pe.Graphics, drawRect); break;
				}
			}

			OnPaintState(pe.Graphics, drawRect);

			if (Text != null && Text != "")
			{
				OnPaintText(pe.Graphics, drawRect);
			}

		}

		private void OnPaintText(Graphics gp, Rectangle Rect)
		{
			SolidBrush drawBrushFore = new SolidBrush(this.ForeColor);
			if (Text != null)
			{
				gp.DrawString(Text, this.Font, drawBrushFore, Rect, m_TextAlign);
			}
			drawBrushFore.Dispose();
		}
		private void OnPaintImage(Graphics gp, Rectangle Rect)
		{
			if (m_eValue == STATE.ON)
			{
				if (m_FillImage != null) gp.DrawImage(m_FillImage, Rect);
			}
			else
			{
				if ((m_eStyle & STYLE.MOUSE_UP) != 0 && m_eMouseUp == STATE.ON)
				{
					if (m_MouseUpImage != null) gp.DrawImage(m_MouseUpImage, Rect);
				}
				else
				{
					if (BackgroundImage != null) gp.DrawImage(BackgroundImage, Rect);
				}
			}
		}
		private void OnPaintDraw(Graphics gp, Rectangle Rect)
		{
			if (m_eValue == STATE.ON)
			{
				SolidBrush drawBrushFill = new SolidBrush(m_FillColor);
				gp.FillRectangle(drawBrushFill, Rect);
				drawBrushFill.Dispose();
				{//위쪽
					Pen BolderUpPen = new Pen(Color.DarkGray);
					gp.DrawLine(BolderUpPen, Rect.Left, Rect.Top, Rect.Right, Rect.Top);
					gp.DrawLine(BolderUpPen, Rect.Right, Rect.Top, Rect.Right, Rect.Bottom);
					BolderUpPen.Dispose();
				}
				{//아래쪽
					Pen BolderDnPen = new Pen(Color.White);
					gp.DrawLine(BolderDnPen, Rect.Left, Rect.Top, Rect.Left, Rect.Bottom);
					gp.DrawLine(BolderDnPen, Rect.Left, Rect.Bottom, Rect.Right, Rect.Bottom);
					BolderDnPen.Dispose();
				}
			}
			else
			{
				if ((m_eStyle & STYLE.MOUSE_UP) != 0 && m_eMouseUp == STATE.ON)
				{
					SolidBrush drawBrushMouseUp = new SolidBrush(m_MouseUpColor);
					gp.FillRectangle(drawBrushMouseUp, Rect);
					drawBrushMouseUp.Dispose();
				}
				else
				{
					SolidBrush drawBrushBack = new SolidBrush(BackColor);
					gp.FillRectangle(drawBrushBack, Rect);
					drawBrushBack.Dispose();
				}

				{//아래쪽
					Pen BolderDnPen = new Pen(Color.DarkGray);
					gp.DrawLine(BolderDnPen, Rect.Left, Rect.Top, Rect.Left, Rect.Bottom);
					gp.DrawLine(BolderDnPen, Rect.Left, Rect.Bottom, Rect.Right, Rect.Bottom);
					BolderDnPen.Dispose();
				}

				{//위쪽
					Pen BolderUpPen = new Pen(Color.White);
					gp.DrawLine(BolderUpPen, Rect.Left, Rect.Top, Rect.Right, Rect.Top);
					gp.DrawLine(BolderUpPen, Rect.Right, Rect.Top, Rect.Right, Rect.Bottom);
					BolderUpPen.Dispose();
				}
			}
		}

		private void OnPaintRound(Graphics gp, Rectangle Rect)
		{
			SolidBrush drawBrushBack = new SolidBrush(BackColor);
			gp.FillRectangle(drawBrushBack, Rect);
			drawBrushBack.Dispose();

			IntPtr hdc = gp.GetHdc();
			int nColor;
			if (m_eValue == STATE.ON)
			{
				nColor = GDI.RGB(m_PushColor.R, m_PushColor.G, m_PushColor.B);
			}
			else
			{
				nColor = GDI.RGB(m_FillColor.R, m_FillColor.G, m_FillColor.B);
			}
			IntPtr hBrush = GDI.CreateSolidBrush(nColor);
			IntPtr oBrush = GDI.SelectObject(hdc, hBrush);

			IntPtr hPen = GDI.CreatePen((int)GDI.PEN_STYLE.PS_SOLID, 1, GDI.RGB(m_BorderColor.R, m_BorderColor.G, m_BorderColor.B));
			IntPtr oPen = GDI.SelectObject(hdc, hPen);

			GDI.RoundRect(hdc, Rect.Left, Rect.Top, Rect.Right, Rect.Bottom, m_nRound, m_nRound);
			GDI.SelectObject(hdc, oPen);
			GDI.DeleteObject(hPen);
			GDI.SelectObject(hdc, oBrush);
			GDI.DeleteObject(hBrush);
			gp.ReleaseHdc(hdc);

			int nSpace = m_nRound / 2;
			Pen BolderPen;
			Pen LightPen;
			if (m_eValue == STATE.ON)
			{
				BolderPen = new Pen(Color.LightGray);
				LightPen = new Pen(Color.DarkGray);
				gp.DrawLine(LightPen, Rect.Left + nSpace, Rect.Top + 2, Rect.Right - nSpace, Rect.Top + 2);
				gp.DrawLine(LightPen, Rect.Right - 3, Rect.Top + nSpace, Rect.Right - 3, Rect.Bottom - nSpace);
				gp.DrawLine(LightPen, Rect.Left + 2, Rect.Top + nSpace, Rect.Left + 2, Rect.Bottom - nSpace);
				gp.DrawLine(LightPen, Rect.Left + nSpace, Rect.Bottom - 3, Rect.Right - nSpace, Rect.Bottom - 3);
				LightPen.Dispose();
			}
			else if ((m_eStyle & STYLE.MOUSE_UP) != 0 && m_eMouseUp == STATE.ON)
			{
				BolderPen = new Pen(m_MouseUpColor);
			}
			else
			{
				BolderPen = new Pen(Color.FromArgb((byte)(m_FillColor.R / 1.2), (byte)(m_FillColor.G / 1.2), (byte)(m_FillColor.B / 1.2)));
			}

			gp.DrawLine(BolderPen, Rect.Left + nSpace, Rect.Top + 1, Rect.Right - nSpace, Rect.Top + 1);
			gp.DrawLine(BolderPen, Rect.Right - 2, Rect.Top + nSpace, Rect.Right - 2, Rect.Bottom - nSpace);
			gp.DrawLine(BolderPen, Rect.Left + 1, Rect.Top + nSpace, Rect.Left + 1, Rect.Bottom - nSpace);
			gp.DrawLine(BolderPen, Rect.Left + nSpace, Rect.Bottom - 2, Rect.Right - nSpace, Rect.Bottom - 2);

			BolderPen.Dispose();
		}

		private void OnPaintState(Graphics gp, Rectangle Rect)
		{
			if (m_eState == STATE.ON)
			{
				if (m_StateOnImage != null && m_pState != null) gp.DrawImage(m_StateOnImage, m_pState);

			}
			else
			{
				if (m_StateOffImage != null && m_pState != null) gp.DrawImage(m_StateOffImage, m_pState);
			}

		}
		protected override void OnClick(EventArgs e)
		{

		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (Enabled == true)
			{
				if (e.Button == MouseButtons.Left)
				{
					Focus();
					if ((m_eStyle & STYLE.TOGGLE) != 0)
					{
						if (m_eValue == STATE.ON)
						{
							Value = STATE.OFF;
						}
						else
						{
							Value = STATE.ON;
						}
						base.OnClick(e);
					}
					else //if (Enabled == true && (m_eStyle & STYLE.TOGGLE) == 0)
					{
						Value = STATE.ON;
						base.OnClick(e);
					}
				}
			}

		}
		protected override void OnLostFocus(EventArgs e)
		{
			if (Enabled == true)
			{
				if ((m_eStyle & STYLE.TOGGLE) == 0)
				{
					if (m_eValue == STATE.ON)
					{
						Value = STATE.OFF;
					}
				}
			}
		}
		protected override void OnMouseEnter(EventArgs e)
		{
			if (Enabled == true)
			{
				if ((m_eStyle & STYLE.MOUSE_UP) != 0)
				{
					m_eMouseUp = STATE.ON;
					Refresh();
				}
			}
		}



		protected override void OnMouseLeave(EventArgs e)
		{
			if (Enabled == true)
			{
				if ((m_eStyle & STYLE.MOUSE_UP) != 0)
				{
					m_eMouseUp = STATE.OFF;
					Refresh();
				}
			}
		}



		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (Enabled == true)
			{
				if ((m_eStyle & STYLE.TOGGLE) == 0)
				{
					Value = STATE.OFF;
				}
			}
		}




	}

}
