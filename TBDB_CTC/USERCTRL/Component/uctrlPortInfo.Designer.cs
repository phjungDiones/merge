namespace TBDB_CTC.UserCtrl
{
    partial class uctrlPortInfo
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.a1Panel3 = new Owf.Controls.A1Panel();
            this.lbPortName = new System.Windows.Forms.Label();
            this.lbPortLockStatus = new System.Windows.Forms.Label();
            this.lbPortMID = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.lbPortLotID = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.lbPortPPID = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.lbPortPath = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.lbPortStatus = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.a1Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel5.Controls.Add(this.a1Panel3);
            this.panel5.Controls.Add(this.lbPortLockStatus);
            this.panel5.Controls.Add(this.lbPortMID);
            this.panel5.Controls.Add(this.label68);
            this.panel5.Controls.Add(this.lbPortLotID);
            this.panel5.Controls.Add(this.label65);
            this.panel5.Controls.Add(this.lbPortPPID);
            this.panel5.Controls.Add(this.label64);
            this.panel5.Controls.Add(this.lbPortPath);
            this.panel5.Controls.Add(this.label62);
            this.panel5.Controls.Add(this.lbPortStatus);
            this.panel5.Controls.Add(this.label71);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(299, 172);
            this.panel5.TabIndex = 964;
            // 
            // a1Panel3
            // 
            this.a1Panel3.BorderColor = System.Drawing.Color.White;
            this.a1Panel3.BorderWidth = 0;
            this.a1Panel3.Controls.Add(this.lbPortName);
            this.a1Panel3.GradientEndColor = System.Drawing.Color.Blue;
            this.a1Panel3.GradientStartColor = System.Drawing.Color.Black;
            this.a1Panel3.Image = null;
            this.a1Panel3.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel3.Location = new System.Drawing.Point(2, 0);
            this.a1Panel3.Margin = new System.Windows.Forms.Padding(2);
            this.a1Panel3.Name = "a1Panel3";
            this.a1Panel3.RoundCornerRadius = 6;
            this.a1Panel3.ShadowOffSet = 0;
            this.a1Panel3.Size = new System.Drawing.Size(295, 29);
            this.a1Panel3.TabIndex = 973;
            // 
            // lbPortName
            // 
            this.lbPortName.BackColor = System.Drawing.Color.Transparent;
            this.lbPortName.Font = new System.Drawing.Font("맑은 고딕", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPortName.ForeColor = System.Drawing.Color.White;
            this.lbPortName.Location = new System.Drawing.Point(2, 3);
            this.lbPortName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPortName.Name = "lbPortName";
            this.lbPortName.Size = new System.Drawing.Size(291, 23);
            this.lbPortName.TabIndex = 0;
            this.lbPortName.Text = "PortName";
            this.lbPortName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPortLockStatus
            // 
            this.lbPortLockStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lbPortLockStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPortLockStatus.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPortLockStatus.ForeColor = System.Drawing.Color.LightGray;
            this.lbPortLockStatus.Location = new System.Drawing.Point(2, 146);
            this.lbPortLockStatus.Name = "lbPortLockStatus";
            this.lbPortLockStatus.Size = new System.Drawing.Size(155, 26);
            this.lbPortLockStatus.TabIndex = 972;
            this.lbPortLockStatus.Text = "Lock Status";
            this.lbPortLockStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPortMID
            // 
            this.lbPortMID.BackColor = System.Drawing.Color.Transparent;
            this.lbPortMID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPortMID.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPortMID.ForeColor = System.Drawing.Color.LightGray;
            this.lbPortMID.Location = new System.Drawing.Point(107, 123);
            this.lbPortMID.Name = "lbPortMID";
            this.lbPortMID.Size = new System.Drawing.Size(190, 23);
            this.lbPortMID.TabIndex = 971;
            this.lbPortMID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.Transparent;
            this.label68.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label68.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label68.ForeColor = System.Drawing.Color.LightGray;
            this.label68.Location = new System.Drawing.Point(2, 123);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(105, 23);
            this.label68.TabIndex = 970;
            this.label68.Text = "MID";
            this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPortLotID
            // 
            this.lbPortLotID.BackColor = System.Drawing.Color.Transparent;
            this.lbPortLotID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPortLotID.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPortLotID.ForeColor = System.Drawing.Color.LightGray;
            this.lbPortLotID.Location = new System.Drawing.Point(107, 100);
            this.lbPortLotID.Name = "lbPortLotID";
            this.lbPortLotID.Size = new System.Drawing.Size(190, 23);
            this.lbPortLotID.TabIndex = 969;
            this.lbPortLotID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.Transparent;
            this.label65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label65.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label65.ForeColor = System.Drawing.Color.LightGray;
            this.label65.Location = new System.Drawing.Point(2, 100);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(105, 23);
            this.label65.TabIndex = 968;
            this.label65.Text = "LOT ID";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPortPPID
            // 
            this.lbPortPPID.BackColor = System.Drawing.Color.Transparent;
            this.lbPortPPID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPortPPID.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPortPPID.ForeColor = System.Drawing.Color.LightGray;
            this.lbPortPPID.Location = new System.Drawing.Point(107, 77);
            this.lbPortPPID.Name = "lbPortPPID";
            this.lbPortPPID.Size = new System.Drawing.Size(190, 23);
            this.lbPortPPID.TabIndex = 967;
            this.lbPortPPID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.Transparent;
            this.label64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label64.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label64.ForeColor = System.Drawing.Color.LightGray;
            this.label64.Location = new System.Drawing.Point(2, 77);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(105, 23);
            this.label64.TabIndex = 966;
            this.label64.Text = "PPID";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPortPath
            // 
            this.lbPortPath.BackColor = System.Drawing.Color.Transparent;
            this.lbPortPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPortPath.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPortPath.ForeColor = System.Drawing.Color.LightGray;
            this.lbPortPath.Location = new System.Drawing.Point(107, 54);
            this.lbPortPath.Name = "lbPortPath";
            this.lbPortPath.Size = new System.Drawing.Size(190, 23);
            this.lbPortPath.TabIndex = 965;
            this.lbPortPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label62.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label62.ForeColor = System.Drawing.Color.LightGray;
            this.label62.Location = new System.Drawing.Point(2, 54);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(105, 23);
            this.label62.TabIndex = 964;
            this.label62.Text = "PATH";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPortStatus
            // 
            this.lbPortStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbPortStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPortStatus.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPortStatus.ForeColor = System.Drawing.Color.LightGray;
            this.lbPortStatus.Location = new System.Drawing.Point(107, 31);
            this.lbPortStatus.Name = "lbPortStatus";
            this.lbPortStatus.Size = new System.Drawing.Size(190, 23);
            this.lbPortStatus.TabIndex = 963;
            this.lbPortStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.Transparent;
            this.label71.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label71.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label71.ForeColor = System.Drawing.Color.LightGray;
            this.label71.Location = new System.Drawing.Point(2, 31);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(105, 23);
            this.label71.TabIndex = 949;
            this.label71.Text = "STATUS";
            this.label71.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uctrlPortInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel5);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "uctrlPortInfo";
            this.Size = new System.Drawing.Size(299, 172);
            this.panel5.ResumeLayout(false);
            this.a1Panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbPortLockStatus;
        private System.Windows.Forms.Label lbPortMID;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label lbPortLotID;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label lbPortPPID;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label lbPortPath;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label lbPortStatus;
        private System.Windows.Forms.Label label71;
        private Owf.Controls.A1Panel a1Panel3;
        private System.Windows.Forms.Label lbPortName;
    }
}
