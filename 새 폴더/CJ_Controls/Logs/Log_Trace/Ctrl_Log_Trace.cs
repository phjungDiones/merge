using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CJ_Controls.Log_Trace
{
	public partial class Ctrl_Log_Trace : UserControl
	{
		int m_nMaxCount = 500;
		private System.Windows.Forms.ImageList RowHeight; //높이 조절
		public event MessageSenderEventHandler _MessageView = null;
		private delegate void LogView(MessageSenderEventArg e);
		Delegate _Delegate_Log = null;
		public Ctrl_Log_Trace()
		{
			InitializeComponent();
			RowHeight = new ImageList();
			RowHeight.ImageSize = new System.Drawing.Size(1, 16);
			listView_Msg.SmallImageList = RowHeight;
			_MessageView += new MessageSenderEventHandler(MsgRecived);
			_Delegate_Log = new LogView(SetLog);
		}

		private void LogControl_Load(object sender, EventArgs e)
		{

		}

		private void listView_Msg_Resize(object sender, EventArgs e)
		{
			int nWidth = ((ListView)sender).Width;
			int nColumnCount = ((ListView)sender).Columns.Count;
			for (int i = 0; i < nColumnCount; i++)
			{
				if (i < nColumnCount - 1)
				{//맨뒤를 제외한 이전 컬럼의 넓이를 다 뺀다.
					nWidth -= ((ListView)sender).Columns[i].Width;
				}
				else
				{//앞의 컬럼들을 포함해 넓이가 일정 수준이상이면 빼준다..
					if(nWidth > 200)
						((ListView)sender).Columns[i].Width = nWidth - 22;
				}
			}
		}
		private void button_Clear_Click(object sender, EventArgs e)
		{
			listView_Msg.Items.Clear();
		}

		public Color CaptionBack
		{
			get { return label_Caption.BackColor; }
			set
			{
				panel_Caption.BackColor = value;
				label_Caption.BackColor = value;
			}
		}
		public Color CaptionFore
		{
			get { return label_Caption.ForeColor; }
			set
			{
				label_Caption.ForeColor = value;
			}
		}

		public bool HideBottomStatus
		{
			get { return panel_Status.Visible; }
			set
			{
				panel_Status.Visible = value;
			}
		}

		public string Caption
		{
			get { return label_Caption.Text; }
			set
			{
				label_Caption.Text = value;
			}
		}

		#region 외부에서 호출 할 함수
		public void AddMsg(string strInfo, string strMsg, Color txtColor)
		{
			_MessageView(this, new MessageSenderEventArg(DateTime.Now, strInfo, strMsg, txtColor));
		}
		#endregion

		private void MsgRecived(object sender, MessageSenderEventArg e)
		{
			try
			{
				this.BeginInvoke(_Delegate_Log, e);
			}
			catch { }
			//Invoke(_Delegate_Log, e);
		}

		private void SetLog(MessageSenderEventArg e)
		{
			ListViewItem Item = listView_Msg.Items.Add("");
			Item.ForeColor = e.TextColor;
			Item.SubItems.Add(string.Format("{0:yyyy-MM-dd} {1:HH:mm:ss}.{2:d03}", e.SaveTime, e.SaveTime, e.SaveTime.Millisecond)); //날짜/시간
			Item.SubItems.Add(e.InfoMsg); //정보
			Item.SubItems.Add(e.Message); //메세지

			int nCount = listView_Msg.Items.Count;
			if (nCount > m_nMaxCount)
			{
				listView_Msg.Items.RemoveAt(0);
				nCount -= 1;
			}

			listView_Msg.EnsureVisible(nCount - 1);
			listView_Msg.SelectedIndices.Add(nCount - 1);
		}

		private void listView_Msg_DoubleClick(object sender, EventArgs e)
		{
			ListView listView = ((ListView)sender);
			int nIndex = listView.SelectedItems[0].Index;
			if (nIndex < 0)
				return;

			string strDateTime = listView.Items[nIndex].SubItems[1].Text;
			string strInfo = listView.Items[nIndex].SubItems[2].Text;
			string strMsg = listView.Items[nIndex].SubItems[3].Text;

			Form_TraceOneLog dlg = new Form_TraceOneLog();
			dlg.SetMessage(strDateTime, strInfo, strMsg);
			dlg.ShowDialog();
		}
		
	}

	public delegate void MessageSenderEventHandler(object sender, MessageSenderEventArg e);
	public class MessageSenderEventArg : EventArgs
	{
		private DateTime _SaveTime = DateTime.Now;
		private string _InfoMsg = "";
		private string _Message = "";
		private Color _Color = Color.Black;
		public DateTime SaveTime
		{
			get { return _SaveTime; }
			set { _SaveTime = value; }
		}
		public string InfoMsg
		{
			get { return _InfoMsg; }
			set { _InfoMsg = value; }
		}
		public string Message
		{
			get { return _Message; }
			set { _Message = value; }
		}
		public Color TextColor
		{
			get { return _Color; }
			set { _Color = value; }
		}
		public MessageSenderEventArg(DateTime dt, string strInfo, string strMsg, Color txtColor)
		{
			_SaveTime = dt;
			_InfoMsg = strInfo;
			_Message = strMsg;
			_Color = txtColor;
		}
	}
}
