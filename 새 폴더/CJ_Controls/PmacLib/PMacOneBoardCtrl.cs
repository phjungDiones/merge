using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CJ_Controls.PmacLib
{
	public partial class PMacOneBoardCtrl
	{
		public PMacOneBoardCtrl()
		{
			for (int nAxis = 0; nAxis < (int)SERVO_CNT.MAX_AXIS_COUNT; nAxis++)
			{
				int nAxis_1Base = nAxis + 1;
				m_AxisAllList.Add(new AxisInfo(nAxis_1Base));
			}
		}

		public void Init_Pmac(uint nBoardNo, int nInputStartAddr, int nOutputStartAddr, int nMaxIoCount)
		{
			m_nBoardNumber = nBoardNo;

			_Pmac = new PmacCtrl(nBoardNo, nInputStartAddr, nOutputStartAddr, nMaxIoCount);
			Load_ServoData(BoardNumber);
			LoadIOList();

			_Pmac.OpenPmac();

			_PmacThread = new Thread(new ThreadStart(Run));
			_PmacThread.IsBackground = true;
			_PmacThread.Start();
		}

		private DateTime dt_Seq_Tick_Check = DateTime.MaxValue;
		private Thread _PmacThread = null;
		private void Run()
		{
			while (true)
			{
				Thread.Sleep(20);

				try
				{
					if (_Pmac.IsOpen() == false)
						continue;

					for (int nAxis = 0; nAxis < (int)SERVO_CNT.MAX_AXIS_COUNT; nAxis++)
					{
						if (AxisAllList[nAxis].UseAxis == true)
						{
							_Pmac.ReadAxisCurVal(AxisAllList[nAxis]);
							_Pmac.IsHomeDone(AxisAllList[nAxis]);
							_Pmac.IsHomming(AxisAllList[nAxis]);
						}
					}

					_Pmac.ReadIOState();
					for (int i = 0; i < (int)_Pmac.IoInputVal.Length; i++)
					{
						if (IoList_All.Input.Count > i)
							IoList_All.Input[i].IsOn = _Pmac.IoInputVal[i];

						if (IoList_All.Output.Count > i)
							IoList_All.Output[i].IsOn = _Pmac.IoOutputVal[i];
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

				#region SEQUENCE_TICK_CHECK LOG
				if (dt_Seq_Tick_Check == DateTime.MaxValue)
				{
					dt_Seq_Tick_Check = DateTime.Now;
				}
				else
				{
					long nElapsed = ((DateTime.Now.Ticks - dt_Seq_Tick_Check.Ticks) / 10000);
					if (nElapsed > 500)
					{
						//string strLog = string.Format("MainSequence Tick {0}", nElapsed);
					}
					dt_Seq_Tick_Check = DateTime.Now;
				}
				#endregion
			}
		}

		private PmacCtrl _Pmac = null;
		public PmacCtrl GetPmac()
		{
			return _Pmac;
		}

		private IO_List _IoList_All = new IO_List();
		public IO_List IoList_All
		{
			get { return _IoList_All; }
			set { _IoList_All = value; }
		}
		public void LoadIOList()
		{
			try
			{
				string strDir = "Set";
				if (!Directory.Exists(strDir) && strDir != string.Empty)
				{
					Directory.CreateDirectory(strDir);
				}

				string strFilePath = strDir + "\\" + "PmacIoList_BD" + BoardNumber + ".xml";
				using (FileStream fs = new FileStream(strFilePath, FileMode.Open))
				{
					XmlSerializer xs = new XmlSerializer(typeof(IO_List));
					_IoList_All = xs.Deserialize(fs) as IO_List;
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		public void SaveIOList()
		{
			try
			{
				string strDir = "Set";
				if (!Directory.Exists(strDir) && strDir != string.Empty)
				{
					Directory.CreateDirectory(strDir);
				}

				string strFilePath = strDir + "\\" + "PmacIoList_BD" + BoardNumber + ".xml";
				if (File.Exists(strFilePath))
					File.Delete(strFilePath);

				using (FileStream fs = new FileStream(strFilePath, FileMode.Create))
				{
					XmlSerializer xs = new XmlSerializer(typeof(IO_List));
					xs.Serialize(fs, _IoList_All);
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private List<AxisInfo> m_AxisAllList = new List<AxisInfo>();
		public List<AxisInfo> AxisAllList
		{
			get { return m_AxisAllList; }
			set { m_AxisAllList = value; }
		}

		private uint m_nBoardNumber = 0;
		public uint BoardNumber
		{
			get { return m_nBoardNumber; }
		}

		public string Load_ServoData(uint nBoardNum)
		{
			string strRtn = "";
			try
			{
				string strDir = "Set";
				if (!Directory.Exists(strDir) && strDir != string.Empty)
				{
					Directory.CreateDirectory(strDir);
				}

				string strFilePath = strDir + "\\" + "ServoData_BD" + nBoardNum + ".xml";
				using (FileStream fs = new FileStream(strFilePath, FileMode.Open))
				{
					XmlSerializer xs = new XmlSerializer(typeof(List<AxisInfo>));
					m_AxisAllList = xs.Deserialize(fs) as List<AxisInfo>;
					strRtn = "Servo Data Load Success! BoardNo:" + nBoardNum;
				}
			}
			catch (System.Exception ex)
			{
				strRtn = "Servo Data Load Fail! BoardNo:" + nBoardNum + " " + ex.Message;
			}
			return strRtn;
		}

		public bool Save_ServoData(uint nBoardNum)
		{
			bool bRtn = false;
			string strRtn = "";
			try
			{
				string strDir = "Set";
				if (!Directory.Exists(strDir) && strDir != string.Empty)
				{
					Directory.CreateDirectory(strDir);
				}

				string strFilePath = strDir + "\\" + "ServoData_BD" + nBoardNum + ".xml";
				using (FileStream fs = new FileStream(strFilePath, FileMode.Create))
				{
					XmlSerializer xs = new XmlSerializer(typeof(List<AxisInfo>));
					xs.Serialize(fs, m_AxisAllList);
					bRtn = true;
					strRtn = "Servo Data Save Success! BoardNo:" + nBoardNum;
				}
			}
			catch (System.Exception ex)
			{
				strRtn = "Servo Data Save Fail! BoardNo:" + nBoardNum + " " + ex.Message;
				bRtn = false;
			}

			return bRtn;
		}

		public void ShowServoConfig(IWin32Window _Parent)
		{
			Form_Engr_Servo_Config dlg = new Form_Engr_Servo_Config();
			dlg.MotorCtrl = this;
			dlg.SetBoardNo(BoardNumber);
			dlg.ShowDialog(_Parent);
		}

		public void ShowIoConfig(IWin32Window _Parent)
		{
			Form_Engr_Io_List dlg = new Form_Engr_Io_List();
			dlg.PMacControl = this;
			dlg.ShowDialog(_Parent);
		}
	}
}
