using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.Log_Trace
{
	public partial class Form_TraceOneLog : Form
	{
		public Form_TraceOneLog()
		{
			InitializeComponent();
		}

		private void Form_TraceOneLog_Load(object sender, EventArgs e)
		{
			Label_DateTime.Text = m_strDateTime;
			this.Text = Label_Info.Text = m_strInfo;
			TextBox_Message.Text = m_strMsg;
		}

		string m_strDateTime = "";
		string m_strInfo = "";
		string m_strMsg = "";
		public void SetMessage(string strTime, string strInfo, string strMsg)
		{
			m_strDateTime = strTime;
			m_strInfo = strInfo;
			m_strMsg = strMsg;
		}
	}
}
