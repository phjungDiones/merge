using CJ_Controls.Communication.QuadraVTM4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_CTC.Data;

namespace TBDB_Handler.MOTION
{
    public class MotionVtm
    {

        private MainData _Main = null;


        public MotionVtm()
        {
            _Main = MainData.Instance;
        }

        #region Send Cmd
        public bool Initialize()
        {
            _Main.GetVaccumTmData().Robot.Open(_Main.ConfigMgr.VacTM_Robot_Com.Comport, _Main.ConfigMgr.VacTM_Robot_Com.Baudrate);

            Thread.Sleep(300);

            return _Main.GetVaccumTmData().Robot.IsOpen();
        }

        public bool AlarmClear()
        {
            _Main.GetVaccumTmData().Robot.Cmd_SendClear();
            return true;
        }

        public bool MoveHome()
        {
            _Main.GetVaccumTmData().Robot.Cmd_SendHomeAll();

            return true;
        }

        public bool MoveStop()
        {
            _Main.GetVaccumTmData().Robot.Cmd_SendRelease();
            return true;
        }

        public bool MoveStage(int nStage, int nSlot, ARM _Arm, RADIAL_POS _RadialPos, UPDOWN_POS _UpDnPos)
        {
            _Main.GetVaccumTmData().Robot.Cmd_SendGoTo(nStage, nSlot, _Arm, _RadialPos, _UpDnPos);
            return true;
        }

        public bool MovePickup(int nStage, int nSlot, ARM _Arm)
        {
            _Main.GetVaccumTmData().Robot.Cmd_SendPick(nStage, nSlot, _Arm);
            return true;
        }

        public bool MovePlace(int nStage, int nSlot, ARM _Arm)
        {
            _Main.GetVaccumTmData().Robot.Cmd_SendPlace(nStage, nSlot, _Arm);
            return true;
        }
        public bool MoveZAxisUp(int nStage, int nSlot, ARM _Arm)
        {
            _Main.GetVaccumTmData().Robot.Cmd_SendZAxis(nStage, nSlot, _Arm, UPDOWN_POS.UP);
            return true;
        }

        public bool MoveZAxisDown(int nStage, int nSlot, ARM _Arm)
        {
            _Main.GetVaccumTmData().Robot.Cmd_SendZAxis(nStage, nSlot, _Arm, UPDOWN_POS.DOWN);
            return true;
        }

        public bool MoveExtend(int nStage, int nSlot, ARM _Arm, UPDOWN_POS _UpDnPos)
        {
            RADIAL_POS _RadialPos = RADIAL_POS.EXTENDED;
            _Main.GetVaccumTmData().Robot.Cmd_SendGoTo(nStage, nSlot, _Arm, _RadialPos, _UpDnPos);
            //_Main.GetVaccumTmData().Robot.Cmd_Extend(nStage, nSlot, _Arm, _UpDnPos);
            return true;
        }

        public bool MoveRetract(int nStage, int nSlot, ARM _Arm, UPDOWN_POS _UpDnPos)
        {
            RADIAL_POS _RadialPos = RADIAL_POS.RETRACTED;
            _Main.GetVaccumTmData().Robot.Cmd_SendGoTo(nStage, nSlot, _Arm, _RadialPos, _UpDnPos);
            //_Main.GetVaccumTmData().Robot.Cmd_Retract(nStage, nSlot, _Arm, _UpDnPos);
            return true;
        }

        #endregion


        public bool IsHostModeCheck()
        {
            if (_Main.GetVaccumTmData().Robot.RobotData.HostControlMode == CONTROL_MODE.HOST)
                return true;
            else
                return false;
        }

        public bool IsAlarmCheck()
        {
            if (_Main.GetVaccumTmData().Robot.WorkStatus == WORK_STATUS.ERROR)
                return true;
            else
                return false;
        }

        public bool IsMoving()
        {
            if (_Main.GetVaccumTmData().Robot.WorkStatus == WORK_STATUS.MOVING)
                return true;
            else
                return false;
        }

        public bool CheckComplete()
        {
#if !_REAL_MC
            return true;
#endif
            if (_Main.GetVaccumTmData().Robot.WorkStatus == WORK_STATUS.IDLE)
                return true;
            else
                return false;
        }
        

        public bool IsWaferCheck(ARM _Arm)
        {
            if( _Arm == ARM.LOWER)
            {
                if (_Main.GetVaccumTmData().Robot.RobotData.Wafer_A_Status == WAFER_STS.PRESENT)
                    return true;
                else
                    return false;
            }
            else
            {
                if (_Main.GetVaccumTmData().Robot.RobotData.Wafer_B_Status == WAFER_STS.PRESENT)
                    return true;
                else
                    return false;
            }
        }

        public bool IsVacuumCheck(ARM _Arm)
        {
            if (_Arm == ARM.LOWER)
            {
                if (_Main.GetVaccumTmData().Robot.RobotData.Wafer_A_Status == WAFER_STS.PRESENT)
                    return true;
                else
                    return false;
            }
            else
            {
                if (_Main.GetVaccumTmData().Robot.RobotData.Wafer_B_Status == WAFER_STS.PRESENT)
                    return true;
                else
                    return false;
            }
        }
    }
}
