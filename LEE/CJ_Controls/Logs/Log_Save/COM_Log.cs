using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace CJ_Controls.Log_Save
{
	public class COM_LogSaver : Component
	{
		private string m_strExt = ".csv";
		[Category("Log_Setting"), Description("확장자 (ex: '.log' , '.csv')"), DefaultValue(false)]
		public string EXTENDER
		{
			get { return m_strExt; }
			set { m_strExt = value; }
		}

		private int m_nKeepDay = 90;
		[Category("Log_Setting"),Description("로그가 보관되는 기간"),DefaultValue(false)]
		public int KEEP_DAY
		{
			get { return m_nKeepDay; }
			set { m_nKeepDay = value; }
		}
		private int m_nSaveTime = 0;
		[Category("Log_Setting"), Description("로그가 기록되는 시간 (0:하루, 1-24:시간)"), DefaultValue(false)]
		public int SAVE_HOUR
		{
			get { return m_nSaveTime; }
			set
			{
				if (value < 0 || value > 24)
					m_nSaveTime = 0;
				else
					m_nSaveTime = value;
			}
		}

		private string m_strLogRootPath = "Log\\";
		[Category("Log_Setting"), Description("로그가 기록되는 기본 폴더"), DefaultValue(false)]
		public string LOG_ROOT_PATH
		{
			get { return m_strLogRootPath; }
			set
			{
				m_strLogRootPath = value;

				if (m_strLogRootPath.EndsWith("\\") == false)
					m_strLogRootPath += "\\";
			}
		}

		private List<OneLog> m_Log = null;
		private Thread m_SaveThread = null;
		private Thread m_DeleteThread = null;
		public COM_LogSaver()
		{
			m_Log = new List<OneLog>();
			SetLog("EXCEPTION");

			m_SaveThread = new Thread(new ThreadStart(Run_Save));
			m_SaveThread.IsBackground = true;

			m_DeleteThread = new Thread(new ThreadStart(Run_Delete));
			m_DeleteThread.IsBackground = true;
		}

		public void LogProcessStart()
		{
			m_SaveThread.Start();
			m_DeleteThread.Start();
		}
		public void LogProcessEnd()
		{
			m_SaveThread.Abort();
			m_DeleteThread.Abort();
			foreach (OneLog _One in m_Log)
			{
				_One.CloseFile();
			}
		}

		public int IsExistLogName(string key)
		{
			int nRtn = -1;

			int nIndex = 0;
			foreach (OneLog _Log in m_Log)
			{
				if (_Log.GetName().Equals(key))
				{
					nRtn = nIndex;
					break;
				}
				nIndex++;
			}
			return nRtn;
		}

		private void Run_Save()
		{
			while (true)
			{
				lock (m_Log)
				{
					foreach (OneLog _Log in m_Log)
					{
						try
						{
							_Log.SaveData();
						}
						catch (Exception ex)
						{
							Console.Write(ex.Message);
						}
					}
				}
				Thread.Sleep(20);
			}
		}

		private void Run_Delete()
		{
			while (true)
			{
				lock (m_Log)
				{
					foreach (OneLog _Log in m_Log)
					{
						try
						{
							_Log.DeleteOldFile();
						}
						catch (Exception ex)
						{
							Console.Write(ex.Message);
						}
					}
				}

				Thread.Sleep( (1000*60)*60 ); //한시간 주기로 도세요~
			}
		}

		public void SetLog(string strLogName)
		{
			//Log 폴더
			if (!Directory.Exists(m_strLogRootPath))
			{
				Directory.CreateDirectory(m_strLogRootPath);
			}

			SetLog(m_strLogRootPath, strLogName);
		}

		public void SetLog(string DirectoryPath, string strLogName)
		{
			int nIndex = IsExistLogName(strLogName);
			if(nIndex < 0)
			{//없다. 추가.
				lock(m_Log)
					m_Log.Add(new OneLog(DirectoryPath, strLogName, m_strExt, m_nKeepDay, m_nSaveTime));
			}
			else
			{//있다. 설정변경.
				lock (m_Log)
					m_Log[nIndex].InitLog(DirectoryPath, strLogName, m_strExt, m_nKeepDay, m_nSaveTime);
			}
		}

		public void LogTextOut(string LogName, string Message, DateTime LogTime)
		{
			lock (m_Log)
			{
				int nIndex = IsExistLogName(LogName);
				if (nIndex < 0)
				{
					SetLog(LogName);
				}

				// 그래도 없으면,, EXCEPTION
				nIndex = IsExistLogName(LogName);
				if (nIndex < 0)
				{
					int nEx = IsExistLogName("EXCEPTION");
					if (nEx < 0)
						return;

					m_Log[nEx].AddMsg(LogTime, string.Format("[{0}] {1}", LogName, Message));
				}
				else
				{
					m_Log[nIndex].AddMsg(LogTime, Message);
				}
			}
		}
		public void LogTextOut(string LogName, string Message)
		{
			LogTextOut(LogName, Message, DateTime.Now);
		}
	}

	class OneLog
	{
		string m_strDir = "";
		string m_strLogName = "";
		string m_strExt = ".log";
		int m_nKeepDay = 30;
		int m_nSaveTime = 0;

		private Queue<LogQueue> _SaveQ = new Queue<LogQueue>();

		public OneLog(string strDir, string strName, string strExt, int nKeepDay, int nSaveTime)
		{
			InitLog(strDir, strName, strExt, nKeepDay, nSaveTime);
		}
		
		public string GetName()
		{
			return m_strLogName;
		}
		public void InitLog(string strDir, string strName, string strExt, int nKeepDay, int nSaveTime)
		{
			m_strDir = strDir + strName + "\\";
			m_strLogName = strName;
			m_strExt = strExt;
			m_nKeepDay = nKeepDay;
			m_nSaveTime = nSaveTime;

			//어짜피 저장 할 때, 오픈한다.
			//OpenFile(DateTime.Now);
		}

		public void AddMsg(DateTime time, string Message)
		{
			lock (_SaveQ)
			{
				_SaveQ.Enqueue(new LogQueue(time, Message));
			}
		}

		public void SaveData()
		{
			lock (_SaveQ)
			{
				while (_SaveQ.Count > 0)
				{
					LogQueue item = _SaveQ.Dequeue();
					TextOut(item.GetData(),item.GetTime());
				}
			}
		}

		FileStream logFileStream = null;		// Log를 남기기 위한 File Handle
		StreamWriter logStreamWriter = null;	// File에 쓰기위한 StreamWriter
		string m_strFullPath = "";				// File 경로.

		public void TextOut(string msg, DateTime dateTime)
		{
			OpenFile(dateTime);
			logStreamWriter.WriteLine(msg);
			logStreamWriter.Flush();
		}

		private void OpenFile(DateTime dt)
		{
			if (logFileStream != null)
			{
				if (m_strFullPath.Equals(m_strDir + m_strLogName + "_" + GetCurFileName(dt) + m_strExt))
					return;
				else
					CloseFile();
			}

			m_strFullPath = m_strDir + m_strLogName + "_" + GetCurFileName(dt) + m_strExt;
			if (!Directory.Exists(m_strDir) && m_strDir != string.Empty)
			{
				Directory.CreateDirectory(m_strDir);
			}

			//열기
			logFileStream = new FileStream(m_strFullPath, FileMode.Append, FileAccess.Write, FileShare.Read);
			logStreamWriter = new StreamWriter(logFileStream, Encoding.Default);
		}
		public void CloseFile()
		{
			if (logStreamWriter != null)
				logStreamWriter.Close();
			if (logFileStream != null)
				logFileStream.Close();
			logFileStream = null;
			logStreamWriter = null;
		}
		private string GetCurFileName(DateTime timeCur)
		{// [2012-04-27] by Changjin.Jeong : 옵션에 따라 이름생성.
			string strRtnFileName = "";
			if (m_nSaveTime == 0)
			{// 하루
				strRtnFileName = string.Format("{0}", timeCur.ToString("yyyyMMdd"));
			}
			else if (m_nSaveTime == 1)
			{// 한시간
				strRtnFileName = string.Format("{0}_{1:d02}", timeCur.ToString("yyyyMMdd"), timeCur.Hour);
			}
			else
			{
				// 시간단위 저장은 시작시간 고정..
				int nStartHour = 0;
				nStartHour = (timeCur.Hour / m_nSaveTime) * m_nSaveTime;
				strRtnFileName = string.Format("{0}_{1:d02}~", timeCur.ToString("yyyyMMdd"), nStartHour);
			}
			return strRtnFileName;
		}

		public void DeleteOldFile()
		{
			TimeSpan timeSpan = new TimeSpan(m_nKeepDay, 0, 0, 0);
			DateTime timeDelete = DateTime.Now - timeSpan;
			string[] strAllFiles = System.IO.Directory.GetFiles(m_strDir, "*", System.IO.SearchOption.AllDirectories);

			foreach (string strOnePath in strAllFiles)
			{
				System.IO.FileInfo fi = new System.IO.FileInfo(strOnePath);

				if (timeDelete >= fi.CreationTime || timeDelete >= fi.LastWriteTime //)		//지정시간 이전꺼
					|| DateTime.Now < fi.CreationTime || DateTime.Now < fi.LastWriteTime) //오늘날짜보다 더 큰것.
				{
					try
					{
						fi.Delete();
					}
					catch (System.Exception ex)
					{
						Console.WriteLine("Delete file Error! ({0})", ex.Message);
					}
				}
			}
		}
	}

	class LogQueue
	{
		DateTime m_time = DateTime.MinValue;
		string m_strMsg = "";

		public LogQueue(DateTime time, string strMsg)
		{
			m_time = time;
			m_strMsg = strMsg;
		}
		public string GetData()
		{
			string strRtn = "";
			if (m_strMsg != "")
			{
				if(m_strMsg[0] == '\t')
					strRtn = string.Format("{0}:{1:d03}{2}", m_time.ToString("yyyy-MM-dd HH:mm:ss"), m_time.Millisecond, m_strMsg);
				else
					strRtn = string.Format("{0}:{1:d03},{2}", m_time.ToString("yyyy-MM-dd HH:mm:ss"), m_time.Millisecond, m_strMsg);
			}

			return strRtn;
		}
		public DateTime GetTime()
		{
			return m_time;
		}
	}
}
