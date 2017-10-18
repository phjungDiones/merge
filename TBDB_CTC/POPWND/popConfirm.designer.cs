namespace TBDB_CTC.POPWND
{
    partial class popConfirm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.win_GlassButton2 = new CJ_Controls.Windows.Win_GlassButton();
            this.win_GlassButton1 = new CJ_Controls.Windows.Win_GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel1.Controls.Add(this.win_GlassButton2);
            this.panel1.Controls.Add(this.win_GlassButton1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(11, 11);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 287);
            this.panel1.TabIndex = 0;
            // 
            // win_GlassButton2
            // 
            this.win_GlassButton2.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.win_GlassButton2.Location = new System.Drawing.Point(385, 146);
            this.win_GlassButton2.Margin = new System.Windows.Forms.Padding(2);
            this.win_GlassButton2.Name = "win_GlassButton2";
            this.win_GlassButton2.Size = new System.Drawing.Size(147, 87);
            this.win_GlassButton2.TabIndex = 2;
            this.win_GlassButton2.Text = "취소";
            this.win_GlassButton2.Click += new System.EventHandler(this.win_GlassButton2_Click);
            // 
            // win_GlassButton1
            // 
            this.win_GlassButton1.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.win_GlassButton1.Location = new System.Drawing.Point(132, 146);
            this.win_GlassButton1.Margin = new System.Windows.Forms.Padding(2);
            this.win_GlassButton1.Name = "win_GlassButton1";
            this.win_GlassButton1.Size = new System.Drawing.Size(147, 87);
            this.win_GlassButton1.TabIndex = 1;
            this.win_GlassButton1.Text = "확인";
            this.win_GlassButton1.Click += new System.EventHandler(this.win_GlassButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(64, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(543, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "작업을 진행하시겠습니까?";
            // 
            // popConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(666, 309);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "popConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "popConfirm";
            this.Load += new System.EventHandler(this.popConfirm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private CJ_Controls.Windows.Win_GlassButton win_GlassButton2;
        private CJ_Controls.Windows.Win_GlassButton win_GlassButton1;
    }
}