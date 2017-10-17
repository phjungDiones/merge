namespace TBDB_CTC.UserCtrl.SubForm
{
    partial class subMainScreen
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tmrStatus = new System.Windows.Forms.Timer(this.components);
            this.lbWaferAL = new System.Windows.Forms.Label();
            this.lbWaferCP = new System.Windows.Forms.Label();
            this.lbWaferLami = new System.Windows.Forms.Label();
            this.lbWaferPMC = new System.Windows.Forms.Label();
            this.lbWaferLL1 = new System.Windows.Forms.Label();
            this.lbWaferLL2 = new System.Windows.Forms.Label();
            this.lbWaferBD = new System.Windows.Forms.Label();
            this.lbFmLow = new System.Windows.Forms.Label();
            this.lbFmUp = new System.Windows.Forms.Label();
            this.lbAtmUp = new System.Windows.Forms.Label();
            this.lbAtmLow = new System.Windows.Forms.Label();
            this.lbVtmUp = new System.Windows.Forms.Label();
            this.lbVtmLow = new System.Windows.Forms.Label();
            this.btnLamiLoad = new System.Windows.Forms.Button();
            this.btnLamiUnload = new System.Windows.Forms.Button();
            this.btnLamiReset = new System.Windows.Forms.Button();
            this.btnPmcReset = new System.Windows.Forms.Button();
            this.btnPmcUnload = new System.Windows.Forms.Button();
            this.btnPmcLoad = new System.Windows.Forms.Button();
            this.btnMapping = new System.Windows.Forms.Button();
            this.uctrlPortSlot4 = new TBDB_CTC.UserCtrl.uctrlPortSlot();
            this.uctrlPortSlot3 = new TBDB_CTC.UserCtrl.uctrlPortSlot();
            this.uctrlPortSlot2 = new TBDB_CTC.UserCtrl.uctrlPortSlot();
            this.uctrlPortSlot1 = new TBDB_CTC.UserCtrl.uctrlPortSlot();
            this.uctrlPortInfo4 = new TBDB_CTC.UserCtrl.uctrlPortInfo();
            this.uctrlPortInfo3 = new TBDB_CTC.UserCtrl.uctrlPortInfo();
            this.uctrlPortInfo2 = new TBDB_CTC.UserCtrl.uctrlPortInfo();
            this.uctrlUnitInfo3 = new TBDB_CTC.UserCtrl.uctrlUnitInfo();
            this.uctrlUnitInfo4 = new TBDB_CTC.UserCtrl.uctrlUnitInfo();
            this.uctrlUnitInfo2 = new TBDB_CTC.UserCtrl.uctrlUnitInfo();
            this.uctrlUnitInfo1 = new TBDB_CTC.UserCtrl.uctrlUnitInfo();
            this.uctrlPortInfo1 = new TBDB_CTC.UserCtrl.uctrlPortInfo();
            this.lbWaferLami2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TBDB_CTC.Properties.Resources.Chamber_Body;
            this.pictureBox1.Location = new System.Drawing.Point(339, 6);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(487, 421);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 982;
            this.pictureBox1.TabStop = false;
            // 
            // tmrStatus
            // 
            this.tmrStatus.Interval = 300;
            this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
            // 
            // lbWaferAL
            // 
            this.lbWaferAL.BackColor = System.Drawing.Color.Black;
            this.lbWaferAL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaferAL.ForeColor = System.Drawing.Color.White;
            this.lbWaferAL.Location = new System.Drawing.Point(487, 298);
            this.lbWaferAL.Name = "lbWaferAL";
            this.lbWaferAL.Size = new System.Drawing.Size(87, 21);
            this.lbWaferAL.TabIndex = 993;
            this.lbWaferAL.Text = "A.L";
            this.lbWaferAL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWaferCP
            // 
            this.lbWaferCP.BackColor = System.Drawing.Color.Black;
            this.lbWaferCP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaferCP.ForeColor = System.Drawing.Color.White;
            this.lbWaferCP.Location = new System.Drawing.Point(487, 276);
            this.lbWaferCP.Name = "lbWaferCP";
            this.lbWaferCP.Size = new System.Drawing.Size(87, 21);
            this.lbWaferCP.TabIndex = 994;
            this.lbWaferCP.Text = "CP";
            this.lbWaferCP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWaferLami
            // 
            this.lbWaferLami.BackColor = System.Drawing.Color.Black;
            this.lbWaferLami.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaferLami.ForeColor = System.Drawing.Color.White;
            this.lbWaferLami.Location = new System.Drawing.Point(575, 221);
            this.lbWaferLami.Name = "lbWaferLami";
            this.lbWaferLami.Size = new System.Drawing.Size(105, 27);
            this.lbWaferLami.TabIndex = 995;
            this.lbWaferLami.Text = "LAMI";
            this.lbWaferLami.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWaferPMC
            // 
            this.lbWaferPMC.BackColor = System.Drawing.Color.Black;
            this.lbWaferPMC.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaferPMC.ForeColor = System.Drawing.Color.White;
            this.lbWaferPMC.Location = new System.Drawing.Point(378, 142);
            this.lbWaferPMC.Name = "lbWaferPMC";
            this.lbWaferPMC.Size = new System.Drawing.Size(105, 27);
            this.lbWaferPMC.TabIndex = 996;
            this.lbWaferPMC.Text = "PMC";
            this.lbWaferPMC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWaferLL1
            // 
            this.lbWaferLL1.BackColor = System.Drawing.Color.Black;
            this.lbWaferLL1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaferLL1.ForeColor = System.Drawing.Color.White;
            this.lbWaferLL1.Location = new System.Drawing.Point(487, 155);
            this.lbWaferLL1.Name = "lbWaferLL1";
            this.lbWaferLL1.Size = new System.Drawing.Size(87, 20);
            this.lbWaferLL1.TabIndex = 997;
            this.lbWaferLL1.Text = "LL1";
            this.lbWaferLL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWaferLL2
            // 
            this.lbWaferLL2.BackColor = System.Drawing.Color.Black;
            this.lbWaferLL2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaferLL2.ForeColor = System.Drawing.Color.White;
            this.lbWaferLL2.Location = new System.Drawing.Point(487, 175);
            this.lbWaferLL2.Name = "lbWaferLL2";
            this.lbWaferLL2.Size = new System.Drawing.Size(87, 20);
            this.lbWaferLL2.TabIndex = 998;
            this.lbWaferLL2.Text = "LL2";
            this.lbWaferLL2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWaferBD
            // 
            this.lbWaferBD.BackColor = System.Drawing.Color.Black;
            this.lbWaferBD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaferBD.ForeColor = System.Drawing.Color.White;
            this.lbWaferBD.Location = new System.Drawing.Point(487, 194);
            this.lbWaferBD.Name = "lbWaferBD";
            this.lbWaferBD.Size = new System.Drawing.Size(87, 20);
            this.lbWaferBD.TabIndex = 999;
            this.lbWaferBD.Text = "BD";
            this.lbWaferBD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFmLow
            // 
            this.lbFmLow.BackColor = System.Drawing.Color.Gray;
            this.lbFmLow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFmLow.ForeColor = System.Drawing.Color.White;
            this.lbFmLow.Location = new System.Drawing.Point(487, 354);
            this.lbFmLow.Name = "lbFmLow";
            this.lbFmLow.Size = new System.Drawing.Size(86, 29);
            this.lbFmLow.TabIndex = 1000;
            this.lbFmLow.Text = "LOW";
            this.lbFmLow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFmUp
            // 
            this.lbFmUp.BackColor = System.Drawing.Color.Gray;
            this.lbFmUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFmUp.ForeColor = System.Drawing.Color.White;
            this.lbFmUp.Location = new System.Drawing.Point(487, 325);
            this.lbFmUp.Name = "lbFmUp";
            this.lbFmUp.Size = new System.Drawing.Size(86, 29);
            this.lbFmUp.TabIndex = 1001;
            this.lbFmUp.Text = "UP";
            this.lbFmUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAtmUp
            // 
            this.lbAtmUp.BackColor = System.Drawing.Color.Gray;
            this.lbAtmUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAtmUp.ForeColor = System.Drawing.Color.White;
            this.lbAtmUp.Location = new System.Drawing.Point(486, 221);
            this.lbAtmUp.Name = "lbAtmUp";
            this.lbAtmUp.Size = new System.Drawing.Size(86, 24);
            this.lbAtmUp.TabIndex = 1003;
            this.lbAtmUp.Text = "UP";
            this.lbAtmUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAtmLow
            // 
            this.lbAtmLow.BackColor = System.Drawing.Color.Gray;
            this.lbAtmLow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAtmLow.ForeColor = System.Drawing.Color.White;
            this.lbAtmLow.Location = new System.Drawing.Point(486, 245);
            this.lbAtmLow.Name = "lbAtmLow";
            this.lbAtmLow.Size = new System.Drawing.Size(86, 24);
            this.lbAtmLow.TabIndex = 1002;
            this.lbAtmLow.Text = "LOW";
            this.lbAtmLow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVtmUp
            // 
            this.lbVtmUp.BackColor = System.Drawing.Color.Gray;
            this.lbVtmUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVtmUp.ForeColor = System.Drawing.Color.White;
            this.lbVtmUp.Location = new System.Drawing.Point(487, 103);
            this.lbVtmUp.Name = "lbVtmUp";
            this.lbVtmUp.Size = new System.Drawing.Size(86, 24);
            this.lbVtmUp.TabIndex = 1005;
            this.lbVtmUp.Text = "UP";
            this.lbVtmUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVtmLow
            // 
            this.lbVtmLow.BackColor = System.Drawing.Color.Gray;
            this.lbVtmLow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVtmLow.ForeColor = System.Drawing.Color.White;
            this.lbVtmLow.Location = new System.Drawing.Point(486, 127);
            this.lbVtmLow.Name = "lbVtmLow";
            this.lbVtmLow.Size = new System.Drawing.Size(86, 24);
            this.lbVtmLow.TabIndex = 1004;
            this.lbVtmLow.Text = "LOW";
            this.lbVtmLow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLamiLoad
            // 
            this.btnLamiLoad.BackColor = System.Drawing.Color.White;
            this.btnLamiLoad.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLamiLoad.Location = new System.Drawing.Point(701, 154);
            this.btnLamiLoad.Name = "btnLamiLoad";
            this.btnLamiLoad.Size = new System.Drawing.Size(72, 29);
            this.btnLamiLoad.TabIndex = 1006;
            this.btnLamiLoad.Text = "Load";
            this.btnLamiLoad.UseVisualStyleBackColor = false;
            this.btnLamiLoad.Click += new System.EventHandler(this.btnLamiLoad_Click);
            // 
            // btnLamiUnload
            // 
            this.btnLamiUnload.BackColor = System.Drawing.Color.White;
            this.btnLamiUnload.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLamiUnload.Location = new System.Drawing.Point(701, 183);
            this.btnLamiUnload.Name = "btnLamiUnload";
            this.btnLamiUnload.Size = new System.Drawing.Size(72, 29);
            this.btnLamiUnload.TabIndex = 1007;
            this.btnLamiUnload.Text = "Unload";
            this.btnLamiUnload.UseVisualStyleBackColor = false;
            this.btnLamiUnload.Click += new System.EventHandler(this.btnLamiUnload_Click);
            // 
            // btnLamiReset
            // 
            this.btnLamiReset.BackColor = System.Drawing.Color.White;
            this.btnLamiReset.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLamiReset.Location = new System.Drawing.Point(701, 213);
            this.btnLamiReset.Name = "btnLamiReset";
            this.btnLamiReset.Size = new System.Drawing.Size(72, 29);
            this.btnLamiReset.TabIndex = 1008;
            this.btnLamiReset.Text = "Reset";
            this.btnLamiReset.UseVisualStyleBackColor = false;
            this.btnLamiReset.Click += new System.EventHandler(this.btnLamiReset_Click);
            // 
            // btnPmcReset
            // 
            this.btnPmcReset.BackColor = System.Drawing.Color.White;
            this.btnPmcReset.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPmcReset.Location = new System.Drawing.Point(393, 241);
            this.btnPmcReset.Name = "btnPmcReset";
            this.btnPmcReset.Size = new System.Drawing.Size(72, 29);
            this.btnPmcReset.TabIndex = 1011;
            this.btnPmcReset.Text = "Reset";
            this.btnPmcReset.UseVisualStyleBackColor = false;
            this.btnPmcReset.Click += new System.EventHandler(this.btnPmcReset_Click);
            // 
            // btnPmcUnload
            // 
            this.btnPmcUnload.BackColor = System.Drawing.Color.White;
            this.btnPmcUnload.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPmcUnload.Location = new System.Drawing.Point(393, 211);
            this.btnPmcUnload.Name = "btnPmcUnload";
            this.btnPmcUnload.Size = new System.Drawing.Size(72, 29);
            this.btnPmcUnload.TabIndex = 1010;
            this.btnPmcUnload.Text = "Unload";
            this.btnPmcUnload.UseVisualStyleBackColor = false;
            this.btnPmcUnload.Click += new System.EventHandler(this.btnPmcUnload_Click);
            // 
            // btnPmcLoad
            // 
            this.btnPmcLoad.BackColor = System.Drawing.Color.White;
            this.btnPmcLoad.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPmcLoad.Location = new System.Drawing.Point(393, 182);
            this.btnPmcLoad.Name = "btnPmcLoad";
            this.btnPmcLoad.Size = new System.Drawing.Size(72, 29);
            this.btnPmcLoad.TabIndex = 1009;
            this.btnPmcLoad.Text = "Load";
            this.btnPmcLoad.UseVisualStyleBackColor = false;
            this.btnPmcLoad.Click += new System.EventHandler(this.btnPmcLoad_Click);
            // 
            // btnMapping
            // 
            this.btnMapping.BackColor = System.Drawing.Color.White;
            this.btnMapping.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMapping.Location = new System.Drawing.Point(340, 384);
            this.btnMapping.Name = "btnMapping";
            this.btnMapping.Size = new System.Drawing.Size(86, 36);
            this.btnMapping.TabIndex = 1012;
            this.btnMapping.Text = "Mapping";
            this.btnMapping.UseVisualStyleBackColor = false;
            this.btnMapping.Click += new System.EventHandler(this.btnMapping_Click);
            // 
            // uctrlPortSlot4
            // 
            this.uctrlPortSlot4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uctrlPortSlot4.bStatus = false;
            this.uctrlPortSlot4.Location = new System.Drawing.Point(716, 434);
            this.uctrlPortSlot4.LpmWaferStatus = TBDB_Handler.GLOBAL.LPM_Wafer.Exist;
            this.uctrlPortSlot4.Name = "uctrlPortSlot4";
            this.uctrlPortSlot4.Size = new System.Drawing.Size(110, 283);
            this.uctrlPortSlot4.Slot = 0;
            this.uctrlPortSlot4.TabIndex = 992;
            // 
            // uctrlPortSlot3
            // 
            this.uctrlPortSlot3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uctrlPortSlot3.bStatus = false;
            this.uctrlPortSlot3.Location = new System.Drawing.Point(591, 434);
            this.uctrlPortSlot3.LpmWaferStatus = TBDB_Handler.GLOBAL.LPM_Wafer.Exist;
            this.uctrlPortSlot3.Name = "uctrlPortSlot3";
            this.uctrlPortSlot3.Size = new System.Drawing.Size(110, 283);
            this.uctrlPortSlot3.Slot = 0;
            this.uctrlPortSlot3.TabIndex = 991;
            // 
            // uctrlPortSlot2
            // 
            this.uctrlPortSlot2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uctrlPortSlot2.bStatus = false;
            this.uctrlPortSlot2.Location = new System.Drawing.Point(464, 434);
            this.uctrlPortSlot2.LpmWaferStatus = TBDB_Handler.GLOBAL.LPM_Wafer.Exist;
            this.uctrlPortSlot2.Name = "uctrlPortSlot2";
            this.uctrlPortSlot2.Size = new System.Drawing.Size(110, 283);
            this.uctrlPortSlot2.Slot = 0;
            this.uctrlPortSlot2.TabIndex = 990;
            // 
            // uctrlPortSlot1
            // 
            this.uctrlPortSlot1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uctrlPortSlot1.bStatus = false;
            this.uctrlPortSlot1.Location = new System.Drawing.Point(339, 434);
            this.uctrlPortSlot1.LpmWaferStatus = TBDB_Handler.GLOBAL.LPM_Wafer.Exist;
            this.uctrlPortSlot1.Name = "uctrlPortSlot1";
            this.uctrlPortSlot1.Size = new System.Drawing.Size(110, 283);
            this.uctrlPortSlot1.Slot = 0;
            this.uctrlPortSlot1.TabIndex = 989;
            // 
            // uctrlPortInfo4
            // 
            this.uctrlPortInfo4.Location = new System.Drawing.Point(850, 545);
            this.uctrlPortInfo4.Margin = new System.Windows.Forms.Padding(1);
            this.uctrlPortInfo4.Name = "uctrlPortInfo4";
            this.uctrlPortInfo4.PortLockStatus = false;
            this.uctrlPortInfo4.PortLotID = "";
            this.uctrlPortInfo4.PortMID = "";
            this.uctrlPortInfo4.PortName = "PortName";
            this.uctrlPortInfo4.PortPath = "";
            this.uctrlPortInfo4.PortPPID = "";
            this.uctrlPortInfo4.PortStatus = "";
            this.uctrlPortInfo4.Size = new System.Drawing.Size(299, 172);
            this.uctrlPortInfo4.TabIndex = 988;
            // 
            // uctrlPortInfo3
            // 
            this.uctrlPortInfo3.Location = new System.Drawing.Point(10, 545);
            this.uctrlPortInfo3.Margin = new System.Windows.Forms.Padding(1);
            this.uctrlPortInfo3.Name = "uctrlPortInfo3";
            this.uctrlPortInfo3.PortLockStatus = false;
            this.uctrlPortInfo3.PortLotID = "";
            this.uctrlPortInfo3.PortMID = "";
            this.uctrlPortInfo3.PortName = "PortName";
            this.uctrlPortInfo3.PortPath = "";
            this.uctrlPortInfo3.PortPPID = "";
            this.uctrlPortInfo3.PortStatus = "";
            this.uctrlPortInfo3.Size = new System.Drawing.Size(299, 172);
            this.uctrlPortInfo3.TabIndex = 987;
            // 
            // uctrlPortInfo2
            // 
            this.uctrlPortInfo2.Location = new System.Drawing.Point(850, 362);
            this.uctrlPortInfo2.Margin = new System.Windows.Forms.Padding(1);
            this.uctrlPortInfo2.Name = "uctrlPortInfo2";
            this.uctrlPortInfo2.PortLockStatus = false;
            this.uctrlPortInfo2.PortLotID = "";
            this.uctrlPortInfo2.PortMID = "";
            this.uctrlPortInfo2.PortName = "PortName";
            this.uctrlPortInfo2.PortPath = "";
            this.uctrlPortInfo2.PortPPID = "";
            this.uctrlPortInfo2.PortStatus = "";
            this.uctrlPortInfo2.Size = new System.Drawing.Size(299, 172);
            this.uctrlPortInfo2.TabIndex = 986;
            // 
            // uctrlUnitInfo3
            // 
            this.uctrlUnitInfo3.Location = new System.Drawing.Point(850, 182);
            this.uctrlUnitInfo3.Margin = new System.Windows.Forms.Padding(1);
            this.uctrlUnitInfo3.Name = "uctrlUnitInfo3";
            this.uctrlUnitInfo3.Size = new System.Drawing.Size(299, 163);
            this.uctrlUnitInfo3.TabIndex = 985;
            this.uctrlUnitInfo3.UnitName = "UnitName";
            // 
            // uctrlUnitInfo4
            // 
            this.uctrlUnitInfo4.Location = new System.Drawing.Point(850, 6);
            this.uctrlUnitInfo4.Margin = new System.Windows.Forms.Padding(1);
            this.uctrlUnitInfo4.Name = "uctrlUnitInfo4";
            this.uctrlUnitInfo4.Size = new System.Drawing.Size(299, 163);
            this.uctrlUnitInfo4.TabIndex = 984;
            this.uctrlUnitInfo4.UnitName = "UnitName";
            // 
            // uctrlUnitInfo2
            // 
            this.uctrlUnitInfo2.Location = new System.Drawing.Point(10, 182);
            this.uctrlUnitInfo2.Margin = new System.Windows.Forms.Padding(1);
            this.uctrlUnitInfo2.Name = "uctrlUnitInfo2";
            this.uctrlUnitInfo2.Size = new System.Drawing.Size(299, 163);
            this.uctrlUnitInfo2.TabIndex = 983;
            this.uctrlUnitInfo2.UnitName = "UnitName";
            // 
            // uctrlUnitInfo1
            // 
            this.uctrlUnitInfo1.Location = new System.Drawing.Point(10, 6);
            this.uctrlUnitInfo1.Margin = new System.Windows.Forms.Padding(1);
            this.uctrlUnitInfo1.Name = "uctrlUnitInfo1";
            this.uctrlUnitInfo1.Size = new System.Drawing.Size(299, 163);
            this.uctrlUnitInfo1.TabIndex = 981;
            this.uctrlUnitInfo1.UnitName = "UnitName";
            // 
            // uctrlPortInfo1
            // 
            this.uctrlPortInfo1.Location = new System.Drawing.Point(10, 362);
            this.uctrlPortInfo1.Margin = new System.Windows.Forms.Padding(1);
            this.uctrlPortInfo1.Name = "uctrlPortInfo1";
            this.uctrlPortInfo1.PortLockStatus = false;
            this.uctrlPortInfo1.PortLotID = "";
            this.uctrlPortInfo1.PortMID = "";
            this.uctrlPortInfo1.PortName = "PortName";
            this.uctrlPortInfo1.PortPath = "";
            this.uctrlPortInfo1.PortPPID = "";
            this.uctrlPortInfo1.PortStatus = "";
            this.uctrlPortInfo1.Size = new System.Drawing.Size(299, 172);
            this.uctrlPortInfo1.TabIndex = 980;
            // 
            // lbWaferLami2
            // 
            this.lbWaferLami2.BackColor = System.Drawing.Color.Black;
            this.lbWaferLami2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaferLami2.ForeColor = System.Drawing.Color.White;
            this.lbWaferLami2.Location = new System.Drawing.Point(575, 249);
            this.lbWaferLami2.Name = "lbWaferLami2";
            this.lbWaferLami2.Size = new System.Drawing.Size(105, 27);
            this.lbWaferLami2.TabIndex = 1013;
            this.lbWaferLami2.Text = "LAMI";
            this.lbWaferLami2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subMainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.Controls.Add(this.lbWaferLami2);
            this.Controls.Add(this.btnMapping);
            this.Controls.Add(this.btnPmcReset);
            this.Controls.Add(this.btnPmcUnload);
            this.Controls.Add(this.btnPmcLoad);
            this.Controls.Add(this.btnLamiReset);
            this.Controls.Add(this.btnLamiUnload);
            this.Controls.Add(this.btnLamiLoad);
            this.Controls.Add(this.lbVtmUp);
            this.Controls.Add(this.lbVtmLow);
            this.Controls.Add(this.lbAtmUp);
            this.Controls.Add(this.lbAtmLow);
            this.Controls.Add(this.lbFmUp);
            this.Controls.Add(this.lbFmLow);
            this.Controls.Add(this.lbWaferBD);
            this.Controls.Add(this.lbWaferLL2);
            this.Controls.Add(this.lbWaferLL1);
            this.Controls.Add(this.lbWaferPMC);
            this.Controls.Add(this.lbWaferLami);
            this.Controls.Add(this.lbWaferCP);
            this.Controls.Add(this.lbWaferAL);
            this.Controls.Add(this.uctrlPortSlot4);
            this.Controls.Add(this.uctrlPortSlot3);
            this.Controls.Add(this.uctrlPortSlot2);
            this.Controls.Add(this.uctrlPortSlot1);
            this.Controls.Add(this.uctrlPortInfo4);
            this.Controls.Add(this.uctrlPortInfo3);
            this.Controls.Add(this.uctrlPortInfo2);
            this.Controls.Add(this.uctrlUnitInfo3);
            this.Controls.Add(this.uctrlUnitInfo4);
            this.Controls.Add(this.uctrlUnitInfo2);
            this.Controls.Add(this.uctrlUnitInfo1);
            this.Controls.Add(this.uctrlPortInfo1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "subMainScreen";
            this.Size = new System.Drawing.Size(1159, 724);
            this.Load += new System.EventHandler(this.subMainScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private uctrlPortSlot uctrlPortSlot4;
        private uctrlPortSlot uctrlPortSlot3;
        private uctrlPortSlot uctrlPortSlot2;
        private uctrlPortSlot uctrlPortSlot1;
        private uctrlPortInfo uctrlPortInfo4;
        private uctrlPortInfo uctrlPortInfo3;
        private uctrlPortInfo uctrlPortInfo2;
        private uctrlUnitInfo uctrlUnitInfo3;
        private uctrlUnitInfo uctrlUnitInfo4;
        private uctrlUnitInfo uctrlUnitInfo2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private uctrlUnitInfo uctrlUnitInfo1;
        private uctrlPortInfo uctrlPortInfo1;
        private System.Windows.Forms.Timer tmrStatus;
        private System.Windows.Forms.Label lbWaferAL;
        private System.Windows.Forms.Label lbWaferCP;
        private System.Windows.Forms.Label lbWaferLami;
        private System.Windows.Forms.Label lbWaferPMC;
        private System.Windows.Forms.Label lbWaferLL1;
        private System.Windows.Forms.Label lbWaferLL2;
        private System.Windows.Forms.Label lbWaferBD;
        private System.Windows.Forms.Label lbFmLow;
        private System.Windows.Forms.Label lbFmUp;
        private System.Windows.Forms.Label lbAtmUp;
        private System.Windows.Forms.Label lbAtmLow;
        private System.Windows.Forms.Label lbVtmUp;
        private System.Windows.Forms.Label lbVtmLow;
        private System.Windows.Forms.Button btnLamiLoad;
        private System.Windows.Forms.Button btnLamiUnload;
        private System.Windows.Forms.Button btnLamiReset;
        private System.Windows.Forms.Button btnPmcReset;
        private System.Windows.Forms.Button btnPmcUnload;
        private System.Windows.Forms.Button btnPmcLoad;
        private System.Windows.Forms.Button btnMapping;
        private System.Windows.Forms.Label lbWaferLami2;
    }
}
