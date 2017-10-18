namespace CJ_Controls.Splash_Screen
{
	partial class Form_Splash
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
			this.lblStatus = new System.Windows.Forms.Label();
			this.pgbProcess = new System.Windows.Forms.ProgressBar();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.lblStatus);
			this.panel1.Controls.Add(this.pgbProcess);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(753, 430);
			this.panel1.TabIndex = 0;
			// 
			// lblStatus
			// 
			this.lblStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblStatus.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblStatus.ForeColor = System.Drawing.Color.Black;
			this.lblStatus.Location = new System.Drawing.Point(0, 383);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(751, 25);
			this.lblStatus.TabIndex = 2;
			this.lblStatus.Text = "Loading...";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pgbProcess
			// 
			this.pgbProcess.BackColor = System.Drawing.Color.White;
			this.pgbProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pgbProcess.Location = new System.Drawing.Point(0, 408);
			this.pgbProcess.MarqueeAnimationSpeed = 20;
			this.pgbProcess.Name = "pgbProcess";
			this.pgbProcess.Size = new System.Drawing.Size(751, 20);
			this.pgbProcess.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.pgbProcess.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::CJ_Controls.Properties.Resources.laser_737441_640;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(751, 383);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// Form_Splash
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(753, 430);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form_Splash";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CJ_SplashForm";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CJ_SplashForm_FormClosing);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ProgressBar pgbProcess;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}