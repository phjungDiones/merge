using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PI.GLOBAL
{
    [Serializable]
    public class TBInterfaceAddrSet
    {
        #region Loading Interface
        [CategoryAttribute("1. LOADING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LOADING START")]
        public Int64 nLoadingStartAddr
        {
            get;
            set;
        }

        [CategoryAttribute("1. LOADING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LOADING DATA WRITE")]
        public Int64 nLoadingDataWrite
        {
            get;
            set;
        }
        #endregion

        #region Pre-Align Interface
        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("ALIGN START")]
        public Int64 nPreAlignStartAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST POSITION WRITE")]
        public Int64 nPreAlign1stPosWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST POSITION MOVE")]
        public Int64 nPreAlign1stPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST POSITION MOVE DONE")]
        public Int64 nPreAlign1stMoveDoneAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND POSITION WRITE")]
        public Int64 nPreAlign2ndPosWriteAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND POSITION MOVE")]
        public Int64 nPreAlign2ndPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND POSITION MOVE DONE")]
        public Int64 nPreAlign2ndMoveDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("VCR POSITION WRITE")]
        public Int64 nPreAlign3rdPosWriteAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("VCR POSITION MOVE")]
        public Int64 nPreAlign3rdPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("VCR POSITION MOVE DONE")]
        public Int64 nPreAlign3rdMoveDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("ALIGNMENT RESULT WRITE")]
        public Int64 nPreAlignResultWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("ALIGN COMPLETE")]
        public Int64 nPreAlignCompleteAddr
        {
            get;
            set;
        }
        #endregion

        #region Peeling Interface 1
        [CategoryAttribute("3-1. PEELING INTERFACE ADDRESS (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING START")]
        public Int64 nPeelingStartAddr1
        {
            get;
            set;
        }

        [CategoryAttribute("3-1. PEELING INTERFACE ADDRESS (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PROC-POSITION WRITE")]
        public Int64 nPeelingProcPosWriteAddr1
        {
            get;
            set;
        }

        [CategoryAttribute("3-1. PEELING INTERFACE ADDRESS (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("SENSOR READ START")]
        public Int64 nPeelingSensorReadStart1
        {
            get;
            set;
        }

        [CategoryAttribute("3-1. PEELING INTERFACE ADDRESS (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("SENSOR READ STOP")]
        public Int64 nPeelingSensorReadStop1
        {
            get;
            set;
        }
        [CategoryAttribute("3-1. PEELING INTERFACE ADDRESS (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("RESULT DATA WRITE")]
        public Int64 nPeelingSensorResultWrite1
        {
            get;
            set;
        }
        #endregion

        #region Peeling Interface 2
        [CategoryAttribute("3-2. PEELING INTERFACE ADDRESS (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING START")]
        public Int64 nPeelingStartAddr2
        {
            get;
            set;
        }

        [CategoryAttribute("3-2. PEELING INTERFACE ADDRESS (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PROC-POSITION WRITE")]
        public Int64 nPeelingProcPosWriteAddr2
        {
            get;
            set;
        }

        [CategoryAttribute("3-2. PEELING INTERFACE ADDRESS (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("SENSOR READ START")]
        public Int64 nPeelingSensorReadStart2
        {
            get;
            set;
        }

        [CategoryAttribute("3-2. PEELING INTERFACE ADDRESS (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("SENSOR READ STOP")]
        public Int64 nPeelingSensorReadStop2
        {
            get;
            set;
        }
        [CategoryAttribute("3-2. PEELING INTERFACE ADDRESS (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("RESULT DATA WRITE")]
        public Int64 nPeelingSensorResultWrite2
        {
            get;
            set;
        }
        #endregion

        #region Fine-Alignment Interface
        [CategoryAttribute("4. FINE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("FINE-ALIGN START")]
        public Int64 nFineAlignStartAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST POSITION WRITE")]
        public Int64 nFineAlign1stPosWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST POSITION MOVE")]
        public Int64 nFineAlign1stPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST POSITION MOVE DONE")]
        public Int64 nFineAlign1stMoveDoneAddr
        {
            get;
            set;
        }
        [CategoryAttribute("4. FINE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND POSITION WRITE")]
        public Int64 nFineAlign2ndPosWriteAddr
        {
            get;
            set;
        }
        [CategoryAttribute("4. FINE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND POSITION MOVE")]
        public Int64 nFineAlign2ndPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND POSITION MOVE DONE")]
        public Int64 nFineAlign2ndMoveDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("ALIGNMENT RESULT WRITE")]
        public Int64 nFineAlignResultWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("FINE-ALIGN COMPLETE")]
        public Int64 nFineAlignCompleteAddr
        {
            get;
            set;
        }
        #endregion

        #region Scanner-Work Interface
        [CategoryAttribute("5. SCANNER WORKING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER CUTTING START")]
        public Int64 nLaserCuttingStartAddr
        {
            get;
            set;
        }

        [CategoryAttribute("5. SCANNER WORKING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST POSITION WRITE")]
        public Int64 nLaserCutting1stPosWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("5. SCANNER WORKING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST POSITION MOVE")]
        public Int64 nLaserCutting1stPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("5. SCANNER WORKING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST POSITION MOVE DONE")]
        public Int64 nLaserCutting1stMoveDoneAddr
        {
            get;
            set;
        }
        [CategoryAttribute("5. SCANNER WORKING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND POSITION WRITE")]
        public Int64 nLaserCutting2ndPosWriteAddr
        {
            get;
            set;
        }
        [CategoryAttribute("5. SCANNER WORKING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND POSITION MOVE")]
        public Int64 nLaserCutting2ndPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("5. SCANNER WORKING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND POSITION MOVE DONE")]
        public Int64 nLaserCutting2ndMoveDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("5. SCANNER WORKING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER CUTTING COMPLETE")]
        public Int64 nLaserCuttingCompleteAddr
        {
            get;
            set;
        }
        #endregion

        #region Inspection Interface
        [CategoryAttribute("6. INSPECTION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION START")]
        public Int64 nInspectionStartAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. INSPECTION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("REF POSITION WRITE")]
        public Int64 nInspection1stPosWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. INSPECTION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("REF POSITION MOVE")]
        public Int64 nInspection1stPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. INSPECTION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("REF POSITION MOVE DONE")]
        public Int64 nInspection1stMoveDoneAddr
        {
            get;
            set;
        }
        [CategoryAttribute("6. INSPECTION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("REF POSITION WRITE")]
        public Int64 nInspection2ndPosWriteAddr
        {
            get;
            set;
        }
        [CategoryAttribute("6. INSPECTION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("NEXT POSITION MOVE")]
        public Int64 nInspection2ndPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. INSPECTION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("NEXT POSITION MOVE DONE")]
        public Int64 nInspection2ndMoveDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. INSPECTION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("RESULT DATA WRITE")]
        public Int64 nInspectionResultWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. INSPECTION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION COMPLETE")]
        public Int64 nInspectionCompleteAddr
        {
            get;
            set;
        }
        #endregion

        #region Config Scanner Calibration Interface
        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SCANNER CALIBRATION START")]
        public Int64 nScannerCalStartAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SCANNER CALIBRATION POS WRITE")]
        public Int64 nScannerCalPosWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SCANNER CALIBRATION POSITION MOVE")]
        public Int64 nScannerCalPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SCANNER CALIBRATION MOVE DONE")]
        public Int64 nScannerCalMoveDoneAddr
        {
            get;
            set;
        }
        
        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("TURN TABLE 45 MOVE")]
        public Int64 nTurnTable45MoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("TURN TABLE 45 MOVE DONE")]
        public Int64 nTurnTable45MoveDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION START POSITION WRITE")]
        public Int64 nVisionCalStartPosWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION START POSITION MOVE")]
        public Int64 nVisionCalStartPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION START POSITION MOVE DONE")]
        public Int64 nVisionCalStartMoveDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION NEXT POSITION WRITE")]
        public Int64 nVisionCalIncPosWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION NEXT POSITION MOVE")]
        public Int64 nVisionCalIncPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION NEXT POSITION MOVE DONE")]
        public Int64 nVisionCalIncMoveDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. SCANNER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SCANNER CALIBRATION COMPLETE")]
        public Int64 nScannerCalCompleteAddr
        {
            get;
            set;
        }
        #endregion
         
        #region Config Laser Power Calibration Interface
        [CategoryAttribute("8. LASER POWER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER POWER CALIBRATION START")]
        public Int64 nLaserCalStartAddr
        {
            get;
            set;
        }

        [CategoryAttribute("8. LASER POWER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER POWER CALIBRATION POS WRITE")]
        public Int64 nLaserCalPosWriteAddr
        {
            get;
            set;
        }

        [CategoryAttribute("8. LASER POWER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER POWER CALIBRATION POSITION MOVE")]
        public Int64 nLaserCalPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("8. LASER POWER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER POWER CALIBRATION MOVE DONE")]
        public Int64 nLaserCalMoveDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("8. LASER POWER CALIBRATION INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER POWER CALIBRATION COMPLETE")]
        public Int64 nLaserCalCompleteAddr
        {
            get;
            set;
        }
        #endregion

        #region Config Turn-Table Flatness Interface
        [CategoryAttribute("9. TURN-TABLE FLATNESS CHECK INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("FLATNESS CHECK START")]
        public Int64 nTurnTableFlatnessStartAddr
        {
            get;
            set;
        }

        [CategoryAttribute("9. TURN-TABLE FLATNESS CHECK INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("TURN-TABLE 45 MOVE")]
        public Int64 nTurnTableNextMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("9. TURN-TABLE FLATNESS CHECK INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("TURN-TABLE 45 DONE")]
        public Int64 nTurnTableNextDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("9. TURN-TABLE FLATNESS CHECK INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING HEAD MOVE TO MEASURE POSITION")]
        public Int64 nPeelingHeadPosMoveAddr
        {
            get;
            set;
        }

        [CategoryAttribute("9. TURN-TABLE FLATNESS CHECK INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING HEAD MOVE DONE")]
        public Int64 nPeelingHeadMoveDoneAddr
        {
            get;
            set;
        }

        [CategoryAttribute("9. TURN-TABLE FLATNESS CHECK INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("FLATNESS CHECK COMPLETE")]
        public Int64 nTurnTableFlatnessCompleteAddr
        {
            get;
            set;
        }
        #endregion

        #region Unloading Interface
        [CategoryAttribute("10. UNLOADING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("UNLOADING START")]
        public Int64 nUnloadingStartAddr
        {
            get;
            set;
        }

        [CategoryAttribute("10. UNLOADING INTERFACE ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("UNLOADING COMPLETE")]
        public Int64 nUnloadingCompleteAddr
        {
            get;
            set;
        }
        #endregion
    }
    
    [Serializable]
    public class TBPosWriteAddrSet
    {
        #region Data Write for Loading
        [CategoryAttribute("1. LOADING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("PANEL INFO (PANEL ID)")]
        public Int64 nLoadCommonPanelIDAddr
        {
            get;
            set;
        }
        [CategoryAttribute("1. LOADING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("PANEL INFO (STAGE ANGLE 1:180)")]
        public Int64 nLoadCommonStageAngleAddr
        {
            get;
            set;
        }
        [CategoryAttribute("1. LOADING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("PANEL INFO (PANEL TOTAL COUNT - X)")]
        public Int64 nLoadCommonTotalCountXAddr
        {
            get;
            set;
        }
        [CategoryAttribute("1. LOADING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("PANEL INFO (PANEL TOTAL COUNT - Y)")]
        public Int64 nLoadCommonTotalCountYAddr
        {
            get;
            set;
        }
        [CategoryAttribute("1. LOADING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("PANEL LOAD STATE #1")]
        public Int64 nPanelLoadState1Addr
        {
            get;
            set;
        }
        [CategoryAttribute("1. LOADING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("PANEL LOAD STATE #2")]
        public Int64 nPanelLoadState2Addr
        {
            get;
            set;
        }
        [CategoryAttribute("1. LOADING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("PANEL STAGE COL(X) NO #1")]
        public Int64 nPanelLoadStageCol1Addr
        {
            get;
            set;
        }
        [CategoryAttribute("1. LOADING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("PANEL STAGE ROW(Y) NO #1")]
        public Int64 nPanelLoadStageRow1Addr
        {
            get;
            set;
        }
        [CategoryAttribute("1. LOADING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("PANEL STAGE COL(X) NO #2")]
        public Int64 nPanelLoadStageCol2Addr
        {
            get;
            set;
        }
        [CategoryAttribute("1. LOADING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("PANEL STAGE ROW(Y) NO #2")]
        public Int64 nPanelLoadStageRow2Addr
        {
            get;
            set;
        }
        #endregion

        #region Data Write for Pre-Align
        [CategoryAttribute("2. PRE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST GRAB POSTION X-AXIS")]
        public Int64 nPreAlignGrab1PosXAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST GRAB POSTION Y-AXIS")]
        public Int64 nPreAlignGrab1PosYAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("1ST GRAB POSTION Z-AXIS")]
        public Int64 nPreAlignGrab1PosZAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND GRAB POSTION X-AXIS")]
        public Int64 nPreAlignGrab2PosXAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND GRAB POSTION Y-AXIS")]
        public Int64 nPreAlignGrab2PosYAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("2ND GRAB POSTION Z-AXIS")]
        public Int64 nPreAlignGrab2PosZAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("VCR GRAB POSTION X-AXIS")]
        public Int64 nPreAlignGrab3PosXAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("VCR GRAB POSTION Y-AXIS")]
        public Int64 nPreAlignGrab3PosYAddr
        {
            get;
            set;
        }
        [CategoryAttribute("2. PRE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("VCR GRAB POSTION Z-AXIS")]
        public Int64 nPreAlignGrab3PosZAddr
        {
            get;
            set;
        }
        #endregion

        #region Peeling1 
        [CategoryAttribute("3-1. PEELING SEQ (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING START POSITION X")]
        public Int64 nPeelingStarPosXAddr1
        {
            get;
            set;
        }

        [CategoryAttribute("3-1. PEELING SEQ (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING START POSITION Y")]
        public Int64 nPeelingStarPosYAddr1
        {
            get;
            set;
        }

        [CategoryAttribute("3-1. PEELING SEQ (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING START POSITION Z")]
        public Int64 nPeelingStarPosZAddr1
        {
            get;
            set;
        }

        [CategoryAttribute("3-1. PEELING SEQ (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 1ST STEP POSITION X")]
        public Int64 nPeelingStep1PosXAddr1
        {
            get;
            set;
        }
        [CategoryAttribute("3-1. PEELING SEQ (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 1ST STEP POSITION Y")]
        public Int64 nPeelingStep1PosYAddr1
        {
            get;
            set;
        }
        [CategoryAttribute("3-1. PEELING SEQ (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 1ST STEP POSITION Z")]
        public Int64 nPeelingStep1PosZAddr1
        {
            get;
            set;
        }
        [CategoryAttribute("3-1. PEELING SEQ (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 2ND STEP POSITION X")]
        public Int64 nPeelingStep2PosXAddr1
        {
            get;
            set;
        }
        [CategoryAttribute("3-1. PEELING SEQ (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 2ND STEP POSITION Y")]
        public Int64 nPeelingStep2PosYAddr1
        {
            get;
            set;
        }
        [CategoryAttribute("3-1. PEELING SEQ (1)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 2ND STEP POSITION Z")]
        public Int64 nPeelingStep2PosZAddr1
        {
            get;
            set;
        }
        #endregion

        #region Peeling2
        [CategoryAttribute("3-2. PEELING SEQ (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING START POSITION X")]
        public Int64 nPeelingStarPosXAddr2
        {
            get;
            set;
        }

        [CategoryAttribute("3-2. PEELING SEQ (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING START POSITION Y")]
        public Int64 nPeelingStarPosYAddr2
        {
            get;
            set;
        }

        [CategoryAttribute("3-2. PEELING SEQ (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING START POSITION Z")]
        public Int64 nPeelingStarPosZAddr2
        {
            get;
            set;
        }

        [CategoryAttribute("3-2. PEELING SEQ (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 1ST STEP POSITION X")]
        public Int64 nPeelingStep1PosXAddr2
        {
            get;
            set;
        }
        [CategoryAttribute("3-2. PEELING SEQ (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 1ST STEP POSITION Y")]
        public Int64 nPeelingStep1PosYAddr2
        {
            get;
            set;
        }
        [CategoryAttribute("3-2. PEELING SEQ (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 1ST STEP POSITION Z")]
        public Int64 nPeelingStep1PosZAddr2
        {
            get;
            set;
        }
        [CategoryAttribute("3-2. PEELING SEQ (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 2ND STEP POSITION X")]
        public Int64 nPeelingStep2PosXAddr2
        {
            get;
            set;
        }
        [CategoryAttribute("3-2. PEELING SEQ (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 2ND STEP POSITION Y")]
        public Int64 nPeelingStep2PosYAddr2
        {
            get;
            set;
        }
        [CategoryAttribute("3-2. PEELING SEQ (2)"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING 2ND STEP POSITION Z")]
        public Int64 nPeelingStep2PosZAddr2
        {
            get;
            set;
        }
        #endregion

        #region Fine-Alignment
        [CategoryAttribute("4. FINE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("FINE-ALIGN START POSITION X")]
        public Int64 nFineAlignStartPosXAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("FINE-ALIGN START POSITION Y")]
        public Int64 nFineAlignStartPosYAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("FINE-ALIGN START POSITION Z")]
        public Int64 nFineAlignStartPosZAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("FINE-ALIGN 2ND POSITION X")]
        public Int64 nFineAlign2ndPosXAddr
        {
            get;
            set;
        }
        [CategoryAttribute("4. FINE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("FINE-ALIGN 2ND POSITION Y")]
        public Int64 nFineAlign2ndPosYAddr
        {
            get;
            set;
        }
        [CategoryAttribute("4. FINE-ALIGN SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("FINE-ALIGN 2ND POSITION Z")]
        public Int64 nFineAlign2ndPosZAddr
        {
            get;
            set;
        }
        #endregion

        #region Scanner-Work
        [CategoryAttribute("5. SCANNER WORKING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER CUTTING 1ST POSITION X")]
        public Int64 nLaserCuttingStartPosXAddr
        {
            get;
            set;
        }

        [CategoryAttribute("5. SCANNER WORKING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER CUTTING 1ST POSITION Z")]
        public Int64 nLaserCuttingStartPosZAddr
        {
            get;
            set;
        }

        [CategoryAttribute("5. SCANNER WORKING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER CUTTING 2ND POSITION X")]
        public Int64 nLaserCutting2ndPosXAddr
        {
            get;
            set;
        }

        [CategoryAttribute("5. SCANNER WORKING SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER CUTTING 2ND POSITION Z")]
        public Int64 nLaserCutting2ndPosZAddr
        {
            get;
            set;
        }
        #endregion

        #region Inspection
        [CategoryAttribute("6. INSPECTION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION START POSITION X")]
        public Int64 nInspectionStartPosXAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. INSPECTION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION START POSITION Y")]
        public Int64 nInspectionStartPosYAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. INSPECTION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION START POSITION Z")]
        public Int64 nInspectionStartPosZAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. INSPECTION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION NEXT POSITION X")]
        public Int64 nInspectionNextPosXAddr
        {
            get;
            set;
        }
        [CategoryAttribute("6. INSPECTION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION NEXT POSITION Y")]
        public Int64 nInspectionNextPosYAddr
        {
            get;
            set;
        }
        [CategoryAttribute("6. INSPECTION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION NEXT POSITION Z")]
        public Int64 nInspectionNextPosZAddr
        {
            get;
            set;
        }
        #endregion

        #region Scanner Calibration
        [CategoryAttribute("6. SCANNER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("SCANNER SHOT POSITION X")]
        public Int64 nScannerShotPosXAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. SCANNER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("SCANNER SHOT POSITION Z")]
        public Int64 nScannerShotPosZAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. SCANNER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION START POSITION X")]
        public Int64 nVisionCalStartPosXAddr
        {
            get;
            set;
        }

        [CategoryAttribute("6. SCANNER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION START POSITION Y")]
        public Int64 nVisionCalStartPosYAddr
        {
            get;
            set;
        }
        [CategoryAttribute("6. SCANNER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION START POSITION Z")]
        public Int64 nVisionCalStartPosZAddr
        {
            get;
            set;
        }
        [CategoryAttribute("6. SCANNER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION NEXT INC-POSITION X")]
        public Int64 nVisionCalNextPosXAddr
        {
            get;
            set;
        }
        [CategoryAttribute("6. SCANNER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("VISION CALIBRATION NEXT INC-POSITION Y")]
        public Int64 nVisionCalNextPosYAddr
        {
            get;
            set;
        }
        #endregion

        #region Scanner Calibration
        [CategoryAttribute("7. LASER POWER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER SHOT POSITION X")]
        public Int64 nLaserShotPosXAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. LASER POWER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER SHOT POSITION Z")]
        public Int64 nLaserShotPosZAddr
        {
            get;
            set;
        }

        [CategoryAttribute("7. LASER POWER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER CALIBRATION TIME (HOUR)")]
        public Int64 nLaserCalStartTime_H
        {
            get;
            set;
        }

        [CategoryAttribute("7. LASER POWER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER CALIBRATION TIME (MIN)")]
        public Int64 nLaserCalStartTime_M
        {
            get;
            set;
        }
        [CategoryAttribute("7. LASER POWER CALIBRATION SEQ"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER CALIBRATION TIME (SEC)")]
        public Int64 nLaserCalStartTime_S
        {
            get;
            set;
        }
        
        #endregion
    }

    [Serializable]
    public class TBDataRecordingAddrSet
    {
        #region Main Control Data Memory
        [CategoryAttribute("1. MAIN CONTROL ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("CELL ID")]
        public Int64 nCellIDAddr
        {
            get;
            set;
        }

        [CategoryAttribute("1. MAIN CONTROL ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LOADING STATE")]
        public Int64 nLoadingStateAddr
        {
            get;
            set;
        }

        [CategoryAttribute("1. MAIN CONTROL ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("VCR")]
        public Int64 nVCRAddr
        {
            get;
            set;
        }

        [CategoryAttribute("1. MAIN CONTROL ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("CELL POSITION X")]
        public Int64 nCellPosXAddr
        {
            get;
            set;
        }

        [CategoryAttribute("1. MAIN CONTROL ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("CELL POSITION Y")]
        public Int64 nCellPosYAddr
        {
            get;
            set;
        }
        
        #endregion

        #region Pre-Align Data Memory
        [CategoryAttribute("2. PRE-ALIGN DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("REFERENCE X")]
        public Int64 nPreAlignRefXAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("REFERENCE Y")]
        public Int64 nPreAlignRefYAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("ALIGN ANGLE")]
        public Int64 nPreAlignAngleAddr
        {
            get;
            set;
        }

        [CategoryAttribute("2. PRE-ALIGN DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("JUDGEMENT")]
        public Int64 nPreAlignJudgeAddr
        {
            get;
            set;
        }
        #endregion

        #region Peeling Data Address
        [CategoryAttribute("3. PEELING DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING CHECK MIN")]
        public Int64 nPeelingCheckMinAddr
        {
            get;
            set;
        }

        [CategoryAttribute("3. PEELING DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING CHECK MAX")]
        public Int64 nPeelingCheckMaxAddr
        {
            get;
            set;
        }

        [CategoryAttribute("3. PEELING DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEELING CHECK JUDGE")]
        public Int64 nPeelingCheckJudgeAddr
        {
            get;
            set;
        }
        #endregion

        #region Fine-Align Data Address
        [CategoryAttribute("4. FINE-ALIGN DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("REFERENCE X")]
        public Int64 nFineAlignRefXAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("REFERENCE Y")]
        public Int64 nFineAlignRefYAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("ALIGN ANGLE")]
        public Int64 nFineAlignAngleAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("ALIGN JUDGEMENT")]
        public Int64 nFineAlignJudgeAddr
        {
            get;
            set;
        }

        [CategoryAttribute("4. FINE-ALIGN DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("PEEL CHECK JUDGEMENT")]
        public Int64 nPeelCheckJudgeAddr
        {
            get;
            set;
        }
        #endregion

        //#region Scanner Work Data Address
        //[CategoryAttribute("5. SCANNER WORK DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER POWER")]
        //public Int64 nScannerWorkLaserPowerAddr
        //{
        //    get;
        //    set;
        //}

        //[CategoryAttribute("5. SCANNER WORK DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("LASER FREQUENCY")]
        //public Int64 nScannerWorkLaserFrqAddr
        //{
        //    get;
        //    set;
        //}

        //[CategoryAttribute("5. SCANNER WORK DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("DXF FILE NO")]
        //public Int64 nScannerWorkDxfFileNoAddr
        //{
        //    get;
        //    set;
        //}
        //#endregion

        #region Inspection Data Address
        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION TOTAL DETECTED DEFECT QTY")]
        public Int64 nInspectionTotalDefectQty
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL SIZE WIDTH TOP")]
        public Int64 nInspectionPnWidthTop
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL SIZE WIDTH BOTTOM")]
        public Int64 nInspectionPnWidthBot
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL SIZE WIDTH HEIGHT LEFT")]
        public Int64 nInspectionPnWidthLeft
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL SIZE WIDTH RIGHT")]
        public Int64 nInspectionPnWidthRight
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION CHAMFER CUT LENGHT 1")]
        public Int64 nInspectionChamferCutLenght1
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION CHAMFER CUT ANGLE 1")]
        public Int64 nInspectionChamferCutAngle1
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL LEFT CUT OFFSET LT X")]
        public Int64 nInspectionLeftCutOffsetLTX
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL LEFT CUT OFFSET LT Y")]
        public Int64 nInspectionLeftCutOffsetLTY
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL LEFT CUT OFFSET LB X")]
        public Int64 nInspectionLeftCutOffsetLBX
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL LEFT CUT OFFSET LB Y")]
        public Int64 nInspectionLeftCutOffsetLBY
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION CHAMFER CUT LENGHT 2")]
        public Int64 nInspectionChamferCutLenght2
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION CHAMFER CUT ANGLE 2")]
        public Int64 nInspectionChamferCutAngle2
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL LEFT CUT OFFSET RT X")]
        public Int64 nInspectionLeftCutOffsetRTX
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL LEFT CUT OFFSET RT Y")]
        public Int64 nInspectionLeftCutOffsetRTY
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL LEFT CUT OFFSET RB X")]
        public Int64 nInspectionLeftCutOffsetRBX
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL LEFT CUT OFFSET RB Y")]
        public Int64 nInspectionLeftCutOffsetRBY
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL LEFT SIZE JUDGE")]
        public Int64 nInspectionLeftSizeJudge
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL RIGHT SIZE JUDGE")]
        public Int64 nInspectionRightSizeJudge
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL TOP SIZE JUDGE")]
        public Int64 nInspectionTopSizeJudge
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL BOTTOM SIZE JUDGE")]
        public Int64 nInspectionBotSizeJudge
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL LEFT FLATNESS")]
        public Int64 nInspectionLeftFlatness
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL TOP FLATNESS")]
        public Int64 nInspectionTopFlatness
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL RIGHT FLATNESS")]
        public Int64 nInspectionRightFlatness
        {
            get;
            set;
        }

        [CategoryAttribute("5. INSPECTION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INSPECTION PANEL BOTTOM FLATNESS")]
        public Int64 nInspectionBotFlatness
        {
            get;
            set;
        }

        #endregion

        #region Information Data Address
        [CategoryAttribute("6. INFORMATION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INFOMATION PANEL JUDGEMENT")]
        public Int64 nInformationPnJudge
        {
            get;
            set;
        }

        [CategoryAttribute("6. INFORMATION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INFOMATION PANEL GRADE")]
        public Int64 nInformationPnGrade
        {
            get;
            set;
        }

        [CategoryAttribute("6. INFORMATION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INFOMATION STEP ID")]
        public Int64 nInformationStepID
        {
            get;
            set;
        }

        [CategoryAttribute("6. INFORMATION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INFOMATION PRODUCT ID")]
        public Int64 nInformationProductID
        {
            get;
            set;
        }

        [CategoryAttribute("6. INFORMATION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INFOMATION EQUIPMENT ID")]
        public Int64 nInformationEquipID
        {
            get;
            set;
        }

        [CategoryAttribute("6. INFORMATION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INFOMATION PROCESSING TIME")]
        public Int64 nInformationProcTime
        {
            get;
            set;
        }

        [CategoryAttribute("6. INFORMATION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INFOMATION START TIME")]
        public Int64 nInformationStartTime
        {
            get;
            set;

        }

        [CategoryAttribute("6. INFORMATION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INFOMATION END TIME")]
        public Int64 nInformationEndTime
        {
            get;
            set;
        }

        [CategoryAttribute("6. INFORMATION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INFOMATION TURN TABLE ID")]
        public Int64 nInformationTurnTableID
        {
            get;
            set;
        }

        [CategoryAttribute("6. INFORMATION DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("INFOMATION PEELING TABLE")]
        public Int64 nInformationPeelingTable
        {
            get;
            set;
        }

        #endregion

        #region SUMMARY Data Address
        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY PANEL PEELING FLAG")]
        public Int64 nSummaryPnPeelingFlag
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY PANEL PEELING HEIGHT")]
        public Int64 nSummaryPnPeelingHeight
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY MA DEFECT QTY")]
        public Int64 nSummaryMaDefectQty
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY SI DEFECT QTY")]
        public Int64 nSummarySiDefectQty
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY FE DEFECT QTY")]
        public Int64 nSummaryFeDefectQty
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY CH DEFECT QTY")]
        public Int64 nSummaryChDefectQty
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY PANEL R1 LEFT")]
        public Int64 nSummaryPnR1Left
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY PANEL R2 LEFT")]
        public Int64 nSummaryPnR2Left
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY PANEL R1 RIGHT")]
        public Int64 nSummaryPnR1Right
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY PANEL R2 LEFT")]
        public Int64 nSummaryPnR2Right
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY PANEL ALIGN MARK DEVIATION")]
        public Int64 nSummaryPnAlignMarkDeviation
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY ALIGN MARK DISTANCE L M1")]
        public Int64 nSummaryAlignMarkDistLM1
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY ALIGN MARK DISTANCE B M1")]
        public Int64 nSummaryAlignMarkDistBM1
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY ALIGN MARK DISTANCE M1 M2")]
        public Int64 nSummaryAlignMarkDistM1M2
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY ALIGN MARK DISTANCE M2 B")]
        public Int64 nSummaryAlignMarkDistM2B
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY ALIGN MARK DISTANCE M2 R")]
        public Int64 nSummaryAlignMarkDistM2R
        {
            get;
            set;
        }

        [CategoryAttribute("7. SUMMARY DATA ADDRESS"), DescriptionAttribute("Descript"), DisplayNameAttribute("SUMMARY PANEL ID NAMING")]
        public Int64 nSummaryPnIDNaming
        {
            get;
            set;
        }
        #endregion
    }
}
