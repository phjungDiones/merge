using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.WinControls
{
	public sealed class UiControlRenderer
	{
		#region Common Method

		/// <summary>
		/// 해당 영역에 코너가 둥근 사각형 패스를 만든다.
		/// </summary>
		/// <param name="bounds">그릴 영역</param>
		/// <param name="radius">코너 반지름</param>
		/// <returns>생성된 Path</returns>
		private static GraphicsPath CreateRoundPath(Rectangle bounds, int radius)
		{
			GraphicsPath path = new GraphicsPath();

			// bottom Left
			path.AddArc(bounds.X, bounds.Height + bounds.Y - radius, radius, radius, 90, 90);

			// top left
			path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);

			// top right
			path.AddArc(bounds.Width + bounds.X - radius, bounds.Y, radius, radius, 270, 90);

			// bottom right
			path.AddArc(bounds.Width + bounds.X - radius, bounds.Height + bounds.Y - radius, radius, radius, 0, 90);

			// 마무리 선 연결
			path.CloseAllFigures();

			return path;
		}
		#endregion

		#region Draw Parent Background
		/// <summary>
		/// 자식 컨트롤의 부모를 배경으로 그린다. (투명 효과 낼때.. 배경이 보여야 할때..)
		/// </summary>
		/// <param name="g">배경을 그릴 때 사용되는 System.Drawing.Graphics입니다.</param>
		/// <param name="bounds">배경의 범위를 지정하는 System.Drawing.Rectangle입니다.</param>
		/// <param name="childControl">자식 컨트롤 입니다.</param>
		public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			ButtonRenderer.DrawParentBackground(g, bounds, childControl);
		}
		#endregion

		#region Draw Round Button

		#region Round Button Draw Option

		/// <summary>
		/// 버튼 코너 반지름
		/// </summary>
		private static int RoundButtonRadius = 6;
		/// <summary>
		/// 버튼 테두리 색상
		/// </summary>
		private static Color RoundButtonBorderColor = Color.FromArgb(128, 0, 0, 0);
		/// <summary>
		/// 버튼 그라디에이션 밝은 색상
		/// </summary>
		private static Color RoundButtonGradientBrightColor = Color.FromArgb(64, 255, 255, 255);
		/// <summary>
		/// 버튼 그라디에이션 어두운 색상
		/// </summary>
		private static Color RoundButtonGradientDarkColor = Color.FromArgb(64, 0, 0, 0);
		/// <summary>
		/// 버튼 눌렸을때 그림자 색상
		/// </summary>
		private static Color RoundButtonPressShadowColor = Color.FromArgb(128, 32, 32, 32);
		/// <summary>
		/// 버튼위로 마우스 올라갔을때 오버레이되는 어두운 색상
		/// </summary>
		private static Color RoundButtonHotDarkOverayColor = Color.FromArgb(16, 0, 0, 0);
		/// <summary>
		/// 버튼 위로 마우스 올라갔을때 오버레이되는 밝은 색상
		/// </summary>
		private static Color RoundButtonHotBrightOverayColor = Color.FromArgb(16, 255, 255, 255);
		/// <summary>
		/// 버튼 비활성화 시 오버레이되는 밝은 색상
		/// </summary>
		private static Color RoundButtonDisabledBrightOverayColor = Color.FromArgb(64, 128, 128, 128);
		/// <summary>
		/// 버튼 비활성화 시 오버레이되는 어두운 색상
		/// </summary>
		private static Color RoundButtonDisabledDarkOverayColor = Color.FromArgb(64, 192, 192, 192);
		/// <summary>
		/// 버튼 포커스 되었을때 테두리 선 색상
		/// </summary>
		private static Color RoundButtonFocusedBorderLineColor = Color.FromArgb(0, 0, 0, 0);

		#endregion
		/// <summary>
		/// 지정된 상태, 범위를 사용하여 버튼 컨트롤을 그립니다.
		/// </summary>
		/// <param name="g">버튼를 그릴 때 사용되는 System.Drawing.Graphics입니다.</param>
		/// <param name="bounds">버튼의 범위를 지정하는 System.Drawing.Rectangle입니다.</param>
		/// <param name="backColor">버튼의 배경색을 지정하는 System.Drawing.Color입니다.</param>                
		/// <param name="focused">버튼에 포커스 사각형을 그리려면 true이고, 그렇지 않으면 false입니다.</param>
		/// <param name="pressed">버튼이 눌린 상태라면 true이고, 그렇지 않으면 false입니다.</param>
		/// <param name="hot">버튼에 마우스가 올려진 상태라면 true이고, 그렇지 않으면 false입니다.</param>
		/// <param name="enabled">버튼이 활성화 된 상태라면 true이고, 그렇지 않으면 false입니다.</param>
		public static void DrawRoundButton(Graphics g, Rectangle bounds, Color backColor, bool focused, bool pressed, bool hot, bool enabled, bool gradient, LinearGradientMode linearGradientMode)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			bounds.Width--;
			bounds.Height--;
			int radius = UiControlRenderer.RoundButtonRadius;
			using (GraphicsPath path = CreateRoundPath(bounds, radius))
			using (Pen linePen = new Pen(Color.Transparent, 1.0f))
			using (SolidBrush brush = new SolidBrush(backColor))
			{
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.FillPath(brush, path);
				Color brightGradientColor = pressed ? UiControlRenderer.RoundButtonGradientDarkColor : UiControlRenderer.RoundButtonGradientBrightColor;
				Color darkGradientColor = pressed ? UiControlRenderer.RoundButtonGradientBrightColor : UiControlRenderer.RoundButtonGradientDarkColor;
				if (gradient)
				{
					using (LinearGradientBrush gradientBrush = new LinearGradientBrush(bounds, brightGradientColor, darkGradientColor, linearGradientMode))
					{
						g.FillPath(gradientBrush, path);
					}
				}
				if (!enabled)
				{
					brush.Color = backColor.GetBrightness() < 0.5 ? UiControlRenderer.RoundButtonDisabledBrightOverayColor : UiControlRenderer.RoundButtonDisabledDarkOverayColor;
					g.FillPath(brush, path);
				}
				else
				{
					if (pressed)
					{
						linePen.Color = UiControlRenderer.RoundButtonPressShadowColor;
						Rectangle pressBounds = bounds;
						pressBounds.X++;
						pressBounds.Y++;
						using (GraphicsPath pressPath = CreateRoundPath(pressBounds, radius))
						{
							g.DrawPath(linePen, pressPath);
						}
					}
					if (hot)
					{
						brush.Color = backColor.GetBrightness() < 0.5 ? UiControlRenderer.RoundButtonHotBrightOverayColor : UiControlRenderer.RoundButtonHotDarkOverayColor;
						g.FillPath(brush, path);
					}
				}
				linePen.Color = UiControlRenderer.RoundButtonBorderColor;
				g.DrawPath(linePen, path);
				if (focused)
				{
					linePen.DashStyle = DashStyle.Dash;
					linePen.Color = UiControlRenderer.RoundButtonFocusedBorderLineColor;
					g.DrawPath(linePen, path);
				}
				g.SmoothingMode = SmoothingMode.None;
			}
		}


		#endregion

		#region Draw Check Box
		#region Draw Check Box Options
		public static Color CheckBoxDarkBorderColor = Color.FromArgb(64, 0, 0, 0);
		public static Color CheckBoxGradientBrightColor = Color.FromArgb(64, 255, 255, 255);
		public static Color CheckBoxGradientDarkColor = Color.FromArgb(64, 0, 0, 0);
		#endregion
		public static Rectangle GetCheckBoxBounds(Rectangle bounds, Size checkBoxSize, Padding padding, System.Drawing.ContentAlignment checkAlign)
		{
			Rectangle rect = bounds;
			rect.Width = checkBoxSize.Width == 0 ? 12 : checkBoxSize.Width;
			rect.Height = checkBoxSize.Height == 0 ? 8 : checkBoxSize.Height;

			if ((checkAlign & (System.Drawing.ContentAlignment.MiddleLeft | System.Drawing.ContentAlignment.MiddleCenter | System.Drawing.ContentAlignment.MiddleRight)) == checkAlign)
			{
				// Middle
				rect.Y += (bounds.Height - rect.Height) / 2;

			}
			else if ((checkAlign & (System.Drawing.ContentAlignment.TopLeft | System.Drawing.ContentAlignment.TopCenter | System.Drawing.ContentAlignment.TopRight)) == checkAlign)
			{
				// Top
				rect.Y += padding.Top;
			}
			else
			{
				// bottom
				rect.Y += bounds.Height - rect.Height - padding.Bottom;
			}


			if ((checkAlign & (System.Drawing.ContentAlignment.TopLeft | System.Drawing.ContentAlignment.MiddleLeft | System.Drawing.ContentAlignment.BottomLeft)) == checkAlign)
			{
				rect.X += padding.Left;
				// Left
			}
			else if ((checkAlign & (System.Drawing.ContentAlignment.TopRight | System.Drawing.ContentAlignment.MiddleRight | System.Drawing.ContentAlignment.BottomRight)) == checkAlign)
			{
				// Right
				rect.X += bounds.Width - padding.Right - rect.Width;
			}
			else
			{
				// Center
				rect.X += (bounds.Width - rect.Width) / 2;
			}
			return rect;
		}
		public static void DrawCheckBox(Graphics g, Rectangle bounds, Color backColor)
		{
			using (Pen pen = new Pen(Brushes.Black, 1.0f))
			using (Brush gradientBrush = new LinearGradientBrush(bounds, UiControlRenderer.CheckBoxGradientBrightColor, UiControlRenderer.CheckBoxGradientDarkColor, LinearGradientMode.Vertical))
			using (Brush brush = new SolidBrush(backColor))
			{
				g.FillRectangle(brush, bounds);
				g.FillRectangle(gradientBrush, bounds);
				pen.Color = UiControlRenderer.CheckBoxDarkBorderColor;
				pen.Width = 2.0f;
				g.DrawRectangle(pen, bounds);
				pen.Color = Color.Black;
				pen.Width = 1.0f;
				g.DrawRectangle(pen, bounds);
			}
		}

		#endregion

		#region Draw ComboBox
		#region Draw ComboBox Option
		public static int ComboBoxRoundRadius = 4;
		#endregion

		public static void DrawRoundComboBoxText(Graphics g, Rectangle bounds, Color backColor, bool isGradient)
		{
			g.SmoothingMode = SmoothingMode.HighQuality;
			//Rectangle textBounds = bounds;
			Rectangle dropDownButtonBounds = bounds;
			Size dropDownButtonSize = GetComboBoxButtonSize(bounds);

			dropDownButtonBounds.Size = dropDownButtonSize;
			dropDownButtonBounds.X = bounds.X + bounds.Width - dropDownButtonSize.Width;


			using (GraphicsPath dropDownPath = CreateRoundPath(dropDownButtonBounds, ComboBoxRoundRadius))
			using (GraphicsPath trianglePAth = new GraphicsPath())
			using (GraphicsPath textBoxPath = CreateRoundPath(bounds, ComboBoxRoundRadius))
			using (SolidBrush brush = new SolidBrush(backColor))
			using (Pen pen = new Pen(Color.FromArgb(128, 0, 0, 0), 1.0f))
			{

				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				g.FillPath(brush, textBoxPath);



				int gap = 4;
				trianglePAth.AddLine(dropDownButtonBounds.X + gap, dropDownButtonBounds.Y + dropDownButtonBounds.Height / 2, dropDownButtonBounds.X + dropDownButtonBounds.Width - gap, dropDownButtonBounds.Y + dropDownButtonBounds.Height / 2);
				trianglePAth.AddLine(dropDownButtonBounds.X + dropDownButtonBounds.Width - gap, dropDownButtonBounds.Y + dropDownButtonBounds.Height / 2, dropDownButtonBounds.X + (dropDownButtonBounds.Width / 2), dropDownButtonBounds.Y + dropDownButtonBounds.Height - gap);

				trianglePAth.CloseAllFigures();
				brush.Color = Color.White;
				pen.Color = Color.Black;
				g.FillPath(brush, trianglePAth);
				using (LinearGradientBrush triangleBrush = new LinearGradientBrush(dropDownButtonBounds, Color.FromArgb(128, 255, 255, 255), Color.FromArgb(128, 0, 0, 0), LinearGradientMode.Vertical))
				{
					g.FillPath(triangleBrush, trianglePAth);
				}
				g.DrawPath(pen, trianglePAth);
				if (isGradient)
				{
					using (LinearGradientBrush gradientBrush = new LinearGradientBrush(bounds, Color.FromArgb(64, 255, 255, 255), Color.FromArgb(64, 0, 0, 0), LinearGradientMode.Vertical))
					{
						g.FillPath(gradientBrush, textBoxPath);
					}
				}
				g.DrawLine(pen, dropDownButtonBounds.X - 1, dropDownButtonBounds.Y, dropDownButtonBounds.X - 1, bounds.Height);
				//g.DrawPath(pen, dropDownPath);

			}
		}
		public static void DrawComboBoxText(Graphics g, Rectangle bounds, Color backColor, bool isGradient)
		{
			g.SmoothingMode = SmoothingMode.HighQuality;
			Rectangle dropDownButtonBounds = bounds;
			Size dropDownButtonSize = GetComboBoxButtonSize(bounds);
			dropDownButtonBounds.Size = dropDownButtonSize;
			dropDownButtonBounds.X = bounds.X + bounds.Width - dropDownButtonSize.Width;

			using (GraphicsPath trianglePAth = new GraphicsPath())
			using (SolidBrush brush = new SolidBrush(backColor))
			using (Pen pen = new Pen(Color.FromArgb(128, 0, 0, 0), 1.0f))
			{
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				g.FillRectangle(brush, bounds);

				int gap = 4;
				trianglePAth.AddLine(dropDownButtonBounds.X + gap, dropDownButtonBounds.Y + dropDownButtonBounds.Height / 2, dropDownButtonBounds.X + dropDownButtonBounds.Width - gap, dropDownButtonBounds.Y + dropDownButtonBounds.Height / 2);
				trianglePAth.AddLine(dropDownButtonBounds.X + dropDownButtonBounds.Width - gap, dropDownButtonBounds.Y + dropDownButtonBounds.Height / 2, dropDownButtonBounds.X + (dropDownButtonBounds.Width / 2), dropDownButtonBounds.Y + dropDownButtonBounds.Height - gap);

				trianglePAth.CloseAllFigures();
				brush.Color = Color.White;
				pen.Color = Color.Black;
				g.FillPath(brush, trianglePAth);
				using (LinearGradientBrush triangleBrush = new LinearGradientBrush(dropDownButtonBounds, Color.FromArgb(128, 255, 255, 255), Color.FromArgb(128, 0, 0, 0), LinearGradientMode.Vertical))
				{
					g.FillPath(triangleBrush, trianglePAth);
				}
				g.DrawPath(pen, trianglePAth);
				if (isGradient)
				{
					using (LinearGradientBrush gradientBrush = new LinearGradientBrush(bounds, Color.FromArgb(64, 255, 255, 255), Color.FromArgb(64, 0, 0, 0), LinearGradientMode.Vertical))
					{
						g.FillRectangle(gradientBrush, bounds);
					}
				}
				g.DrawLine(pen, dropDownButtonBounds.X - 1, dropDownButtonBounds.Y, dropDownButtonBounds.X - 1, bounds.Height);
			}
		}
		public static Size GetComboBoxButtonSize(Rectangle bounds)
		{
			return new Size(bounds.Height - 4, bounds.Height);
		}
		#endregion
	}
}
