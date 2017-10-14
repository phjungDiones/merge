using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.Communication.SRZ
{
	public partial class Form_SRZ_IO_Maker : Form
	{
		ImageList dumImgList = new ImageList();
		private COM_SRZ_IO_List m_IO_List_DataSet = null;
		public COM_SRZ_IO_List IO_List_DataSet
		{
			get { return m_IO_List_DataSet; }
			set { m_IO_List_DataSet = value; }
		}

		public Form_SRZ_IO_Maker()
		{
			InitializeComponent();
			dumImgList.ImageSize = new System.Drawing.Size(1, 16); //리스트 뷰 라인 간격 조절용
		}
		private void Form_SRZ_IO_Maker_Load(object sender, EventArgs e)
		{
			ComboBox_ModuleType.SelectedIndex = 0;
			Init_CpuList();
			Timer_Refresh.Enabled = true;
		}
		private void Btn_CpuAdd_Click(object sender, EventArgs e)
		{
			AddCPU();
		}
		private void Btn_CpuDel_Click(object sender, EventArgs e)
		{
			DeleteCpu();
		}
		private void AddCPU()
		{
			m_IO_List_DataSet.SRZ_Cpu.Add(new SRZ_CPU_Struct());
			m_IO_List_DataSet.Refresh_IO_Name();
			Init_CpuList();
		}
		private void DeleteCpu()
		{
			int nIndex = ListBox_CPU.SelectedIndex;
			if (nIndex < 0)
			{
				MessageBox.Show("삭제 할 CPU를 선택 하세요!");
				return;
			}
			ListBox_CPU.Items.RemoveAt(nIndex);
			m_IO_List_DataSet.SRZ_Cpu.RemoveAt(nIndex);
			m_IO_List_DataSet.Refresh_IO_Name();
			Init_CpuList();
		}
		private void AddModule()
		{
			int nCpuIndex = ListBox_CPU.SelectedIndex;
			int nModuleType = ComboBox_ModuleType.SelectedIndex;
			if (nCpuIndex < 0 || nModuleType < 0)
			{
				MessageBox.Show("CPU와 모듈 타입을 선택 하세요!");
				return;
			}

			m_IO_List_DataSet.SRZ_Cpu[nCpuIndex].Module_List.Add(new SRZ_Module_Struct((SRZ_IO_TYPE)nModuleType));
			m_IO_List_DataSet.Refresh_IO_Name();
			Init_ModuleList();
		}
		private void DeleteModule()
		{
			int nCpuIndex = ListBox_CPU.SelectedIndex;
			int nModuleIndex = ListBox_Module.SelectedIndex;
			if (nCpuIndex < 0 || nModuleIndex < 0)
			{
				MessageBox.Show("삭제 할 CPU와 모듈을 선택 하세요!");
				return;
			}

			ListBox_Module.Items.RemoveAt(nModuleIndex);
			m_IO_List_DataSet.SRZ_Cpu[nCpuIndex].Module_List.RemoveAt(nModuleIndex);
			m_IO_List_DataSet.Refresh_IO_Name();
			Init_ModuleList();
		}
		private void Init_CpuList()
		{
			ListBox_CPU.Items.Clear();
			for (int nCnt = 0; nCnt < m_IO_List_DataSet.SRZ_Cpu.Count; nCnt++)
			{
				string strName = string.Format("CPU#{0}", nCnt + 1);
				ListBox_CPU.Items.Add(strName);
			}
			ListBox_CPU.SelectedIndex = -1;
			Init_ModuleList();
		}
		private void Init_ModuleList()
		{
			ListBox_Module.Items.Clear();
			int nCpu = ListBox_CPU.SelectedIndex;
			if (nCpu < 0)
				return;
			for (int nCnt = 0; nCnt < m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List.Count; nCnt++)
			{
				ListBox_Module.Items.Add(string.Format("{0} #{1:d02}",m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List[nCnt].IO_Type.ToString(),nCnt+1));
			}
			ListBox_Module.SelectedIndex = -1;
			Init_IoListView();
		}
		private void Init_IoListView()
		{
			ListView_IO_List.SmallImageList = dumImgList;
			ListView_IO_List.Items.Clear();
			int nCpu = ListBox_CPU.SelectedIndex;
			int nModule = ListBox_Module.SelectedIndex;
			if (nCpu < 0 || nModule < 0)
			{
				return;
			}
			switch (m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List[nModule].IO_Type)
			{
				case SRZ_IO_TYPE.TIO_8888:
					{
						int nCnt = 0;
						SetListVIew(ref nCnt, "CUR", m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List[nModule].Read_Value);
						SetListVIew(ref nCnt, "SET", m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List[nModule].Set_Value);
						SetListVIew(ref nCnt, "OUT", m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List[nModule].Read_Out_Value);
					}break;
				case SRZ_IO_TYPE.TIO_VVVV:
					{
						int nCnt = 0;
						SetListVIew(ref nCnt, "ENV", m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List[nModule].Read_Value);
					} break;
				case SRZ_IO_TYPE.DIO:
					{
						int nCnt = 0;
						SetListVIew(ref nCnt, "SET", m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List[nModule].Set_Value);
						SetListVIew(ref nCnt, "READ", m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List[nModule].Read_Value);
					} break;
			}
		}
		private void SetListVIew(ref int nCnt, string strType, List<SRZ_IO> _IOs)
		{
			foreach (SRZ_IO _Io in _IOs)
			{
				ListViewItem Item = ListView_IO_List.Items.Add(string.Format("{0:d02}", ++nCnt)); //Num
				Item.SubItems.Add(string.Format("{0}_{1}",_Io.IO_Type.ToString(), strType)); //IO Type
				Item.SubItems.Add(_Io.IO_Name); //IO Name
				Item.SubItems.Add(""); //IO Value
			}
		}

		private void Btn_ModuleAdd_Click(object sender, EventArgs e)
		{
			AddModule();
		}

		private void Btn_ModuleDel_Click(object sender, EventArgs e)
		{
			DeleteModule();
		}

		private void ListBox_CPU_SelectedIndexChanged(object sender, EventArgs e)
		{
			Init_ModuleList();
		}

		private void ListBox_Module_SelectedIndexChanged(object sender, EventArgs e)
		{
			Init_IoListView();
		}

		private void Btn_DataSave_Click(object sender, EventArgs e)
		{
			m_IO_List_DataSet.SaveSrzIOList();
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
			string strType = listView.Items[nIndex].SubItems[1].Text;
			string strIoName = listView.Items[nIndex].SubItems[2].Text;

			//뿌리기
			Label_Io_Num.Text = strNum;
			Label_Io_Type.Text = strType;
			txtBox_IoName.Text = strIoName;
		}

		private void Btn_IO_List_Change_Click(object sender, EventArgs e)
		{
			if (ListView_IO_List.SelectedItems.Count <= 0)
			{
				return;
			}

			int nCpu = ListBox_CPU.SelectedIndex;
			int nModule = ListBox_Module.SelectedIndex;
			int nIo = ListView_IO_List.SelectedItems[0].Index;

			try
			{
				if (Check_IO_Name(txtBox_IoName.Text, nCpu, nModule) == true)
				{
					MessageBox.Show("동일한 이름의 IO가 존재합니다. 이름을 변경하세요.");
					return;
				}
				if (MessageBox.Show("입력하신 값으로 변경 하시겠습니까?", "값 변경", MessageBoxButtons.YesNo,
					MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).Equals(DialogResult.No))
				{
					return;
				}

				string strType = ListView_IO_List.Items[nIo].SubItems[1].Text;
				string strIoName = ListView_IO_List.Items[nIo].SubItems[2].Text;
				SRZ_IO _IO = m_IO_List_DataSet.GetLinkData(strIoName, nCpu, m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List[nModule].IO_Type);
				_IO.IO_Name = txtBox_IoName.Text;

				if (strType.IndexOf("SET") >= 0)
				{
					if(txtBox_Value.Text != "")
						_IO.Value = float.Parse(txtBox_Value.Text);
				}

				Init_IoListView();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private bool Check_IO_Name(string strName, int nCpu, int nModule)
		{
			bool bRtn = false;
			for (int nCntCpu = 0; nCntCpu < m_IO_List_DataSet.SRZ_Cpu.Count; nCntCpu++)
			{
				for (int nCntModule = 0; nCntModule < m_IO_List_DataSet.SRZ_Cpu[nCntCpu].Module_List.Count; nCntModule++)
				{
					for (int nCntIO = 0; nCntIO < m_IO_List_DataSet.SRZ_Cpu[nCntCpu].Module_List[nCntModule].Read_Value.Count; nCntIO++)
					{
						if (strName == m_IO_List_DataSet.SRZ_Cpu[nCntCpu].Module_List[nCntModule].Read_Value[nCntIO].IO_Name)
						{
							if (!(nCpu == nCntCpu && nModule == nCntModule) && strName != "")
							{//입력된 IO Name이 있을때, 자기꺼는 제외하고, 중복된 이름이 있으면,,
								bRtn = true;
								break;
							}
						}
					}
					if (bRtn == true)
						break;

					for (int nCntIO = 0; nCntIO < m_IO_List_DataSet.SRZ_Cpu[nCntCpu].Module_List[nCntModule].Set_Value.Count; nCntIO++)
					{
						if (strName == m_IO_List_DataSet.SRZ_Cpu[nCntCpu].Module_List[nCntModule].Set_Value[nCntIO].IO_Name)
						{
							if (!(nCpu == nCntCpu && nModule == nCntModule) && strName != "")
							{//입력된 IO Name이 있을때, 자기꺼는 제외하고, 중복된 이름이 있으면,,
								bRtn = true;
								break;
							}
						}
					}
					if (bRtn == true)
						break;

					for (int nCntIO = 0; nCntIO < m_IO_List_DataSet.SRZ_Cpu[nCntCpu].Module_List[nCntModule].Read_Out_Value.Count; nCntIO++)
					{
						if (strName == m_IO_List_DataSet.SRZ_Cpu[nCntCpu].Module_List[nCntModule].Read_Out_Value[nCntIO].IO_Name)
						{
							if (!(nCpu == nCntCpu && nModule == nCntModule) && strName != "")
							{//입력된 IO Name이 있을때, 자기꺼는 제외하고, 중복된 이름이 있으면,,
								bRtn = true;
								break;
							}
						}
					}
					if (bRtn == true)
						break;
				}
				if (bRtn == true)
					break;
			}

			return bRtn;
		}

		private void Timer_Refresh_Tick(object sender, EventArgs e)
		{
			Refesh_IoListView();
		}
		private void Refesh_IoListView()
		{
			int nCpu = ListBox_CPU.SelectedIndex;
			int nModule = ListBox_Module.SelectedIndex;
			if (nCpu < 0 || nModule < 0 || ListView_IO_List.Items.Count <= 0)
			{
				return;
			}

			try
			{
				for (int nIndex = 0; nIndex < ListView_IO_List.Items.Count; nIndex++)
				{
					ListViewItem _Item = ListView_IO_List.Items[nIndex];
					string strIoName = _Item.SubItems[2].Text;
					SRZ_IO _IO = m_IO_List_DataSet.GetLinkData(strIoName, nCpu, m_IO_List_DataSet.SRZ_Cpu[nCpu].Module_List[nModule].IO_Type);
					_Item.SubItems[3].Text = string.Format("{0}", _IO.Value);
				}
			}
			catch
			{ }
		}
		private void Form_SRZ_IO_Maker_FormClosing(object sender, FormClosingEventArgs e)
		{
			Timer_Refresh.Enabled = false;
		}
	}
}
