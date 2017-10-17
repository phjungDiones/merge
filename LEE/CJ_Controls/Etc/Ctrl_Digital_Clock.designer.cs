namespace CJ_Controls.Etc
{
	partial class Ctrl_Digital_Clock
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
			this.components = new System.ComponentModel.Container();
			this.LayoutPanel_Time = new CJ_Controls.Windows.Win_QuickTableLayoutPanel();
			this.Label_YMD = new CJ_Controls.Windows.Win_LedLabel();
			this.Label_Time = new CJ_Controls.Windows.Win_LedLabel();
			this.Timer_EveryTime = new System.Windows.Forms.Timer(this.components);
			this.LayoutPanel_Time.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Label_YMD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Label_Time)).BeginInit();
			this.SuspendLayout();
			// 
			// LayoutPanel_Time
			// 
			this.LayoutPanel_Time.BackColor = System.Drawing.Color.Transparent;
			this.LayoutPanel_Time.ColumnCount = 1;
			this.LayoutPanel_Time.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.LayoutPanel_Time.Controls.Add(this.Label_YMD, 0, 0);
			this.LayoutPanel_Time.Controls.Add(this.Label_Time, 0, 1);
			this.LayoutPanel_Time.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutPanel_Time.Location = new System.Drawing.Point(0, 0);
			this.LayoutPanel_Time.Name = "LayoutPanel_Time";
			this.LayoutPanel_Time.RowCount = 2;
			this.LayoutPanel_Time.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.LayoutPanel_Time.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.LayoutPanel_Time.Size = new System.Drawing.Size(343, 129);
			this.LayoutPanel_Time.TabIndex = 335;
			// 
			// Label_YMD
			// 
			this.Label_YMD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Label_YMD.BackColor = System.Drawing.Color.Transparent;
			this.Label_YMD.BackColor_1 = System.Drawing.Color.Transparent;
			this.Label_YMD.BackColor_2 = System.Drawing.Color.Transparent;
			this.Label_YMD.BevelRate = 0.5F;
			this.Label_YMD.BorderColor = System.Drawing.Color.Transparent;
			this.Label_YMD.FadedColor = System.Drawing.Color.Transparent;
			this.Label_YMD.FocusedBorderColor = System.Drawing.Color.Transparent;
			this.Label_YMD.ForeColor = System.Drawing.Color.Black;
			this.Label_YMD.HighlightOpaque = ((byte)(50));
			this.Label_YMD.Location = new System.Drawing.Point(3, 3);
			this.Label_YMD.Name = "Label_YMD";
			this.Label_YMD.RoundCorner = true;
			this.Label_YMD.ShowHighlight = true;
			this.Label_YMD.Size = new System.Drawing.Size(337, 58);
			this.Label_YMD.TabIndex = 0;
			this.Label_YMD.Text = "0000-00-00";
			this.Label_YMD.TextAlignment = CJ_Controls.Windows.Win_LedLabel.Alignment.Right;
			this.Label_YMD.TotalCharCount = 10;
			// 
			// Label_Time
			// 
			this.Label_Time.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Label_Time.BackColor = System.Drawing.Color.Transparent;
			this.Label_Time.BackColor_1 = System.Drawing.Color.Transparent;
			this.Label_Time.BackColor_2 = System.Drawing.Color.Transparent;
			this.Label_Time.BevelRate = 0.5F;
			this.Label_Time.BorderColor = System.Drawing.Color.Transparent;
			this.Label_Time.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label_Time.FadedColor = System.Drawing.Color.Transparent;
			this.Label_Time.FocusedBorderColor = System.Drawing.Color.Transparent;
			this.Label_Time.ForeColor = System.Drawing.Color.Black;
			this.Label_Time.HighlightOpaque = ((byte)(50));
			this.Label_Time.Location = new System.Drawing.Point(3, 67);
			this.Label_Time.Name = "Label_Time";
			this.Label_Time.RoundCorner = true;
			this.Label_Time.ShowHighlight = true;
			this.Label_Time.Size = new System.Drawing.Size(337, 59);
			this.Label_Time.TabIndex = 1;
			this.Label_Time.Text = "00:00:00.0";
			this.Label_Time.TextAlignment = CJ_Controls.Windows.Win_LedLabel.Alignment.Right;
			this.Label_Time.TotalCharCount = 10;
			// 
			// Timer_EveryTime
			// 
			this.Timer_EveryTime.Enabled = true;
			this.Timer_EveryTime.Interval = 1000;
			this.Timer_EveryTime.Tick += new System.EventHandler(this.Timer_EveryTime_Tick);
			// 
			// Ctrl_Digital_Clock
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Transparent;
			this.BackgroundImage = global::CJ_Controls.Properties.Resources.Clock;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.Controls.Add(this.LayoutPanel_Time);
			this.Name = "Ctrl_Digital_Clock";
			this.Size = new System.Drawing.Size(343, 129);
			this.LayoutPanel_Time.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Label_YMD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Label_Time)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private CJ_Controls.Windows.Win_QuickTableLayoutPanel LayoutPanel_Time;
		private CJ_Controls.Windows.Win_LedLabel Label_YMD;
		private CJ_Controls.Windows.Win_LedLabel Label_Time;
		private System.Windows.Forms.Timer Timer_EveryTime;
	}
}
