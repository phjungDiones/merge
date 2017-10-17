using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CJ_Controls.Windows;
using System.Collections;

namespace CJ_Controls.Communication.SRZ
{
	public partial class Ctrl_SRZ_IO_View : UserControl
	{
		public Ctrl_SRZ_IO_View()
		{
			InitializeComponent();
		}

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

		COM_SRZ_Communication m_SRZ_Comm = null;
		COM_SRZ_IO_List m_SRZ_IoList = null;
		Thread _ThreadRefreshIO = null;

		private void Ctrl_SRZ_IO_View_Resize(object sender, EventArgs e)
		{
			if (this.Width >= 1000)
			{
				Panel_Input.Dock = DockStyle.Left;
				int nHarf = this.Width / 2;
				Panel_Input.Width = nHarf;
			}
			else
			{
				if (this.Width <= 10)
					return;

				Panel_Input.Dock = DockStyle.Top;
				int nHarf = this.Height / 2;
				Panel_Input.Height = nHarf;
			}
		}

		public void SetSRZ(COM_SRZ_Communication _SrzCom)
		{
			m_SRZ_Comm = _SrzCom;
			if (m_SRZ_Comm == null)
				return;

			m_SRZ_IoList = m_SRZ_Comm.SRZ_IO_List;
			if (m_SRZ_IoList == null)
				return;

			Init_List();
			_ThreadRefreshIO = new Thread(new ThreadStart(RefreshIO));
			_ThreadRefreshIO.IsBackground = true;
			_ThreadRefreshIO.Start();
		}

		private void Init_List()
		{
			for (int nCpu = 0; nCpu < m_SRZ_IoList.SRZ_Cpu.Count; nCpu++)
			{
				Win_ListView _Input_ListView = MakeListView(nCpu, false);
				Win_ListView _Output_ListView = MakeListView(nCpu, true);
				// INPUT
				for (int nModule = 0; nModule < m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List.Count; nModule++)
				{
					if (m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List[nModule].IO_Type == SRZ_IO_TYPE.TIO_8888
						|| m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List[nModule].IO_Type == SRZ_IO_TYPE.TIO_VVVV)
					{
						AddItems(_Input_ListView, m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List[nModule].Read_Value);
					}
				}
				for (int nModule = 0; nModule < m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List.Count; nModule++)
				{
					if (m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List[nModule].IO_Type == SRZ_IO_TYPE.TIO_8888)
					{
						AddItems(_Input_ListView, m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List[nModule].Read_Out_Value);
					}
				}
				for (int nModule = 0; nModule < m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List.Count; nModule++)
				{
					if (m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List[nModule].IO_Type == SRZ_IO_TYPE.DIO)
					{
						AddItems(_Input_ListView, m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List[nModule].Read_Value);
					}
				}
				// OUTPUT
				for (int nModule = 0; nModule < m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List.Count; nModule++)
				{
					AddItems(_Output_ListView, m_SRZ_IoList.SRZ_Cpu[nCpu].Module_List[nModule].Set_Value);
				}
				TabAdd_Input(nCpu, _Input_ListView);
				TabAdd_Output(nCpu, _Output_ListView);
			}
		}
		private void AddItems(Win_ListView _ListView, List<SRZ_IO> _IO_List)
		{
			for (int nIndex = 0; nIndex < _IO_List.Count; nIndex++)
			{
				if (SpareIO_Visable == false)
				{
					if (_IO_List[nIndex].IO_Name.IndexOf("SPARE") >= 0)
						continue;
				}

				ListViewItem list = null;
				list = _ListView.Items.Add(_IO_List[nIndex].IO_Name);
				list.SubItems.Add(_IO_List[nIndex].IO_Type.ToString());

				if (_IO_List[nIndex].IO_Type == SRZ_IO_TYPE.DIO)
				{
					string _Val = _IO_List[nIndex].Value == 0 ? "OFF" : "ON";
					list.SubItems.Add(_Val);
				}
				else
				{
					string _Val = _IO_List[nIndex].Value.ToString();
					list.SubItems.Add(_Val);
				}
			}
		}
		private Win_ListView MakeListView(int nAdapter, bool bDobuleClick)
		{
			Win_ListView _ListView = new Win_ListView();
			_ListView.SuspendLayout();
			ImageList dumImgList = new ImageList();
			dumImgList.ImageSize = new System.Drawing.Size(1, 20); //리스트 뷰 라인 간격 조절용
			_ListView.SmallImageList = dumImgList;

			//컬럼 만들기.
			System.Windows.Forms.ColumnHeader _Column1 = new System.Windows.Forms.ColumnHeader();
			_Column1.Text = "Name"; _Column1.Width = 300;
			System.Windows.Forms.ColumnHeader _Column2 = new System.Windows.Forms.ColumnHeader();
			_Column2.Text = "Type"; _Column2.Width = 100;
			System.Windows.Forms.ColumnHeader _Column3 = new System.Windows.Forms.ColumnHeader();
			_Column3.Text = "Value"; _Column3.Width = 80;
			_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { _Column1, _Column2, _Column3});

			_ListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			_ListView.Dock = System.Windows.Forms.DockStyle.Fill;
			_ListView.Font = new System.Drawing.Font("굴림체", 9F);
			_ListView.FullRowSelect = true;
			_ListView.GridLines = true;
			_ListView.OwnerDraw = true;
			_ListView.Location = new System.Drawing.Point(3, 3);
			_ListView.UseCompatibleStateImageBehavior = false;
			_ListView.View = System.Windows.Forms.View.Details;
			_ListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView_DrawColumnHeader);
			_ListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_DrawSubItem);
			if (bDobuleClick == true)
			{
				_ListView.DoubleClick += new System.EventHandler(this.ListView_Output_DoubleClick);
			}
			_ListView.ResumeLayout(false);
			return _ListView;
		}

		private void TabAdd_Input(int nCpu, Win_ListView _ListView)
		{
			if (_ListView.Items.Count <= 0)
				return;

			TabPage _Page = new TabPage();
			_Page.SuspendLayout();
			_Page.Location = new System.Drawing.Point(4, 29);
			_Page.Padding = new System.Windows.Forms.Padding(3);
			_Page.TabIndex = nCpu;

			_Page.Text = string.Format("CPU#{0}", nCpu + 1);

			_Page.UseVisualStyleBackColor = false;
            
			_Page.Controls.Add(_ListView);
			_Page.ResumeLayout(false);

            Tab_Input.Controls.Owner.BackColor = Color.DimGray;
			Tab_Input.SuspendLayout();
			Tab_Input.Controls.Add(_Page);
			Tab_Input.ResumeLayout(false);
		}
		private void TabAdd_Output(int nCpu, Win_ListView _ListView)
		{
			if (_ListView.Items.Count <= 0)
				return;

			TabPage _Page = new TabPage();
			_Page.SuspendLayout();
			_Page.Location = new System.Drawing.Point(4, 29);
			_Page.Padding = new System.Windows.Forms.Padding(3);
			_Page.TabIndex = nCpu;

			_Page.Text = string.Format("CPU#{0}", nCpu + 1);

			_Page.UseVisualStyleBackColor = true;

			_Page.Controls.Add(_ListView);
			_Page.ResumeLayout(false);

            Tab_Output.Controls.Owner.BackColor = Color.DimGray;
			Tab_Output.SuspendLayout();
			Tab_Output.Controls.Add(_Page);
			Tab_Output.ResumeLayout(false);
		}
		private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}
		private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			int nIndex = e.ItemIndex;
			int nColumn = e.ColumnIndex;
			if (nIndex > -1 && nColumn == 2)
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

                    //backBrush = new SolidBrush(Color.DimGray);
                    //foreBrush = new SolidBrush(Color.White);
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
		private void ListView_Output_DoubleClick(object sender, EventArgs e)
		{
			if (UseOutputControl == false)
				return;

			ListView listView = ((ListView)sender);
			int nIndex = listView.SelectedItems[0].Index;
			if (nIndex < 0)
				return;

			string strIOName = listView.Items[nIndex].SubItems[0].Text;
			string strIoType = listView.Items[nIndex].SubItems[1].Text;
			if (strIOName.StartsWith("SPARE_") == true)
				return; //스페어다.

			if (strIoType == SRZ_IO_TYPE.TIO_8888.ToString())
			{
				SRZ_IO _IO = m_SRZ_IoList.GetLinkData(strIOName, SRZ_IO_TYPE.TIO_8888);
				if (_IO != null)
				{
					Form_SRZ_Output_Control dlg = new Form_SRZ_Output_Control();
					dlg.SetIO(_IO);
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						
					}
				}
			}
			else if (strIoType == SRZ_IO_TYPE.DIO.ToString())
			{
				SRZ_IO _IO = m_SRZ_IoList.GetLinkData(strIOName, SRZ_IO_TYPE.DIO);
				if (_IO != null)
				{
					float fVal = (_IO.Value == 0) ? 1 : 0; // 현재값과 반대로 보낸다.
					string strMsg = string.Format("Do you Wan't Send Select IO?\r\n\r\n - IO Name : {0}\r\n - Value : {1}", _IO.IO_Name, (fVal == 1) ? "ON" : "OFF");
					if (!MessageBox.Show(strMsg, "Send IO", MessageBoxButtons.YesNo,
						MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
						return;
					_IO.Value = fVal;
				}
			}
		}
		private void RefreshIO()
		{
			while (true)
			{
				try
				{
					if (m_SRZ_Comm == null)
						return;
					if (m_SRZ_IoList == null)
						return;

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
			//Input 갱신
			Refresh_IO_List(Tab_Input.SelectedIndex, Tab_Input.SelectedTab);
			//Output 갱신
			Refresh_IO_List(Tab_Output.SelectedIndex, Tab_Output.SelectedTab);
		}
		private void Refresh_IO_List(int nTabIndex, TabPage _Page)
		{
			if (nTabIndex < 0 || _Page == null)
				return;

			string strIO_Name = "";
			string strIO_Type = "";
			foreach (Control ctrl in _Page.Controls)
			{
				if (ctrl.GetType().Name == "Win_ListView")
				{
					Win_ListView listView = ctrl as Win_ListView;

					if (listView != null)
					{
						foreach (ListViewItem list in listView.Items)
						{
							strIO_Name = list.SubItems[0].Text;
							strIO_Type = list.SubItems[1].Text;

							SRZ_IO _IO = null;
							
							if (strIO_Type == SRZ_IO_TYPE.TIO_8888.ToString())
							{
								_IO = m_SRZ_IoList.GetLinkData(strIO_Name, nTabIndex, SRZ_IO_TYPE.TIO_8888);
							}
							else if (strIO_Type == SRZ_IO_TYPE.TIO_VVVV.ToString())
							{
								_IO = m_SRZ_IoList.GetLinkData(strIO_Name, nTabIndex, SRZ_IO_TYPE.TIO_VVVV);
							}
							else if (strIO_Type == SRZ_IO_TYPE.DIO.ToString())
							{
								_IO = m_SRZ_IoList.GetLinkData(strIO_Name, nTabIndex, SRZ_IO_TYPE.DIO);
							}

							if (_IO != null)
							{
								if (_IO.IO_Type == SRZ_IO_TYPE.DIO)
								{
									string strVal = _IO.Value == 0 ? "OFF" : "ON";
									if (strVal != list.SubItems[2].Text)
									{
										list.SubItems[2].Text = strVal;
									}
								}
								else
								{
									string strVal = _IO.Value.ToString();
									if (strVal != list.SubItems[2].Text)
									{
										list.SubItems[2].Text = strVal;
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
