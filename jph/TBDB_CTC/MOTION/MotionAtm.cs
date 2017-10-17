﻿using CJ_Controls.Communication.CybogRobot_HTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_CTC.Data;

namespace TBDB_Handler.MOTION
{
     
    public class MotionAtm
    {
        private MainData _Main = null;

        public MotionAtm()
        {
            _Main = MainData.Instance;
        }

        #region Send Cmd
        public bool Initialize()
        {
            _Main.GetAtmTmData().Robot.Open(_Main.ConfigMgr.AtmTM_Robot_Com.Comport, _Main.ConfigMgr.AtmTM_Robot_Com.Baudrate);

            Thread.Sleep(300);

            return _Main.GetAtmTmData().Robot.IsOpen();
        }

        public bool MoveHome()
        {
            _Main.GetAtmTmData().Robot.Cmd_WriteInitialize();
            return true;
        }

        public bool ServoOn()
        {
            _Main.GetAtmTmData().Robot.Cmd_WriteServoOnOff(true);
            return true;
        }

        public bool ServoOff()
        {
            _Main.GetAtmTmData().Robot.Cmd_WriteServoOnOff(false);
            return true;
        }

        public bool MoveStop(PAUSE_STATUS _Status)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_MotionPauseStop(_Status);
            return true;
        }

        public bool MoveReadyPos()
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_GoReady();
            return true;
        }

        public bool MoveWaitFold(int nStage, int nSlot, ARM _Hand)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_MoveWait_FG(nStage, nSlot, _Hand);
            return true;
        }

        public bool MovePickup(int nStage, int nSlot, ARM _Hand)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_Get(nStage, nSlot, _Hand);
            return true;
        }

        public bool MovePlace(int nStage, int nSlot, ARM _Hand)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_Put(nStage, nSlot, _Hand);
            return true;
        }

        public bool MovePickPlace(int nGetStage, int nGetSlot, ARM _GetHand, int nPutStage, int nPutSlot, ARM _PutHand)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_Transfer(nGetStage, nGetSlot, _GetHand, nPutStage, nPutSlot, _PutHand);
            return true;
        }

        public bool MoveArmFold()
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_ArmFold();
            return true;
        }

        public bool MoveLowFlipHand(bool bOn)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_FlipHand(bOn);
            return true;
        }

        public bool SetHandVacuum(ARM _Hand, bool bOn)
        {
            _Main.GetAtmTmData().Robot.Cmd_Write_Move_ArmVac(_Hand, bOn);
            return true;
        }


        public bool AlarmClear()
        {
            _Main.GetAtmTmData().Robot.Cmd_WriteErrorReset();

            return true;
        }
        #endregion


        #region Read Cmd
        public bool IsHomeCheck()
        {           
            //_Main.GetAtmTmData().Robot.Cmd_Read_RobotStatus();
            if (_Main.GetAtmTmData().Robot.WorkStatus == WORK_STATUS.IDLE )
                return true;
            else
                return false;
        }

        public bool IsAlarmCheck()
        {
            if (_Main.GetAtmTmData().Robot.WorkStatus == WORK_STATUS.ERROR)
                return true;
            else
                return false;
        }


        public bool IsServoOn()
        {
            //_Main.GetAtmTmData().Robot.Cmd_Read_RobotStatus();
            if (_Main.GetAtmTmData().Robot.RobotData.ServoOnStatus == true)
                return true;
            else
                return false;
        }
        public bool IsMoving()
        {
            //_Main.GetAtmTmData().Robot.Cmd_Read_RobotStatus();
            if (_Main.GetAtmTmData().Robot.RobotData.RunStatus == true)
                return true;
            else
                return false;
        }

        public bool CheckComplete()
        {
#if !_REAL_MC
            return true;
#endif
            if (_Main.GetAtmTmData().Robot.WorkStatus == WORK_STATUS.IDLE)
                return true;
            else
                return false;
        }

        public bool IsArmFold()
        {
            //_Main.GetAtmTmData().Robot.Cmd_Read_RobotStatus();
            if (_Main.GetAtmTmData().Robot.RobotData.ArmFoldStatus == true)
                return true;
            else
                return false;
        }

        public bool IsLowHandVacOn()
        {
            //_Main.GetAtmTmData().Robot.Cmd_Read_RobotStatus();
            if (_Main.GetAtmTmData().Robot.RobotData.LowerHand_VacSts == true)
                return true;
            else
                return false;
        }

        public bool IsUpandVacOn()
        {
            //_Main.GetAtmTmData().Robot.Cmd_Read_RobotStatus();
            if (_Main.GetAtmTmData().Robot.RobotData.UpperHand_VacSts == true)
                return true;
            else
                return false;
        }

        public bool IsWaferUpper()
        {
            //_Main.GetAtmTmData().Robot.Cmd_Read_RobotStatus();
            if (_Main.GetAtmTmData().Robot.RobotData.UpperHand_WaferSts == true)
                return true;
            else
                return false;
        }

        public bool IsWaferLower()
        {
            //_Main.GetAtmTmData().Robot.Cmd_Read_RobotStatus();
            if (_Main.GetAtmTmData().Robot.RobotData.LowerHand_WaferSts == true)
                return true;
            else
                return false;
        }

        #endregion
    }
}