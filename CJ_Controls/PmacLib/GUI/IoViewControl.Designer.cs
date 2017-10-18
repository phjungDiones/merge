namespace CJ_Controls.PmacLib
{
	partial class IoViewControl
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbl_IoList = new CJ_Controls.Windows.Win_QuickTableLayoutPanel();
			this.ListView_InputList = new CJ_Controls.Windows.Win_ListView();
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ListView_OutputList = new CJ_Controls.Windows.Win_ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tbl_IoList.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BackColor = System.Drawing.Color.Gainsboro;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(471, 1);
			this.label1.Margin = new System.Windows.Forms.Padding(1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(469, 33);
			this.label1.TabIndex = 441;
			this.label1.Text = "Output";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.BackColor = System.Drawing.Color.Gainsboro;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(1, 1);
			this.label2.Margin = new System.Windows.Forms.Padding(1);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(468, 33);
			this.label2.TabIndex = 440;
			this.label2.Text = "Input";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbl_IoList
			// 
			this.tbl_IoList.ColumnCount = 2;
			this.tbl_IoList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tbl_IoList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tbl_IoList.Controls.Add(this.ListView_InputList, 0, 1);
			this.tbl_IoList.Controls.Add(this.ListView_OutputList, 0, 1);
			this.tbl_IoList.Controls.Add(this.label2, 0, 0);
			this.tbl_IoList.Controls.Add(this.label1, 1, 0);
			this.tbl_IoList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbl_IoList.Location = new System.Drawing.Point(0, 0);
			this.tbl_IoList.Name = "tbl_IoList";
			this.tbl_IoList.RowCount = 2;
			this.tbl_IoList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tbl_IoList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tbl_IoList.Size = new System.Drawing.Size(941, 583);
			this.tbl_IoList.TabIndex = 442;
			// 
			// ListView_InputList
			// 
			this.ListView_InputList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
			this.ListView_InputList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListView_InputList.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.ListView_InputList.FullRowSelect = true;
			this.ListView_InputList.GridLines = true;
			this.ListView_InputList.Location = new System.Drawing.Point(3, 38);
			this.ListView_InputList.MultiSelect = false;
			this.ListView_InputList.Name = "ListView_InputList";
			this.ListView_InputList.OwnerDraw = true;
			this.ListView_InputList.Size = new System.Drawing.Size(464, 542);
			this.ListView_InputList.TabIndex = 443;
			this.ListView_InputList.UseCompatibleStateImageBehavior = false;
			this.ListView_InputList.View = System.Windows.Forms.View.Details;
			this.ListView_InputList.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView_DrawColumnHeader);
			this.ListView_InputList.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_DrawSubItem);
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Num";
			this.columnHeader7.Width = 40;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Addr";
			this.columnHeader8.Width = 50;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "SubAddr";
			this.columnHeader9.Width = 100;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Cable";
			this.columnHeader10.Width = 70;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "IO Name";
			this.columnHeader11.Width = 260;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Val";
			// 
			// ListView_OutputList
			// 
			this.ListView_OutputList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			this.ListView_OutputList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListView_OutputList.Font = new System.Drawing.Font("돋움체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.ListView_OutputList.FullRowSelect = true;
			this.ListView_OutputList.GridLines = true;
			this.ListView_OutputList.Location = new System.Drawing.Point(473, 38);
			this.ListView_OutputList.MultiSelect = false;
			this.ListView_OutputList.Name = "ListView_OutputList";
			this.ListView_OutputList.OwnerDraw = true;
			this.ListView_OutputList.Size = new System.Drawing.Size(465, 542);
			this.ListView_OutputList.TabIndex = 442;
			this.ListView_OutputList.UseCompatibleStateImageBehavior = false;
			this.ListView_OutputList.View = System.Windows.Forms.View.Details;
			this.ListView_OutputList.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView_DrawColumnHeader);
			this.ListView_OutputList.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListView_DrawSubItem);
			this.ListView_OutputList.DoubleClick += new System.EventHandler(this.ListView_OutputList_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Num";
			this.columnHeader1.Width = 40;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Addr";
			this.columnHeader2.Width = 50;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "SubAddr";
			this.columnHeader3.Width = 100;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Cable";
			this.columnHeader4.Width = 70;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "IO Name";
			this.columnHeader5.Width = 260;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Val";
			// 
			// IoViewControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tbl_IoList);
			this.Name = "IoViewControl";
			this.Size = new System.Drawing.Size(941, 583);
			this.Load += new System.EventHandler(this.IoViewControl_Load);
			this.tbl_IoList.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private CJ_Controls.Windows.Win_QuickTableLayoutPanel tbl_IoList;
		private CJ_Controls.Windows.Win_ListView ListView_OutputList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private CJ_Controls.Windows.Win_ListView ListView_InputList;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader6;
	}
}
