namespace TBDB_CTC.UserCtrl.SubForm.RecipeSub
{
    partial class subRecipeProcLami
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.listboxData = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtProcessTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtPressure = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pll = new System.Windows.Forms.Panel();
            this.txtUpperTemp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtLowerTemp = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtPressTime = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pll.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.txtName);
            this.panel4.Location = new System.Drawing.Point(458, 51);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(117, 40);
            this.panel4.TabIndex = 1130;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtName.Location = new System.Drawing.Point(3, 2);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(110, 29);
            this.txtName.TabIndex = 920;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(253, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 40);
            this.label1.TabIndex = 1128;
            this.label1.Text = "LAMINATOR INFO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.Location = new System.Drawing.Point(3, 694);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(242, 79);
            this.btnDelete.TabIndex = 1127;
            this.btnDelete.Text = "delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.Location = new System.Drawing.Point(1071, 694);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(240, 79);
            this.btnSave.TabIndex = 1126;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // listboxData
            // 
            this.listboxData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listboxData.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listboxData.FormattingEnabled = true;
            this.listboxData.ItemHeight = 21;
            this.listboxData.Items.AddRange(new object[] {
            "Test"});
            this.listboxData.Location = new System.Drawing.Point(3, 4);
            this.listboxData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listboxData.Name = "listboxData";
            this.listboxData.Size = new System.Drawing.Size(242, 674);
            this.listboxData.TabIndex = 1125;
            this.listboxData.SelectedIndexChanged += new System.EventHandler(this.listboxData_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtProcessTime);
            this.panel1.Location = new System.Drawing.Point(458, 95);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(117, 40);
            this.panel1.TabIndex = 1132;
            // 
            // txtProcessTime
            // 
            this.txtProcessTime.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtProcessTime.Location = new System.Drawing.Point(3, 2);
            this.txtProcessTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProcessTime.Name = "txtProcessTime";
            this.txtProcessTime.Size = new System.Drawing.Size(110, 29);
            this.txtProcessTime.TabIndex = 920;
            this.txtProcessTime.Click += new System.EventHandler(this.tbAlngeOffset_Click);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(253, 51);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(207, 40);
            this.label6.TabIndex = 1129;
            this.label6.Text = "Name";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(253, 95);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(207, 40);
            this.label2.TabIndex = 1131;
            this.label2.Text = "Process Time";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtPressure);
            this.panel2.Location = new System.Drawing.Point(458, 139);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(117, 40);
            this.panel2.TabIndex = 1134;
            // 
            // txtPressure
            // 
            this.txtPressure.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPressure.Location = new System.Drawing.Point(3, 2);
            this.txtPressure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPressure.Name = "txtPressure";
            this.txtPressure.Size = new System.Drawing.Size(110, 29);
            this.txtPressure.TabIndex = 920;
            this.txtPressure.Click += new System.EventHandler(this.tbAlngeOffset_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(253, 139);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(207, 40);
            this.label3.TabIndex = 1133;
            this.label3.Text = "Pressure";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(583, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 1135;
            this.label4.Text = "sec";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(583, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 15);
            this.label5.TabIndex = 1136;
            this.label5.Text = "t";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(583, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 15);
            this.label7.TabIndex = 1139;
            this.label7.Text = "℃";
            // 
            // pll
            // 
            this.pll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pll.Controls.Add(this.txtUpperTemp);
            this.pll.Location = new System.Drawing.Point(458, 182);
            this.pll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pll.Name = "pll";
            this.pll.Size = new System.Drawing.Size(117, 40);
            this.pll.TabIndex = 1138;
            // 
            // txtUpperTemp
            // 
            this.txtUpperTemp.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtUpperTemp.Location = new System.Drawing.Point(3, 2);
            this.txtUpperTemp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUpperTemp.Name = "txtUpperTemp";
            this.txtUpperTemp.Size = new System.Drawing.Size(110, 29);
            this.txtUpperTemp.TabIndex = 920;
            this.txtUpperTemp.Click += new System.EventHandler(this.tbAlngeOffset_Click);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(253, 182);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.label8.Size = new System.Drawing.Size(207, 40);
            this.label8.TabIndex = 1137;
            this.label8.Text = "Upper Temp";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(583, 239);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 15);
            this.label9.TabIndex = 1142;
            this.label9.Text = "℃";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.txtLowerTemp);
            this.panel5.Location = new System.Drawing.Point(458, 225);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(117, 40);
            this.panel5.TabIndex = 1141;
            // 
            // txtLowerTemp
            // 
            this.txtLowerTemp.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtLowerTemp.Location = new System.Drawing.Point(3, 2);
            this.txtLowerTemp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLowerTemp.Name = "txtLowerTemp";
            this.txtLowerTemp.Size = new System.Drawing.Size(110, 29);
            this.txtLowerTemp.TabIndex = 920;
            this.txtLowerTemp.Click += new System.EventHandler(this.tbAlngeOffset_Click);
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(253, 225);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.label10.Size = new System.Drawing.Size(207, 40);
            this.label10.TabIndex = 1140;
            this.label10.Text = "Lower Temp";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(583, 282);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 15);
            this.label11.TabIndex = 1145;
            this.label11.Text = "sec";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.txtPressTime);
            this.panel6.Location = new System.Drawing.Point(458, 269);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(117, 40);
            this.panel6.TabIndex = 1144;
            // 
            // txtPressTime
            // 
            this.txtPressTime.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPressTime.Location = new System.Drawing.Point(3, 2);
            this.txtPressTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPressTime.Name = "txtPressTime";
            this.txtPressTime.Size = new System.Drawing.Size(110, 29);
            this.txtPressTime.TabIndex = 920;
            this.txtPressTime.Click += new System.EventHandler(this.tbAlngeOffset_Click);
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(253, 269);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.label12.Size = new System.Drawing.Size(207, 40);
            this.label12.TabIndex = 1143;
            this.label12.Text = "Pressing Time";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // subRecipeProcLami
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.Controls.Add(this.label11);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pll);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.listboxData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "subRecipeProcLami";
            this.Size = new System.Drawing.Size(1325, 789);
            this.Load += new System.EventHandler(this.subRecipeProcLami_Load);
            this.VisibleChanged += new System.EventHandler(this.subRecipeProcLami_VisibleChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pll.ResumeLayout(false);
            this.pll.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox listboxData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtProcessTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtPressure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pll;
        private System.Windows.Forms.TextBox txtUpperTemp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtLowerTemp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txtPressTime;
        private System.Windows.Forms.Label label12;

    }
}
