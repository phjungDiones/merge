namespace CJ_Controls.Vision.Viewer
{
    partial class Display
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Display));
            this.panCaption = new System.Windows.Forms.Panel();
            this.panPattern = new System.Windows.Forms.Panel();
            this.panMenu = new System.Windows.Forms.Panel();
            this.cmdZoom100 = new GUI.Button();
            this.cmdFit = new GUI.Button();
            this.cmdZoomOut = new GUI.Button();
            this.cmdZoomIn = new GUI.Button();
            this.cmdPan = new GUI.Button();
            this.cmdPoint = new GUI.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c_View = new View();
            this.panMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panCaption
            // 
            this.panCaption.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panCaption.BackgroundImage")));
            this.panCaption.Dock = System.Windows.Forms.DockStyle.Right;
            this.panCaption.Location = new System.Drawing.Point(208, 0);
            this.panCaption.Name = "panCaption";
            this.panCaption.Size = new System.Drawing.Size(159, 22);
            this.panCaption.TabIndex = 1;
            // 
            // panPattern
            // 
            this.panPattern.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panPattern.BackgroundImage")));
            this.panPattern.Dock = System.Windows.Forms.DockStyle.Left;
            this.panPattern.Location = new System.Drawing.Point(0, 0);
            this.panPattern.Name = "panPattern";
            this.panPattern.Size = new System.Drawing.Size(63, 22);
            this.panPattern.TabIndex = 0;
            // 
            // panMenu
            // 
            this.panMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panMenu.BackgroundImage")));
            this.panMenu.Controls.Add(this.cmdZoom100);
            this.panMenu.Controls.Add(this.cmdFit);
            this.panMenu.Controls.Add(this.cmdZoomOut);
            this.panMenu.Controls.Add(this.cmdZoomIn);
            this.panMenu.Controls.Add(this.cmdPan);
            this.panMenu.Controls.Add(this.cmdPoint);
            this.panMenu.Controls.Add(this.panCaption);
            this.panMenu.Controls.Add(this.panPattern);
            this.panMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panMenu.Location = new System.Drawing.Point(0, 381);
            this.panMenu.Name = "panMenu";
            this.panMenu.Size = new System.Drawing.Size(367, 22);
            this.panMenu.TabIndex = 3;
            // 
            // cmdZoom100
            // 
            this.cmdZoom100.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdZoom100.BackgroundImage")));
            this.cmdZoom100.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdZoom100.BorderColor = System.Drawing.Color.Blue;
            this.cmdZoom100.FillColor = System.Drawing.Color.AliceBlue;
            this.cmdZoom100.FillImage = ((System.Drawing.Image)(resources.GetObject("cmdZoom100.FillImage")));
            this.cmdZoom100.Location = new System.Drawing.Point(164, 1);
            this.cmdZoom100.MouseUpColor = System.Drawing.Color.OrangeRed;
            this.cmdZoom100.MouseUpImage = null;
            this.cmdZoom100.Name = "cmdZoom100";
            this.cmdZoom100.PushColor = System.Drawing.Color.LightGray;
            this.cmdZoom100.Round = 9;
            this.cmdZoom100.Shape = GUI.Button.SHAPE.RECTANGLE;
            this.cmdZoom100.Size = new System.Drawing.Size(16, 22);
            this.cmdZoom100.State = GUI.Button.STATE.OFF;
            this.cmdZoom100.StateOffImage = null;
            this.cmdZoom100.StateOnImage = null;
            this.cmdZoom100.StatePos = new System.Drawing.Point(0, 0);
            this.cmdZoom100.Style = GUI.Button.STYLE.IMAGE_PUSH;
            this.cmdZoom100.TabIndex = 13;
            this.cmdZoom100.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cmdZoom100.Value = GUI.Button.STATE.OFF;
            this.cmdZoom100.Click += new System.EventHandler(this.cmdZoom100_Click);
            // 
            // cmdFit
            // 
            this.cmdFit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdFit.BackgroundImage")));
            this.cmdFit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdFit.BorderColor = System.Drawing.Color.Blue;
            this.cmdFit.FillColor = System.Drawing.Color.AliceBlue;
            this.cmdFit.FillImage = ((System.Drawing.Image)(resources.GetObject("cmdFit.FillImage")));
            this.cmdFit.Location = new System.Drawing.Point(147, 1);
            this.cmdFit.MouseUpColor = System.Drawing.Color.OrangeRed;
            this.cmdFit.MouseUpImage = null;
            this.cmdFit.Name = "cmdFit";
            this.cmdFit.PushColor = System.Drawing.Color.LightGray;
            this.cmdFit.Round = 9;
            this.cmdFit.Shape = GUI.Button.SHAPE.RECTANGLE;
            this.cmdFit.Size = new System.Drawing.Size(16, 22);
            this.cmdFit.State = GUI.Button.STATE.OFF;
            this.cmdFit.StateOffImage = null;
            this.cmdFit.StateOnImage = null;
            this.cmdFit.StatePos = new System.Drawing.Point(0, 0);
            this.cmdFit.Style = GUI.Button.STYLE.IMAGE_PUSH;
            this.cmdFit.TabIndex = 12;
            this.cmdFit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cmdFit.Value = GUI.Button.STATE.OFF;
            this.cmdFit.Click += new System.EventHandler(this.cmdFit_Click);
            // 
            // cmdZoomOut
            // 
            this.cmdZoomOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdZoomOut.BackgroundImage")));
            this.cmdZoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdZoomOut.BorderColor = System.Drawing.Color.Blue;
            this.cmdZoomOut.FillColor = System.Drawing.Color.AliceBlue;
            this.cmdZoomOut.FillImage = ((System.Drawing.Image)(resources.GetObject("cmdZoomOut.FillImage")));
            this.cmdZoomOut.Location = new System.Drawing.Point(128, 1);
            this.cmdZoomOut.MouseUpColor = System.Drawing.Color.OrangeRed;
            this.cmdZoomOut.MouseUpImage = null;
            this.cmdZoomOut.Name = "cmdZoomOut";
            this.cmdZoomOut.PushColor = System.Drawing.Color.LightGray;
            this.cmdZoomOut.Round = 9;
            this.cmdZoomOut.Shape = GUI.Button.SHAPE.RECTANGLE;
            this.cmdZoomOut.Size = new System.Drawing.Size(16, 22);
            this.cmdZoomOut.State = GUI.Button.STATE.OFF;
            this.cmdZoomOut.StateOffImage = null;
            this.cmdZoomOut.StateOnImage = null;
            this.cmdZoomOut.StatePos = new System.Drawing.Point(0, 0);
            this.cmdZoomOut.Style = GUI.Button.STYLE.IMAGE_TOGGLE;
            this.cmdZoomOut.TabIndex = 11;
            this.cmdZoomOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cmdZoomOut.Value = GUI.Button.STATE.OFF;
            this.cmdZoomOut.Click += new System.EventHandler(this.cmdZoomOut_Click);
            // 
            // cmdZoomIn
            // 
            this.cmdZoomIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdZoomIn.BackgroundImage")));
            this.cmdZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdZoomIn.BorderColor = System.Drawing.Color.Blue;
            this.cmdZoomIn.FillColor = System.Drawing.Color.AliceBlue;
            this.cmdZoomIn.FillImage = ((System.Drawing.Image)(resources.GetObject("cmdZoomIn.FillImage")));
            this.cmdZoomIn.Location = new System.Drawing.Point(111, 1);
            this.cmdZoomIn.MouseUpColor = System.Drawing.Color.OrangeRed;
            this.cmdZoomIn.MouseUpImage = null;
            this.cmdZoomIn.Name = "cmdZoomIn";
            this.cmdZoomIn.PushColor = System.Drawing.Color.LightGray;
            this.cmdZoomIn.Round = 9;
            this.cmdZoomIn.Shape = GUI.Button.SHAPE.RECTANGLE;
            this.cmdZoomIn.Size = new System.Drawing.Size(16, 22);
            this.cmdZoomIn.State = GUI.Button.STATE.OFF;
            this.cmdZoomIn.StateOffImage = null;
            this.cmdZoomIn.StateOnImage = null;
            this.cmdZoomIn.StatePos = new System.Drawing.Point(0, 0);
            this.cmdZoomIn.Style = GUI.Button.STYLE.IMAGE_TOGGLE;
            this.cmdZoomIn.TabIndex = 10;
            this.cmdZoomIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cmdZoomIn.Value = GUI.Button.STATE.OFF;
            this.cmdZoomIn.Click += new System.EventHandler(this.cmdZoomIn_Click);
            // 
            // cmdPan
            // 
            this.cmdPan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdPan.BackgroundImage")));
            this.cmdPan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdPan.BorderColor = System.Drawing.Color.Blue;
            this.cmdPan.FillColor = System.Drawing.Color.AliceBlue;
            this.cmdPan.FillImage = ((System.Drawing.Image)(resources.GetObject("cmdPan.FillImage")));
            this.cmdPan.Location = new System.Drawing.Point(93, 1);
            this.cmdPan.MouseUpColor = System.Drawing.Color.OrangeRed;
            this.cmdPan.MouseUpImage = null;
            this.cmdPan.Name = "cmdPan";
            this.cmdPan.PushColor = System.Drawing.Color.LightGray;
            this.cmdPan.Round = 9;
            this.cmdPan.Shape = GUI.Button.SHAPE.RECTANGLE;
            this.cmdPan.Size = new System.Drawing.Size(16, 22);
            this.cmdPan.State = GUI.Button.STATE.OFF;
            this.cmdPan.StateOffImage = null;
            this.cmdPan.StateOnImage = null;
            this.cmdPan.StatePos = new System.Drawing.Point(0, 0);
            this.cmdPan.Style = GUI.Button.STYLE.IMAGE_TOGGLE;
            this.cmdPan.TabIndex = 9;
            this.cmdPan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cmdPan.Value = GUI.Button.STATE.OFF;
            this.cmdPan.Click += new System.EventHandler(this.cmdPan_Click);
            // 
            // cmdPoint
            // 
            this.cmdPoint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdPoint.BackgroundImage")));
            this.cmdPoint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdPoint.BorderColor = System.Drawing.Color.Blue;
            this.cmdPoint.FillColor = System.Drawing.Color.AliceBlue;
            this.cmdPoint.FillImage = ((System.Drawing.Image)(resources.GetObject("cmdPoint.FillImage")));
            this.cmdPoint.Location = new System.Drawing.Point(76, 1);
            this.cmdPoint.MouseUpColor = System.Drawing.Color.OrangeRed;
            this.cmdPoint.MouseUpImage = null;
            this.cmdPoint.Name = "cmdPoint";
            this.cmdPoint.PushColor = System.Drawing.Color.LightGray;
            this.cmdPoint.Round = 9;
            this.cmdPoint.Shape = GUI.Button.SHAPE.RECTANGLE;
            this.cmdPoint.Size = new System.Drawing.Size(16, 22);
            this.cmdPoint.State = GUI.Button.STATE.OFF;
            this.cmdPoint.StateOffImage = null;
            this.cmdPoint.StateOnImage = null;
            this.cmdPoint.StatePos = new System.Drawing.Point(0, 0);
            this.cmdPoint.Style = GUI.Button.STYLE.IMAGE_TOGGLE;
            this.cmdPoint.TabIndex = 8;
            this.cmdPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cmdPoint.Value = GUI.Button.STATE.ON;
            this.cmdPoint.Click += new System.EventHandler(this.cmdPoint_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c_View);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(367, 381);
            this.panel1.TabIndex = 4;
            // 
            // c_View
            // 
            this.c_View.Bitmap = null;
            this.c_View.ControlMode = View.E_VIEW_CONTROL_MODE.VCM_POINT;
            this.c_View.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.c_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_View.Location = new System.Drawing.Point(0, 0);
            this.c_View.Name = "c_View";
            this.c_View.Overlay = null;
            this.c_View.OverlayControl = false;
            this.c_View.Size = new System.Drawing.Size(367, 381);
            this.c_View.Static = null;
            this.c_View.TabIndex = 0;
            this.c_View.Text = "view1";
            this.c_View.Transparency = 255;
            this.c_View.Zoom = 0F;
            this.c_View.ChangeDraw += new ChangeDraw(this.c_View_ChangeDraw);
            this.c_View.MouseUp += new System.Windows.Forms.MouseEventHandler(this.c_View_MouseUp);
            this.c_View.ChangeSelect += new ChangeSelect(this.c_View_ChangeSelect);
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panMenu);
            this.Name = "Display";
            this.Size = new System.Drawing.Size(367, 403);
            this.Resize += new System.EventHandler(this.Display_Resize);
            this.panMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panCaption;
        private System.Windows.Forms.Panel panPattern;
        private System.Windows.Forms.Panel panMenu;
        private GUI.Button cmdZoom100;
        private GUI.Button cmdFit;
        private GUI.Button cmdZoomOut;
        private GUI.Button cmdZoomIn;
        private GUI.Button cmdPan;
        private GUI.Button cmdPoint;
        private System.Windows.Forms.Panel panel1;
        private View c_View;
    }
}
