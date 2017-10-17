using CJ_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.PmacLib
{
	public partial class Form_Engr_Io_List : Form
	{
		public Form_Engr_Io_List()
		{
			InitializeComponent();
		}

		private PMacOneBoardCtrl _PmacCtrl = null;
		public PMacOneBoardCtrl PMacControl
		{
			get { return _PmacCtrl; }
			set { _PmacCtrl = value; }
		}

		private void Form_Engr_Io_List_Load(object sender, EventArgs e)
		{
			Io_Display();
		}
		private void Io_Display()
		{
			Io_Display_Input();
			Io_Display_Output();
		}
		private void Io_Display_Input()
		{
			ListView _ListView = ListView_InputList;
			_ListView.Items.Clear();

			IO_List _List = _PmacCtrl.IoList_All;
			int nCnt = 0;
			foreach (DeviceNetIO _Io in _List.Input)
			{
				nCnt++;
				//no,add,subaddr,type,cable,ioname
				ListViewItem Item = _ListView.Items.Add(nCnt.ToString("000")); //No
				Item.SubItems.Add(_Io.Address.ToString()); //Addr
				Item.SubItems.Add(_Io.Description.ToString()); //SubAddr
				Item.SubItems.Add(_Io.IO_Type.ToString()); //Type
				Item.SubItems.Add(_Io.Cable.ToString()); //cable
				Item.SubItems.Add(_Io.IO_Name.ToString()); //IoName
			}
		}
		private void Io_Display_Output()
		{
			ListView _ListView = ListView_OutputList;
			_ListView.Items.Clear();

			IO_List _List = _PmacCtrl.IoList_All;
			int nCnt = 0;
			foreach (DeviceNetIO _Io in _List.Output)
			{
				nCnt++;
				//no,add,subaddr,type,cable,ioname
				ListViewItem Item = _ListView.Items.Add(nCnt.ToString("000")); //No
				Item.SubItems.Add(_Io.Address.ToString()); //Addr
				Item.SubItems.Add(_Io.Description.ToString()); //SubAddr
				Item.SubItems.Add(_Io.IO_Type.ToString()); //Type
				Item.SubItems.Add(_Io.Cable.ToString()); //cable
				Item.SubItems.Add(_Io.IO_Name.ToString()); //IoName
			}
		}

		private void Btn_ServoData_Save_Click(object sender, EventArgs e)
		{
			Listview_To_IoList();
			_PmacCtrl.SaveIOList();
		}
		private void Listview_To_IoList()
		{
			_PmacCtrl.IoList_All.Input.Clear();
			ListView _ListView = ListView_InputList;
			for (int nRow = 0; nRow < _ListView.Items.Count; nRow++)
			{
				string strNum = _ListView.Items[nRow].SubItems[0].Text;
				string strAddr = _ListView.Items[nRow].SubItems[1].Text;
				strAddr = strAddr.Replace("M", "");
				strAddr = strAddr.Replace("->", "");
				strAddr = strAddr.Replace(" ", "");
				ushort uAddr = ushort.Parse(strAddr);
				string strSubAddr = _ListView.Items[nRow].SubItems[2].Text;
				string strType = _ListView.Items[nRow].SubItems[3].Text;
				string strCable = _ListView.Items[nRow].SubItems[4].Text;
				string strIoName = _ListView.Items[nRow].SubItems[5].Text;

				DeviceNetIO _IO = new DeviceNetIO(strIoName, uAddr, 0, DNET_IO_TYPE.D_INPUT, strCable, strSubAddr);
				_PmacCtrl.IoList_All.Input.Add(_IO);
			}

			_PmacCtrl.IoList_All.Output.Clear();
			_ListView = ListView_OutputList;
			for (int nRow = 0; nRow < _ListView.Items.Count; nRow++)
			{
				string strNum = _ListView.Items[nRow].SubItems[0].Text;
				string strAddr = _ListView.Items[nRow].SubItems[1].Text;
				strAddr = strAddr.Replace("M", "");
				strAddr = strAddr.Replace("->", "");
				strAddr = strAddr.Replace(" ", "");
				ushort uAddr = ushort.Parse(strAddr);
				string strSubAddr = _ListView.Items[nRow].SubItems[2].Text;
				string strType = _ListView.Items[nRow].SubItems[3].Text;
				string strCable = _ListView.Items[nRow].SubItems[4].Text;
				string strIoName = _ListView.Items[nRow].SubItems[5].Text;

				DeviceNetIO _IO = new DeviceNetIO(strIoName, uAddr, 0, DNET_IO_TYPE.D_OUTPUT, strCable, strSubAddr);
				_PmacCtrl.IoList_All.Output.Add(_IO);
			}
		}
		private void Btn_ServoData_Reload_Click(object sender, EventArgs e)
		{
			_PmacCtrl.LoadIOList();
			Io_Display();
		}
		private void Btn_Close_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("저장 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				Btn_ServoData_Save.PerformClick();
			}

			if (MessageBox.Show("종료 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				this.Close();
			}
		}

		private void ListView_InputList_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
				{
					ListView _listview = sender as ListView;
					SetListView(_listview);
				}
				else if (e.KeyCode == Keys.Delete)
				{

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("붙여넣기 실패!\r\n" + ex.Message);
			}
		}
		private void ListView_OutputList_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
				{
					ListView _listview = sender as ListView;
					SetListView(_listview);
				}
				else if (e.KeyCode == Keys.Delete)
				{

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("붙여넣기 실패!\r\n" + ex.Message);
			}
		}
		private List<string> PasteClipboardData()
		{
			List<string> _RtnString = new List<string>();
			bool bOK = false;
			try
			{
				char[] rowSplitter = { '\r', '\n' };
				IDataObject dataInClipboard = Clipboard.GetDataObject();
				string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);
				string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < rowsInClipboard.Length; i++)
				{
					_RtnString.Add(rowsInClipboard[i]);
				}
				bOK = true;
			}
			catch
			{
				bOK = false;
			}
			finally
			{
				if (bOK == false)
				{
					MessageBox.Show("형식이 맞지 않는 문자가 있습니다!");
				}
			}
			return _RtnString;
		}
		private void SetListView(ListView _listview)
		{
			_listview.Items.Clear();
			List<string> _RowString = PasteClipboardData();
			char[] columnSplitter = { '\t' };
			int nCnt = 0;
			for (int nRow = 0; nRow < _RowString.Count; nRow++)
			{
				string[] strColInClipboard = _RowString[nRow].Split(columnSplitter, StringSplitOptions.None);
				if (strColInClipboard.Length < 1)
				{
					continue;
				}
				if (strColInClipboard[0] == "")
				{
					continue;
				}

				nCnt++;
				ListViewItem Item = _listview.Items.Add(nCnt.ToString("000")); //No

				if (strColInClipboard.Length >= 1)
				{
					string strAddr = strColInClipboard[0].ToString();
					strAddr = strAddr.Replace("M", "");
					strAddr = strAddr.Replace("->", "");
					strAddr = strAddr.Replace(" ", "");
					Item.SubItems.Add(strAddr); //Addr
				}
				else Item.SubItems.Add(""); //Addr

				if (strColInClipboard.Length >= 2) Item.SubItems.Add(strColInClipboard[1].ToString()); //SubAddr
				else Item.SubItems.Add(""); //SubAddr

				if (Tab_IoList.SelectedIndex == 0)	Item.SubItems.Add(DNET_IO_TYPE.D_INPUT.ToString()); //Type
				else Item.SubItems.Add(DNET_IO_TYPE.D_OUTPUT.ToString()); //Type

				if (strColInClipboard.Length >= 3) Item.SubItems.Add(strColInClipboard[2].ToString()); //cable
				else Item.SubItems.Add(""); //cable

				if (strColInClipboard.Length >= 4) Item.SubItems.Add(strColInClipboard[3].ToString()); //IoName
				else Item.SubItems.Add(""); //IoName
			}
		}

		private void ListView_InputList_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView _ListView = sender as ListView;
			if (_ListView.SelectedItems.Count > 0)
			{
				int nIndex = _ListView.SelectedItems[0].Index;
				Text_Input_Addr.Text = _ListView.Items[nIndex].SubItems[1].Text;
				Text_Input_SubAddr.Text = _ListView.Items[nIndex].SubItems[2].Text;
				Text_Input_Type.Text = DNET_IO_TYPE.D_INPUT.ToString();
				Text_Input_Cable.Text = _ListView.Items[nIndex].SubItems[4].Text;
				Text_Input_IoName.Text = _ListView.Items[nIndex].SubItems[5].Text;
			}
		}

		private void Btn_Input_Apply_Click(object sender, EventArgs e)
		{
			ListView _ListView = ListView_InputList;
			if (_ListView.SelectedItems.Count > 0)
			{
				int nIndex = _ListView.SelectedItems[0].Index;
				_ListView.Items[nIndex].SubItems[1].Text = Text_Input_Addr.Text;
				_ListView.Items[nIndex].SubItems[2].Text = Text_Input_SubAddr.Text;
				_ListView.Items[nIndex].SubItems[4].Text = Text_Input_Cable.Text;
				_ListView.Items[nIndex].SubItems[5].Text = Text_Input_IoName.Text;
			}
		}

		private void ListView_OutputList_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView _ListView = sender as ListView;
			if (_ListView.SelectedItems.Count > 0)
			{
				int nIndex = _ListView.SelectedItems[0].Index;
				Text_Output_Addr.Text = _ListView.Items[nIndex].SubItems[1].Text;
				Text_Output_SubAddr.Text = _ListView.Items[nIndex].SubItems[2].Text;
				Text_Output_Type.Text = DNET_IO_TYPE.D_INPUT.ToString();
				Text_Output_Cable.Text = _ListView.Items[nIndex].SubItems[4].Text;
				Text_Output_IoName.Text = _ListView.Items[nIndex].SubItems[5].Text;
			}
		}

		private void Btn_Output_Apply_Click(object sender, EventArgs e)
		{
			ListView _ListView = ListView_OutputList;
			if (_ListView.SelectedItems.Count > 0)
			{
				int nIndex = _ListView.SelectedItems[0].Index;
				_ListView.Items[nIndex].SubItems[1].Text = Text_Output_Addr.Text;
				_ListView.Items[nIndex].SubItems[2].Text = Text_Output_SubAddr.Text;
				_ListView.Items[nIndex].SubItems[4].Text = Text_Output_Cable.Text;
				_ListView.Items[nIndex].SubItems[5].Text = Text_Output_IoName.Text;
			}
		}
	}
}
