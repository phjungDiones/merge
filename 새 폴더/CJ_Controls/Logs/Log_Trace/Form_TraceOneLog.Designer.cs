namespace CJ_Controls.Log_Trace
{
	partial class Form_TraceOneLog
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
			this.Layout_Message = new CJ_Controls.Windows.Win_QuickTableLayoutPanel();
			this.label56 = new System.Windows.Forms.Label();
			this.Label_DateTime = new System.Windows.Forms.Label();
			this.Label_Info = new System.Windows.Forms.Label();
			this.TextBox_Message = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Layout_Message.SuspendLayout();
			this.SuspendLayout();
			// 
			// Layout_Message
			// 
			this.Layout_Message.ColumnCount = 2;
			this.Layout_Message.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
			this.Layout_Message.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.Layout_Message.Controls.Add(this.label56, 0, 0);
			this.Layout_Message.Controls.Add(this.Label_DateTime, 1, 0);
			this.Layout_Message.Controls.Add(this.Label_Info, 1, 1);
			this.Layout_Message.Controls.Add(this.TextBox_Message, 1, 2);
			this.Layout_Message.Controls.Add(this.label2, 0, 1);
			this.Layout_Message.Controls.Add(this.label1, 0, 2);
			this.Layout_Message.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Layout_Message.Location = new System.Drawing.Point(0, 0);
			this.Layout_Message.Margin = new System.Windows.Forms.Padding(0);
			this.Layout_Message.Name = "Layout_Message";
			this.Layout_Message.RowCount = 3;
			this.Layout_Message.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.Layout_Message.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.Layout_Message.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.Layout_Message.Size = new System.Drawing.Size(696, 396);
			this.Layout_Message.TabIndex = 107;
			// 
			// label56
			// 
			this.label56.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label56.BackColor = System.Drawing.Color.Silver;
			this.label56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label56.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label56.Location = new System.Drawing.Point(0, 0);
			this.label56.Margin = new System.Windows.Forms.Padding(0);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(130, 30);
			this.label56.TabIndex = 100;
			this.label56.Text = "Date Time";
			this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_DateTime
			// 
			this.Label_DateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Label_DateTime.BackColor = System.Drawing.Color.White;
			this.Label_DateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_DateTime.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Label_DateTime.Location = new System.Drawing.Point(130, 0);
			this.Label_DateTime.Margin = new System.Windows.Forms.Padding(0);
			this.Label_DateTime.Name = "Label_DateTime";
			this.Label_DateTime.Size = new System.Drawing.Size(566, 30);
			this.Label_DateTime.TabIndex = 101;
			this.Label_DateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_Info
			// 
			this.Label_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Label_Info.BackColor = System.Drawing.Color.White;
			this.Label_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Info.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Label_Info.Location = new System.Drawing.Point(130, 30);
			this.Label_Info.Margin = new System.Windows.Forms.Padding(0);
			this.Label_Info.Name = "Label_Info";
			this.Label_Info.Size = new System.Drawing.Size(566, 30);
			this.Label_Info.TabIndex = 103;
			this.Label_Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TextBox_Message
			// 
			this.TextBox_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBox_Message.BackColor = System.Drawing.Color.White;
			this.TextBox_Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TextBox_Message.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.TextBox_Message.Location = new System.Drawing.Point(130, 60);
			this.TextBox_Message.Margin = new System.Windows.Forms.Padding(0);
			this.TextBox_Message.Multiline = true;
			this.TextBox_Message.Name = "TextBox_Message";
			this.TextBox_Message.ReadOnly = true;
			this.TextBox_Message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TextBox_Message.Size = new System.Drawing.Size(566, 336);
			this.TextBox_Message.TabIndex = 105;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.BackColor = System.Drawing.Color.Silver;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label2.Location = new System.Drawing.Point(0, 30);
			this.label2.Margin = new System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(130, 30);
			this.label2.TabIndex = 102;
			this.label2.Text = "Information";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BackColor = System.Drawing.Color.Silver;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(0, 60);
			this.label1.Margin = new System.Windows.Forms.Padding(0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 336);
			this.label1.TabIndex = 104;
			this.label1.Text = "Message";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Form_TraceOneLog
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(696, 396);
			this.Controls.Add(this.Layout_Message);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form_TraceOneLog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Form_TraceOneLog";
			this.Load += new System.EventHandler(this.Form_TraceOneLog_Load);
			this.Layout_Message.ResumeLayout(false);
			this.Layout_Message.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Windows.Win_QuickTableLayoutPanel Layout_Message;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.Label Label_DateTime;
		private System.Windows.Forms.Label Label_Info;
		private System.Windows.Forms.TextBox TextBox_Message;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;

	}
}