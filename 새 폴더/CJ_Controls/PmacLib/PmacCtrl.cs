using System;
using System.Collections.Generic;
using System.Text;
using CJ_Controls.Pcomm32Functions;
using System.Xml.Serialization;
using System.IO;
using CJ_Controls;

/// 2016.08.17
/// Changjin.Jeong : C++ 에서 C# 용으로 변경
namespace CJ_Controls.PmacLib
{
	public enum UMAC_VARIABLE : int
	{
		I_VAR = 0,
		M_VAR = 1,
		P_VAR = 2,
		Q_VAR = 3,
	}

	public enum SERVO_CNT : int
	{
		MAX_AXIS_COUNT = 32,
	}
	public enum AXIS_TYPE : int
	{
		SERVO = 0,
		STEPPING,
		LINEAR,
	}
	public class PmacCtrl
	{
		#region 맴버
		public UInt32 m_dwDeviceNo = 0;
		public Int32 m_nDriverOpen = 0;
		object _obj = new object();

		private bool[] m_bIpIOSta;
		public bool[] IoInputVal
		{
			get { return m_bIpIOSta; }
		}
		private bool[] m_bOpIOSta;
		public bool[] IoOutputVal
		{
			get { return m_bOpIOSta; }
		}
		#endregion

		public PmacCtrl(UInt32 _DeviceNo, int nInputStartAddr, int nOutputStartAddr,  int nMaxIoCount)
		{
			m_dwDeviceNo = _DeviceNo;
			m_nInputStartAddr = nInputStartAddr;
			m_nOutputStartAddr = nOutputStartAddr;
			m_nMaxIoCount = nMaxIoCount;
			m_bIpIOSta = new bool[nMaxIoCount];
			m_bOpIOSta = new bool[nMaxIoCount];

			InitializePmac();
		}
		~PmacCtrl()
		{
			ClosePmac();
		}
		public void InitializePmac()
		{
			#region Scale 기존것..참조
			//////////////////////////////////////////////////////////////////////////
			///////////////////*	ScaleFactor Use Method	*/////////////////////////
			//////////////////////////////////////////////////////////////////////////
			//	If	Scale = 0.5 ==> 0.5㎛ * A = 1㎜ => A = 2000 
			//	And Scale = 1 ==> 1㎛ * B = 1㎜ => b = 1000
			//  Stage T : 360도 = 17175800   =>   1도 = 47710.55555555556
			//////////////////////////////////////////////////////////////////////////
			/*
			m_dScaleFactor[0] = 0;
			m_dScaleFactor[1] = 10000; //Stage Y
			m_dScaleFactor[2] = 10000;
			m_dScaleFactor[3] = 10000;
			m_dScaleFactor[4] = 10000;
			m_dScaleFactor[5] = 10000; //Stage X
			m_dScaleFactor[6] = 10000;
			m_dScaleFactor[7] = 10000;
			m_dScaleFactor[8] = 10000;
			m_dScaleFactor[9] = 47710.55555555556; //Stage T
			m_dScaleFactor[10] = 47710.55555555556;
			m_dScaleFactor[11] = 10000; //Stage Z
			m_dScaleFactor[12] = 10000;
			m_dScaleFactor[13] = 5000; //Picker
			m_dScaleFactor[14] = 5000;
			m_dScaleFactor[15] = 500; //Cutter
			m_dScaleFactor[16] = 500;
			m_dScaleFactor[17] = 1;
			m_dScaleFactor[18] = 1;
			m_dScaleFactor[19] = 1;
			m_dScaleFactor[20] = 1;
			m_dScaleFactor[21] = 1;
			m_dScaleFactor[22] = 1;
			m_dScaleFactor[23] = 1;
			m_dScaleFactor[24] = 1;
			m_dScaleFactor[25] = 1;
			m_dScaleFactor[26] = 1;
			m_dScaleFactor[27] = 10000; //가상축 Y
			m_dScaleFactor[28] = 10000;
			m_dScaleFactor[29] = 10000; //가상축 X
			m_dScaleFactor[30] = 10000;
			m_dScaleFactor[31] = 1;
			m_dScaleFactor[32] = 1;
			*/
			#endregion
		}

		public int OpenPmac()
		{
			int nRtn = 0;
            //m_dwDeviceNo = PMAC.PmacSelect(0);
			nRtn = PMAC.OpenPmacDevice(m_dwDeviceNo);
            if (nRtn <= 0)
			{
				//MessageBox.Show("장치가 OPEN되지 않았습니다..", "에러");
                m_nDriverOpen = nRtn;
			}
			else
			{
				m_nDriverOpen = nRtn;
			}
			return nRtn;
		}
		public bool IsOpen()
		{
			bool bRtn = false;
			if (m_nDriverOpen > 0)
				bRtn = true;
			return bRtn;
		}
		private void ClosePmac()
		{
			if (m_nDriverOpen == 0)
				return;

			PMAC.ClosePmacDevice(m_dwDeviceNo);
		}

		// Send Command
		public int SendCommand_StringBuilder(string strInput, ref string strRet)
		{
			if (m_nDriverOpen == 0)
				return -1;

			int nRtn = -1;
            lock (_obj)
            {
                StringBuilder strResponse = new StringBuilder();
                PMAC.PmacGetResponseA(m_dwDeviceNo, strResponse, 255, new StringBuilder(strInput));
                strRet = strResponse.ToString();
                nRtn = 1;
            }
			return nRtn;
		}
		public int SendCommand(string strInput, ref string strRet)
		{
			if (m_nDriverOpen == 0)
				return -1;

			int nRtn = -1;
			lock (_obj)
			{
				//byCommand = System.Text.Encoding.GetEncoding("euc-kr").GetBytes(strValue);
				byte[] _Array = Encoding.ASCII.GetBytes(strInput);
				byte[] _Res = new byte[255];
				PMAC.PmacGetResponseA(m_dwDeviceNo, _Res, 255, _Array);

				// strResponse = System.Text.Encoding.GetEncoding("euc-kr").GetString(byResponse);
				strRet = System.Text.Encoding.ASCII.GetString(_Res).TrimEnd('\0');
				nRtn = 1;
			}
			return nRtn;
		}
		public int SendCtrlCommand(byte byInput, ref string strRet)
		{
			if (m_nDriverOpen == 0)
				return -1;

			int nRtn = -1;
			lock (_obj)
			{
				StringBuilder strResponse = new StringBuilder();
				PMAC.PmacGetControlResponseA(m_dwDeviceNo, strResponse, 255, byInput);
				strRet = strResponse.ToString();
				nRtn = 1;
			}
			return nRtn;
		}

		// 특정 변수값 읽어 오기
		public int GetVariableUMAC(UMAC_VARIABLE eVarType, int nPos, ref double dValue)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;
			switch (eVarType)
			{
				case UMAC_VARIABLE.I_VAR: strCommand = string.Format("I{0}", nPos); break;
				case UMAC_VARIABLE.M_VAR: strCommand = string.Format("M{0}", nPos); break;
				case UMAC_VARIABLE.P_VAR: strCommand = string.Format("P{0}", nPos); break;
				case UMAC_VARIABLE.Q_VAR: strCommand = string.Format("Q{0}", nPos); break;
			}

			nRtn = SendCommand(strCommand, ref strRet);
			dValue = double.Parse(strRet);

			return nRtn;
		}

		// Axis 현재 값 읽어오기
		public int ReadAxisCurVal(AxisInfo _Axis)
		{
			string strCmd = "", strResData = "";
			int nRet = 0;
			strCmd += string.Format("I{0}08 ", _Axis.AxisNo_1Base); //
			strCmd += string.Format("I{0}09 ", _Axis.AxisNo_1Base); //
            strCmd += string.Format("I10 "); //Hardware Interlock (
			strCmd += string.Format("M{0}74 ", _Axis.AxisNo_1Base); //Speed (Vel)
			strCmd += string.Format("M{0}62 ", _Axis.AxisNo_1Base); //Current Position (Encoder)
			strCmd += string.Format("M{0}69 ", _Axis.AxisNo_1Base); //Position Offset (보정값)
			strCmd += string.Format("M{0}68 ", _Axis.AxisNo_1Base); //Torque On/Off
			strCmd += string.Format("M{0}21 ", _Axis.AxisNo_1Base); //Plus Limit
			strCmd += string.Format("M{0}22 ", _Axis.AxisNo_1Base); //Minus Limit
			strCmd += string.Format("M{0}39 ", _Axis.AxisNo_1Base); //Servo On/Off
			strCmd += string.Format("M{0}40 ", _Axis.AxisNo_1Base); //In position Bit (모터 정지 확인 : Stop=1, Moving=0)
			strCmd += string.Format("M{0}42 ", _Axis.AxisNo_1Base); //Following err
			strCmd += string.Format("M{0}43 ", _Axis.AxisNo_1Base); //Amp Fault
			nRet = SendCommand(strCmd, ref strResData);
			if (nRet > 0)
			{
				char[] _CmdSplitter = { '\r' };
				string[] strColInClipboard = strResData.Split(_CmdSplitter, StringSplitOptions.None);
                long Ix08 = 0; long.TryParse(strColInClipboard[0], out Ix08);
                long Ix09 = 0; long.TryParse(strColInClipboard[1], out Ix09);
                long Ix10 = 0; long.TryParse(strColInClipboard[2], out Ix10);
                float fSpeed = 0; float.TryParse(strColInClipboard[3], out fSpeed);
                float fCurPos = 0; float.TryParse(strColInClipboard[4], out fCurPos);
                float fPosOffset = 0; float.TryParse(strColInClipboard[5], out fPosOffset);
				float fTorque = 0; float.TryParse(strColInClipboard[6], out fTorque);
				int nPlusLimit = 0; int.TryParse(strColInClipboard[7], out nPlusLimit);
				int nMinusLimit = 0; int.TryParse(strColInClipboard[8], out nMinusLimit);
				int nServoOnOff = 0; int.TryParse(strColInClipboard[9], out nServoOnOff);
				int nInPosBit = 0; int.TryParse(strColInClipboard[10], out nInPosBit);
				int nFllowingErr = 0; int.TryParse(strColInClipboard[11], out nFllowingErr);
				int nAmpFault = 0; int.TryParse(strColInClipboard[12], out nAmpFault);

				//축 현재 정보 세팅.
                _Axis.CurInfo.Speed = (fSpeed / (Ix09 * 32.0) * (8388608.0 / Ix10) * 1000.0) / _Axis.Scale;
                _Axis.CurInfo.Position = (fCurPos + fPosOffset) / (Ix08 * 32.0) / _Axis.Scale;
				_Axis.CurInfo.Torque = (float)((fTorque / 32767.0) * 100.0);
				_Axis.CurInfo.Plimit = nPlusLimit;
				_Axis.CurInfo.Mlimit = nMinusLimit;
				_Axis.CurInfo.ServoOnOff = nServoOnOff;
				_Axis.CurInfo.InPositionBit = nInPosBit;
				_Axis.CurInfo.FollowingErr = nFllowingErr;
				_Axis.CurInfo.AmpFault = nAmpFault;
			}
			return nRet;
		}

        // 전체 IO 값 읽어오기.
		private int m_nInputStartAddr = 4000;
		private int m_nOutputStartAddr = 7000;
		private int m_nMaxIoCount = 200;
        public void ReadIOState()
        {
            string strCmd = "", strResIO = "";
            int nRet = 0;
            int nReadCount = 50;//Input : 유맥은 96개 이상을 한꺼번에 읽어오면 미친다고 한다.
            for (int nCnt = 0; nCnt < (int)m_nMaxIoCount; nCnt += nReadCount)
            {
                int nStartAddr = nCnt + (int)m_nInputStartAddr;
                int nEndAddr = nStartAddr + (nReadCount - 1);
                strCmd = string.Format("M{0}..{1}", nStartAddr, nEndAddr);
                nRet = SendCommand(strCmd, ref strResIO);
                if (nRet > 0)
                {
                    char[] _CmdSplitter = { '\r' };
                    string[] _IoArray = strResIO.Split(_CmdSplitter, StringSplitOptions.None);
                    for (int i = 0; i < nReadCount; i++)
                    {
                        int nBufferIndex = i;
                        int nIoIndex = nCnt + i;
                        if (_IoArray.Length > nBufferIndex
                            && m_bIpIOSta.Length > nIoIndex)
                        {
                            if (_IoArray[nBufferIndex] == "1")
                                m_bIpIOSta[nIoIndex] = true;
                            else
                                m_bIpIOSta[nIoIndex] = false;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            //Output : 유맥은 96개 이상을 한꺼번에 읽어오면 미친다고 한다.
            strResIO = "";
            for (int nCnt = 0; nCnt < (int)m_nMaxIoCount; nCnt += nReadCount)
            {
                int nStartAddr = nCnt + (int)m_nOutputStartAddr;
                int nEndAddr = nStartAddr + (nReadCount - 1);
                strCmd = string.Format("M{0}..{1}", nStartAddr, nEndAddr);
                nRet = SendCommand(strCmd, ref strResIO);
                if (nRet > 0)
                {
                    char[] _CmdSplitter = { '\r' };
                    string[] _IoArray = strResIO.Split(_CmdSplitter, StringSplitOptions.None);
                    for (int i = 0; i < nReadCount; i++)
                    {
                        int nBufferIndex = i;
                        int nIoIndex = nCnt + i;
                        if (_IoArray.Length > nBufferIndex
                            && m_bIpIOSta.Length > nIoIndex)
                        {
                            if (_IoArray[nBufferIndex] == "1")
                                m_bOpIOSta[nIoIndex] = true;
                            else
                                m_bOpIOSta[nIoIndex] = false;
                        }
                    }
                }
            }
        }
		public void ReadIOState_OneCmd()
		{
			string strCmd = "", strResIO = "";
            int nRet = 0;
            int nCycle = 10;
            for (int i = 0; i < 14; i++)
            {
                strCmd = "";
                for (int j = 0; j < nCycle; j++)
                {
                    strCmd += string.Format("M{0:0000} ", (int)m_nInputStartAddr + j + (i * nCycle));
                }
                nRet = SendCommand(strCmd, ref strResIO);
                if (nRet > 0)
                {
                    char[] _CmdSplitter = { '\r' };
                    string[] _IoArray = strResIO.Split(_CmdSplitter, StringSplitOptions.None);
                    for (int j = 0; j < nCycle; j++)
                    {
                        int nIndex = j + (i * nCycle);
                        if (_IoArray[j] == "1")
                            m_bIpIOSta[nIndex] = true;
                        else
                            m_bIpIOSta[nIndex] = false;
                    }
                }

                strCmd = "";
                for (int j = 0; j < nCycle; j++)
                {
                    strCmd += string.Format("M{0:0000} ", (int)m_nOutputStartAddr + j + (i * nCycle));
                }
                nRet = SendCommand(strCmd, ref strResIO);
                if (nRet > 0)
                {
                    char[] _CmdSplitter = { '\r' };
                    string[] _IoArray = strResIO.Split(_CmdSplitter, StringSplitOptions.None);
                    for (int j = 0; j < nCycle; j++)
                    {
                        int nIndex = j + (i * nCycle);
                        if (_IoArray[j] == "1")
                            m_bOpIOSta[nIndex] = true;
                        else
                            m_bOpIOSta[nIndex] = false;
                    }
                }
			}
		}

		// 특정 변수값 설정 하기
		public int SetVariableUMAC(UMAC_VARIABLE eVarType, int nPos, double dValue)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;

			switch (eVarType)
			{
				case UMAC_VARIABLE.I_VAR: strCommand = string.Format("I{0} = {1}", nPos, dValue); break;
				case UMAC_VARIABLE.M_VAR: strCommand = string.Format("M{0} = {1}", nPos, dValue); break;
				case UMAC_VARIABLE.P_VAR: strCommand = string.Format("P{0} = {1}", nPos, dValue); break;
				case UMAC_VARIABLE.Q_VAR: strCommand = string.Format("Q{0} = {1}", nPos, dValue); break;
			}

			nRtn = SendCommand(strCommand, ref strRet);
			return nRtn;
		}

		// Output IO 변수값 설정 하기
		public int SetOutIOValUMAC(int nIndex, bool bVal, bool bMustSet = false)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;
			if (!bMustSet)
			{
				// 현재 IO 상태가 지령값과 같으면 SendCommand 생략
				if (m_bOpIOSta[nIndex] == bVal)
					return 0;
			}
			strCommand = string.Format("M{0} = {1}", nIndex + (int)m_nOutputStartAddr, bVal ? 1 : 0);
			nRtn = SendCommand(strCommand, ref strRet);
			if (nRtn > 0)
			{
				m_bOpIOSta[nIndex] = bVal;
			}

			return nRtn;
		}
		public int SetOutIOValUMAC(DeviceNetIO _IO, bool bVal, bool bMustSet = false)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;
			int nIndex = _IO.Address - (int)m_nOutputStartAddr;
			if (!bMustSet)
			{
				// 현재 IO 상태가 지령값과 같으면 SendCommand 생략
				if (m_bOpIOSta[nIndex] == bVal)
					return 0;
			}
			strCommand = string.Format("M{0}={1}", _IO.Address, bVal ? 1 : 0);
			nRtn = SendCommand(strCommand, ref strRet);
			if (nRtn > 0)
			{
				m_bOpIOSta[nIndex] = bVal;
			}

			return nRtn;
		}
		public int SetOutIOValUMACArray(List<DeviceNetIO> _IoList, bool bVal)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;
			int nBit = bVal ? 1 : 0;
			for (int nCnt = 0; nCnt < _IoList.Count; nCnt++)
			{
				strCommand += string.Format("M{0}={1} ", _IoList[nCnt].Address, nBit);
			}

			nRtn = SendCommand(strCommand, ref strRet);
			if (nRtn > 0)
			{
				for (int nCnt = 0; nCnt < _IoList.Count; nCnt++)
				{
					m_bOpIOSta[_IoList[nCnt].Address - (int)m_nOutputStartAddr] = bVal;
				}
			}
			return nRtn;
		}

		#region Motor 설정
        public int SetMotorAllSpeed_Default()
        {
            string strCommand = "", strRet = "";
            strCommand = string.Format("%100");

            int nRtn = -1;

            nRtn = SendCommand(strCommand, ref strRet);

            return nRtn;
        }
		// Servo On/Off
		public int ServoOnOff(AxisInfo _AxisInfo, bool bOn)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;

			if (bOn)
			{
				switch (_AxisInfo.AxisType)
				{
					case AXIS_TYPE.SERVO:
					case AXIS_TYPE.STEPPING:
						strCommand = string.Format("#{0}J/", _AxisInfo.AxisNo_1Base);
						break;
					case AXIS_TYPE.LINEAR:
						strCommand = string.Format("#{0}d$", _AxisInfo.AxisNo_1Base);
						break;
				}
			}
			else
			{
				strCommand = string.Format("#{0}k", _AxisInfo.AxisNo_1Base);
			}

			nRtn = SendCommand(strCommand, ref strRet);

			return nRtn;
		}

		//	입력된 mm값을 cts로 바꿔주는 함수 mm->cts
		double GetCTSFromMM(int nAxis_1Base, double dPos, double dScale) 
		{
			return (double)(dPos * dScale);
		}

		//	speed는 mm/s 단위
		public int SetMoveSpeed(int nAxis, double dSpeed, double dScale)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;

			strCommand = GetMoveSpeed_Cmd(nAxis, dSpeed, dScale);

			nRtn = SendCommand(strCommand, ref strRet);
		
			return nRtn;
		}
		public string GetMoveSpeed_Cmd(int nAxis, double dSpeed, double dScale)
		{
			string strCommand = "";

			double dConvertSpd;

			//	Umac 1개로 전축을 제어함으로 아래와 같이 Umac을 구분지을 필요가 없다.
			dConvertSpd = Math.Abs(GetCTSFromMM(nAxis, dSpeed, dScale));

			dConvertSpd /= 1000.0;	//실제 입력은 cts/msec이므로 /1000을 한다.
			strCommand = string.Format("I{0}22={1:0.000}", nAxis, dConvertSpd);

			return strCommand;
		}

		// mm/s단위로 입력받아 scale에 따라 cts/msec로 설정한다.
		public int SetMoveAccel(int nAxis, double dAccel)	//>	가속도는 mm/s 단위
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;
			if(dAccel >= 0)
				return -1;

			strCommand = GetMoveAccel_Cmd(nAxis, dAccel);
			nRtn = SendCommand(strCommand, ref strRet);

			return nRtn;
		}

		public string GetMoveAccel_Cmd(int nAxis, double dAccel)	//>	가속도는 mm/s 단위
		{
			string strCommand = "";
			if(dAccel >= 0)
				return "";

			strCommand = string.Format("I{0}20={1} I{2}21={3}", nAxis, (int)dAccel, nAxis, (float)(dAccel/4.0));
			return strCommand;
		}

		// mm 단위로 입력 받아 절대 위치로 움직인다.
		public int MotorMove_Abs(int nAxis, double dPos, double dVel, double dAcc, double dScale)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;
			double  dPosition;

			string strVel = GetMoveSpeed_Cmd(nAxis, dVel, dScale);	// 속도 설정
			string strAcc = GetMoveAccel_Cmd(nAxis, dAcc);	// 가속도 설정
			dPosition = GetCTSFromMM(nAxis, dPos, dScale);

			strCommand = string.Format("{0} {1} #{2}J={3}", strVel, strAcc, nAxis, (float)dPosition);
			nRtn = SendCommand(strCommand, ref strRet);

			return nRtn;
		}

		// mm 단위로 입력 받아 상대 위치로 움직인다.
		public int MotorMove_Rel(int nAxis, double dPos, double dVel, double dAcc, double dScale)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;
			double dPosition;

			string strVel = GetMoveSpeed_Cmd(nAxis, dVel, dScale);	// 속도 설정
			string strAcc = GetMoveAccel_Cmd(nAxis, dAcc);	// 가속도 설정
			dPosition = GetCTSFromMM(nAxis, dPos, dScale);

			strCommand = string.Format("{0} {1} #{2}J:{3}", strVel, strAcc, nAxis, (float)dPosition);
			nRtn = SendCommand(strCommand, ref strRet);

			return nRtn;
		}

		public int StartHome(int nAxis_PlcNo)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;

			strCommand = string.Format("enaplc {0}", nAxis_PlcNo);
			nRtn = SendCommand(strCommand, ref strRet);

			return nRtn;
		}

		public int AbortHome(int nAxis_PlcNo) 
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;

			strCommand = string.Format("displc {0}", nAxis_PlcNo);
			nRtn = SendCommand(strCommand, ref strRet);

			return nRtn;
		}

		public int IsHomeDone(int nAxis_PlcNo)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;

			//Pxx0(홈밍중) Pxx1(홈밍끝)을 의미한다.
			//strCmd.Format("P{0}0", nHomeCmdId);
			strCommand = string.Format("P{0}1", nAxis_PlcNo);
			SendCommand(strCommand, ref strRet);
			nRtn = int.Parse(strRet);
			return nRtn; 
		}
		public int IsHomeDone(AxisInfo _Axisinfo)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;
			if (_Axisinfo.HomeEndCmd == "")
				return nRtn;

			//Pxx0(홈밍중) Pxx1(홈밍끝)을 의미한다.
			//strCmd.Format("P{0}0", nHomeCmdId);
			strCommand = string.Format("{0}", _Axisinfo.HomeEndCmd);
			SendCommand(strCommand, ref strRet);
			nRtn = int.Parse(strRet);

			_Axisinfo.CurInfo.HomeEnd = nRtn;
			return nRtn;
		}
        public int IsHomming(AxisInfo _Axisinfo)
        {
            string strCommand = "", strRet = "";
            int nRtn = -1;
            if (_Axisinfo.HommingCmd == "")
                return nRtn;

            strCommand = string.Format("{0}", _Axisinfo.HommingCmd);
            SendCommand(strCommand, ref strRet);
            nRtn = int.Parse(strRet);

            _Axisinfo.CurInfo.Homming = nRtn;
            return nRtn;
        }
		public int JogStart(int nAxis, bool bDirection)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;

			if (bDirection)
				strCommand = string.Format("#{0}J+", nAxis);
			else
				strCommand = string.Format("#{0}J-", nAxis);

			nRtn = SendCommand(strCommand, ref strRet);

			return nRtn;
		}
        public int JogStart(AxisInfo _Axis,int nSpeed, bool bDirection)
        {
            string strCommand = "", strRet = "";
            int nRtn = -1;
            string strVel = GetMoveSpeed_Cmd(_Axis.AxisNo_1Base, nSpeed, _Axis.Scale);	// 속도 설정
            string strAcc = GetMoveAccel_Cmd(_Axis.AxisNo_1Base, 200);	// 가속도 설정

            if (bDirection)
                strCommand = string.Format("{0} {1} #{2}J+", strVel, strAcc, _Axis.AxisNo_1Base);
            else
                strCommand = string.Format("{0} {1} #{2}J-", strVel, strAcc, _Axis.AxisNo_1Base);

            nRtn = SendCommand(strCommand, ref strRet);

            return nRtn;
        }
		public int JogStop(int nAxis)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;

			strCommand = string.Format("#{0}J/", nAxis);
			nRtn = SendCommand(strCommand, ref strRet);

			return nRtn;
		}
		public int JogStop(AxisInfo _Axis)
		{
			string strCommand = "", strRet = "";
			int nRtn = -1;

			strCommand = string.Format("#{0}J/", _Axis.AxisNo_1Base);
			nRtn = SendCommand(strCommand, ref strRet);

			return nRtn;
		}
		#endregion
	}

	[Serializable]
	public class AxisInfo
	{
		public AxisInfo()
		{
		}
		public AxisInfo(int nAxisNo_1Base)
		{
			_AxisNo = nAxisNo_1Base;
			_UseAxis = false;
			_AxisType = AXIS_TYPE.SERVO;
			_Name = "";
			_HommingCmd = "";
			_HomeEndCmd = "";
			_PlcNo = 0;
			_dScale = 0;
		}

		private int _AxisNo;
		private bool _UseAxis;
		private AXIS_TYPE _AxisType;
		private string _Name;
		private string _HommingCmd;
		private string _HomeEndCmd;
		private int _PlcNo;
		private double _dScale;
		private List<PT_Data> _PtData = new List<PT_Data>();
		private AxisCurInfo _CurInfo = new AxisCurInfo();

		public int AxisNo_1Base
		{
			get { return _AxisNo; }
			set { _AxisNo = value; }
		}
		public bool UseAxis
		{
			get { return _UseAxis; }
			set { _UseAxis = value; }
		}
		public AXIS_TYPE AxisType
		{
			get { return _AxisType; }
			set { _AxisType = value; }
		}
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		public string HommingCmd
		{
			get { return _HommingCmd; }
			set { _HommingCmd = value; }
		}
		public string HomeEndCmd
		{
			get { return _HomeEndCmd; }
			set { _HomeEndCmd = value; }
		}
		public int PlcNo
		{
			get { return _PlcNo; }
			set { _PlcNo = value; }
		}
		public double Scale
		{
			get { return _dScale; }
			set { _dScale = value; }
		}
		public List<PT_Data> PtList
		{
			get { return _PtData; }
			set { _PtData = value; }
		}

		[XmlIgnore]
		public AxisCurInfo CurInfo
		{
			get { return _CurInfo; }
			set { _CurInfo = value; }
		}
	}

	[Serializable]
	public class PT_Data
	{
		public PT_Data()
		{
			_PT_Name = "";
			_dPositon = 0;
			_dSpeed = 0;
			_dAccel = 0;
		}
		private string _PT_Name;
		private double _dPositon;
		private double _dSpeed;
		private double _dAccel;

		public string Name
		{
			get { return _PT_Name; }
			set { _PT_Name = value; }
		}
		public double Position
		{
			get { return _dPositon; }
			set { _dPositon = value; }
		}
		public double Speed
		{
			get { return _dSpeed; }
			set { _dSpeed = value; }
		}
		public double Accel
		{
			get { return _dAccel; }
			set { _dAccel = value; }
		}
	}

	[Serializable]
	public class AxisCurInfo
	{
		public AxisCurInfo()
		{
			_nServoOnOff = 0;
			_fSpeed = 0;
			_fPosition = 0;
			_nHomming = 0;
			_nHomeEnd = 0;
			_nPlimit = 0;
			_nMlimit = 0;
			_nAmpFault = 0;
			_nFollowingErr = 0;
			_nInPositionBit = 0;
			_fTorque = 0;
		}
		private int _nServoOnOff;
		private double _fSpeed;
		private double _fPosition;
		private double _fTargetPos;
		private int _nHomming;
		private int _nHomeEnd;
		private int _nPlimit;
		private int _nMlimit;
		private int _nAmpFault;
		private int _nFollowingErr;
		private int _nInPositionBit;
		private float _fTorque;	//부하율

		public int ServoOnOff
		{
			get { return _nServoOnOff; }
			set { _nServoOnOff = value; }
		}
		public double Speed
		{
			get { return _fSpeed; }
			set { _fSpeed = value; }
		}
		public double Position
		{
			get { return _fPosition; }
			set { _fPosition = value; }
		}
		public double TargetPos
		{
			get { return _fTargetPos; }
			set { _fTargetPos = value; }
		}
		public int Homming
		{
			get { return _nHomming; }
			set { _nHomming = value; }
		}
		public int HomeEnd
		{
			get { return _nHomeEnd; }
			set { _nHomeEnd = value; }
		}
		public int Plimit
		{
			get { return _nPlimit; }
			set { _nPlimit = value; }
		}
		public int Mlimit
		{
			get { return _nMlimit; }
			set { _nMlimit = value; }
		}
		public int AmpFault
		{
			get { return _nAmpFault; }
			set { _nAmpFault = value; }
		}
		public int FollowingErr
		{
			get { return _nFollowingErr; }
			set { _nFollowingErr = value; }
		}
		public int InPositionBit
		{
			get { return _nInPositionBit; }
			set { _nInPositionBit = value; }
		}
		public float Torque
		{
			get { return _fTorque; }
			set { _fTorque = value; }
		}
	}

	public class IO_List
	{
		public IO_List()
		{
			SetInputList();
			SetOutputList();
		}

		private List<DeviceNetIO> _InputList = new List<DeviceNetIO>();
		public List<DeviceNetIO> Input
		{
			get { return _InputList; }
			set { _InputList = value; }
		}

		private List<DeviceNetIO> _OutputList = new List<DeviceNetIO>();
		public List<DeviceNetIO> Output
		{
			get { return _OutputList; }
			set { _OutputList = value; }
		}

		private void SetInputList()
		{

		}
		private void SetOutputList()
		{

		}
	}
}
