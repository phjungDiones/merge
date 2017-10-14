namespace CJ_Controls.WinControls.Config
{
	partial class Ctrl_Set_H_TouchInput
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
			this.Label_Name = new System.Windows.Forms.Label();
			this.Panel_Value = new System.Windows.Forms.Panel();
			this.Label_Value = new System.Windows.Forms.Label();
			this.Panel_Value.SuspendLayout();
			this.SuspendLayout();
			// 
			// Label_Name
			// 
			this.Label_Name.BackColor = System.Drawing.Color.Transparent;
			this.Label_Name.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Label_Name.Location = new System.Drawing.Point(0, 0);
			this.Label_Name.Margin = new System.Windows.Forms.Padding(3);
			this.Label_Name.Name = "Label_Name";
			this.Label_Name.Size = new System.Drawing.Size(220, 42);
			this.Label_Name.TabIndex = 1;
			this.Label_Name.Text = "Option Name";
			this.Label_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Label_Name.DoubleClick += new System.EventHandler(this.Label_Value_DoubleClick);
			// 
			// Panel_Value
			// 
			this.Panel_Value.Controls.Add(this.Label_Value);
			this.Panel_Value.Dock = System.Windows.Forms.DockStyle.Right;
			this.Panel_Value.Location = new System.Drawing.Point(220, 0);
			this.Panel_Value.Margin = new System.Windows.Forms.Padding(0);
			this.Panel_Value.Name = "Panel_Value";
			this.Panel_Value.Padding = new System.Windows.Forms.Padding(3);
			this.Panel_Value.Size = new System.Drawing.Size(180, 42);
			this.Panel_Value.TabIndex = 3;
			// 
			// Label_Value
			// 
			this.Label_Value.BackColor = System.Drawing.Color.White;
			this.Label_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label_Value.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Label_Value.Location = new System.Drawing.Point(3, 3);
			this.Label_Value.Margin = new System.Windows.Forms.Padding(0);
			this.Label_Value.Name = "Label_Value";
			this.Label_Value.Size = new System.Drawing.Size(174, 36);
			this.Label_Value.TabIndex = 1;
			this.Label_Value.Text = "Value";
			this.Label_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Label_Value.Click += new System.EventHandler(this.Label_Value_Click);
			// 
			// Ctrl_Set_TouchInput
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.Label_Name);
			this.Controls.Add(this.Panel_Value);
			this.Name = "Ctrl_Set_TouchInput";
			this.Size = new System.Drawing.Size(400, 42);
			this.Panel_Value.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label Label_Name;
		private System.Windows.Forms.Panel Panel_Value;
		private System.Windows.Forms.Label Label_Value;
	}
}
