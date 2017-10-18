using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.WinControls.Config
{
	public partial class Ctrl_Set_V_TouchInput : UserControl
	{
		public enum Input_Type
		{
			Keyboard = 0,
			KeyPad = 1,
		}
		public enum Return_Data_Type
		{
			Text = 0,
			Integer = 1,
			Float = 2,
		}

		public Ctrl_Set_V_TouchInput()
		{
			InitializeComponent();

			this.DoubleBuffered = true;
		}
		private void Ctrl_Set_V_TouchInput_Load(object sender, EventArgs e)
		{

		}

		#region Event
		public event EventHandler Change_Text_Value;
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

		[Category("Option Value"), Description("Text")]
		public string Value_Text
		{
			get { return Label_Value.Text; }
			set { Label_Value.Text = value; }
		}
		public int Value_Int
		{
			get
			{
				int _Rtn = 0;
				try
				{
					_Rtn = int.Parse(Label_Value.Text);
				}
				catch
				{
					_Rtn = 0;
				}
				return _Rtn;
			}
		}
		public float Value_Float
		{
			get
			{
				float _Rtn = 0;
				try
				{
					_Rtn = float.Parse(Label_Value.Text);
				}
				catch
				{
					_Rtn = 0;
				}
				return _Rtn;
			}
		}

		[Category("Option Value"), Description("Alignment")]
		public System.Drawing.ContentAlignment Value_Align
		{
			get { return Label_Value.TextAlign; }
			set { Label_Value.TextAlign = value; }
		}

		[Category("Option Value"), Description("Value Height Size")]
		public int Value_Height
		{
			get { return Panel_Value.Height; }
			set { Panel_Value.Height = value; }
		}

		[Category("Option Value"), Description("Input Type")]
		public Input_Type InputType
		{
			get { return m_KeyInputType; }
			set { m_KeyInputType = value; }
		}

		[Category("Option Value"), Description("Data Type")]
		public Return_Data_Type DataType
		{
			get;
			set;
		}

		[Category("Option Value"), Description("Read Only")]
		public bool Read_Only
		{
			get;
			set;
		}
		#endregion 속성

		private Input_Type m_KeyInputType = Input_Type.KeyPad;
		private void Label_Value_DoubleClick(object sender, EventArgs e)
		{
			if (Read_Only == true)
				return;

			string str = Label_Value.Text;
			if(m_KeyInputType == Input_Type.KeyPad)
				str = CJ_Controls.Class_Public.Instance.ShowKeyPad(this, str);
			else if (m_KeyInputType == Input_Type.Keyboard)
				str = CJ_Controls.Class_Public.Instance.ShowKeyBoard(this, str);

			if (Label_Value.Text != str)
			{
				Label_Value.Text = str;
				if (Change_Text_Value != null)
				{
					Change_Text_Value(this, EventArgs.Empty);
				}
			}
		}

		private void Label_Value_Click(object sender, EventArgs e)
		{
			if (Read_Only == true)
				return;

			string str = Label_Value.Text;
			if (m_KeyInputType == Input_Type.KeyPad)
				str = CJ_Controls.Class_Public.Instance.ShowKeyPad(this, str);
			else if (m_KeyInputType == Input_Type.Keyboard)
				str = CJ_Controls.Class_Public.Instance.ShowKeyBoard(this, str);

			if (Label_Value.Text != str)
			{
				Label_Value.Text = str;
				if (Change_Text_Value != null)
				{
					Change_Text_Value(this, EventArgs.Empty);
				}
			}
		}

		private bool Check_Data_Type(string _Str)
		{
			bool bRtn = false;
			switch (DataType)
			{
				case Return_Data_Type.Integer:
					{
						try
						{
							int nInt = int.Parse(_Str);
							bRtn = true;
						}
						catch
						{
							bRtn = false;
						}
					} break;
				case Return_Data_Type.Float:
					{
						try
						{
							float fFloat = float.Parse(_Str);
							bRtn = true;
						}
						catch
						{
							bRtn = false;
						}
					} break;
				default:
					{
						bRtn = true;
					} break;
			}
			return bRtn;
		}
	}
}
