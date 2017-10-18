namespace CJ_Controls.WinControls.Config
{
	partial class Ctrl_Set_String
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
			this.Panel_Value = new System.Windows.Forms.Panel();
			this.Text_Value = new System.Windows.Forms.TextBox();
			this.Panel_Name = new System.Windows.Forms.Panel();
			this.Label_Name = new System.Windows.Forms.Label();
			this.Panel_Value.SuspendLayout();
			this.Panel_Name.SuspendLayout();
			this.SuspendLayout();
			// 
			// Panel_Value
			// 
			this.Panel_Value.Controls.Add(this.Text_Value);
			this.Panel_Value.Dock = System.Windows.Forms.DockStyle.Right;
			this.Panel_Value.Location = new System.Drawing.Point(259, 2);
			this.Panel_Value.Name = "Panel_Value";
			this.Panel_Value.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.Panel_Value.Size = new System.Drawing.Size(180, 28);
			this.Panel_Value.TabIndex = 2;
			// 
			// Text_Value
			// 
			this.Text_Value.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Text_Value.Location = new System.Drawing.Point(2, 0);
			this.Text_Value.Name = "Text_Value";
			this.Text_Value.Size = new System.Drawing.Size(178, 27);
			this.Text_Value.TabIndex = 0;
			this.Text_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_Value_KeyDown);
			// 
			// Panel_Name
			// 
			this.Panel_Name.Controls.Add(this.Label_Name);
			this.Panel_Name.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Panel_Name.Location = new System.Drawing.Point(2, 2);
			this.Panel_Name.Name = "Panel_Name";
			this.Panel_Name.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.Panel_Name.Size = new System.Drawing.Size(257, 28);
			this.Panel_Name.TabIndex = 3;
			// 
			// Label_Name
			// 
			this.Label_Name.BackColor = System.Drawing.Color.Transparent;
			this.Label_Name.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Label_Name.Location = new System.Drawing.Point(2, 0);
			this.Label_Name.Name = "Label_Name";
			this.Label_Name.Size = new System.Drawing.Size(255, 28);
			this.Label_Name.TabIndex = 0;
			this.Label_Name.Text = "Option Name";
			this.Label_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Ctrl_Set_String
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.Panel_Name);
			this.Controls.Add(this.Panel_Value);
			this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Ctrl_Set_String";
			this.Padding = new System.Windows.Forms.Padding(2);
			this.Size = new System.Drawing.Size(441, 32);
			this.Panel_Value.ResumeLayout(false);
			this.Panel_Value.PerformLayout();
			this.Panel_Name.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel Panel_Value;
		private System.Windows.Forms.TextBox Text_Value;
		private System.Windows.Forms.Panel Panel_Name;
		private System.Windows.Forms.Label Label_Name;
	}
}
