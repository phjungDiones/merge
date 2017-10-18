using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using CJ_Controls;

namespace CJ_Controls.Communication
{
	/// <summary>
	/// MelsecNet 통신을 위한 클래스이다.
	/// </summary>
	public class COM_Melsec : Component
	{
		#region Log 보내기
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public event MessageEventHandler MessageEvent;

		private void LogTextOut(string message)
		{
			if (MessageEvent != null)
				MessageEvent(this, new MessageEventArgs(message));
		}
		#endregion

		private short _DeviceType_Bit = 23;
		[Category("MelsecNet 설정"), Description("Bit 영역 주소"), DefaultValue(false)]
		public short DEVICETYPE_BIT
		{
			get { return _DeviceType_Bit; }
			set { _DeviceType_Bit = value; }
		}
		private short _DeviceType_Word = 24;
		[Category("MelsecNet 설정"), Description("Word 영역 주소"), DefaultValue(false)]  
		public short DEVICETYPE_WORD
		{
			get { return _DeviceType_Word; }
			set { _DeviceType_Word = value; }
		}
		private short _Device_Channel = 151;
		[Category("MelsecNet 설정"), Description("채널 설정 (보드 속성에 있는 값)"), DefaultValue(false)]
		public short DEVICE_CH
		{
			get { return _Device_Channel; }
			set { _Device_Channel = value; }
		}

		private int nPath;
		private bool bOpened = false;
		[Category("MelsecNet 값"), Description("Melsec Net의 현재 상태"), DefaultValue(false)]
		public bool IsOpen
		{
			get { return bOpened; }
		}

		public COM_Melsec()
		{
		}

		public int Open()
		{
			if (bOpened)
			{
				LogTextOut("Melsec Board is Already Opened!");
				return -1;
			}

			try
			{
				short sRet = mdOpen(_Device_Channel, -1, out nPath);
				if (sRet != 0)
				{
					string strLog = String.Format("mdOpen() Error({0})", sRet);
					LogTextOut(strLog);
					return -2;
				}
				bOpened = true;
			}
			catch (Exception ex)
			{
				LogTextOut(ex.Message);
			}
			return 1;
		}

		public int GetBit(short sAddress)
		{
			if (!bOpened)
			{
				LogTextOut("Melsec Board is not Opened!");
				return -1;
			}

			short[] sDev = new short[4];
			short[] sRead = new short[1];

			sDev[0] = 1;
			sDev[1] = _DeviceType_Bit;
			sDev[2] = sAddress;
			sDev[3] = 16;

			short sRet = mdRandR(nPath, 0xFF, sDev, sRead, 2);
			if (sRet != 0)
			{
				string strLog = String.Format("mdRandR() Error({0})", sRet);
				LogTextOut(strLog);
				return -2;
			}

			if ((sRead[0] & 0x0001) > 0)
			{
				return 1;
			}

			return 0;
		}

		public int GetWord(short sAddress, short[] sRead, short sSize)
		{
			if (!bOpened)
			{
				LogTextOut("Melsec Board is not Opened!");
				return -1;
			}

			sSize *= 2;
			short sRet = mdReceive(nPath, 255, _DeviceType_Word, sAddress, ref sSize, sRead);
			if (sRet != 0)
			{
				string strLog = String.Format("mdReceive() Error({0})", sRet);
				LogTextOut(strLog); 
				return -2;
			}
			sSize /= 2;
			LogTextOut("GetWord : " + Convert.ToString(sAddress, 16) + " = " + sRead[0].ToString());
			return 1;
		}
		public int GetWord(int nAddress, ref short sRead)
		{
			short[] data = new short[1];
			int nRet = GetWordEx(nAddress, data, 1);
			sRead = data[0];
			return nRet;
		}
		public int GetWord(short sAddress, ref short sRead)
		{
			short[] data = new short[1];
			int nRet = GetWord(sAddress, data, 1);
			sRead = data[0];
			return nRet;
		}
		public int GetWordEx(int nAddress, short[] sRead, int nSize)
		{
			if (!bOpened)
			{
				LogTextOut("Melsec Board is not Opened!");
				return -1;
			}

			nSize *= 2;
			int nRet = mdReceiveEx(nPath, 0, 255, (int)_DeviceType_Word, nAddress, ref nSize, sRead);
			if (nRet != 0)
			{
				string strLog = String.Format("mdReceive() Error({0})", nRet);
				LogTextOut(strLog);
				return -2;
			}
			nSize /= 2;
			return 1;
		}
		

		public int SetWord(short sAddress, short sWrite)
		{
			short[] value = new short[] { sWrite };
			return SetWord(sAddress, value, 1);

		}
		public int SetWord(int sAddress, short sWrite)
		{
			short[] value = new short[] { sWrite };
			return SetWordEx(sAddress, value, 1);

		}
		public int SetWord(short sAddress, short[] sWrite, short sSize)
		{
			if (!bOpened)
			{
				LogTextOut("Melsec Board is not Opened!");
				return -1;
			}

			sSize *= 2;
			short sRet = mdSend(nPath, 255, _DeviceType_Word, sAddress, ref sSize, sWrite);
			if (sRet != 0)
			{
				string strLog = String.Format("mdSend() Error({0})", sRet);
				LogTextOut(strLog);
				return -2;
			}
			LogTextOut("SetWord : " + Convert.ToString(sAddress, 16) + " = " + sWrite[0].ToString());
			sSize /= 2;
			return 1;
		}

		public int SetWordEx(int nAddress, short[] sWrite, int nSize)
		{
			if (!bOpened)
			{
				LogTextOut("Melsec Board is not Opened!");
				return -1;
			}

			nSize *= 2;
			int nRet = mdSendEx(nPath, 0, 255, (int)_DeviceType_Word, nAddress, ref nSize, sWrite);
			if (nRet != 0)
			{
				string strLog = String.Format("mdSendEx() Error({0})", nRet);
				LogTextOut(strLog);
				return -2;
			}
			nSize /= 2;
			LogTextOut("Word : " + Convert.ToString(nAddress, 16) + " = " + sWrite.ToString());
			return 1;
		}

		public int SetBit(short sAddress)
		{
			if (!bOpened)
			{
				LogTextOut("Melsec Board is not Opened!");
				return -1;
			}

			short sRet = mdDevSet(nPath, 255, _DeviceType_Bit, sAddress);
			if (sRet != 0)
			{
				string strLog = String.Format("mdDevSet() Error({0})", sRet);
				LogTextOut(strLog);
				return -2;
			}
			LogTextOut("Bit : " + Convert.ToString(sAddress, 16) + " = On");
			return 1;
		}

		public int ResetBit(short sAddress)
		{
			if (!bOpened)
			{
				LogTextOut("Melsec Board is not Opened!");
				return -1;
			}

			short sRet = mdDevRst(nPath, 255, _DeviceType_Bit, sAddress);
			if (sRet != 0)
			{
				string strLog = String.Format("mdDevRst() Error({0})", sRet);
				LogTextOut(strLog);
				return -2;
			}
			LogTextOut("Bit : " + Convert.ToString(sAddress, 16) + " = Off");
			return 1;
		}

		public int SetBitByWord(short sAddress, short[] sWrite, short sSize)
		{
			if (!bOpened)
			{
				LogTextOut("Melsec Board is not Opened!");
				return -1;
			}

			sSize *= 2;
			short sRet = mdSend(nPath, 255, _DeviceType_Bit, sAddress, ref sSize, sWrite);
			if (sRet != 0)
			{
				string strLog = String.Format("mdSend() Error({0})", sRet);
				LogTextOut(strLog);
				return -2;
			}

			sSize /= 2;
			return 1;
		}

		public int GetBitByWord(short sAddress, short[] sRead, short sSize)
		{
			if (!bOpened)
			{
				LogTextOut("Melsec Board is not Opened!");
				return -1;
			}

			short[] sDev = new short[4];

			sDev[0] = 1;
			sDev[1] = _DeviceType_Bit;
			sDev[2] = sAddress;
			sDev[3] = (short)(16 * sSize);

			short sRet = mdRandR(nPath, 0xFF, sDev, sRead, (short)(sSize * 2));
			if (sRet != 0)
			{
				string strLog = String.Format("mdRandR() Error({0})", sRet);
				LogTextOut(strLog);
				return -2;
			}

			return 1;
		}

		public int GetString(short sAddress, short sSize, ref string strRead)
		{
			short[] sRead = new short[sSize];

			if (GetWord(sAddress, sRead, sSize) != 1)
			{
				return -1;
			}

			for (int i = 0; i < sSize; i++)
			{
				strRead += (char)(sRead[i] & 0x00FF);
				strRead += (char)((sRead[i] & 0xFF00) >> 8);
			}
			strRead = strRead.TrimEnd(' ');

			return 1;
		}

		public int SetString(short sAddress, short sSize, string strWrite)
		{
			char cVal;
			short sVal;
			short[] sWrite = new short[sSize];

			if (sSize * 2 < strWrite.Length)
			{
				return -1;
			}

			int nOrder = 0;
			for (int i = 0; i < strWrite.Length; i++)
			{
				sVal = 0;
				cVal = strWrite[i];

				if ((i % 2) == 0)
				{
					sVal = (short)cVal;
					sWrite[nOrder] = sVal;
				}
				else
				{
					sVal = (short)cVal;
					sVal = (short)(sVal << 8);
					sWrite[nOrder] = (short)((ushort)sWrite[nOrder] | (ushort)sVal);
					nOrder++;
				}
			}

			if ((strWrite.Length % 2) != 0)
			{
				sVal = 0x20;
				sVal = (short)(sVal << 8);
				sWrite[nOrder] = (short)((ushort)sWrite[nOrder] | (ushort)sVal);
				nOrder++;
			}

			if (SetWord(sAddress, sWrite, sSize) != 1)
			{
				return -2;
			}
			return 1;
		}

		public int GetStringFromUTF8(int nAddress, short sSize, ref string strRead)
		{ //Cjinnnn : 유니코드 읽어오기.
			short[] sRead = new short[sSize];

			if (GetWordEx(nAddress, sRead, sSize) != 1)
			{
				return -1;
			}

			byte[] byteData = new byte[sSize * 2];

			for (int i = 0; i < sSize; i++)
			{
				byteData[i * 2] = Convert.ToByte(sRead[i] & 0x00FF);
				byteData[i * 2 + 1] = Convert.ToByte((sRead[i] & 0xFF00) >> 8);
			}
			strRead = System.Text.Encoding.UTF8.GetString(byteData);
			strRead = strRead.TrimEnd('\0');
			strRead = strRead.TrimEnd(' ');

			return 1;
		}
		public int SetStringToUTF8(int nAddress, short sSize, string strWrite)
		{ //Cjinnnn : 유니코드 저장하기.
			byte[] defaultBytes = System.Text.Encoding.Default.GetBytes(strWrite);
			byte[] utf8byte = System.Text.Encoding.Convert(System.Text.Encoding.Default, System.Text.Encoding.UTF8, defaultBytes);
			//string resultString = System.Text.Encoding.UTF8.GetString(utf8byte);

			short[] sWrite = new short[sSize];
			for (int i = 0; i < sSize; i++)
			{
				sWrite[i] += (short)(utf8byte[i * 2 + 1] << 8);
				sWrite[i] += (short)(utf8byte[i * 2]);
			}

			if (SetWordEx(nAddress, sWrite, sSize) != 1)
			{
				return -2;
			}
			return 1;
		}
		public int Close()
		{
			if (!bOpened)
			{
				LogTextOut("Melsec Board is not Opened!");
				return -1;
			}

			short sRet = mdClose(nPath);
			if (sRet != 0)
			{
				string strLog = String.Format("mdClose() Error({0})", sRet);
				LogTextOut(strLog);
				return -2;
			}

			bOpened = false;
			return 1;
		}

		[DllImport("MdFunc32.dll")]
		internal static extern short mdOpen(short chan, short mode, out int path);

		[DllImport("MdFunc32.dll")]
		internal static extern short mdClose(int path);

		[DllImport("MdFunc32.dll")]
		internal static extern short mdRandR(int Path, short stdno, short[] dev, short[] sRead, short sSize);

		[DllImport("MdFunc32.dll")]
		internal static extern short mdDevSet(int Path, short stdno, short sDevType, short sAddress);

		[DllImport("MdFunc32.dll")]
		internal static extern short mdDevRst(int Path, short stdno, short sDevType, short sAddress);

		[DllImport("MdFunc32.dll")]
		internal static extern short mdSend(int Path, short stdno, short sDevType, short sAddress, ref short sSize, short[] sWrite);

		[DllImport("MdFunc32.dll")]
		internal static extern int mdSendEx(int path, int netno, int stdno, int devType, int nAddress, ref int nSize, short[] sWrite);

		[DllImport("MdFunc32.dll")]
		internal static extern short mdReceive(int Path, short stdno, short sDevType, short sAddress, ref short sSize, short[] sRead);

		[DllImport("MdFunc32.dll")]
		internal static extern int mdReceiveEx(int path, int netno, int stdno, int devType, int nAddress, ref int nSize, short[] sRead);
	}
}
