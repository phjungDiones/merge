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
	* Ŭ������ : SystemData
	* �ۼ��� : JWBaek
	* �����ۼ��� : 2012�� 8�� 09��
	* ���������� : 2012�� 8�� 09��
	* 
	* ������:
	* - System Resource Monitoring Ŭ����
	*   
	*   =�ҽ���ó=
	*   �ڵ�������Ʈ�� 'An Implementation of System Monitor'�� 
	*   �ܺο��� ���� ���� ���ϰ� ����
	*   �����ּ� : http://www.codeproject.com/Articles/20380/An-Implementation-of-System-Monitor
	* 
	* �����:
	* - 1. DataChart.cs, SystemData.cs, ResourceMonitor.cs, ResourceMonitor.Designer.cs ���� �߰�
	* - 2. �ַ�� Ž���⿡�� �����߰�(System.Management, System.Management.Instrumentation)
	* - 3. Rebuild All
	* - 4. �������ڿ��� ��Ŭ��->�׸���->.NET Framework �������->ã�ƺ��⿡�� Rebuild�� ���̳ʸ����� �����ϸ� ResourceMonitor �߰���
	* - 5. ���ϴ°��� ��Ʈ�� �߰��ϸ� �ڵ� �����
	*  
	* ���� ���ǻ���
	* - 
	---------------------------------------*/
	public class SystemData
	{
		#region �̱��� ���
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
			//���� �޸� ũ�� ����(���� �ѹ��� �����ϸ��)
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
		 * CPU ����
		 ---------------------------------------------------------------------*/
		CurSWCpuUsage _CurSWCpu = new CurSWCpuUsage();
		public double GetCpuUsage( bool bCurSW)
		{
			/*
			Math.Ceiling(doubleValue);// �ø�
			Math.Round(doubleValue); // �ݿø�
			Math.Truncate(doubleValue);// ����
			 */
			double dCpuUsage = 0;

			if(bCurSW)
				dCpuUsage = _CurSWCpu.GetUsage();
			else
				dCpuUsage = GetCounterValue(_cpuCounter, "Processor", "% Processor Time", "_Total");

			return Math.Round(dCpuUsage);
		}

		/*---------------------------------------------------------------------
		 * ���� �޸� ��뷮 ���ڿ� Get
		 * "00.00%(usage(00.00)GB / total(00.00)GB)" ������ ���ڿ� ����
		 ---------------------------------------------------------------------*/
		public string GetPhysicalMemoryString()
		{
			//���� �޸� ��뷮 ���ϱ� (��ü - ��밡���� �޸𸮾�)
			double usage = PhysicalMemory - PhysicalMemoryAvailableBytes;

			//���ڿ��� ��ȯ
			string result = PhysicalMemoryUsage.ToString("F") + "% (" + FormatBytes(usage) + " / " + FormatBytes(PhysicalMemory) + ")";

			return result;
		}
		/*---------------------------------------------------------------------
		 * ���� �޸� ��뷮 ���ڿ� Get
		 * "00.00%(usage(00.00)GB / total(00.00)GB)" ������ ���ڿ� ����
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
		 * CPU �µ� Get
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
					double kelvin_Temp = (raw_Temp / 10); //�̰� �̺�.
					double Celsius_Temp = (raw_Temp / 10) - 273.15; //�̰� ����.
					double Fahrenheit_Temp = ((raw_Temp / 10) - 273.15) * 9 / 5 + 32; //�̰� ȭ��.
					
					_Temp.Add(Celsius_Temp.ToString()); //���ø� �������ش�.
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("GetCpuTemperature " + ex.Message);
			}
			return _Temp;
		}
		/*----------------------------------------------------------------
		 * Logical Hard Disk �������� string Get
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
		 * Logical Hard Disk �����(%) Get
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
		 * ���˺�ȯ
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
		 * Logical Hard Disk ���� Get
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
		 �����޸� �뷮 Get
		 �����޸� �뷮�� �߰��� ��������� �����Ƿ� ���۽� �ѹ��� ȣ���ϸ� ��
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