namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    partial class subManualETC
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
            this.btnGateClose = new Glass.GlassButton();
            this.btnGateOpen = new Glass.GlassButton();
            this.tmrStatus = new System.Windows.Forms.Timer(this.components);
            this.glassButton1 = new Glass.GlassButton();
            this.glassButton2 = new Glass.GlassButton();
            this.glassButton3 = new Glass.GlassButton();
            this.glassButton4 = new Glass.GlassButton();
            this.lbBDOpen = new System.Windows.Forms.Label();
            this.lbBDClose = new System.Windows.Forms.Label();
            this.lbTMClose = new System.Windows.Forms.Label();
            this.lbTMOpen = new System.Windows.Forms.Label();
            this.lbLLClose = new System.Windows.Forms.Label();
            this.lbLLOpen = new System.Windows.Forms.Label();
            this.btnVtmArmExt1 = new Glass.GlassButton();
            this.glassButton5 = new Glass.GlassButton();
            this.glassButton6 = new Glass.GlassButton();
            this.glassButton7 = new Glass.GlassButton();
            this.glassButton8 = new Glass.GlassButton();
            this.glassButton9 = new Glass.GlassButton();
            this.glassButton10 = new Glass.GlassButton();
            this.glassButton11 = new Glass.GlassButton();
            this.btnVtmPump = new Glass.GlassButton();
            this.btnVtmVent = new Glass.GlassButton();
            this.btnLoadlockVent = new Glass.GlassButton();
            this.btnLoadlockPump = new Glass.GlassButton();
            this.SuspendLayout();
            // 
            // btnGateClose
            // 
            this.btnGateClose.BackColor = System.Drawing.Color.DimGray;
            this.btnGateClose.Enabled = false;
            this.btnGateClose.FadeOnFocus = true;
            this.btnGateClose.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGateClose.GlowColor = System.Drawing.Color.White;
            this.btnGateClose.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnGateClose.Location = new System.Drawing.Point(444, 44);
            this.btnGateClose.Name = "btnGateClose";
            this.btnGateClose.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnGateClose.ShineColor = System.Drawing.Color.DarkGray;
            this.btnGateClose.Size = new System.Drawing.Size(121, 49);
            this.btnGateClose.TabIndex = 83;
            this.btnGateClose.Text = "BD Gate Close";
            this.btnGateClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGateClose.Visible = false;
            this.btnGateClose.Click += new System.EventHandler(this.btnGateClose_Click);
            // 
            // btnGateOpen
            // 
            this.btnGateOpen.BackColor = System.Drawing.Color.DimGray;
            this.btnGateOpen.Enabled = false;
            this.btnGateOpen.FadeOnFocus = true;
            this.btnGateOpen.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGateOpen.GlowColor = System.Drawing.Color.White;
            this.btnGateOpen.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnGateOpen.Location = new System.Drawing.Point(317, 44);
            this.btnGateOpen.Name = "btnGateOpen";
            this.btnGateOpen.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnGateOpen.ShineColor = System.Drawing.Color.DarkGray;
            this.btnGateOpen.Size = new System.Drawing.Size(121, 49);
            this.btnGateOpen.TabIndex = 82;
            this.btnGateOpen.Text = "BD Gate Open";
            this.btnGateOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGateOpen.Visible = false;
            this.btnGateOpen.Click += new System.EventHandler(this.btnGateOpen_Click);
            // 
            // tmrStatus
            // 
            this.tmrStatus.Interval = 300;
            this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
            // 
            // glassButton1
            // 
            this.glassButton1.BackColor = System.Drawing.Color.DimGray;
            this.glassButton1.Enabled = false;
            this.glassButton1.FadeOnFocus = true;
            this.glassButton1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton1.GlowColor = System.Drawing.Color.White;
            this.glassButton1.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton1.Location = new System.Drawing.Point(444, 109);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton1.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton1.Size = new System.Drawing.Size(121, 49);
            this.glassButton1.TabIndex = 85;
            this.glassButton1.Text = "TM Gate Close";
            this.glassButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton1.Visible = false;
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // glassButton2
            // 
            this.glassButton2.BackColor = System.Drawing.Color.DimGray;
            this.glassButton2.Enabled = false;
            this.glassButton2.FadeOnFocus = true;
            this.glassButton2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton2.GlowColor = System.Drawing.Color.White;
            this.glassButton2.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton2.Location = new System.Drawing.Point(317, 109);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton2.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton2.Size = new System.Drawing.Size(121, 49);
            this.glassButton2.TabIndex = 84;
            this.glassButton2.Text = "TM Gate Open";
            this.glassButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton2.Visible = false;
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            // 
            // glassButton3
            // 
            this.glassButton3.BackColor = System.Drawing.Color.DimGray;
            this.glassButton3.Enabled = false;
            this.glassButton3.FadeOnFocus = true;
            this.glassButton3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton3.GlowColor = System.Drawing.Color.White;
            this.glassButton3.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton3.Location = new System.Drawing.Point(444, 173);
            this.glassButton3.Name = "glassButton3";
            this.glassButton3.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton3.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton3.Size = new System.Drawing.Size(121, 49);
            this.glassButton3.TabIndex = 87;
            this.glassButton3.Text = "LL Gate Close";
            this.glassButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton3.Visible = false;
            this.glassButton3.Click += new System.EventHandler(this.glassButton3_Click);
            // 
            // glassButton4
            // 
            this.glassButton4.BackColor = System.Drawing.Color.DimGray;
            this.glassButton4.Enabled = false;
            this.glassButton4.FadeOnFocus = true;
            this.glassButton4.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton4.GlowColor = System.Drawing.Color.White;
            this.glassButton4.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton4.Location = new System.Drawing.Point(317, 173);
            this.glassButton4.Name = "glassButton4";
            this.glassButton4.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton4.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton4.Size = new System.Drawing.Size(121, 49);
            this.glassButton4.TabIndex = 86;
            this.glassButton4.Text = "LL Gate Open";
            this.glassButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton4.Visible = false;
            this.glassButton4.Click += new System.EventHandler(this.glassButton4_Click);
            // 
            // lbBDOpen
            // 
            this.lbBDOpen.BackColor = System.Drawing.Color.Gray;
            this.lbBDOpen.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBDOpen.ForeColor = System.Drawing.Color.White;
            this.lbBDOpen.Location = new System.Drawing.Point(299, 458);
            this.lbBDOpen.Name = "lbBDOpen";
            this.lbBDOpen.Size = new System.Drawing.Size(130, 35);
            this.lbBDOpen.TabIndex = 104;
            this.lbBDOpen.Text = "Open";
            this.lbBDOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbBDOpen.Visible = false;
            // 
            // lbBDClose
            // 
            this.lbBDClose.BackColor = System.Drawing.Color.Gray;
            this.lbBDClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBDClose.ForeColor = System.Drawing.Color.White;
            this.lbBDClose.Location = new System.Drawing.Point(435, 458);
            this.lbBDClose.Name = "lbBDClose";
            this.lbBDClose.Size = new System.Drawing.Size(130, 35);
            this.lbBDClose.TabIndex = 105;
            this.lbBDClose.Text = "Close";
            this.lbBDClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbBDClose.Visible = false;
            // 
            // lbTMClose
            // 
            this.lbTMClose.BackColor = System.Drawing.Color.Gray;
            this.lbTMClose.Enabled = false;
            this.lbTMClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTMClose.ForeColor = System.Drawing.Color.White;
            this.lbTMClose.Location = new System.Drawing.Point(435, 523);
            this.lbTMClose.Name = "lbTMClose";
            this.lbTMClose.Size = new System.Drawing.Size(130, 35);
            this.lbTMClose.TabIndex = 107;
            this.lbTMClose.Text = "Close";
            this.lbTMClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTMClose.Visible = false;
            // 
            // lbTMOpen
            // 
            this.lbTMOpen.BackColor = System.Drawing.Color.Gray;
            this.lbTMOpen.Enabled = false;
            this.lbTMOpen.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTMOpen.ForeColor = System.Drawing.Color.White;
            this.lbTMOpen.Location = new System.Drawing.Point(299, 523);
            this.lbTMOpen.Name = "lbTMOpen";
            this.lbTMOpen.Size = new System.Drawing.Size(130, 35);
            this.lbTMOpen.TabIndex = 106;
            this.lbTMOpen.Text = "Open";
            this.lbTMOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTMOpen.Visible = false;
            // 
            // lbLLClose
            // 
            this.lbLLClose.BackColor = System.Drawing.Color.Gray;
            this.lbLLClose.Enabled = false;
            this.lbLLClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLLClose.ForeColor = System.Drawing.Color.White;
            this.lbLLClose.Location = new System.Drawing.Point(435, 587);
            this.lbLLClose.Name = "lbLLClose";
            this.lbLLClose.Size = new System.Drawing.Size(130, 35);
            this.lbLLClose.TabIndex = 109;
            this.lbLLClose.Text = "Close";
            this.lbLLClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbLLClose.Visible = false;
            // 
            // lbLLOpen
            // 
            this.lbLLOpen.BackColor = System.Drawing.Color.Gray;
            this.lbLLOpen.Enabled = false;
            this.lbLLOpen.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLLOpen.ForeColor = System.Drawing.Color.White;
            this.lbLLOpen.Location = new System.Drawing.Point(299, 587);
            this.lbLLOpen.Name = "lbLLOpen";
            this.lbLLOpen.Size = new System.Drawing.Size(130, 35);
            this.lbLLOpen.TabIndex = 108;
            this.lbLLOpen.Text = "Open";
            this.lbLLOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbLLOpen.Visible = false;
            // 
            // btnVtmArmExt1
            // 
            this.btnVtmArmExt1.BackColor = System.Drawing.Color.DimGray;
            this.btnVtmArmExt1.Enabled = false;
            this.btnVtmArmExt1.FadeOnFocus = true;
            this.btnVtmArmExt1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnVtmArmExt1.GlowColor = System.Drawing.Color.White;
            this.btnVtmArmExt1.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnVtmArmExt1.Location = new System.Drawing.Point(317, 228);
            this.btnVtmArmExt1.Name = "btnVtmArmExt1";
            this.btnVtmArmExt1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnVtmArmExt1.ShineColor = System.Drawing.Color.DarkGray;
            this.btnVtmArmExt1.Size = new System.Drawing.Size(121, 49);
            this.btnVtmArmExt1.TabIndex = 110;
            this.btnVtmArmExt1.Text = "VTM Arm Ext #1 ON";
            this.btnVtmArmExt1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVtmArmExt1.Visible = false;
            this.btnVtmArmExt1.Click += new System.EventHandler(this.btnVtmArmExt1_Click);
            // 
            // glassButton5
            // 
            this.glassButton5.BackColor = System.Drawing.Color.DimGray;
            this.glassButton5.Enabled = false;
            this.glassButton5.FadeOnFocus = true;
            this.glassButton5.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton5.GlowColor = System.Drawing.Color.White;
            this.glassButton5.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton5.Location = new System.Drawing.Point(317, 283);
            this.glassButton5.Name = "glassButton5";
            this.glassButton5.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton5.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton5.Size = new System.Drawing.Size(121, 49);
            this.glassButton5.TabIndex = 111;
            this.glassButton5.Text = "VTM Arm Ext #2 ON";
            this.glassButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton5.Visible = false;
            this.glassButton5.Click += new System.EventHandler(this.glassButton5_Click);
            // 
            // glassButton6
            // 
            this.glassButton6.BackColor = System.Drawing.Color.DimGray;
            this.glassButton6.Enabled = false;
            this.glassButton6.FadeOnFocus = true;
            this.glassButton6.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton6.GlowColor = System.Drawing.Color.White;
            this.glassButton6.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton6.Location = new System.Drawing.Point(317, 338);
            this.glassButton6.Name = "glassButton6";
            this.glassButton6.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton6.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton6.Size = new System.Drawing.Size(121, 49);
            this.glassButton6.TabIndex = 112;
            this.glassButton6.Text = "VTM Arm Ext #3 ON";
            this.glassButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton6.Visible = false;
            this.glassButton6.Click += new System.EventHandler(this.glassButton6_Click);
            // 
            // glassButton7
            // 
            this.glassButton7.BackColor = System.Drawing.Color.DimGray;
            this.glassButton7.Enabled = false;
            this.glassButton7.FadeOnFocus = true;
            this.glassButton7.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton7.GlowColor = System.Drawing.Color.White;
            this.glassButton7.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton7.Location = new System.Drawing.Point(317, 393);
            this.glassButton7.Name = "glassButton7";
            this.glassButton7.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton7.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton7.Size = new System.Drawing.Size(121, 49);
            this.glassButton7.TabIndex = 113;
            this.glassButton7.Text = "VTM Arm Ext #4 ON";
            this.glassButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton7.Visible = false;
            this.glassButton7.Click += new System.EventHandler(this.glassButton7_Click);
            // 
            // glassButton8
            // 
            this.glassButton8.BackColor = System.Drawing.Color.DimGray;
            this.glassButton8.Enabled = false;
            this.glassButton8.FadeOnFocus = true;
            this.glassButton8.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton8.GlowColor = System.Drawing.Color.White;
            this.glassButton8.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton8.Location = new System.Drawing.Point(444, 393);
            this.glassButton8.Name = "glassButton8";
            this.glassButton8.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton8.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton8.Size = new System.Drawing.Size(121, 49);
            this.glassButton8.TabIndex = 117;
            this.glassButton8.Text = "VTM Arm Ext #4 OFF";
            this.glassButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton8.Visible = false;
            this.glassButton8.Click += new System.EventHandler(this.glassButton8_Click);
            // 
            // glassButton9
            // 
            this.glassButton9.BackColor = System.Drawing.Color.DimGray;
            this.glassButton9.Enabled = false;
            this.glassButton9.FadeOnFocus = true;
            this.glassButton9.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton9.GlowColor = System.Drawing.Color.White;
            this.glassButton9.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton9.Location = new System.Drawing.Point(444, 338);
            this.glassButton9.Name = "glassButton9";
            this.glassButton9.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton9.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton9.Size = new System.Drawing.Size(121, 49);
            this.glassButton9.TabIndex = 116;
            this.glassButton9.Text = "VTM Arm Ext #3 OFF";
            this.glassButton9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton9.Visible = false;
            this.glassButton9.Click += new System.EventHandler(this.glassButton9_Click);
            // 
            // glassButton10
            // 
            this.glassButton10.BackColor = System.Drawing.Color.DimGray;
            this.glassButton10.Enabled = false;
            this.glassButton10.FadeOnFocus = true;
            this.glassButton10.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton10.GlowColor = System.Drawing.Color.White;
            this.glassButton10.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton10.Location = new System.Drawing.Point(444, 283);
            this.glassButton10.Name = "glassButton10";
            this.glassButton10.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton10.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton10.Size = new System.Drawing.Size(121, 49);
            this.glassButton10.TabIndex = 115;
            this.glassButton10.Text = "VTM Arm Ext #2 OFF";
            this.glassButton10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton10.Visible = false;
            this.glassButton10.Click += new System.EventHandler(this.glassButton10_Click);
            // 
            // glassButton11
            // 
            this.glassButton11.BackColor = System.Drawing.Color.DimGray;
            this.glassButton11.Enabled = false;
            this.glassButton11.FadeOnFocus = true;
            this.glassButton11.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.glassButton11.GlowColor = System.Drawing.Color.White;
            this.glassButton11.InnerBorderColor = System.Drawing.Color.Transparent;
            this.glassButton11.Location = new System.Drawing.Point(444, 228);
            this.glassButton11.Name = "glassButton11";
            this.glassButton11.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton11.ShineColor = System.Drawing.Color.DarkGray;
            this.glassButton11.Size = new System.Drawing.Size(121, 49);
            this.glassButton11.TabIndex = 114;
            this.glassButton11.Text = "VTM Arm Ext #1 OFF";
            this.glassButton11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.glassButton11.Visible = false;
            this.glassButton11.Click += new System.EventHandler(this.glassButton11_Click);
            // 
            // btnVtmPump
            // 
            this.btnVtmPump.BackColor = System.Drawing.Color.DimGray;
            this.btnVtmPump.FadeOnFocus = true;
            this.btnVtmPump.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnVtmPump.GlowColor = System.Drawing.Color.White;
            this.btnVtmPump.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnVtmPump.Location = new System.Drawing.Point(18, 17);
            this.btnVtmPump.Name = "btnVtmPump";
            this.btnVtmPump.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnVtmPump.ShineColor = System.Drawing.Color.DarkGray;
            this.btnVtmPump.Size = new System.Drawing.Size(121, 49);
            this.btnVtmPump.TabIndex = 118;
            this.btnVtmPump.Text = "VTM Pumping";
            this.btnVtmPump.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVtmPump.Click += new System.EventHandler(this.btnVtmPump_Click);
            // 
            // btnVtmVent
            // 
            this.btnVtmVent.BackColor = System.Drawing.Color.DimGray;
            this.btnVtmVent.FadeOnFocus = true;
            this.btnVtmVent.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnVtmVent.GlowColor = System.Drawing.Color.White;
            this.btnVtmVent.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnVtmVent.Location = new System.Drawing.Point(145, 17);
            this.btnVtmVent.Name = "btnVtmVent";
            this.btnVtmVent.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnVtmVent.ShineColor = System.Drawing.Color.DarkGray;
            this.btnVtmVent.Size = new System.Drawing.Size(121, 49);
            this.btnVtmVent.TabIndex = 119;
            this.btnVtmVent.Text = "VTM Venting";
            this.btnVtmVent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVtmVent.Click += new System.EventHandler(this.btnVtmVent_Click);
            // 
            // btnLoadlockVent
            // 
            this.btnLoadlockVent.BackColor = System.Drawing.Color.DimGray;
            this.btnLoadlockVent.FadeOnFocus = true;
            this.btnLoadlockVent.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLoadlockVent.GlowColor = System.Drawing.Color.White;
            this.btnLoadlockVent.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnLoadlockVent.Location = new System.Drawing.Point(145, 89);
            this.btnLoadlockVent.Name = "btnLoadlockVent";
            this.btnLoadlockVent.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnLoadlockVent.ShineColor = System.Drawing.Color.DarkGray;
            this.btnLoadlockVent.Size = new System.Drawing.Size(121, 49);
            this.btnLoadlockVent.TabIndex = 121;
            this.btnLoadlockVent.Text = "Loadlock   Venting";
            this.btnLoadlockVent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoadlockVent.Click += new System.EventHandler(this.btnLoadlockVent_Click);
            // 
            // btnLoadlockPump
            // 
            this.btnLoadlockPump.BackColor = System.Drawing.Color.DimGray;
            this.btnLoadlockPump.FadeOnFocus = true;
            this.btnLoadlockPump.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLoadlockPump.GlowColor = System.Drawing.Color.White;
            this.btnLoadlockPump.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnLoadlockPump.Location = new System.Drawing.Point(18, 89);
            this.btnLoadlockPump.Name = "btnLoadlockPump";
            this.btnLoadlockPump.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnLoadlockPump.ShineColor = System.Drawing.Color.DarkGray;
            this.btnLoadlockPump.Size = new System.Drawing.Size(121, 49);
            this.btnLoadlockPump.TabIndex = 120;
            this.btnLoadlockPump.Text = "Loadlock Pumping";
            this.btnLoadlockPump.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoadlockPump.Click += new System.EventHandler(this.btnLoadlockPump_Click);
            // 
            // subManualETC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnLoadlockVent);
            this.Controls.Add(this.btnLoadlockPump);
            this.Controls.Add(this.btnVtmVent);
            this.Controls.Add(this.btnVtmPump);
            this.Controls.Add(this.glassButton8);
            this.Controls.Add(this.glassButton9);
            this.Controls.Add(this.glassButton10);
            this.Controls.Add(this.glassButton11);
            this.Controls.Add(this.glassButton7);
            this.Controls.Add(this.glassButton6);
            this.Controls.Add(this.glassButton5);
            this.Controls.Add(this.btnVtmArmExt1);
            this.Controls.Add(this.lbLLClose);
            this.Controls.Add(this.lbLLOpen);
            this.Controls.Add(this.lbTMClose);
            this.Controls.Add(this.lbTMOpen);
            this.Controls.Add(this.lbBDClose);
            this.Controls.Add(this.lbBDOpen);
            this.Controls.Add(this.glassButton3);
            this.Controls.Add(this.glassButton4);
            this.Controls.Add(this.glassButton1);
            this.Controls.Add(this.glassButton2);
            this.Controls.Add(this.btnGateClose);
            this.Controls.Add(this.btnGateOpen);
            this.Name = "subManualETC";
            this.Size = new System.Drawing.Size(617, 729);
            this.Load += new System.EventHandler(this.subManualETC_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Glass.GlassButton btnGateClose;
        private Glass.GlassButton btnGateOpen;
        private System.Windows.Forms.Timer tmrStatus;
        private Glass.GlassButton glassButton1;
        private Glass.GlassButton glassButton2;
        private Glass.GlassButton glassButton3;
        private Glass.GlassButton glassButton4;
        private System.Windows.Forms.Label lbBDOpen;
        private System.Windows.Forms.Label lbBDClose;
        private System.Windows.Forms.Label lbTMClose;
        private System.Windows.Forms.Label lbTMOpen;
        private System.Windows.Forms.Label lbLLClose;
        private System.Windows.Forms.Label lbLLOpen;
        private Glass.GlassButton btnVtmArmExt1;
        private Glass.GlassButton glassButton5;
        private Glass.GlassButton glassButton6;
        private Glass.GlassButton glassButton7;
        private Glass.GlassButton glassButton8;
        private Glass.GlassButton glassButton9;
        private Glass.GlassButton glassButton10;
        private Glass.GlassButton glassButton11;
        private Glass.GlassButton btnVtmPump;
        private Glass.GlassButton btnVtmVent;
        private Glass.GlassButton btnLoadlockVent;
        private Glass.GlassButton btnLoadlockPump;
    }
}
