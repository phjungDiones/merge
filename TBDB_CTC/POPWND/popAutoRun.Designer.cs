namespace TBDB_CTC.POPWND
{
    partial class popAutoRun
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
            this.btnAbort = new Glass.GlassButton();
            this.btnPause = new Glass.GlassButton();
            this.btnStop = new Glass.GlassButton();
            this.btnStart = new Glass.GlassButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CtcStatus_0 = new System.Windows.Forms.Label();
            this.lb22 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 390);
            this.panel1.TabIndex = 994;
            // 
            // btnAbort
            // 
            this.btnAbort.BackColor = System.Drawing.Color.Gray;
            this.btnAbort.FadeOnFocus = true;
            this.btnAbort.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbort.GlowColor = System.Drawing.Color.White;
            this.btnAbort.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnAbort.Location = new System.Drawing.Point(149, 289);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnAbort.ShineColor = System.Drawing.Color.DarkGray;
            this.btnAbort.Size = new System.Drawing.Size(110, 50);
            this.btnAbort.TabIndex = 1000;
            this.btnAbort.Text = "Abort";
            this.btnAbort.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Gray;
            this.btnPause.FadeOnFocus = true;
            this.btnPause.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.GlowColor = System.Drawing.Color.White;
            this.btnPause.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnPause.Location = new System.Drawing.Point(33, 289);
            this.btnPause.Name = "btnPause";
            this.btnPause.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnPause.ShineColor = System.Drawing.Color.DarkGray;
            this.btnPause.Size = new System.Drawing.Size(110, 50);
            this.btnPause.TabIndex = 999;
            this.btnPause.Text = "Pause";
            this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Gray;
            this.btnStop.FadeOnFocus = true;
            this.btnStop.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.GlowColor = System.Drawing.Color.White;
            this.btnStop.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnStop.Location = new System.Drawing.Point(149, 233);
            this.btnStop.Name = "btnStop";
            this.btnStop.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnStop.ShineColor = System.Drawing.Color.DarkGray;
            this.btnStop.Size = new System.Drawing.Size(110, 50);
            this.btnStop.TabIndex = 998;
            this.btnStop.Text = "Stop";
            this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Gray;
            this.btnStart.FadeOnFocus = true;
            this.btnStart.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.GlowColor = System.Drawing.Color.White;
            this.btnStart.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnStart.Location = new System.Drawing.Point(33, 233);
            this.btnStart.Name = "btnStart";
            this.btnStart.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnStart.ShineColor = System.Drawing.Color.DarkGray;
            this.btnStart.Size = new System.Drawing.Size(110, 50);
            this.btnStart.TabIndex = 997;
            this.btnStart.Text = "Start";
            this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.CtcStatus_0);
            this.panel2.Controls.Add(this.lb22);
            this.panel2.Controls.Add(this.btnStart);
            this.panel2.Controls.Add(this.btnAbort);
            this.panel2.Controls.Add(this.btnPause);
            this.panel2.Controls.Add(this.btnStop);
            this.panel2.Location = new System.Drawing.Point(208, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(277, 366);
            this.panel2.TabIndex = 1001;
            // 
            // CtcStatus_0
            // 
            this.CtcStatus_0.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CtcStatus_0.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CtcStatus_0.ForeColor = System.Drawing.Color.Black;
            this.CtcStatus_0.Location = new System.Drawing.Point(140, 14);
            this.CtcStatus_0.Name = "CtcStatus_0";
            this.CtcStatus_0.Size = new System.Drawing.Size(124, 27);
            this.CtcStatus_0.TabIndex = 1006;
            this.CtcStatus_0.Text = "IDLE";
            this.CtcStatus_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb22
            // 
            this.lb22.BackColor = System.Drawing.Color.Gray;
            this.lb22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb22.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb22.ForeColor = System.Drawing.Color.White;
            this.lb22.Location = new System.Drawing.Point(22, 14);
            this.lb22.Name = "lb22";
            this.lb22.Size = new System.Drawing.Size(117, 27);
            this.lb22.TabIndex = 1005;
            this.lb22.Text = "STATUS";
            this.lb22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(11, 11);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(191, 366);
            this.panel3.TabIndex = 1002;
            // 
            // popAutoRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 390);
            this.Controls.Add(this.panel1);
            this.Name = "popAutoRun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "popAutoRun";
            this.Load += new System.EventHandler(this.popAutoRun_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Glass.GlassButton btnAbort;
        private Glass.GlassButton btnPause;
        private Glass.GlassButton btnStop;
        private Glass.GlassButton btnStart;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label CtcStatus_0;
        private System.Windows.Forms.Label lb22;
    }
}