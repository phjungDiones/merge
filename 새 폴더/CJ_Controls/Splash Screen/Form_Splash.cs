using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.Splash_Screen
{
	public partial class Form_Splash : Form
	{
		public Form_Splash()
		{
			InitializeComponent();
		}
		delegate void StringParameterDelegate(string Text);
		delegate void SplashShowCloseDelegate();
		private bool CloseSplashScreenFlag = false;
		public void ShowSplashScreen()
		{
			if (InvokeRequired)
			{
				// We're not in the UI thread, so we need to call BeginInvoke
				BeginInvoke(new SplashShowCloseDelegate(ShowSplashScreen));
				return;
			}
			this.Show();
			this.CenterToScreen();
			this.TopMost = true;
			Application.Run(this);
		}
		public void CloseSplashScreen()
		{
			if (InvokeRequired)
			{
				// We're not in the UI thread, so we need to call BeginInvoke
				BeginInvoke(new SplashShowCloseDelegate(CloseSplashScreen));
				return;
			}
			CloseSplashScreenFlag = true;
			this.Close();
		}
		public void UdpateStatusText(string Text)
		{
			if (InvokeRequired)
			{
				// We're not in the UI thread, so we need to call BeginInvoke
				BeginInvoke(new StringParameterDelegate(UdpateStatusText), new object[] { Text });
				return;
			}
			// Must be on the UI thread if we've got this far
			lblStatus.ForeColor = Color.Black;
			lblStatus.Text = Text;
		}

		private void CJ_SplashForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (CloseSplashScreenFlag == false)
				e.Cancel = true;
		}
	}
}
