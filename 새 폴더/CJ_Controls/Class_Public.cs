using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Windows.Forms;
using CJ_Controls.WinControls.Virtual_Key;

namespace CJ_Controls
{
	//공용 함수
	public class Class_Public
	{
		#region 싱글톤 멤버
		private static Class_Public _Instance = null;
		private static object _LockObj = new object();

		public static Class_Public Instance
		{
			get
			{
				if (_Instance == null)
				{
					lock (_LockObj)
					{
						if (_Instance == null)
						{
							_Instance = new Class_Public();
						}
					}
				}
				return _Instance;
			}
		}
		#endregion

		public Class_Public()
		{

		}

		public void CreateDir(string strPath)
		{
			if (!Directory.Exists(strPath) && strPath != string.Empty)
			{
				Directory.CreateDirectory(strPath);
			}
		}

		object _MainFrame = null;
		public object MainFrame
		{
			get { return _MainFrame; }
			set { _MainFrame = value; }
		}

		public string ShowKeyPad(IWin32Window _Parent, string _FirstText, bool bPwdType = false)
		{
			string strRtn = _FirstText;
			Form_Virtual_KeyPad dlg = new Form_Virtual_KeyPad();
			dlg.SetPwdType(bPwdType);
			dlg.SetFirstText(_FirstText);
			if (dlg.ShowDialog(_Parent) == DialogResult.OK)
			{
				strRtn = dlg.GetRtnValue();
			}
			return strRtn;
		}
		public string ShowKeyBoard(IWin32Window _Parent, string _FirstText, bool bPwdType = false)
		{
			string strRtn = _FirstText;
			Form_Virtual_Keyboard dlg = new Form_Virtual_Keyboard();
			dlg.SetPwdType(bPwdType);
			dlg.SetFirstText(_FirstText);
			if (dlg.ShowDialog(_Parent) == DialogResult.OK)
			{
				strRtn = dlg.GetRtnValue();
			}
			return strRtn;
		}
	}

	#region INI 파일 관리
	public class IniFile
	{
		public string path;

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section,
			string key, string val, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section,
			string key, string def, StringBuilder retVal, int size, string filePath);

		public IniFile(string INIPath)
		{
			path = INIPath;
		}

		public void WriteValue(string Section, string Key, string Value)
		{
			WritePrivateProfileString(Section, Key, Value, this.path);
		}

		public string ReadValue(string Section, string Key, string Default)
		{
			StringBuilder buffer = new StringBuilder(255);
			GetPrivateProfileString(Section, Key, Default, buffer, 255, this.path);

			return buffer.ToString();
		}

		public void WriteValue(string Section, string Key, int Value)
		{
			WritePrivateProfileString(Section, Key, Value.ToString(), this.path);
		}

		public int ReadValue(string Section, string Key, int Default)
		{
			StringBuilder buffer = new StringBuilder(255);
			GetPrivateProfileString(Section, Key, Default.ToString(), buffer, 255, this.path);

			return int.Parse(buffer.ToString());
		}
	}
	#endregion

	#region LastInput Time Check
	public class LastInput_TimeOver
	{
		private struct LASTINPUTINFO
		{
			public uint cbSize;
			public uint dwTime;
		}

		[DllImport("User32.dll")]
		private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
		[DllImport("Kernel32.dll")]
		private static extern uint GetLastError();

		public static uint GetIdleTime()
		{
			LASTINPUTINFO lastInPut = new LASTINPUTINFO();
			lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
			GetLastInputInfo(ref lastInPut);

			return ((uint)Environment.TickCount - lastInPut.dwTime);
		}
		/* 예제
		private void Auto_LogOut()
		{
			MainData _Main = MainData.Instance;
			if (_Main.Login_Status != LOGIN_STATUS.NONE)
			{
				uint nLoginTime = LastInput_TimeOver.GetIdleTime();
				Label_Login.Text = string.Format("{0} ({1:000})", _Main.Login_Status, (nLoginTime / 1000));
				if (nLoginTime >= (_Main.AutoLogout_Time * 1000) )
				{
					LogOut_Proc();
				}
			}
		}*/
	}
	#endregion

	#region System Time Change
	public class SystemTime_Change
	{
		[StructLayout(LayoutKind.Sequential)]
		internal struct SYSTEMTIME
		{
			internal ushort wYear;
			internal ushort wMonth;
			internal ushort wDayOfWeek;
			internal ushort wDay;
			internal ushort wHour;
			internal ushort wMinute;
			internal ushort wSecond;
			internal ushort wMilliseconds;
		}

		[DllImport("kernel32.dll")]
		internal static extern Boolean SetLocalTime(ref SYSTEMTIME lpSystemTime);
		public static void SetTime(DateTime dtNewTime)
		{
			// Call the native GetSystemTime method
			// with the defined structure.
			SYSTEMTIME systime = new SYSTEMTIME();

			systime.wYear = (ushort)dtNewTime.Year;
			systime.wMonth = (ushort)dtNewTime.Month;
			systime.wDay = (ushort)dtNewTime.Day;
			systime.wHour = (ushort)dtNewTime.Hour;
			systime.wMinute = (ushort)dtNewTime.Minute;
			systime.wSecond = (ushort)dtNewTime.Second;
			systime.wMilliseconds = (ushort)dtNewTime.Millisecond;

			SetLocalTime(ref systime);
		}

		public static bool SetTime(short sYear, short sMonth, short sDay, short sHour, short sMinute, short sSecond)
		{
			// Call the native GetSystemTime method
			// with the defined structure.
			SYSTEMTIME systime = new SYSTEMTIME();

			systime.wYear = (ushort)sYear;
			systime.wMonth = (ushort)sMonth;
			systime.wDay = (ushort)sDay;
			systime.wHour = (ushort)sHour;
			systime.wMinute = (ushort)sMinute;
			systime.wSecond = (ushort)sSecond;
			systime.wMilliseconds = 0;

			bool bRet = false;
			bRet = SetLocalTime(ref systime);
			return bRet;
		}
	}
	#endregion

	#region Event Message Argument
	public class MessageEventArgs : EventArgs
	{
		private string _Text = "";
		public MessageEventArgs(string test)
		{
			this._Text = test;
		}

		public string Text
		{
			get { return this._Text; }
		}
	}
	#endregion

	#region DeviceNet IO 관련
	public enum DNET_IO_TYPE : int
	{
		D_INPUT = 0,
		D_OUTPUT,
		A_INPUT,
		A_OUTPUT,
	}

	[Serializable]
	public class DeviceNetIO
	{
		//공통
		public DNET_IO_TYPE IO_Type = DNET_IO_TYPE.D_INPUT;
		public ushort Address;
		public string IO_Name;
		public string Cable;
		public ushort SubAddr;
		public string Description;

		[XmlIgnore]
		public bool IsOn;
		[XmlIgnore]
		public short AnalogValue;

		public DeviceNetIO()
		{
			//공통
			IO_Type = DNET_IO_TYPE.D_INPUT;
			Address = 0;
			IO_Name = "";
			Cable = "";
			SubAddr = 0;
			Description = "";
		}
		public DeviceNetIO(DNET_IO_TYPE type)
		{
			//공통
			IO_Type = type;
			Address = 0;
			IO_Name = "";
			Cable = "";
			SubAddr = 0;
			Description = "";
		}
		public DeviceNetIO(string name, ushort addr, ushort subaddr, DNET_IO_TYPE type, string strCable, string strDesc="")
		{
			Address = addr;
			IO_Name = name;
			IO_Type = type;
			Cable = strCable;
			SubAddr = subaddr;
			Description = strDesc;
		}
		public override string ToString()
		{
			return IO_Name;
		}
	}
	#endregion

	#region SRZ IO 관련
	public enum SRZ_IO_TYPE : int
	{
		TIO_8888 = 0,
		TIO_VVVV,
		DIO,
	}

	[Serializable]
	public class SRZ_IO
	{
		public SRZ_IO_TYPE IO_Type = SRZ_IO_TYPE.TIO_8888;
		public string IO_Name = "";

		[XmlIgnore]
		public float Value = 0;

		[XmlIgnore]
		public float CurValue = -1;

		public SRZ_IO()
		{
			IO_Type = SRZ_IO_TYPE.TIO_8888;
			IO_Name = "";
			Value = 0;
			CurValue = -1;
		}
		public SRZ_IO(SRZ_IO_TYPE type)
		{
			IO_Type = type;
			IO_Name = "";
			Value = 0;
			CurValue = -1;
		}
		public override string ToString()
		{
			return IO_Name;
		}
	}
	#endregion

	#region Top Messageobox
	public class TopMessagebox
	{
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		public TopMessagebox()
		{
		}

		public static void Show(string strMsg)
		{
			System.Windows.Forms.MessageBox.Show(GetForegroundWindowHandle(), strMsg);
		}

		private static System.Windows.Forms.IWin32Window GetForegroundWindowHandle()
		{
			IntPtr handle = IntPtr.Zero;
			handle = GetForegroundWindow();
			return new WindowWrapper(handle);
		}
	}

	public class WindowWrapper : System.Windows.Forms.IWin32Window
	{
		private IntPtr _hwnd;
		public WindowWrapper(IntPtr handle)
		{
			_hwnd = handle;
		}
		public IntPtr Handle
		{
			get { return _hwnd; }
		}
	}
	#endregion

	#region ASCII Code
	public enum ASCII : byte
	{
		NULL = 0x00, SOH = 0x01, STX = 0x02, ETX = 0x03, EOT = 0x04, ENQ = 0x05, ACK = 0x06, BELL = 0x07,
		BS = 0x08, HT = 0x09, LF = 0x0A, VT = 0x0B, FF = 0x0C, CR = 0x0D, SO = 0x0E, SI = 0x0F, DC1 = 0x11,
		DC2 = 0x12, DC3 = 0x13, DC4 = 0x14, NAK = 0x15, SYN = 0x16, ETB = 0x17, CAN = 0x18, EM = 0x19,
		SUB = 0x1A, ESC = 0x1B, FS = 0x1C, GS = 0x1D, RS = 0x1E, US = 0x1F, SP = 0x20, DEL = 0x7F
	}
	#endregion
}
