namespace TBDB_CTC.GUI
{
    partial class frmMainView
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
            this.panSubMenuC1 = new Owf.Controls.A1Panel();
            this.lbSubMenuC1 = new System.Windows.Forms.Label();
            this.panSubMenuC0 = new Owf.Controls.A1Panel();
            this.lbSubMenuC0 = new System.Windows.Forms.Label();
            this.a1Panel3 = new Owf.Controls.A1Panel();
            this.lbPortName = new System.Windows.Forms.Label();
            this.a1Panel1 = new Owf.Controls.A1Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panMainSubClient = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbRunMode = new System.Windows.Forms.ComboBox();
            this.lbCtcRunMode = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ledCtcModeVTM = new Bulb.LedBulb();
            this.ledCtcModeATM = new Bulb.LedBulb();
            this.ledBulb5 = new Bulb.LedBulb();
            this.ledBulb6 = new Bulb.LedBulb();
            this.ledBulb3 = new Bulb.LedBulb();
            this.ledBulb4 = new Bulb.LedBulb();
            this.ledBulb2 = new Bulb.LedBulb();
            this.ledBulb1 = new Bulb.LedBulb();
            this.panel1.SuspendLayout();
            this.panSubMenuC1.SuspendLayout();
            this.panSubMenuC0.SuspendLayout();
            this.a1Panel3.SuspendLayout();
            this.a1Panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.panSubMenuC1);
            this.panel1.Controls.Add(this.panSubMenuC0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1159, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(105, 819);
            this.panel1.TabIndex = 966;
            // 
            // panSubMenuC1
            // 
            this.panSubMenuC1.BackColor = System.Drawing.Color.Transparent;
            this.panSubMenuC1.BorderColor = System.Drawing.Color.White;
            this.panSubMenuC1.Controls.Add(this.lbSubMenuC1);
            this.panSubMenuC1.GradientEndColor = System.Drawing.SystemColors.ButtonFace;
            this.panSubMenuC1.GradientStartColor = System.Drawing.Color.DimGray;
            this.panSubMenuC1.Image = null;
            this.panSubMenuC1.ImageLocation = new System.Drawing.Point(4, 4);
            this.panSubMenuC1.Location = new System.Drawing.Point(6, 95);
            this.panSubMenuC1.Name = "panSubMenuC1";
            this.panSubMenuC1.RoundCornerRadius = 8;
            this.panSubMenuC1.ShadowOffSet = 6;
            this.panSubMenuC1.Size = new System.Drawing.Size(96, 77);
            this.panSubMenuC1.TabIndex = 55;
            // 
            // lbSubMenuC1
            // 
            this.lbSubMenuC1.BackColor = System.Drawing.Color.Transparent;
            this.lbSubMenuC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSubMenuC1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbSubMenuC1.ForeColor = System.Drawing.Color.Black;
            this.lbSubMenuC1.Location = new System.Drawing.Point(0, 0);
            this.lbSubMenuC1.Name = "lbSubMenuC1";
            this.lbSubMenuC1.Size = new System.Drawing.Size(96, 77);
            this.lbSubMenuC1.TabIndex = 0;
            this.lbSubMenuC1.Tag = "1";
            this.lbSubMenuC1.Text = "Verification";
            this.lbSubMenuC1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbSubMenuC1.Click += new System.EventHandler(this.OnManualSubMenuButtonClick);
            // 
            // panSubMenuC0
            // 
            this.panSubMenuC0.BackColor = System.Drawing.Color.Transparent;
            this.panSubMenuC0.BorderColor = System.Drawing.Color.White;
            this.panSubMenuC0.Controls.Add(this.lbSubMenuC0);
            this.panSubMenuC0.GradientEndColor = System.Drawing.Color.Black;
            this.panSubMenuC0.GradientStartColor = System.Drawing.Color.DimGray;
            this.panSubMenuC0.Image = null;
            this.panSubMenuC0.ImageLocation = new System.Drawing.Point(4, 4);
            this.panSubMenuC0.Location = new System.Drawing.Point(6, 12);
            this.panSubMenuC0.Name = "panSubMenuC0";
            this.panSubMenuC0.RoundCornerRadius = 8;
            this.panSubMenuC0.ShadowOffSet = 6;
            this.panSubMenuC0.Size = new System.Drawing.Size(96, 77);
            this.panSubMenuC0.TabIndex = 54;
            // 
            // lbSubMenuC0
            // 
            this.lbSubMenuC0.BackColor = System.Drawing.Color.Transparent;
            this.lbSubMenuC0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSubMenuC0.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbSubMenuC0.ForeColor = System.Drawing.Color.Gold;
            this.lbSubMenuC0.Location = new System.Drawing.Point(0, 0);
            this.lbSubMenuC0.Name = "lbSubMenuC0";
            this.lbSubMenuC0.Size = new System.Drawing.Size(96, 77);
            this.lbSubMenuC0.TabIndex = 0;
            this.lbSubMenuC0.Tag = "0";
            this.lbSubMenuC0.Text = "Main";
            this.lbSubMenuC0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbSubMenuC0.Click += new System.EventHandler(this.OnManualSubMenuButtonClick);
            // 
            // a1Panel3
            // 
            this.a1Panel3.BorderColor = System.Drawing.Color.White;
            this.a1Panel3.BorderWidth = 0;
            this.a1Panel3.Controls.Add(this.lbPortName);
            this.a1Panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.a1Panel3.GradientEndColor = System.Drawing.Color.Black;
            this.a1Panel3.GradientStartColor = System.Drawing.Color.White;
            this.a1Panel3.Image = null;
            this.a1Panel3.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel3.Location = new System.Drawing.Point(0, 0);
            this.a1Panel3.Margin = new System.Windows.Forms.Padding(2);
            this.a1Panel3.Name = "a1Panel3";
            this.a1Panel3.RoundCornerRadius = 6;
            this.a1Panel3.ShadowOffSet = 0;
            this.a1Panel3.Size = new System.Drawing.Size(1159, 20);
            this.a1Panel3.TabIndex = 980;
            // 
            // lbPortName
            // 
            this.lbPortName.BackColor = System.Drawing.Color.Transparent;
            this.lbPortName.Font = new System.Drawing.Font("맑은 고딕", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPortName.ForeColor = System.Drawing.Color.Gold;
            this.lbPortName.Location = new System.Drawing.Point(2, 1);
            this.lbPortName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPortName.Name = "lbPortName";
            this.lbPortName.Size = new System.Drawing.Size(338, 16);
            this.lbPortName.TabIndex = 0;
            this.lbPortName.Text = "MAIN SCREEN";
            this.lbPortName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // a1Panel1
            // 
            this.a1Panel1.BorderColor = System.Drawing.Color.White;
            this.a1Panel1.BorderWidth = 0;
            this.a1Panel1.Controls.Add(this.label1);
            this.a1Panel1.GradientEndColor = System.Drawing.Color.Gray;
            this.a1Panel1.GradientStartColor = System.Drawing.Color.Silver;
            this.a1Panel1.Image = null;
            this.a1Panel1.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel1.Location = new System.Drawing.Point(2, 24);
            this.a1Panel1.Margin = new System.Windows.Forms.Padding(2);
            this.a1Panel1.Name = "a1Panel1";
            this.a1Panel1.RoundCornerRadius = 6;
            this.a1Panel1.ShadowOffSet = 0;
            this.a1Panel1.Size = new System.Drawing.Size(145, 22);
            this.a1Panel1.TabIndex = 981;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scheduler message";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(148, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1005, 21);
            this.label2.TabIndex = 982;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ledBulb5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.ledBulb6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ledBulb3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ledBulb4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ledBulb2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ledBulb1);
            this.groupBox1.Location = new System.Drawing.Point(622, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 44);
            this.groupBox1.TabIndex = 983;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(469, 21);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 937;
            this.label6.Text = "Abort";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(369, 21);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 935;
            this.label8.Text = "Go Vaccum";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(276, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 933;
            this.label4.Text = "Go ATM";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(190, 21);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 931;
            this.label5.Text = "Process";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(97, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 929;
            this.label3.Text = "Vacuum";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(24, 21);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 927;
            this.label7.Text = "ATM";
            // 
            // panMainSubClient
            // 
            this.panMainSubClient.Location = new System.Drawing.Point(0, 95);
            this.panMainSubClient.Name = "panMainSubClient";
            this.panMainSubClient.Size = new System.Drawing.Size(1159, 724);
            this.panMainSubClient.TabIndex = 984;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbRunMode);
            this.groupBox2.Controls.Add(this.lbCtcRunMode);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.ledCtcModeVTM);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.ledCtcModeATM);
            this.groupBox2.Location = new System.Drawing.Point(7, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 44);
            this.groupBox2.TabIndex = 985;
            this.groupBox2.TabStop = false;
            // 
            // cbRunMode
            // 
            this.cbRunMode.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbRunMode.FormattingEnabled = true;
            this.cbRunMode.Items.AddRange(new object[] {
            "ATM Mode",
            "VTM Mode"});
            this.cbRunMode.Location = new System.Drawing.Point(263, 13);
            this.cbRunMode.Name = "cbRunMode";
            this.cbRunMode.Size = new System.Drawing.Size(106, 23);
            this.cbRunMode.TabIndex = 959;
            this.cbRunMode.SelectedIndexChanged += new System.EventHandler(this.cbRunMode_SelectedIndexChanged);
            // 
            // lbCtcRunMode
            // 
            this.lbCtcRunMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCtcRunMode.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbCtcRunMode.ForeColor = System.Drawing.Color.White;
            this.lbCtcRunMode.Location = new System.Drawing.Point(141, 13);
            this.lbCtcRunMode.Name = "lbCtcRunMode";
            this.lbCtcRunMode.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbCtcRunMode.Size = new System.Drawing.Size(116, 24);
            this.lbCtcRunMode.TabIndex = 958;
            this.lbCtcRunMode.Text = "RunMode : VTM";
            this.lbCtcRunMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("맑은 고딕", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ForeColor = System.Drawing.Color.Silver;
            this.label13.Location = new System.Drawing.Point(97, 21);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 929;
            this.label13.Text = "VTM";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("맑은 고딕", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.ForeColor = System.Drawing.Color.Silver;
            this.label14.Location = new System.Drawing.Point(24, 21);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 927;
            this.label14.Text = "ATM";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ledCtcModeVTM
            // 
            this.ledCtcModeVTM.Color = System.Drawing.Color.LightSlateGray;
            this.ledCtcModeVTM.DarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(46)))), ((int)(((byte)(52)))));
            this.ledCtcModeVTM.DarkDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ledCtcModeVTM.Location = new System.Drawing.Point(78, 19);
            this.ledCtcModeVTM.Margin = new System.Windows.Forms.Padding(2);
            this.ledCtcModeVTM.Name = "ledCtcModeVTM";
            this.ledCtcModeVTM.On = true;
            this.ledCtcModeVTM.Size = new System.Drawing.Size(23, 15);
            this.ledCtcModeVTM.TabIndex = 928;
            this.ledCtcModeVTM.Text = "ledBulb11";
            // 
            // ledCtcModeATM
            // 
            this.ledCtcModeATM.Color = System.Drawing.Color.LightSlateGray;
            this.ledCtcModeATM.DarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(46)))), ((int)(((byte)(52)))));
            this.ledCtcModeATM.DarkDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ledCtcModeATM.Location = new System.Drawing.Point(5, 19);
            this.ledCtcModeATM.Margin = new System.Windows.Forms.Padding(2);
            this.ledCtcModeATM.Name = "ledCtcModeATM";
            this.ledCtcModeATM.On = true;
            this.ledCtcModeATM.Size = new System.Drawing.Size(23, 15);
            this.ledCtcModeATM.TabIndex = 926;
            this.ledCtcModeATM.Text = "ledBulb12";
            // 
            // ledBulb5
            // 
            this.ledBulb5.Color = System.Drawing.Color.Red;
            this.ledBulb5.DarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ledBulb5.DarkDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ledBulb5.Location = new System.Drawing.Point(450, 19);
            this.ledBulb5.Margin = new System.Windows.Forms.Padding(2);
            this.ledBulb5.Name = "ledBulb5";
            this.ledBulb5.On = true;
            this.ledBulb5.Size = new System.Drawing.Size(23, 15);
            this.ledBulb5.TabIndex = 936;
            this.ledBulb5.Text = "ledBulb5";
            // 
            // ledBulb6
            // 
            this.ledBulb6.Color = System.Drawing.Color.Yellow;
            this.ledBulb6.DarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(0)))));
            this.ledBulb6.DarkDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ledBulb6.Location = new System.Drawing.Point(350, 19);
            this.ledBulb6.Margin = new System.Windows.Forms.Padding(2);
            this.ledBulb6.Name = "ledBulb6";
            this.ledBulb6.On = true;
            this.ledBulb6.Size = new System.Drawing.Size(23, 15);
            this.ledBulb6.TabIndex = 934;
            this.ledBulb6.Text = "ledBulb6";
            // 
            // ledBulb3
            // 
            this.ledBulb3.Color = System.Drawing.Color.Blue;
            this.ledBulb3.DarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(85)))));
            this.ledBulb3.DarkDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ledBulb3.Location = new System.Drawing.Point(257, 19);
            this.ledBulb3.Margin = new System.Windows.Forms.Padding(2);
            this.ledBulb3.Name = "ledBulb3";
            this.ledBulb3.On = true;
            this.ledBulb3.Size = new System.Drawing.Size(23, 15);
            this.ledBulb3.TabIndex = 932;
            this.ledBulb3.Text = "ledBulb3";
            // 
            // ledBulb4
            // 
            this.ledBulb4.DarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.ledBulb4.DarkDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ledBulb4.Location = new System.Drawing.Point(171, 19);
            this.ledBulb4.Margin = new System.Windows.Forms.Padding(2);
            this.ledBulb4.Name = "ledBulb4";
            this.ledBulb4.On = true;
            this.ledBulb4.Size = new System.Drawing.Size(23, 15);
            this.ledBulb4.TabIndex = 930;
            this.ledBulb4.Text = "ledBulb4";
            // 
            // ledBulb2
            // 
            this.ledBulb2.Color = System.Drawing.Color.Aqua;
            this.ledBulb2.DarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.ledBulb2.DarkDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ledBulb2.Location = new System.Drawing.Point(78, 19);
            this.ledBulb2.Margin = new System.Windows.Forms.Padding(2);
            this.ledBulb2.Name = "ledBulb2";
            this.ledBulb2.On = true;
            this.ledBulb2.Size = new System.Drawing.Size(23, 15);
            this.ledBulb2.TabIndex = 928;
            this.ledBulb2.Text = "ledBulb2";
            // 
            // ledBulb1
            // 
            this.ledBulb1.Color = System.Drawing.Color.LightSteelBlue;
            this.ledBulb1.DarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(62)))), ((int)(((byte)(94)))));
            this.ledBulb1.DarkDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ledBulb1.Location = new System.Drawing.Point(5, 19);
            this.ledBulb1.Margin = new System.Windows.Forms.Padding(2);
            this.ledBulb1.Name = "ledBulb1";
            this.ledBulb1.On = true;
            this.ledBulb1.Size = new System.Drawing.Size(23, 15);
            this.ledBulb1.TabIndex = 926;
            this.ledBulb1.Text = "ledBulb1";
            // 
            // frmMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1264, 819);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panMainSubClient);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.a1Panel1);
            this.Controls.Add(this.a1Panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMainView";
            this.Text = "frmAuto";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMainView_Load);
            this.panel1.ResumeLayout(false);
            this.panSubMenuC1.ResumeLayout(false);
            this.panSubMenuC0.ResumeLayout(false);
            this.a1Panel3.ResumeLayout(false);
            this.a1Panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Owf.Controls.A1Panel a1Panel3;
        private System.Windows.Forms.Label lbPortName;
        private Owf.Controls.A1Panel a1Panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private Bulb.LedBulb ledBulb5;
        private System.Windows.Forms.Label label8;
        private Bulb.LedBulb ledBulb6;
        private System.Windows.Forms.Label label4;
        private Bulb.LedBulb ledBulb3;
        private System.Windows.Forms.Label label5;
        private Bulb.LedBulb ledBulb4;
        private System.Windows.Forms.Label label3;
        private Bulb.LedBulb ledBulb2;
        private System.Windows.Forms.Label label7;
        private Bulb.LedBulb ledBulb1;
        private System.Windows.Forms.Panel panMainSubClient;
        private Owf.Controls.A1Panel panSubMenuC1;
        private System.Windows.Forms.Label lbSubMenuC1;
        private Owf.Controls.A1Panel panSubMenuC0;
        private System.Windows.Forms.Label lbSubMenuC0;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private Bulb.LedBulb ledCtcModeVTM;
        private System.Windows.Forms.Label label14;
        private Bulb.LedBulb ledCtcModeATM;
        private System.Windows.Forms.Label lbCtcRunMode;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cbRunMode;
    }
}