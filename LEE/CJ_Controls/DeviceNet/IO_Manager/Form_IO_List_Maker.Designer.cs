namespace CJ_Controls.DeviceNet
{
	partial class Form_IO_List_Maker
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
            this.label56 = new System.Windows.Forms.Label();
            this.ListBox_Adapter = new System.Windows.Forms.ListBox();
            this.ListBox_Module = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Label_IO_List = new System.Windows.Forms.Label();
            this.ComboBox_ModuleType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Label_InputMax = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Label_OutputMax = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBox_IoName = new System.Windows.Forms.TextBox();
            this.txtBox_Cable = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Label_Io_SubAddr = new System.Windows.Forms.Label();
            this.Label_Io_Addr = new System.Windows.Forms.Label();
            this.Label_Io_Num = new System.Windows.Forms.Label();
            this.Btn_IO_List_Change = new CJ_Controls.Windows.Win_GlassButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_AdapterAdd = new CJ_Controls.Windows.Win_GlassButton();
            this.Btn_AdapterDel = new CJ_Controls.Windows.Win_GlassButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_ModuleAdd = new CJ_Controls.Windows.Win_GlassButton();
            this.Btn_ModuleDel = new CJ_Controls.Windows.Win_GlassButton();
            this.Btn_DataSave = new CJ_Controls.Windows.Win_GlassButton();
            this.ListView_IO_List = new CJ_Controls.Windows.Win_ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.tbAddr = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.Silver;
            this.label56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label56.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label56.Location = new System.Drawing.Point(12, 9);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(114, 29);
            this.label56.TabIndex = 101;
            this.label56.Text = "Adapter";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ListBox_Adapter
            // 
            this.ListBox_Adapter.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.ListBox_Adapter.FormattingEnabled = true;
            this.ListBox_Adapter.ItemHeight = 23;
            this.ListBox_Adapter.Location = new System.Drawing.Point(12, 41);
            this.ListBox_Adapter.Name = "ListBox_Adapter";
            this.ListBox_Adapter.Size = new System.Drawing.Size(114, 234);
            this.ListBox_Adapter.TabIndex = 424;
            this.ListBox_Adapter.SelectedIndexChanged += new System.EventHandler(this.ListBox_Adapter_SelectedIndexChanged);
            this.ListBox_Adapter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListBox_Adapter_MouseDown);
            // 
            // ListBox_Module
            // 
            this.ListBox_Module.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.ListBox_Module.FormattingEnabled = true;
            this.ListBox_Module.ItemHeight = 21;
            this.ListBox_Module.Location = new System.Drawing.Point(132, 41);
            this.ListBox_Module.Name = "ListBox_Module";
            this.ListBox_Module.Size = new System.Drawing.Size(114, 214);
            this.ListBox_Module.TabIndex = 426;
            this.ListBox_Module.SelectedIndexChanged += new System.EventHandler(this.ListBox_Module_SelectedIndexChanged);
            this.ListBox_Module.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListBox_Module_MouseDown);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(132, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 29);
            this.label1.TabIndex = 425;
            this.label1.Text = "Module";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_IO_List
            // 
            this.Label_IO_List.BackColor = System.Drawing.Color.Silver;
            this.Label_IO_List.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_IO_List.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.Label_IO_List.Location = new System.Drawing.Point(252, 9);
            this.Label_IO_List.Name = "Label_IO_List";
            this.Label_IO_List.Size = new System.Drawing.Size(530, 29);
            this.Label_IO_List.TabIndex = 428;
            this.Label_IO_List.Text = "IO List";
            this.Label_IO_List.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ComboBox_ModuleType
            // 
            this.ComboBox_ModuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_ModuleType.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ComboBox_ModuleType.FormattingEnabled = true;
            this.ComboBox_ModuleType.Items.AddRange(new object[] {
            "D_INPUT",
            "D_OUTPUT",
            "A_INPUT",
            "A_OUTPUT"});
            this.ComboBox_ModuleType.Location = new System.Drawing.Point(3, 4);
            this.ComboBox_ModuleType.Name = "ComboBox_ModuleType";
            this.ComboBox_ModuleType.Size = new System.Drawing.Size(106, 20);
            this.ComboBox_ModuleType.TabIndex = 434;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(12, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 29);
            this.label2.TabIndex = 435;
            this.label2.Text = "Input Max";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_InputMax
            // 
            this.Label_InputMax.BackColor = System.Drawing.Color.Yellow;
            this.Label_InputMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_InputMax.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_InputMax.Location = new System.Drawing.Point(132, 363);
            this.Label_InputMax.Name = "Label_InputMax";
            this.Label_InputMax.Size = new System.Drawing.Size(114, 29);
            this.Label_InputMax.TabIndex = 436;
            this.Label_InputMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Silver;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(12, 396);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 29);
            this.label4.TabIndex = 437;
            this.label4.Text = "Output Max";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_OutputMax
            // 
            this.Label_OutputMax.BackColor = System.Drawing.Color.Yellow;
            this.Label_OutputMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_OutputMax.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_OutputMax.Location = new System.Drawing.Point(132, 396);
            this.Label_OutputMax.Name = "Label_OutputMax";
            this.Label_OutputMax.Size = new System.Drawing.Size(114, 29);
            this.Label_OutputMax.TabIndex = 438;
            this.Label_OutputMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbAddr);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtBox_IoName);
            this.groupBox1.Controls.Add(this.txtBox_Cable);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.Label_Io_SubAddr);
            this.groupBox1.Controls.Add(this.Label_Io_Addr);
            this.groupBox1.Controls.Add(this.Label_Io_Num);
            this.groupBox1.Controls.Add(this.Btn_IO_List_Change);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(252, 363);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 108);
            this.groupBox1.TabIndex = 440;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[ IO List 변경 ]";
            // 
            // txtBox_IoName
            // 
            this.txtBox_IoName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_IoName.Font = new System.Drawing.Font("굴림", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtBox_IoName.Location = new System.Drawing.Point(110, 77);
            this.txtBox_IoName.Name = "txtBox_IoName";
            this.txtBox_IoName.Size = new System.Drawing.Size(279, 22);
            this.txtBox_IoName.TabIndex = 446;
            // 
            // txtBox_Cable
            // 
            this.txtBox_Cable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_Cable.Font = new System.Drawing.Font("굴림", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtBox_Cable.Location = new System.Drawing.Point(110, 48);
            this.txtBox_Cable.Name = "txtBox_Cable";
            this.txtBox_Cable.Size = new System.Drawing.Size(146, 22);
            this.txtBox_Cable.TabIndex = 445;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Silver;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(6, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 22);
            this.label11.TabIndex = 444;
            this.label11.Text = "IO Name";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Io_SubAddr
            // 
            this.Label_Io_SubAddr.BackColor = System.Drawing.Color.Gainsboro;
            this.Label_Io_SubAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_Io_SubAddr.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Io_SubAddr.ForeColor = System.Drawing.Color.Blue;
            this.Label_Io_SubAddr.Location = new System.Drawing.Point(325, 18);
            this.Label_Io_SubAddr.Name = "Label_Io_SubAddr";
            this.Label_Io_SubAddr.Size = new System.Drawing.Size(66, 22);
            this.Label_Io_SubAddr.TabIndex = 443;
            this.Label_Io_SubAddr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Io_Addr
            // 
            this.Label_Io_Addr.BackColor = System.Drawing.Color.Gainsboro;
            this.Label_Io_Addr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_Io_Addr.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Io_Addr.ForeColor = System.Drawing.Color.Blue;
            this.Label_Io_Addr.Location = new System.Drawing.Point(194, 18);
            this.Label_Io_Addr.Name = "Label_Io_Addr";
            this.Label_Io_Addr.Size = new System.Drawing.Size(66, 22);
            this.Label_Io_Addr.TabIndex = 442;
            this.Label_Io_Addr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Io_Num
            // 
            this.Label_Io_Num.BackColor = System.Drawing.Color.Gainsboro;
            this.Label_Io_Num.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_Io_Num.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Io_Num.ForeColor = System.Drawing.Color.Blue;
            this.Label_Io_Num.Location = new System.Drawing.Point(64, 18);
            this.Label_Io_Num.Name = "Label_Io_Num";
            this.Label_Io_Num.Size = new System.Drawing.Size(66, 22);
            this.Label_Io_Num.TabIndex = 441;
            this.Label_Io_Num.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_IO_List_Change
            // 
            this.Btn_IO_List_Change.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Btn_IO_List_Change.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.Btn_IO_List_Change.ForeColor = System.Drawing.Color.Black;
            this.Btn_IO_List_Change.GlowColor = System.Drawing.Color.White;
            this.Btn_IO_List_Change.Location = new System.Drawing.Point(403, 17);
            this.Btn_IO_List_Change.Name = "Btn_IO_List_Change";
            this.Btn_IO_List_Change.OuterBorderColor = System.Drawing.Color.Transparent;
            this.Btn_IO_List_Change.Size = new System.Drawing.Size(117, 82);
            this.Btn_IO_List_Change.TabIndex = 440;
            this.Btn_IO_List_Change.Text = "변경";
            this.Btn_IO_List_Change.Click += new System.EventHandler(this.Btn_IO_List_Change_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Silver;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 22);
            this.label7.TabIndex = 439;
            this.label7.Text = "Cable";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Silver;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(267, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 22);
            this.label6.TabIndex = 438;
            this.label6.Text = "SubAdd";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Silver;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(136, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 22);
            this.label5.TabIndex = 437;
            this.label5.Text = "Addr";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 22);
            this.label3.TabIndex = 436;
            this.label3.Text = "Num";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Btn_AdapterAdd);
            this.panel1.Controls.Add(this.Btn_AdapterDel);
            this.panel1.Location = new System.Drawing.Point(12, 274);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(114, 74);
            this.panel1.TabIndex = 441;
            // 
            // Btn_AdapterAdd
            // 
            this.Btn_AdapterAdd.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Btn_AdapterAdd.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.Btn_AdapterAdd.ForeColor = System.Drawing.Color.Black;
            this.Btn_AdapterAdd.GlowColor = System.Drawing.Color.White;
            this.Btn_AdapterAdd.Location = new System.Drawing.Point(9, 4);
            this.Btn_AdapterAdd.Name = "Btn_AdapterAdd";
            this.Btn_AdapterAdd.OuterBorderColor = System.Drawing.Color.Transparent;
            this.Btn_AdapterAdd.Size = new System.Drawing.Size(96, 31);
            this.Btn_AdapterAdd.TabIndex = 429;
            this.Btn_AdapterAdd.Text = "추가";
            this.Btn_AdapterAdd.Click += new System.EventHandler(this.Btn_AdapterAdd_Click);
            // 
            // Btn_AdapterDel
            // 
            this.Btn_AdapterDel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Btn_AdapterDel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.Btn_AdapterDel.ForeColor = System.Drawing.Color.Black;
            this.Btn_AdapterDel.GlowColor = System.Drawing.Color.White;
            this.Btn_AdapterDel.Location = new System.Drawing.Point(9, 37);
            this.Btn_AdapterDel.Name = "Btn_AdapterDel";
            this.Btn_AdapterDel.OuterBorderColor = System.Drawing.Color.Transparent;
            this.Btn_AdapterDel.Size = new System.Drawing.Size(96, 31);
            this.Btn_AdapterDel.TabIndex = 431;
            this.Btn_AdapterDel.Text = "삭제";
            this.Btn_AdapterDel.Click += new System.EventHandler(this.Btn_AdapterDel_Click);
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
            this.panel2.Size = new System.Drawing.Size(114, 97);
            this.panel2.TabIndex = 442;
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
            this.Btn_ModuleAdd.Size = new System.Drawing.Size(106, 31);
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
            this.Btn_ModuleDel.Size = new System.Drawing.Size(106, 31);
            this.Btn_ModuleDel.TabIndex = 433;
            this.Btn_ModuleDel.Text = "삭제";
            this.Btn_ModuleDel.Click += new System.EventHandler(this.Btn_ModuleDel_Click);
            // 
            // Btn_DataSave
            // 
            this.Btn_DataSave.BackColor = System.Drawing.Color.Blue;
            this.Btn_DataSave.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.Btn_DataSave.ForeColor = System.Drawing.Color.Black;
            this.Btn_DataSave.GlowColor = System.Drawing.Color.White;
            this.Btn_DataSave.Location = new System.Drawing.Point(12, 428);
            this.Btn_DataSave.Name = "Btn_DataSave";
            this.Btn_DataSave.OuterBorderColor = System.Drawing.Color.Transparent;
            this.Btn_DataSave.Size = new System.Drawing.Size(234, 44);
            this.Btn_DataSave.TabIndex = 439;
            this.Btn_DataSave.Text = "데이저 파일 저장";
            this.Btn_DataSave.Click += new System.EventHandler(this.Btn_DataSave_Click);
            // 
            // ListView_IO_List
            // 
            this.ListView_IO_List.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.ListView_IO_List.FullRowSelect = true;
            this.ListView_IO_List.GridLines = true;
            this.ListView_IO_List.Location = new System.Drawing.Point(252, 41);
            this.ListView_IO_List.MultiSelect = false;
            this.ListView_IO_List.Name = "ListView_IO_List";
            this.ListView_IO_List.Size = new System.Drawing.Size(530, 308);
            this.ListView_IO_List.TabIndex = 427;
            this.ListView_IO_List.UseCompatibleStateImageBehavior = false;
            this.ListView_IO_List.View = System.Windows.Forms.View.Details;
            this.ListView_IO_List.SelectedIndexChanged += new System.EventHandler(this.ListView_IO_List_SelectedIndexChanged);
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
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Silver;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(262, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 22);
            this.label8.TabIndex = 447;
            this.label8.Text = "Addr";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbAddr
            // 
            this.tbAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAddr.Font = new System.Drawing.Font("굴림", 9.5F, System.Drawing.FontStyle.Bold);
            this.tbAddr.Location = new System.Drawing.Point(322, 48);
            this.tbAddr.Name = "tbAddr";
            this.tbAddr.Size = new System.Drawing.Size(67, 22);
            this.tbAddr.TabIndex = 448;
            // 
            // Form_IO_List_Maker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(794, 483);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Btn_DataSave);
            this.Controls.Add(this.Label_OutputMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Label_InputMax);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Label_IO_List);
            this.Controls.Add(this.ListView_IO_List);
            this.Controls.Add(this.ListBox_Module);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBox_Adapter);
            this.Controls.Add(this.label56);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_IO_List_Maker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IO List Maker";
            this.Load += new System.EventHandler(this.Form_IO_List_Maker_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.ListBox ListBox_Adapter;
		private System.Windows.Forms.ListBox ListBox_Module;
		private System.Windows.Forms.Label label1;
		private Windows.Win_ListView ListView_IO_List;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Label Label_IO_List;
		private Windows.Win_GlassButton Btn_AdapterAdd;
		private Windows.Win_GlassButton Btn_AdapterDel;
		private Windows.Win_GlassButton Btn_ModuleAdd;
		private Windows.Win_GlassButton Btn_ModuleDel;
		private System.Windows.Forms.ComboBox ComboBox_ModuleType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label Label_InputMax;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label Label_OutputMax;
		private Windows.Win_GlassButton Btn_DataSave;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtBox_IoName;
		private System.Windows.Forms.TextBox txtBox_Cable;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label Label_Io_SubAddr;
		private System.Windows.Forms.Label Label_Io_Addr;
		private System.Windows.Forms.Label Label_Io_Num;
		private Windows.Win_GlassButton Btn_IO_List_Change;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TextBox tbAddr;
        private System.Windows.Forms.Label label8;
	}
}