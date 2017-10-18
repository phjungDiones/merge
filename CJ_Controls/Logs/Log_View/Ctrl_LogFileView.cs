using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CJ_Controls.Log_View
{
	public partial class Ctrl_LogFileView : UserControl
	{
		private string m_LogPathRoot = "Log";

		public string LogPathRoot
		{
			get { return m_LogPathRoot; }
			set { m_LogPathRoot = value; }
		}
		public Ctrl_LogFileView()
		{
			InitializeComponent();
		}

		private void LogFileView_Load(object sender, EventArgs e)
		{
			Static_SettreeViewLogTree();
		}

		public void Static_SettreeViewLogTree()
		{
			LogTree.Nodes.Clear();

			if (!Directory.Exists(m_LogPathRoot) && m_LogPathRoot != string.Empty)
			{
				Directory.CreateDirectory(m_LogPathRoot);
			}

			DirectoryInfo root = new DirectoryInfo(m_LogPathRoot);
			TreeNode treeNode = new TreeNode(root.FullName, 0, 0);

			LogTree.Nodes.Add(treeNode);
			Static_LoopDir(root, treeNode);
			LogTree.ExpandAll();
		}
		private void Static_LoopDir(DirectoryInfo dir, TreeNode n)
		{
			DirectoryInfo[] dirInfo = dir.GetDirectories();

			foreach (DirectoryInfo Folder in dirInfo)
			{
				TreeNode treeNode = new TreeNode(Folder.Name, 0, 1);
				n.Nodes.Add(treeNode);
				try
				{
					//접근할 수 없는 폴더접근 > Error!! 

					if ((Folder.GetDirectories()).Length > 0)
					{
						this.Static_LoopDir(Folder, treeNode);
					}

				}
				catch { continue; }
			}

		}
		private void LogTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			DisplaylistViewLogFile(rdAll.Checked);
		}

		private void DisplaylistViewLogFile(bool bAll)
		{
			try
			{
				LogFileListView.Items.Clear();
				DirectoryInfo dir = new DirectoryInfo(LogTree.SelectedNode.FullPath);
				FileInfo[] fArr = dir.GetFiles();
				if (fArr.Length > 0)
				{
					bool bCsvFile = false;
					if (bAll)
					{
						foreach (FileInfo f in fArr)
						{
							try
							{
								if (f.Name.Remove(0, f.Name.Length - 3) == "csv")
									bCsvFile = true;
								else
									bCsvFile = false;
								ListViewItem item = new ListViewItem(f.Name, bCsvFile ? 1 : 0);
								item.SubItems.Add(f.CreationTime.ToString("yyyy-MM-dd"));
								item.SubItems.Add((f.Length / 1024).ToString("0,0") + " KB");
								LogFileListView.Items.Add(item);
								
							}
							catch
							{

							}
						}
					}
					else
					{
						foreach (FileInfo f in fArr)
						{
							try
							{
								if (f.Name.Remove(0, f.Name.Length - 3) == "csv")
									bCsvFile = true;
								else
									bCsvFile = false;


								string strStart = String.Format("{0:0000}{1:00}{2:00}", dtStartTime.Value.Year, dtStartTime.Value.Month, dtStartTime.Value.Day);
								string strEnd = String.Format("{0:0000}{1:00}{2:00}", dtEndTime.Value.Year, dtEndTime.Value.Month, dtEndTime.Value.Day);
								string strDate = f.CreationTime.ToString("yyyyMMdd");
								if (CanConvertToInt32(strDate))
								{
									if ((Convert.ToInt32(strDate) >= Convert.ToInt32(strStart)) && (Convert.ToInt32(strDate) <= Convert.ToInt32(strEnd)))
									{
										ListViewItem item = new ListViewItem(f.Name, bCsvFile ? 1 : 0);
										item.SubItems.Add(f.CreationTime.ToString("yyyy-MM-dd"));
										item.SubItems.Add((f.Length / 1024).ToString("0,0") + " KB");
										LogFileListView.Items.Add(item);
									}
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{

			}
		}

		private bool CanConvertToInt32(string strValue)
		{
			char[] digits = strValue.ToCharArray();

			foreach (char dig in digits)
			{
				if (dig < '0' || dig > '9')
				{
					return false;
				}
			}
			return true;
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			DisplaylistViewLogFile(rdAll.Checked);
		}

		private void Button_FolderRefresh_Click(object sender, EventArgs e)
		{
			Static_SettreeViewLogTree();
			DisplaylistViewLogFile(rdAll.Checked);
		}

		private void LogFileListView_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (LogFileListView.SelectedItems.Count > 0)
				{
					ListViewItem item = LogFileListView.SelectedItems[0];
					string strFilePath = LogTree.SelectedNode.FullPath.ToString() + "\\" + item.Text;

					//this.BeginInvoke(new MethodInvoker(delegate
					//{
						Form_LogView FormLogView = new Form_LogView();
						FormLogView.FilePath = strFilePath;
						FormLogView.ShowDialog();
						GC.Collect();
					//}));
				}
				foreach (ListViewItem item in LogFileListView.Items)
				{
					item.Selected = false;
				}
			}
			catch
			{
			}
		}
	}
}
