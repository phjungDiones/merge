using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CJ_Controls.SystemMonitor
{
	/* ---------------------------------------
	 * Resource Monitor 사용자 컨트롤
	 * 자세한 사용법은 SystemData.cs 파일 참조
	 ---------------------------------------*/
	public partial class Ctrl_ResourceMonitor : UserControl
	{
		#region Declare Private Member Variable
		private Thread _monitoringThread = null;
		private bool _isMonitoring = false;
		private bool _isCpuTempView = false;
		#endregion

		#region Encapsulate Private Member Variable
		public bool IsMonitoring
		{
			get { return _isMonitoring; }
			set { _isMonitoring = value; }
		}
		public bool IsCpuTempView
		{
			get { return _isCpuTempView; }
			set { _isCpuTempView = value; }
		}
		public Thread MonitoringThread
		{
			get { return _monitoringThread; }
		}
		#endregion

		#region GUI 갱신용 데이터들.
		private delegate void DataRefresh();
		string m_strSwCpuUsage = "";
		int m_nSwCpuUsage = 0;

		string m_strPcCpuUsage = "";
		int m_nPcCpuUsage = 0;
//		string m_strCpuTemp = "";

		string m_strVirMem = "";
		float m_fVirMem = 0;

		string m_strPicMem = "";
		float m_fPicMem = 0;

		string m_strHdd_C = "";
		int m_nHdd_C = 0;

		string m_strHdd_D = "";
		int m_nHdd_D = 0;

		List<string> m_GetTemp = null;
		#endregion

		#region Constructor/Dispose
		public Ctrl_ResourceMonitor()
		{
			InitializeComponent();
			MonitoringStart();
		}

		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			//Thread 종료
			MonitoringStop();

			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		#endregion
		/*-------------------------------------------------------------
         * Resource 정보 출력 시작 - 생성자에서 자동시작됨
         -------------------------------------------------------------*/
		public bool MonitoringStart()
		{
			MonitoringStop();

			IsMonitoring = true;
			_monitoringThread = new Thread(new ThreadStart(MonitoringRun));
			_monitoringThread.IsBackground = true;
			_monitoringThread.Start();

			return true;
		}
		/*-------------------------------------------------------------
		 * Resource 정보 출력 종료 - Dispose()에서 자동종료됨
		 -------------------------------------------------------------*/
		public void MonitoringStop()
		{
			if (IsMonitoring)
			{
				IsMonitoring = false;
				bool result = MonitoringThread.Join(1100);
				if (!result)
					MonitoringThread.Abort();
			}
		}
		/*-------------------------------------------------------------
		 * Resource 정보 출력 스레드함수
		 -------------------------------------------------------------*/
		private void MonitoringRun()
		{
			while (IsMonitoring)
			{
				Thread.Sleep(1000);
				ShowResource();
			}
		}
		public int GetCpuUsage(bool bCurSw)
		{
			int nCpuUsage = 0;
			if (bCurSw)
				nCpuUsage = m_nSwCpuUsage;
			else
				nCpuUsage = m_nPcCpuUsage;

			return nCpuUsage;
		}
		/*-------------------------------------------------------------
		 * Resource 정보 출력
		 -------------------------------------------------------------*/
		private void ShowResource()
		{
			SystemData sysData = SystemData.Instance;

			try
			{
				// SW CPU 사용율 출력
				m_nSwCpuUsage = Convert.ToInt32(sysData.GetCpuUsage(true));
				if (m_nSwCpuUsage < 0)
					m_nSwCpuUsage = 0;
				m_strSwCpuUsage = string.Format("{0}%", m_nSwCpuUsage);

				// PC CPU 사용율 출력
				int nPcCpu = Convert.ToInt32(sysData.GetCpuUsage(false));
				m_nPcCpuUsage = (nPcCpu >= m_nSwCpuUsage) ? nPcCpu : m_nSwCpuUsage;
				m_strPcCpuUsage = string.Format("{0}%", m_nPcCpuUsage);

				//CPU 온도 표시
//				m_strCpuTemp = sysData.GetCPUTemperature();

				//메모리 사용량 출력 (가상메모리)
				m_fVirMem = sysData.VirtualMemoryUsage;
				m_strVirMem = sysData.GetVirtualMemoryString();

				//메모리 사용량 출력 (물리메모리)
				m_fPicMem = sysData.PhysicalMemoryUsage;
				m_strPicMem = sysData.GetPhysicalMemoryString();

				//논리 하드디스크 사용율 출력 (C drive)
				m_strHdd_C = sysData.GetLogicalDiskSpace("C:");
				m_nHdd_C = sysData.GetLogicalDiskUsage("C:");

				//논리 하드디스크 사용율 출력 (D drive)
				m_strHdd_D = sysData.GetLogicalDiskSpace("D:");
				m_nHdd_D = sysData.GetLogicalDiskUsage("D:");

				m_GetTemp = sysData.GetCPUTemperature();
				Invoke(new DataRefresh(ViewData));
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("PC Resource Monitoring : " + ex.Message);
			}
		}

		bool m_bRed = true;
		int m_nDanger = 70;
		private void ViewData()
		{
			ProgressBar_SW_CPU.Value = m_nSwCpuUsage < 0 ? 0 : m_nSwCpuUsage;
			Label_SW_CPU_Val.Text = m_strSwCpuUsage;

			if (m_nSwCpuUsage < m_nDanger)
				Label_SW_CPU.ForeColor = Color.Blue;
			else
				Label_SW_CPU.ForeColor = m_bRed ? Color.Red : Color.Orange;

			ProgressBar_PC_CPU.Value = m_nPcCpuUsage;
			Label_PC_CPU_Val.Text = m_strPcCpuUsage;

			if (m_nPcCpuUsage < m_nDanger)
				Label_PC_CPU.ForeColor = Color.Blue;
			else
				Label_PC_CPU.ForeColor = m_bRed ? Color.Red : Color.Orange;

			//CPU 온도 표시
//			if (m_strCpuTemp != "")
//				lblCPUTemp.Text = m_strCpuTemp + "˚C";
//			else
//				lblCPUTemp.Text = "";

			//메모리 사용량 출력 (가상메모리)
			pgbVirtualMemory.Value = (int)m_fVirMem;
			lblVirtualMemory.Text = m_strVirMem;
			if (m_fVirMem < m_nDanger)
				Label_VirtualMem.ForeColor = Color.Blue;
			else
				Label_VirtualMem.ForeColor = m_bRed ? Color.Red : Color.Orange;

			//메모리 사용량 출력 (물리메모리)
			pgbPhysicalMemory.Value = (int)m_fPicMem;
			lblPhysicalMemory.Text = m_strPicMem;
			if (m_fPicMem < m_nDanger)
				Label_PhysicalMem.ForeColor = Color.Blue;
			else
				Label_PhysicalMem.ForeColor = m_bRed ? Color.Red : Color.Orange;

			//논리 하드디스크 사용율 출력 (C drive)
			lblCDriveSpace.Text = m_strHdd_C;
			pgbCDrive.Value = m_nHdd_C;
			if (m_nHdd_C < m_nDanger)
				Label_Hdd_C.ForeColor = Color.Blue;
			else
				Label_Hdd_C.ForeColor = m_bRed ? Color.Red : Color.Orange;

			//논리 하드디스크 사용율 출력 (D drive)
			lblDDriveSpace.Text = m_strHdd_D;
			pgbDDrive.Value = m_nHdd_D;
			if (m_nHdd_D < m_nDanger)
				Label_Hdd_D.ForeColor = Color.Blue;
			else
				Label_Hdd_D.ForeColor = m_bRed ? Color.Red : Color.Orange;

			//CPU 온도 출력
			if (IsCpuTempView == true)
			{
				if (m_GetTemp != null)
				{
					if (m_GetTemp.Count <= 0)
					{
						if (GroupCPU.Text != "[ CPU Usage ]")
							GroupCPU.Text = "[ CPU Usage ]";
					}
					else
					{
						string strTemp = "[ CPU Usage : ";
						for (int i = 0; i < m_GetTemp.Count; i++)
						{
							strTemp += string.Format("{0}℃", m_GetTemp[i]);
							if (m_GetTemp.Count - 1 <= i)
								strTemp += " ]";
							else
								strTemp += ",";
						}
						if (GroupCPU.Text != strTemp)
							GroupCPU.Text = strTemp;
					}
				}
			}

			m_bRed = !m_bRed;
		}
	}
}
