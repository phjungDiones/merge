namespace CJ_Controls.PmacLib
{
	partial class Form_Engr_Io_List
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Tab_IoList = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.ListView_InputList = new CJ_Controls.Windows.Win_ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel1 = new System.Windows.Forms.Panel();
			this.Text_Input_IoName = new System.Windows.Forms.TextBox();
			this.Text_Input_Cable = new System.Windows.Forms.TextBox();
			this.Text_Input_Type = new System.Windows.Forms.TextBox();
			this.Text_Input_SubAddr = new System.Windows.Forms.TextBox();
			this.Text_Input_Addr = new System.Windows.Forms.TextBox();
			this.Btn_Input_Apply = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.ListView_OutputList = new CJ_Controls.Windows.Win_ListView();
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Btn_ServoData_Reload = new System.Windows.Forms.Button();
			this.Btn_ServoData_Save = new System.Windows.Forms.Button();
			this.Btn_Close = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new CJ_Controls.Windows.Win_QuickTableLayoutPanel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.Text_Output_IoName = new System.Windows.Forms.TextBox();
			this.Text_Output_Cable = new System.Windows.Forms.TextBox();
			this.Text_Output_Type = new System.Windows.Forms.TextBox();
			this.Text_Output_SubAddr = new System.Windows.Forms.TextBox();
			this.Text_Output_Addr = new System.Windows.Forms.TextBox();
			this.Btn_Output_Apply = new System.Windows.Forms.Button();
			this.Tab_IoList.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// Tab_IoList
			// 
			this.Tab_IoList.Controls.Add(this.tabPage1);
			this.Tab_IoList.Controls.Add(this.tabPage2);
			this.Tab_IoList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tab_IoList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Tab_IoList.ItemSize = new System.Drawing.Size(78, 30);
			this.Tab_IoList.Location = new System.Drawing.Point(0, 0);
			this.Tab_IoList.Name = "Tab_IoList";
			this.Tab_IoList.SelectedIndex = 0;
			this.Tab_IoList.Size = new System.Drawing.Size(563, 431);
			this.Tab_IoList.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.ListView_InputList);
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 34);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(555, 393);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "INPUT LIST";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// ListView_InputList
			// 
			this.ListView_InputList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			this.ListView_InputList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListView_InputList.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ListView_InputList.FullRowSelect = true;
			this.ListView_InputList.GridLines = true;
			this.ListView_InputList.Location = new System.Drawing.Point(3, 3);
			this.ListView_InputList.MultiSelect = false;
			this.ListView_InputList.Name = "ListView_InputList";
			this.ListView_InputList.Size = new System.Drawing.Size(549, 310);
			this.ListView_InputList.TabIndex = 429;
			this.ListView_InputList.UseCompatibleStateImageBehavior = false;
			this.ListView_InputList.View = System.Windows.Forms.View.Details;
			this.ListView_InputList.SelectedIndexChanged += new System.EventHandler(this.ListView_InputList_SelectedIndexChanged);
			this.ListView_InputList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_InputList_KeyDown);
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
			this.columnHeader3.Width = 70;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Type";
			this.columnHeader4.Width = 50;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Cable";
			this.columnHeader5.Width = 70;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "IO Name";
			this.columnHeader6.Width = 245;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.Text_Input_IoName);
			this.panel1.Controls.Add(this.Text_Input_Cable);
			this.panel1.Controls.Add(this.Text_Input_Type);
			this.panel1.Controls.Add(this.Text_Input_SubAddr);
			this.panel1.Controls.Add(this.Text_Input_Addr);
			this.panel1.Controls.Add(this.Btn_Input_Apply);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(3, 313);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(549, 77);
			this.panel1.TabIndex = 430;
			// 
			// Text_Input_IoName
			// 
			this.Text_Input_IoName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Input_IoName.Location = new System.Drawing.Point(8, 41);
			this.Text_Input_IoName.Name = "Text_Input_IoName";
			this.Text_Input_IoName.Size = new System.Drawing.Size(418, 27);
			this.Text_Input_IoName.TabIndex = 466;
			// 
			// Text_Input_Cable
			// 
			this.Text_Input_Cable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Input_Cable.Location = new System.Drawing.Point(326, 8);
			this.Text_Input_Cable.Name = "Text_Input_Cable";
			this.Text_Input_Cable.Size = new System.Drawing.Size(100, 27);
			this.Text_Input_Cable.TabIndex = 466;
			// 
			// Text_Input_Type
			// 
			this.Text_Input_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Input_Type.Location = new System.Drawing.Point(220, 8);
			this.Text_Input_Type.Name = "Text_Input_Type";
			this.Text_Input_Type.Size = new System.Drawing.Size(100, 27);
			this.Text_Input_Type.TabIndex = 466;
			// 
			// Text_Input_SubAddr
			// 
			this.Text_Input_SubAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Input_SubAddr.Location = new System.Drawing.Point(114, 8);
			this.Text_Input_SubAddr.Name = "Text_Input_SubAddr";
			this.Text_Input_SubAddr.Size = new System.Drawing.Size(100, 27);
			this.Text_Input_SubAddr.TabIndex = 466;
			// 
			// Text_Input_Addr
			// 
			this.Text_Input_Addr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Input_Addr.Location = new System.Drawing.Point(8, 8);
			this.Text_Input_Addr.Name = "Text_Input_Addr";
			this.Text_Input_Addr.Size = new System.Drawing.Size(100, 27);
			this.Text_Input_Addr.TabIndex = 466;
			// 
			// Btn_Input_Apply
			// 
			this.Btn_Input_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Input_Apply.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Btn_Input_Apply.ForeColor = System.Drawing.Color.Black;
			this.Btn_Input_Apply.Location = new System.Drawing.Point(432, 8);
			this.Btn_Input_Apply.Name = "Btn_Input_Apply";
			this.Btn_Input_Apply.Size = new System.Drawing.Size(112, 60);
			this.Btn_Input_Apply.TabIndex = 465;
			this.Btn_Input_Apply.Text = "Apply";
			this.Btn_Input_Apply.UseVisualStyleBackColor = true;
			this.Btn_Input_Apply.Click += new System.EventHandler(this.Btn_Input_Apply_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.ListView_OutputList);
			this.tabPage2.Controls.Add(this.panel2);
			this.tabPage2.Location = new System.Drawing.Point(4, 34);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(555, 393);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "OUTPUT LIST";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// ListView_OutputList
			// 
			this.ListView_OutputList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
			this.ListView_OutputList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListView_OutputList.Font = new System.Drawing.Font("굴림", 9F);
			this.ListView_OutputList.FullRowSelect = true;
			this.ListView_OutputList.GridLines = true;
			this.ListView_OutputList.Location = new System.Drawing.Point(3, 3);
			this.ListView_OutputList.MultiSelect = false;
			this.ListView_OutputList.Name = "ListView_OutputList";
			this.ListView_OutputList.Size = new System.Drawing.Size(549, 310);
			this.ListView_OutputList.TabIndex = 430;
			this.ListView_OutputList.UseCompatibleStateImageBehavior = false;
			this.ListView_OutputList.View = System.Windows.Forms.View.Details;
			this.ListView_OutputList.SelectedIndexChanged += new System.EventHandler(this.ListView_OutputList_SelectedIndexChanged);
			this.ListView_OutputList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_OutputList_KeyDown);
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
			this.columnHeader9.Width = 70;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Type";
			this.columnHeader10.Width = 50;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Cable";
			this.columnHeader11.Width = 70;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "IO Name";
			this.columnHeader12.Width = 245;
			// 
			// Btn_ServoData_Reload
			// 
			this.Btn_ServoData_Reload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_ServoData_Reload.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
			this.Btn_ServoData_Reload.ForeColor = System.Drawing.Color.Black;
			this.Btn_ServoData_Reload.Location = new System.Drawing.Point(190, 3);
			this.Btn_ServoData_Reload.Name = "Btn_ServoData_Reload";
			this.Btn_ServoData_Reload.Size = new System.Drawing.Size(181, 55);
			this.Btn_ServoData_Reload.TabIndex = 466;
			this.Btn_ServoData_Reload.Text = "RELOAD FILE";
			this.Btn_ServoData_Reload.UseVisualStyleBackColor = true;
			this.Btn_ServoData_Reload.Click += new System.EventHandler(this.Btn_ServoData_Reload_Click);
			// 
			// Btn_ServoData_Save
			// 
			this.Btn_ServoData_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_ServoData_Save.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
			this.Btn_ServoData_Save.ForeColor = System.Drawing.Color.Black;
			this.Btn_ServoData_Save.Location = new System.Drawing.Point(3, 3);
			this.Btn_ServoData_Save.Name = "Btn_ServoData_Save";
			this.Btn_ServoData_Save.Size = new System.Drawing.Size(181, 55);
			this.Btn_ServoData_Save.TabIndex = 465;
			this.Btn_ServoData_Save.Text = "SAVE FILE";
			this.Btn_ServoData_Save.UseVisualStyleBackColor = true;
			this.Btn_ServoData_Save.Click += new System.EventHandler(this.Btn_ServoData_Save_Click);
			// 
			// Btn_Close
			// 
			this.Btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Close.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
			this.Btn_Close.Location = new System.Drawing.Point(377, 3);
			this.Btn_Close.Name = "Btn_Close";
			this.Btn_Close.Size = new System.Drawing.Size(183, 55);
			this.Btn_Close.TabIndex = 464;
			this.Btn_Close.Text = "CLOSE";
			this.Btn_Close.UseVisualStyleBackColor = true;
			this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.Controls.Add(this.Btn_ServoData_Save, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.Btn_Close, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.Btn_ServoData_Reload, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 431);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(563, 61);
			this.tableLayoutPanel1.TabIndex = 467;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.Text_Output_IoName);
			this.panel2.Controls.Add(this.Text_Output_Cable);
			this.panel2.Controls.Add(this.Text_Output_Type);
			this.panel2.Controls.Add(this.Text_Output_SubAddr);
			this.panel2.Controls.Add(this.Text_Output_Addr);
			this.panel2.Controls.Add(this.Btn_Output_Apply);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(3, 313);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(549, 77);
			this.panel2.TabIndex = 431;
			// 
			// Text_Output_IoName
			// 
			this.Text_Output_IoName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Output_IoName.Location = new System.Drawing.Point(8, 41);
			this.Text_Output_IoName.Name = "Text_Output_IoName";
			this.Text_Output_IoName.Size = new System.Drawing.Size(418, 27);
			this.Text_Output_IoName.TabIndex = 466;
			// 
			// Text_Output_Cable
			// 
			this.Text_Output_Cable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Output_Cable.Location = new System.Drawing.Point(326, 8);
			this.Text_Output_Cable.Name = "Text_Output_Cable";
			this.Text_Output_Cable.Size = new System.Drawing.Size(100, 27);
			this.Text_Output_Cable.TabIndex = 466;
			// 
			// Text_Output_Type
			// 
			this.Text_Output_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Output_Type.Location = new System.Drawing.Point(220, 8);
			this.Text_Output_Type.Name = "Text_Output_Type";
			this.Text_Output_Type.Size = new System.Drawing.Size(100, 27);
			this.Text_Output_Type.TabIndex = 466;
			// 
			// Text_Output_SubAddr
			// 
			this.Text_Output_SubAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Output_SubAddr.Location = new System.Drawing.Point(114, 8);
			this.Text_Output_SubAddr.Name = "Text_Output_SubAddr";
			this.Text_Output_SubAddr.Size = new System.Drawing.Size(100, 27);
			this.Text_Output_SubAddr.TabIndex = 466;
			// 
			// Text_Output_Addr
			// 
			this.Text_Output_Addr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Output_Addr.Location = new System.Drawing.Point(8, 8);
			this.Text_Output_Addr.Name = "Text_Output_Addr";
			this.Text_Output_Addr.Size = new System.Drawing.Size(100, 27);
			this.Text_Output_Addr.TabIndex = 466;
			// 
			// Btn_Output_Apply
			// 
			this.Btn_Output_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Output_Apply.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Btn_Output_Apply.ForeColor = System.Drawing.Color.Black;
			this.Btn_Output_Apply.Location = new System.Drawing.Point(432, 8);
			this.Btn_Output_Apply.Name = "Btn_Output_Apply";
			this.Btn_Output_Apply.Size = new System.Drawing.Size(112, 60);
			this.Btn_Output_Apply.TabIndex = 465;
			this.Btn_Output_Apply.Text = "Apply";
			this.Btn_Output_Apply.UseVisualStyleBackColor = true;
			this.Btn_Output_Apply.Click += new System.EventHandler(this.Btn_Output_Apply_Click);
			// 
			// Form_Engr_Io_List
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 492);
			this.ControlBox = false;
			this.Controls.Add(this.Tab_IoList);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form_Engr_Io_List";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form_Engr_Io_List";
			this.Load += new System.EventHandler(this.Form_Engr_Io_List_Load);
			this.Tab_IoList.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl Tab_IoList;
		private System.Windows.Forms.TabPage tabPage1;
		private CJ_Controls.Windows.Win_ListView ListView_InputList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.TabPage tabPage2;
		private CJ_Controls.Windows.Win_ListView ListView_OutputList;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.Button Btn_ServoData_Reload;
		private System.Windows.Forms.Button Btn_ServoData_Save;
		private System.Windows.Forms.Button Btn_Close;
		private CJ_Controls.Windows.Win_QuickTableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox Text_Input_IoName;
		private System.Windows.Forms.TextBox Text_Input_Cable;
		private System.Windows.Forms.TextBox Text_Input_Type;
		private System.Windows.Forms.TextBox Text_Input_SubAddr;
		private System.Windows.Forms.TextBox Text_Input_Addr;
		private System.Windows.Forms.Button Btn_Input_Apply;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TextBox Text_Output_IoName;
		private System.Windows.Forms.TextBox Text_Output_Cable;
		private System.Windows.Forms.TextBox Text_Output_Type;
		private System.Windows.Forms.TextBox Text_Output_SubAddr;
		private System.Windows.Forms.TextBox Text_Output_Addr;
		private System.Windows.Forms.Button Btn_Output_Apply;

	}
}