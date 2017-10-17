using CJ_Controls.Communication.QuadraVTM4;
using CJ_Controls.Communication.CybogRobot_HTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using TBDB_Handler.SEQ;

namespace TBDB_Handler.GLOBAL
{

    public enum UNIT
    {
        LPM,
        FM_ROBOT,
        ALINGER,
        ATM_ROBOT,
        LAMINATOR,
        LOADLOCK,
        VTM_ROBOT,
        PMC,
        HP,
    }

    public enum fn : int
    {
        err = -1,
        busy,
        success
    };

    public enum HAND
    {
        LOWER = 1,
        UPPER = 2,
    }

    public enum LPM_Wafer
    {
        Exist,
        Empty,
        Unload,
    }

    [Serializable]
    public enum WaferType
    {
        CARRIER,
        DEVICE,
        BONDED,
    }

    [Serializable]
    public enum EFEM_TYPE
    {
        A_CARRIER = 0,
        B_CARRIER,
        C_DEVICE,
        D_DEVICE,
    }

    public class GlobalDefine
    {
    }

    [Serializable]
    public enum RUN_MODE
    {
        FULL = 0,
        ONLY_LAMI,
        ONLY_BOND,
    }


    public class MCDF
    {
        public const int MAX_SLOT_COUNT = 25;
        public const int MAX_SLOT_CP = 3;
        public const int MAX_SLOT_LOADLOCK = 3;
        public const int MAX_ARM_COUNT = 3;

    
        //설비상태
        public const int MC_IDLE = 0; //프로그램 구동후 초기화 전
        public const int MC_INITIALIZING = 1; //초기화 중일때
        public const int MC_STOP = 2; //초기화 된 상태로 정지
        public const int MC_ERR_STOP = 3; //에러로 정지된 상태
        public const int MC_RUN = 4; //RUN

        //Run Event
        public const int CMD_STOP = 0;
        public const int CMD_RUN = 1;

        //Log 종류
        public const int EVENT_LOG = 0;
        public const int ERROR_LOG = 1;
        public const int RUN_LOG = 2;
        public const int COMM_LOG = 3; // 통신로그

        //Login Level
        public const int LEVEL_NOT_LOGIN = -1;
        public const int LEVEL_OP = 0;
        public const int LEVEL_ENG = 1;
        public const int LEVEL_MAK = 2;

        //Safty
        public const int SAFETY_OK = 0;
    }


    public static class ERDF
    {
        public const int ERR_CNT = 1000;

        public const int EMO_STOP = 1;
        public const int EMO_FRONT = 2;
        public const int EMO_REAR = 3;
        public const int EMO_LEFT = 4;
        public const int EMO_RIGHT = 5;

        public const int DOOR_OPEN = 10;
        public const int DOOR_FRONT = 11;
        public const int DOOR_REAR = 12;
        public const int DOOR_LEFT = 13;
        public const int DOOR_RIGHT = 14;

        public const int MAIN_AIR_LOW = 20;
        public const int MAIN_VAC_LOW = 21;

        public const int E_TRSF_VAC_1 = 50;
        public const int E_TRSF_VAC_2 = 51;
        public const int E_TRSF_PCB_NOT_DETECT = 55;

        public const int E_STAGE_VAC_1 = 60;
        public const int E_STAGE_VAC_2 = 61;
        public const int E_STAGE_VAC_3 = 62;

        public const int E_CM_NOT_FOUND_PATTERN_ALIGN_FIRST = 100;
        public const int E_CM_NOT_FOUND_PATTERN_ALIGN_SECOND = 101;
        public const int E_DSPS_SETUP_ERR = 102;
        public const int E_MEASURE_DISP_ERR = 103;                      // 변위센서 측정 에러
        public const int E_READ_BARCODE_ERR = 104;                      // 바코드 읽기 실패
        public const int E_CM_NOT_HORIZONTAL = 105;                     // ALIGN 시 수평이 안될 때
        public const int E_CM_FAIL_INSPECTION = 106;                    // INSPECTION 실패

        public const int E_CYL_MOVE = 150;
        public const int E_SV_INVALID_VEL = 200;

        public const int E_SV_MOVE = 500;
        public const int E_SV_AMP = 520;
        public const int E_SV_NOT_HOME = 540;

        public const int E_SV_INTERLOCK_0 = 600;

        //Error Script
        public static string[] sErrNo = new string[1000];
        public static string[] sErrTitle = new string[1000];
        public static string[] sErrCause = new string[1000];
        public static string[] sErrAction = new string[1000];
        public static string[] sErrAction2 = new string[1000];

        public static int[] nErrHistCnt = new int[1000];
        public static string[] strErrHistLastTime = new string[1000];
    }



    [Serializable]
    public class ProcInfoBase
    {
        public string strProcName { get; set; }
    }

    [Serializable]
    public class ProcInfoAlign : ProcInfoBase
    {
        // PreAlign,PostAlign Skip 옵션으로 결정되어 의미가 없다
        //public bool bUse = false;
        //public double dAngle { get; set; }
        public float dAngle { get; set; }
    }

    [Serializable]
    public class ProcInfoLami : ProcInfoBase
    {
        // RUN MODE로 결정되어 의미가 없다
        //public bool bUse = false;
        public int nProcTimeSec = 0;
        public double dPressure = 0;
        public double dUpperTemp = 0;
        public double dLowerTemp = 0;
        public UInt64 nPressingTimeSec = 0;
    }

    [Serializable]
    public class ProcInfoBond : ProcInfoBase
    {
        public double dTotalTimeSec = 0.0;
        public bool bVisionUse = false;
        public double dPressure = 0.0;
        public double dPressTimeSec = 0.0;
        public double dUpperTemp = 0.0;
        public double dLowerTemp = 0.0;
        public double dAPCPos = 0.0;
        public double dBacklightCH1 = 0.0;
        public double dBacklightCH2 = 0.0;
        public double dBacklightCH3 = 0.0;
    }


    /// <summary>
    /// 시퀀스 시 저장할 웨이퍼 데이터
    /// </summary>
    [Serializable]
    public class WaferData
    {
        public EFEM_TYPE efemType;
        public WaferType waferType;
        public int nSlot;
        public bool bIsPreAlign;
        public bool bIsPostAlign;
        public bool bIsLami;
        public bool bIsBond;
        public bool bIsHP;

        public WaferData()
        {
            efemType = EFEM_TYPE.A_CARRIER;
            waferType = WaferType.CARRIER;
            nSlot = 0;
            bIsPreAlign = false;
            bIsPostAlign = false;
            bIsLami = false;
            bIsBond = false;
            bIsHP = false;
        }

        public WaferData(EFEM_TYPE efType, WaferType wType, int nSlot)
        {
            this.efemType = efType;
            this.waferType = wType;
            this.nSlot = nSlot;
            Init();
        }

        public void Init()
        {
            bIsPreAlign = false;
            bIsPostAlign = false;
            bIsLami = false;
            bIsBond = false;
            bIsHP = false;
        }

        public void PreAlign()
        {
            bIsPreAlign = true;
        }

        public void PostAlign()
        {
            bIsPostAlign = true;
        }

        public void Laminate()
        {
            if(waferType == WaferType.CARRIER)
                bIsLami = true;
        }

        public void Bonding()
        {
            waferType = WaferType.BONDED;
            bIsBond = true;
        }

        public void HotPlate()
        {
            bIsHP = true;
        }

        //동작 완료 확인용
        public bool Finish_PreAlign()
        {
            return bIsPreAlign;
        }

        public bool Finish_PostAlign()
        {
            return bIsPostAlign;
        }

        public bool Finish_Laminate()
        {
            return bIsLami;
        }

        public bool Finish_Bonding()
        {
            return bIsBond;
        }

        public bool Finish_HotPlate()
        {
            return bIsHP;
        }
    }

    /// <summary>
    /// 시퀀스 시 어디있는지와 WaferData 연결
    /// </summary>
    [Serializable]
    public class SeqShared
    {
        [XmlIgnore]
        private static readonly object objLock = new object();

        public WaferData[] carrierArray1 = new WaferData[MCDF.MAX_SLOT_COUNT];
        public WaferData[] deviceArray1 = new WaferData[MCDF.MAX_SLOT_COUNT];
        public WaferData[] carrierArray2 = new WaferData[MCDF.MAX_SLOT_COUNT];
        public WaferData[] deviceArray2 = new WaferData[MCDF.MAX_SLOT_COUNT];

        //0 : 사용안함, 1: LowHand, 2: UpperHand
        public WaferData[] robotFM = new WaferData[MCDF.MAX_ARM_COUNT]; 
        public WaferData[] robotATM = new WaferData[MCDF.MAX_ARM_COUNT];
        public WaferData[] robotVTM = new WaferData[MCDF.MAX_ARM_COUNT];

        //         public WaferData[] robotFM = null;
        //         public WaferData[] robotATM = null;
        //         public WaferData[] robotVTM = null;
        public WaferData aligner = null;
        public WaferData[] lami = new WaferData[2];
        public WaferData bonder = null;
        public WaferData hp = null;
        public WaferData[] CP = new WaferData[MCDF.MAX_SLOT_CP];
        public WaferData[] loadlock = new WaferData[MCDF.MAX_SLOT_LOADLOCK];

        #region Serialization
        public bool Save()
        {
            string path = "D:\\temp.xml";
            lock (objLock)
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SeqShared));
                    using (StreamWriter wr = new StreamWriter(path))
                    {
                        xmlSerializer.Serialize(wr, this);
                    }
                }
                catch (Exception ex)
                {
                    //LogMgr.Inst.LogAdd(MCDF.EXCEPT_LOG, ex);
                    return false;
                }
            }

            return true;
        }

        public static SeqShared Load(string path)
        {
            SeqShared inst = null;

            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SeqShared));
                using (StreamReader rd = new StreamReader(path))
                {
                    inst = (SeqShared)xmlSerializer.Deserialize(rd);
                }
            }
            catch (Exception ex)
            {
                //LogMgr.Inst.LogAdd(MCDF.EXCEPT_LOG, ex);
            }

            return inst;
        } 
        #endregion

        /// <summary>
        /// 처음 시작할 때 EFME에 전부 로딩 (기타 위치에 있는 데이터 언로딩)
        /// 자재를 수동으로 먼저 뺀 후 부르는게 좋다
        /// 쓰레드가 접근 중인 런 상태에서는 부르면 안됨
        /// </summary>
        /// <param name="nMaxSlot"></param>
        public void Init(int nMaxSlot)
        {
            for (int nSlot = 0; nSlot < MCDF.MAX_SLOT_COUNT; nSlot++)
            {
                if (nSlot < nMaxSlot)
                {
                    carrierArray1[nSlot] = new WaferData(EFEM_TYPE.A_CARRIER, WaferType.CARRIER, nSlot);
                    deviceArray1[nSlot] = new WaferData(EFEM_TYPE.C_DEVICE, WaferType.DEVICE, nSlot);

                    carrierArray2[nSlot] = new WaferData(EFEM_TYPE.B_CARRIER, WaferType.CARRIER, nSlot);
                    deviceArray2[nSlot] = new WaferData(EFEM_TYPE.D_DEVICE, WaferType.DEVICE, nSlot);
                }
                else
                {
                    carrierArray1[nSlot] = null;
                    deviceArray1[nSlot] = null;

                    carrierArray2[nSlot] = null;
                    deviceArray2[nSlot] = null;
                }
            }

            aligner = null;
            lami[0] = null;
            lami[1] = null;
            bonder = null;
            hp = null;

            for (int nSlot = 0; nSlot < MCDF.MAX_ARM_COUNT; nSlot++)
            {
                robotFM[nSlot] = null;
                robotATM[nSlot] = null;
                robotVTM[nSlot] = null;
            }

            for (int nSlot = 0; nSlot < MCDF.MAX_SLOT_CP; nSlot++)
            {
                CP[nSlot] = null;
            }

            for (int nSlot = 0; nSlot < MCDF.MAX_SLOT_LOADLOCK; nSlot++)
            {
                loadlock[nSlot] = null;
            }

            Save();
        }

        WaferData[] GetEfemData(EFEM_TYPE efType)
        {
            switch (efType)
            {
                case EFEM_TYPE.A_CARRIER:
                    return carrierArray1;
                case EFEM_TYPE.B_CARRIER:
                    return carrierArray2;
                case EFEM_TYPE.C_DEVICE:
                    return deviceArray1;
                case EFEM_TYPE.D_DEVICE:
                    return deviceArray2;
                default:
                    System.Diagnostics.Debug.Fail("EFEM_TYPE이 이상함");
                    break;
            }

            return null;
        }

        int GetLoadlockSlot(WaferType wType)
        {
            int nSlot = -1;
            switch (wType)
            {
                case WaferType.CARRIER:
                    nSlot = 0;
                    break;
                case WaferType.DEVICE:
                    nSlot = 1;
                    break;
                case WaferType.BONDED:
                    nSlot = 2;
                    break;
                default:
                    System.Diagnostics.Debug.Fail("타입이 이상함");
                    break;
            }

            return nSlot;
        }

        #region Wafer를 들고있는지 확인하는 함수

        /// <summary>
        /// EFEM의 슬롯에 Wafer가 있는지
        /// </summary>
        /// <param name="efType"></param>
        /// <param name="nSlot"></param>
        /// <returns></returns>
        public bool IsInSlotEFEM(EFEM_TYPE efType, int nSlot)
        {
            WaferData[] wData = GetEfemData(efType);
            if (wData[nSlot] == null)
                return false;

            return true;
        }

        /// <summary>
        /// FM 로봇이 Wafer를 들고 있는지
        /// </summary>
        /// <returns></returns>
        public bool IsInFM(HAND arm)
        {
            if (robotFM[(int)arm] == null)
                return false;

            return true;
        }

        /// <summary>
        /// ATM 로봇이 Wafer를 들고 있는지
        /// </summary>
        /// <returns></returns>
        public bool IsInATM(HAND arm)
        {
            if (robotATM[(int)arm] == null)
                return false;

            return true;
        }

        /// <summary>
        /// VTM 로봇이 Wafer를 들고 있는지
        /// </summary>
        /// <returns></returns>
        public bool IsInVTM(HAND arm)
        {
            if (robotVTM[(int)arm] == null)
                return false;

            return true;
        }

        /// <summary>
        /// Aligner가 Wafer를 들고 있는지
        /// </summary>
        /// <returns></returns>
        public bool IsInAligner()
        {
            if (aligner == null)
                return false;

            return true;
        }

        /// <summary>
        /// 라미에 Wafer가 있는지
        /// </summary>
        /// <returns></returns>
        public bool IsInLami(int nLami)
        {
            if (lami[nLami] == null)
                return false;

            return true;
        }

        /// <summary>
        /// 본더에 Wafer가 있는지
        /// </summary>
        /// <returns></returns>
        public bool IsInBonder()
        {
            if (bonder == null)
                return false;

            return true;
        }

        /// <summary>
        /// CP에 Wafer가 있는지
        /// </summary>
        /// <param name="nSlot"></param>
        /// <returns></returns>
        public bool IsInCP(int nSlot)
        {
            if (CP[nSlot] == null)
                return false;

            return true;
        }

        /// <summary>
        /// 로드락에 Wafer가 있는지
        /// </summary>
        /// <param name="nSlot"></param>
        /// <returns></returns>
        public bool IsInLoadLock(int nSlot)
        {
            if (loadlock[nSlot] == null)
                return false;

            return true;
        }

        /// <summary>
        /// Hot Plate에 Wafer가 있는지
        /// </summary>
        /// <returns></returns>
        public bool IsInHP()
        {
            if (hp == null)
                return false;

            return true;
        }

        #endregion


        #region Wafer 이동 함수

        /// <summary>
        /// EFEM에서 FM Robot으로 이동
        /// </summary>
        /// <param name="nSlot"></param>
        public bool LoadingEfemToFm(EFEM_TYPE efType, int nSlot, HAND arm)
        {
            WaferData[] wData = GetEfemData(efType);
            if (wData[nSlot] == null || robotFM[(int)arm] != null)
                return false;

            robotFM[(int)arm] = wData[nSlot];
            wData[nSlot] = null;

            Save();

            return true;
        }

        /// <summary>
        /// FM Robot에서 Aligner로 이동
        /// </summary>
        /// <returns></returns>
        public bool LoadingFmToAligner(HAND arm)
        {
            if (robotFM[(int)arm] == null || aligner != null)
                return false;

            aligner = robotFM[(int)arm];
            robotFM[(int)arm] = null;

            Save();

            return true;
        }

        /// <summary>
        /// Aligner에서 ATM으로 이동
        /// </summary>
        /// <returns></returns>
        public bool LoadingAlignerToAtm(HAND arm)
        {
            if (aligner == null || robotATM[(int)arm] != null)
                return false;

            robotATM[(int)arm] = aligner;
            aligner = null;

            Save();

            return true;
        }

        /// <summary>
        /// ATM에서 LAMI로 이동
        /// </summary>
        /// <returns></returns>
        public bool LoadingAtmToLami(HAND arm, int nlami)
        {
            if (robotATM[(int)arm] == null || lami[nlami] != null)
                return false;

            lami[nlami] = robotATM[(int)arm];
            robotATM[(int)arm] = null;

            Save();

            return true;
        }

        /// <summary>
        /// LAMI에서 ATM으로 이동
        /// </summary>
        /// <returns></returns>
        public bool LoadingLamiToAtm(HAND arm, int nlami)
        {
            if (lami[nlami] == null || robotATM[(int)arm] != null)
                return false;

            robotATM[(int)arm] = lami[nlami];
            lami[nlami] = null;

            Save();

            return true;
        }

        /// <summary>
        /// ATM에서 Loadlock으로 이동
        /// </summary>
        /// <param name="nSlot">Loadlock 슬롯</param>
        /// <returns></returns>
        public bool LoadingAtmToLoadLock(int nSlot, HAND arm)
        {
            if (robotATM[(int)arm] == null || loadlock[nSlot] != null)
                return false;

            loadlock[nSlot] = robotATM[(int)arm];
            robotATM[(int)arm] = null;

            Save();

            return true;
        }

        /// <summary>
        /// ATM에서 LoadLock으로 이동
        /// </summary>
        /// <param name="wType">Wafer Type</param>
        /// <returns></returns>
        public bool LoadingAtmToLoadLock(WaferType wType, HAND arm)
        {
            int nSlot = GetLoadlockSlot(wType);

            if (robotATM[(int)arm] == null || loadlock[nSlot] != null)
                return false;

            loadlock[nSlot] = robotATM[(int)arm];
            robotATM[(int)arm] = null;

            Save();

            return true;
        }

        /// <summary>
        /// Loadlock에서 VTM으로 이동
        /// </summary>
        /// <param name="nSlot">Loadlock 슬롯</param>
        /// <returns></returns>
        public bool LoadingLoadLockToVtm(int nSlot, HAND arm)
        {
            if (loadlock[nSlot] == null || robotVTM[(int)arm] != null)
                return false;

            robotVTM[(int)arm] = loadlock[nSlot];
            loadlock[0] = null;
            loadlock[1] = null;
            Save();

            return true;
        }

        /// <summary>
        /// Loadlock에서 VTM으로 이동
        /// </summary>
        /// <param name="wType">로드락 슬롯</param>
        /// <returns></returns>
        public bool LoadingLoadLockToVtm(WaferType wType, HAND arm)
        {
            int nSlot = GetLoadlockSlot(wType);

            if (loadlock[nSlot] == null || robotVTM[(int)arm] != null)
                return false;

            robotVTM[(int)arm] = loadlock[nSlot];
            loadlock[0] = null;
            loadlock[1] = null;

            Save();

            return true;
        }

        ///// <summary>
        ///// Loadlock에서 VTM으로 이동
        ///// </summary>
        ///// <param name="nSlot">Loadlock 슬롯</param>
        ///// <returns></returns>
        //public bool LoadingLoadLockToVtm(int nSlot, HAND arm)
        //{
        //    if (loadlock[nSlot] == null || robotVTM[(int)arm] != null)
        //        return false;

        //    robotVTM[(int)arm] = loadlock[nSlot];
        //    loadlock[nSlot] = null;

        //    Save();

        //    return true;
        //}

        ///// <summary>
        ///// Loadlock에서 VTM으로 이동
        ///// </summary>
        ///// <param name="wType">로드락 슬롯</param>
        ///// <returns></returns>
        //public bool LoadingLoadLockToVtm(WaferType wType, HAND arm)
        //{
        //    int nSlot = GetLoadlockSlot(wType);

        //    if (loadlock[nSlot] == null || robotVTM[(int)arm] != null)
        //        return false;

        //    robotVTM[(int)arm] = loadlock[nSlot];
        //    loadlock[nSlot] = null;

        //    Save();

        //    return true;
        //}

        /// <summary>
        /// VTM에서 Bonder로 이동
        /// </summary>
        /// <returns></returns>
        public bool LoadingVtmToBonder(HAND arm)
        {
            if (robotVTM[(int)arm] == null || bonder != null)
                return false;

            bonder = robotVTM[(int)arm];
            robotVTM[(int)arm] = null;

            Save();

            return true;
        }

        /// <summary>
        /// Bonder에서 VTM으로 이동
        /// </summary>
        /// <returns></returns>
        public bool LoadingBonderToVtm(HAND arm)
        {
            if (bonder == null || robotVTM[(int)arm] != null)
                return false;

            robotVTM[(int)arm] = bonder;
            bonder = null;

            Save();

            return true;
        }

        /// <summary>
        /// VTM에서 HP로 이동
        /// </summary>
        /// <returns></returns>
        public bool LoadingVtmToHp(HAND arm)
        {
            if (robotVTM[(int)arm] == null || hp != null)
                return false;

            hp = robotVTM[(int)arm];
            robotVTM[(int)arm] = null;

            Save();

            return true;
        }

        /// <summary>
        /// HP에서 VTM으로 이동
        /// </summary>
        /// <returns></returns>
        public bool LoadingHpToVtm(HAND arm)
        {
            if (hp == null || robotVTM[(int)arm] != null)
                return false;

            robotVTM[(int)arm] = hp;
            hp = null;

            Save();

            return true;
        }

        /// <summary>
        /// VTM에서 Loadlock으로 이동
        /// </summary>
        /// <param name="nSlot">로드락 슬롯</param>
        /// <returns></returns>
        public bool LoadingVtmToLoadlock(int nSlot, HAND arm)
        {
            if (robotVTM[(int)arm] == null || loadlock[nSlot] != null)
                return false;

            loadlock[nSlot] = robotVTM[(int)arm];
            robotVTM[(int)arm] = null;

            Save();

            return true;
        }

        /// <summary>
        /// VTM에서 Loadlock으로 이동
        /// </summary>
        /// <param name="wType">Wafer 타입</param>
        /// <returns></returns>
        public bool LoadingVtmToLoadlock(WaferType wType, HAND arm)
        {
            int nSlot = GetLoadlockSlot(wType);

            if (robotVTM[(int)arm] == null || loadlock[nSlot] != null)
                return false;

            loadlock[nSlot] = robotVTM[(int)arm];
            robotVTM[(int)arm] = null;

            Save();

            return true;
        }

        /// <summary>
        /// Loadlock에서 ATM으로 이동
        /// </summary>
        /// <param name="nSlot"></param>
        /// <returns></returns>
        public bool LoadingLoadlockToAtm(int nSlot, HAND arm)
        {
            if (loadlock[nSlot] == null || robotATM[(int)arm] != null)
                return false;

            robotATM[(int)arm] = loadlock[nSlot];
            loadlock[nSlot] = null;

            Save();

            return true;
        }
        
        /// <summary>
        /// ATM에서 CP로 이동
        /// </summary>
        /// <param name="nSlot">CP 슬롯 번호</param>
        /// <returns></returns>
        public bool LoadingAtmToCp(int nSlot, HAND arm)
        {
            if (robotATM[(int)arm] == null || CP[nSlot] != null)
                return false;

            CP[nSlot] = robotATM[(int)arm];
            robotATM[(int)arm] = null;

            Save();

            return true;
        }

        /// <summary>
        /// CP에서 FM으로 이동
        /// </summary>
        /// <param name="nSlot"></param>
        /// <returns></returns>
        public bool LoadingCpToFm(int nSlot, HAND arm)
        {
            if (CP[nSlot] == null || robotFM[(int)arm] != null)
                return false;

            robotFM[(int)arm] = CP[nSlot];
            CP[nSlot] = null;

            Save();

            return true;
        }

        /// <summary>
        /// FM에서 EFEM으로 이동
        /// </summary>
        /// <param name="efType"></param>
        /// <param name="nSlot"></param>
        /// <returns></returns>
        public bool LoadingFmToEfem(EFEM_TYPE efType, int nSlot, HAND arm)
        {
            WaferData[] wData = GetEfemData(efType);
            if (robotFM[(int)arm] == null || wData[nSlot] != null)
                return false;

            wData[nSlot] = robotFM[(int)arm];
            robotFM[(int)arm] = null;

            Save();

            return true;
        }
        #endregion


        #region 프로세스 끝남 확인
        /// <summary>
        /// PreAlign이 끝남
        /// </summary>
        /// <returns></returns>
        public bool PreAlign()
        {
            if (aligner == null)
                return false;

            aligner.PreAlign();
            Save();

            return true;
        }

        /// <summary>
        /// PostAlign이 끝남
        /// </summary>
        /// <returns></returns>
        public bool PostAlign()
        {
            if (aligner == null)
                return false;

            aligner.PostAlign();
            Save();

            return true;
        }

        /// <summary>
        /// 라미 끝남
        /// </summary>
        /// <returns></returns>
        public bool Laminate(int nlami)
        {
            if (lami[nlami] == null)
                return false;

            lami[nlami].Laminate();
            Save();

            return true;
        }

        /// <summary>
        /// 본딩 끝남
        /// </summary>
        /// <returns></returns>
        public bool Bonding()
        {
            if (bonder == null)
                return false;

            bonder.Bonding();
            Save();

            return true;
        }

        /// <summary>
        /// Hot Plate 끝남
        /// </summary>
        /// <returns></returns>
        public bool HotPlate()
        {
            if (hp == null)
                return false;

            hp.HotPlate();
            Save();

            return true;
        } 
        #endregion
    }


    [Serializable]
    public class Config
    {
        public int nRobotStatusTimeOut;
        public int nRobotMoveTimeOut;

        public Config()
        {

        }
    }

    [Serializable]
    public class WorkModel
    {
        public int[] nLoadlockPos = new int[5];
        public int nLLSpeed;
        public int nLLAcc;

        public WorkModel()
        {
            nLLSpeed = 51200;
            nLLAcc = 51200;
        }
    }

    public struct ALIGNER_RECIPE
    {
        public int nNo;
        public bool bUseMode;
        public float fCarrAngle;  //Carrier용 Align Angle
        public float fDeviceAngle; //Device용
    }

    public struct LAMI_RECIPE
    {
        public int nNo;
        public bool bUseMode;
        public int nProcessTime;
        public double dPressure;
        public double dUpperTemp;
        public double dLowerTemp;
        public double dPressingTime;
    }

    public struct PMC_RECIPE
    {
        public bool bUseVision;
        public double dPressure;
        public double dPressingTime;
        public double dUpperTemp;
        public double dLowerTemp;
        public double dAPC_Pos;
        public double dCh_1;
        public double dCh_2;
        public double dCh_3;
        public double dStandby_UpTemp;
        public double dStandby_LowTemp;
    }

    public struct McStage
    {
        public bool isRdy;          // 장비 초기화 확인 플래그
        public bool isRun;          // Auto 작동 확인 플래그
        public bool isManualRun;    // Manual 작동 확인 플래그
        public bool isWaitPopup;    // Popup 창 대기 플래그
        public bool isErr;
        public bool isLotEnd;

        //public bool isCycleStop;
        public bool isInitializing;

        public bool isInitialized;

        public bool isWateForStop;
        public bool isCheckStopPos;
        public bool isMovingStopPos;
        public bool isFinalizeBegin; //partial module pickup
        public bool isFinalizeEnd;
        public int nLastErrNo;
        public bool[] isHomeComplete;
        public int nCurLoginLevel;
        public bool isDry;
        public double[] dStopPos;
        public string strCurRecipe;
        public bool isWorkEnd;          // Work End
        public bool isPrevWorkEnd;
    };

    public struct ManualInfo
    {
        public HAND SelArmATM;
        public HAND SelArmFM;
        public HAND SelArmVTM;

        public FMStage mnlStageFM;
        public AtmStage mnlStageATM;
        public VtmStage mnlStageVTM;

        public int nSelectSource;
        public int nSelectDest;
        public int nSlotSource;
        public int nSlotDest;
    }

    public struct LoadlockMotor
    {
        public int nModule;
        public int nSpeed;
        public int nAcc;

        public int nActPos;
        public int nEncPos;
        public bool bMoving;
        public bool bInpos;
        public bool bLimitP;
        public bool bLmitN;
        public bool bHome;
        public bool bAlarm;
        public bool bServo;
        public int nHomeStatus;
        public bool bIsOpen;
    }

    public struct Interlock
    {
        public bool bLLMoving; //동시동작 시 확인필요 ATM,VTM
        public bool bAlignMoving;
        public bool bBondingProcess;
    }

    public struct WaferInfo
    {
        //public WaferType bWaferTypeAL;
        //public WaferType bWaferTypeLL;

        public bool[,] bWaferUnloadExist;
        public int[] nWaferLoadSlot;
        public int[] nWaferUnloadSlot;

        //         public bool bWaferLL1;
        //         public bool bWaferLL2;
        //         public bool bWaferBD;
        //         public bool bWaferLami;
        //         public bool bWaferCP1;
        //         public bool bWaferCP2;
        //         public bool bWaferCP3;
        //         public bool bWaferAL;
        //         public bool bWaferPmc;
        // 
        //         public bool bWaferFmUp;
        //         public bool bWaferFmLow;
        //         public bool bWaferAtmUp;
        //         public bool bWaferAtmLow;
        //         public bool bWaferVtmUp;
        //         public bool bWaferVtmLow;
        // 
        //         public bool bLamiLoad;
        //         public bool bLamiUnload;
        // 
        //         public bool bPmcLoad;
        //         public bool bPmcUnload;


        //Simul
         public bool bLamiLoad;
         public bool bLamiUnload;
         
         public bool bPmcLoad;
         public bool bPmcUnload;

    }
}
