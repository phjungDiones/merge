using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.WinControls.Config
{
	public partial class Ctrl_Set_Number : UserControl
	{
		public Ctrl_Set_Number()
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

		[Category("Option Value"), Description("Maximum")]
		public Decimal Maximum
		{
			get { return Num_Value.Maximum; }
			set { Num_Value.Maximum = value; }
		}
		[Category("Option Value"), Description("Minimum")]
		public Decimal Minimum
		{
			get { return Num_Value.Minimum; }
			set { Num_Value.Minimum = value; }
		}
		[Category("Option Value"), Description("Value")]
		public Decimal Value_Num
		{
			get { return Num_Value.Value; }
			set { Num_Value.Value = value; }
		}
		[Category("Option Value"), Description("Alignment")]
		public System.Windows.Forms.HorizontalAlignment Value_Align
		{
			get { return Num_Value.TextAlign; }
			set { Num_Value.TextAlign = value; }
		}

		[Category("Option Value"), Description("Value Width Size")]
		public int Value_Width
		{
			get { return Panel_Value.Width; }
			set { Panel_Value.Width = value; }
		}
		#endregion 속성

	}
}
