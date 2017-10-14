using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CJ_Controls.Etc
{
	public partial class LogoControl : UserControl
	{
		public LogoControl()
		{
			InitializeComponent();
		}

		private string m_Date = "2016.08.19";
		public string BuildDate
		{
			get { return m_Date; }
			set { m_Date = value; }
		}

		private void LogoControl_Load(object sender, EventArgs e)
		{
			lbVersion.Text = "Ver:" + GetVersion();
			lbBuildDate.Text = "";
			//lbBuildDate.Text = m_Date;
		}

		private string GetVersion()
		{
			string version = string.Format("{0}", Application.ProductVersion);
			Match match = Regex.Match(version, @"\d*\.\d*\.\d*");
			if (match.Success)
			{
				version = match.Value;
			}
			else
			{
				version = "0.0.0";
			}
			return version;
		}
	}
}
