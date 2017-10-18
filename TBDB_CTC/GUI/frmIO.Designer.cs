namespace TBDB_CTC.GUI
{
    partial class frmIO
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
            this.components = new System.ComponentModel.Container();
            this.IO_List_View = new CJ_Controls.DeviceNet.Ctrl_DNet_IO_List_View();
            this.btnCreateList = new Glass.GlassButton();
            this.tmrStatus = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // IO_List_View
            // 
            this.IO_List_View.Location = new System.Drawing.Point(8, 8);
            this.IO_List_View.Name = "IO_List_View";
            this.IO_List_View.Size = new System.Drawing.Size(1145, 805);
            this.IO_List_View.TabIndex = 1;
            this.IO_List_View.TabName = "";
            // 
            // btnCreateList
            // 
            this.btnCreateList.BackColor = System.Drawing.Color.DimGray;
            this.btnCreateList.FadeOnFocus = true;
            this.btnCreateList.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateList.GlowColor = System.Drawing.Color.White;
            this.btnCreateList.InnerBorderColor = System.Drawing.Color.Transparent;
            this.btnCreateList.Location = new System.Drawing.Point(6, 12);
            this.btnCreateList.Name = "btnCreateList";
            this.btnCreateList.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnCreateList.ShineColor = System.Drawing.Color.DarkGray;
            this.btnCreateList.Size = new System.Drawing.Size(91, 77);
            this.btnCreateList.TabIndex = 921;
            this.btnCreateList.Text = "Create list";
            this.btnCreateList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateList.Click += new System.EventHandler(this.btnCreateList_Click);
            // 
            // tmrStatus
            // 
            this.tmrStatus.Interval = 500;
            this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.btnCreateList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1159, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(105, 819);
            this.panel2.TabIndex = 1006;
            // 
            // frmIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1264, 819);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.IO_List_View);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmIO";
            this.Text = "frmIO";
            this.Load += new System.EventHandler(this.frmIO_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CJ_Controls.DeviceNet.Ctrl_DNet_IO_List_View IO_List_View;
        private Glass.GlassButton btnCreateList;
        private System.Windows.Forms.Timer tmrStatus;
        private System.Windows.Forms.Panel panel2;
    }
}