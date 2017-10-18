namespace CJ_Controls.Log_Trace
{
	partial class Ctrl_Log_Trace
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
			this.button_Clear = new System.Windows.Forms.Button();
			this.panel_Caption = new System.Windows.Forms.Panel();
			this.label_Caption = new System.Windows.Forms.Label();
			this.panel_Log = new System.Windows.Forms.Panel();
			this.listView_Msg = new CJ_Controls.Windows.Win_ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel_Status = new System.Windows.Forms.Panel();
			this.panel_Caption.SuspendLayout();
			this.panel_Log.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_Clear
			// 
			this.button_Clear.Dock = System.Windows.Forms.DockStyle.Right;
			this.button_Clear.Location = new System.Drawing.Point(371, 0);
			this.button_Clear.Name = "button_Clear";
			this.button_Clear.Size = new System.Drawing.Size(76, 25);
			this.button_Clear.TabIndex = 9;
			this.button_Clear.Text = "Clear";
			this.button_Clear.UseVisualStyleBackColor = true;
			this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
			// 
			// panel_Caption
			// 
			this.panel_Caption.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panel_Caption.Controls.Add(this.label_Caption);
			this.panel_Caption.Controls.Add(this.button_Clear);
			this.panel_Caption.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel_Caption.Location = new System.Drawing.Point(0, 0);
			this.panel_Caption.Name = "panel_Caption";
			this.panel_Caption.Size = new System.Drawing.Size(447, 25);
			this.panel_Caption.TabIndex = 6;
			// 
			// label_Caption
			// 
			this.label_Caption.BackColor = System.Drawing.Color.Gainsboro;
			this.label_Caption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label_Caption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label_Caption.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label_Caption.ForeColor = System.Drawing.Color.Black;
			this.label_Caption.Location = new System.Drawing.Point(0, 0);
			this.label_Caption.Margin = new System.Windows.Forms.Padding(0);
			this.label_Caption.Name = "label_Caption";
			this.label_Caption.Size = new System.Drawing.Size(371, 25);
			this.label_Caption.TabIndex = 4;
			this.label_Caption.Text = "메세지 뷰어";
			this.label_Caption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel_Log
			// 
			this.panel_Log.Controls.Add(this.listView_Msg);
			this.panel_Log.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_Log.Location = new System.Drawing.Point(0, 25);
			this.panel_Log.Name = "panel_Log";
			this.panel_Log.Size = new System.Drawing.Size(447, 181);
			this.panel_Log.TabIndex = 7;
			// 
			// listView_Msg
			// 
			this.listView_Msg.BackColor = System.Drawing.Color.White;
			this.listView_Msg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.listView_Msg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView_Msg.ForeColor = System.Drawing.Color.White;
			this.listView_Msg.FullRowSelect = true;
			this.listView_Msg.GridLines = true;
			this.listView_Msg.Location = new System.Drawing.Point(0, 0);
			this.listView_Msg.MultiSelect = false;
			this.listView_Msg.Name = "listView_Msg";
			this.listView_Msg.Size = new System.Drawing.Size(447, 181);
			this.listView_Msg.TabIndex = 1;
			this.listView_Msg.UseCompatibleStateImageBehavior = false;
			this.listView_Msg.View = System.Windows.Forms.View.Details;
			this.listView_Msg.DoubleClick += new System.EventHandler(this.listView_Msg_DoubleClick);
			this.listView_Msg.Resize += new System.EventHandler(this.listView_Msg_Resize);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 0;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "기록 시간";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader2.Width = 150;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "정보";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 80;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "메 세 지";
			this.columnHeader4.Width = 193;
			// 
			// panel_Status
			// 
			this.panel_Status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel_Status.Location = new System.Drawing.Point(0, 206);
			this.panel_Status.Name = "panel_Status";
			this.panel_Status.Size = new System.Drawing.Size(447, 21);
			this.panel_Status.TabIndex = 4;
			// 
			// Ctrl_Log_Trace
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel_Log);
			this.Controls.Add(this.panel_Caption);
			this.Controls.Add(this.panel_Status);
			this.Name = "Ctrl_Log_Trace";
			this.Size = new System.Drawing.Size(447, 227);
			this.Load += new System.EventHandler(this.LogControl_Load);
			this.panel_Caption.ResumeLayout(false);
			this.panel_Log.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel panel_Caption;
        public System.Windows.Forms.Label label_Caption;
        private System.Windows.Forms.Panel panel_Log;
		public CJ_Controls.Windows.Win_ListView listView_Msg;
        private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button button_Clear;
		private System.Windows.Forms.Panel panel_Status;

    }
}
