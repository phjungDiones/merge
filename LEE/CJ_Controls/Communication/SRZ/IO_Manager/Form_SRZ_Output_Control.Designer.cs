namespace CJ_Controls.Communication.SRZ
{
	partial class Form_SRZ_Output_Control
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
			this.label56 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Label_IO_Name = new System.Windows.Forms.Label();
			this.Label_IO_Type = new System.Windows.Forms.Label();
			this.Num_IoValue = new System.Windows.Forms.NumericUpDown();
			this.Btn_Set = new CJ_Controls.Windows.Win_GlassButton();
			this.Btn_Cancel = new CJ_Controls.Windows.Win_GlassButton();
			((System.ComponentModel.ISupportInitialize)(this.Num_IoValue)).BeginInit();
			this.SuspendLayout();
			// 
			// label56
			// 
			this.label56.BackColor = System.Drawing.Color.Silver;
			this.label56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label56.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.label56.Location = new System.Drawing.Point(12, 9);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(76, 29);
			this.label56.TabIndex = 443;
			this.label56.Text = "Name";
			this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Silver;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(12, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 29);
			this.label1.TabIndex = 444;
			this.label1.Text = "Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Silver;
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(12, 71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 29);
			this.label2.TabIndex = 445;
			this.label2.Text = "Value";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_IO_Name
			// 
			this.Label_IO_Name.BackColor = System.Drawing.Color.White;
			this.Label_IO_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_IO_Name.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.Label_IO_Name.Location = new System.Drawing.Point(94, 9);
			this.Label_IO_Name.Name = "Label_IO_Name";
			this.Label_IO_Name.Size = new System.Drawing.Size(255, 29);
			this.Label_IO_Name.TabIndex = 446;
			this.Label_IO_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label_IO_Type
			// 
			this.Label_IO_Type.BackColor = System.Drawing.Color.White;
			this.Label_IO_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_IO_Type.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.Label_IO_Type.Location = new System.Drawing.Point(94, 40);
			this.Label_IO_Type.Name = "Label_IO_Type";
			this.Label_IO_Type.Size = new System.Drawing.Size(255, 29);
			this.Label_IO_Type.TabIndex = 447;
			this.Label_IO_Type.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Num_IoValue
			// 
			this.Num_IoValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Num_IoValue.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
			this.Num_IoValue.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.Num_IoValue.Location = new System.Drawing.Point(94, 71);
			this.Num_IoValue.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.Num_IoValue.Name = "Num_IoValue";
			this.Num_IoValue.Size = new System.Drawing.Size(255, 29);
			this.Num_IoValue.TabIndex = 457;
			this.Num_IoValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Num_IoValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Num_IoValue_KeyDown);
			// 
			// Btn_Set
			// 
			this.Btn_Set.BackColor = System.Drawing.Color.PaleTurquoise;
			this.Btn_Set.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Btn_Set.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
			this.Btn_Set.ForeColor = System.Drawing.Color.Black;
			this.Btn_Set.GlowColor = System.Drawing.Color.White;
			this.Btn_Set.Location = new System.Drawing.Point(94, 106);
			this.Btn_Set.Name = "Btn_Set";
			this.Btn_Set.OuterBorderColor = System.Drawing.Color.Transparent;
			this.Btn_Set.Size = new System.Drawing.Size(127, 54);
			this.Btn_Set.TabIndex = 458;
			this.Btn_Set.Text = "Set";
			this.Btn_Set.Click += new System.EventHandler(this.Btn_Set_Click);
			// 
			// Btn_Cancel
			// 
			this.Btn_Cancel.BackColor = System.Drawing.Color.PaleTurquoise;
			this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Btn_Cancel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
			this.Btn_Cancel.ForeColor = System.Drawing.Color.Black;
			this.Btn_Cancel.GlowColor = System.Drawing.Color.White;
			this.Btn_Cancel.Location = new System.Drawing.Point(222, 106);
			this.Btn_Cancel.Name = "Btn_Cancel";
			this.Btn_Cancel.OuterBorderColor = System.Drawing.Color.Transparent;
			this.Btn_Cancel.Size = new System.Drawing.Size(127, 54);
			this.Btn_Cancel.TabIndex = 459;
			this.Btn_Cancel.Text = "Cancel";
			this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
			// 
			// Form_SRZ_Output_Control
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(361, 173);
			this.ControlBox = false;
			this.Controls.Add(this.Btn_Cancel);
			this.Controls.Add(this.Btn_Set);
			this.Controls.Add(this.Num_IoValue);
			this.Controls.Add(this.Label_IO_Type);
			this.Controls.Add(this.Label_IO_Name);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label56);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form_SRZ_Output_Control";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Form_SRZ_Output_Control";
			this.Load += new System.EventHandler(this.Form_SRZ_Output_Control_Load);
			((System.ComponentModel.ISupportInitialize)(this.Num_IoValue)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label Label_IO_Name;
		private System.Windows.Forms.Label Label_IO_Type;
		private System.Windows.Forms.NumericUpDown Num_IoValue;
		private Windows.Win_GlassButton Btn_Set;
		private Windows.Win_GlassButton Btn_Cancel;
	}
}