namespace CJ_Controls.Vision.Aligner
{
    partial class PCam
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
            this.c_pAngle = new GUI.PValue();
            this.c_pE = new GUI.PValue();
            this.c_pInfro = new GUI.PScalar();
            this.c_pDist = new GUI.PScalar();
            this.c_pCal = new GUI.PScalar();
            this.c_pDiffer = new GUI.PScalar();
            this.c_pObject = new GUI.PScalar();
            this.c_pTarget = new GUI.PScalar();
            this.SuspendLayout();
            // 
            // c_pAngle
            // 
            this.c_pAngle.BolderColor = System.Drawing.Color.DarkGray;
            this.c_pAngle.Cal = 1;
            this.c_pAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_pAngle.ForeColor = System.Drawing.Color.Blue;
            this.c_pAngle.Format = "{0:F2}";
            this.c_pAngle.Location = new System.Drawing.Point(122, 228);
            this.c_pAngle.Max = 100000;
            this.c_pAngle.Min = -100000;
            this.c_pAngle.Name = "c_pAngle";
            this.c_pAngle.Size = new System.Drawing.Size(115, 37);
            this.c_pAngle.TabIndex = 7;
            this.c_pAngle.Text = "Angle";
            this.c_pAngle.Value = 0;
            this.c_pAngle.ValueColor = System.Drawing.Color.White;
            // 
            // c_pE
            // 
            this.c_pE.BolderColor = System.Drawing.Color.DarkGray;
            this.c_pE.Cal = 1;
            this.c_pE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_pE.ForeColor = System.Drawing.Color.Blue;
            this.c_pE.Format = "{0:F1}";
            this.c_pE.Location = new System.Drawing.Point(2, 228);
            this.c_pE.Max = 100000;
            this.c_pE.Min = -100000;
            this.c_pE.Name = "c_pE";
            this.c_pE.Size = new System.Drawing.Size(115, 37);
            this.c_pE.TabIndex = 6;
            this.c_pE.Text = "E";
            this.c_pE.Value = 0;
            this.c_pE.ValueColor = System.Drawing.Color.White;
            // 
            // c_pInfro
            // 
            this.c_pInfro.BackColor = System.Drawing.Color.LightGray;
            this.c_pInfro.BolderColor = System.Drawing.Color.DarkGray;
            this.c_pInfro.CalX = 1;
            this.c_pInfro.CalY = 1;
            this.c_pInfro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_pInfro.ForeColor = System.Drawing.Color.Blue;
            this.c_pInfro.FormatX = "X";
            this.c_pInfro.FormatY = "Y";
            this.c_pInfro.Location = new System.Drawing.Point(2, 3);
            this.c_pInfro.MaxX = 100000;
            this.c_pInfro.MaxY = 100000;
            this.c_pInfro.MinX = -100000;
            this.c_pInfro.MinY = -100000;
            this.c_pInfro.Name = "c_pInfro";
            this.c_pInfro.Scalar = null;
            this.c_pInfro.Size = new System.Drawing.Size(234, 37);
            this.c_pInfro.TabIndex = 5;
            this.c_pInfro.Text = "INF.";
            this.c_pInfro.ValueColor = System.Drawing.Color.LightGray;
            // 
            // c_pDist
            // 
            this.c_pDist.BackColor = System.Drawing.Color.LightGray;
            this.c_pDist.BolderColor = System.Drawing.Color.DarkGray;
            this.c_pDist.CalX = 1;
            this.c_pDist.CalY = 1;
            this.c_pDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_pDist.ForeColor = System.Drawing.Color.Blue;
            this.c_pDist.FormatX = "{0:F1}";
            this.c_pDist.FormatY = "{0:F1}";
            this.c_pDist.Location = new System.Drawing.Point(2, 190);
            this.c_pDist.MaxX = 100000;
            this.c_pDist.MaxY = 100000;
            this.c_pDist.MinX = -100000;
            this.c_pDist.MinY = -100000;
            this.c_pDist.Name = "c_pDist";
            this.c_pDist.Scalar = null;
            this.c_pDist.Size = new System.Drawing.Size(234, 37);
            this.c_pDist.TabIndex = 4;
            this.c_pDist.Text = "Dist.";
            this.c_pDist.ValueColor = System.Drawing.Color.WhiteSmoke;
            // 
            // c_pCal
            // 
            this.c_pCal.BackColor = System.Drawing.Color.LightGray;
            this.c_pCal.BolderColor = System.Drawing.Color.DarkGray;
            this.c_pCal.CalX = 1;
            this.c_pCal.CalY = 1;
            this.c_pCal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_pCal.ForeColor = System.Drawing.Color.Blue;
            this.c_pCal.FormatX = "{0:F3}";
            this.c_pCal.FormatY = "{0:F3}";
            this.c_pCal.Location = new System.Drawing.Point(2, 152);
            this.c_pCal.MaxX = 100000;
            this.c_pCal.MaxY = 100000;
            this.c_pCal.MinX = -100000;
            this.c_pCal.MinY = -100000;
            this.c_pCal.Name = "c_pCal";
            this.c_pCal.Scalar = null;
            this.c_pCal.Size = new System.Drawing.Size(234, 37);
            this.c_pCal.TabIndex = 3;
            this.c_pCal.Text = "Cal.";
            this.c_pCal.ValueColor = System.Drawing.Color.WhiteSmoke;
            // 
            // c_pDiffer
            // 
            this.c_pDiffer.BackColor = System.Drawing.Color.LightGray;
            this.c_pDiffer.BolderColor = System.Drawing.Color.DarkGray;
            this.c_pDiffer.CalX = 1;
            this.c_pDiffer.CalY = 1;
            this.c_pDiffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_pDiffer.ForeColor = System.Drawing.Color.Blue;
            this.c_pDiffer.FormatX = "{0:F1}";
            this.c_pDiffer.FormatY = "{0:F1}";
            this.c_pDiffer.Location = new System.Drawing.Point(2, 114);
            this.c_pDiffer.MaxX = 100000;
            this.c_pDiffer.MaxY = 100000;
            this.c_pDiffer.MinX = -100000;
            this.c_pDiffer.MinY = -100000;
            this.c_pDiffer.Name = "c_pDiffer";
            this.c_pDiffer.Scalar = null;
            this.c_pDiffer.Size = new System.Drawing.Size(234, 37);
            this.c_pDiffer.TabIndex = 2;
            this.c_pDiffer.Text = "Differ";
            this.c_pDiffer.ValueColor = System.Drawing.Color.WhiteSmoke;
            // 
            // c_pObject
            // 
            this.c_pObject.BackColor = System.Drawing.Color.LightGray;
            this.c_pObject.BolderColor = System.Drawing.Color.DarkGray;
            this.c_pObject.CalX = 1;
            this.c_pObject.CalY = 1;
            this.c_pObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_pObject.ForeColor = System.Drawing.Color.Blue;
            this.c_pObject.FormatX = "{0:F1}";
            this.c_pObject.FormatY = "{0:F1}";
            this.c_pObject.Location = new System.Drawing.Point(2, 77);
            this.c_pObject.MaxX = 100000;
            this.c_pObject.MaxY = 100000;
            this.c_pObject.MinX = -100000;
            this.c_pObject.MinY = -100000;
            this.c_pObject.Name = "c_pObject";
            this.c_pObject.Scalar = null;
            this.c_pObject.Size = new System.Drawing.Size(234, 37);
            this.c_pObject.TabIndex = 1;
            this.c_pObject.Text = "Object";
            this.c_pObject.ValueColor = System.Drawing.Color.WhiteSmoke;
            // 
            // c_pTarget
            // 
            this.c_pTarget.BackColor = System.Drawing.Color.LightGray;
            this.c_pTarget.BolderColor = System.Drawing.Color.DarkGray;
            this.c_pTarget.CalX = 1;
            this.c_pTarget.CalY = 1;
            this.c_pTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_pTarget.ForeColor = System.Drawing.Color.Blue;
            this.c_pTarget.FormatX = "{0:F1}";
            this.c_pTarget.FormatY = "{0:F1}";
            this.c_pTarget.Location = new System.Drawing.Point(2, 40);
            this.c_pTarget.MaxX = 100000;
            this.c_pTarget.MaxY = 100000;
            this.c_pTarget.MinX = -100000;
            this.c_pTarget.MinY = -100000;
            this.c_pTarget.Name = "c_pTarget";
            this.c_pTarget.Scalar = null;
            this.c_pTarget.Size = new System.Drawing.Size(234, 37);
            this.c_pTarget.TabIndex = 0;
            this.c_pTarget.Text = "Target";
            this.c_pTarget.ValueColor = System.Drawing.Color.WhiteSmoke;
            // 
            // PCam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.c_pAngle);
            this.Controls.Add(this.c_pE);
            this.Controls.Add(this.c_pInfro);
            this.Controls.Add(this.c_pDist);
            this.Controls.Add(this.c_pCal);
            this.Controls.Add(this.c_pDiffer);
            this.Controls.Add(this.c_pObject);
            this.Controls.Add(this.c_pTarget);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PCam";
            this.Size = new System.Drawing.Size(241, 269);
            this.Resize += new System.EventHandler(this.PCam_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private GUI.PScalar c_pTarget;
        private GUI.PScalar c_pObject;
        private GUI.PScalar c_pDiffer;
        private GUI.PScalar c_pCal;
        private GUI.PScalar c_pDist;
        private GUI.PScalar c_pInfro;
        private GUI.PValue c_pE;
        private GUI.PValue c_pAngle;

    }
}
