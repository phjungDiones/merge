namespace CJ_Controls.Log_View
{
	partial class Ctrl_LogFileView
	{
		/// <summary> 
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 구성 요소 디자이너에서 생성한 코드

		/// <summary> 
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ctrl_LogFileView));
            this.Panel_LogView_Top = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtStartTime = new System.Windows.Forms.DateTimePicker();
            this.rdSelectedDate = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.imageListLog = new System.Windows.Forms.ImageList(this.components);
            this.imageListFolser = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LogTree = new System.Windows.Forms.TreeView();
            this.Button_FolderRefresh = new System.Windows.Forms.Button();
            this.label174 = new System.Windows.Forms.Label();
            this.LogFileListView = new System.Windows.Forms.ListView();
            this.ColFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.Panel_LogView_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_LogView_Top
            // 
            this.Panel_LogView_Top.Controls.Add(this.btnRefresh);
            this.Panel_LogView_Top.Controls.Add(this.label1);
            this.Panel_LogView_Top.Controls.Add(this.dtEndTime);
            this.Panel_LogView_Top.Controls.Add(this.dtStartTime);
            this.Panel_LogView_Top.Controls.Add(this.rdSelectedDate);
            this.Panel_LogView_Top.Controls.Add(this.rdAll);
            this.Panel_LogView_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_LogView_Top.Location = new System.Drawing.Point(0, 0);
            this.Panel_LogView_Top.Name = "Panel_LogView_Top";
            this.Panel_LogView_Top.Size = new System.Drawing.Size(1264, 71);
            this.Panel_LogView_Top.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(671, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(137, 30);
            this.btnRefresh.TabIndex = 52;
            this.btnRefresh.Text = "Day Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(435, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "~";
            // 
            // dtEndTime
            // 
            this.dtEndTime.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.dtEndTime.Location = new System.Drawing.Point(451, 26);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Size = new System.Drawing.Size(207, 21);
            this.dtEndTime.TabIndex = 50;
            // 
            // dtStartTime
            // 
            this.dtStartTime.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.dtStartTime.Location = new System.Drawing.Point(225, 26);
            this.dtStartTime.Name = "dtStartTime";
            this.dtStartTime.Size = new System.Drawing.Size(207, 21);
            this.dtStartTime.TabIndex = 49;
            // 
            // rdSelectedDate
            // 
            this.rdSelectedDate.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdSelectedDate.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdSelectedDate.Checked = true;
            this.rdSelectedDate.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.rdSelectedDate.Location = new System.Drawing.Point(119, 20);
            this.rdSelectedDate.Name = "rdSelectedDate";
            this.rdSelectedDate.Size = new System.Drawing.Size(100, 30);
            this.rdSelectedDate.TabIndex = 48;
            this.rdSelectedDate.TabStop = true;
            this.rdSelectedDate.Text = "Date";
            this.rdSelectedDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdAll
            // 
            this.rdAll.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdAll.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdAll.Enabled = false;
            this.rdAll.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.rdAll.Location = new System.Drawing.Point(13, 20);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(100, 30);
            this.rdAll.TabIndex = 47;
            this.rdAll.Text = "All";
            this.rdAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageListLog
            // 
            this.imageListLog.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLog.ImageStream")));
            this.imageListLog.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLog.Images.SetKeyName(0, "File_NonSelect.ico");
            this.imageListLog.Images.SetKeyName(1, "File_Select.ico");
            // 
            // imageListFolser
            // 
            this.imageListFolser.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFolser.ImageStream")));
            this.imageListFolser.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListFolser.Images.SetKeyName(0, "Folder_Closed.ico");
            this.imageListFolser.Images.SetKeyName(1, "Folder_Open.ico");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 71);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LogTree);
            this.splitContainer1.Panel1.Controls.Add(this.Button_FolderRefresh);
            this.splitContainer1.Panel1.Controls.Add(this.label174);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LogFileListView);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 748);
            this.splitContainer1.SplitterDistance = 354;
            this.splitContainer1.TabIndex = 1;
            // 
            // LogTree
            // 
            this.LogTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTree.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LogTree.ImageIndex = 0;
            this.LogTree.ImageList = this.imageListFolser;
            this.LogTree.Location = new System.Drawing.Point(0, 38);
            this.LogTree.Name = "LogTree";
            this.LogTree.SelectedImageIndex = 0;
            this.LogTree.Size = new System.Drawing.Size(354, 660);
            this.LogTree.TabIndex = 683;
            this.LogTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LogTree_AfterSelect);
            // 
            // Button_FolderRefresh
            // 
            this.Button_FolderRefresh.BackColor = System.Drawing.Color.MidnightBlue;
            this.Button_FolderRefresh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Button_FolderRefresh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Button_FolderRefresh.ForeColor = System.Drawing.Color.White;
            this.Button_FolderRefresh.Location = new System.Drawing.Point(0, 698);
            this.Button_FolderRefresh.Name = "Button_FolderRefresh";
            this.Button_FolderRefresh.Size = new System.Drawing.Size(354, 50);
            this.Button_FolderRefresh.TabIndex = 682;
            this.Button_FolderRefresh.Text = "Directory Refresh";
            this.Button_FolderRefresh.UseVisualStyleBackColor = false;
            this.Button_FolderRefresh.Click += new System.EventHandler(this.Button_FolderRefresh_Click);
            // 
            // label174
            // 
            this.label174.BackColor = System.Drawing.Color.Gainsboro;
            this.label174.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label174.Dock = System.Windows.Forms.DockStyle.Top;
            this.label174.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label174.ForeColor = System.Drawing.Color.Black;
            this.label174.Location = new System.Drawing.Point(0, 0);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(354, 38);
            this.label174.TabIndex = 680;
            this.label174.Text = "Folder";
            this.label174.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LogFileListView
            // 
            this.LogFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColFile,
            this.ColDate,
            this.colSize});
            this.LogFileListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogFileListView.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LogFileListView.FullRowSelect = true;
            this.LogFileListView.Location = new System.Drawing.Point(0, 38);
            this.LogFileListView.Name = "LogFileListView";
            this.LogFileListView.Size = new System.Drawing.Size(906, 710);
            this.LogFileListView.SmallImageList = this.imageListLog;
            this.LogFileListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LogFileListView.TabIndex = 682;
            this.LogFileListView.UseCompatibleStateImageBehavior = false;
            this.LogFileListView.View = System.Windows.Forms.View.Details;
            this.LogFileListView.DoubleClick += new System.EventHandler(this.LogFileListView_DoubleClick);
            // 
            // ColFile
            // 
            this.ColFile.Tag = "";
            this.ColFile.Text = "File";
            this.ColFile.Width = 431;
            // 
            // ColDate
            // 
            this.ColDate.Text = "Date";
            this.ColDate.Width = 176;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.Width = 100;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(906, 38);
            this.label2.TabIndex = 681;
            this.label2.Text = "File List";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ctrl_LogFileView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.Panel_LogView_Top);
            this.Name = "Ctrl_LogFileView";
            this.Size = new System.Drawing.Size(1264, 819);
            this.Load += new System.EventHandler(this.LogFileView_Load);
            this.Panel_LogView_Top.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel Panel_LogView_Top;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dtEndTime;
		private System.Windows.Forms.DateTimePicker dtStartTime;
		private System.Windows.Forms.RadioButton rdSelectedDate;
		private System.Windows.Forms.RadioButton rdAll;
		private System.Windows.Forms.ImageList imageListFolser;
		private System.Windows.Forms.ImageList imageListLog;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label174;
		private System.Windows.Forms.ListView LogFileListView;
		private System.Windows.Forms.ColumnHeader ColFile;
		private System.Windows.Forms.ColumnHeader ColDate;
		private System.Windows.Forms.ColumnHeader colSize;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TreeView LogTree;
		private System.Windows.Forms.Button Button_FolderRefresh;
	}
}
