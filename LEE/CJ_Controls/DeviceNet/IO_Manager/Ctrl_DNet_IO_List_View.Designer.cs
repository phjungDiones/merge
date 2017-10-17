namespace CJ_Controls.DeviceNet
{
	partial class Ctrl_DNet_IO_List_View
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
            this.Panel_Input = new System.Windows.Forms.Panel();
            this.Tab_Input = new System.Windows.Forms.TabControl();
            this.label2 = new System.Windows.Forms.Label();
            this.Panel_Output = new System.Windows.Forms.Panel();
            this.Tab_Output = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel_Input.SuspendLayout();
            this.Panel_Output.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Input
            // 
            this.Panel_Input.Controls.Add(this.Tab_Input);
            this.Panel_Input.Controls.Add(this.label2);
            this.Panel_Input.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel_Input.Location = new System.Drawing.Point(0, 0);
            this.Panel_Input.Name = "Panel_Input";
            this.Panel_Input.Size = new System.Drawing.Size(273, 431);
            this.Panel_Input.TabIndex = 0;
            // 
            // Tab_Input
            // 
            this.Tab_Input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Input.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.Tab_Input.ItemSize = new System.Drawing.Size(150, 25);
            this.Tab_Input.Location = new System.Drawing.Point(0, 29);
            this.Tab_Input.Name = "Tab_Input";
            this.Tab_Input.SelectedIndex = 0;
            this.Tab_Input.Size = new System.Drawing.Size(273, 402);
            this.Tab_Input.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(273, 29);
            this.label2.TabIndex = 436;
            this.label2.Text = "Input";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_Output
            // 
            this.Panel_Output.Controls.Add(this.Tab_Output);
            this.Panel_Output.Controls.Add(this.label1);
            this.Panel_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Output.Location = new System.Drawing.Point(273, 0);
            this.Panel_Output.Name = "Panel_Output";
            this.Panel_Output.Size = new System.Drawing.Size(294, 431);
            this.Panel_Output.TabIndex = 1;
            // 
            // Tab_Output
            // 
            this.Tab_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Output.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.Tab_Output.ItemSize = new System.Drawing.Size(150, 25);
            this.Tab_Output.Location = new System.Drawing.Point(0, 29);
            this.Tab_Output.Name = "Tab_Output";
            this.Tab_Output.SelectedIndex = 0;
            this.Tab_Output.Size = new System.Drawing.Size(294, 402);
            this.Tab_Output.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 29);
            this.label1.TabIndex = 437;
            this.label1.Text = "Output";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Ctrl_DNet_IO_List_View
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.Panel_Output);
            this.Controls.Add(this.Panel_Input);
            this.Name = "Ctrl_DNet_IO_List_View";
            this.Size = new System.Drawing.Size(567, 431);
            this.Load += new System.EventHandler(this.Ctrl_IO_List_View_Load);
            this.Resize += new System.EventHandler(this.Ctrl_IO_List_View_Resize);
            this.Panel_Input.ResumeLayout(false);
            this.Panel_Output.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel Panel_Input;
		private System.Windows.Forms.Panel Panel_Output;
		private System.Windows.Forms.TabControl Tab_Input;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl Tab_Output;

	}
}
