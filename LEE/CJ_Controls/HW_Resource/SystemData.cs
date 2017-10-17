using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using ComTypes = System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Collections.Generic;

namespace CJ_Controls.SystemMonitor
{
	/* ---------------------------------------
	* 클래스명 : SystemData
	* 작성자 : JWBaek
	* 최초작성일 : 2012년 8월 09일
	* 최종수정일 : 2012년 8월 09일
	* 
	* 사용목적:
	* - System Resource Monitoring 클래스
	*   
	*   =소스출처=
	*   코드프로젝트의 'An Implementation of System Monitor'를 
	*   외부에서 좀더 쓰기 편하게 수정
	*   원본주소 : http://www.codeproject.com/Articles/20380/An-Implementation-of-System-Monitor
	* 
	* 사용방법:
	* - 1. DataChart.cs, SystemData.cs, ResourceMonitor.cs, ResourceMonitor.Designer.cs 파일 추가
	* - 2. 솔루션 탐색기에서 참조추가(System.Management, System.Management.Instrumentation)
	* - 3. Rebuild All
	* - 4. 도구상자에서 우클릭->항목선택->.NET Framework 구성요소->찾아보기에서 Rebuild한 바이너리파일 선택하면 ResourceMonitor 추가됨
	* - 5. 원하는곳에 컨트롤 추가하면 자동 실행됨
	*  
	* 사용시 유의사항
	* - 
	---------------------------------------*/
	public class SystemData
	{
		#region 싱글톤 멤버
		private static SystemData _Instance = null;
		private static object lockObj = new object();

		public static SystemData Instance
		{
			get
			{
				if (_Instance == null)  //Double Checked Lock
				{
					lock (lockObj)
					{
						if (_Instance == null)
						{
							_Instance = new SystemData();
						}
					}
				}
				return _Instance;
			}
		}
		#endregion

		#region Declare Private Member Variable

		PerformanceCounter _cpuCounter = new PerformanceCounter();
		PerformanceCounter _memoryCounter = new PerformanceCounter();
		private UInt64 _physicalMemory = 0;

		//PerformanceCounter _cpuCounter = new PerformanceCounter();
		//PerformanceCounter _diskReadCounter = new PerformanceCounter();
		//PerformanceCounter _diskWriteCounter = new PerformanceCounter();        
		#endregion

		#region Constructor
		public SystemData()
		{
			//물리 메모리 크기 저장(최초 한번만 저장하면됨)
			PhysicalMemory = GetPhysicalMemory();
		}
		#endregion

		#region Encapsulate Private Member Variable
		public PerformanceCounter MemoryCounter
		{
			get { return _memoryCounter; }
		}
		public UInt64 PhysicalMemory
		{
			get { return _physicalMemory; }
			set { _physicalMemory = value; }
		}
		#endregion


		//         public string GetProcessorData()
		// 		{
		//             double d = GetCounterValue(CPUCounter, "Processor", "% Processor Time", "_Total");
		// 			return (int)d +"%";//: d.ToString("F") +"%";
		// 		}

		public float VirtualMemoryUsage
		{
			get
			{
				return GetCounterValue(MemoryCounter, "Memory", "% Committed Bytes In Use", null);
			}
		}
		public float PhysicalMemoryUsage
		{
			get
			{
				double d = PhysicalMemory - PhysicalMemoryAvailableBytes;

				d /= PhysicalMemory;
				d *= 100;

				return (float)d;
			}
		}
		public double PhysicalMemoryAvailableBytes
		{
			get
			{
				return GetCounterValue(MemoryCounter, "Memory", "Available Bytes", null);
			}
		}

		/*---------------------------------------------------------------------
		 * CPU 사용률
		 ---------------------------------------------------------------------*/
		CurSWCpuUsage _CurSWCpu = new CurSWCpuUsage();
		public double GetCpuUsage( bool bCurSW)
		{
			/*
			Math.Ceiling(doubleValue);// 올림
			Math.Round(doubleValue); // 반올림
			Math.Truncate(doubleValue);// 버림
			 */
			double dCpuUsage = 0;

			if(bCurSW)
				dCpuUsage = _CurSWCpu.GetUsage();
			else
				dCpuUsage = GetCounterValue(_cpuCounter, "Processor", "% Processor Time", "_Total");

			return Math.Round(dCpuUsage);
		}

		/*---------------------------------------------------------------------
		 * 물리 메모리 사용량 문자열 Get
		 * "00.00%(usage(00.00)GB / total(00.00)GB)" 형태의 문자열 리턴
		 ---------------------------------------------------------------------*/
		public string GetPhysicalMemoryString()
		{
			//현재 메모리 사용량 구하기 (전체 - 사용가능한 메모리양)
			double usage = PhysicalMemory - PhysicalMemoryAvailableBytes;

			//문자열로 변환
			string result = PhysicalMemoryUsage.ToString("F") + "% (" + FormatBytes(usage) + " / " + FormatBytes(PhysicalMemory) + ")";

			return result;
		}
		/*---------------------------------------------------------------------
		 * 가상 메모리 사용량 문자열 Get
		 * "00.00%(usage(00.00)GB / total(00.00)GB)" 형태의 문자열 리턴
		 ---------------------------------------------------------------------*/
		public string GetVirtualMemoryString()
		{
			string str = "";
			double d = 0;
			try
			{
				d = GetCounterValue(MemoryCounter, "Memory", "% Committed Bytes In Use", null);
				str = d.ToString("F") + "% (";

				d = GetCounterValue(MemoryCounter, "Memory", "Committed Bytes", null);
				str += FormatBytes(d) + " / ";

				d = GetCounterValue(MemoryCounter, "Memory", "Commit Limit", null);
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("GetVirtualMemoryString " + ex.Message);
			}
			
			return str + FormatBytes(d) + ") ";
		}
		// 		public enum DiskData {ReadAndWrite, Read, Write};
		// 
		// 		public double GetDiskData(DiskData dd)
		// 		{
		// 			return	dd==DiskData.Read? 
		// 						GetCounterValue(_diskReadCounter, "PhysicalDisk", "Disk Read Bytes/sec", "_Total"):
		// 					dd==DiskData.Write? 
		// 						GetCounterValue(_diskWriteCounter, "PhysicalDisk", "Disk Write Bytes/sec", "_Total"):
		// 					dd==DiskData.ReadAndWrite? 
		// 						GetCounterValue(_diskReadCounter, "PhysicalDisk", "Disk Read Bytes/sec", "_Total")+
		// 						GetCounterValue(_diskWriteCounter, "PhysicalDisk", "Disk Write Bytes/sec", "_Total"):
		// 					0;
		// 		}

		/*----------------------------------------------------------
		 * CPU 온도 Get
		 ----------------------------------------------------------*/
		public List<string> GetCPUTemperature()
		{
			List<string> _Temp = new List<string>();
			try
			{
				ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
				foreach (ManagementObject obj in searcher.Get())
				{
					double raw_Temp = Convert.ToDouble(obj.Properties["CurrentTemperature"].Value);
					double kelvin_Temp = (raw_Temp / 10); //이게 켈빈.
					double Celsius_Temp = (raw_Temp / 10) - 273.15; //이게 섭시.
					double Fahrenheit_Temp = ((raw_Temp / 10) - 273.15) * 9 / 5 + 32; //이게 화씨.
					
					_Temp.Add(Celsius_Temp.ToString()); //섭시만 리턴해준다.
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("GetCpuTemperature " + ex.Message);
			}
			return _Temp;
		}
		/*----------------------------------------------------------------
		 * Logical Hard Disk 남은공간 string Get
		 ----------------------------------------------------------------*/
		public string GetLogicalDiskSpace(string driveName)
		{
			string result = "";
			ManagementObject diskInfo = GetLogicalDiskInfo(driveName);

			if (diskInfo != null)
			{
				UInt64 driveSize = (UInt64)diskInfo["Size"];
				UInt64 driveSpace = (UInt64)diskInfo["FreeSpace"];	// C:10.32 GB, D:5.87GB
				result = FormatBytes(driveSize) + " / " + FormatBytes(driveSpace) + " Remained";
			}

			return result;
		}
		/*----------------------------------------------------------------
		 * Logical Hard Disk 사용율(%) Get
		 ----------------------------------------------------------------*/
		public int GetLogicalDiskUsage(string driveName)
		{
			int result = 0;
			ManagementObject diskInfo = GetLogicalDiskInfo(driveName);
			if (diskInfo != null)
			{
				UInt64 driveSize = (UInt64)diskInfo["Size"];
				UInt64 driveSpace = (UInt64)diskInfo["FreeSpace"];	// C:10.32 GB, D:5.87GB

				double usage = driveSize - driveSpace;

				usage /= driveSize;
				usage *= 100;
				result = (int)usage;
			}
			return result;
		}

		#region Implement Private Member Function
		enum Unit { B, KB, MB, GB, ER }
		/*----------------------------------------------------------------
		 * 포맷변환
		 ----------------------------------------------------------------*/
		private string FormatBytes(double bytes)
		{
			int unit = 0;
			while (bytes > 1024)
			{
				bytes /= 1024;
				++unit;
			}

			string s = /*_compactFormat? ((int)bytes).ToString():*/ bytes.ToString("F") + " ";
			return s + ((Unit)unit).ToString();
		}
		/*----------------------------------------------------------------
		 * Logical Hard Disk 정보 Get
		 ----------------------------------------------------------------*/
		private ManagementObject GetLogicalDiskInfo(string driveName)
		{
			ManagementObject result = null;

			ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");
			foreach (ManagementObject obj in search.Get())
			{
				string drive = (string)obj["DeviceID"];
				if (driveName == drive)
				{
					result = obj;
					break;
				}
			}

			return result;
		}
		/*-------------------------------------------------------------
		 물리메모리 용량 Get
		 물리메모리 용량은 중간에 변경될일이 없으므로 시작시 한번만 호출하면 됨
		-------------------------------------------------------------*/
		private UInt64 GetPhysicalMemory()
		{
			UInt64 ret = 0;

			try
			{
				ManagementObjectSearcher result = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");

				foreach (ManagementObject obj in result.Get())
				{
					//Type type = obj["totalphysicalmemory"].GetType();
					ret = (UInt64)obj["totalphysicalmemory"];
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("GetPhysicalMemory " + ex.Message);
			}
			
			return ret;
		}

		private float GetCounterValue(PerformanceCounter pc, string categoryName, string counterName, string instanceName)
		{
			pc.CategoryName = categoryName;
			pc.CounterName = counterName;
			pc.InstanceName = instanceName;
			return pc.NextValue();
		}
		#endregion
	}

	class CurSWCpuUsage
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool GetSystemTimes(
					out ComTypes.FILETIME lpIdleTime,
					out ComTypes.FILETIME lpKernelTime,
					out ComTypes.FILETIME lpUserTime
					);

		ComTypes.FILETIME _prevSysKernel;
		ComTypes.FILETIME _prevSysUser;

		TimeSpan _prevProcTotal;

		double _cpuUsage;
		DateTime _lastRun;
		long _runCount;

		public CurSWCpuUsage()
		{
			_cpuUsage = -1;
			_lastRun = DateTime.MinValue;
			_prevSysUser.dwHighDateTime = _prevSysUser.dwLowDateTime = 0;
			_prevSysKernel.dwHighDateTime = _prevSysKernel.dwLowDateTime = 0;
			_prevProcTotal = TimeSpan.MinValue;
			_runCount = 0;
		}

		public double GetUsage()
		{
			double cpuCopy = _cpuUsage;
			if (Interlocked.Increment(ref _runCount) == 1)
			{
				if (!EnoughTimePassed)
				{
					Interlocked.Decrement(ref _runCount);
					return cpuCopy;
				}

				ComTypes.FILETIME sysIdle, sysKernel, sysUser;
				TimeSpan procTime;

				Process process = Process.GetCurrentProcess();
				procTime = process.TotalProcessorTime;

				if (!GetSystemTimes(out sysIdle, out sysKernel, out sysUser))
				{
					Interlocked.Decrement(ref _runCount);
					return cpuCopy;
				}

				if (!IsFirstRun)
				{
					UInt64 sysKernelDiff =
						SubtractTimes(sysKernel, _prevSysKernel);
					UInt64 sysUserDiff =
						SubtractTimes(sysUser, _prevSysUser);

					UInt64 sysTotal = sysKernelDiff + sysUserDiff;

					Int64 procTotal = procTime.Ticks - _prevProcTotal.Ticks;

					if (sysTotal > 0)
					{
						_cpuUsage = (float)((100.0 * procTotal) / sysTotal);
					}
				}

				_prevProcTotal = procTime;
				_prevSysKernel = sysKernel;
				_prevSysUser = sysUser;

				_lastRun = DateTime.Now;

				cpuCopy = _cpuUsage;
			}
			Interlocked.Decrement(ref _runCount);

			return cpuCopy;

		}

		private UInt64 SubtractTimes(ComTypes.FILETIME a, ComTypes.FILETIME b)
		{
			UInt64 aInt =
			((UInt64)(a.dwHighDateTime << 32)) | (UInt64)a.dwLowDateTime;
			UInt64 bInt =
			((UInt64)(b.dwHighDateTime << 32)) | (UInt64)b.dwLowDateTime;

			return aInt - bInt;
		}

		private bool EnoughTimePassed
		{
			get
			{
				const int minimumElapsedMS = 250;
				TimeSpan sinceLast = DateTime.Now - _lastRun;
				return sinceLast.TotalMilliseconds > minimumElapsedMS;
			}
		}

		private bool IsFirstRun
		{
			get
			{
				return (_lastRun == DateTime.MinValue) ? true : false;
			}
		}
	}
}