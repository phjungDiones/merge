namespace CJ_Controls.Etc
{
	partial class LogoControl
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
			this.lbBuildDate = new System.Windows.Forms.Label();
			this.lbVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbBuildDate
			// 
			this.lbBuildDate.BackColor = System.Drawing.Color.Transparent;
			this.lbBuildDate.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbBuildDate.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbBuildDate.ForeColor = System.Drawing.Color.Gainsboro;
			this.lbBuildDate.Location = new System.Drawing.Point(0, 0);
			this.lbBuildDate.Margin = new System.Windows.Forms.Padding(0);
			this.lbBuildDate.Name = "lbBuildDate";
			this.lbBuildDate.Size = new System.Drawing.Size(92, 15);
			this.lbBuildDate.TabIndex = 4;
			this.lbBuildDate.Text = "Date: 2016.08.19";
			this.lbBuildDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbVersion
			// 
			this.lbVersion.BackColor = System.Drawing.Color.Transparent;
			this.lbVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lbVersion.Font = new System.Drawing.Font("굴림", 9F);
			this.lbVersion.ForeColor = System.Drawing.Color.White;
			this.lbVersion.Location = new System.Drawing.Point(0, 69);
			this.lbVersion.Margin = new System.Windows.Forms.Padding(0);
			this.lbVersion.Name = "lbVersion";
			this.lbVersion.Size = new System.Drawing.Size(92, 26);
			this.lbVersion.TabIndex = 3;
			this.lbVersion.Text = "Ver. 1.0.0";
			this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LogoControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImage = global::CJ_Controls.Properties.Resources.Logo;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.Controls.Add(this.lbBuildDate);
			this.Controls.Add(this.lbVersion);
			this.Name = "LogoControl";
			this.Size = new System.Drawing.Size(92, 95);
			this.Load += new System.EventHandler(this.LogoControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbBuildDate;
		private System.Windows.Forms.Label lbVersion;
	}
}
