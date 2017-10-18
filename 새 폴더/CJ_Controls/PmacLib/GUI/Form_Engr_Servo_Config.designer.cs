namespace CJ_Controls.PmacLib
{
	partial class Form_Engr_Servo_Config
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
			this.Btn_Close = new System.Windows.Forms.Button();
			this.Btn_ServoData_Save = new System.Windows.Forms.Button();
			this.ListBox_Axis = new System.Windows.Forms.ListBox();
			this.Label_Axis_List = new System.Windows.Forms.Label();
			this.Label_Axis_Name = new System.Windows.Forms.Label();
			this.Text_Axis_Name = new System.Windows.Forms.TextBox();
			this.Label_Axis_Use = new System.Windows.Forms.Label();
			this.Combo_Axis_Use = new System.Windows.Forms.ComboBox();
			this.Tab_Type_Value_Setting = new System.Windows.Forms.TabControl();
			this.Page_Position_Setting = new System.Windows.Forms.TabPage();
			this.Panel_Position_Data = new System.Windows.Forms.Panel();
			this.Btn_Position_Add = new System.Windows.Forms.Button();
			this.Btn_Position_Delete = new System.Windows.Forms.Button();
			this.Btn_Axis_PositionData_Set = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.Num_Position_speed = new System.Windows.Forms.NumericUpDown();
			this.Btn_Position_GetData = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.Num_Position_accel = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.Text_Position_Name = new System.Windows.Forms.TextBox();
			this.Num_Position_Value = new System.Windows.Forms.NumericUpDown();
			this.Label_Positon_Name = new System.Windows.Forms.Label();
			this.Label_PositionValue = new System.Windows.Forms.Label();
			this.ListBox_Position = new System.Windows.Forms.ListBox();
			this.Label_PositionList = new System.Windows.Forms.Label();
			this.Label_Board_No = new System.Windows.Forms.Label();
			this.Btn_Axis_Data_Set = new System.Windows.Forms.Button();
			this.Btn_ServoData_Reload = new System.Windows.Forms.Button();
			this.Label_Axis_Scaled = new System.Windows.Forms.Label();
			this.Num_Scale_Value = new System.Windows.Forms.NumericUpDown();
			this.Label_Axis_Homming = new System.Windows.Forms.Label();
			this.Label_Axis_HomeEnd = new System.Windows.Forms.Label();
			this.Text_Homming = new System.Windows.Forms.TextBox();
			this.Text_HomeEnd = new System.Windows.Forms.TextBox();
			this.Num_PlcNo = new System.Windows.Forms.NumericUpDown();
			this.Label_Axis_PLC_No = new System.Windows.Forms.Label();
			this.Label_Axis_Type = new System.Windows.Forms.Label();
			this.Combo_Axis_Type = new System.Windows.Forms.ComboBox();
			this.Tab_Type_Value_Setting.SuspendLayout();
			this.Page_Position_Setting.SuspendLayout();
			this.Panel_Position_Data.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_Position_speed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Position_accel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Position_Value)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Scale_Value)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_PlcNo)).BeginInit();
			this.SuspendLayout();
			// 
			// Btn_Close
			// 
			this.Btn_Close.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
			this.Btn_Close.Location = new System.Drawing.Point(529, 504);
			this.Btn_Close.Name = "Btn_Close";
			this.Btn_Close.Size = new System.Drawing.Size(247, 55);
			this.Btn_Close.TabIndex = 21;
			this.Btn_Close.Text = "CLOSE";
			this.Btn_Close.UseVisualStyleBackColor = true;
			this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
			// 
			// Btn_ServoData_Save
			// 
			this.Btn_ServoData_Save.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
			this.Btn_ServoData_Save.ForeColor = System.Drawing.Color.Black;
			this.Btn_ServoData_Save.Location = new System.Drawing.Point(12, 504);
			this.Btn_ServoData_Save.Name = "Btn_ServoData_Save";
			this.Btn_ServoData_Save.Size = new System.Drawing.Size(247, 55);
			this.Btn_ServoData_Save.TabIndex = 22;
			this.Btn_ServoData_Save.Text = "SAVE FILE";
			this.Btn_ServoData_Save.UseVisualStyleBackColor = true;
			this.Btn_ServoData_Save.Click += new System.EventHandler(this.Btn_ServoData_Save_Click);
			// 
			// ListBox_Axis
			// 
			this.ListBox_Axis.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ListBox_Axis.FormattingEnabled = true;
			this.ListBox_Axis.ItemHeight = 23;
			this.ListBox_Axis.Location = new System.Drawing.Point(12, 101);
			this.ListBox_Axis.Name = "ListBox_Axis";
			this.ListBox_Axis.Size = new System.Drawing.Size(145, 395);
			this.ListBox_Axis.TabIndex = 446;
			this.ListBox_Axis.SelectedIndexChanged += new System.EventHandler(this.ListBox_Axis_SelectedIndexChanged);
			// 
			// Label_Axis_List
			// 
			this.Label_Axis_List.BackColor = System.Drawing.Color.Silver;
			this.Label_Axis_List.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Axis_List.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Label_Axis_List.Location = new System.Drawing.Point(12, 69);
			this.Label_Axis_List.Name = "Label_Axis_List";
			this.Label_Axis_List.Size = new System.Drawing.Size(145, 29);
			this.Label_Axis_List.TabIndex = 445;
			this.Label_Axis_List.Text = "AXIS";
			this.Label_Axis_List.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_Axis_Name
			// 
			this.Label_Axis_Name.BackColor = System.Drawing.Color.Silver;
			this.Label_Axis_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Axis_Name.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Label_Axis_Name.Location = new System.Drawing.Point(427, 68);
			this.Label_Axis_Name.Name = "Label_Axis_Name";
			this.Label_Axis_Name.Size = new System.Drawing.Size(258, 29);
			this.Label_Axis_Name.TabIndex = 449;
			this.Label_Axis_Name.Text = "AXIS NAME";
			this.Label_Axis_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Text_Axis_Name
			// 
			this.Text_Axis_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Axis_Name.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Text_Axis_Name.Location = new System.Drawing.Point(427, 100);
			this.Text_Axis_Name.Name = "Text_Axis_Name";
			this.Text_Axis_Name.Size = new System.Drawing.Size(258, 27);
			this.Text_Axis_Name.TabIndex = 450;
			// 
			// Label_Axis_Use
			// 
			this.Label_Axis_Use.BackColor = System.Drawing.Color.Silver;
			this.Label_Axis_Use.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Axis_Use.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Label_Axis_Use.Location = new System.Drawing.Point(163, 68);
			this.Label_Axis_Use.Name = "Label_Axis_Use";
			this.Label_Axis_Use.Size = new System.Drawing.Size(126, 29);
			this.Label_Axis_Use.TabIndex = 451;
			this.Label_Axis_Use.Text = "AXIS USE";
			this.Label_Axis_Use.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Combo_Axis_Use
			// 
			this.Combo_Axis_Use.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Combo_Axis_Use.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Combo_Axis_Use.FormattingEnabled = true;
			this.Combo_Axis_Use.Items.AddRange(new object[] {
            "NOT USE",
            "USE"});
			this.Combo_Axis_Use.Location = new System.Drawing.Point(163, 100);
			this.Combo_Axis_Use.Name = "Combo_Axis_Use";
			this.Combo_Axis_Use.Size = new System.Drawing.Size(126, 27);
			this.Combo_Axis_Use.TabIndex = 452;
			// 
			// Tab_Type_Value_Setting
			// 
			this.Tab_Type_Value_Setting.Controls.Add(this.Page_Position_Setting);
			this.Tab_Type_Value_Setting.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Tab_Type_Value_Setting.ItemSize = new System.Drawing.Size(160, 35);
			this.Tab_Type_Value_Setting.Location = new System.Drawing.Point(163, 214);
			this.Tab_Type_Value_Setting.Name = "Tab_Type_Value_Setting";
			this.Tab_Type_Value_Setting.SelectedIndex = 0;
			this.Tab_Type_Value_Setting.Size = new System.Drawing.Size(614, 283);
			this.Tab_Type_Value_Setting.TabIndex = 453;
			this.Tab_Type_Value_Setting.SelectedIndexChanged += new System.EventHandler(this.Tab_Type_Value_Setting_SelectedIndexChanged);
			// 
			// Page_Position_Setting
			// 
			this.Page_Position_Setting.BackColor = System.Drawing.SystemColors.Control;
			this.Page_Position_Setting.Controls.Add(this.Panel_Position_Data);
			this.Page_Position_Setting.Controls.Add(this.Label_PositionValue);
			this.Page_Position_Setting.Controls.Add(this.ListBox_Position);
			this.Page_Position_Setting.Controls.Add(this.Label_PositionList);
			this.Page_Position_Setting.Location = new System.Drawing.Point(4, 39);
			this.Page_Position_Setting.Name = "Page_Position_Setting";
			this.Page_Position_Setting.Padding = new System.Windows.Forms.Padding(3);
			this.Page_Position_Setting.Size = new System.Drawing.Size(606, 240);
			this.Page_Position_Setting.TabIndex = 1;
			this.Page_Position_Setting.Text = "Position Data Setting";
			// 
			// Panel_Position_Data
			// 
			this.Panel_Position_Data.BackColor = System.Drawing.Color.Gainsboro;
			this.Panel_Position_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel_Position_Data.Controls.Add(this.Btn_Position_Add);
			this.Panel_Position_Data.Controls.Add(this.Btn_Position_Delete);
			this.Panel_Position_Data.Controls.Add(this.Btn_Axis_PositionData_Set);
			this.Panel_Position_Data.Controls.Add(this.label6);
			this.Panel_Position_Data.Controls.Add(this.Num_Position_speed);
			this.Panel_Position_Data.Controls.Add(this.Btn_Position_GetData);
			this.Panel_Position_Data.Controls.Add(this.label2);
			this.Panel_Position_Data.Controls.Add(this.Num_Position_accel);
			this.Panel_Position_Data.Controls.Add(this.label1);
			this.Panel_Position_Data.Controls.Add(this.Text_Position_Name);
			this.Panel_Position_Data.Controls.Add(this.Num_Position_Value);
			this.Panel_Position_Data.Controls.Add(this.Label_Positon_Name);
			this.Panel_Position_Data.Location = new System.Drawing.Point(165, 41);
			this.Panel_Position_Data.Name = "Panel_Position_Data";
			this.Panel_Position_Data.Size = new System.Drawing.Size(426, 188);
			this.Panel_Position_Data.TabIndex = 449;
			// 
			// Btn_Position_Add
			// 
			this.Btn_Position_Add.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Btn_Position_Add.Location = new System.Drawing.Point(12, 134);
			this.Btn_Position_Add.Name = "Btn_Position_Add";
			this.Btn_Position_Add.Size = new System.Drawing.Size(130, 37);
			this.Btn_Position_Add.TabIndex = 450;
			this.Btn_Position_Add.Text = "Add";
			this.Btn_Position_Add.UseVisualStyleBackColor = true;
			this.Btn_Position_Add.Click += new System.EventHandler(this.Btn_Position_Add_Click);
			// 
			// Btn_Position_Delete
			// 
			this.Btn_Position_Delete.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Btn_Position_Delete.Location = new System.Drawing.Point(143, 134);
			this.Btn_Position_Delete.Name = "Btn_Position_Delete";
			this.Btn_Position_Delete.Size = new System.Drawing.Size(130, 37);
			this.Btn_Position_Delete.TabIndex = 451;
			this.Btn_Position_Delete.Text = "Delete";
			this.Btn_Position_Delete.UseVisualStyleBackColor = true;
			this.Btn_Position_Delete.Click += new System.EventHandler(this.Btn_Position_Delete_Click);
			// 
			// Btn_Axis_PositionData_Set
			// 
			this.Btn_Axis_PositionData_Set.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Btn_Axis_PositionData_Set.Location = new System.Drawing.Point(274, 134);
			this.Btn_Axis_PositionData_Set.Name = "Btn_Axis_PositionData_Set";
			this.Btn_Axis_PositionData_Set.Size = new System.Drawing.Size(130, 37);
			this.Btn_Axis_PositionData_Set.TabIndex = 452;
			this.Btn_Axis_PositionData_Set.Text = "Data Set";
			this.Btn_Axis_PositionData_Set.UseVisualStyleBackColor = true;
			this.Btn_Axis_PositionData_Set.Click += new System.EventHandler(this.Btn_Axis_PositionData_Set_Click);
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Silver;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(12, 69);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(130, 27);
			this.label6.TabIndex = 472;
			this.label6.Text = "Speed";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Num_Position_speed
			// 
			this.Num_Position_speed.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Num_Position_speed.Location = new System.Drawing.Point(144, 69);
			this.Num_Position_speed.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
			this.Num_Position_speed.Name = "Num_Position_speed";
			this.Num_Position_speed.Size = new System.Drawing.Size(260, 27);
			this.Num_Position_speed.TabIndex = 471;
			// 
			// Btn_Position_GetData
			// 
			this.Btn_Position_GetData.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Btn_Position_GetData.Location = new System.Drawing.Point(342, 40);
			this.Btn_Position_GetData.Name = "Btn_Position_GetData";
			this.Btn_Position_GetData.Size = new System.Drawing.Size(62, 27);
			this.Btn_Position_GetData.TabIndex = 470;
			this.Btn_Position_GetData.Text = "Get";
			this.Btn_Position_GetData.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Silver;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(12, 98);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(130, 27);
			this.label2.TabIndex = 463;
			this.label2.Text = "Accel";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Num_Position_accel
			// 
			this.Num_Position_accel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Num_Position_accel.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.Num_Position_accel.Location = new System.Drawing.Point(144, 98);
			this.Num_Position_accel.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.Num_Position_accel.Name = "Num_Position_accel";
			this.Num_Position_accel.Size = new System.Drawing.Size(260, 27);
			this.Num_Position_accel.TabIndex = 462;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Silver;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(12, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 27);
			this.label1.TabIndex = 461;
			this.label1.Text = "Position";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Text_Position_Name
			// 
			this.Text_Position_Name.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Text_Position_Name.Location = new System.Drawing.Point(144, 11);
			this.Text_Position_Name.Name = "Text_Position_Name";
			this.Text_Position_Name.Size = new System.Drawing.Size(260, 27);
			this.Text_Position_Name.TabIndex = 460;
			// 
			// Num_Position_Value
			// 
			this.Num_Position_Value.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Num_Position_Value.Location = new System.Drawing.Point(144, 40);
			this.Num_Position_Value.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.Num_Position_Value.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
			this.Num_Position_Value.Name = "Num_Position_Value";
			this.Num_Position_Value.Size = new System.Drawing.Size(194, 27);
			this.Num_Position_Value.TabIndex = 459;
			// 
			// Label_Positon_Name
			// 
			this.Label_Positon_Name.BackColor = System.Drawing.Color.Silver;
			this.Label_Positon_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Positon_Name.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Label_Positon_Name.Location = new System.Drawing.Point(12, 11);
			this.Label_Positon_Name.Name = "Label_Positon_Name";
			this.Label_Positon_Name.Size = new System.Drawing.Size(130, 27);
			this.Label_Positon_Name.TabIndex = 458;
			this.Label_Positon_Name.Text = "PT_Name";
			this.Label_Positon_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_PositionValue
			// 
			this.Label_PositionValue.BackColor = System.Drawing.Color.Silver;
			this.Label_PositionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_PositionValue.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Label_PositionValue.Location = new System.Drawing.Point(165, 10);
			this.Label_PositionValue.Name = "Label_PositionValue";
			this.Label_PositionValue.Size = new System.Drawing.Size(426, 29);
			this.Label_PositionValue.TabIndex = 448;
			this.Label_PositionValue.Text = "Position Data";
			this.Label_PositionValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ListBox_Position
			// 
			this.ListBox_Position.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ListBox_Position.FormattingEnabled = true;
			this.ListBox_Position.ItemHeight = 23;
			this.ListBox_Position.Location = new System.Drawing.Point(14, 41);
			this.ListBox_Position.Name = "ListBox_Position";
			this.ListBox_Position.Size = new System.Drawing.Size(145, 188);
			this.ListBox_Position.TabIndex = 447;
			this.ListBox_Position.SelectedIndexChanged += new System.EventHandler(this.ListBox_Position_SelectedIndexChanged);
			// 
			// Label_PositionList
			// 
			this.Label_PositionList.BackColor = System.Drawing.Color.Silver;
			this.Label_PositionList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_PositionList.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Label_PositionList.Location = new System.Drawing.Point(14, 10);
			this.Label_PositionList.Name = "Label_PositionList";
			this.Label_PositionList.Size = new System.Drawing.Size(145, 29);
			this.Label_PositionList.TabIndex = 446;
			this.Label_PositionList.Text = "Position";
			this.Label_PositionList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_Board_No
			// 
			this.Label_Board_No.BackColor = System.Drawing.Color.Gold;
			this.Label_Board_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Board_No.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label_Board_No.ForeColor = System.Drawing.Color.Black;
			this.Label_Board_No.Location = new System.Drawing.Point(12, 9);
			this.Label_Board_No.Name = "Label_Board_No";
			this.Label_Board_No.Size = new System.Drawing.Size(766, 56);
			this.Label_Board_No.TabIndex = 454;
			this.Label_Board_No.Text = "SERVO BOARD";
			this.Label_Board_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Btn_Axis_Data_Set
			// 
			this.Btn_Axis_Data_Set.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
			this.Btn_Axis_Data_Set.ForeColor = System.Drawing.Color.Black;
			this.Btn_Axis_Data_Set.Location = new System.Drawing.Point(690, 68);
			this.Btn_Axis_Data_Set.Name = "Btn_Axis_Data_Set";
			this.Btn_Axis_Data_Set.Size = new System.Drawing.Size(88, 129);
			this.Btn_Axis_Data_Set.TabIndex = 462;
			this.Btn_Axis_Data_Set.Text = "Data Set";
			this.Btn_Axis_Data_Set.UseVisualStyleBackColor = true;
			this.Btn_Axis_Data_Set.Click += new System.EventHandler(this.Btn_Axis_Data_Set_Click);
			// 
			// Btn_ServoData_Reload
			// 
			this.Btn_ServoData_Reload.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
			this.Btn_ServoData_Reload.ForeColor = System.Drawing.Color.Black;
			this.Btn_ServoData_Reload.Location = new System.Drawing.Point(271, 504);
			this.Btn_ServoData_Reload.Name = "Btn_ServoData_Reload";
			this.Btn_ServoData_Reload.Size = new System.Drawing.Size(247, 55);
			this.Btn_ServoData_Reload.TabIndex = 463;
			this.Btn_ServoData_Reload.Text = "RELOAD FILE";
			this.Btn_ServoData_Reload.UseVisualStyleBackColor = true;
			this.Btn_ServoData_Reload.Click += new System.EventHandler(this.Btn_ServoData_Reload_Click);
			// 
			// Label_Axis_Scaled
			// 
			this.Label_Axis_Scaled.BackColor = System.Drawing.Color.Silver;
			this.Label_Axis_Scaled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Axis_Scaled.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Label_Axis_Scaled.Location = new System.Drawing.Point(559, 138);
			this.Label_Axis_Scaled.Name = "Label_Axis_Scaled";
			this.Label_Axis_Scaled.Size = new System.Drawing.Size(126, 29);
			this.Label_Axis_Scaled.TabIndex = 464;
			this.Label_Axis_Scaled.Text = "SCALED";
			this.Label_Axis_Scaled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Num_Scale_Value
			// 
			this.Num_Scale_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Num_Scale_Value.DecimalPlaces = 1;
			this.Num_Scale_Value.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Num_Scale_Value.Location = new System.Drawing.Point(559, 170);
			this.Num_Scale_Value.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.Num_Scale_Value.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
			this.Num_Scale_Value.Name = "Num_Scale_Value";
			this.Num_Scale_Value.Size = new System.Drawing.Size(126, 27);
			this.Num_Scale_Value.TabIndex = 473;
			this.Num_Scale_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// Label_Axis_Homming
			// 
			this.Label_Axis_Homming.BackColor = System.Drawing.Color.Silver;
			this.Label_Axis_Homming.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Axis_Homming.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Label_Axis_Homming.Location = new System.Drawing.Point(163, 138);
			this.Label_Axis_Homming.Name = "Label_Axis_Homming";
			this.Label_Axis_Homming.Size = new System.Drawing.Size(126, 29);
			this.Label_Axis_Homming.TabIndex = 475;
			this.Label_Axis_Homming.Text = "HOMMING";
			this.Label_Axis_Homming.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_Axis_HomeEnd
			// 
			this.Label_Axis_HomeEnd.BackColor = System.Drawing.Color.Silver;
			this.Label_Axis_HomeEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Axis_HomeEnd.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Label_Axis_HomeEnd.Location = new System.Drawing.Point(295, 138);
			this.Label_Axis_HomeEnd.Name = "Label_Axis_HomeEnd";
			this.Label_Axis_HomeEnd.Size = new System.Drawing.Size(126, 29);
			this.Label_Axis_HomeEnd.TabIndex = 474;
			this.Label_Axis_HomeEnd.Text = "HOME END";
			this.Label_Axis_HomeEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Text_Homming
			// 
			this.Text_Homming.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_Homming.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Text_Homming.Location = new System.Drawing.Point(163, 170);
			this.Text_Homming.Name = "Text_Homming";
			this.Text_Homming.Size = new System.Drawing.Size(126, 27);
			this.Text_Homming.TabIndex = 477;
			// 
			// Text_HomeEnd
			// 
			this.Text_HomeEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Text_HomeEnd.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Text_HomeEnd.Location = new System.Drawing.Point(295, 170);
			this.Text_HomeEnd.Name = "Text_HomeEnd";
			this.Text_HomeEnd.Size = new System.Drawing.Size(126, 27);
			this.Text_HomeEnd.TabIndex = 478;
			// 
			// Num_PlcNo
			// 
			this.Num_PlcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Num_PlcNo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Num_PlcNo.Location = new System.Drawing.Point(427, 170);
			this.Num_PlcNo.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.Num_PlcNo.Name = "Num_PlcNo";
			this.Num_PlcNo.Size = new System.Drawing.Size(126, 27);
			this.Num_PlcNo.TabIndex = 480;
			this.Num_PlcNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Num_PlcNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// Label_Axis_PLC_No
			// 
			this.Label_Axis_PLC_No.BackColor = System.Drawing.Color.Silver;
			this.Label_Axis_PLC_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Axis_PLC_No.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Label_Axis_PLC_No.Location = new System.Drawing.Point(427, 138);
			this.Label_Axis_PLC_No.Name = "Label_Axis_PLC_No";
			this.Label_Axis_PLC_No.Size = new System.Drawing.Size(126, 29);
			this.Label_Axis_PLC_No.TabIndex = 479;
			this.Label_Axis_PLC_No.Text = "PLC NO";
			this.Label_Axis_PLC_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_Axis_Type
			// 
			this.Label_Axis_Type.BackColor = System.Drawing.Color.Silver;
			this.Label_Axis_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Axis_Type.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
			this.Label_Axis_Type.Location = new System.Drawing.Point(295, 68);
			this.Label_Axis_Type.Name = "Label_Axis_Type";
			this.Label_Axis_Type.Size = new System.Drawing.Size(126, 29);
			this.Label_Axis_Type.TabIndex = 451;
			this.Label_Axis_Type.Text = "AXIS Type";
			this.Label_Axis_Type.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Combo_Axis_Type
			// 
			this.Combo_Axis_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Combo_Axis_Type.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
			this.Combo_Axis_Type.FormattingEnabled = true;
			this.Combo_Axis_Type.Items.AddRange(new object[] {
            "SERVO",
            "STEPPING",
            "LINEAR"});
			this.Combo_Axis_Type.Location = new System.Drawing.Point(295, 100);
			this.Combo_Axis_Type.Name = "Combo_Axis_Type";
			this.Combo_Axis_Type.Size = new System.Drawing.Size(126, 27);
			this.Combo_Axis_Type.TabIndex = 452;
			// 
			// Form_Engr_Servo_Config
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(789, 571);
			this.ControlBox = false;
			this.Controls.Add(this.Num_PlcNo);
			this.Controls.Add(this.Label_Axis_PLC_No);
			this.Controls.Add(this.Text_HomeEnd);
			this.Controls.Add(this.Text_Homming);
			this.Controls.Add(this.Label_Axis_Homming);
			this.Controls.Add(this.Label_Axis_HomeEnd);
			this.Controls.Add(this.Num_Scale_Value);
			this.Controls.Add(this.Label_Axis_Scaled);
			this.Controls.Add(this.Btn_ServoData_Reload);
			this.Controls.Add(this.Btn_Axis_Data_Set);
			this.Controls.Add(this.Label_Board_No);
			this.Controls.Add(this.Tab_Type_Value_Setting);
			this.Controls.Add(this.Combo_Axis_Type);
			this.Controls.Add(this.Combo_Axis_Use);
			this.Controls.Add(this.Label_Axis_Type);
			this.Controls.Add(this.Label_Axis_Use);
			this.Controls.Add(this.Text_Axis_Name);
			this.Controls.Add(this.Label_Axis_Name);
			this.Controls.Add(this.ListBox_Axis);
			this.Controls.Add(this.Label_Axis_List);
			this.Controls.Add(this.Btn_ServoData_Save);
			this.Controls.Add(this.Btn_Close);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form_Engr_Servo_Config";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form_ServoConfig";
			this.Load += new System.EventHandler(this.Form_Engr_Servo_Config_Load);
			this.Tab_Type_Value_Setting.ResumeLayout(false);
			this.Page_Position_Setting.ResumeLayout(false);
			this.Panel_Position_Data.ResumeLayout(false);
			this.Panel_Position_Data.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_Position_speed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Position_accel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Position_Value)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Scale_Value)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_PlcNo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button Btn_Close;
		private System.Windows.Forms.Button Btn_ServoData_Save;
		private System.Windows.Forms.ListBox ListBox_Axis;
		private System.Windows.Forms.Label Label_Axis_List;
		private System.Windows.Forms.Label Label_Axis_Name;
		private System.Windows.Forms.TextBox Text_Axis_Name;
		private System.Windows.Forms.Label Label_Axis_Use;
		private System.Windows.Forms.ComboBox Combo_Axis_Use;
		private System.Windows.Forms.TabControl Tab_Type_Value_Setting;
		private System.Windows.Forms.TabPage Page_Position_Setting;
		private System.Windows.Forms.ListBox ListBox_Position;
		private System.Windows.Forms.Label Label_PositionList;
		private System.Windows.Forms.Panel Panel_Position_Data;
		private System.Windows.Forms.Label Label_PositionValue;
		private System.Windows.Forms.Button Btn_Position_Delete;
		private System.Windows.Forms.Button Btn_Position_Add;
		private System.Windows.Forms.NumericUpDown Num_Position_Value;
		private System.Windows.Forms.Label Label_Positon_Name;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox Text_Position_Name;
		private System.Windows.Forms.Button Btn_Position_GetData;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown Num_Position_accel;
		private System.Windows.Forms.Label Label_Board_No;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown Num_Position_speed;
		private System.Windows.Forms.Button Btn_Axis_PositionData_Set;
		private System.Windows.Forms.Button Btn_Axis_Data_Set;
		private System.Windows.Forms.Button Btn_ServoData_Reload;
		private System.Windows.Forms.Label Label_Axis_Scaled;
		private System.Windows.Forms.NumericUpDown Num_Scale_Value;
		private System.Windows.Forms.Label Label_Axis_Homming;
		private System.Windows.Forms.Label Label_Axis_HomeEnd;
		private System.Windows.Forms.TextBox Text_Homming;
		private System.Windows.Forms.TextBox Text_HomeEnd;
		private System.Windows.Forms.NumericUpDown Num_PlcNo;
		private System.Windows.Forms.Label Label_Axis_PLC_No;
		private System.Windows.Forms.Label Label_Axis_Type;
		private System.Windows.Forms.ComboBox Combo_Axis_Type;
	}
}