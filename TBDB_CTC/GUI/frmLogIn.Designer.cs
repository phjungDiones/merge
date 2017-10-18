namespace TBDB_CTC.GUI
{
    partial class frmLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogIn));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxUserID = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnMaker = new System.Windows.Forms.Button();
            this.btnEng = new System.Windows.Forms.Button();
            this.btnOp = new System.Windows.Forms.Button();
            this.a1Panel6 = new Owf.Controls.A1Panel();
            this.lbPortName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.a1Panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxUserID);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Controls.Add(this.btnMaker);
            this.groupBox1.Controls.Add(this.btnEng);
            this.groupBox1.Controls.Add(this.btnOp);
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(305, 172);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 475);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(99, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 22);
            this.label2.TabIndex = 29;
            this.label2.Text = "USER ID";
            // 
            // tbxUserID
            // 
            this.tbxUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxUserID.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbxUserID.Location = new System.Drawing.Point(255, 249);
            this.tbxUserID.Name = "tbxUserID";
            this.tbxUserID.Size = new System.Drawing.Size(305, 30);
            this.tbxUserID.TabIndex = 28;
            this.tbxUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.Location = new System.Drawing.Point(410, 335);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Padding = new System.Windows.Forms.Padding(15, 0, 20, 0);
            this.btnLogin.Size = new System.Drawing.Size(150, 53);
            this.btnLogin.TabIndex = 25;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(99, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 22);
            this.label1.TabIndex = 27;
            this.label1.Text = "PASSWORD";
            // 
            // txtPass
            // 
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPass.Location = new System.Drawing.Point(255, 285);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '●';
            this.txtPass.Size = new System.Drawing.Size(305, 30);
            this.txtPass.TabIndex = 24;
            this.txtPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnMaker
            // 
            this.btnMaker.BackColor = System.Drawing.SystemColors.Control;
            this.btnMaker.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMaker.Image = ((System.Drawing.Image)(resources.GetObject("btnMaker.Image")));
            this.btnMaker.Location = new System.Drawing.Point(410, 94);
            this.btnMaker.Name = "btnMaker";
            this.btnMaker.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnMaker.Size = new System.Drawing.Size(150, 133);
            this.btnMaker.TabIndex = 23;
            this.btnMaker.Tag = "2";
            this.btnMaker.Text = "MAKER";
            this.btnMaker.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMaker.UseVisualStyleBackColor = false;
            this.btnMaker.Click += new System.EventHandler(this.OnChangeUserLevelButtonClick);
            // 
            // btnEng
            // 
            this.btnEng.BackColor = System.Drawing.SystemColors.Control;
            this.btnEng.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnEng.Image = ((System.Drawing.Image)(resources.GetObject("btnEng.Image")));
            this.btnEng.Location = new System.Drawing.Point(255, 94);
            this.btnEng.Name = "btnEng";
            this.btnEng.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnEng.Size = new System.Drawing.Size(150, 133);
            this.btnEng.TabIndex = 22;
            this.btnEng.Tag = "1";
            this.btnEng.Text = "ENGINEER";
            this.btnEng.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEng.UseVisualStyleBackColor = false;
            this.btnEng.Click += new System.EventHandler(this.OnChangeUserLevelButtonClick);
            // 
            // btnOp
            // 
            this.btnOp.BackColor = System.Drawing.Color.Gold;
            this.btnOp.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOp.Image = ((System.Drawing.Image)(resources.GetObject("btnOp.Image")));
            this.btnOp.Location = new System.Drawing.Point(100, 94);
            this.btnOp.Name = "btnOp";
            this.btnOp.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnOp.Size = new System.Drawing.Size(150, 133);
            this.btnOp.TabIndex = 21;
            this.btnOp.Tag = "0";
            this.btnOp.Text = "OP";
            this.btnOp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOp.UseVisualStyleBackColor = false;
            this.btnOp.Click += new System.EventHandler(this.OnChangeUserLevelButtonClick);
            // 
            // a1Panel6
            // 
            this.a1Panel6.BorderColor = System.Drawing.Color.White;
            this.a1Panel6.BorderWidth = 0;
            this.a1Panel6.Controls.Add(this.lbPortName);
            this.a1Panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.a1Panel6.GradientEndColor = System.Drawing.Color.Black;
            this.a1Panel6.GradientStartColor = System.Drawing.Color.White;
            this.a1Panel6.Image = null;
            this.a1Panel6.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel6.Location = new System.Drawing.Point(0, 0);
            this.a1Panel6.Margin = new System.Windows.Forms.Padding(2);
            this.a1Panel6.Name = "a1Panel6";
            this.a1Panel6.RoundCornerRadius = 6;
            this.a1Panel6.ShadowOffSet = 0;
            this.a1Panel6.Size = new System.Drawing.Size(1264, 20);
            this.a1Panel6.TabIndex = 1000;
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
            this.lbPortName.Text = "USER LOGGING SCREEN";
            this.lbPortName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1264, 819);
            this.Controls.Add(this.a1Panel6);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmLogIn";
            this.Text = "frmLogIn";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.a1Panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxUserID;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnMaker;
        private System.Windows.Forms.Button btnEng;
        private System.Windows.Forms.Button btnOp;
        private Owf.Controls.A1Panel a1Panel6;
        private System.Windows.Forms.Label lbPortName;
    }
}