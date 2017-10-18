using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CJ_Controls.WinControls.Config
{
	public partial class Ctrl_Set_ComboBox : UserControl
	{
		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
		public static extern int SendMessage(IntPtr hwnd, uint Msg, int wParam, int lParam);

		public Ctrl_Set_ComboBox()
		{
			InitializeComponent();
		}

		#region Event
		public event EventHandler Change_SelectItem_Value;
		#endregion

		#region 속성
		[Category("Option Name"), Description("Option Name")]
		public string OptionName
		{
			get { return Label_Name.Text; }
			set { Label_Name.Text = value; }
		}

		[Category("Option Name"), Description("Text Alignment")]
		public System.Drawing.ContentAlignment TextAlign
		{
			get { return Label_Name.TextAlign; }
			set { Label_Name.TextAlign = value; }
		}

		[Category("Option Value"), Description("Value Items")]
		public string[] Items
		{
			get { return Combo_Value.DataSource as string[]; }
			set { Combo_Value.DataSource = value; }
		}
		[Category("Option Value"), Description("Value SelectedIndex")]
		public object SelectedItem
		{
			get { return Combo_Value.SelectedItem; }
			set { Combo_Value.SelectedItem = value; }
		}
		[Category("Option Value"), Description("Value SelectedIndex")]
		public int SelectedIndex
		{
			get { return Combo_Value.SelectedIndex; }
			set { Combo_Value.SelectedIndex = value; }
		}
		[Category("Option Value"), Description("Value Count")]
		public int Count
		{
			get { return Combo_Value.Items.Count; }
		}

		[Category("Option Value"), Description("Value Width Size")]
		public int Value_Width
		{
			get { return Panel_Value.Width; }
			set { Panel_Value.Width = value; }
		}

		//private int _nItem_Height = 30;
		[Category("Option Value"), Description("Value Item Height Size")]
		public int Item_Height
		{
			get { return Combo_Value.ItemHeight; }
			set
			{
				Combo_Value.ItemHeight = value;
				SetCombobox_Height(Panel_Value.Height - 6);
			}
		}
		
		private void SetCombobox_Height(int nHeight)
		{
			uint nCB_SETITEMHEIGHT = 0x0153;
			SendMessage(Combo_Value.Handle, nCB_SETITEMHEIGHT, -1, nHeight);
			Combo_Value.Refresh();
		}

		public void SetItems(string[] _Items)
		{
			this.BeginInvoke(new MethodInvoker(delegate()
			{
				Combo_Value.DataSource = _Items;
			}));
		}
		#endregion 속성

		private void Ctrl_Set_ComboBox_Resize(object sender, EventArgs e)
		{
			SetCombobox_Height(Panel_Value.Height - 6);
		}

		private void Combo_Value_DrawItem(object sender, DrawItemEventArgs e)
		{
			ComboBox _Cmb = sender as ComboBox;

			// Draw each string in the array, using a different size, color,
			// and font for each item.
			SolidBrush backBrush = new SolidBrush(_Cmb.BackColor);
			SolidBrush foreBrush = new SolidBrush(_Cmb.ForeColor);

			//배경색 채우기
			e.Graphics.FillRectangle(backBrush, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

			// Draw the background of the item.
			e.DrawBackground();

			//아이템 텍스트 Draw
			//e.Graphics.DrawString(((ComboBox)sender).Items[e.Index].ToString(), this.Font, foreBrush, e.Bounds.X, e.Bounds.Y);
			
			//아이템 텍스트 Draw 가운데 정렬.
			Rectangle drawRect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			e.Graphics.DrawString(((ComboBox)sender).Items[e.Index].ToString(), _Cmb.Font, foreBrush, drawRect, stringFormat);
			
			//리소스 해제
			if (backBrush != null)
				backBrush.Dispose();

			if (foreBrush != null)
				foreBrush.Dispose();

			// Draw the focus rectangle if the mouse hovers over an item.
			e.DrawFocusRectangle();
		}

		private void Combo_Value_SelectedValueChanged(object sender, EventArgs e)
		{
			if (Change_SelectItem_Value != null)
			{
				Change_SelectItem_Value(this, EventArgs.Empty);
			}
		}
	}
}
