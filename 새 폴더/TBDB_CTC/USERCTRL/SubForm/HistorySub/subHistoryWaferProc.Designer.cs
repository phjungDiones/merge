namespace TBDB_CTC.UserCtrl.SubForm.HistorySub
{
    partial class subHistoryWaferProc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdDataView = new System.Windows.Forms.DataGridView();
            this.gridLog_subHistoryWaferProc = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Log = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog_subHistoryWaferProc)).BeginInit();
            this.SuspendLayout();
            // 
            // grdDataView
            // 
            this.grdDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDataView.Location = new System.Drawing.Point(0, 0);
            this.grdDataView.Margin = new System.Windows.Forms.Padding(4);
            this.grdDataView.Name = "grdDataView";
            this.grdDataView.Size = new System.Drawing.Size(1448, 931);
            this.grdDataView.TabIndex = 30;
            // 
            // gridLog_subHistoryWaferProc
            // 
            this.gridLog_subHistoryWaferProc.AllowUserToAddRows = false;
            this.gridLog_subHistoryWaferProc.AllowUserToDeleteRows = false;
            this.gridLog_subHistoryWaferProc.AllowUserToResizeColumns = false;
            this.gridLog_subHistoryWaferProc.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLog_subHistoryWaferProc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridLog_subHistoryWaferProc.BackgroundColor = System.Drawing.Color.Gray;
            this.gridLog_subHistoryWaferProc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.gridLog_subHistoryWaferProc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridLog_subHistoryWaferProc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLog_subHistoryWaferProc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Time,
            this.dataGridViewTextBoxColumn3,
            this.Login,
            this.UserID,
            this.Log});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridLog_subHistoryWaferProc.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridLog_subHistoryWaferProc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLog_subHistoryWaferProc.GridColor = System.Drawing.Color.Silver;
            this.gridLog_subHistoryWaferProc.Location = new System.Drawing.Point(0, 0);
            this.gridLog_subHistoryWaferProc.Margin = new System.Windows.Forms.Padding(4);
            this.gridLog_subHistoryWaferProc.MultiSelect = false;
            this.gridLog_subHistoryWaferProc.Name = "gridLog_subHistoryWaferProc";
            this.gridLog_subHistoryWaferProc.ReadOnly = true;
            this.gridLog_subHistoryWaferProc.RowHeadersVisible = false;
            this.gridLog_subHistoryWaferProc.RowHeadersWidth = 25;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.gridLog_subHistoryWaferProc.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridLog_subHistoryWaferProc.RowTemplate.Height = 35;
            this.gridLog_subHistoryWaferProc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLog_subHistoryWaferProc.ShowCellErrors = false;
            this.gridLog_subHistoryWaferProc.ShowEditingIcon = false;
            this.gridLog_subHistoryWaferProc.ShowRowErrors = false;
            this.gridLog_subHistoryWaferProc.Size = new System.Drawing.Size(1448, 931);
            this.gridLog_subHistoryWaferProc.StandardTab = true;
            this.gridLog_subHistoryWaferProc.TabIndex = 31;
            // 
            // No
            // 
            this.No.DataPropertyName = "No";
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.No.Width = 40;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Time.DefaultCellStyle = dataGridViewCellStyle3;
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Time.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Recipe";
            this.dataGridViewTextBoxColumn3.HeaderText = "Recipe";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // Login
            // 
            this.Login.DataPropertyName = "Login";
            this.Login.HeaderText = "Login";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            this.Login.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Login.Width = 120;
            // 
            // UserID
            // 
            this.UserID.DataPropertyName = "UserID";
            this.UserID.HeaderText = "UserID";
            this.UserID.Name = "UserID";
            this.UserID.ReadOnly = true;
            // 
            // Log
            // 
            this.Log.DataPropertyName = "Log";
            this.Log.HeaderText = "Log";
            this.Log.Name = "Log";
            this.Log.ReadOnly = true;
            this.Log.Width = 670;
            // 
            // subHistoryWaferProc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.gridLog_subHistoryWaferProc);
            this.Controls.Add(this.grdDataView);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "subHistoryWaferProc";
            this.Size = new System.Drawing.Size(1448, 931);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog_subHistoryWaferProc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdDataView;
        internal System.Windows.Forms.DataGridView gridLog_subHistoryWaferProc;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Log;
    }
}
