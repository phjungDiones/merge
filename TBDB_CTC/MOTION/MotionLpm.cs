using CJ_Controls.Communication.Nano300;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_CTC.Data;
using TBDB_Handler.GLOBAL;

namespace TBDB_Handler.MOTION
{
    public enum EFEM
    {
        LPMA = 0,
        LPMB,
        LPMC,
        LPMD,
    }

    public enum ScanMode
    {
        Exist,
        Cross,
        Double,
    }

    public enum LPMStatus
    {
        Load,
        Unload,
    }

    public enum Mode
    {
        ModeAuto = 0,
        ModeMaint,
        ModeMapping,
    }
    

    public class MotionLpm
    {
       private MainData _Main = null;

       public MotionLpm()
        {
            _Main = MainData.Instance;
        }

       #region Send Cmd
       public bool Initialize(EFEM Lpm)
       {
           string Comport = "";
           int nBaud = 0;

           switch (Lpm)
           {
               case EFEM.LPMA:
                   Comport = _Main.ConfigMgr.Port1.Comport;
                   nBaud = _Main.ConfigMgr.Port1.Baudrate;
                   break;

               case EFEM.LPMB:
                   Comport = _Main.ConfigMgr.Port2.Comport;
                   nBaud = _Main.ConfigMgr.Port2.Baudrate;
                   break;

               case EFEM.LPMC:
                   Comport = _Main.ConfigMgr.Port3.Comport;
                   nBaud = _Main.ConfigMgr.Port3.Baudrate;
                   break;

               case EFEM.LPMD:
                   Comport = _Main.ConfigMgr.Port4.Comport;
                   nBaud = _Main.ConfigMgr.Port4.Baudrate;
                   break;
           }

           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Open(Comport, nBaud);
           Thread.Sleep(300);
           return _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().IsOpen();
       }

       public bool MoveHome(EFEM Lpm)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_Home();
           return true;
       }
       public bool SetReset(EFEM Lpm)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_Reset();
           return true;
       }

       public bool SetMode(EFEM Lpm, Mode mode, bool bOn)
       {
           switch (mode)
           {
               case Mode.ModeAuto:
                   _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_AutoMode(bOn);
                   break;

               case Mode.ModeMaint:
                   _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_MAINT_MODE(bOn);
                   break;

               case Mode.ModeMapping:
                   _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_MappingMode(bOn);
                   break;
           }   
           return true;
       }

       public bool AmpOnOff(EFEM Lpm, bool bOn)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_AmpOnOff(bOn);
           return true;
       }

       public bool MoveLock(EFEM Lpm)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_POD_LOCK(true);
           return true;
       }

       public bool MoveUnlock(EFEM Lpm)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_POD_LOCK(false);
           return true;
       }

       public bool MoveLoad(EFEM Lpm)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_Open();
           return true;
       }

       public bool MoveUnload(EFEM Lpm)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_Close();
           return true;
       }

       public bool MoveDockOnOff(EFEM Lpm, bool bOn)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_Dock(bOn);
           return true;
       }

       public bool MoveScanUpDown(EFEM Lpm, bool bUpDown)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_ScanUp(bUpDown);
           return true;
       }

       public bool MoveAbort(EFEM Lpm)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_Abort();
           return true;
       }

       public bool GetMapping(EFEM Lpm)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_GetMapping();
           return true;
       }

       public bool GetStatus(EFEM Lpm)
        {
            _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_Send_Status();
           return true;
            
        }

        public void InitMappingFlag(EFEM Lpm)
        {
            _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.bRecvMapping = false;

        }
        public bool GetMappingComplete(EFEM Lpm)
        {
            return _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.bRecvMapping;           
        }

        #endregion


        public bool GetUnloadSlot(EFEM Lpm, int nSlot)
       {
           return GlobalVariable.WaferInfo.bWaferUnloadExist[(int)Lpm, nSlot];
       }

       public void SetUnloadSlot(EFEM Lpm, int nSlot, bool bSet)
       {
           GlobalVariable.WaferInfo.bWaferUnloadExist[(int)Lpm, nSlot] = bSet;
       }

       public bool CheckWaferSlot(EFEM Lpm, int nMaxCnt, ref int nSlot)
       {
           //웨이퍼 스캔 시 Cross, Double 상태인 웨이퍼가 있는지 확인 
           //있다면 에러 처리 해야 함

           bool bCheck = false;          
           for (int i = 0; i < nMaxCnt; i++ )
           {
               bCheck = GetWaferInfo(Lpm, i, ScanMode.Cross); //Cross Data
               if (bCheck)
               {
                   nSlot = i; break; //검색 된 Slot Index                  
               }

               bCheck = GetWaferInfo(Lpm, i, ScanMode.Double); //Double Data
               if (bCheck)
               {
                   nSlot = i; break;                   
               }
           }
           return bCheck;
       }

       public bool GetWaferInfo(EFEM Lpm, int nIndex, ScanMode Scan )   
       {
           bool bCheck = false;
           switch (Scan)
           {
               case ScanMode.Exist:
                   bCheck = _Main.GetLoaderData().GetPortData((int)Lpm).WaferData[nIndex].WaferUse;
                   break;

               case ScanMode.Cross:
                   bCheck = _Main.GetLoaderData().GetPortData((int)Lpm).WaferData[nIndex].WaferCross;
                   break;

               case ScanMode.Double:
                   bCheck = _Main.GetLoaderData().GetPortData((int)Lpm).WaferData[nIndex].WaferDouble;
                   break;
     
               default:
                   break;
           }
           return bCheck;
       }
        public LPM_Wafer GetLPMWaferStauts(EFEM Lpm, int nIndex)
        {
            if( GetUnloadSlot(Lpm, nIndex ) )
            {
                return LPM_Wafer.Unload;
            }
            else if(GetWaferInfo(Lpm, nIndex, ScanMode.Exist))
            {
                return LPM_Wafer.Exist;
            }
            else
            {
                return LPM_Wafer.Empty;
            }
        }

        

       public void SetWaferInfo(EFEM Lpm, int nIndex, bool bSet)
       {
            //테스트용 함수
           _Main.GetLoaderData().GetPortData((int)Lpm).WaferData[nIndex].WaferUse = bSet;
       }

       public int GetNextWaferSlot(EFEM Lpm, LPMStatus status, int nMaxCnt)
       {
           int nRet = -1;
           for(int i=0; i< nMaxCnt; i++)
           {
               if (status == LPMStatus.Load)
               {
                   //LPM Load시 Mapping Data
                   if (GetWaferInfo(Lpm, i, ScanMode.Exist))
                   {
                       nRet = i; break;//현재 Index를 넘기자
                   } 
               }
               else
               {
                   //Unload 
                   if( GetUnloadSlot(Lpm, i) == false) //Slot이 비었을 경우
                   {
                       nRet = i; break;
                   }
               }
           }
           return nRet;
       }

       #region Read Cmd

       public bool GetErrorCode(EFEM Lpm)
       {
           _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().Cmd_ReadErrorCode();
           return true;
       }

       public bool IsHomeCheck(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_HOME_COMP] == 1)
               return true;
           else
               return false;
       }

       public bool IsAlarmCheck(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_ALARM_OCCURED] == 1)
               return true;
           else
               return false;
       }

       public bool IsDoorOpen(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_OPEN] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_CLOSE] == 0)
               return true;
           else
               return false;
       }

       public bool IsDoorClose(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_CLOSE] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_OPEN] == 0)
               return true;
           else
               return false;
       }

       public bool IsPoupClamp(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_CLAMPED] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNCLAMPED] == 0)
               return true;
           else
               return false;
       }

       public bool IsPoupUnClamp(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNCLAMPED] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_CLAMPED] == 0)
               return true;
           else
               return false;
       }

       public bool IsPoupDock(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_DOCKED] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNDOCKED] == 0)
               return true;
           else
               return false;
       }

       public bool IsPoupUnDock(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_UNDOCKED] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FOUP_DOCKED] == 0)
               return true;
           else
               return false;
       }

       public bool IsLatchLock(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_LATCH_LOCKED] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_LATCH_UNLOCKED] == 0)
               return true;
           else
               return false;
       }

       public bool IsLatchUnlock(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_LATCH_UNLOCKED] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_LATCH_LOCKED] == 0)
               return true;
           else
               return false;
       }

       public bool IsArmExtend(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FINGER_ARM_EXTENDED] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FINGER_ARM_RETRACTED] == 0)
               return true;
           else
               return false;
       }

       public bool IsArmRetract(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FINGER_ARM_RETRACTED] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_FINGER_ARM_EXTENDED] == 0)
               return true;
           else
               return false;
       }

       public bool IsVacuum(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_VACCUM_STATUS] == 1)
               return true;
           else
               return false;
       }

       public bool IsActing(EFEM Lpm)
       {
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_ACTING] == 1)
               return true;
           else
               return false;
       }

       public bool CheckLoad(EFEM Lpm)
       {
#if !_REAL_MC
           return true;
#endif
          //if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_RESERVED] == 1
          //    && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_OPEN] == 1
          //    && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_CLOSE] == 0
          //    && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().WorkStatus == WORK_STATUS.IDLE)
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().WorkStatus == WORK_STATUS.IDLE)
              return true;
          else
              return false;
       }

       public bool CheckUnload(EFEM Lpm)
       {
#if !_REAL_MC
           return true;
#endif
           if (_Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_RESERVED] == 0
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_OPEN] == 0
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().LPM_Data.StatusData[(int)STATUS_DATA.STS_DOOR_CLOSE] == 1
               && _Main.GetLoaderData().GetPortData((int)Lpm).GetNano300().WorkStatus == WORK_STATUS.IDLE)
               return true;
           else
               return false;
       }

       #endregion
    }
}
