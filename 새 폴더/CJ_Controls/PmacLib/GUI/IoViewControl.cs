using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CJ_Controls.Windows;
using CJ_Controls;
using System.Threading;

namespace CJ_Controls.PmacLib
{
	public partial class IoViewControl : UserControl
	{
		public IoViewControl()
		{
			InitializeComponent();
		}

		private void IoViewControl_Load(object sender, EventArgs e)
		{

		}

		public event EventHandler SendIO_Log_Event;

		private bool m_bUseOutputControl = false;
		[Category("Setting"), Description("Output Control Enable/Disable"), DefaultValue(false)]
		public bool UseOutputControl
		{
			get { return m_bUseOutputControl; }
			set { m_bUseOutputControl = value; }
		}

		private bool m_bSpareIO_Visable = false;
		[Category("Setting"), Description("Spare IO Visable"), DefaultValue(false)]
		public bool SpareIO_Visable
		{
			get { return m_bSpareIO_Visable; }
			set { m_bSpareIO_Visable = value; }
		}

		private PMacOneBoardCtrl m_PmacModule = null;
		private IO_List m_IoList_All = null;
		private Thread _ThreadRefreshIO = null;
		public void Init(PMacOneBoardCtrl _PmacModule)
		{
			m_PmacModule = _PmacModule;
			m_IoList_All = m_PmacModule.IoList_All;
			Io_Display_Input(m_IoList_All);
			Io_Display_Output(m_IoList_All);

			_ThreadRefreshIO = new Thread(new ThreadStart(RefreshIO));
			_ThreadRefreshIO.IsBackground = true;
			_ThreadRefreshIO.Start();
		}
		private void Io_Display_Input(IO_List _IoList)
		{
			ListView _ListView = ListView_InputList;

			ImageList dumImgList = new ImageList();
			dumImgList.ImageSize = new System.Drawing.Size(1, 26); //리스트 뷰 라인 간격 조절용
			_ListView.SmallImageList = dumImgList;

			_ListView.Items.Clear();

			int nCnt = 0;
			foreach (DeviceNetIO _Io in _IoList.Input)
			{
				nCnt++;
				//no,add,subaddr,type,cable,ioname
				ListViewItem Item = _ListView.Items.Add(nCnt.ToString("000")); //No
				Item.SubItems.Add("M" + _Io.Address.ToString()); //Addr
				Item.SubItems.Add(_Io.Description.ToString()); //SubAddr
				Item.SubItems.Add(_Io.Cable.ToString()); //cable
				Item.SubItems.Add(_Io.IO_Name.ToString()); //IoName
				Item.SubItems.Add(_Io.IsOn.ToString()); //IoVal
			}
		}
		private void Io_Display_Output(IO_List _IoList)
		{
			ListView _ListView = ListView_OutputList;

			ImageList dumImgList = new ImageList();
			dumImgList.ImageSize = new System.Drawing.Size(1, 26); //리스트 뷰 라인 간격 조절용
			_ListView.SmallImageList = dumImgList;

			_ListView.Items.Clear();

			int nCnt = 0;
			foreach (DeviceNetIO _Io in _IoList.Output)
			{
				nCnt++;
				//no,add,subaddr,type,cable,ioname
				ListViewItem Item = _ListView.Items.Add(nCnt.ToString("000")); //No
				Item.SubItems.Add("M" + _Io.Address.ToString()); //Addr
				Item.SubItems.Add(_Io.Description.ToString()); //SubAddr
				Item.SubItems.Add(_Io.Cable.ToString()); //cable
				Item.SubItems.Add(_Io.IO_Name.ToString()); //IoName
				Item.SubItems.Add(_Io.IsOn.ToString()); //IoVal
			}
		}
		private void RefreshIO()
		{
			while (true)
			{
				try
				{
					this.BeginInvoke(new MethodInvoker(delegate
					{
						RefreshView();
					}));
				}
				catch// (Exception ex)
				{
					//Console.WriteLine(string.Format("IO View Thread Exception..{0}", ex.Message));
				}

				Thread.Sleep(500);
			}
		}
		private void RefreshView()
		{
			foreach (ListViewItem list in ListView_InputList.Items)
			{
				int nIO_Index = int.Parse(list.SubItems[0].Text) - 1;

				DeviceNetIO _IO = m_IoList_All.Input[nIO_Index];
				string strOnOff = _IO.IsOn == true ? "ON" : "OFF";
				if (strOnOff != list.SubItems[5].Text)
				{
					list.SubItems[5].Text = strOnOff;
				}
			}

			foreach (ListViewItem list in ListView_OutputList.Items)
			{
				int nIO_Index = int.Parse(list.SubItems[0].Text) - 1;

				DeviceNetIO _IO = m_IoList_All.Output[nIO_Index];
				string strOnOff = _IO.IsOn == true ? "ON" : "OFF";
				if (strOnOff != list.SubItems[5].Text)
				{
					list.SubItems[5].Text = strOnOff;
				}
			}
		}
		private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}
		private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			int nIndex = e.ItemIndex;
			int nColumn = e.ColumnIndex;
			if (nIndex > -1 && nColumn == 5)
			{
				Graphics g = e.Graphics;

				//사용할 브러쉬 선언
				SolidBrush backBrush = null;
				SolidBrush foreBrush = null;

				//선택된 항목이 아닌경우 일반적으로 브러쉬 생성
				if ((e.ItemState & ListViewItemStates.Focused) == 0)
				{
					backBrush = new SolidBrush(SystemColors.Window);
					foreBrush = new SolidBrush(SystemColors.WindowText);
				}
				else
				{//선택된 항목의 경우는 선택된 항목인 경우 브러쉬 생성
					backBrush = new SolidBrush(SystemColors.Highlight);
					foreBrush = new SolidBrush(SystemColors.HighlightText);
				}

				//강조 인덱스가 설정 된 경우
				if (e.SubItem.Text == "ON")
				{
					if (foreBrush != null)
						foreBrush.Dispose();

					if ((e.ItemState & ListViewItemStates.Focused) == 0)
					{//포커스가 아닌 놈은 선택 색으로 텍스트 브러쉬를 생성
						backBrush = new SolidBrush(Color.Lime);
						foreBrush = new SolidBrush(SystemColors.WindowText);
					}
					else
					{//선택된 항목의 경우 브러쉬 생성
						backBrush = new SolidBrush(Color.Silver);
						foreBrush = new SolidBrush(Color.Yellow);
					}
				}

				//배경색 채우기
				e.Graphics.FillRectangle(backBrush, e.SubItem.Bounds.X, e.SubItem.Bounds.Y, e.SubItem.Bounds.Width, e.SubItem.Bounds.Height);

				//아이템 텍스트 Draw
				e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, foreBrush, e.SubItem.Bounds.X + 5, e.SubItem.Bounds.Y + 5);

				//리소스 해제
				if (backBrush != null)
					backBrush.Dispose();

				if (foreBrush != null)
					foreBrush.Dispose();
			}
			else
			{
				e.DrawDefault = true;
			}
		}

		private void ListView_OutputList_DoubleClick(object sender, EventArgs e)
		{
			if (UseOutputControl == false)
				return;

			ListView listView = ((ListView)sender);
			int nIO_Index = listView.SelectedItems[0].Index;
			if (nIO_Index < 0)
				return;

			DeviceNetIO _IO = m_IoList_All.Output[nIO_Index];
			if (_IO != null)
			{
				bool bOn = !_IO.IsOn; // 현재값과 반대로 보낸다.
				string strMsg = string.Format("Do you Wan't Send Select IO?\r\n\r\n - IO Name : {0}\r\n - Send Value : {1}", _IO.IO_Name, (bOn) ? "ON" : "OFF");
				if (!MessageBox.Show(strMsg, "Send IO", MessageBoxButtons.YesNo,
					MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
					return;

				m_PmacModule.GetPmac().SetOutIOValUMAC(_IO, bOn);

				if (SendIO_Log_Event != null)
				{
					string strLog = string.Format("IO Name : {0}, SendVal : {1}", _IO.IO_Name, bOn);
					SendIO_Log_Event(strLog, EventArgs.Empty);
				}
			}
		}
	}
}
