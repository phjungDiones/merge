namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    partial class subManualVTM
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
            this.glassButton1 = new Glass.GlassButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnZDown = new Glass.GlassButton();
            this.btnArmRetract = new Glass.GlassButton();
            this.btnArmExt = new Glass.GlassButton();
            this.btnUpZ = new Glass.GlassButton();
            this.btnStop = new Glass.GlassButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnServoOff = new Glass.GlassButton();
            this.btnServoOn = new Glass.GlassButton();
            this.btnReset = new Glass.GlassButton();
            this.btnInit = new Glass.GlassButton();
            this.btnPutinto = new Glass.GlassButton();
            this.btnGetfrom = new Glass.GlassButton();
            this.btnGoto = new Glass.GlassButton();
            this.cbArm = new System.Windows.Forms.ComboBox();
            this.cbUpdown = new System.Windows.Forms.ComboBox();
            this.cbExRe = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.a1Panel1 = new Owf.Controls.A1Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbReady = new System.Windows.Forms.Label();
            this.lbWaferB = new System.Windows.Forms.Label();
            this.lbWaferA = new System.Windows.Forms.Label();
            this.lbError = new System.Windows.Forms.Label();
            this.lbMoving = new System.Windows.Forms.Label();
            this.a1Panel2 = new Owf.Controls.A1Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.tmrStatus = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnGateClose = new Glass.GlassButton();
            this.btnGateOpen = new Glass.GlassButton();
            this.cbStage = new System.Windows.Forms.ComboBox();
            this.cbSlot = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.a1Panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.a1Panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // glassButton1
            // 
            this.glassButton1.BackColor = System.Drawing.Color.DimGray;
            this.glassButton1.FadeOnFocus = true;
            this.glassButton1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton1.GlowColor = System.Drawing.Color.White;
            this.glassButton1.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton1.Location = new System.Drawing.Point(74, 594);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton1.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton1.Size = new System.Drawing.Size(50, 49);
            this.glassButton1.TabIndex = 83;
            this.glassButton1.Text = "PING";
            this.glassButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton1.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(14, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 28);
            this.label5.TabIndex = 82;
            this.label5.Text = "EXTEND / RETRACT";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(14, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 28);
            this.label4.TabIndex = 81;
            this.label4.Text = "UP / DOWN";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnZDown
            // 
            this.btnZDown.BackColor = System.Drawing.Color.DimGray;
            this.btnZDown.FadeOnFocus = true;
            this.btnZDown.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnZDown.GlowColor = System.Drawing.Color.White;
            this.btnZDown.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnZDown.Location = new System.Drawing.Point(144, 452);
            this.btnZDown.Name = "btnZDown";
            this.btnZDown.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnZDown.ShineColor = System.Drawing.Color.DarkGray;
            this.btnZDown.Size = new System.Drawing.Size(121, 49);
            this.btnZDown.TabIndex = 80;
            this.btnZDown.Text = "Z AXIS DOWN";
            this.btnZDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnZDown.Click += new System.EventHandler(this.btnZDown_Click);
            // 
            // btnArmRetract
            // 
            this.btnArmRetract.BackColor = System.Drawing.Color.DimGray;
            this.btnArmRetract.FadeOnFocus = true;
            this.btnArmRetract.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnArmRetract.GlowColor = System.Drawing.Color.White;
            this.btnArmRetract.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnArmRetract.Location = new System.Drawing.Point(144, 507);
            this.btnArmRetract.Name = "btnArmRetract";
            this.btnArmRetract.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnArmRetract.ShineColor = System.Drawing.Color.DarkGray;
            this.btnArmRetract.Size = new System.Drawing.Size(121, 49);
            this.btnArmRetract.TabIndex = 79;
            this.btnArmRetract.Text = "ARM RETRACT";
            this.btnArmRetract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnArmRetract.Click += new System.EventHandler(this.btnArmRetract_Click);
            // 
            // btnArmExt
            // 
            this.btnArmExt.BackColor = System.Drawing.Color.DimGray;
            this.btnArmExt.FadeOnFocus = true;
            this.btnArmExt.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnArmExt.GlowColor = System.Drawing.Color.White;
            this.btnArmExt.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnArmExt.Location = new System.Drawing.Point(17, 507);
            this.btnArmExt.Name = "btnArmExt";
            this.btnArmExt.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnArmExt.ShineColor = System.Drawing.Color.DarkGray;
            this.btnArmExt.Size = new System.Drawing.Size(121, 49);
            this.btnArmExt.TabIndex = 78;
            this.btnArmExt.Text = "ARM EXTEND";
            this.btnArmExt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnArmExt.Click += new System.EventHandler(this.btnArmExt_Click);
            // 
            // btnUpZ
            // 
            this.btnUpZ.BackColor = System.Drawing.Color.DimGray;
            this.btnUpZ.FadeOnFocus = true;
            this.btnUpZ.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUpZ.GlowColor = System.Drawing.Color.White;
            this.btnUpZ.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnUpZ.Location = new System.Drawing.Point(17, 452);
            this.btnUpZ.Name = "btnUpZ";
            this.btnUpZ.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnUpZ.ShineColor = System.Drawing.Color.DarkGray;
            this.btnUpZ.Size = new System.Drawing.Size(121, 49);
            this.btnUpZ.TabIndex = 77;
            this.btnUpZ.Text = "Z AXIS UP";
            this.btnUpZ.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpZ.Click += new System.EventHandler(this.btnUpZ_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.DimGray;
            this.btnStop.FadeOnFocus = true;
            this.btnStop.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStop.GlowColor = System.Drawing.Color.White;
            this.btnStop.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnStop.Location = new System.Drawing.Point(144, 342);
            this.btnStop.Name = "btnStop";
            this.btnStop.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnStop.ShineColor = System.Drawing.Color.DarkGray;
            this.btnStop.Size = new System.Drawing.Size(121, 49);
            this.btnStop.TabIndex = 76;
            this.btnStop.Text = "STOP";
            this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 28);
            this.label3.TabIndex = 75;
            this.label3.Text = "ARM";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 28);
            this.label2.TabIndex = 74;
            this.label2.Text = "SLOT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 28);
            this.label1.TabIndex = 66;
            this.label1.Text = "STAGE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnServoOff
            // 
            this.btnServoOff.BackColor = System.Drawing.Color.DimGray;
            this.btnServoOff.FadeOnFocus = true;
            this.btnServoOff.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnServoOff.GlowColor = System.Drawing.Color.White;
            this.btnServoOff.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnServoOff.Location = new System.Drawing.Point(144, 287);
            this.btnServoOff.Name = "btnServoOff";
            this.btnServoOff.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnServoOff.ShineColor = System.Drawing.Color.DarkGray;
            this.btnServoOff.Size = new System.Drawing.Size(121, 49);
            this.btnServoOff.TabIndex = 73;
            this.btnServoOff.Text = "SERVO OFF";
            this.btnServoOff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnServoOff.Click += new System.EventHandler(this.btnServoOff_Click);
            // 
            // btnServoOn
            // 
            this.btnServoOn.BackColor = System.Drawing.Color.DimGray;
            this.btnServoOn.FadeOnFocus = true;
            this.btnServoOn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnServoOn.GlowColor = System.Drawing.Color.White;
            this.btnServoOn.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnServoOn.Location = new System.Drawing.Point(17, 287);
            this.btnServoOn.Name = "btnServoOn";
            this.btnServoOn.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnServoOn.ShineColor = System.Drawing.Color.DarkGray;
            this.btnServoOn.Size = new System.Drawing.Size(121, 49);
            this.btnServoOn.TabIndex = 72;
            this.btnServoOn.Text = "SERVO ON";
            this.btnServoOn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnServoOn.Click += new System.EventHandler(this.btnServoOn_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.DimGray;
            this.btnReset.FadeOnFocus = true;
            this.btnReset.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnReset.GlowColor = System.Drawing.Color.White;
            this.btnReset.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnReset.Location = new System.Drawing.Point(144, 232);
            this.btnReset.Name = "btnReset";
            this.btnReset.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnReset.ShineColor = System.Drawing.Color.DarkGray;
            this.btnReset.Size = new System.Drawing.Size(121, 49);
            this.btnReset.TabIndex = 71;
            this.btnReset.Text = "RESET";
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnInit
            // 
            this.btnInit.BackColor = System.Drawing.Color.DimGray;
            this.btnInit.FadeOnFocus = true;
            this.btnInit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInit.GlowColor = System.Drawing.Color.White;
            this.btnInit.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnInit.Location = new System.Drawing.Point(17, 232);
            this.btnInit.Name = "btnInit";
            this.btnInit.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnInit.ShineColor = System.Drawing.Color.DarkGray;
            this.btnInit.Size = new System.Drawing.Size(121, 49);
            this.btnInit.TabIndex = 70;
            this.btnInit.Text = "INIT";
            this.btnInit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnPutinto
            // 
            this.btnPutinto.BackColor = System.Drawing.Color.DimGray;
            this.btnPutinto.FadeOnFocus = true;
            this.btnPutinto.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPutinto.GlowColor = System.Drawing.Color.White;
            this.btnPutinto.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnPutinto.Location = new System.Drawing.Point(144, 397);
            this.btnPutinto.Name = "btnPutinto";
            this.btnPutinto.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnPutinto.ShineColor = System.Drawing.Color.DarkGray;
            this.btnPutinto.Size = new System.Drawing.Size(121, 49);
            this.btnPutinto.TabIndex = 69;
            this.btnPutinto.Text = "PLACE";
            this.btnPutinto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPutinto.Click += new System.EventHandler(this.btnPutinto_Click);
            // 
            // btnGetfrom
            // 
            this.btnGetfrom.BackColor = System.Drawing.Color.DimGray;
            this.btnGetfrom.FadeOnFocus = true;
            this.btnGetfrom.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGetfrom.GlowColor = System.Drawing.Color.White;
            this.btnGetfrom.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnGetfrom.Location = new System.Drawing.Point(17, 397);
            this.btnGetfrom.Name = "btnGetfrom";
            this.btnGetfrom.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnGetfrom.ShineColor = System.Drawing.Color.DarkGray;
            this.btnGetfrom.Size = new System.Drawing.Size(121, 49);
            this.btnGetfrom.TabIndex = 68;
            this.btnGetfrom.Text = "PICK UP";
            this.btnGetfrom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGetfrom.Click += new System.EventHandler(this.btnGetfrom_Click);
            // 
            // btnGoto
            // 
            this.btnGoto.BackColor = System.Drawing.Color.DimGray;
            this.btnGoto.FadeOnFocus = true;
            this.btnGoto.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGoto.GlowColor = System.Drawing.Color.White;
            this.btnGoto.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnGoto.Location = new System.Drawing.Point(17, 342);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnGoto.ShineColor = System.Drawing.Color.DarkGray;
            this.btnGoto.Size = new System.Drawing.Size(121, 49);
            this.btnGoto.TabIndex = 67;
            this.btnGoto.Text = "GO TO";
            this.btnGoto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // cbArm
            // 
            this.cbArm.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbArm.FormattingEnabled = true;
            this.cbArm.Items.AddRange(new object[] {
            "LOWER",
            "UPPER"});
            this.cbArm.Location = new System.Drawing.Point(157, 119);
            this.cbArm.Name = "cbArm";
            this.cbArm.Size = new System.Drawing.Size(131, 23);
            this.cbArm.TabIndex = 86;
            // 
            // cbUpdown
            // 
            this.cbUpdown.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbUpdown.FormattingEnabled = true;
            this.cbUpdown.Items.AddRange(new object[] {
            "UP",
            "DOWN"});
            this.cbUpdown.Location = new System.Drawing.Point(157, 151);
            this.cbUpdown.Name = "cbUpdown";
            this.cbUpdown.Size = new System.Drawing.Size(131, 23);
            this.cbUpdown.TabIndex = 87;
            // 
            // cbExRe
            // 
            this.cbExRe.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbExRe.FormattingEnabled = true;
            this.cbExRe.Items.AddRange(new object[] {
            "EXTEND",
            "RETRACT"});
            this.cbExRe.Location = new System.Drawing.Point(157, 182);
            this.cbExRe.Name = "cbExRe";
            this.cbExRe.Size = new System.Drawing.Size(131, 23);
            this.cbExRe.TabIndex = 88;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbSlot);
            this.panel1.Controls.Add(this.cbStage);
            this.panel1.Controls.Add(this.a1Panel1);
            this.panel1.Controls.Add(this.cbExRe);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbUpdown);
            this.panel1.Controls.Add(this.btnGoto);
            this.panel1.Controls.Add(this.cbArm);
            this.panel1.Controls.Add(this.btnGetfrom);
            this.panel1.Controls.Add(this.btnPutinto);
            this.panel1.Controls.Add(this.btnInit);
            this.panel1.Controls.Add(this.glassButton1);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnServoOn);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnServoOff);
            this.panel1.Controls.Add(this.btnZDown);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnArmRetract);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnArmExt);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnUpZ);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(299, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(317, 724);
            this.panel1.TabIndex = 976;
            // 
            // a1Panel1
            // 
            this.a1Panel1.BackColor = System.Drawing.Color.Black;
            this.a1Panel1.BorderColor = System.Drawing.Color.DimGray;
            this.a1Panel1.Controls.Add(this.label6);
            this.a1Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.a1Panel1.GradientEndColor = System.Drawing.Color.White;
            this.a1Panel1.GradientStartColor = System.Drawing.Color.Silver;
            this.a1Panel1.Image = null;
            this.a1Panel1.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel1.Location = new System.Drawing.Point(0, 0);
            this.a1Panel1.Name = "a1Panel1";
            this.a1Panel1.ShadowOffSet = 2;
            this.a1Panel1.Size = new System.Drawing.Size(315, 31);
            this.a1Panel1.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(14, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "ACTION";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbReady);
            this.panel2.Controls.Add(this.lbWaferB);
            this.panel2.Controls.Add(this.lbWaferA);
            this.panel2.Controls.Add(this.lbError);
            this.panel2.Controls.Add(this.lbMoving);
            this.panel2.Controls.Add(this.a1Panel2);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(291, 391);
            this.panel2.TabIndex = 977;
            // 
            // lbReady
            // 
            this.lbReady.BackColor = System.Drawing.Color.Gray;
            this.lbReady.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReady.ForeColor = System.Drawing.Color.White;
            this.lbReady.Location = new System.Drawing.Point(27, 91);
            this.lbReady.Name = "lbReady";
            this.lbReady.Size = new System.Drawing.Size(150, 35);
            this.lbReady.TabIndex = 109;
            this.lbReady.Text = "Ready";
            this.lbReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWaferB
            // 
            this.lbWaferB.BackColor = System.Drawing.Color.Gray;
            this.lbWaferB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaferB.ForeColor = System.Drawing.Color.White;
            this.lbWaferB.Location = new System.Drawing.Point(27, 219);
            this.lbWaferB.Name = "lbWaferB";
            this.lbWaferB.Size = new System.Drawing.Size(150, 35);
            this.lbWaferB.TabIndex = 106;
            this.lbWaferB.Text = "Wafer B";
            this.lbWaferB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWaferA
            // 
            this.lbWaferA.BackColor = System.Drawing.Color.Gray;
            this.lbWaferA.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaferA.ForeColor = System.Drawing.Color.White;
            this.lbWaferA.Location = new System.Drawing.Point(27, 182);
            this.lbWaferA.Name = "lbWaferA";
            this.lbWaferA.Size = new System.Drawing.Size(150, 35);
            this.lbWaferA.TabIndex = 105;
            this.lbWaferA.Text = "Wafer A";
            this.lbWaferA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbError
            // 
            this.lbError.BackColor = System.Drawing.Color.Gray;
            this.lbError.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbError.ForeColor = System.Drawing.Color.White;
            this.lbError.Location = new System.Drawing.Point(27, 127);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(150, 35);
            this.lbError.TabIndex = 104;
            this.lbError.Text = "Error";
            this.lbError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMoving
            // 
            this.lbMoving.BackColor = System.Drawing.Color.Gray;
            this.lbMoving.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMoving.ForeColor = System.Drawing.Color.White;
            this.lbMoving.Location = new System.Drawing.Point(27, 54);
            this.lbMoving.Name = "lbMoving";
            this.lbMoving.Size = new System.Drawing.Size(150, 35);
            this.lbMoving.TabIndex = 103;
            this.lbMoving.Text = "Moving";
            this.lbMoving.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // a1Panel2
            // 
            this.a1Panel2.BackColor = System.Drawing.Color.Black;
            this.a1Panel2.BorderColor = System.Drawing.Color.DimGray;
            this.a1Panel2.Controls.Add(this.label7);
            this.a1Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.a1Panel2.GradientEndColor = System.Drawing.Color.White;
            this.a1Panel2.GradientStartColor = System.Drawing.Color.Silver;
            this.a1Panel2.Image = null;
            this.a1Panel2.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel2.Location = new System.Drawing.Point(0, 0);
            this.a1Panel2.Name = "a1Panel2";
            this.a1Panel2.ShadowOffSet = 2;
            this.a1Panel2.Size = new System.Drawing.Size(289, 31);
            this.a1Panel2.TabIndex = 38;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(14, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "STATUS";
            // 
            // tmrStatus
            // 
            this.tmrStatus.Interval = 500;
            this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnGateClose);
            this.panel3.Controls.Add(this.btnGateOpen);
            this.panel3.ForeColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(3, 398);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(291, 328);
            this.panel3.TabIndex = 978;
            this.panel3.Visible = false;
            // 
            // btnGateClose
            // 
            this.btnGateClose.BackColor = System.Drawing.Color.DimGray;
            this.btnGateClose.FadeOnFocus = true;
            this.btnGateClose.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGateClose.GlowColor = System.Drawing.Color.White;
            this.btnGateClose.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnGateClose.Location = new System.Drawing.Point(149, 102);
            this.btnGateClose.Name = "btnGateClose";
            this.btnGateClose.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnGateClose.ShineColor = System.Drawing.Color.DarkGray;
            this.btnGateClose.Size = new System.Drawing.Size(121, 49);
            this.btnGateClose.TabIndex = 81;
            this.btnGateClose.Text = "BD Gate Close";
            this.btnGateClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGateClose.Click += new System.EventHandler(this.btnGateClose_Click);
            // 
            // btnGateOpen
            // 
            this.btnGateOpen.BackColor = System.Drawing.Color.DimGray;
            this.btnGateOpen.FadeOnFocus = true;
            this.btnGateOpen.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGateOpen.GlowColor = System.Drawing.Color.White;
            this.btnGateOpen.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnGateOpen.Location = new System.Drawing.Point(22, 102);
            this.btnGateOpen.Name = "btnGateOpen";
            this.btnGateOpen.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnGateOpen.ShineColor = System.Drawing.Color.DarkGray;
            this.btnGateOpen.Size = new System.Drawing.Size(121, 49);
            this.btnGateOpen.TabIndex = 80;
            this.btnGateOpen.Text = "BD Gate Open";
            this.btnGateOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGateOpen.Click += new System.EventHandler(this.btnGateOpen_Click);
            // 
            // cbStage
            // 
            this.cbStage.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbStage.FormattingEnabled = true;
            this.cbStage.Location = new System.Drawing.Point(157, 57);
            this.cbStage.Name = "cbStage";
            this.cbStage.Size = new System.Drawing.Size(131, 23);
            this.cbStage.TabIndex = 89;
            // 
            // cbSlot
            // 
            this.cbSlot.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbSlot.FormattingEnabled = true;
            this.cbSlot.Location = new System.Drawing.Point(157, 88);
            this.cbSlot.Name = "cbSlot";
            this.cbSlot.Size = new System.Drawing.Size(131, 23);
            this.cbSlot.TabIndex = 103;
            // 
            // subManualVTM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "subManualVTM";
            this.Size = new System.Drawing.Size(619, 731);
            this.Load += new System.EventHandler(this.subManualVTM_Load);
            this.panel1.ResumeLayout(false);
            this.a1Panel1.ResumeLayout(false);
            this.a1Panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.a1Panel2.ResumeLayout(false);
            this.a1Panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Glass.GlassButton glassButton1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Glass.GlassButton btnZDown;
        private Glass.GlassButton btnArmRetract;
        private Glass.GlassButton btnArmExt;
        private Glass.GlassButton btnUpZ;
        private Glass.GlassButton btnStop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Glass.GlassButton btnServoOff;
        private Glass.GlassButton btnServoOn;
        private Glass.GlassButton btnReset;
        private Glass.GlassButton btnInit;
        private Glass.GlassButton btnPutinto;
        private Glass.GlassButton btnGetfrom;
        private Glass.GlassButton btnGoto;
        private System.Windows.Forms.ComboBox cbArm;
        private System.Windows.Forms.ComboBox cbUpdown;
        private System.Windows.Forms.ComboBox cbExRe;
        private System.Windows.Forms.Panel panel1;
        private Owf.Controls.A1Panel a1Panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private Owf.Controls.A1Panel a1Panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbMoving;
        private System.Windows.Forms.Timer tmrStatus;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.Label lbWaferB;
        private System.Windows.Forms.Label lbWaferA;
        private System.Windows.Forms.Label lbReady;
        private System.Windows.Forms.Panel panel3;
        private Glass.GlassButton btnGateClose;
        private Glass.GlassButton btnGateOpen;
        private System.Windows.Forms.ComboBox cbStage;
        private System.Windows.Forms.ComboBox cbSlot;
    }
}
