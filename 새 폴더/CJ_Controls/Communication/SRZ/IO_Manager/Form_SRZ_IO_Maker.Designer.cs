namespace CJ_Controls.Communication.SRZ
{
	partial class Form_SRZ_IO_Maker
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
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.Btn_CpuAdd = new CJ_Controls.Windows.Win_GlassButton();
			this.Btn_CpuDel = new CJ_Controls.Windows.Win_GlassButton();
			this.ListBox_CPU = new System.Windows.Forms.ListBox();
			this.label56 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.Btn_ModuleAdd = new CJ_Controls.Windows.Win_GlassButton();
			this.Btn_ModuleDel = new CJ_Controls.Windows.Win_GlassButton();
			this.ComboBox_ModuleType = new System.Windows.Forms.ComboBox();
			this.ListBox_Module = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Label_IO_List = new System.Windows.Forms.Label();
			this.Label_Io_Num = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.Label_Io_Type = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtBox_IoName = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.Btn_IO_List_Change = new CJ_Controls.Windows.Win_GlassButton();
			this.ListView_IO_List = new CJ_Controls.Windows.Win_ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Btn_DataSave = new CJ_Controls.Windows.Win_GlassButton();
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.txtBox_Value = new System.Windows.Forms.TextBox();
			this.Timer_Refresh = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Silver;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.Btn_CpuAdd);
			this.panel1.Controls.Add(this.Btn_CpuDel);
			this.panel1.Location = new System.Drawing.Point(12, 274);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(114, 74);
			this.panel1.TabIndex = 444;
			// 
			// Btn_CpuAdd
			// 
			this.Btn_CpuAdd.BackColor = System.Drawing.Color.PaleTurquoise;
			this.Btn_CpuAdd.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.Btn_CpuAdd.ForeColor = System.Drawing.Color.Black;
			this.Btn_CpuAdd.GlowColor = System.Drawing.Color.White;
			this.Btn_CpuAdd.Location = new System.Drawing.Point(9, 4);
			this.Btn_CpuAdd.Name = "Btn_CpuAdd";
			this.Btn_CpuAdd.OuterBorderColor = System.Drawing.Color.Transparent;
			this.Btn_CpuAdd.Size = new System.Drawing.Size(96, 31);
			this.Btn_CpuAdd.TabIndex = 429;
			this.Btn_CpuAdd.Text = "추가";
			this.Btn_CpuAdd.Click += new System.EventHandler(this.Btn_CpuAdd_Click);
			// 
			// Btn_CpuDel
			// 
			this.Btn_CpuDel.BackColor = System.Drawing.Color.PaleTurquoise;
			this.Btn_CpuDel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.Btn_CpuDel.ForeColor = System.Drawing.Color.Black;
			this.Btn_CpuDel.GlowColor = System.Drawing.Color.White;
			this.Btn_CpuDel.Location = new System.Drawing.Point(9, 37);
			this.Btn_CpuDel.Name = "Btn_CpuDel";
			this.Btn_CpuDel.OuterBorderColor = System.Drawing.Color.Transparent;
			this.Btn_CpuDel.Size = new System.Drawing.Size(96, 31);
			this.Btn_CpuDel.TabIndex = 431;
			this.Btn_CpuDel.Text = "삭제";
			this.Btn_CpuDel.Click += new System.EventHandler(this.Btn_CpuDel_Click);
			// 
			// ListBox_CPU
			// 
			this.ListBox_CPU.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
			this.ListBox_CPU.FormattingEnabled = true;
			this.ListBox_CPU.ItemHeight = 23;
			this.ListBox_CPU.Location = new System.Drawing.Point(12, 41);
			this.ListBox_CPU.Name = "ListBox_CPU";
			this.ListBox_CPU.Size = new System.Drawing.Size(114, 234);
			this.ListBox_CPU.TabIndex = 443;
			this.ListBox_CPU.SelectedIndexChanged += new System.EventHandler(this.ListBox_CPU_SelectedIndexChanged);
			// 
			// label56
			// 
			this.label56.BackColor = System.Drawing.Color.Silver;
			this.label56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label56.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.label56.Location = new System.Drawing.Point(12, 9);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(114, 29);
			this.label56.TabIndex = 442;
			this.label56.Text = "CPU";
			this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Silver;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.Btn_ModuleAdd);
			this.panel2.Controls.Add(this.Btn_ModuleDel);
			this.panel2.Controls.Add(this.ComboBox_ModuleType);
			this.panel2.Location = new System.Drawing.Point(132, 251);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(189, 97);
			this.panel2.TabIndex = 447;
			// 
			// Btn_ModuleAdd
			// 
			this.Btn_ModuleAdd.BackColor = System.Drawing.Color.PaleTurquoise;
			this.Btn_ModuleAdd.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.Btn_ModuleAdd.ForeColor = System.Drawing.Color.Black;
			this.Btn_ModuleAdd.GlowColor = System.Drawing.Color.White;
			this.Btn_ModuleAdd.Location = new System.Drawing.Point(3, 27);
			this.Btn_ModuleAdd.Name = "Btn_ModuleAdd";
			this.Btn_ModuleAdd.OuterBorderColor = System.Drawing.Color.Transparent;
			this.Btn_ModuleAdd.Size = new System.Drawing.Size(181, 31);
			this.Btn_ModuleAdd.TabIndex = 432;
			this.Btn_ModuleAdd.Text = "추가";
			this.Btn_ModuleAdd.Click += new System.EventHandler(this.Btn_ModuleAdd_Click);
			// 
			// Btn_ModuleDel
			// 
			this.Btn_ModuleDel.BackColor = System.Drawing.Color.PaleTurquoise;
			this.Btn_ModuleDel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.Btn_ModuleDel.ForeColor = System.Drawing.Color.Black;
			this.Btn_ModuleDel.GlowColor = System.Drawing.Color.White;
			this.Btn_ModuleDel.Location = new System.Drawing.Point(3, 60);
			this.Btn_ModuleDel.Name = "Btn_ModuleDel";
			this.Btn_ModuleDel.OuterBorderColor = System.Drawing.Color.Transparent;
			this.Btn_ModuleDel.Size = new System.Drawing.Size(181, 31);
			this.Btn_ModuleDel.TabIndex = 433;
			this.Btn_ModuleDel.Text = "삭제";
			this.Btn_ModuleDel.Click += new System.EventHandler(this.Btn_ModuleDel_Click);
			// 
			// ComboBox_ModuleType
			// 
			this.ComboBox_ModuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboBox_ModuleType.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.ComboBox_ModuleType.FormattingEnabled = true;
			this.ComboBox_ModuleType.Items.AddRange(new object[] {
            "TIO_8888",
            "TIO_VVVV",
            "DIO"});
			this.ComboBox_ModuleType.Location = new System.Drawing.Point(3, 4);
			this.ComboBox_ModuleType.Name = "ComboBox_ModuleType";
			this.ComboBox_ModuleType.Size = new System.Drawing.Size(181, 20);
			this.ComboBox_ModuleType.TabIndex = 434;
			// 
			// ListBox_Module
			// 
			this.ListBox_Module.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
			this.ListBox_Module.FormattingEnabled = true;
			this.ListBox_Module.ItemHeight = 21;
			this.ListBox_Module.Location = new System.Drawing.Point(132, 41);
			this.ListBox_Module.Name = "ListBox_Module";
			this.ListBox_Module.Size = new System.Drawing.Size(189, 214);
			this.ListBox_Module.TabIndex = 446;
			this.ListBox_Module.SelectedIndexChanged += new System.EventHandler(this.ListBox_Module_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Silver;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(132, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(189, 29);
			this.label1.TabIndex = 445;
			this.label1.Text = "Module";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_IO_List
			// 
			this.Label_IO_List.BackColor = System.Drawing.Color.Silver;
			this.Label_IO_List.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_IO_List.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.Label_IO_List.Location = new System.Drawing.Point(327, 9);
			this.Label_IO_List.Name = "Label_IO_List";
			this.Label_IO_List.Size = new System.Drawing.Size(476, 29);
			this.Label_IO_List.TabIndex = 450;
			this.Label_IO_List.Text = "IO List";
			this.Label_IO_List.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_Io_Num
			// 
			this.Label_Io_Num.BackColor = System.Drawing.Color.Gainsboro;
			this.Label_Io_Num.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Io_Num.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Label_Io_Num.ForeColor = System.Drawing.Color.Blue;
			this.Label_Io_Num.Location = new System.Drawing.Point(385, 354);
			this.Label_Io_Num.Name = "Label_Io_Num";
			this.Label_Io_Num.Size = new System.Drawing.Size(92, 22);
			this.Label_Io_Num.TabIndex = 452;
			this.Label_Io_Num.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Silver;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label3.Location = new System.Drawing.Point(327, 354);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58, 22);
			this.label3.TabIndex = 451;
			this.label3.Text = "Num";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_Io_Type
			// 
			this.Label_Io_Type.BackColor = System.Drawing.Color.Gainsboro;
			this.Label_Io_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Io_Type.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Label_Io_Type.ForeColor = System.Drawing.Color.Blue;
			this.Label_Io_Type.Location = new System.Drawing.Point(540, 354);
			this.Label_Io_Type.Name = "Label_Io_Type";
			this.Label_Io_Type.Size = new System.Drawing.Size(187, 22);
			this.Label_Io_Type.TabIndex = 454;
			this.Label_Io_Type.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Silver;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label4.Location = new System.Drawing.Point(482, 354);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(58, 22);
			this.label4.TabIndex = 453;
			this.label4.Text = "Type";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtBox_IoName
			// 
			this.txtBox_IoName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtBox_IoName.Font = new System.Drawing.Font("굴림", 9.5F, System.Drawing.FontStyle.Bold);
			this.txtBox_IoName.Location = new System.Drawing.Point(431, 379);
			this.txtBox_IoName.Name = "txtBox_IoName";
			this.txtBox_IoName.Size = new System.Drawing.Size(222, 22);
			this.txtBox_IoName.TabIndex = 456;
			// 
			// label11
			// 
			this.label11.BackColor = System.Drawing.Color.Silver;
			this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label11.Location = new System.Drawing.Point(327, 379);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(104, 22);
			this.label11.TabIndex = 455;
			this.label11.Text = "IO Name";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Btn_IO_List_Change
			// 
			this.Btn_IO_List_Change.BackColor = System.Drawing.Color.PaleTurquoise;
			this.Btn_IO_List_Change.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.Btn_IO_List_Change.ForeColor = System.Drawing.Color.Black;
			this.Btn_IO_List_Change.GlowColor = System.Drawing.Color.White;
			this.Btn_IO_List_Change.Location = new System.Drawing.Point(733, 354);
			this.Btn_IO_List_Change.Name = "Btn_IO_List_Change";
			this.Btn_IO_List_Change.OuterBorderColor = System.Drawing.Color.Transparent;
			this.Btn_IO_List_Change.Size = new System.Drawing.Size(70, 47);
			this.Btn_IO_List_Change.TabIndex = 457;
			this.Btn_IO_List_Change.Text = "변경";
			this.Btn_IO_List_Change.Click += new System.EventHandler(this.Btn_IO_List_Change_Click);
			// 
			// ListView_IO_List
			// 
			this.ListView_IO_List.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.ListView_IO_List.FullRowSelect = true;
			this.ListView_IO_List.GridLines = true;
			this.ListView_IO_List.Location = new System.Drawing.Point(327, 41);
			this.ListView_IO_List.MultiSelect = false;
			this.ListView_IO_List.Name = "ListView_IO_List";
			this.ListView_IO_List.Size = new System.Drawing.Size(476, 307);
			this.ListView_IO_List.TabIndex = 449;
			this.ListView_IO_List.UseCompatibleStateImageBehavior = false;
			this.ListView_IO_List.View = System.Windows.Forms.View.Details;
			this.ListView_IO_List.SelectedIndexChanged += new System.EventHandler(this.ListView_IO_List_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Num";
			this.columnHeader1.Width = 50;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Type";
			this.columnHeader2.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "IO Name";
			this.columnHeader3.Width = 250;
			// 
			// Btn_DataSave
			// 
			this.Btn_DataSave.BackColor = System.Drawing.Color.Blue;
			this.Btn_DataSave.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.Btn_DataSave.ForeColor = System.Drawing.Color.Black;
			this.Btn_DataSave.GlowColor = System.Drawing.Color.White;
			this.Btn_DataSave.Location = new System.Drawing.Point(16, 354);
			this.Btn_DataSave.Name = "Btn_DataSave";
			this.Btn_DataSave.OuterBorderColor = System.Drawing.Color.Transparent;
			this.Btn_DataSave.Size = new System.Drawing.Size(305, 47);
			this.Btn_DataSave.TabIndex = 448;
			this.Btn_DataSave.Text = "데이저 파일 저장";
			this.Btn_DataSave.Click += new System.EventHandler(this.Btn_DataSave_Click);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Val";
			this.columnHeader4.Width = 50;
			// 
			// txtBox_Value
			// 
			this.txtBox_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtBox_Value.Font = new System.Drawing.Font("굴림", 9.5F, System.Drawing.FontStyle.Bold);
			this.txtBox_Value.Location = new System.Drawing.Point(654, 379);
			this.txtBox_Value.Name = "txtBox_Value";
			this.txtBox_Value.Size = new System.Drawing.Size(73, 22);
			this.txtBox_Value.TabIndex = 458;
			// 
			// Timer_Refresh
			// 
			this.Timer_Refresh.Interval = 300;
			this.Timer_Refresh.Tick += new System.EventHandler(this.Timer_Refresh_Tick);
			// 
			// Form_SRZ_IO_Maker
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(816, 414);
			this.Controls.Add(this.txtBox_Value);
			this.Controls.Add(this.Btn_IO_List_Change);
			this.Controls.Add(this.txtBox_IoName);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.Label_Io_Type);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.Label_Io_Num);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.Label_IO_List);
			this.Controls.Add(this.ListView_IO_List);
			this.Controls.Add(this.Btn_DataSave);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.ListBox_Module);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.ListBox_CPU);
			this.Controls.Add(this.label56);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form_SRZ_IO_Maker";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Form_SRZ_IO_Maker";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_SRZ_IO_Maker_FormClosing);
			this.Load += new System.EventHandler(this.Form_SRZ_IO_Maker_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private Windows.Win_GlassButton Btn_CpuAdd;
		private Windows.Win_GlassButton Btn_CpuDel;
		private System.Windows.Forms.ListBox ListBox_CPU;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.Panel panel2;
		private Windows.Win_GlassButton Btn_ModuleAdd;
		private Windows.Win_GlassButton Btn_ModuleDel;
		private System.Windows.Forms.ComboBox ComboBox_ModuleType;
		private System.Windows.Forms.ListBox ListBox_Module;
		private System.Windows.Forms.Label label1;
		private Windows.Win_GlassButton Btn_DataSave;
		private System.Windows.Forms.Label Label_IO_List;
		private Windows.Win_ListView ListView_IO_List;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label Label_Io_Num;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label Label_Io_Type;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtBox_IoName;
		private System.Windows.Forms.Label label11;
		private Windows.Win_GlassButton Btn_IO_List_Change;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.TextBox txtBox_Value;
		private System.Windows.Forms.Timer Timer_Refresh;
	}
}