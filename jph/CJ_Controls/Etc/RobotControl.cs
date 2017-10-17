using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.Etc
{
	public partial class RobotControl : UserControl
	{
		#region Enum
		public enum ROBOT_STATUS
		{
			NORMAL,
			ACTION_LEFT_HAND,
			ACTION_RIGHT_HAND,
		}
		public enum ROBOT_DIRECTION
		{
			UP = 0,
			DOWN = 180,
			LEFT = 270,
			RIGHT = 90,
		}
		#endregion

		#region Field

		private const int Control_XY_Size = 70;
		private const int GLASS_SIZE = 20;

		private bool _LeftGlassVisible = true;
		private bool _RightGalssVisible = true;

		private string _LeftGlassText = string.Empty;
		private string _RightGlassText = string.Empty;

		private ROBOT_STATUS _RobotStatus = ROBOT_STATUS.NORMAL;
		private ROBOT_DIRECTION _RobotDirection = ROBOT_DIRECTION.UP;
		private Color _GlassColor = Color.FromArgb(200, 128, 255, 255);

		private SolidBrush GLASS_BRUSH = new SolidBrush(Color.FromArgb(200, 128, 255, 255));
		Bitmap _DrawBitMap = new Bitmap(Control_XY_Size, Control_XY_Size);
		#endregion

		#region Property
		public ROBOT_DIRECTION RobotDirection
		{
			get { return _RobotDirection; }
			set
			{
				if (_RobotDirection == value)
				{
					return;
				}
				_RobotDirection = value;
				Invalidate();
			}
		}
		public string LeftGlassText
		{
			get { return _LeftGlassText; }
			set
			{
				if (_LeftGlassText == value)
				{
					return;
				}
				_LeftGlassText = value;
				Invalidate();
			}
		}
		public string RightGlassText
		{
			get { return _RightGlassText; }
			set
			{
				if (_RightGlassText == value)
				{
					return;
				}
				_RightGlassText = value;
				Invalidate();
			}
		}

		public ROBOT_STATUS RobotStatus
		{
			get { return _RobotStatus; }
			set
			{
				SetRobotStatus(value);
				if (_RobotStatus == value)
				{
					return;
				}
				_RobotStatus = value;
				Invalidate();
			}
		}
		private Point LowerGlassLocation { get; set; }
		private Point UpperGlassLocation { get; set; }
		public bool LeftGlassVisible
		{
			get { return _LeftGlassVisible; }
			set
			{
				if (_LeftGlassVisible == value)
				{
					return;
				}
				_LeftGlassVisible = value;
				Invalidate();
			}
		}
		public bool RightGalssVisible
		{
			get { return _RightGalssVisible; }
			set
			{
				if (_RightGalssVisible == value)
				{
					return;
				}
				_RightGalssVisible = value;
				Invalidate();
			}
		}
		private StringFormat GlassStringFormat { get; set; }
		public Color GlassColor
		{
			get { return _GlassColor; }
			set
			{
				if (_GlassColor == value)
				{
					return;
				}
				_GlassColor = value;
				Invalidate();
			}
		}
		#endregion

		public RobotControl()
		{
			InitializeComponent();
			this.GlassStringFormat = new StringFormat();
			this.GlassStringFormat.Alignment = StringAlignment.Center;
			this.GlassStringFormat.LineAlignment = StringAlignment.Near;

			this.LeftGlassVisible = true;
			this.RightGalssVisible = true;
			this.RobotDirection = ROBOT_DIRECTION.UP;
			this.RobotStatus = ROBOT_STATUS.NORMAL;
		}
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, Control_XY_Size, Control_XY_Size, specified);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			using (Graphics gfx = Graphics.FromImage(_DrawBitMap))
			{
				int nSize_Harf = Control_XY_Size / 2;
				gfx.Clear(this.BackColor);
				gfx.TranslateTransform(nSize_Harf, nSize_Harf);
				gfx.RotateTransform((float)this.RobotDirection);
				gfx.TranslateTransform(-nSize_Harf, -nSize_Harf);

				gfx.DrawImage(this.imageList1.Images[(int)this.RobotStatus], this.ClientRectangle);
				gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
				if (this.LeftGlassVisible)
				{
					Rectangle glassRect = new Rectangle(this.LowerGlassLocation, new Size(GLASS_SIZE, GLASS_SIZE));
					GLASS_BRUSH.Color = this.GlassColor;
					//gfx.FillRectangle(GLASS_BRUSH, glassRect);
					gfx.FillEllipse(GLASS_BRUSH, glassRect);
					ConvertGlassRect(this.RobotDirection, ref glassRect);
					TextRenderer.DrawText(gfx, this.LeftGlassText, this.Font, glassRect, this.BackColor, Color.Transparent, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak);
				}

				if (this.RightGalssVisible)
				{
					Rectangle glassRect = new Rectangle(this.UpperGlassLocation, new Size(GLASS_SIZE, GLASS_SIZE));
					//gfx.FillRectangle(GLASS_BRUSH, glassRect);
					gfx.FillEllipse(GLASS_BRUSH, glassRect);
					ConvertGlassRect(this.RobotDirection, ref glassRect);
					TextRenderer.DrawText(gfx, this.RightGlassText, this.Font, glassRect, this.BackColor, Color.Transparent, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak);
				}
				e.Graphics.DrawImage(_DrawBitMap, 0, 0);

			}
		}
		private void ConvertGlassRect(ROBOT_DIRECTION position, ref Rectangle rect)
		{
			int nSize_Harf = Control_XY_Size / 2;
			rect.Location = RotatePoint(rect.Location, new Point(nSize_Harf, nSize_Harf), (float)this.RobotDirection);
			switch (position)
			{
				case ROBOT_DIRECTION.DOWN:
					rect.X -= rect.Width;
					rect.Y -= rect.Height;
					break;
				case ROBOT_DIRECTION.LEFT:
					rect.Y -= rect.Height;
					break;
				case ROBOT_DIRECTION.RIGHT:
					rect.X -= rect.Width;
					break;
				default:
					break;

			}
		}
		private Point RotatePoint(Point pointToRotate, Point centerPoint, float angle)
		{
			double radians = angle * (Math.PI / 180);
			double cos = Math.Cos(radians);
			double sin = Math.Sin(radians);
			return new Point
			{
				X = (int)(cos * (pointToRotate.X - centerPoint.X) - sin * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
				Y = (int)(sin * (pointToRotate.X - centerPoint.X) + cos * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
			};
		}
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			//base.OnPaintBackground(e);
		}

		private void SetRobotStatus(ROBOT_STATUS robotStatus)
		{
			int nSize_20_1 = Control_XY_Size / 20;
			switch (robotStatus)
			{
				case ROBOT_STATUS.NORMAL:
					LowerGlassLocation = new Point(nSize_20_1 * 4 + 2, nSize_20_1 * 9 + 1);
					UpperGlassLocation = new Point(nSize_20_1 * 12, nSize_20_1 * 9 + 1);
					break;
				case ROBOT_STATUS.ACTION_LEFT_HAND:
					LowerGlassLocation = new Point(nSize_20_1 * 4 + 2, nSize_20_1 * 1 - 2);
					UpperGlassLocation = new Point(nSize_20_1 * 12, nSize_20_1 * 9 + 1);
					break;
				case ROBOT_STATUS.ACTION_RIGHT_HAND:
					LowerGlassLocation = new Point(nSize_20_1 * 4 + 2, nSize_20_1 * 9 + 1);
					UpperGlassLocation = new Point(nSize_20_1 * 12, nSize_20_1 * 1 - 2);
					break;
				default:
					break;
			}
		}
	}
}
