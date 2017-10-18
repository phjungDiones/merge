namespace TBDB_CTC.POPWND
{
    partial class popAppLoading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(popAppLoading));
            this.lbLoadingStatus = new System.Windows.Forms.Label();
            this.prgLoading = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lbLoadingStatus
            // 
            this.lbLoadingStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbLoadingStatus.ForeColor = System.Drawing.Color.White;
            this.lbLoadingStatus.Location = new System.Drawing.Point(371, 312);
            this.lbLoadingStatus.Name = "lbLoadingStatus";
            this.lbLoadingStatus.Size = new System.Drawing.Size(331, 23);
            this.lbLoadingStatus.TabIndex = 3;
            this.lbLoadingStatus.Text = "GUI Loading && initializing";
            this.lbLoadingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prgLoading
            // 
            this.prgLoading.BackColor = System.Drawing.SystemColors.Control;
            this.prgLoading.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.prgLoading.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.prgLoading.Location = new System.Drawing.Point(0, 333);
            this.prgLoading.Name = "prgLoading";
            this.prgLoading.Size = new System.Drawing.Size(702, 23);
            this.prgLoading.TabIndex = 4;
            // 
            // popStartUp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(702, 356);
            this.Controls.Add(this.prgLoading);
            this.Controls.Add(this.lbLoadingStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "popStartUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "popStartUp";
            this.Shown += new System.EventHandler(this.popAppLoading_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbLoadingStatus;
        private System.Windows.Forms.ProgressBar prgLoading;

    }
}