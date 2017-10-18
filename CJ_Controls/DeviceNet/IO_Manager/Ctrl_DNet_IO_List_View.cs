using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CJ_Controls.Windows;

namespace CJ_Controls.DeviceNet
{
	public partial class Ctrl_DNet_IO_List_View : UserControl
	{
		COM_DeviceNet m_DNet = null;
		COM_DeviceNet_IO_List m_DNet_IoList = null;
		Thread _ThreadRefreshIO = null;
		private string _TabName = "";
		[Category("Tab Setting"), Description("구분자는 \";\" 입니다.\r\nex) One;Two;Three;"), DefaultValue(false)]
		public string TabName
		{
			get { return _TabName; }
			set { _TabName = value; }
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

		public Ctrl_DNet_IO_List_View()
		{
			InitializeComponent();
		}

		public void SetDeviceNet(COM_DeviceNet _DNet)
		{
			m_DNet = _DNet;
			if (m_DNet == null)
				return;

			m_DNet_IoList = m_DNet.DNet_IO_List;
			if (m_DNet_IoList == null)
				return;

			Init_List();
			_ThreadRefreshIO = new Thread(new ThreadStart(RefreshIO));
			_ThreadRefreshIO.IsBackground = true;
			_ThreadRefreshIO.Start();
		}

		string [] strTabNames;
		private void Ctrl_IO_List_View_Load(object sender, EventArgs e)
		{
			if(TabName != "")
				strTabNames = TabName.Split(';');
		}
		private void RefreshIO()
		{
			while (true)
			{
				try
				{
					if (m_DNet == null)
						return;
					if (m_DNet_IoList == null)
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
            int nOutputTab = 0;
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
							strIO_Type = list.SubItems[5].Text;

							DeviceNetIO _IO = null;
							if (strIO_Type == DNET_IO_TYPE.D_INPUT.ToString())
							{
								_IO = m_DNet_IoList.GetLinkData(strIO_Name, nTabIndex, DNET_IO_TYPE.D_INPUT);
							}
							else if (strIO_Type == DNET_IO_TYPE.D_OUTPUT.ToString())
							{
                                //if (nTabIndex == 1) nTabIndex = 2;

                                //OUTPUT일 경우 TAB 2번이 없기 때문에 1번탭 이후1씩증가
                                if (nTabIndex >= 1) nOutputTab = nTabIndex + 1; 
                                _IO = m_DNet_IoList.GetLinkData(strIO_Name, nOutputTab, DNET_IO_TYPE.D_OUTPUT);
							}
							else if (strIO_Type == DNET_IO_TYPE.A_INPUT.ToString())
							{
								_IO = m_DNet_IoList.GetLinkData(strIO_Name, nTabIndex, DNET_IO_TYPE.A_INPUT);
							}

							if (_IO != null)
							{
								if (_IO.IO_Type == DNET_IO_TYPE.D_INPUT || _IO.IO_Type == DNET_IO_TYPE.D_OUTPUT)
								{
									string strOnOff = _IO.IsOn == true ? "ON" : "OFF";
									if (strOnOff != list.SubItems[3].Text)
									{
										list.SubItems[3].Text = strOnOff;
									}
								}
								else
								{
									list.SubItems[3].Text = _IO.AnalogValue.ToString();
								}
							}
						}
					}
				}
			}
		}
		private void Init_List()
		{
			for (int nAdapter = 0; nAdapter < m_DNet_IoList.DNet_Adapter.Count; nAdapter++)
			{
				Win_ListView _Input_ListView = MakeListView(nAdapter);
				Win_ListView _Output_ListView = MakeListView(nAdapter);
				for (int nModule = 0; nModule < m_DNet_IoList.DNet_Adapter[nAdapter].Module_List.Count; nModule++)
				{
					if (m_DNet_IoList.DNet_Adapter[nAdapter].Module_List[nModule].IO_Type == DNET_IO_TYPE.D_INPUT
						|| m_DNet_IoList.DNet_Adapter[nAdapter].Module_List[nModule].IO_Type == DNET_IO_TYPE.A_INPUT)
					{
						AddItems(_Input_ListView, m_DNet_IoList.DNet_Adapter[nAdapter].Module_List[nModule]);
					}
					else
					{
						AddItems(_Output_ListView, m_DNet_IoList.DNet_Adapter[nAdapter].Module_List[nModule]);
					}
				}
				TabAdd_Input(nAdapter, _Input_ListView);
				TabAdd_Output(nAdapter, _Output_ListView);
			}
		}
		private void AddItems(Win_ListView _ListView, DNet_Module_Struct _Module)
		{
			for (int nIndex = 0; nIndex < _Module.IO_List.Count; nIndex++)
			{
                //테스트
// 				if (SpareIO_Visable == false)
// 				{
// 					if (_Module.IO_List[nIndex].IO_Name.IndexOf("SPARE") >= 0)
// 						continue;
// 				}

				ListViewItem list = null;
				if (_Module.IO_List[nIndex].IO_Name != "")
				{
					list = _ListView.Items.Add(_Module.IO_List[nIndex].IO_Name);
				}
				else
				{
					list = _ListView.Items.Add(string.Format("SPARE_{0:000}_{1:x04}_{2}"
															, _Module.IO_List[nIndex].Address
															, _Module.IO_List[nIndex].SubAddr
															, _Module.IO_List[nIndex].IO_Type));
				}

				list.SubItems.Add(_Module.IO_List[nIndex].Address.ToString());
				if (_Module.IO_Type == DNET_IO_TYPE.D_INPUT
					|| _Module.IO_Type == DNET_IO_TYPE.D_OUTPUT)
				{
					list.SubItems.Add(string.Format("0x{0:x04}", _Module.IO_List[nIndex].SubAddr));
					if (_Module.IO_List[nIndex].IsOn.Equals(true))
						list.SubItems.Add("ON");
					else
						list.SubItems.Add("OFF");
				}
				else
				{
					list.SubItems.Add(_Module.IO_List[nIndex].SubAddr.ToString());
					list.SubItems.Add(_Module.IO_List[nIndex].AnalogValue.ToString());
				}
				list.SubItems.Add(_Module.IO_List[nIndex].Cable);
				list.SubItems.Add(_Module.IO_List[nIndex].IO_Type.ToString());
			}
		}
		private Win_ListView MakeListView(int nAdapter)
		{
			Win_ListView _ListView = new Win_ListView();
			_ListView.SuspendLayout();
			ImageList dumImgList = new ImageList();
			dumImgList.ImageSize = new System.Drawing.Size(1, 30); //리스트 뷰 라인 간격 조절용
			_ListView.SmallImageList = dumImgList;

			//컬럼 만들기.
// 			System.Windows.Forms.ColumnHeader _Column1 = new System.Windows.Forms.ColumnHeader();
// 			_Column1.Text = "Name"; _Column1.Width = 230;
// 			System.Windows.Forms.ColumnHeader _Column2 = new System.Windows.Forms.ColumnHeader();
// 			_Column2.Text = "Addr"; _Column2.Width = 50;
// 			System.Windows.Forms.ColumnHeader _Column3 = new System.Windows.Forms.ColumnHeader();
// 			_Column3.Text = "SubAddr"; _Column3.Width = 60;
// 			System.Windows.Forms.ColumnHeader _Column4 = new System.Windows.Forms.ColumnHeader();
// 			_Column4.Text = "Val"; _Column4.Width = 80;
// 			System.Windows.Forms.ColumnHeader _Column5 = new System.Windows.Forms.ColumnHeader();
// 			_Column5.Text = "Cable"; _Column5.Width = 0;// 70;
// 			System.Windows.Forms.ColumnHeader _Column6 = new System.Windows.Forms.ColumnHeader();
// 			_Column6.Text = "Type"; _Column6.Width = 0;

            System.Windows.Forms.ColumnHeader _Column1 = new System.Windows.Forms.ColumnHeader();
            _Column1.Text = "Name"; _Column1.Width = 290;
            System.Windows.Forms.ColumnHeader _Column2 = new System.Windows.Forms.ColumnHeader();
            _Column2.Text = "Addr"; _Column2.Width = 80;
            System.Windows.Forms.ColumnHeader _Column3 = new System.Windows.Forms.ColumnHeader();
            _Column3.Text = "SubAddr"; _Column3.Width = 80;
            System.Windows.Forms.ColumnHeader _Column4 = new System.Windows.Forms.ColumnHeader();
            _Column4.Text = "Val"; _Column4.Width = 80;
            System.Windows.Forms.ColumnHeader _Column5 = new System.Windows.Forms.ColumnHeader();
            _Column5.Text = "Cable"; _Column5.Width = 0;// 70;
            System.Windows.Forms.ColumnHeader _Column6 = new System.Windows.Forms.ColumnHeader();
            _Column6.Text = "Type"; _Column6.Width = 0;

            _Column1.TextAlign = HorizontalAlignment.Center;
            _Column2.TextAlign = HorizontalAlignment.Center;
            _Column3.TextAlign = HorizontalAlignment.Center;
            _Column4.TextAlign = HorizontalAlignment.Center;
            _Column5.TextAlign = HorizontalAlignment.Center;
            _Column6.TextAlign = HorizontalAlignment.Center;

			_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { _Column1, _Column2, _Column3, _Column4, _Column5, _Column6 });

			_ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			_ListView.Dock = System.Windows.Forms.DockStyle.Fill;
			//_ListView.Font = new System.Drawing.Font("굴림체", 9F);
            _ListView.Font = new System.Drawing.Font("고딕", 9F);
//             _ListView.BackColor = Color.DimGray;
//             _ListView.BackColor = Color.FromArgb(50,50,50);
            _ListView.ForeColor = Color.Black;
			_ListView.FullRowSelect = true;
			_ListView.GridLines = false;
			_ListView.OwnerDraw = true;
			_ListView.Location = new System.Drawing.Point(3, 3);
			_ListView.UseCompatibleStateImageBehavior = false;
			_ListView.View = System.Windows.Forms.View.Details;
			_ListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView_DrawColumnHeader);
			_ListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_DrawSubItem);
			_ListView.DoubleClick += new System.EventHandler(this.ListView_Output_DoubleClick);
			_ListView.ResumeLayout(false);
			return _ListView;
		}
		private void TabAdd_Input(int nAdapter, Win_ListView _ListView)
		{
			if (_ListView.Items.Count <= 0)
				return;

			TabPage _Page = new TabPage();
			_Page.SuspendLayout();
			_Page.Location = new System.Drawing.Point(4, 29);
			_Page.Padding = new System.Windows.Forms.Padding(3);
			_Page.TabIndex = nAdapter;

			if (strTabNames == null)
				_Page.Text = string.Format("#{0}", nAdapter + 1);
			else
			{
				if (strTabNames.Length > nAdapter)
					_Page.Text = strTabNames[nAdapter];
				else
					_Page.Text = string.Format("#{0}", nAdapter + 1);
			}
				

			_Page.UseVisualStyleBackColor = false;
            //_Page.BackColor = Color.DimGray;
            //_Page.ForeColor = Color.White;
			_Page.Controls.Add(_ListView);
			_Page.ResumeLayout(false);

            Tab_Input.Controls.Owner.BackColor = Color.DarkGray;
			Tab_Input.SuspendLayout();
			Tab_Input.Controls.Add(_Page);
			Tab_Input.ResumeLayout(false);
		}
		private void TabAdd_Output(int nAdapter, Win_ListView _ListView)
		{
			if (_ListView.Items.Count <= 0)
				return;

			TabPage _Page = new TabPage();
			_Page.SuspendLayout();
			_Page.Location = new System.Drawing.Point(4, 29);
			_Page.Padding = new System.Windows.Forms.Padding(3);
			_Page.TabIndex = nAdapter;

			if (strTabNames == null)
				_Page.Text = string.Format("#{0}", nAdapter + 1);
			else
			{
				if (strTabNames.Length > nAdapter)
					_Page.Text = strTabNames[nAdapter];
				else
					_Page.Text = string.Format("#{0}", nAdapter + 1);
			}

			_Page.UseVisualStyleBackColor = true;
			_Page.Controls.Add(_ListView);
			_Page.ResumeLayout(false);

			Tab_Output.SuspendLayout();
			Tab_Output.Controls.Add(_Page);
			Tab_Output.ResumeLayout(false);
		}

		private void Ctrl_IO_List_View_Resize(object sender, EventArgs e)
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
		private void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}
		private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			int nIndex = e.ItemIndex;
			int nColumn = e.ColumnIndex;
			if (nIndex > -1 && nColumn == 3)
			{
				Graphics g = e.Graphics;

				//사용할 브러쉬 선언
				SolidBrush backBrush = null;
				SolidBrush foreBrush = null;

				//선택된 항목이 아닌경우 일반적으로 브러쉬 생성
				if ((e.ItemState & ListViewItemStates.Focused) == 0)
				{
					//backBrush = new SolidBrush(SystemColors.Window);
					//foreBrush = new SolidBrush(SystemColors.WindowText);

                    backBrush = new SolidBrush(Color.DimGray);
                    foreBrush = new SolidBrush(Color.White);
				}
				else
				{//선택된 항목의 경우는 선택된 항목인 경우 브러쉬 생성
					//backBrush = new SolidBrush(SystemColors.Highlight);
					//foreBrush = new SolidBrush(SystemColors.HighlightText);

                    backBrush = new SolidBrush(Color.DimGray);
                    foreBrush = new SolidBrush(Color.White);
				}

				//강조 인덱스가 설정 된 경우
				if (e.SubItem.Text == "ON")
				{
					if (foreBrush != null)
						foreBrush.Dispose();

					if ((e.ItemState & ListViewItemStates.Focused) == 0)
					{//포커스가 아닌 놈은 선택 색으로 텍스트 브러쉬를 생성
						//backBrush = new SolidBrush(Color.Lime);
						//foreBrush = new SolidBrush(SystemColors.WindowText);

                        //backBrush = new SolidBrush(Color.LightSkyBlue);
                        backBrush = new SolidBrush(Color.LightGreen);
                        foreBrush = new SolidBrush(SystemColors.WindowText);
					}
					else
					{//선택된 항목의 경우 브러쉬 생성
						//backBrush = new SolidBrush(Color.Silver);
						//foreBrush = new SolidBrush(Color.Yellow);

                        backBrush = new SolidBrush(Color.Navy);
                        foreBrush = new SolidBrush(Color.White);
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
            //테스트
			//if (UseOutputControl == false)
			//	return;

			ListView listView = ((ListView)sender);
			int nIndex = listView.SelectedItems[0].Index;
			if (nIndex < 0)
				return;

			string strIOName = listView.Items[nIndex].SubItems[0].Text;

            //테스트
			//if (strIOName.StartsWith("SPARE_") == true)
			//	return; //스페어다.

			DeviceNetIO _IO = m_DNet_IoList.GetLinkData(strIOName, DNET_IO_TYPE.D_OUTPUT);

			if (_IO != null)
			{
				bool bOn = !_IO.IsOn; // 현재값과 반대로 보낸다.
				string strMsg = string.Format("Do you Wan't Send Select IO?\r\n\r\n - IO Name : {0}\r\n - Value : {1}", _IO.IO_Name, (bOn) ? "ON" : "OFF");
				if (!MessageBox.Show(strMsg, "Send IO", MessageBoxButtons.YesNo,
					MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
					return;

				m_DNet.WriteBit(_IO, bOn);
			}
		}
	}
}
