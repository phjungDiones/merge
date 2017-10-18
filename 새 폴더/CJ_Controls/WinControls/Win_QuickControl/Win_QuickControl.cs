using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace CJ_Controls.Windows
{
	//Cjinnnn: 속도개선 더블버퍼링
	public class Win_QuickDataGridView : DataGridView
	{
		public Win_QuickDataGridView()
		{
			DoubleBuffered = true;
		}
	}

	//Cjinnnn: 속도개선 더블버퍼링
	public class Win_QuickForm : Form
	{
		public Win_QuickForm()
		{
			this.SetStyle(ControlStyles.DoubleBuffer, true);			// 1
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);	// 2
			this.SetStyle(ControlStyles.UserPaint, true);				// 3
		}
	}

	//Cjinnnn: 속도개선 더블버퍼링
	public class Win_QuickTableLayoutPanel : TableLayoutPanel
	{
		public Win_QuickTableLayoutPanel()
		{
			this.SetStyle(ControlStyles.DoubleBuffer, true);			// 1
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);	// 2
			this.SetStyle(ControlStyles.UserPaint, true);				// 3
		}
	}

	//Cjinnnn: 속도개선 더블버퍼링
	public class Win_QuickPanel : Panel
	{
		public Win_QuickPanel()
		{
			this.DoubleBuffered = true;
		}
	}

	//Cjinnnn: 속도개선 더블버퍼링
	public class DF_UserControl : UserControl
	{
		public DF_UserControl()
		{
			this.DoubleBuffered = true;
		}
	}
}
