using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.Communication.SRZ
{
	public partial class Form_SRZ_Output_Control : Form
	{
		public Form_SRZ_Output_Control()
		{
			InitializeComponent();
		}

		private SRZ_IO m_IO = null;
		public void SetIO(SRZ_IO _IO)
		{
			m_IO = _IO;
		}

		private void Form_SRZ_Output_Control_Load(object sender, EventArgs e)
		{
			Label_IO_Name.Text = m_IO.IO_Name;
			Label_IO_Type.Text = m_IO.IO_Type.ToString();
			Num_IoValue.Value = Convert.ToDecimal(m_IO.Value);
		}

		private void Btn_Set_Click(object sender, EventArgs e)
		{
			if (Num_IoValue.Value != Convert.ToDecimal(m_IO.Value))
			{
				m_IO.Value = float.Parse(Num_IoValue.Value.ToString());
				Close();
			}
			else
			{
				string strMsg = string.Format("Before:{0}, Current:{1}\r\n값이 같습니다.", m_IO.Value,Num_IoValue.Value);
				MessageBox.Show(strMsg);
			}
		}

		private void Btn_Cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void Num_IoValue_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData.Equals(Keys.Return))
			{
				Btn_Set.PerformClick();
			}
		}
	}
}
