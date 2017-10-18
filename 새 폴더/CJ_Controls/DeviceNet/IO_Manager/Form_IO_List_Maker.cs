using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.DeviceNet
{
	public partial class Form_IO_List_Maker : Form
	{
		private COM_DeviceNet_IO_List m_IO_List_DataSet = null;
		public COM_DeviceNet_IO_List IO_List_DataSet
		{
			get { return m_IO_List_DataSet; }
			set { m_IO_List_DataSet = value; }
		}

		ImageList dumImgList = new ImageList();
		public Form_IO_List_Maker()
		{
			InitializeComponent();
			dumImgList.ImageSize = new System.Drawing.Size(1, 16); //리스트 뷰 라인 간격 조절용
		}
		private void Init_AdapterList()
		{
			ListBox_Adapter.Items.Clear();
			for (int nCnt = 0; nCnt < m_IO_List_DataSet.DNet_Adapter.Count; nCnt++)
			{
				string strName = string.Format("Adapter#{0}", nCnt+1);
				ListBox_Adapter.Items.Add(strName);
			}
			ListBox_Adapter.SelectedIndex = -1;
			Init_ModuleList();
		}
		private void Init_ModuleList()
		{
			ListBox_Module.Items.Clear();
			int nAdapter = ListBox_Adapter.SelectedIndex;
			if (nAdapter < 0)
				return;
			for (int nCnt = 0; nCnt < m_IO_List_DataSet.DNet_Adapter[nAdapter].Module_List.Count; nCnt++)
			{
				ListBox_Module.Items.Add(m_IO_List_DataSet.DNet_Adapter[nAdapter].Module_List[nCnt].IO_Type.ToString());
			}
			ListBox_Module.SelectedIndex = -1;
			Init_IoListView();
		}
		private void Init_IoListView()
		{
			ListView_IO_List.SmallImageList = dumImgList;
			ListView_IO_List.Items.Clear();
			int nAdapter = ListBox_Adapter.SelectedIndex;
			int nModule = ListBox_Module.SelectedIndex;
			if(nAdapter < 0 || nModule < 0)
			{
				return;
			}

			List<DeviceNetIO> _List = m_IO_List_DataSet.DNet_Adapter[nAdapter].Module_List[nModule].IO_List;
			int nCnt = 0;
			foreach (DeviceNetIO _Io in _List)
			{
				_Io.IO_Type = m_IO_List_DataSet.DNet_Adapter[nAdapter].Module_List[nModule].IO_Type;
				ListViewItem Item = ListView_IO_List.Items.Add(nCnt.ToString()); //Num
				Item.SubItems.Add(_Io.Address.ToString()); //Address

				if (_Io.IO_Type == DNET_IO_TYPE.D_INPUT || _Io.IO_Type == DNET_IO_TYPE.D_OUTPUT)
					Item.SubItems.Add(string.Format("0x{0:x04}", _Io.SubAddr)); //SubAddr (디지털:포지션값, 아날로그:컨피그값)
				else
					Item.SubItems.Add(_Io.SubAddr.ToString()); //SubAddr (디지털:포지션값, 아날로그:컨피그값)
				
				string strIoType = "";
				switch (_Io.IO_Type)
				{
					case DNET_IO_TYPE.D_INPUT: strIoType = "DI"; break;
					case DNET_IO_TYPE.D_OUTPUT: strIoType = "DO"; break;
					case DNET_IO_TYPE.A_INPUT: strIoType = "AI"; break;
					case DNET_IO_TYPE.A_OUTPUT: strIoType = "AO"; break;
				}
				Item.SubItems.Add(strIoType); //Type
				Item.SubItems.Add(_Io.Cable); //Cable
				Item.SubItems.Add(_Io.IO_Name); //IO Name
				nCnt++;
			}

			Label_Io_Num.Text = "";
			Label_Io_Addr.Text = "";
			Label_Io_SubAddr.Text = "";
			txtBox_Cable.Text = "";
			txtBox_IoName.Text = "";
		}

		private void RefreshAddress()
		{
			ushort usInputMax = 0;
			ushort usOutputMax = 0;

			m_IO_List_DataSet.RefreshAddress(ref usInputMax, ref usOutputMax);

			Label_InputMax.Text = usInputMax.ToString();
			Label_OutputMax.Text = usOutputMax.ToString();
		}

		private void Form_IO_List_Maker_Load(object sender, EventArgs e)
		{
			RefreshAddress();
			ComboBox_ModuleType.SelectedIndex = 0;
			Init_AdapterList();
		}

		private void Btn_AdapterAdd_Click(object sender, EventArgs e)
		{
			AddAdapter();
		}

		private void Btn_AdapterDel_Click(object sender, EventArgs e)
		{
			DeleteAdapter();
		}

		private void AddAdapter()
		{
			m_IO_List_DataSet.DNet_Adapter.Add(new DNet_Adapter_Struct());
			Init_AdapterList();
			RefreshAddress();
		}
		private void DeleteAdapter()
		{
			int nIndex = ListBox_Adapter.SelectedIndex;
			if (nIndex < 0)
			{
				MessageBox.Show("삭제 할 아답터를 선택 하세요!");
				return;
			}
			ListBox_Adapter.Items.RemoveAt(nIndex);
			m_IO_List_DataSet.DNet_Adapter.RemoveAt(nIndex);
			RefreshAddress();
			Init_AdapterList();
		}

		private void Btn_ModuleAdd_Click(object sender, EventArgs e)
		{
			AddModule();
		}
		private void Btn_ModuleDel_Click(object sender, EventArgs e)
		{
			DeleteModule();
		}
		private void AddModule()
		{
			int nAdapterIndex = ListBox_Adapter.SelectedIndex;
			int nModuleType = ComboBox_ModuleType.SelectedIndex;
			if (nAdapterIndex < 0 || nModuleType < 0)
			{
				MessageBox.Show("아답터와 모듈 타입을 선택 하세요!");
				return;
			}

			ListBox_Module.Items.Add(((DNET_IO_TYPE)nModuleType).ToString());
			m_IO_List_DataSet.DNet_Adapter[nAdapterIndex].Module_List.Add(new DNet_Module_Struct((DNET_IO_TYPE)nModuleType));
			RefreshAddress();
		}
		private void DeleteModule()
		{
			int nAdapterIndex = ListBox_Adapter.SelectedIndex;
			int nModuleIndex = ListBox_Module.SelectedIndex;
			if (nAdapterIndex < 0 || nModuleIndex < 0)
			{
				MessageBox.Show("삭제 할 아답터와 모듈을 선택 하세요!");
				return;
			}

			ListBox_Module.Items.RemoveAt(nModuleIndex);
			m_IO_List_DataSet.DNet_Adapter[nAdapterIndex].Module_List.RemoveAt(nModuleIndex);
			RefreshAddress();
			Init_ModuleList();
		}

		private void ListBox_Adapter_SelectedIndexChanged(object sender, EventArgs e)
		{
			Init_ModuleList();
		}

		private void ListBox_Module_SelectedIndexChanged(object sender, EventArgs e)
		{
			Init_IoListView();
		}

		private void ListView_IO_List_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView listView = ((ListView)sender);
			if (listView.SelectedItems.Count <= 0)
				return;

			int nIndex = listView.SelectedItems[0].Index;
			if (nIndex < 0)
				return;

			string strNum = listView.Items[nIndex].SubItems[0].Text;
			string strAddr = listView.Items[nIndex].SubItems[1].Text;
			string strSubAddr = listView.Items[nIndex].SubItems[2].Text;
			string strType = listView.Items[nIndex].SubItems[3].Text;
			string strCable = listView.Items[nIndex].SubItems[4].Text;
			string strIoName = listView.Items[nIndex].SubItems[5].Text;

			//뿌리기
			Label_Io_Num.Text = strNum;
			Label_Io_Addr.Text = strAddr;
			Label_Io_SubAddr.Text = strSubAddr;

			if (strCable != "")
				txtBox_Cable.Text = strCable;
			if (strIoName != "")
				txtBox_IoName.Text = strIoName;
		}

		private void ListBox_Adapter_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (MessageBox.Show("선택하신 아답터를 삭제 하시겠습니까?", "아답터 삭제", MessageBoxButtons.YesNo,
					MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).Equals(DialogResult.Yes))
				{
					DeleteAdapter();
				}
			}
		}

		private void ListBox_Module_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (MessageBox.Show("선택하신 모듈을 삭제 하시겠습니까?", "모듈삭제", MessageBoxButtons.YesNo,
					MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).Equals(DialogResult.Yes))
				{
					DeleteModule();
				}
			}
		}

		private void Btn_DataSave_Click(object sender, EventArgs e)
		{
			m_IO_List_DataSet.SaveDeviceNetIOList();
		}

		private void Btn_IO_List_Change_Click(object sender, EventArgs e)
		{
			if (ListView_IO_List.SelectedItems.Count <= 0)
			{
				return;
			}

			int nAdapter = ListBox_Adapter.SelectedIndex;
			int nModule = ListBox_Module.SelectedIndex;
			int nIo = ListView_IO_List.SelectedItems[0].Index;

			try
			{
				if (Check_IO_Name(txtBox_IoName.Text, nAdapter, nModule, nIo) == true)
				{
					MessageBox.Show("동일한 이름의 IO가 존재합니다. 이름을 변경하세요.");
					return;
				}
				if (MessageBox.Show("입력하신 값으로 변경 하시겠습니까?", "값 변경", MessageBoxButtons.YesNo,
					MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).Equals(DialogResult.No))
				{
					return;
				}

				DeviceNetIO _IO = m_IO_List_DataSet.DNet_Adapter[nAdapter].Module_List[nModule].IO_List[nIo];
				_IO.Cable = txtBox_Cable.Text;
				_IO.IO_Name = txtBox_IoName.Text;
                //_IO.Address = Convert.ToUInt16(tbAddr.Text);

				Init_IoListView();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private bool Check_IO_Name(string strName, int nAdapter, int nModule, int nIo)
		{
			bool bRtn = false;
			for (int nCntAdapter = 0; nCntAdapter < m_IO_List_DataSet.DNet_Adapter.Count; nCntAdapter++)
			{
				for (int nCntModule = 0; nCntModule < m_IO_List_DataSet.DNet_Adapter[nCntAdapter].Module_List.Count; nCntModule++)
				{
					for (int nCntIO = 0; nCntIO < m_IO_List_DataSet.DNet_Adapter[nCntAdapter].Module_List[nCntModule].IO_List.Count; nCntIO++)
					{
						if (strName == m_IO_List_DataSet.DNet_Adapter[nCntAdapter].Module_List[nCntModule].IO_List[nCntIO].IO_Name)
						{
							if (!(nAdapter == nCntAdapter && nModule == nCntModule && nIo == nCntIO) && strName != "")
							{//입력된 IO Name이 있을때, 자기꺼는 제외하고, 중복된 이름이 있으면,,
								bRtn = true;
								break;
							}
						}
					}
					if(bRtn == true)
						break;
				}
				if(bRtn == true)
					break;
			}

			return bRtn;
		}
	}
}
