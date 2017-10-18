namespace TBDB_CTC.UserCtrl.SubForm.ManualSub
{
    partial class subManualAligner
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
            this.btnRotateWafer = new Glass.GlassButton();
            this.btnStartAl = new Glass.GlassButton();
            this.btnSetAlignOffset = new Glass.GlassButton();
            this.btnClear = new Glass.GlassButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVacOff = new Glass.GlassButton();
            this.btnVacOn = new Glass.GlassButton();
            this.btnChuckDown = new Glass.GlassButton();
            this.btnChuckUp = new Glass.GlassButton();
            this.btnReset = new Glass.GlassButton();
            this.btnInit = new Glass.GlassButton();
            this.tbAlngeOffset = new System.Windows.Forms.TextBox();
            this.tbStartAlign = new System.Windows.Forms.TextBox();
            this.tbRotateVal = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.a1Panel1 = new Owf.Controls.A1Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbCurAngle = new System.Windows.Forms.Label();
            this.lbVacStatus = new System.Windows.Forms.Label();
            this.lbChuckPos = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.a1Panel2 = new Owf.Controls.A1Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tmrStatus = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.a1Panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.a1Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRotateWafer
            // 
            this.btnRotateWafer.BackColor = System.Drawing.Color.DimGray;
            this.btnRotateWafer.FadeOnFocus = true;
            this.btnRotateWafer.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRotateWafer.GlowColor = System.Drawing.Color.White;
            this.btnRotateWafer.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnRotateWafer.Location = new System.Drawing.Point(162, 161);
            this.btnRotateWafer.Name = "btnRotateWafer";
            this.btnRotateWafer.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnRotateWafer.ShineColor = System.Drawing.Color.DarkGray;
            this.btnRotateWafer.Size = new System.Drawing.Size(129, 50);
            this.btnRotateWafer.TabIndex = 75;
            this.btnRotateWafer.Text = "ROTATE WAFER";
            this.btnRotateWafer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRotateWafer.Click += new System.EventHandler(this.btnRotateWafer_Click);
            // 
            // btnStartAl
            // 
            this.btnStartAl.BackColor = System.Drawing.Color.DimGray;
            this.btnStartAl.FadeOnFocus = true;
            this.btnStartAl.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStartAl.GlowColor = System.Drawing.Color.White;
            this.btnStartAl.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnStartAl.Location = new System.Drawing.Point(162, 106);
            this.btnStartAl.Name = "btnStartAl";
            this.btnStartAl.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnStartAl.ShineColor = System.Drawing.Color.DarkGray;
            this.btnStartAl.Size = new System.Drawing.Size(129, 50);
            this.btnStartAl.TabIndex = 74;
            this.btnStartAl.Text = "START ALIGNMENT";
            this.btnStartAl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStartAl.Click += new System.EventHandler(this.btnStartAl_Click);
            // 
            // btnSetAlignOffset
            // 
            this.btnSetAlignOffset.BackColor = System.Drawing.Color.DimGray;
            this.btnSetAlignOffset.FadeOnFocus = true;
            this.btnSetAlignOffset.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSetAlignOffset.GlowColor = System.Drawing.Color.White;
            this.btnSetAlignOffset.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnSetAlignOffset.Location = new System.Drawing.Point(162, 51);
            this.btnSetAlignOffset.Name = "btnSetAlignOffset";
            this.btnSetAlignOffset.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnSetAlignOffset.ShineColor = System.Drawing.Color.DarkGray;
            this.btnSetAlignOffset.Size = new System.Drawing.Size(129, 50);
            this.btnSetAlignOffset.TabIndex = 73;
            this.btnSetAlignOffset.Text = "SET ALIGN OFFSET";
            this.btnSetAlignOffset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSetAlignOffset.Click += new System.EventHandler(this.btnSetAlignOffset_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.DimGray;
            this.btnClear.FadeOnFocus = true;
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClear.GlowColor = System.Drawing.Color.White;
            this.btnClear.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnClear.Location = new System.Drawing.Point(152, 326);
            this.btnClear.Name = "btnClear";
            this.btnClear.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnClear.ShineColor = System.Drawing.Color.DarkGray;
            this.btnClear.Size = new System.Drawing.Size(121, 49);
            this.btnClear.TabIndex = 72;
            this.btnClear.Text = "CLEAR";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(17, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 28);
            this.label3.TabIndex = 71;
            this.label3.Text = "ROTATE WAFER";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 28);
            this.label2.TabIndex = 70;
            this.label2.Text = "START ALIGN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 28);
            this.label1.TabIndex = 62;
            this.label1.Text = "ALIGN OFFSET";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnVacOff
            // 
            this.btnVacOff.BackColor = System.Drawing.Color.DimGray;
            this.btnVacOff.FadeOnFocus = true;
            this.btnVacOff.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnVacOff.GlowColor = System.Drawing.Color.White;
            this.btnVacOff.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnVacOff.Location = new System.Drawing.Point(152, 436);
            this.btnVacOff.Name = "btnVacOff";
            this.btnVacOff.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnVacOff.ShineColor = System.Drawing.Color.DarkGray;
            this.btnVacOff.Size = new System.Drawing.Size(121, 49);
            this.btnVacOff.TabIndex = 69;
            this.btnVacOff.Text = "VACUUM OFF";
            this.btnVacOff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVacOff.Click += new System.EventHandler(this.btnVacOff_Click);
            // 
            // btnVacOn
            // 
            this.btnVacOn.BackColor = System.Drawing.Color.DimGray;
            this.btnVacOn.FadeOnFocus = true;
            this.btnVacOn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnVacOn.GlowColor = System.Drawing.Color.White;
            this.btnVacOn.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnVacOn.Location = new System.Drawing.Point(25, 436);
            this.btnVacOn.Name = "btnVacOn";
            this.btnVacOn.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnVacOn.ShineColor = System.Drawing.Color.DarkGray;
            this.btnVacOn.Size = new System.Drawing.Size(121, 49);
            this.btnVacOn.TabIndex = 68;
            this.btnVacOn.Text = "VACUUM ON";
            this.btnVacOn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVacOn.Click += new System.EventHandler(this.btnVacOn_Click);
            // 
            // btnChuckDown
            // 
            this.btnChuckDown.BackColor = System.Drawing.Color.DimGray;
            this.btnChuckDown.FadeOnFocus = true;
            this.btnChuckDown.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnChuckDown.GlowColor = System.Drawing.Color.White;
            this.btnChuckDown.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnChuckDown.Location = new System.Drawing.Point(152, 381);
            this.btnChuckDown.Name = "btnChuckDown";
            this.btnChuckDown.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnChuckDown.ShineColor = System.Drawing.Color.DarkGray;
            this.btnChuckDown.Size = new System.Drawing.Size(121, 49);
            this.btnChuckDown.TabIndex = 67;
            this.btnChuckDown.Text = "CHUCK DOWN";
            this.btnChuckDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnChuckDown.Click += new System.EventHandler(this.btnChuckDown_Click);
            // 
            // btnChuckUp
            // 
            this.btnChuckUp.BackColor = System.Drawing.Color.DimGray;
            this.btnChuckUp.FadeOnFocus = true;
            this.btnChuckUp.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnChuckUp.GlowColor = System.Drawing.Color.White;
            this.btnChuckUp.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnChuckUp.Location = new System.Drawing.Point(25, 381);
            this.btnChuckUp.Name = "btnChuckUp";
            this.btnChuckUp.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnChuckUp.ShineColor = System.Drawing.Color.DarkGray;
            this.btnChuckUp.Size = new System.Drawing.Size(121, 49);
            this.btnChuckUp.TabIndex = 66;
            this.btnChuckUp.Text = "CHUCK UP";
            this.btnChuckUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnChuckUp.Click += new System.EventHandler(this.btnChuckUp_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.DimGray;
            this.btnReset.FadeOnFocus = true;
            this.btnReset.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnReset.GlowColor = System.Drawing.Color.White;
            this.btnReset.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnReset.Location = new System.Drawing.Point(152, 271);
            this.btnReset.Name = "btnReset";
            this.btnReset.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnReset.ShineColor = System.Drawing.Color.DarkGray;
            this.btnReset.Size = new System.Drawing.Size(121, 49);
            this.btnReset.TabIndex = 65;
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
            this.btnInit.Location = new System.Drawing.Point(25, 271);
            this.btnInit.Name = "btnInit";
            this.btnInit.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnInit.ShineColor = System.Drawing.Color.DarkGray;
            this.btnInit.Size = new System.Drawing.Size(121, 49);
            this.btnInit.TabIndex = 64;
            this.btnInit.Text = "INIT";
            this.btnInit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // tbAlngeOffset
            // 
            this.tbAlngeOffset.Location = new System.Drawing.Point(17, 80);
            this.tbAlngeOffset.Name = "tbAlngeOffset";
            this.tbAlngeOffset.Size = new System.Drawing.Size(140, 21);
            this.tbAlngeOffset.TabIndex = 76;
            this.tbAlngeOffset.Text = "0";
            this.tbAlngeOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbStartAlign
            // 
            this.tbStartAlign.Location = new System.Drawing.Point(17, 135);
            this.tbStartAlign.Name = "tbStartAlign";
            this.tbStartAlign.Size = new System.Drawing.Size(140, 21);
            this.tbStartAlign.TabIndex = 77;
            this.tbStartAlign.Text = "0";
            this.tbStartAlign.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRotateVal
            // 
            this.tbRotateVal.Location = new System.Drawing.Point(17, 190);
            this.tbRotateVal.Name = "tbRotateVal";
            this.tbRotateVal.Size = new System.Drawing.Size(140, 21);
            this.tbRotateVal.TabIndex = 78;
            this.tbRotateVal.Text = "0";
            this.tbRotateVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.a1Panel1);
            this.panel1.Controls.Add(this.tbRotateVal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbStartAlign);
            this.panel1.Controls.Add(this.btnInit);
            this.panel1.Controls.Add(this.tbAlngeOffset);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnRotateWafer);
            this.panel1.Controls.Add(this.btnChuckUp);
            this.panel1.Controls.Add(this.btnStartAl);
            this.panel1.Controls.Add(this.btnChuckDown);
            this.panel1.Controls.Add(this.btnSetAlignOffset);
            this.panel1.Controls.Add(this.btnVacOn);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnVacOff);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(303, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 763);
            this.panel1.TabIndex = 974;
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
            this.a1Panel1.Size = new System.Drawing.Size(311, 31);
            this.a1Panel1.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(34, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "ACTION";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lbCurAngle);
            this.panel2.Controls.Add(this.lbVacStatus);
            this.panel2.Controls.Add(this.lbChuckPos);
            this.panel2.Controls.Add(this.lbStatus);
            this.panel2.Controls.Add(this.a1Panel2);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(5, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(292, 763);
            this.panel2.TabIndex = 975;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(9, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 35);
            this.label9.TabIndex = 108;
            this.label9.Text = "Current Angle";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(9, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 35);
            this.label8.TabIndex = 107;
            this.label8.Text = "Vacuum";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(9, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 35);
            this.label7.TabIndex = 106;
            this.label7.Text = "Chuck Pos";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(9, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 35);
            this.label6.TabIndex = 105;
            this.label6.Text = "Stauts";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCurAngle
            // 
            this.lbCurAngle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbCurAngle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurAngle.ForeColor = System.Drawing.Color.Black;
            this.lbCurAngle.Location = new System.Drawing.Point(126, 209);
            this.lbCurAngle.Name = "lbCurAngle";
            this.lbCurAngle.Size = new System.Drawing.Size(149, 35);
            this.lbCurAngle.TabIndex = 104;
            this.lbCurAngle.Text = "Current Angle";
            this.lbCurAngle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVacStatus
            // 
            this.lbVacStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbVacStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVacStatus.ForeColor = System.Drawing.Color.Black;
            this.lbVacStatus.Location = new System.Drawing.Point(126, 159);
            this.lbVacStatus.Name = "lbVacStatus";
            this.lbVacStatus.Size = new System.Drawing.Size(149, 35);
            this.lbVacStatus.TabIndex = 103;
            this.lbVacStatus.Text = "Vacuum";
            this.lbVacStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbChuckPos
            // 
            this.lbChuckPos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbChuckPos.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChuckPos.ForeColor = System.Drawing.Color.Black;
            this.lbChuckPos.Location = new System.Drawing.Point(126, 109);
            this.lbChuckPos.Name = "lbChuckPos";
            this.lbChuckPos.Size = new System.Drawing.Size(149, 35);
            this.lbChuckPos.TabIndex = 102;
            this.lbChuckPos.Text = "Chuck Pos";
            this.lbChuckPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbStatus
            // 
            this.lbStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.ForeColor = System.Drawing.Color.Black;
            this.lbStatus.Location = new System.Drawing.Point(126, 59);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(149, 35);
            this.lbStatus.TabIndex = 101;
            this.lbStatus.Text = "Stauts";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.a1Panel2.Size = new System.Drawing.Size(290, 31);
            this.a1Panel2.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(33, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "STATUS";
            // 
            // tmrStatus
            // 
            this.tmrStatus.Interval = 500;
            this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
            // 
            // subManualAligner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "subManualAligner";
            this.Size = new System.Drawing.Size(619, 731);
            this.Load += new System.EventHandler(this.subManualAligner_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.a1Panel1.ResumeLayout(false);
            this.a1Panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.a1Panel2.ResumeLayout(false);
            this.a1Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Glass.GlassButton btnRotateWafer;
        private Glass.GlassButton btnStartAl;
        private Glass.GlassButton btnSetAlignOffset;
        private Glass.GlassButton btnClear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Glass.GlassButton btnVacOff;
        private Glass.GlassButton btnVacOn;
        private Glass.GlassButton btnChuckDown;
        private Glass.GlassButton btnChuckUp;
        private Glass.GlassButton btnReset;
        private Glass.GlassButton btnInit;
        private System.Windows.Forms.TextBox tbAlngeOffset;
        private System.Windows.Forms.TextBox tbStartAlign;
        private System.Windows.Forms.TextBox tbRotateVal;
        private System.Windows.Forms.Panel panel1;
        private Owf.Controls.A1Panel a1Panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private Owf.Controls.A1Panel a1Panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer tmrStatus;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbVacStatus;
        private System.Windows.Forms.Label lbChuckPos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbCurAngle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}
