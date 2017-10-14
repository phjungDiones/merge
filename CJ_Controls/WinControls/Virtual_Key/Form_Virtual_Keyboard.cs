using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.WinControls.Virtual_Key
{
	public partial class Form_Virtual_Keyboard : Form
	{
		public Form_Virtual_Keyboard()
		{
			InitializeComponent();
		}
		string m_FirstText = "";
		string m_Input_Text = "";
		bool m_bPwdType = false;
		public void SetFirstText(string strText)
		{
			m_FirstText = strText;
		}
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
		private void Form_Virtual_Keyboard_Load(object sender, EventArgs e)
		{
			m_Input_Text = m_FirstText;

			Print_Label_Text();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			m_Input_Text = "";
			Print_Label_Text();
		}

		private void btnKey_Click(object sender, EventArgs e)
		{
			Button _Btn = sender as Button;
			InputKey(_Btn.Text);
		}
		private void InputKey(string str)
		{
			string strText = m_Input_Text;
			m_Input_Text = strText + str;

			Print_Label_Text();
		}
		private void Check_Shift_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox _Chk = sender as CheckBox;
			if (_Chk.Checked == true)
			{
				_Chk.BackColor = Color.Lime;
				SetLargeText();
			}
			else
			{
				_Chk.BackColor = SystemColors.Control;
				SetSmalText();
			}
		}
		private void SetSmalText()
		{
			btnNum01.Text = "1";
			btnNum02.Text = "2";
			btnNum03.Text = "3";
			btnNum04.Text = "4";
			btnNum05.Text = "5";
			btnNum06.Text = "6";
			btnNum07.Text = "7";
			btnNum08.Text = "8";
			btnNum09.Text = "9";
			btnNum00.Text = "0";
			btnMinus.Text = "-";
			btnEqual.Text = "=";
			btn_Q.Text = "q";
			btn_W.Text = "w";
			btn_E.Text = "e";
			btn_R.Text = "r";
			btn_T.Text = "t";
			btn_Y.Text = "y";
			btn_U.Text = "u";
			btn_I.Text = "i";
			btn_O.Text = "o";
			btn_P.Text = "p";
			btn_Left_GH.Text = "[";
			btn_Right_GH.Text = "]";
			btn_Won.Text = "\\";
			btn_A.Text = "a";
			btn_S.Text = "s";
			btn_D.Text = "d";
			btn_F.Text = "f";
			btn_G.Text = "g";
			btn_H.Text = "h";
			btn_J.Text = "j";
			btn_K.Text = "k";
			btn_L.Text = "l";
			btn_SemiColon.Text = ";";
			btn_Quotes.Text = "'";
			btn_Z.Text = "z";
			btn_X.Text = "x";
			btn_C.Text = "c";
			btn_V.Text = "v";
			btn_B.Text = "b";
			btn_N.Text = "n";
			btn_M.Text = "m";
			btn_Comma.Text = ",";
			btn_Period.Text = ".";
			btn_Slash.Text = "/";
		}
		private void SetLargeText()
		{
			btnNum01.Text = "!";
			btnNum02.Text = "@";
			btnNum03.Text = "#";
			btnNum04.Text = "$";
			btnNum05.Text = "%";
			btnNum06.Text = "^";
			btnNum07.Text = "&&";
			btnNum08.Text = "*";
			btnNum09.Text = "(";
			btnNum00.Text = ")";
			btnMinus.Text = "_";
			btnEqual.Text = "+";
			btn_Q.Text = "Q";
			btn_W.Text = "W";
			btn_E.Text = "E";
			btn_R.Text = "R";
			btn_T.Text = "T";
			btn_Y.Text = "Y";
			btn_U.Text = "U";
			btn_I.Text = "I";
			btn_O.Text = "O";
			btn_P.Text = "P";
			btn_Left_GH.Text = "{";
			btn_Right_GH.Text = "}";
			btn_Won.Text = "|";
			btn_A.Text = "A";
			btn_S.Text = "S";
			btn_D.Text = "D";
			btn_F.Text = "F";
			btn_G.Text = "G";
			btn_H.Text = "H";
			btn_J.Text = "J";
			btn_K.Text = "K";
			btn_L.Text = "L";
			btn_SemiColon.Text = ":";
			btn_Quotes.Text = "\"";
			btn_Z.Text = "Z";
			btn_X.Text = "X";
			btn_C.Text = "C";
			btn_V.Text = "V";
			btn_B.Text = "B";
			btn_N.Text = "N";
			btn_M.Text = "M";
			btn_Comma.Text = "<";
			btn_Period.Text = ">";
			btn_Slash.Text = "?";
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

		private void btnEnter_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void Form_Virtual_Keyboard_KeyDown(object sender, KeyEventArgs e)
		{
			Button _btn = sender as Button;
			if (_btn != null)
				return;

			if (e.KeyCode == Keys.Delete)
			{
				btnClear.PerformClick();
			}
			else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
			{
				btnNum01.PerformClick();
			}
			else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
			{
				btnNum02.PerformClick();
			}
			else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
			{
				btnNum03.PerformClick();
			}
			else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
			{
				btnNum04.PerformClick();
			}
			else if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
			{
				btnNum05.PerformClick();
			}
			else if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
			{
				btnNum06.PerformClick();
			}
			else if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
			{
				btnNum07.PerformClick();
			}
			else if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
			{
				btnNum08.PerformClick();
			}
			else if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
			{
				btnNum09.PerformClick();
			}
			else if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
			{
				btnNum00.PerformClick();
			}
			else if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
			{
				btnMinus.PerformClick();
			}
			else if (e.KeyCode == Keys.Oemplus)
			{
				btnEqual.PerformClick();
			}
			else if (e.KeyCode == Keys.Back)
			{
				btnBackSpace.PerformClick();
			}
			else if (e.KeyValue >= (int)Keys.A && e.KeyValue <= (int)Keys.Z)
			{
				string strKeyChar = e.KeyCode.ToString();
				if (Check_Shift.Checked)
					InputKey(strKeyChar.ToUpper());
				else
					InputKey(strKeyChar.ToLower());
			}
			else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
			{
				btnEnter.PerformClick();
			}
			else if (e.KeyCode == Keys.OemOpenBrackets)
			{
				btn_Left_GH.PerformClick();
			}
			else if (e.KeyCode == Keys.OemCloseBrackets)
			{
				btn_Right_GH.PerformClick();
			}
			else if (e.KeyCode == Keys.OemPipe)
			{
				btn_Won.PerformClick();
			}
			else if (e.KeyCode == Keys.OemSemicolon)
			{
				btn_SemiColon.PerformClick();
			}
			else if (e.KeyCode == Keys.OemQuotes)
			{
				btn_Quotes.PerformClick();
			}
			else if (e.KeyCode == Keys.Oemcomma)
			{
				btn_Comma.PerformClick();
			}
			else if (e.KeyCode == Keys.OemPeriod)
			{
				btn_Period.PerformClick();
			}
			else if (e.KeyCode == Keys.OemQuestion)
			{
				btn_Slash.PerformClick();
			}
			else if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Shift
				|| e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
			{
				Check_Shift.Checked = true;
			}
		}

		private void Form_Virtual_Keyboard_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Shift
				|| e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
			{
				Check_Shift.Checked = false;
			}
		}
	}
}
