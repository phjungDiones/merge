using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.WinControls.Virtual_Key
{
	/// <summary>
	/// Changjin.Jeong : Keypad가 필요해 만들었다.
	/// </summary>
	public partial class Form_Virtual_KeyPad : Form
	{
		public Form_Virtual_KeyPad()
		{
			InitializeComponent();
		}

		string m_FirstText = "";
		bool m_bFirst = false;
		public void SetFirstText(string strText)
		{
			m_FirstText = strText;
			m_bFirst = true;
		}

		string m_Input_Text = "";
		bool m_bPwdType = false;
		public void SetPwdType(bool bPwdType)
		{
			m_bPwdType = bPwdType;
		}

		public string GetRtnValue()
		{
			return m_Input_Text;
		}

		private void Print_Label_Text()
		{
			if (m_bPwdType == false)
				Label_Text.Text = m_Input_Text;
			else
			{
				Label_Text.Text = "";
				for (int i = 0; i < m_Input_Text.Length; i++)
				{
					Label_Text.Text += "*";
				}
			}
		}

		private void Form_Virtual_KeyPad_Load(object sender, EventArgs e)
		{
			m_Input_Text = m_FirstText;
			Print_Label_Text();
		}

		private void btnNumber_Click(object sender, EventArgs e)
		{
			Button _Btn = sender as Button;
			InputKey(_Btn.Text);
		}
		private void btnPlusMinus_Click(object sender, EventArgs e)
		{
			string str = m_Input_Text;
			if (str.IndexOf("-") >= 0)
			{
				m_Input_Text = str.Replace("-", "");
			}
			else
			{
				m_Input_Text = "-" + str;
			}
			Print_Label_Text();
		}
		private void btnDot_Click(object sender, EventArgs e)
		{
			Button _Btn = sender as Button;
			string strTag = _Btn.Text;
			string strText = m_Input_Text.Replace(".", "");
			m_Input_Text = strText + strTag; //앞에꺼 다 빼고,, 맨뒤에 붙여준다.
			Print_Label_Text();
		}
		private void btnClear_Click(object sender, EventArgs e)
		{
			m_Input_Text = "";
			Print_Label_Text();
		}

		private void btnBackSpace_Click(object sender, EventArgs e)
		{
			string str = m_Input_Text;
			if (str.Length > 0)
			{
				m_Input_Text = str.Substring(0, str.Length - 1);
				Print_Label_Text();
			}
		}
		private void InputKey(string str)
		{
			if (m_bFirst == true)
			{
				m_Input_Text = "";
				m_bFirst = false;
			}
			string strText = m_Input_Text;
			m_Input_Text = strText + str;
			Print_Label_Text();
		}

		private void btnEnter_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void Form_Virtual_KeyPad_KeyDown(object sender, KeyEventArgs e)
		{
			Button _btn = sender as Button;
			if (_btn != null)
				return;

			if (e.KeyValue == (int)Keys.D1 || e.KeyValue == (int)Keys.NumPad1)
			{
				InputKey("1");
			}
			else if (e.KeyValue == (int)Keys.D2 || e.KeyValue == (int)Keys.NumPad2)
			{
				InputKey("2");
			}
			else if (e.KeyValue == (int)Keys.D3 || e.KeyValue == (int)Keys.NumPad3)
			{
				InputKey("3");
			}
			else if (e.KeyValue == (int)Keys.D4 || e.KeyValue == (int)Keys.NumPad4)
			{
				InputKey("4");
			}
			else if (e.KeyValue == (int)Keys.D5 || e.KeyValue == (int)Keys.NumPad5)
			{
				InputKey("5");
			}
			else if (e.KeyValue == (int)Keys.D6 || e.KeyValue == (int)Keys.NumPad6)
			{
				InputKey("6");
			}
			else if (e.KeyValue == (int)Keys.D7 || e.KeyValue == (int)Keys.NumPad7)
			{
				InputKey("7");
			}
			else if (e.KeyValue == (int)Keys.D8 || e.KeyValue == (int)Keys.NumPad8)
			{
				InputKey("8");
			}
			else if (e.KeyValue == (int)Keys.D9 || e.KeyValue == (int)Keys.NumPad9)
			{
				InputKey("9");
			}
			else if (e.KeyValue == (int)Keys.D0 || e.KeyValue == (int)Keys.NumPad0)
			{
				InputKey("0");
			}
			else if (e.KeyValue == (int)Keys.Back)
			{
				btnBackSpace.PerformClick();
			}
			else if (e.KeyValue == (int)Keys.Delete)
			{
				btnClear.PerformClick();
			}
			else if (e.KeyValue == (int)Keys.Enter || e.KeyValue == (int)Keys.Return)
			{
				btnEnter.PerformClick();
			}
			else if (e.KeyValue == (int)Keys.OemPeriod || e.KeyValue == (int)Keys.Decimal)
			{
				btnDot.PerformClick();
			}
			else if (e.KeyValue == (int)Keys.OemMinus || e.KeyValue == (int)Keys.Subtract)
			{
				string str = m_Input_Text;
				if (str.IndexOf("-") >= 0)
				{
					str = str.Replace("-", "");
					m_Input_Text = "-" + str;
				}
				else
				{
					m_Input_Text = "-" + str;
				}
				Print_Label_Text();
			}
			else if (e.KeyValue == (int)Keys.Oemplus || e.KeyValue == (int)Keys.Add)
			{
				string str = m_Input_Text;
				if (str.IndexOf("-") >= 0)
				{
					str = str.Replace("-", "");
					m_Input_Text = str;
				}
				Print_Label_Text();
			}
		}
	}
}
