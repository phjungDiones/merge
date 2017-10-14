using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CJ_Controls.DeviceNet
{
	/// <summary>
	/// Date : 2005.12.03 
	/// To Control Hilscher Mast card
	/// </summary>
	/// 

	public class XHilscher
	{
		private COM_DeviceNet m_DeviceNet=null;
		// Board Open Flag
		private bool bOpen = false;

		// Max In/Out Address
		private ushort usMaxInputAddress = 0;
		private ushort usMaxOutputAddress = 0;

		// In/Out Buffer
		public ushort[] usInputBuffer;
		public ushort[] usOutputBuffer;

		public XHilscher(COM_DeviceNet _Parent)
		{
			m_DeviceNet = _Parent;
		}
		private void LogTextOut(string msg)
		{
			m_DeviceNet.LogTextOut(msg);
		}
		public bool IsOpen()
		{
			return bOpen;
		}

		public int Open(ushort usMaxInputAddress, ushort usMaxOutputAddress)
		{
			if (IsOpen())
			{
				LogTextOut("DeviceNet, Open(), Already, Board is Opened");

				return -1000;
			}

			int nRet = -2000;
			try
			{
				
				nRet = XWin32Hilscher.DevOpenDriver(0);
				if (nRet != XWin32Hilscher.DRV_NO_ERROR)
				{
					LogTextOut(String.Format("DeviceNet, DevOpenDriver() Error({0})", nRet));
					return nRet;
				}

				XWin32Hilscher.BOARD_INFO tBoardInfo = new XWin32Hilscher.BOARD_INFO();
				nRet = XWin32Hilscher.DevGetBoardInfo(0, (ushort)Marshal.SizeOf(tBoardInfo), out tBoardInfo);
				if (nRet != XWin32Hilscher.DRV_NO_ERROR)
				{

					LogTextOut(String.Format("DeviceNet, DevGetBoardInfo() Error({0})", nRet));
					return nRet;
				}

				XWin32Hilscher.BOARD[] tBoardList = new XWin32Hilscher.BOARD[XWin32Hilscher.MAX_DEV_BOARDS] { tBoardInfo.tBoard0, tBoardInfo.tBoard1, tBoardInfo.tBoard2, tBoardInfo.tBoard3 };
				for (int i = 0; i < XWin32Hilscher.MAX_DEV_BOARDS; i++)
				{
					if (tBoardList[i].usAvailable != 0)
					{
						nRet = XWin32Hilscher.DevInitBoard(tBoardList[i].usBoardNumber, IntPtr.Zero);
						if (nRet != XWin32Hilscher.DRV_NO_ERROR)
						{

							LogTextOut(String.Format("DeviceNet, DevInitBoard() Error(){0}", nRet));
							return nRet;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogTextOut(String.Format("DeviceNet, DevInitBoard() Error(){0}", ex.Message));
				return nRet;
			}
#region ÁÖ¼®
            nRet = XWin32Hilscher.DevReset(0, XWin32Hilscher.COLDSTART, 8000);
            if (nRet != XWin32Hilscher.DRV_NO_ERROR)
            {
                MessageBox.Show("DeviceNet, Open(), DevReset() Error");
                return nRet;
            }

            nRet = XWin32Hilscher.DevSetHostState(0, XWin32Hilscher.HOST_READY, 0);
            if (nRet != XWin32Hilscher.DRV_NO_ERROR)
            {
                MessageBox.Show("DeviceNet, Open(), DevSetHostState() Error");
                return nRet;
            }
#endregion
			this.usMaxInputAddress = usMaxInputAddress;
			usInputBuffer = new ushort[usMaxInputAddress / 2 + 1];

			this.usMaxOutputAddress = usMaxOutputAddress;
			usOutputBuffer = new ushort[usMaxOutputAddress / 2 + 1];

			bOpen = true;

			return 1;
		}

		public int Close()
		{
			int nRet = XWin32Hilscher.DevSetHostState(0, XWin32Hilscher.HOST_NOT_READY, 0);
			if (nRet != XWin32Hilscher.DRV_NO_ERROR)
			{
				LogTextOut(String.Format("DeviceNet, DevSetHostState() Error({0})", nRet));
				return nRet;
			}

			nRet = XWin32Hilscher.DevExitBoard(0);
			if (nRet != XWin32Hilscher.DRV_NO_ERROR)
			{
				LogTextOut(String.Format("DeviceNet, DevExitBoard() Error({0})", nRet));
				return nRet;
			}

			nRet = XWin32Hilscher.DevCloseDriver(0);
			if (nRet != XWin32Hilscher.DRV_NO_ERROR)
			{
				LogTextOut(String.Format("DeviceNet, DevCloseDriver() Error({0})", nRet));
				return nRet;
			}

			// Set Open Flag
			bOpen = false;

			return 1;
		}

		public ushort GetReadOut(ushort usAddress)
		{
			ushort[] usReadOutBuff = new ushort[1];
			int nRet = ReadOutput(usAddress, usReadOutBuff, 0);
			return usReadOutBuff[0];
		}

		public int WriteBit(ushort usAddress, ushort usWriteData, bool bOn)
		{
			if (!IsOpen())
			{
				LogTextOut("DeviceNet, WriteBit(), DeviceNet is not opened");
				return -1000;
			}

			ushort[] usReadOutBuff = new ushort[1];
			int nRet = ReadOutput(usAddress, usReadOutBuff, 0);
			if (nRet < 0)
			{
				return nRet;
			}

			if (bOn)
			{
				usReadOutBuff[0] = (ushort)(usReadOutBuff[0] | usWriteData);
			}
			else
			{
				usReadOutBuff[0] = (ushort)(usReadOutBuff[0] & ~usWriteData);
			}

			return WriteOutput(usAddress, usReadOutBuff, 0);
		}

		public bool ReadBit(ushort usAddress, ushort usReadBit)
		{
			if (usInputBuffer == null)
				return false;

			if (usAddress / 2 > usInputBuffer.Length)
			{
				LogTextOut("XHilscher, In ReadBit(), Invalid Address = " + usAddress);
				return false;
			}
			if ((usInputBuffer[usAddress / 2] & usReadBit) > 0)
			{
				return true;
			}
			return false;
		}

		public int WriteOutput(ushort usAddress, ushort[] usWriteData, uint unTimeout)
		{
			if (!IsOpen())
			{
				LogTextOut("DeviceNet, WriteOutput(), DeviceNet is not opened");
				return -1000;
			}

			int nRet = -2000;
			try
			{
				XWin32Hilscher.COMSTATE comState = new XWin32Hilscher.COMSTATE();
				nRet = XWin32Hilscher.DevExchangeIOErr(0, usAddress, (ushort)(usWriteData.Length * 2), usWriteData, 0, 0, null, out comState, unTimeout);
				if (nRet != XWin32Hilscher.DRV_NO_ERROR)
				{
					LogTextOut(String.Format("DeviceNet, WriteOutput(), DevExchangeIOErr Error {0}", nRet));
					return nRet;
				}

				switch (comState.usMode)
				{
					case XWin32Hilscher.STATE_MODE_3: // Cyclic transfer of the state field including the state error flag (usStateFlag)
						if (comState.usStateFlag != 0)
						{
							LogTextOut("DeviceNet, DevExchangeIOErr(), COMSTATE.usStateFlag Error");
							return -3000;
						}
						break;
					case XWin32Hilscher.STATE_MODE_4:
						if (comState.usStateFlag != 0) // Event driven transfer of the state field including the usStateFlag
						{
							LogTextOut("DeviceNet, DevExchangeIOErr(), COMSTATE.usStateFlag Error");
							return -4000;
						}
						break;
					default:
						LogTextOut("DeviceNet, DevExchangeIOErr(), Invalid COMSTATE.usMode Error");
						return -5000;
				}
				nRet = 1;
			}
			catch (Exception e)
			{
				LogTextOut(e.ToString());
			}

			return nRet;
		}

		public int ReadAllOutput()
		{
			int nRet = ReadOutput(0, usOutputBuffer, 0);
			if (nRet < 0)
			{
				LogTextOut(String.Format("DeviceNet, ReadAllOutput() Error({0})", nRet));
				return nRet;
			}

			return 1;
		}

		public int ReadOutput(ushort usAddress, ushort[] usReadSentData, uint unTimeout)
		{
			if (!IsOpen())
			{
				LogTextOut("DeviceNet, ReadOutput(), DeviceNet is not opened");
				return -1000;
			}

			int nRet = -2000;

			try
			{
				nRet = XWin32Hilscher.DevReadSendData(0, usAddress, (ushort)(usReadSentData.Length * 2), usReadSentData);
				if (nRet != XWin32Hilscher.DRV_NO_ERROR)
				{
					LogTextOut(String.Format("DeviceNet, DevReadSendData() Error({0})", nRet));
					return nRet;
				}
				nRet = 1;
			}
			catch (Exception e)
			{
				LogTextOut(e.ToString());
			}

			return nRet;
		}

		public bool ReadOutBit(ushort usAddress, ushort usReadData)
		{
			bool bRet = false;
			ushort[] usReadSentData = new ushort[1];

			if (ReadOutput(usAddress, usReadSentData, 0) > 0)
			{
				if ((usReadSentData[0] & usReadData) > 0)
				{
					bRet = true;
				}
			}
			else
			{
				LogTextOut("XHilscher, ReadOutBit() Error");
			}
			return bRet;
		}

		public int ReadAllInput()
		{
			int nRet = ReadInput(0, usInputBuffer, 0);
			if (nRet < 0)
			{
				LogTextOut(String.Format("DeviceNet, ReadAllInput() Error({0})", nRet));
				return nRet;
			}

			return 1;
		}
		public ushort ReadInput_WORD_CJ(ushort usAddress)
		{
			ushort sRtn = usInputBuffer[(usAddress / 2)];
			return sRtn;
		}
		public int ReadInput(ushort usAddress, ushort[] usReadData, uint unTimeout)
		{
			if (!IsOpen())
			{
				LogTextOut("DeviceNet, ReadInput(), DeviceNet is not opened");
				return -1000;
			}

			int nRet = -2000;

			try
			{
				XWin32Hilscher.COMSTATE comState = new XWin32Hilscher.COMSTATE();
				nRet = XWin32Hilscher.DevExchangeIOErr(0, 0, 0, null, usAddress, (ushort)(usReadData.Length * 2), usReadData, out comState, unTimeout);
				if (nRet != XWin32Hilscher.DRV_NO_ERROR)
				{
					LogTextOut(String.Format("DeviceNet, ReadInput(), DevExchangeIOErr Error {0}", nRet));
					return nRet;
				}

				switch (comState.usMode)
				{
					case XWin32Hilscher.STATE_MODE_3: // Cyclic transfer of the state field including the state error flag (usStateFlag)
						if (comState.usStateFlag != 0)
						{
							LogTextOut("DeviceNet, ReadInput(), COMSTATE.usStateFlag Error");
							return -4000;
						}
						break;
					case XWin32Hilscher.STATE_MODE_4:
						if (comState.usStateFlag != 0) // Event driven transfer of the state field including the usStateFlag
						{
							LogTextOut("DeviceNet, ReadInput(), COMSTATE.usStateFlag Error");
							return -5000;
						}
						break;
					default:
						LogTextOut("DeviceNet, ReadInput(), Invalid COMSTATE.usMode Error");
						return -6000;
				}
				nRet = 1;
			}
			catch (Exception e)
			{
				LogTextOut(e.ToString());
			}
			return nRet;
		}
	}
}
