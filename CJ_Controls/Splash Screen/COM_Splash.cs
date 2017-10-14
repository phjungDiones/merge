using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading;
using CJ_Controls;

namespace CJ_Controls.Splash_Screen
{
	public class COM_Splash : Component
	{
		#region Log 보내기
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public event MessageEventHandler MessageEvent;

		private void LogTextOut(string message)
		{
			if (MessageEvent != null)
				MessageEvent(this, new MessageEventArgs(message));
		}
		#endregion

		public COM_Splash()
		{
		}
		private Thread splashThread = null;
		public Form_Splash splashForm = null;
		public void StartSplash()
		{
			splashThread = new Thread(new ThreadStart(ShowSplashScreen));
			splashThread.IsBackground = true;
			splashThread.Start();
		}
		public void ShowSplashScreen()
		{
			if (splashForm == null)
			{
				splashForm = new Form_Splash();
				splashForm.ShowSplashScreen();
			}
		}
		public void EndSplash()
		{
			if (splashForm != null)
			{
				splashForm.CloseSplashScreen();
				splashForm = null;
			}
		}
		public void UdpateStatusText(string Text)
		{
			if (splashForm != null)
			{
				splashForm.UdpateStatusText(Text);
				LogTextOut(Text);
			}
		}
	}
}
