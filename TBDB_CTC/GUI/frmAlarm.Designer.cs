namespace TBDB_CTC.GUI
{
    partial class frmAlarm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.a1Panel6 = new Owf.Controls.A1Panel();
            this.lbPortName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridLog_alarm = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.win_GlassButton3 = new CJ_Controls.Windows.Win_GlassButton();
            this.win_GlassButton1 = new CJ_Controls.Windows.Win_GlassButton();
            this.win_GlassButton2 = new CJ_Controls.Windows.Win_GlassButton();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.a1Panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog_alarm)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // a1Panel6
            // 
            this.a1Panel6.BorderColor = System.Drawing.Color.White;
            this.a1Panel6.BorderWidth = 0;
            this.a1Panel6.Controls.Add(this.lbPortName);
            this.a1Panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.a1Panel6.GradientEndColor = System.Drawing.Color.Black;
            this.a1Panel6.GradientStartColor = System.Drawing.Color.White;
            this.a1Panel6.Image = null;
            this.a1Panel6.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel6.Location = new System.Drawing.Point(0, 0);
            this.a1Panel6.Margin = new System.Windows.Forms.Padding(2);
            this.a1Panel6.Name = "a1Panel6";
            this.a1Panel6.RoundCornerRadius = 6;
            this.a1Panel6.ShadowOffSet = 0;
            this.a1Panel6.Size = new System.Drawing.Size(1580, 25);
            this.a1Panel6.TabIndex = 1000;
            this.a1Panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.a1Panel6_Paint);
            // 
            // lbPortName
            // 
            this.lbPortName.BackColor = System.Drawing.Color.Transparent;
            this.lbPortName.Font = new System.Drawing.Font("맑은 고딕", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPortName.ForeColor = System.Drawing.Color.Gold;
            this.lbPortName.Location = new System.Drawing.Point(2, 1);
            this.lbPortName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPortName.Name = "lbPortName";
            this.lbPortName.Size = new System.Drawing.Size(422, 20);
            this.lbPortName.TabIndex = 0;
            this.lbPortName.Text = "ALARM SCREEN";
            this.lbPortName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1449, 25);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(131, 999);
            this.panel2.TabIndex = 1006;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1430, 100);
            this.panel1.TabIndex = 1007;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(631, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 80);
            this.label1.TabIndex = 0;
            this.label1.Text = "sample";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gridLog_alarm);
            this.panel3.Location = new System.Drawing.Point(12, 136);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1430, 644);
            this.panel3.TabIndex = 1008;
            // 
            // gridLog_alarm
            // 
            this.gridLog_alarm.AllowUserToAddRows = false;
            this.gridLog_alarm.AllowUserToDeleteRows = false;
            this.gridLog_alarm.AllowUserToResizeColumns = false;
            this.gridLog_alarm.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLog_alarm.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridLog_alarm.BackgroundColor = System.Drawing.Color.Gray;
            this.gridLog_alarm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.gridLog_alarm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridLog_alarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLog_alarm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.Model,
            this.No,
            this.AlarmDescription,
            this.DateTime});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridLog_alarm.DefaultCellStyle = dataGridViewCellStyle7;
            this.gridLog_alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLog_alarm.GridColor = System.Drawing.Color.Silver;
            this.gridLog_alarm.Location = new System.Drawing.Point(0, 0);
            this.gridLog_alarm.Margin = new System.Windows.Forms.Padding(4);
            this.gridLog_alarm.MultiSelect = false;
            this.gridLog_alarm.Name = "gridLog_alarm";
            this.gridLog_alarm.RowHeadersVisible = false;
            this.gridLog_alarm.RowHeadersWidth = 25;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.gridLog_alarm.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.gridLog_alarm.RowTemplate.Height = 35;
            this.gridLog_alarm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLog_alarm.ShowCellErrors = false;
            this.gridLog_alarm.ShowEditingIcon = false;
            this.gridLog_alarm.ShowRowErrors = false;
            this.gridLog_alarm.Size = new System.Drawing.Size(1430, 644);
            this.gridLog_alarm.StandardTab = true;
            this.gridLog_alarm.TabIndex = 33;
            this.gridLog_alarm.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLog_alarm_CellContentClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.win_GlassButton2);
            this.panel4.Controls.Add(this.win_GlassButton3);
            this.panel4.Controls.Add(this.win_GlassButton1);
            this.panel4.Location = new System.Drawing.Point(12, 786);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1430, 66);
            this.panel4.TabIndex = 1009;
            // 
            // win_GlassButton3
            // 
            this.win_GlassButton3.Location = new System.Drawing.Point(1176, 12);
            this.win_GlassButton3.Name = "win_GlassButton3";
            this.win_GlassButton3.Size = new System.Drawing.Size(119, 42);
            this.win_GlassButton3.TabIndex = 36;
            this.win_GlassButton3.Text = "Clear";
            this.win_GlassButton3.Click += new System.EventHandler(this.win_GlassButton3_Click);
            // 
            // win_GlassButton1
            // 
            this.win_GlassButton1.Location = new System.Drawing.Point(1301, 12);
            this.win_GlassButton1.Name = "win_GlassButton1";
            this.win_GlassButton1.Size = new System.Drawing.Size(119, 42);
            this.win_GlassButton1.TabIndex = 34;
            this.win_GlassButton1.Text = "All Clear";
            this.win_GlassButton1.Click += new System.EventHandler(this.win_GlassButton1_Click);
            // 
            // win_GlassButton2
            // 
            this.win_GlassButton2.Location = new System.Drawing.Point(511, 12);
            this.win_GlassButton2.Name = "win_GlassButton2";
            this.win_GlassButton2.Size = new System.Drawing.Size(119, 42);
            this.win_GlassButton2.TabIndex = 37;
            this.win_GlassButton2.Text = "Recorvery";
            this.win_GlassButton2.Click += new System.EventHandler(this.win_GlassButton2_Click);
            // 
            // index
            // 
            this.index.DataPropertyName = "index";
            this.index.HeaderText = "index";
            this.index.Name = "index";
            this.index.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.index.Width = 80;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Model.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Model.Width = 150;
            // 
            // No
            // 
            this.No.DataPropertyName = "No";
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.No.Width = 114;
            // 
            // AlarmDescription
            // 
            this.AlarmDescription.DataPropertyName = "AlarmDescription";
            this.AlarmDescription.HeaderText = "AlarmDescription";
            this.AlarmDescription.Name = "AlarmDescription";
            this.AlarmDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AlarmDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AlarmDescription.Width = 640;
            // 
            // DateTime
            // 
            this.DateTime.DataPropertyName = "DateTime";
            this.DateTime.HeaderText = "DateTime";
            this.DateTime.Name = "DateTime";
            this.DateTime.ReadOnly = true;
            this.DateTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DateTime.Width = 180;
            // 
            // frmAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1580, 1024);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.a1Panel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmAlarm";
            this.Text = "frmAlarm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAlarm_Load);
            this.a1Panel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLog_alarm)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Owf.Controls.A1Panel a1Panel6;
        private System.Windows.Forms.Label lbPortName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.DataGridView gridLog_alarm;
        private System.Windows.Forms.Panel panel4;
        private CJ_Controls.Windows.Win_GlassButton win_GlassButton1;
        private CJ_Controls.Windows.Win_GlassButton win_GlassButton3;
        private CJ_Controls.Windows.Win_GlassButton win_GlassButton2;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
    }
}