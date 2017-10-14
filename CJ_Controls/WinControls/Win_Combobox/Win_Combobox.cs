using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.WinControls
{
	public class UiComboBox : ComboBox
	{
		private bool _GradientEnabled = false;
		private bool _RoundEnabled = false;

		public bool RoundEnabled
		{
			get { return _RoundEnabled; }
			set
			{
				if (_RoundEnabled == value)
				{
					return;
				}
				_RoundEnabled = value;
				Invalidate();
			}
		}

		public bool GradientEnabled
		{
			get { return _GradientEnabled; }
			set
			{
				if (_GradientEnabled == value)
				{
					return;
				}
				_GradientEnabled = value;
				Invalidate();
			}
		}
		public UiComboBox()
			: base()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.Opaque, false);
			this.SetAutoSizeMode(AutoSizeMode.GrowOnly);
			this.DrawMode = DrawMode.OwnerDrawVariable;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics gfx = e.Graphics;
			Rectangle bounds = this.ClientRectangle;
			bounds.Width--;
			bounds.Height--;
			if (this.RoundEnabled)
			{
				UiControlRenderer.DrawParentBackground(gfx, this.ClientRectangle, this);
				UiControlRenderer.DrawRoundComboBoxText(gfx, bounds, this.BackColor, this.GradientEnabled);
			}
			else
			{
				UiControlRenderer.DrawComboBoxText(gfx, bounds, this.BackColor, this.GradientEnabled);
			}
			Rectangle textBounds = this.ClientRectangle;
			textBounds.Width -= UiControlRenderer.GetComboBoxButtonSize(this.ClientRectangle).Width;
			TextRenderer.DrawText(gfx, GetItemText(this.SelectedItem), this.Font, textBounds, this.ForeColor, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
			e.Dispose();
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			Invalidate();
		}
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (e.Index < 0)
			{
				base.OnDrawItem(e);
				return;
			}
			Rectangle textBounds = e.Bounds;
			textBounds.X--;
			string text = this.GetItemText(this.Items[e.Index]);
			//textBounds.Width -= ViatronControlRenderer.GetComboBoxButtonSize(this.ClientRectangle).Width;            
			e.DrawBackground();
			TextRenderer.DrawText(e.Graphics, text, this.Font, textBounds, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
			base.OnDrawItem(e);
		}
	}
}
