namespace TBDB_CTC.UserCtrl.SubForm.RecipeSub
{
    partial class subRecipe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel25 = new System.Windows.Forms.Panel();
            this.cbbMaxSlot = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbbRunMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbbPreAlignCarrier = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbbPreAlignDevice = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbbPostAlignCarrier = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkUsePreAlignCarrier = new System.Windows.Forms.CheckBox();
            this.chkUsePreAlignDevice = new System.Windows.Forms.CheckBox();
            this.chkUsePostAlignCarrier = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbbLami = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cbbBonder = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtHpTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkUseHP = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbxRcpTarget = new System.Windows.Forms.TextBox();
            this.tbxRcpSoruce = new System.Windows.Forms.TextBox();
            this.a1Panel3 = new Owf.Controls.A1Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.a1Panel1 = new Owf.Controls.A1Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnModelDel = new System.Windows.Forms.Button();
            this.btnModelChange = new System.Windows.Forms.Button();
            this.btnModelNew = new System.Windows.Forms.Button();
            this.gridModel = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.lbRcpName = new System.Windows.Forms.Label();
            this.panel25.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.a1Panel3.SuspendLayout();
            this.a1Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridModel)).BeginInit();
            this.SuspendLayout();
            // 
            // panel25
            // 
            this.panel25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel25.Controls.Add(this.cbbMaxSlot);
            this.panel25.Location = new System.Drawing.Point(604, 34);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(184, 32);
            this.panel25.TabIndex = 992;
            // 
            // cbbMaxSlot
            // 
            this.cbbMaxSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMaxSlot.FormattingEnabled = true;
            this.cbbMaxSlot.ItemHeight = 12;
            this.cbbMaxSlot.Location = new System.Drawing.Point(2, 4);
            this.cbbMaxSlot.Name = "cbbMaxSlot";
            this.cbbMaxSlot.Size = new System.Drawing.Size(173, 20);
            this.cbbMaxSlot.TabIndex = 0;
            this.cbbMaxSlot.Tag = "0";
            this.cbbMaxSlot.SelectedIndexChanged += new System.EventHandler(this.cbbMaxSlot_SelectedIndexChanged);
            // 
            // label34
            // 
            this.label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label34.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label34.ForeColor = System.Drawing.Color.White;
            this.label34.Location = new System.Drawing.Point(424, 34);
            this.label34.Name = "label34";
            this.label34.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label34.Size = new System.Drawing.Size(181, 32);
            this.label34.TabIndex = 991;
            this.label34.Text = "Max Use Slot";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbbRunMode);
            this.panel1.Location = new System.Drawing.Point(604, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 32);
            this.panel1.TabIndex = 994;
            // 
            // cbbRunMode
            // 
            this.cbbRunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRunMode.FormattingEnabled = true;
            this.cbbRunMode.ItemHeight = 12;
            this.cbbRunMode.Location = new System.Drawing.Point(2, 4);
            this.cbbRunMode.Name = "cbbRunMode";
            this.cbbRunMode.Size = new System.Drawing.Size(173, 20);
            this.cbbRunMode.TabIndex = 0;
            this.cbbRunMode.Tag = "1";
            this.cbbRunMode.SelectedIndexChanged += new System.EventHandler(this.cbbMaxSlot_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(424, 69);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(181, 32);
            this.label1.TabIndex = 993;
            this.label1.Text = "Run Mode";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cbbPreAlignCarrier);
            this.panel2.Location = new System.Drawing.Point(604, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(184, 32);
            this.panel2.TabIndex = 996;
            // 
            // cbbPreAlignCarrier
            // 
            this.cbbPreAlignCarrier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPreAlignCarrier.FormattingEnabled = true;
            this.cbbPreAlignCarrier.ItemHeight = 12;
            this.cbbPreAlignCarrier.Location = new System.Drawing.Point(2, 4);
            this.cbbPreAlignCarrier.Name = "cbbPreAlignCarrier";
            this.cbbPreAlignCarrier.Size = new System.Drawing.Size(173, 20);
            this.cbbPreAlignCarrier.TabIndex = 0;
            this.cbbPreAlignCarrier.Tag = "2";
            this.cbbPreAlignCarrier.SelectedIndexChanged += new System.EventHandler(this.cbbMaxSlot_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(424, 104);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(181, 32);
            this.label2.TabIndex = 995;
            this.label2.Text = "Pre-Align Carrier Condition";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cbbPreAlignDevice);
            this.panel3.Location = new System.Drawing.Point(604, 139);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(184, 32);
            this.panel3.TabIndex = 998;
            // 
            // cbbPreAlignDevice
            // 
            this.cbbPreAlignDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPreAlignDevice.FormattingEnabled = true;
            this.cbbPreAlignDevice.ItemHeight = 12;
            this.cbbPreAlignDevice.Location = new System.Drawing.Point(2, 4);
            this.cbbPreAlignDevice.Name = "cbbPreAlignDevice";
            this.cbbPreAlignDevice.Size = new System.Drawing.Size(173, 20);
            this.cbbPreAlignDevice.TabIndex = 0;
            this.cbbPreAlignDevice.Tag = "3";
            this.cbbPreAlignDevice.SelectedIndexChanged += new System.EventHandler(this.cbbMaxSlot_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(424, 139);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(181, 32);
            this.label3.TabIndex = 997;
            this.label3.Text = "Pre-Align Device Condition";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.cbbPostAlignCarrier);
            this.panel4.Location = new System.Drawing.Point(604, 174);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(184, 32);
            this.panel4.TabIndex = 1000;
            // 
            // cbbPostAlignCarrier
            // 
            this.cbbPostAlignCarrier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPostAlignCarrier.FormattingEnabled = true;
            this.cbbPostAlignCarrier.ItemHeight = 12;
            this.cbbPostAlignCarrier.Location = new System.Drawing.Point(2, 4);
            this.cbbPostAlignCarrier.Name = "cbbPostAlignCarrier";
            this.cbbPostAlignCarrier.Size = new System.Drawing.Size(173, 20);
            this.cbbPostAlignCarrier.TabIndex = 0;
            this.cbbPostAlignCarrier.Tag = "4";
            this.cbbPostAlignCarrier.SelectedIndexChanged += new System.EventHandler(this.cbbMaxSlot_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(424, 174);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label4.Size = new System.Drawing.Size(181, 32);
            this.label4.TabIndex = 999;
            this.label4.Text = "Post-Align Carrier Condition";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkUsePreAlignCarrier
            // 
            this.chkUsePreAlignCarrier.AutoSize = true;
            this.chkUsePreAlignCarrier.ForeColor = System.Drawing.Color.White;
            this.chkUsePreAlignCarrier.Location = new System.Drawing.Point(902, 71);
            this.chkUsePreAlignCarrier.Name = "chkUsePreAlignCarrier";
            this.chkUsePreAlignCarrier.Size = new System.Drawing.Size(145, 16);
            this.chkUsePreAlignCarrier.TabIndex = 1001;
            this.chkUsePreAlignCarrier.Text = "Use Pre-Align Carrier";
            this.chkUsePreAlignCarrier.UseVisualStyleBackColor = true;
            this.chkUsePreAlignCarrier.Visible = false;
            // 
            // chkUsePreAlignDevice
            // 
            this.chkUsePreAlignDevice.AutoSize = true;
            this.chkUsePreAlignDevice.ForeColor = System.Drawing.Color.White;
            this.chkUsePreAlignDevice.Location = new System.Drawing.Point(902, 106);
            this.chkUsePreAlignDevice.Name = "chkUsePreAlignDevice";
            this.chkUsePreAlignDevice.Size = new System.Drawing.Size(145, 16);
            this.chkUsePreAlignDevice.TabIndex = 1002;
            this.chkUsePreAlignDevice.Text = "Use Pre-Align Device";
            this.chkUsePreAlignDevice.UseVisualStyleBackColor = true;
            this.chkUsePreAlignDevice.Visible = false;
            // 
            // chkUsePostAlignCarrier
            // 
            this.chkUsePostAlignCarrier.AutoSize = true;
            this.chkUsePostAlignCarrier.ForeColor = System.Drawing.Color.White;
            this.chkUsePostAlignCarrier.Location = new System.Drawing.Point(902, 141);
            this.chkUsePostAlignCarrier.Name = "chkUsePostAlignCarrier";
            this.chkUsePostAlignCarrier.Size = new System.Drawing.Size(151, 16);
            this.chkUsePostAlignCarrier.TabIndex = 1003;
            this.chkUsePostAlignCarrier.Text = "Use Post-Align Carrier";
            this.chkUsePostAlignCarrier.UseVisualStyleBackColor = true;
            this.chkUsePostAlignCarrier.Visible = false;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.cbbLami);
            this.panel5.Location = new System.Drawing.Point(604, 209);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(184, 32);
            this.panel5.TabIndex = 1005;
            // 
            // cbbLami
            // 
            this.cbbLami.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLami.FormattingEnabled = true;
            this.cbbLami.ItemHeight = 12;
            this.cbbLami.Location = new System.Drawing.Point(2, 4);
            this.cbbLami.Name = "cbbLami";
            this.cbbLami.Size = new System.Drawing.Size(173, 20);
            this.cbbLami.TabIndex = 0;
            this.cbbLami.Tag = "5";
            this.cbbLami.SelectedIndexChanged += new System.EventHandler(this.cbbMaxSlot_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(424, 209);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label5.Size = new System.Drawing.Size(181, 32);
            this.label5.TabIndex = 1004;
            this.label5.Text = "Laminator Condition";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.cbbBonder);
            this.panel6.Location = new System.Drawing.Point(604, 244);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(184, 32);
            this.panel6.TabIndex = 1007;
            // 
            // cbbBonder
            // 
            this.cbbBonder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbBonder.FormattingEnabled = true;
            this.cbbBonder.ItemHeight = 12;
            this.cbbBonder.Location = new System.Drawing.Point(2, 4);
            this.cbbBonder.Name = "cbbBonder";
            this.cbbBonder.Size = new System.Drawing.Size(173, 20);
            this.cbbBonder.TabIndex = 0;
            this.cbbBonder.Tag = "6";
            this.cbbBonder.SelectedIndexChanged += new System.EventHandler(this.cbbMaxSlot_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(424, 244);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(181, 32);
            this.label6.TabIndex = 1006;
            this.label6.Text = "Bonder Condition";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.txtHpTime);
            this.panel7.Location = new System.Drawing.Point(604, 281);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(184, 32);
            this.panel7.TabIndex = 1010;
            // 
            // txtHpTime
            // 
            this.txtHpTime.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtHpTime.Location = new System.Drawing.Point(2, 3);
            this.txtHpTime.Name = "txtHpTime";
            this.txtHpTime.Size = new System.Drawing.Size(173, 25);
            this.txtHpTime.TabIndex = 921;
            this.txtHpTime.TextChanged += new System.EventHandler(this.txtHpTime_TextChanged);
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(424, 281);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label7.Size = new System.Drawing.Size(181, 32);
            this.label7.TabIndex = 1009;
            this.label7.Text = "HP Time";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkUseHP
            // 
            this.chkUseHP.AutoSize = true;
            this.chkUseHP.ForeColor = System.Drawing.Color.White;
            this.chkUseHP.Location = new System.Drawing.Point(902, 176);
            this.chkUseHP.Name = "chkUseHP";
            this.chkUseHP.Size = new System.Drawing.Size(66, 16);
            this.chkUseHP.TabIndex = 1011;
            this.chkUseHP.Text = "Use HP";
            this.chkUseHP.UseVisualStyleBackColor = true;
            this.chkUseHP.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(792, 292);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 12);
            this.label8.TabIndex = 1136;
            this.label8.Text = "sec";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.Location = new System.Drawing.Point(937, 555);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(210, 63);
            this.btnSave.TabIndex = 1137;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbxRcpTarget
            // 
            this.tbxRcpTarget.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbxRcpTarget.Location = new System.Drawing.Point(1, 483);
            this.tbxRcpTarget.Name = "tbxRcpTarget";
            this.tbxRcpTarget.Size = new System.Drawing.Size(422, 28);
            this.tbxRcpTarget.TabIndex = 1141;
            this.tbxRcpTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxRcpSoruce
            // 
            this.tbxRcpSoruce.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbxRcpSoruce.Location = new System.Drawing.Point(1, 432);
            this.tbxRcpSoruce.Name = "tbxRcpSoruce";
            this.tbxRcpSoruce.ReadOnly = true;
            this.tbxRcpSoruce.Size = new System.Drawing.Size(422, 28);
            this.tbxRcpSoruce.TabIndex = 1140;
            this.tbxRcpSoruce.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // a1Panel3
            // 
            this.a1Panel3.BorderColor = System.Drawing.Color.Black;
            this.a1Panel3.Controls.Add(this.label9);
            this.a1Panel3.GradientEndColor = System.Drawing.Color.Silver;
            this.a1Panel3.GradientStartColor = System.Drawing.Color.White;
            this.a1Panel3.Image = null;
            this.a1Panel3.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel3.Location = new System.Drawing.Point(1, 459);
            this.a1Panel3.Name = "a1Panel3";
            this.a1Panel3.RoundCornerRadius = 1;
            this.a1Panel3.ShadowOffSet = 1;
            this.a1Panel3.Size = new System.Drawing.Size(423, 26);
            this.a1Panel3.TabIndex = 1139;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(7, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "Target model name";
            // 
            // a1Panel1
            // 
            this.a1Panel1.BorderColor = System.Drawing.Color.Black;
            this.a1Panel1.Controls.Add(this.label10);
            this.a1Panel1.GradientEndColor = System.Drawing.Color.Silver;
            this.a1Panel1.GradientStartColor = System.Drawing.Color.White;
            this.a1Panel1.Image = null;
            this.a1Panel1.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel1.Location = new System.Drawing.Point(1, 408);
            this.a1Panel1.Name = "a1Panel1";
            this.a1Panel1.RoundCornerRadius = 1;
            this.a1Panel1.ShadowOffSet = 1;
            this.a1Panel1.Size = new System.Drawing.Size(423, 26);
            this.a1Panel1.TabIndex = 1138;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(7, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "Source model name";
            // 
            // btnModelDel
            // 
            this.btnModelDel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnModelDel.Location = new System.Drawing.Point(276, 517);
            this.btnModelDel.Name = "btnModelDel";
            this.btnModelDel.Size = new System.Drawing.Size(117, 50);
            this.btnModelDel.TabIndex = 1144;
            this.btnModelDel.Text = "Delete";
            this.btnModelDel.UseVisualStyleBackColor = true;
            this.btnModelDel.Click += new System.EventHandler(this.btnModelDel_Click);
            // 
            // btnModelChange
            // 
            this.btnModelChange.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnModelChange.Location = new System.Drawing.Point(140, 517);
            this.btnModelChange.Name = "btnModelChange";
            this.btnModelChange.Size = new System.Drawing.Size(117, 50);
            this.btnModelChange.TabIndex = 1143;
            this.btnModelChange.Text = "Change";
            this.btnModelChange.UseVisualStyleBackColor = true;
            this.btnModelChange.Click += new System.EventHandler(this.btnModelChange_Click);
            // 
            // btnModelNew
            // 
            this.btnModelNew.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnModelNew.Location = new System.Drawing.Point(5, 517);
            this.btnModelNew.Name = "btnModelNew";
            this.btnModelNew.Size = new System.Drawing.Size(117, 50);
            this.btnModelNew.TabIndex = 1142;
            this.btnModelNew.Text = "New";
            this.btnModelNew.UseVisualStyleBackColor = true;
            this.btnModelNew.Click += new System.EventHandler(this.btnModelNew_Click);
            // 
            // gridModel
            // 
            this.gridModel.AllowUserToAddRows = false;
            this.gridModel.AllowUserToDeleteRows = false;
            this.gridModel.AllowUserToResizeColumns = false;
            this.gridModel.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gridModel.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridModel.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.gridModel.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridModel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridModel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column3,
            this.Column1});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridModel.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridModel.GridColor = System.Drawing.Color.Silver;
            this.gridModel.Location = new System.Drawing.Point(1, 0);
            this.gridModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridModel.MultiSelect = false;
            this.gridModel.Name = "gridModel";
            this.gridModel.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridModel.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridModel.RowHeadersVisible = false;
            this.gridModel.RowHeadersWidth = 25;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gridModel.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gridModel.RowTemplate.Height = 25;
            this.gridModel.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridModel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridModel.ShowCellErrors = false;
            this.gridModel.ShowEditingIcon = false;
            this.gridModel.ShowRowErrors = false;
            this.gridModel.Size = new System.Drawing.Size(421, 409);
            this.gridModel.StandardTab = true;
            this.gridModel.TabIndex = 1145;
            this.gridModel.SelectionChanged += new System.EventHandler(this.gridModel_SelectionChanged);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "MODEL NAME";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "CREATE DATE";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 130;
            // 
            // Column1
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.HeaderText = "LAST DATE";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 130;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(424, 0);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label11.Size = new System.Drawing.Size(181, 32);
            this.label11.TabIndex = 1146;
            this.label11.Text = "Recipe Name";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRcpName
            // 
            this.lbRcpName.BackColor = System.Drawing.Color.LightGray;
            this.lbRcpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbRcpName.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbRcpName.ForeColor = System.Drawing.Color.Black;
            this.lbRcpName.Location = new System.Drawing.Point(606, 0);
            this.lbRcpName.Name = "lbRcpName";
            this.lbRcpName.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbRcpName.Size = new System.Drawing.Size(181, 32);
            this.lbRcpName.TabIndex = 1147;
            this.lbRcpName.Text = "Max Use Slot";
            this.lbRcpName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.Controls.Add(this.lbRcpName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.gridModel);
            this.Controls.Add(this.btnModelDel);
            this.Controls.Add(this.btnModelChange);
            this.Controls.Add(this.btnModelNew);
            this.Controls.Add(this.tbxRcpTarget);
            this.Controls.Add(this.tbxRcpSoruce);
            this.Controls.Add(this.a1Panel3);
            this.Controls.Add(this.a1Panel1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chkUseHP);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkUsePostAlignCarrier);
            this.Controls.Add(this.chkUsePreAlignDevice);
            this.Controls.Add(this.chkUsePreAlignCarrier);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel25);
            this.Controls.Add(this.label34);
            this.Name = "subRecipe";
            this.Size = new System.Drawing.Size(1159, 631);
            this.Load += new System.EventHandler(this.subRecipe_Load);
            this.VisibleChanged += new System.EventHandler(this.subRecipe_VisibleChanged);
            this.panel25.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.a1Panel3.ResumeLayout(false);
            this.a1Panel3.PerformLayout();
            this.a1Panel1.ResumeLayout(false);
            this.a1Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridModel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.ComboBox cbbMaxSlot;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbbRunMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbbPreAlignCarrier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbbPreAlignDevice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cbbPostAlignCarrier;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkUsePreAlignCarrier;
        private System.Windows.Forms.CheckBox chkUsePreAlignDevice;
        private System.Windows.Forms.CheckBox chkUsePostAlignCarrier;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cbbLami;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox cbbBonder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkUseHP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHpTime;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbxRcpTarget;
        private System.Windows.Forms.TextBox tbxRcpSoruce;
        private Owf.Controls.A1Panel a1Panel3;
        private System.Windows.Forms.Label label9;
        private Owf.Controls.A1Panel a1Panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnModelDel;
        private System.Windows.Forms.Button btnModelChange;
        private System.Windows.Forms.Button btnModelNew;
        internal System.Windows.Forms.DataGridView gridModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbRcpName;
    }
}
