namespace CJ_Controls.Log_View
{
	partial class Form_LogView
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

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.Panel_Top = new System.Windows.Forms.Panel();
            this.TextBox_FindVal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Label_PageValue = new System.Windows.Forms.Label();
            this.Label_Page = new System.Windows.Forms.Label();
            this.Button_First = new System.Windows.Forms.Button();
            this.Button_Prev = new System.Windows.Forms.Button();
            this.Button_Next = new System.Windows.Forms.Button();
            this.Button_Last = new System.Windows.Forms.Button();
            this.Panel_Client = new System.Windows.Forms.Panel();
            this.ListBox_Log = new System.Windows.Forms.ListBox();
            this.Panel_Path = new System.Windows.Forms.Panel();
            this.TextBox_Path = new System.Windows.Forms.TextBox();
            this.Label_Path = new System.Windows.Forms.Label();
            this.Panel_Top.SuspendLayout();
            this.Panel_Client.SuspendLayout();
            this.Panel_Path.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Top
            // 
            this.Panel_Top.Controls.Add(this.TextBox_FindVal);
            this.Panel_Top.Controls.Add(this.label1);
            this.Panel_Top.Controls.Add(this.Label_PageValue);
            this.Panel_Top.Controls.Add(this.Label_Page);
            this.Panel_Top.Controls.Add(this.Button_First);
            this.Panel_Top.Controls.Add(this.Button_Prev);
            this.Panel_Top.Controls.Add(this.Button_Next);
            this.Panel_Top.Controls.Add(this.Button_Last);
            this.Panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Top.Location = new System.Drawing.Point(0, 0);
            this.Panel_Top.Name = "Panel_Top";
            this.Panel_Top.Size = new System.Drawing.Size(911, 39);
            this.Panel_Top.TabIndex = 0;
            // 
            // TextBox_FindVal
            // 
            this.TextBox_FindVal.BackColor = System.Drawing.Color.White;
            this.TextBox_FindVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_FindVal.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TextBox_FindVal.ForeColor = System.Drawing.Color.Blue;
            this.TextBox_FindVal.Location = new System.Drawing.Point(307, 0);
            this.TextBox_FindVal.Name = "TextBox_FindVal";
            this.TextBox_FindVal.Size = new System.Drawing.Size(368, 39);
            this.TextBox_FindVal.TabIndex = 12;
            this.TextBox_FindVal.TextChanged += new System.EventHandler(this.TextBox_FindVal_TextChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Plum;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(226, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 39);
            this.label1.TabIndex = 11;
            this.label1.Text = "Find Text";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_PageValue
            // 
            this.Label_PageValue.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Label_PageValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label_PageValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_PageValue.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_PageValue.Location = new System.Drawing.Point(81, 0);
            this.Label_PageValue.Name = "Label_PageValue";
            this.Label_PageValue.Size = new System.Drawing.Size(145, 39);
            this.Label_PageValue.TabIndex = 10;
            this.Label_PageValue.Text = "0 / 0";
            this.Label_PageValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Page
            // 
            this.Label_Page.BackColor = System.Drawing.Color.Plum;
            this.Label_Page.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label_Page.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_Page.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Page.Location = new System.Drawing.Point(0, 0);
            this.Label_Page.Name = "Label_Page";
            this.Label_Page.Size = new System.Drawing.Size(81, 39);
            this.Label_Page.TabIndex = 9;
            this.Label_Page.Text = "Page";
            this.Label_Page.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button_First
            // 
            this.Button_First.Dock = System.Windows.Forms.DockStyle.Right;
            this.Button_First.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Button_First.Location = new System.Drawing.Point(675, 0);
            this.Button_First.Name = "Button_First";
            this.Button_First.Size = new System.Drawing.Size(59, 39);
            this.Button_First.TabIndex = 8;
            this.Button_First.Text = "<<";
            this.Button_First.UseVisualStyleBackColor = true;
            this.Button_First.Click += new System.EventHandler(this.Button_First_Click);
            // 
            // Button_Prev
            // 
            this.Button_Prev.Dock = System.Windows.Forms.DockStyle.Right;
            this.Button_Prev.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Button_Prev.Location = new System.Drawing.Point(734, 0);
            this.Button_Prev.Name = "Button_Prev";
            this.Button_Prev.Size = new System.Drawing.Size(59, 39);
            this.Button_Prev.TabIndex = 7;
            this.Button_Prev.Text = "<";
            this.Button_Prev.UseVisualStyleBackColor = true;
            this.Button_Prev.Click += new System.EventHandler(this.Button_Prev_Click);
            // 
            // Button_Next
            // 
            this.Button_Next.Dock = System.Windows.Forms.DockStyle.Right;
            this.Button_Next.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Button_Next.Location = new System.Drawing.Point(793, 0);
            this.Button_Next.Name = "Button_Next";
            this.Button_Next.Size = new System.Drawing.Size(59, 39);
            this.Button_Next.TabIndex = 6;
            this.Button_Next.Text = ">";
            this.Button_Next.UseVisualStyleBackColor = true;
            this.Button_Next.Click += new System.EventHandler(this.Button_Next_Click);
            // 
            // Button_Last
            // 
            this.Button_Last.Dock = System.Windows.Forms.DockStyle.Right;
            this.Button_Last.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Button_Last.Location = new System.Drawing.Point(852, 0);
            this.Button_Last.Name = "Button_Last";
            this.Button_Last.Size = new System.Drawing.Size(59, 39);
            this.Button_Last.TabIndex = 0;
            this.Button_Last.Text = ">>";
            this.Button_Last.UseVisualStyleBackColor = true;
            this.Button_Last.Click += new System.EventHandler(this.Button_Last_Click);
            // 
            // Panel_Client
            // 
            this.Panel_Client.Controls.Add(this.ListBox_Log);
            this.Panel_Client.Controls.Add(this.Panel_Path);
            this.Panel_Client.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Client.Location = new System.Drawing.Point(0, 39);
            this.Panel_Client.Name = "Panel_Client";
            this.Panel_Client.Size = new System.Drawing.Size(911, 595);
            this.Panel_Client.TabIndex = 1;
            // 
            // ListBox_Log
            // 
            this.ListBox_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBox_Log.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ListBox_Log.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ListBox_Log.FormattingEnabled = true;
            this.ListBox_Log.HorizontalExtent = 2000;
            this.ListBox_Log.HorizontalScrollbar = true;
            this.ListBox_Log.ItemHeight = 12;
            this.ListBox_Log.Location = new System.Drawing.Point(0, 0);
            this.ListBox_Log.Name = "ListBox_Log";
            this.ListBox_Log.Size = new System.Drawing.Size(911, 572);
            this.ListBox_Log.TabIndex = 2;
            this.ListBox_Log.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox_Log_DrawItem);
            // 
            // Panel_Path
            // 
            this.Panel_Path.Controls.Add(this.TextBox_Path);
            this.Panel_Path.Controls.Add(this.Label_Path);
            this.Panel_Path.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_Path.Location = new System.Drawing.Point(0, 572);
            this.Panel_Path.Name = "Panel_Path";
            this.Panel_Path.Size = new System.Drawing.Size(911, 23);
            this.Panel_Path.TabIndex = 1;
            // 
            // TextBox_Path
            // 
            this.TextBox_Path.BackColor = System.Drawing.Color.White;
            this.TextBox_Path.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_Path.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TextBox_Path.Location = new System.Drawing.Point(51, 0);
            this.TextBox_Path.Name = "TextBox_Path";
            this.TextBox_Path.ReadOnly = true;
            this.TextBox_Path.Size = new System.Drawing.Size(860, 21);
            this.TextBox_Path.TabIndex = 3;
            // 
            // Label_Path
            // 
            this.Label_Path.BackColor = System.Drawing.Color.Plum;
            this.Label_Path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_Path.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label_Path.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Path.Location = new System.Drawing.Point(0, 0);
            this.Label_Path.Name = "Label_Path";
            this.Label_Path.Size = new System.Drawing.Size(51, 23);
            this.Label_Path.TabIndex = 0;
            this.Label_Path.Text = "Path";
            this.Label_Path.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_LogView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(911, 634);
            this.Controls.Add(this.Panel_Client);
            this.Controls.Add(this.Panel_Top);
            this.Font = new System.Drawing.Font("돋움체", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MinimizeBox = false;
            this.Name = "Form_LogView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_LogView_FormClosing);
            this.Load += new System.EventHandler(this.Form_LogView_Load);
            this.Panel_Top.ResumeLayout(false);
            this.Panel_Top.PerformLayout();
            this.Panel_Client.ResumeLayout(false);
            this.Panel_Path.ResumeLayout(false);
            this.Panel_Path.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel Panel_Top;
		private System.Windows.Forms.Button Button_Last;
		private System.Windows.Forms.Panel Panel_Client;
		private System.Windows.Forms.Panel Panel_Path;
		private System.Windows.Forms.Label Label_Path;
		private System.Windows.Forms.ListBox ListBox_Log;
		private System.Windows.Forms.Button Button_Next;
		private System.Windows.Forms.Label Label_PageValue;
		private System.Windows.Forms.Label Label_Page;
		private System.Windows.Forms.Button Button_First;
		private System.Windows.Forms.Button Button_Prev;
		private System.Windows.Forms.TextBox TextBox_FindVal;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TextBox_Path;
	}
}