

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace CJ_Controls.Log_View
{
	public partial class Form_LogView : Form
	{
		private string m_strFilePath = "";
		private int m_nPosition = 0;
		List<long> m_ListPageFirst = new List<long>();
		Encoding m_UserEncoding = Encoding.Default;//Encoding.GetEncoding( "ks_c_5601-1987" );
		Thread m_LineCountThread = null;

		public Form_LogView()
		{
			InitializeComponent();
			m_LineCountThread = new Thread(new ThreadStart(GetSizeForAll));
			m_LineCountThread.IsBackground = true;
		}

		private void Form_LogView_Load(object sender, EventArgs e)
		{
			m_nPosition = 0;
			ButtonDisable(false);
			m_LineCountThread.Start();
		}

		public string FilePath
		{
			get { return m_strFilePath; }
			set
			{
				m_strFilePath = value;
				TextBox_Path.Text = m_strFilePath;
			}
		}

		private void ButtonDisable(bool bEnable)
		{
			Button_First.Enabled = bEnable;
			Button_Next.Enabled = bEnable;
			Button_Prev.Enabled = bEnable;
			Button_Last.Enabled = bEnable;
		}
		private void GetSizeForAll()
		{
			m_ListPageFirst.Clear();

			try
			{
				this.BeginInvoke(new MethodInvoker(delegate
				{
					this.Text = "Log Viewer - File Loading....";
					ButtonDisable(true);
					GetFileData();
				}));

				using (StreamReader sReader = new StreamReader(new FileStream(m_strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), m_UserEncoding))
				{
					sReader.BaseStream.Seek(0, SeekOrigin.Begin);

					long nLinePos = 0;
					long nFirstSize = 0;
					string strGet = "";
					m_ListPageFirst.Add(nFirstSize);

					while (sReader.Peek() > -1)
					{
						strGet = "";
						if (nLinePos != 0 && (nLinePos % 1000) == 0)
							m_ListPageFirst.Add(nFirstSize);

						strGet = sReader.ReadLine();
						nLinePos += 1;
						int nLen = Encoding.Default.GetByteCount(strGet) + Encoding.Default.GetByteCount("\r\n");
						nFirstSize += nLen;//Convert.ToUInt32(strGet.Length) + 2);

						if (nLinePos % 5000 == 0)
							Thread.Sleep(10);
					}

					sReader.Close();

					this.BeginInvoke(new MethodInvoker(delegate
						{
							this.Text = "Log Viewer - File Load Complete!";
							Label_PageValue.Text = string.Format("{0} / {1}", m_nPosition + 1, m_ListPageFirst.Count);
						}));
				}
			}
			catch (Exception ex)
			{
// 				this.BeginInvoke(new MethodInvoker(delegate
// 				{
// 					ButtonDisable(true);
// 				}));
				Console.WriteLine(ex.Message);
			}
		}
		private void GetFileData()
		{
			if (m_strFilePath.Length <= 0)
				return;

			ListBox_Log.Items.Clear();

			try
			{
				using (StreamReader sReader = new StreamReader(new FileStream(m_strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), m_UserEncoding))
				{
					sReader.BaseStream.Seek(m_ListPageFirst[m_nPosition], SeekOrigin.Begin);

					Label_PageValue.Text = string.Format("{0} / {1}", m_nPosition + 1, m_ListPageFirst.Count);

					int nPos = 0;
					string strGet = "";

					DateTime dt = DateTime.Now;
					List<string> _list = new List<string>();
					
					while (sReader.Peek() > -1)
					{
						strGet = "";
						strGet = sReader.ReadLine();
						_list.Add(strGet);
						nPos += 1;
						if (nPos >= 1000)
						{
							break;
						}
					}

					ListBox_Log.Items.AddRange(_list.ToArray());
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void Button_First_Click(object sender, EventArgs e)
		{
			if (m_nPosition != 0)
			{
				m_nPosition = 0;
				GetFileData();
			}
		}
		private void Button_Next_Click(object sender, EventArgs e)
		{
			m_nPosition += 1;
			if (m_ListPageFirst.Count > m_nPosition)
				GetFileData();
			else
				m_nPosition = m_ListPageFirst.Count - 1;
		}
		private void Button_Last_Click(object sender, EventArgs e)
		{
			if (m_nPosition != (m_ListPageFirst.Count - 1))
			{
				m_nPosition = m_ListPageFirst.Count - 1;
				GetFileData();
			}
		}
		private void Button_Prev_Click(object sender, EventArgs e)
		{
			m_nPosition -= 1;
			if (0 <= m_nPosition)
				GetFileData();
			else
				m_nPosition = 0;
		}

		private void Form_LogView_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (m_LineCountThread.IsAlive)
				m_LineCountThread.Abort();
		}

		private void TextBox_FindVal_TextChanged(object sender, EventArgs e)
		{
			ListBox_Log.Refresh();
		}

		private void ListBox_Log_DrawItem(object sender, DrawItemEventArgs e)
		{
			//에러처리...
			if (e.Index > -1)
			{
				Graphics g = e.Graphics;

				//사용할 브러쉬 선언
				SolidBrush backBrush = null;
				SolidBrush foreBrush = null;

				//선택된 항목이 아닌경우 일반적으로 브러쉬 생성
				if ((e.State & DrawItemState.Focus) == 0)
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
				string strFind = TextBox_FindVal.Text.ToUpper();
				if (strFind.Length > 0)
				{
					string strText = ((ListBox)sender).Items[e.Index].ToString().ToUpper();
					if (strText.IndexOf(strFind, 0) >= 0)
					{
						if (foreBrush != null)
							foreBrush.Dispose();
						
						if ((e.State & DrawItemState.Focus) == 0)
						{//포커스가 아닌 놈은 선택 색으로 텍스트 브러쉬를 생성
							backBrush = new SolidBrush(Color.Yellow);
							foreBrush = new SolidBrush(Color.Blue);
						}
						else
						{//선택된 항목의 경우 브러쉬 생성
							backBrush = new SolidBrush(Color.Green);
							foreBrush = new SolidBrush(Color.Yellow);
						}
					}
				}

				//배경색 채우기
				e.Graphics.FillRectangle(backBrush, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

				//아이템 텍스트 Draw
				e.Graphics.DrawString( ((ListBox)sender).Items[e.Index].ToString(), this.Font, foreBrush, e.Bounds.X, e.Bounds.Y);

				//리소스 해제
				if (backBrush != null)
					backBrush.Dispose();

				if (foreBrush != null)
					foreBrush.Dispose();
			}
		}
	}
}