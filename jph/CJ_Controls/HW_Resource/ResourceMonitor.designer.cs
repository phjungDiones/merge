namespace CJ_Controls.SystemMonitor
{
	partial class Ctrl_ResourceMonitor
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
			this.ProgressBar_SW_CPU = new System.Windows.Forms.ProgressBar();
			this.Label_SW_CPU_Val = new System.Windows.Forms.Label();
			this.GroupCPU = new System.Windows.Forms.GroupBox();
			this.Label_PC_CPU = new System.Windows.Forms.Label();
			this.Label_PC_CPU_Val = new System.Windows.Forms.Label();
			this.ProgressBar_PC_CPU = new System.Windows.Forms.ProgressBar();
			this.Label_SW_CPU = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Label_PhysicalMem = new System.Windows.Forms.Label();
			this.lblPhysicalMemory = new System.Windows.Forms.Label();
			this.Label_VirtualMem = new System.Windows.Forms.Label();
			this.lblVirtualMemory = new System.Windows.Forms.Label();
			this.pgbPhysicalMemory = new System.Windows.Forms.ProgressBar();
			this.pgbVirtualMemory = new System.Windows.Forms.ProgressBar();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.Label_Hdd_D = new System.Windows.Forms.Label();
			this.lblDDriveSpace = new System.Windows.Forms.Label();
			this.Label_Hdd_C = new System.Windows.Forms.Label();
			this.lblCDriveSpace = new System.Windows.Forms.Label();
			this.pgbDDrive = new System.Windows.Forms.ProgressBar();
			this.pgbCDrive = new System.Windows.Forms.ProgressBar();
			this.GroupCPU.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// ProgressBar_SW_CPU
			// 
			this.ProgressBar_SW_CPU.Location = new System.Drawing.Point(117, 39);
			this.ProgressBar_SW_CPU.Name = "ProgressBar_SW_CPU";
			this.ProgressBar_SW_CPU.Size = new System.Drawing.Size(100, 14);
			this.ProgressBar_SW_CPU.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.ProgressBar_SW_CPU.TabIndex = 0;
			// 
			// Label_SW_CPU_Val
			// 
			this.Label_SW_CPU_Val.Location = new System.Drawing.Point(69, 40);
			this.Label_SW_CPU_Val.Name = "Label_SW_CPU_Val";
			this.Label_SW_CPU_Val.Size = new System.Drawing.Size(39, 12);
			this.Label_SW_CPU_Val.TabIndex = 1;
			this.Label_SW_CPU_Val.Text = "00%";
			this.Label_SW_CPU_Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// GroupCPU
			// 
			this.GroupCPU.Controls.Add(this.Label_PC_CPU);
			this.GroupCPU.Controls.Add(this.Label_PC_CPU_Val);
			this.GroupCPU.Controls.Add(this.ProgressBar_PC_CPU);
			this.GroupCPU.Controls.Add(this.Label_SW_CPU);
			this.GroupCPU.Controls.Add(this.Label_SW_CPU_Val);
			this.GroupCPU.Controls.Add(this.ProgressBar_SW_CPU);
			this.GroupCPU.Location = new System.Drawing.Point(3, 3);
			this.GroupCPU.Name = "GroupCPU";
			this.GroupCPU.Size = new System.Drawing.Size(223, 60);
			this.GroupCPU.TabIndex = 3;
			this.GroupCPU.TabStop = false;
			this.GroupCPU.Text = "[ CPU Usage ]";
			// 
			// Label_PC_CPU
			// 
			this.Label_PC_CPU.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Label_PC_CPU.Location = new System.Drawing.Point(11, 19);
			this.Label_PC_CPU.Name = "Label_PC_CPU";
			this.Label_PC_CPU.Size = new System.Drawing.Size(61, 12);
			this.Label_PC_CPU.TabIndex = 7;
			this.Label_PC_CPU.Text = "PC CPU";
			this.Label_PC_CPU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Label_PC_CPU_Val
			// 
			this.Label_PC_CPU_Val.Location = new System.Drawing.Point(69, 19);
			this.Label_PC_CPU_Val.Name = "Label_PC_CPU_Val";
			this.Label_PC_CPU_Val.Size = new System.Drawing.Size(39, 12);
			this.Label_PC_CPU_Val.TabIndex = 6;
			this.Label_PC_CPU_Val.Text = "00%";
			this.Label_PC_CPU_Val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ProgressBar_PC_CPU
			// 
			this.ProgressBar_PC_CPU.Location = new System.Drawing.Point(117, 19);
			this.ProgressBar_PC_CPU.Name = "ProgressBar_PC_CPU";
			this.ProgressBar_PC_CPU.Size = new System.Drawing.Size(100, 14);
			this.ProgressBar_PC_CPU.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.ProgressBar_PC_CPU.TabIndex = 5;
			// 
			// Label_SW_CPU
			// 
			this.Label_SW_CPU.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Label_SW_CPU.Location = new System.Drawing.Point(11, 40);
			this.Label_SW_CPU.Name = "Label_SW_CPU";
			this.Label_SW_CPU.Size = new System.Drawing.Size(61, 12);
			this.Label_SW_CPU.TabIndex = 4;
			this.Label_SW_CPU.Text = "SW CPU";
			this.Label_SW_CPU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.Label_PhysicalMem);
			this.groupBox2.Controls.Add(this.lblPhysicalMemory);
			this.groupBox2.Controls.Add(this.Label_VirtualMem);
			this.groupBox2.Controls.Add(this.lblVirtualMemory);
			this.groupBox2.Controls.Add(this.pgbPhysicalMemory);
			this.groupBox2.Controls.Add(this.pgbVirtualMemory);
			this.groupBox2.Location = new System.Drawing.Point(3, 69);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(223, 99);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "[ Memory Usage ]";
			// 
			// Label_PhysicalMem
			// 
			this.Label_PhysicalMem.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Label_PhysicalMem.Location = new System.Drawing.Point(6, 59);
			this.Label_PhysicalMem.Name = "Label_PhysicalMem";
			this.Label_PhysicalMem.Size = new System.Drawing.Size(105, 12);
			this.Label_PhysicalMem.TabIndex = 1;
			this.Label_PhysicalMem.Text = "Physical";
			this.Label_PhysicalMem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPhysicalMemory
			// 
			this.lblPhysicalMemory.Location = new System.Drawing.Point(6, 77);
			this.lblPhysicalMemory.Name = "lblPhysicalMemory";
			this.lblPhysicalMemory.Size = new System.Drawing.Size(211, 12);
			this.lblPhysicalMemory.TabIndex = 1;
			this.lblPhysicalMemory.Text = "Physical Memory";
			this.lblPhysicalMemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Label_VirtualMem
			// 
			this.Label_VirtualMem.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Label_VirtualMem.Location = new System.Drawing.Point(6, 20);
			this.Label_VirtualMem.Name = "Label_VirtualMem";
			this.Label_VirtualMem.Size = new System.Drawing.Size(105, 12);
			this.Label_VirtualMem.TabIndex = 1;
			this.Label_VirtualMem.Text = "Virtual";
			this.Label_VirtualMem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblVirtualMemory
			// 
			this.lblVirtualMemory.Location = new System.Drawing.Point(6, 38);
			this.lblVirtualMemory.Name = "lblVirtualMemory";
			this.lblVirtualMemory.Size = new System.Drawing.Size(211, 12);
			this.lblVirtualMemory.TabIndex = 1;
			this.lblVirtualMemory.Text = "Virtual Memory";
			this.lblVirtualMemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pgbPhysicalMemory
			// 
			this.pgbPhysicalMemory.Location = new System.Drawing.Point(117, 59);
			this.pgbPhysicalMemory.Name = "pgbPhysicalMemory";
			this.pgbPhysicalMemory.Size = new System.Drawing.Size(100, 12);
			this.pgbPhysicalMemory.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pgbPhysicalMemory.TabIndex = 0;
			// 
			// pgbVirtualMemory
			// 
			this.pgbVirtualMemory.Location = new System.Drawing.Point(117, 20);
			this.pgbVirtualMemory.Name = "pgbVirtualMemory";
			this.pgbVirtualMemory.Size = new System.Drawing.Size(100, 12);
			this.pgbVirtualMemory.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pgbVirtualMemory.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.Label_Hdd_D);
			this.groupBox3.Controls.Add(this.lblDDriveSpace);
			this.groupBox3.Controls.Add(this.Label_Hdd_C);
			this.groupBox3.Controls.Add(this.lblCDriveSpace);
			this.groupBox3.Controls.Add(this.pgbDDrive);
			this.groupBox3.Controls.Add(this.pgbCDrive);
			this.groupBox3.Location = new System.Drawing.Point(3, 174);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(223, 98);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "[ Hard Disk Usage ]";
			// 
			// Label_Hdd_D
			// 
			this.Label_Hdd_D.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Label_Hdd_D.Location = new System.Drawing.Point(6, 59);
			this.Label_Hdd_D.Name = "Label_Hdd_D";
			this.Label_Hdd_D.Size = new System.Drawing.Size(105, 12);
			this.Label_Hdd_D.TabIndex = 1;
			this.Label_Hdd_D.Text = "D Drive Usage";
			this.Label_Hdd_D.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblDDriveSpace
			// 
			this.lblDDriveSpace.Location = new System.Drawing.Point(6, 77);
			this.lblDDriveSpace.Name = "lblDDriveSpace";
			this.lblDDriveSpace.Size = new System.Drawing.Size(211, 12);
			this.lblDDriveSpace.TabIndex = 1;
			this.lblDDriveSpace.Text = "Physical Memory";
			this.lblDDriveSpace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Label_Hdd_C
			// 
			this.Label_Hdd_C.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Label_Hdd_C.Location = new System.Drawing.Point(6, 20);
			this.Label_Hdd_C.Name = "Label_Hdd_C";
			this.Label_Hdd_C.Size = new System.Drawing.Size(105, 12);
			this.Label_Hdd_C.TabIndex = 1;
			this.Label_Hdd_C.Text = "C Drive Usage";
			this.Label_Hdd_C.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCDriveSpace
			// 
			this.lblCDriveSpace.Location = new System.Drawing.Point(6, 38);
			this.lblCDriveSpace.Name = "lblCDriveSpace";
			this.lblCDriveSpace.Size = new System.Drawing.Size(211, 12);
			this.lblCDriveSpace.TabIndex = 1;
			this.lblCDriveSpace.Text = "Virtual Memory";
			this.lblCDriveSpace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pgbDDrive
			// 
			this.pgbDDrive.Location = new System.Drawing.Point(117, 59);
			this.pgbDDrive.Name = "pgbDDrive";
			this.pgbDDrive.Size = new System.Drawing.Size(100, 12);
			this.pgbDDrive.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pgbDDrive.TabIndex = 0;
			// 
			// pgbCDrive
			// 
			this.pgbCDrive.Location = new System.Drawing.Point(117, 20);
			this.pgbCDrive.Name = "pgbCDrive";
			this.pgbCDrive.Size = new System.Drawing.Size(100, 12);
			this.pgbCDrive.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pgbCDrive.TabIndex = 0;
			// 
			// Ctrl_ResourceMonitor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.GroupCPU);
			this.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Name = "Ctrl_ResourceMonitor";
			this.Size = new System.Drawing.Size(229, 274);
			this.GroupCPU.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar_SW_CPU;
		private System.Windows.Forms.Label Label_SW_CPU_Val;
        private System.Windows.Forms.GroupBox GroupCPU;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label Label_PhysicalMem;
        private System.Windows.Forms.Label lblPhysicalMemory;
        private System.Windows.Forms.Label Label_VirtualMem;
        private System.Windows.Forms.Label lblVirtualMemory;
        private System.Windows.Forms.ProgressBar pgbPhysicalMemory;
        private System.Windows.Forms.ProgressBar pgbVirtualMemory;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label Label_Hdd_D;
        private System.Windows.Forms.Label lblDDriveSpace;
        private System.Windows.Forms.Label Label_Hdd_C;
        private System.Windows.Forms.Label lblCDriveSpace;
        private System.Windows.Forms.ProgressBar pgbDDrive;
		private System.Windows.Forms.ProgressBar pgbCDrive;
		private System.Windows.Forms.Label Label_SW_CPU;
		private System.Windows.Forms.Label Label_PC_CPU;
		private System.Windows.Forms.Label Label_PC_CPU_Val;
		private System.Windows.Forms.ProgressBar ProgressBar_PC_CPU;
    }
}
