namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    partial class subManualTM
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
            this.btnStop = new Glass.GlassButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpVacOff = new Glass.GlassButton();
            this.btnUpVacOn = new Glass.GlassButton();
            this.btnLowVacOff = new Glass.GlassButton();
            this.btnLowVacOn = new Glass.GlassButton();
            this.btnServoOff = new Glass.GlassButton();
            this.btnServoOn = new Glass.GlassButton();
            this.btnReset = new Glass.GlassButton();
            this.btnInit = new Glass.GlassButton();
            this.btnPutinto = new Glass.GlassButton();
            this.btnGetfrom = new Glass.GlassButton();
            this.btnFGReady = new Glass.GlassButton();
            this.cbArm = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.a1Panel1 = new Owf.Controls.A1Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lbUpHand = new System.Windows.Forms.Label();
            this.lbRun = new System.Windows.Forms.Label();
            this.lbLowHand = new System.Windows.Forms.Label();
            this.lbServo = new System.Windows.Forms.Label();
            this.lbArmFold = new System.Windows.Forms.Label();
            this.lbAbnormal = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbReadSpeed = new System.Windows.Forms.TextBox();
            this.tbSpeed = new System.Windows.Forms.TextBox();
            this.btnChangeSpeed = new Glass.GlassButton();
            this.lbSpeed = new System.Windows.Forms.Label();
            this.a1Panel2 = new Owf.Controls.A1Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tmrStatus = new System.Windows.Forms.Timer(this.components);
            this.cbStage = new System.Windows.Forms.ComboBox();
            this.cbSlot = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.a1Panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.a1Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.DimGray;
            this.btnStop.FadeOnFocus = true;
            this.btnStop.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStop.GlowColor = System.Drawing.Color.White;
            this.btnStop.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnStop.Location = new System.Drawing.Point(165, 422);
            this.btnStop.Name = "btnStop";
            this.btnStop.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnStop.ShineColor = System.Drawing.Color.DarkGray;
            this.btnStop.Size = new System.Drawing.Size(121, 49);
            this.btnStop.TabIndex = 71;
            this.btnStop.Text = "STOP";
            this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(19, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 28);
            this.label3.TabIndex = 70;
            this.label3.Text = "ARM";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(19, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 28);
            this.label2.TabIndex = 69;
            this.label2.Text = "SLOT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 28);
            this.label1.TabIndex = 57;
            this.label1.Text = "STAGE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUpVacOff
            // 
            this.btnUpVacOff.BackColor = System.Drawing.Color.DimGray;
            this.btnUpVacOff.FadeOnFocus = true;
            this.btnUpVacOff.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUpVacOff.GlowColor = System.Drawing.Color.White;
            this.btnUpVacOff.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnUpVacOff.Location = new System.Drawing.Point(165, 587);
            this.btnUpVacOff.Name = "btnUpVacOff";
            this.btnUpVacOff.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnUpVacOff.ShineColor = System.Drawing.Color.DarkGray;
            this.btnUpVacOff.Size = new System.Drawing.Size(121, 49);
            this.btnUpVacOff.TabIndex = 68;
            this.btnUpVacOff.Text = "UP VAC OFF";
            this.btnUpVacOff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpVacOff.Click += new System.EventHandler(this.btnUpVacOff_Click);
            // 
            // btnUpVacOn
            // 
            this.btnUpVacOn.BackColor = System.Drawing.Color.DimGray;
            this.btnUpVacOn.FadeOnFocus = true;
            this.btnUpVacOn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUpVacOn.GlowColor = System.Drawing.Color.White;
            this.btnUpVacOn.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnUpVacOn.Location = new System.Drawing.Point(38, 587);
            this.btnUpVacOn.Name = "btnUpVacOn";
            this.btnUpVacOn.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnUpVacOn.ShineColor = System.Drawing.Color.DarkGray;
            this.btnUpVacOn.Size = new System.Drawing.Size(121, 49);
            this.btnUpVacOn.TabIndex = 67;
            this.btnUpVacOn.Text = "UP VAC ON";
            this.btnUpVacOn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpVacOn.Click += new System.EventHandler(this.btnUpVacOn_Click);
            // 
            // btnLowVacOff
            // 
            this.btnLowVacOff.BackColor = System.Drawing.Color.DimGray;
            this.btnLowVacOff.FadeOnFocus = true;
            this.btnLowVacOff.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLowVacOff.GlowColor = System.Drawing.Color.White;
            this.btnLowVacOff.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnLowVacOff.Location = new System.Drawing.Point(165, 532);
            this.btnLowVacOff.Name = "btnLowVacOff";
            this.btnLowVacOff.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnLowVacOff.ShineColor = System.Drawing.Color.DarkGray;
            this.btnLowVacOff.Size = new System.Drawing.Size(121, 49);
            this.btnLowVacOff.TabIndex = 66;
            this.btnLowVacOff.Text = "LOW VAC OFF";
            this.btnLowVacOff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLowVacOff.Click += new System.EventHandler(this.btnLowVacOff_Click);
            // 
            // btnLowVacOn
            // 
            this.btnLowVacOn.BackColor = System.Drawing.Color.DimGray;
            this.btnLowVacOn.FadeOnFocus = true;
            this.btnLowVacOn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLowVacOn.GlowColor = System.Drawing.Color.White;
            this.btnLowVacOn.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnLowVacOn.Location = new System.Drawing.Point(38, 532);
            this.btnLowVacOn.Name = "btnLowVacOn";
            this.btnLowVacOn.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnLowVacOn.ShineColor = System.Drawing.Color.DarkGray;
            this.btnLowVacOn.Size = new System.Drawing.Size(121, 49);
            this.btnLowVacOn.TabIndex = 65;
            this.btnLowVacOn.Text = "LOW VAC ON";
            this.btnLowVacOn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLowVacOn.Click += new System.EventHandler(this.btnLowVacOn_Click);
            // 
            // btnServoOff
            // 
            this.btnServoOff.BackColor = System.Drawing.Color.DimGray;
            this.btnServoOff.FadeOnFocus = true;
            this.btnServoOff.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnServoOff.GlowColor = System.Drawing.Color.White;
            this.btnServoOff.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnServoOff.Location = new System.Drawing.Point(165, 367);
            this.btnServoOff.Name = "btnServoOff";
            this.btnServoOff.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnServoOff.ShineColor = System.Drawing.Color.DarkGray;
            this.btnServoOff.Size = new System.Drawing.Size(121, 49);
            this.btnServoOff.TabIndex = 64;
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
            this.btnServoOn.Location = new System.Drawing.Point(38, 367);
            this.btnServoOn.Name = "btnServoOn";
            this.btnServoOn.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnServoOn.ShineColor = System.Drawing.Color.DarkGray;
            this.btnServoOn.Size = new System.Drawing.Size(121, 49);
            this.btnServoOn.TabIndex = 63;
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
            this.btnReset.Location = new System.Drawing.Point(165, 312);
            this.btnReset.Name = "btnReset";
            this.btnReset.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnReset.ShineColor = System.Drawing.Color.DarkGray;
            this.btnReset.Size = new System.Drawing.Size(121, 49);
            this.btnReset.TabIndex = 62;
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
            this.btnInit.Location = new System.Drawing.Point(38, 312);
            this.btnInit.Name = "btnInit";
            this.btnInit.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnInit.ShineColor = System.Drawing.Color.DarkGray;
            this.btnInit.Size = new System.Drawing.Size(121, 49);
            this.btnInit.TabIndex = 61;
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
            this.btnPutinto.Location = new System.Drawing.Point(165, 477);
            this.btnPutinto.Name = "btnPutinto";
            this.btnPutinto.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnPutinto.ShineColor = System.Drawing.Color.DarkGray;
            this.btnPutinto.Size = new System.Drawing.Size(121, 49);
            this.btnPutinto.TabIndex = 60;
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
            this.btnGetfrom.Location = new System.Drawing.Point(38, 477);
            this.btnGetfrom.Name = "btnGetfrom";
            this.btnGetfrom.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnGetfrom.ShineColor = System.Drawing.Color.DarkGray;
            this.btnGetfrom.Size = new System.Drawing.Size(121, 49);
            this.btnGetfrom.TabIndex = 59;
            this.btnGetfrom.Text = "PICK UP";
            this.btnGetfrom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGetfrom.Click += new System.EventHandler(this.btnGetfrom_Click);
            // 
            // btnFGReady
            // 
            this.btnFGReady.BackColor = System.Drawing.Color.DimGray;
            this.btnFGReady.FadeOnFocus = true;
            this.btnFGReady.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFGReady.GlowColor = System.Drawing.Color.White;
            this.btnFGReady.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnFGReady.Location = new System.Drawing.Point(38, 422);
            this.btnFGReady.Name = "btnFGReady";
            this.btnFGReady.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnFGReady.ShineColor = System.Drawing.Color.DarkGray;
            this.btnFGReady.Size = new System.Drawing.Size(121, 49);
            this.btnFGReady.TabIndex = 58;
            this.btnFGReady.Text = "READY && ARM FOLD";
            this.btnFGReady.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFGReady.Click += new System.EventHandler(this.btnFGReady_Click);
            // 
            // cbArm
            // 
            this.cbArm.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbArm.FormattingEnabled = true;
            this.cbArm.ItemHeight = 15;
            this.cbArm.Items.AddRange(new object[] {
            "LOWER",
            "UPPER"});
            this.cbArm.Location = new System.Drawing.Point(164, 119);
            this.cbArm.Name = "cbArm";
            this.cbArm.Size = new System.Drawing.Size(131, 23);
            this.cbArm.TabIndex = 74;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.a1Panel1);
            this.panel1.Controls.Add(this.lbUpHand);
            this.panel1.Controls.Add(this.lbRun);
            this.panel1.Controls.Add(this.lbLowHand);
            this.panel1.Controls.Add(this.lbServo);
            this.panel1.Controls.Add(this.lbArmFold);
            this.panel1.Controls.Add(this.lbAbnormal);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(291, 724);
            this.panel1.TabIndex = 975;
            // 
            // a1Panel1
            // 
            this.a1Panel1.BackColor = System.Drawing.Color.Black;
            this.a1Panel1.BorderColor = System.Drawing.Color.DimGray;
            this.a1Panel1.Controls.Add(this.label4);
            this.a1Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.a1Panel1.GradientEndColor = System.Drawing.Color.White;
            this.a1Panel1.GradientStartColor = System.Drawing.Color.Silver;
            this.a1Panel1.Image = null;
            this.a1Panel1.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel1.Location = new System.Drawing.Point(0, 0);
            this.a1Panel1.Name = "a1Panel1";
            this.a1Panel1.ShadowOffSet = 2;
            this.a1Panel1.Size = new System.Drawing.Size(289, 31);
            this.a1Panel1.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(14, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "STATUS";
            // 
            // lbUpHand
            // 
            this.lbUpHand.BackColor = System.Drawing.Color.Gray;
            this.lbUpHand.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUpHand.ForeColor = System.Drawing.Color.White;
            this.lbUpHand.Location = new System.Drawing.Point(42, 240);
            this.lbUpHand.Name = "lbUpHand";
            this.lbUpHand.Size = new System.Drawing.Size(150, 35);
            this.lbUpHand.TabIndex = 102;
            this.lbUpHand.Text = "Up Vac";
            this.lbUpHand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbRun
            // 
            this.lbRun.BackColor = System.Drawing.Color.Gray;
            this.lbRun.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRun.ForeColor = System.Drawing.Color.White;
            this.lbRun.Location = new System.Drawing.Point(42, 55);
            this.lbRun.Name = "lbRun";
            this.lbRun.Size = new System.Drawing.Size(150, 35);
            this.lbRun.TabIndex = 100;
            this.lbRun.Text = "Run";
            this.lbRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLowHand
            // 
            this.lbLowHand.BackColor = System.Drawing.Color.Gray;
            this.lbLowHand.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLowHand.ForeColor = System.Drawing.Color.White;
            this.lbLowHand.Location = new System.Drawing.Point(42, 203);
            this.lbLowHand.Name = "lbLowHand";
            this.lbLowHand.Size = new System.Drawing.Size(150, 35);
            this.lbLowHand.TabIndex = 101;
            this.lbLowHand.Text = "Low Vac";
            this.lbLowHand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbServo
            // 
            this.lbServo.BackColor = System.Drawing.Color.Gray;
            this.lbServo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbServo.ForeColor = System.Drawing.Color.White;
            this.lbServo.Location = new System.Drawing.Point(42, 129);
            this.lbServo.Name = "lbServo";
            this.lbServo.Size = new System.Drawing.Size(150, 35);
            this.lbServo.TabIndex = 97;
            this.lbServo.Text = "Servo";
            this.lbServo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbArmFold
            // 
            this.lbArmFold.BackColor = System.Drawing.Color.Gray;
            this.lbArmFold.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbArmFold.ForeColor = System.Drawing.Color.White;
            this.lbArmFold.Location = new System.Drawing.Point(42, 166);
            this.lbArmFold.Name = "lbArmFold";
            this.lbArmFold.Size = new System.Drawing.Size(150, 35);
            this.lbArmFold.TabIndex = 99;
            this.lbArmFold.Text = "Arm Fold";
            this.lbArmFold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAbnormal
            // 
            this.lbAbnormal.BackColor = System.Drawing.Color.Gray;
            this.lbAbnormal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAbnormal.ForeColor = System.Drawing.Color.White;
            this.lbAbnormal.Location = new System.Drawing.Point(42, 92);
            this.lbAbnormal.Name = "lbAbnormal";
            this.lbAbnormal.Size = new System.Drawing.Size(150, 35);
            this.lbAbnormal.TabIndex = 98;
            this.lbAbnormal.Text = "Abnormal";
            this.lbAbnormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cbSlot);
            this.panel2.Controls.Add(this.cbStage);
            this.panel2.Controls.Add(this.tbReadSpeed);
            this.panel2.Controls.Add(this.tbSpeed);
            this.panel2.Controls.Add(this.btnChangeSpeed);
            this.panel2.Controls.Add(this.lbSpeed);
            this.panel2.Controls.Add(this.a1Panel2);
            this.panel2.Controls.Add(this.cbArm);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnStop);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnInit);
            this.panel2.Controls.Add(this.btnUpVacOff);
            this.panel2.Controls.Add(this.btnServoOn);
            this.panel2.Controls.Add(this.btnFGReady);
            this.panel2.Controls.Add(this.btnReset);
            this.panel2.Controls.Add(this.btnUpVacOn);
            this.panel2.Controls.Add(this.btnServoOff);
            this.panel2.Controls.Add(this.btnGetfrom);
            this.panel2.Controls.Add(this.btnLowVacOn);
            this.panel2.Controls.Add(this.btnLowVacOff);
            this.panel2.Controls.Add(this.btnPutinto);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(299, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(317, 724);
            this.panel2.TabIndex = 976;
            // 
            // tbReadSpeed
            // 
            this.tbReadSpeed.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tbReadSpeed.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbReadSpeed.Location = new System.Drawing.Point(164, 178);
            this.tbReadSpeed.Name = "tbReadSpeed";
            this.tbReadSpeed.ReadOnly = true;
            this.tbReadSpeed.Size = new System.Drawing.Size(131, 25);
            this.tbReadSpeed.TabIndex = 100;
            this.tbReadSpeed.Text = "0";
            this.tbReadSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbSpeed
            // 
            this.tbSpeed.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbSpeed.Location = new System.Drawing.Point(164, 148);
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(131, 25);
            this.tbSpeed.TabIndex = 99;
            this.tbSpeed.Text = "0";
            this.tbSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnChangeSpeed
            // 
            this.btnChangeSpeed.BackColor = System.Drawing.Color.DimGray;
            this.btnChangeSpeed.FadeOnFocus = true;
            this.btnChangeSpeed.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnChangeSpeed.GlowColor = System.Drawing.Color.White;
            this.btnChangeSpeed.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnChangeSpeed.Location = new System.Drawing.Point(163, 206);
            this.btnChangeSpeed.Name = "btnChangeSpeed";
            this.btnChangeSpeed.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnChangeSpeed.ShineColor = System.Drawing.Color.DarkGray;
            this.btnChangeSpeed.Size = new System.Drawing.Size(133, 46);
            this.btnChangeSpeed.TabIndex = 98;
            this.btnChangeSpeed.Text = "SET SPEED";
            this.btnChangeSpeed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnChangeSpeed.Click += new System.EventHandler(this.btnChangeSpeed_Click);
            // 
            // lbSpeed
            // 
            this.lbSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lbSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbSpeed.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbSpeed.ForeColor = System.Drawing.Color.White;
            this.lbSpeed.Location = new System.Drawing.Point(19, 148);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(140, 55);
            this.lbSpeed.TabIndex = 97;
            this.lbSpeed.Text = "SPEED";
            this.lbSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // a1Panel2
            // 
            this.a1Panel2.BackColor = System.Drawing.Color.Black;
            this.a1Panel2.BorderColor = System.Drawing.Color.DimGray;
            this.a1Panel2.Controls.Add(this.label5);
            this.a1Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.a1Panel2.GradientEndColor = System.Drawing.Color.White;
            this.a1Panel2.GradientStartColor = System.Drawing.Color.Silver;
            this.a1Panel2.Image = null;
            this.a1Panel2.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel2.Location = new System.Drawing.Point(0, 0);
            this.a1Panel2.Name = "a1Panel2";
            this.a1Panel2.ShadowOffSet = 2;
            this.a1Panel2.Size = new System.Drawing.Size(315, 31);
            this.a1Panel2.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(14, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "ACTION";
            // 
            // tmrStatus
            // 
            this.tmrStatus.Interval = 300;
            this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
            // 
            // cbStage
            // 
            this.cbStage.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbStage.FormattingEnabled = true;
            this.cbStage.Location = new System.Drawing.Point(165, 58);
            this.cbStage.Name = "cbStage";
            this.cbStage.Size = new System.Drawing.Size(131, 23);
            this.cbStage.TabIndex = 101;
            // 
            // cbSlot
            // 
            this.cbSlot.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbSlot.FormattingEnabled = true;
            this.cbSlot.Location = new System.Drawing.Point(165, 87);
            this.cbSlot.Name = "cbSlot";
            this.cbSlot.Size = new System.Drawing.Size(131, 23);
            this.cbSlot.TabIndex = 102;
            // 
            // subManualTM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "subManualTM";
            this.Size = new System.Drawing.Size(619, 731);
            this.Load += new System.EventHandler(this.subManualTM_Load);
            this.panel1.ResumeLayout(false);
            this.a1Panel1.ResumeLayout(false);
            this.a1Panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.a1Panel2.ResumeLayout(false);
            this.a1Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Glass.GlassButton btnStop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Glass.GlassButton btnUpVacOff;
        private Glass.GlassButton btnUpVacOn;
        private Glass.GlassButton btnLowVacOff;
        private Glass.GlassButton btnLowVacOn;
        private Glass.GlassButton btnServoOff;
        private Glass.GlassButton btnServoOn;
        private Glass.GlassButton btnReset;
        private Glass.GlassButton btnInit;
        private Glass.GlassButton btnPutinto;
        private Glass.GlassButton btnGetfrom;
        private Glass.GlassButton btnFGReady;
        private System.Windows.Forms.ComboBox cbArm;
        private System.Windows.Forms.Panel panel1;
        private Owf.Controls.A1Panel a1Panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbUpHand;
        private System.Windows.Forms.Label lbRun;
        private System.Windows.Forms.Label lbLowHand;
        private System.Windows.Forms.Label lbServo;
        private System.Windows.Forms.Label lbArmFold;
        private System.Windows.Forms.Label lbAbnormal;
        private System.Windows.Forms.Panel panel2;
        private Owf.Controls.A1Panel a1Panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbReadSpeed;
        private System.Windows.Forms.TextBox tbSpeed;
        private Glass.GlassButton btnChangeSpeed;
        private System.Windows.Forms.Label lbSpeed;
        private System.Windows.Forms.Timer tmrStatus;
        private System.Windows.Forms.ComboBox cbStage;
        private System.Windows.Forms.ComboBox cbSlot;
    }
}
