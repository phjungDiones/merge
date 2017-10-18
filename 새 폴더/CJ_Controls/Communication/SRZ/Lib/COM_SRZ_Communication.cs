using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace CJ_Controls.Communication.SRZ
{
	public class COM_SRZ_Communication : Component
	{
		#region Log 보내기
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public event MessageEventHandler MessageEvent;

		public void LogTextOut(string message)
		{
			if (MessageEvent != null)
				MessageEvent(this, new MessageEventArgs("[COM_SRZ] " + message));
		}
		public void LibTextOut(string message)
		{
			if (MessageEvent != null)
				MessageEvent(this, new MessageEventArgs(message));
		}
		#endregion

		#region Alarm 보내기
		public event MessageEventHandler AlarmEvent;

		public void SetAlarm(string message)
		{
			if (AlarmEvent != null)
				AlarmEvent(this, new MessageEventArgs(message));
		}
		#endregion

		Class_XRKC m_RKC = new Class_XRKC();
		Thread m_Thread = null;
		public COM_SRZ_Communication()
		{
			m_RKC.MessageEvent += new Class_XRKC.MessageEventHandler(this.RKC_MessageEvent);
			m_RKC.Communication_Event += new Class_XRKC.MessageEventHandler(this.RKC_CommunicationEvent);
			m_Thread = new Thread(new ThreadStart(RunSequnece));
			m_Thread.IsBackground = true;
		}

		COM_SRZ_IO_List _SRZ_IO_List = null;
		[Category("SRZ Set"), Description("SRZ IO List 컴포넌트 연결"), DefaultValue(false)]
		public COM_SRZ_IO_List SRZ_IO_List
		{
			get { return _SRZ_IO_List; }
			set { _SRZ_IO_List = value; }
		}

		[Category("SRZ Port Setting"), Description("Port Name"), DefaultValue(false)]
		public string PortName
		{
			get { return m_RKC._Serial.PortName; }
			set { m_RKC._Serial.PortName = value; }
		}
		[Category("SRZ Port Setting"), Description("Baudrate"), DefaultValue(false)]
		public int BaudRate
		{
			get { return m_RKC._Serial.BaudRate; }
			set { m_RKC._Serial.BaudRate = value; }
		}

		private void RKC_MessageEvent(object sender, CJ_Controls.MessageEventArgs args)
		{
			LibTextOut("[RKC_ERROR] " + args.Text);
		}
		private void RKC_CommunicationEvent(object sender, CJ_Controls.MessageEventArgs args)
		{
			LibTextOut("[RKC_COMMUNICATION] " + args.Text);
		}

		public void Open()
		{
			if (SRZ_IO_List == null)
			{
				LogTextOut(string.Format("SRZ-{0} 에서 SRZ IO List 가 연결 되어있지 않습니다.", PortName));
				return;
			}

			_SeqItem = new SRZ_Seq_Item();
			m_RKC.Open(PortName, BaudRate);
		}
		public void Close()
		{
			m_RKC.Close();
			_SeqItem = new SRZ_Seq_Item();
		}
		public void Start()
		{
			m_Thread.Start();
		}

		private void RunSequnece()
		{
			while (true)
			{
				Thread.Sleep(20);

				if (m_RKC.IsOpen() == true)
				{
					Sequence_SRZ();
				}
				else
				{
					_SeqItem.SeqNum = 0;
					_SeqItem.CpuIndex = 0;
					_SeqItem.ModuleIndex = 0;

					SetAlarm( string.Format("SRZ Comport is not Opened! ({0})",m_RKC._Serial.PortName ));
				}
			}
		}

		private SRZ_Seq_Item _SeqItem;
		private void Sequence_SRZ()
		{
			if (SRZ_IO_List.SRZ_Cpu.Count <= 0)
				return;

			int nSeqNum = _SeqItem.SeqNum;
			int nRet = 0;
			string strBuf = "";

			float[] fGetDatas = _SeqItem.GetDatas;
			float[] fGetOutDatas = _SeqItem.GetOutDatas;
			int[] nGetDatas = _SeqItem.nGetDatas;
			int[] nSetDatas = _SeqItem.SetDatas;
			int[] nMagneticSetValue = _SeqItem.MagneticSetValue;
			switch (nSeqNum)
			{
				case 0:
					{ //INIT
						for (int i = 0; i < fGetDatas.Length; i++)
						{
							fGetDatas[i] = 0;
						}
						for (int i = 0; i < nSetDatas.Length; i++)
						{
							nSetDatas[i] = 0;
						}
						nSeqNum = 100;
					} break;
				case 100:
					{ //ATUO:리모트로 변경한다.
						nRet = m_RKC.SeqWriteData(_SeqItem.CpuIndex, "J1", ref nSetDatas, nSetDatas.Length, 1, 1);
						if (nRet > 0)
						{
							_SeqItem.CpuIndex += 1;
							if (_SeqItem.CpuIndex >= SRZ_IO_List.SRZ_Cpu.Count)
							{
								_SeqItem.CpuIndex = 0;
								nSeqNum = 200;
							}
						}
						else if (nRet < 0)
						{
							strBuf = string.Format("SRZ Auto Setting Error = {0}, cpu:{1}",nSeqNum, _SeqItem.CpuIndex);
							LogTextOut(strBuf);
							SetAlarm(strBuf);
						}
					} break;
				case 200:
					{ //RUN 시킨다.
						nRet = m_RKC.SeqSRZRun(_SeqItem.CpuIndex, true);
						if (nRet > 0)
						{
							_SeqItem.CpuIndex += 1;
							if (_SeqItem.CpuIndex >= SRZ_IO_List.SRZ_Cpu.Count)
							{
								_SeqItem.CpuIndex = 0;
								nSeqNum = 300;
							}
						}
						else if (nRet < 0)
						{
							strBuf = string.Format("Write Run Start Command Error = {0}, cpu:{1}", nSeqNum, _SeqItem.CpuIndex);
							LogTextOut(strBuf);
							SetAlarm(strBuf);
						}
					} break;
				case 300:// 상태 변경 체크 (TIO, DIO)
					{
						//DateTime dt = DateTime.Now;
						//TIO 변경 체크.
						if (_SeqItem.CpuIndex >= SRZ_IO_List.SRZ_Cpu.Count)
						{//끝..
							_SeqItem.CpuIndex = 0;
							// DIO 변경 체크
							nSeqNum = 900;
						}
						else
						{//셋팅..
							while (_SeqItem.CpuIndex < SRZ_IO_List.SRZ_Cpu.Count)
							{
								int srzDataIndex = 0;
								SRZ_CPU_Struct _CPU = SRZ_IO_List.SRZ_Cpu[_SeqItem.CpuIndex];
								if (IsTIO_Different(_CPU) == true)
								{
									for (int i = 0; i < nSetDatas.Length; i++)
									{
										nSetDatas[i] = 0;
									}

									for (int nModule = 0; nModule < _CPU.Module_List.Count; nModule++)
									{
										if (_CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.TIO_8888)
										{
											for (int nIO = 0; nIO < _CPU.Module_List[nModule].Set_Value.Count; nIO++)
											{
												nSetDatas[srzDataIndex++] = (int)_CPU.Module_List[nModule].Set_Value[nIO].Value;
											}
										}
									}
								}
								if (srzDataIndex != 0)
								{
									nSeqNum = 400;
									break;
								}
								else
								{
									_SeqItem.CpuIndex += 1;
								}
							}
						}
						//long nTime = (dt.Ticks - DateTime.Now.Ticks) / 10000;
						//Console.WriteLine(nTime);
					} break;
				case 400:
					{ //Set Temp
						nRet = m_RKC.SeqWriteData(_SeqItem.CpuIndex, "S1", ref nSetDatas, nSetDatas.Length, 1, 7);
						if (nRet > 0)
						{
							int srzDataIndex = 0;
							SRZ_CPU_Struct _CPU = SRZ_IO_List.SRZ_Cpu[_SeqItem.CpuIndex];
							for (int nModule = 0; nModule < _CPU.Module_List.Count; nModule++)
							{
								if (_CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.TIO_8888)
								{
									for (int nIO = 0; nIO < _CPU.Module_List[nModule].Set_Value.Count; nIO++)
									{
										if(nSetDatas.Length > srzDataIndex)
											_CPU.Module_List[nModule].Set_Value[nIO].CurValue = nSetDatas[srzDataIndex++];
									}
								}
							}

							_SeqItem.CpuIndex += 1;
							nSeqNum = 300;
						}
						else if (nRet < 0)
						{
							strBuf = string.Format("SRZ Write Command(S1) Error = {0}, cpu:{1}", nSeqNum, _SeqItem.CpuIndex);
							LogTextOut(strBuf);
							SetAlarm(strBuf);
						}
					} break;
				case 500:
					{ // SRZ Read Env and Cur Value 
						nRet = m_RKC.SeqReadData(_SeqItem.CpuIndex, "M1", ref fGetDatas);
						if (nRet > 0)
						{
							int srzDataIndex = 0;
							SRZ_CPU_Struct _CPU = SRZ_IO_List.SRZ_Cpu[_SeqItem.CpuIndex];

							for (int nModule = 0; nModule < _CPU.Module_List.Count; nModule++)
							{
								if (_CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.TIO_8888
									|| _CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.TIO_VVVV)
								{
									List<SRZ_IO> _Ios = _CPU.Module_List[nModule].Read_Value;
									for (int nIO = 0; nIO < _Ios.Count; nIO++)
									{
										_Ios[nIO].Value = fGetDatas[srzDataIndex++];
									}
								}
							}

							_SeqItem.CpuIndex += 1;
							if (_SeqItem.CpuIndex >= SRZ_IO_List.SRZ_Cpu.Count)
							{
								_SeqItem.CpuIndex = 0;
								nSeqNum = 600;
							}
						}
						else if (nRet < 0)
						{
							strBuf = string.Format("Read Command(Cur & ENV) Error = {0}, cpu:{1}", nSeqNum, _SeqItem.CpuIndex);
							LogTextOut(strBuf);
							SetAlarm(strBuf);
						}
					} break;
				case 600:
					{ // SRZ Read Out Value
						nRet = m_RKC.SeqReadData(_SeqItem.CpuIndex, "O1", ref fGetDatas);
						if (nRet > 0)
						{
							int srzDataIndex = 0;
							SRZ_CPU_Struct _CPU = SRZ_IO_List.SRZ_Cpu[_SeqItem.CpuIndex];
							for (int nModule = 0; nModule < _CPU.Module_List.Count; nModule++)
							{
								if (_CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.TIO_8888)
								{
									List<SRZ_IO> _Ios = _CPU.Module_List[nModule].Read_Out_Value;
									for (int nIO = 0; nIO < _Ios.Count; nIO++)
									{
										_Ios[nIO].Value = fGetDatas[srzDataIndex++];
									}
								}
							}

							_SeqItem.CpuIndex += 1;
							if (_SeqItem.CpuIndex >= SRZ_IO_List.SRZ_Cpu.Count)
							{
								_SeqItem.CpuIndex = 0;
								nSeqNum = 700;
							}
						}
						else if (nRet < 0)
						{
							strBuf = string.Format("Read Command(Out Value) Error = {0}, cpu:{1}", nSeqNum, _SeqItem.CpuIndex);
							LogTextOut(strBuf);
							SetAlarm(strBuf);
						}
					} break;
				case 700:
					{// Read IO 상단.
						nRet = m_RKC.SeqReadData(_SeqItem.CpuIndex, "L1", ref nGetDatas);
						if (nRet > 0)
						{
							try
							{
								int srzDataIndex = 0;
								string strTemp = "";
								SRZ_CPU_Struct _CPU = SRZ_IO_List.SRZ_Cpu[_SeqItem.CpuIndex];
								for (int nModule = 0; nModule < _CPU.Module_List.Count; nModule++)
								{
									if (_CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.DIO)
									{
										strTemp = string.Format("{0:d04}", nGetDatas[srzDataIndex++]);
										_CPU.Module_List[nModule].Read_Value[0].Value = (strTemp.Substring(3, 1) == "1") ? 1 : 0;
										_CPU.Module_List[nModule].Read_Value[1].Value = (strTemp.Substring(2, 1) == "1") ? 1 : 0;
										_CPU.Module_List[nModule].Read_Value[2].Value = (strTemp.Substring(1, 1) == "1") ? 1 : 0;
										_CPU.Module_List[nModule].Read_Value[3].Value = (strTemp.Substring(0, 1) == "1") ? 1 : 0;
									}
								}
							}
							catch// (System.Exception ex)
							{
								strBuf = string.Format("Read IO (L1) Exception Error = {0}, cpu:{1}", nSeqNum, _SeqItem.CpuIndex);
								LogTextOut(strBuf);
							}

							_SeqItem.CpuIndex += 1;
							if (_SeqItem.CpuIndex >= SRZ_IO_List.SRZ_Cpu.Count)
							{
								_SeqItem.CpuIndex = 0;
								nSeqNum = 800;
							}
						}
						else if (nRet < 0)
						{
							strBuf = string.Format("Read Command (L1) Error = {0}, cpu:{1}", nSeqNum, _SeqItem.CpuIndex);
							LogTextOut(strBuf);
							SetAlarm(strBuf);
						}
					} break;
				case 800:
					{// Read IO 하단.
						nRet = m_RKC.SeqReadData(_SeqItem.CpuIndex, "L6", ref nGetDatas);
						if (nRet > 0)
						{
							try
							{
								int srzDataIndex = 0;
								string strTemp = "";
								SRZ_CPU_Struct _CPU = SRZ_IO_List.SRZ_Cpu[_SeqItem.CpuIndex];
								for (int nModule = 0; nModule < _CPU.Module_List.Count; nModule++)
								{
									if (_CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.DIO)
									{
										strTemp = string.Format("{0:d04}", nGetDatas[srzDataIndex++]);
										_CPU.Module_List[nModule].Read_Value[4].Value = (strTemp.Substring(3, 1) == "1") ? 1 : 0;
										_CPU.Module_List[nModule].Read_Value[5].Value = (strTemp.Substring(2, 1) == "1") ? 1 : 0;
										_CPU.Module_List[nModule].Read_Value[6].Value = (strTemp.Substring(1, 1) == "1") ? 1 : 0;
										_CPU.Module_List[nModule].Read_Value[7].Value = (strTemp.Substring(0, 1) == "1") ? 1 : 0;
									}
								}
							}
							catch// (System.Exception ex)
							{
								strBuf = string.Format("Read IO (L6) Exception Error = {0}, cpu:{1}", nSeqNum, _SeqItem.CpuIndex);
								LogTextOut(strBuf);
							}

							_SeqItem.CpuIndex += 1;
							if (_SeqItem.CpuIndex >= SRZ_IO_List.SRZ_Cpu.Count)
							{
								_SeqItem.CpuIndex = 0;
								nSeqNum = 300;
							}
						}
						else if (nRet < 0)
						{
							strBuf = string.Format("Read Command (L6) Error = {0}, cpu:{1}", nSeqNum, _SeqItem.CpuIndex);
							LogTextOut(strBuf);
							SetAlarm(strBuf);
						}
					} break;
				case 900: //Write IO 체크
					{
						//DateTime dt = DateTime.Now;
						// DIO 변경 체크
						if (_SeqItem.CpuIndex >= SRZ_IO_List.SRZ_Cpu.Count)
						{//끝..
							_SeqItem.CpuIndex = 0;
							_SeqItem.ModuleIndex = 0;
							nSeqNum = 500;
						}
						else
						{
							while (_SeqItem.CpuIndex < SRZ_IO_List.SRZ_Cpu.Count)
							{
								SRZ_CPU_Struct _CPU = SRZ_IO_List.SRZ_Cpu[_SeqItem.CpuIndex];
								int nDioModule = IsDIO_Different(_CPU);
								if (nDioModule != 0)
								{
									if (CheckDIO_Write(ref nMagneticSetValue, true, nDioModule))
									{
										_SeqItem.ModuleIndex = nDioModule;
										nSeqNum = 1000;
										break;
									}
								}
								else
								{
									_SeqItem.CpuIndex += 1;
									_SeqItem.ModuleIndex = 0;
								}
							}
						}
						//long nTime = dt.Ticks - DateTime.Now.Ticks;
						//Console.WriteLine("DIO "+ nTime);
					} break;
				case 1000: //Q4
					{//Write 상단
						nRet = m_RKC.SeqWriteDataForDIO(_SeqItem.CpuIndex, "Q4", ref nMagneticSetValue, nMagneticSetValue.Length, _SeqItem.ModuleIndex, 7);
						if (nRet > 0)
						{
							SetDioCur(nMagneticSetValue, true, _SeqItem.ModuleIndex);
							if (CheckDIO_Write(ref nMagneticSetValue, false, _SeqItem.ModuleIndex))
							{
								nSeqNum = 1100;
							}
							else
							{
								nSeqNum = 900;
							}
						}
						else if (nRet < 0)
						{
							strBuf = string.Format("Write DIO (Q4) Error = {0}, cpu:{1}, module:{2}", nSeqNum, _SeqItem.CpuIndex,_SeqItem.ModuleIndex);
							LogTextOut(strBuf);
							SetAlarm(strBuf);
						}
					} break;
				case 1100:
					{//Write 하단
						nRet = m_RKC.SeqWriteDataForDIO(_SeqItem.CpuIndex, "Q5", ref nMagneticSetValue, nMagneticSetValue.Length, _SeqItem.ModuleIndex, 7);
						if (nRet > 0)
						{
							SetDioCur(nMagneticSetValue, false, _SeqItem.ModuleIndex);
							nSeqNum = 900;
						}
						else if (nRet < 0)
						{
							strBuf = string.Format("Write DIO (Q5) Error = {0}, cpu:{1}, module:{2}", nSeqNum, _SeqItem.CpuIndex, _SeqItem.ModuleIndex);
							LogTextOut(strBuf);
							SetAlarm(strBuf);
						}
					} break;
				default:
					break;
			}
			_SeqItem.SeqNum = nSeqNum;
		}

		private bool IsTIO_Different(SRZ_CPU_Struct _CPU)
		{
			bool bRtn = false;
			for (int nModule = 0; nModule < _CPU.Module_List.Count; nModule++)
			{
				if (_CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.TIO_8888)
				{
					if (_CPU.Module_List[nModule].IsDifferent() == true)
					{
						bRtn = true;
						break;
					}
				}
			}
			return bRtn;
		}
		private int IsDIO_Different(SRZ_CPU_Struct _CPU)
		{
			int nRtn = 0;
			int nDioModuleCnt = 0;
			for (int nModule = 0; nModule < _CPU.Module_List.Count; nModule++)
			{
				if (_CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.DIO)
				{
					nDioModuleCnt += 1;
					if (_CPU.Module_List[nModule].IsDifferent() == true)
					{
						nRtn = nDioModuleCnt;
						break;
					}
				}
			}
			return nRtn;
		}
		private void SetDioCur(int[] nMagneticSetValue, bool bTop, int nDioModule)
		{
			int nModuleIndex = 0;
			SRZ_CPU_Struct _CPU = SRZ_IO_List.SRZ_Cpu[_SeqItem.CpuIndex];
			for (int nModule = 0; nModule < _CPU.Module_List.Count; nModule++)
			{
				if (_CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.DIO)
				{
					nModuleIndex += 1;
				}
				else
					continue;

				if (nDioModule == nModuleIndex)
				{
					if (bTop == true)
					{
						_CPU.Module_List[nModule].Set_Value[0].CurValue = nMagneticSetValue[3];
						_CPU.Module_List[nModule].Set_Value[1].CurValue = nMagneticSetValue[2];
						_CPU.Module_List[nModule].Set_Value[2].CurValue = nMagneticSetValue[1];
						_CPU.Module_List[nModule].Set_Value[3].CurValue = nMagneticSetValue[0];
					}
					else
					{
						_CPU.Module_List[nModule].Set_Value[4].CurValue = nMagneticSetValue[3];
						_CPU.Module_List[nModule].Set_Value[5].CurValue = nMagneticSetValue[2];
						_CPU.Module_List[nModule].Set_Value[6].CurValue = nMagneticSetValue[1];
						_CPU.Module_List[nModule].Set_Value[7].CurValue = nMagneticSetValue[0];
					}
					break;
				}
			}
		}
		private bool CheckDIO_Write(ref int[] nMagneticSetValue, bool bTop, int nDioModule)
		{
			bool bRtn = false;
			int nModuleIndex = 0;
			SRZ_CPU_Struct _CPU = SRZ_IO_List.SRZ_Cpu[_SeqItem.CpuIndex];
			for (int nModule = 0; nModule < _CPU.Module_List.Count; nModule++)
			{
				if (_CPU.Module_List[nModule].IO_Type == SRZ_IO_TYPE.DIO)
				{
					nModuleIndex += 1;
				}
				else
					continue;

				if (nDioModule == nModuleIndex)
				{
					if (bTop == true)
					{
						nMagneticSetValue[3] = _CPU.Module_List[nModule].Set_Value[0].Value == 0 ? 0 : 1;
						nMagneticSetValue[2] = _CPU.Module_List[nModule].Set_Value[1].Value == 0 ? 0 : 1;
						nMagneticSetValue[1] = _CPU.Module_List[nModule].Set_Value[2].Value == 0 ? 0 : 1;
						nMagneticSetValue[0] = _CPU.Module_List[nModule].Set_Value[3].Value == 0 ? 0 : 1;
					}
					else
					{
						nMagneticSetValue[3] = _CPU.Module_List[nModule].Set_Value[4].Value == 0 ? 0 : 1;
						nMagneticSetValue[2] = _CPU.Module_List[nModule].Set_Value[5].Value == 0 ? 0 : 1;
						nMagneticSetValue[1] = _CPU.Module_List[nModule].Set_Value[6].Value == 0 ? 0 : 1;
						nMagneticSetValue[0] = _CPU.Module_List[nModule].Set_Value[7].Value == 0 ? 0 : 1;
					}

					bRtn = true;
					break;
				}
			}

			return bRtn;
		}
	}

	public class SRZ_Seq_Item
	{
		private int m_SeqNum = 0;
		private float[] m_GetDatas = new float[64];
		private float[] m_GetOutDatas = new float[64];
		private int[] m_nGetData = new int[64];
		private int[] m_SetDatas = new int[64];
		private int[] m_MagneticSetValue = new int[4];
		private ushort m_nCpuIndex = 0; //0부터 시작
		private int m_nModuleIndex = 0; //1부터 시작
		public float[] GetDatas
		{
			get { return m_GetDatas; }
		}
		public int[] nGetDatas
		{
			get { return m_nGetData; }
		}
		public float[] GetOutDatas
		{
			get { return m_GetOutDatas; }
		}

		public int[] SetDatas
		{
			get { return m_SetDatas; }
		}
		public int[] MagneticSetValue
		{
			get { return m_MagneticSetValue; }
		}
		public int SeqNum
		{
			get { return m_SeqNum; }
			set { m_SeqNum = value; }
		}
		public ushort CpuIndex
		{
			get { return m_nCpuIndex; }
			set { m_nCpuIndex = value; }
		}
		public int ModuleIndex
		{
			get { return m_nModuleIndex; }
			set { m_nModuleIndex = value; }
		}
	}
}
