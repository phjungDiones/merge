using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.WinControls.Config
{
	public partial class Ctrl_Set_String : UserControl
	{
		public Ctrl_Set_String()
		{
			InitializeComponent();
		}

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

		[Category("Option Value"), Description("PassWord Char")]
		public char PasswordChar
		{
			get { return Text_Value.PasswordChar; }
			set { Text_Value.PasswordChar = value; }
		}

		[Category("Option Value"), Description("Text")]
		public string Value_Text
		{
			get { return Text_Value.Text; }
			set { Text_Value.Text = value; }
		}
		[Category("Option Value"), Description("Alignment")]
		public System.Windows.Forms.HorizontalAlignment Value_Align
		{
			get { return Text_Value.TextAlign; }
			set { Text_Value.TextAlign = value; }
		}

		[Category("Option Value"), Description("Value Width Size")]
		public int Value_Width
		{
			get { return Panel_Value.Width; }
			set { Panel_Value.Width = value; }
		}
		#endregion 속성

		public event KeyEventHandler TextValue_KeyDown;
		private void Text_Value_KeyDown(object sender, KeyEventArgs e)
		{
			if (TextValue_KeyDown != null)
			{
				TextValue_KeyDown(sender, e);
			}
		}
	}
}
