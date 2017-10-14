using CJ_Controls.Communication.PA300C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBDB_CTC.Data;

namespace TBDB_Handler.MOTION
{

    public class MotionAlinger
    {
        private MainData _Main = null;

        public MotionAlinger()
        {
            _Main = MainData.Instance;
        }

        #region Send Cmd
        public bool Initialize()
        {
            _Main.GetLoaderData().Aligner.Open(_Main.ConfigMgr.Aligner_Com.Comport, _Main.ConfigMgr.Aligner_Com.Baudrate);

            Thread.Sleep(300);

            return _Main.GetLoaderData().Aligner.IsOpen();
        }

        public bool MoveHome()
        {
            _Main.GetLoaderData().Aligner.Cmd_Send_HOME();

            return true;
        }

        public bool MoveAlign(float fVal)
        {
            _Main.GetLoaderData().Aligner.Cmd_Send_STAL(fVal);

            return true;
        }

        public bool MoveRotate(float fVal)
        {
            _Main.GetLoaderData().Aligner.Cmd_Send_ROTW(fVal);
            return true;
        }

        public bool MoveChuckDown()
        {
            _Main.GetLoaderData().Aligner.Cmd_Send_CKDN();
            return true;
        }

        public bool MoveChuckUp()
        {
            _Main.GetLoaderData().Aligner.Cmd_Send_CKUP();
            return true;
        }

        public bool VacuumOn()
        {
            _Main.GetLoaderData().Aligner.Cmd_Send_VAON();
            return true;
        }

        public bool VacuumOff()
        {
            _Main.GetLoaderData().Aligner.Cmd_Send_VAOF();

            return true;
        }

        public bool AlarmClear()
        {
            _Main.GetLoaderData().Aligner.Cmd_Send_ECLR();

            return true;
        }

        public bool GetVacuumStatus()
        {
            _Main.GetLoaderData().Aligner.Cmd_Read_VACH();
            return true;
        }


        #endregion


        #region Read Cmd
        public bool IsHomeCheck()
        { 
            //_Main.GetLoaderData().Aligner.Cmd_Read_STAT();
            if (_Main.GetLoaderData().Aligner.Pa300C_Data.AlignerStatus == ALIGNER_STATUS.READY)
                return true;
            else
                return false;
        }

        public bool IsAlarmCheck()
        {
            //_Main.GetLoaderData().Aligner.Cmd_Read_STAT();
            if (_Main.GetLoaderData().Aligner.Pa300C_Data.AlignerStatus == ALIGNER_STATUS.ERROR)
                return true;
            else
                return false;
        }

        public bool IsVacuumCheck()
        {
            //_Main.GetLoaderData().Aligner.Cmd_Read_VACH();
            if (_Main.GetLoaderData().Aligner.Pa300C_Data.Vaccum_Sens == ONOFF.ON)
                return true;
            else
                return false;
        }

        public bool IsMoviongAL()
        {
            //_Main.GetLoaderData().Aligner.Cmd_Read_STAT();
            if (_Main.GetLoaderData().Aligner.Pa300C_Data.AlignerStatus == ALIGNER_STATUS.BUSY)
                return true;
            else
                return false;
        }

        public bool IsWaferCheck()
        {
            //_Main.GetLoaderData().Aligner.Cmd_Read_CHKW();
            if (_Main.GetLoaderData().Aligner.Pa300C_Data.WaferStatus == WAFER_STATUS_AL.NO_WAFER)
                return false;
            else
                return true;
        }

        public bool CheckAlign()
        {
#if !_REAL_MC

            return true;
#endif
            if (_Main.GetLoaderData().Aligner.WorkStatus == WORK_STATUS.IDLE)
                return true;
            else
                return false;
        }
        public bool IsChuckUp()
        {
            if (_Main.GetLoaderData().Aligner.Pa300C_Data.ChuckPosition == CHUCK_POS.UP_POS)
                return true;
            else
                return false;
        }

        public bool IsChuckDown()
        {
            if (_Main.GetLoaderData().Aligner.Pa300C_Data.ChuckPosition == CHUCK_POS.DOWN_POS)
                return true;
            else
                return false;
        }

        public bool IsMovingChuck()
        {
            if (_Main.GetLoaderData().Aligner.Pa300C_Data.ChuckPosition == CHUCK_POS.MOVING_STS)
                return true;
            else
                return false;
        }

        public bool GetErrorCode()
        {
            if (_Main.GetLoaderData().Aligner.Pa300C_Data.ChuckPosition == CHUCK_POS.MOVING_STS)
                return true;
            else
                return false;
        }

        #endregion

    }
}
