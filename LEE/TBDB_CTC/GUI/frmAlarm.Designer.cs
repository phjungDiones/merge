namespace TBDB_CTC.GUI
{
    partial class frmAlarm
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
            this.a1Panel6 = new Owf.Controls.A1Panel();
            this.lbPortName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.a1Panel6.SuspendLayout();
            this.SuspendLayout();
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
            this.lbPortName.Text = "ALARM SCREEN";
            this.lbPortName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1159, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(105, 799);
            this.panel2.TabIndex = 1006;
            // 
            // frmAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1264, 819);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.a1Panel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmAlarm";
            this.Text = "frmAlarm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.a1Panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Owf.Controls.A1Panel a1Panel6;
        private System.Windows.Forms.Label lbPortName;
        private System.Windows.Forms.Panel panel2;
    }
}