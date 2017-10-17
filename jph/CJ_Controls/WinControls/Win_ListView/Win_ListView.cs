using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace CJ_Controls.Windows
{
	public class Win_ListView : ListView
	{
		public Win_ListView()
		{
			// Enable internal ListView double-buffering
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

			// Disable default CommCtrl painting on non-XP systems
			if (!IsWinXP)
				SetStyle(ControlStyles.UserPaint, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (GetStyle(ControlStyles.UserPaint))
			{
				Message m = new Message();
				m.HWnd = Handle;
				m.Msg = WM_PRINTCLIENT;
				m.WParam = e.Graphics.GetHdc();
				m.LParam = (IntPtr)PRF_CLIENT;
				DefWndProc(ref m);
				e.Graphics.ReleaseHdc(m.WParam);
			}
			base.OnPaint(e);
		}

		private const int WM_PRINTCLIENT = 0x0318;
		private const int PRF_CLIENT = 0x00000004;
		private bool IsWinXP
		{
			get
			{
				OperatingSystem OS = Environment.OSVersion;
				return (OS.Platform == PlatformID.Win32NT) &&
					((OS.Version.Major > 5) || ((OS.Version.Major == 5) && (OS.Version.Minor == 1)));
			}
		}

		private bool IsWinVista
		{
			get
			{
				OperatingSystem OS = Environment.OSVersion;
				return (OS.Platform == PlatformID.Win32NT) && (OS.Version.Major >= 6);
			}
		}
	}
}
