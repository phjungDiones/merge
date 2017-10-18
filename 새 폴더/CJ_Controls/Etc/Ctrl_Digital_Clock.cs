using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.Etc
{
	public partial class Ctrl_Digital_Clock : UserControl
	{
		public Ctrl_Digital_Clock()
		{
			InitializeComponent();
		}

		private void Timer_EveryTime_Tick(object sender, EventArgs e)
		{
			try
			{
				DateTime curTime = DateTime.Now;
				Label_YMD.Text = curTime.ToString("yyyy-MM-dd");
				Label_Time.Text = string.Format("{0:00}:{1:00}:{2:00}.{3:0}"
												,curTime.Hour
												,curTime.Minute
												,curTime.Second
												,curTime.Millisecond);
			}
			catch
			{

			}
		}
	}
}
