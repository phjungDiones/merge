using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.Windows.Config
{
	public partial class Ctrl_Set_Boolean : UserControl
	{
		public Ctrl_Set_Boolean()
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

		[Category("Option Value"), Description("Value")]
		public bool Value_Checked
		{
			get { return CheckBox_Value.Checked; }
			set { CheckBox_Value.Checked = value; }
		}

		private string _CheckedText = "Checked";
		[Category("Option Value"), Description("Checked Properties")]
		public string Value_Checked_Text
		{
			get { return _CheckedText; }
			set
			{
				_CheckedText = value;
				if (CheckBox_Value.Checked == true)
					CheckBox_Value.Text = _CheckedText;
			}
		}

		private string _UnCheckedText = "UnChecked";
		[Category("Option Value"), Description("UnChecked Properties")]
		public string Value_UnChecked_Text
		{
			get { return _UnCheckedText; }
			set
			{
				_UnCheckedText = value;
				if (CheckBox_Value.Checked == false)
					CheckBox_Value.Text = _UnCheckedText;
			}
		}

		[Category("Option Value"), Description("Value Width Size")]
		public int Value_Width
		{
			get { return Panel_Value.Width; }
			set { Panel_Value.Width = value; }
		}
		#endregion 속성

		#region Event
		public event EventHandler Change_Check_Value;
		#endregion

		private void CheckBox_Value_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox _ChkBox = sender as CheckBox;
			if (_ChkBox.Checked)
			{
				_ChkBox.BackColor = Color.LimeGreen;
				_ChkBox.ForeColor = Color.Black;
				if (_ChkBox.Text != _CheckedText)
				{
					_ChkBox.Text = _CheckedText;
					if (Change_Check_Value != null)
					{
						Change_Check_Value(this, EventArgs.Empty);
					}
				}
			}
			else
			{
				_ChkBox.BackColor = Color.Red;
				_ChkBox.ForeColor = Color.Yellow;

				if (_ChkBox.Text != _UnCheckedText)
				{
					_ChkBox.Text = _UnCheckedText;
					if (Change_Check_Value != null)
					{
						Change_Check_Value(this, EventArgs.Empty);
					}
				}
			}
		}
	}
}
