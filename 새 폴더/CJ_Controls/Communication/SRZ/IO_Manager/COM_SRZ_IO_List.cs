using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace CJ_Controls.Communication.SRZ
{
	public class COM_SRZ_IO_List : Component
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

		public COM_SRZ_IO_List()
		{
			LoadSrzIOList();
		}

		private string IO_FILE_NAME = "SRZ_IO.xml";
		private List<SRZ_CPU_Struct> _SrzCpu = new List<SRZ_CPU_Struct>();
		internal List<SRZ_CPU_Struct> SRZ_Cpu
		{
			get { return _SrzCpu; }
			set { _SrzCpu = value; }
		}

		private void LoadSrzIOList()
		{
			try
			{
				using (FileStream fs = new FileStream(IO_FILE_NAME, FileMode.Open))
				{
					XmlSerializer xs = new XmlSerializer(typeof(List<SRZ_CPU_Struct>));
					_SrzCpu = xs.Deserialize(fs) as List<SRZ_CPU_Struct>;
					Refresh_IO_Name();
					LogTextOut("SRZ IO List Load Success!");
				}
			}
			catch (System.Exception ex)
			{
				LogTextOut(ex.Message);
			}
		}
		public void SaveSrzIOList()
		{
			try
			{
				if (File.Exists(IO_FILE_NAME))
					File.Delete(IO_FILE_NAME);

				using (FileStream fs = new FileStream(IO_FILE_NAME, FileMode.Create))
				{
					XmlSerializer xs = new XmlSerializer(typeof(List<SRZ_CPU_Struct>));
					Refresh_IO_Name();
					xs.Serialize(fs, _SrzCpu);
					LogTextOut("SRZ IO List Save Success!");
				}
			}
			catch (System.Exception ex)
			{
				LogTextOut(ex.Message);
			}
		}

		public void Refresh_IO_Name()
		{
			int nSpareCount = 0;
			string strIO_Name = "";
			for (int nCntCpu = 0; nCntCpu < SRZ_Cpu.Count; nCntCpu++)
			{
				for (int nCntModule = 0; nCntModule < SRZ_Cpu[nCntCpu].Module_List.Count; nCntModule++)
				{
					switch (SRZ_Cpu[nCntCpu].Module_List[nCntModule].IO_Type)
					{
						case SRZ_IO_TYPE.TIO_8888:
							{
								foreach (SRZ_IO _Io in SRZ_Cpu[nCntCpu].Module_List[nCntModule].Read_Value)
								{
									if (_Io.IO_Name == "" || _Io.IO_Name.IndexOf("SPARE") >= 0)
									{
										strIO_Name = string.Format("{0}_{1}_SPARE{2:d03}", _Io.IO_Type.ToString(), "CUR", ++nSpareCount);
										_Io.IO_Name = strIO_Name;
									}
								}
								foreach (SRZ_IO _Io in SRZ_Cpu[nCntCpu].Module_List[nCntModule].Set_Value)
								{
									if (_Io.IO_Name == "" || _Io.IO_Name.IndexOf("SPARE") >= 0)
									{
										strIO_Name = string.Format("{0}_{1}_SPARE{2:d03}", _Io.IO_Type.ToString(), "SET", ++nSpareCount);
										_Io.IO_Name = strIO_Name;
									}
								}
								foreach (SRZ_IO _Io in SRZ_Cpu[nCntCpu].Module_List[nCntModule].Read_Out_Value)
								{
									if (_Io.IO_Name == "" || _Io.IO_Name.IndexOf("SPARE") >= 0)
									{
										strIO_Name = string.Format("{0}_{1}_SPARE{2:d03}", _Io.IO_Type.ToString(), "OUT", ++nSpareCount);
										_Io.IO_Name = strIO_Name;
									}
								}
							} break;
						case SRZ_IO_TYPE.TIO_VVVV:
							{
								foreach (SRZ_IO _Io in SRZ_Cpu[nCntCpu].Module_List[nCntModule].Read_Value)
								{
									if (_Io.IO_Name == "" || _Io.IO_Name.IndexOf("SPARE") >= 0)
									{
										strIO_Name = string.Format("{0}_{1}_SPARE{2:d03}", _Io.IO_Type.ToString(), "ENV", ++nSpareCount);
										_Io.IO_Name = strIO_Name;
									}
								}
							} break;
						case SRZ_IO_TYPE.DIO:
							{
								foreach (SRZ_IO _Io in SRZ_Cpu[nCntCpu].Module_List[nCntModule].Set_Value)
								{
									if (_Io.IO_Name == "" || _Io.IO_Name.IndexOf("SPARE") >= 0)
									{
										strIO_Name = string.Format("{0}_{1}_SPARE{2:d03}", _Io.IO_Type.ToString(), "SET", ++nSpareCount);
										_Io.IO_Name = strIO_Name;
									}
								}
								foreach (SRZ_IO _Io in SRZ_Cpu[nCntCpu].Module_List[nCntModule].Read_Value)
								{
									if (_Io.IO_Name == "" || _Io.IO_Name.IndexOf("SPARE") >= 0)
									{
										strIO_Name = string.Format("{0}_{1}_SPARE{2:d03}", _Io.IO_Type.ToString(), "READ", ++nSpareCount);
										_Io.IO_Name = strIO_Name;
									}
								}
							} break;
					}
				}
			}
		}
		public SRZ_IO GetLinkData(string strIoName, SRZ_IO_TYPE type)
		{
			SRZ_IO Rtn = null;
			for (int nCpu = 0; nCpu < SRZ_Cpu.Count; nCpu++)
			{
				for (int nModule = 0; nModule < SRZ_Cpu[nCpu].Module_List.Count; nModule++)
				{
					if (SRZ_Cpu[nCpu].Module_List[nModule].IO_Type != type)
						continue;

					for (int nIO = 0; nIO < SRZ_Cpu[nCpu].Module_List[nModule].Read_Value.Count; nIO++)
					{
						SRZ_IO _IO = SRZ_Cpu[nCpu].Module_List[nModule].Read_Value[nIO];
						if (_IO.IO_Name.Equals(strIoName) && _IO.IO_Type.Equals(type))
						{
							Rtn = _IO;
							break;
						}
					}
					if (Rtn != null)
						break;

					for (int nIO = 0; nIO < SRZ_Cpu[nCpu].Module_List[nModule].Set_Value.Count; nIO++)
					{
						SRZ_IO _IO = SRZ_Cpu[nCpu].Module_List[nModule].Set_Value[nIO];
						if (_IO.IO_Name.Equals(strIoName) && _IO.IO_Type.Equals(type))
						{
							Rtn = _IO;
							break;
						}
					}
					if (Rtn != null)
						break;

					for (int nIO = 0; nIO < SRZ_Cpu[nCpu].Module_List[nModule].Read_Out_Value.Count; nIO++)
					{
						SRZ_IO _IO = SRZ_Cpu[nCpu].Module_List[nModule].Read_Out_Value[nIO];
						if (_IO.IO_Name.Equals(strIoName) && _IO.IO_Type.Equals(type))
						{
							Rtn = _IO;
							break;
						}
					}
					if (Rtn != null)
						break;
				}
				if (Rtn != null)
					break;
			}
			return Rtn;
		}
		public SRZ_IO GetLinkData(string strIoName, int nCpuIndex, SRZ_IO_TYPE type)
		{
			SRZ_IO Rtn = null;
			for (int nCpu = 0; nCpu < SRZ_Cpu.Count; nCpu++)
			{
				if (nCpu != nCpuIndex)
					continue;

				for (int nModule = 0; nModule < SRZ_Cpu[nCpu].Module_List.Count; nModule++)
				{
					if (SRZ_Cpu[nCpu].Module_List[nModule].IO_Type != type)
						continue;

					for (int nIO = 0; nIO < SRZ_Cpu[nCpu].Module_List[nModule].Read_Value.Count; nIO++)
					{
						SRZ_IO _IO = SRZ_Cpu[nCpu].Module_List[nModule].Read_Value[nIO];
						if (_IO.IO_Name.Equals(strIoName) && _IO.IO_Type.Equals(type))
						{
							Rtn = _IO;
							break;
						}
					}
					if (Rtn != null)
						break;

					for (int nIO = 0; nIO < SRZ_Cpu[nCpu].Module_List[nModule].Set_Value.Count; nIO++)
					{
						SRZ_IO _IO = SRZ_Cpu[nCpu].Module_List[nModule].Set_Value[nIO];
						if (_IO.IO_Name.Equals(strIoName) && _IO.IO_Type.Equals(type))
						{
							Rtn = _IO;
							break;
						}
					}
					if (Rtn != null)
						break;

					for (int nIO = 0; nIO < SRZ_Cpu[nCpu].Module_List[nModule].Read_Out_Value.Count; nIO++)
					{
						SRZ_IO _IO = SRZ_Cpu[nCpu].Module_List[nModule].Read_Out_Value[nIO];
						if (_IO.IO_Name.Equals(strIoName) && _IO.IO_Type.Equals(type))
						{
							Rtn = _IO;
							break;
						}
					}
					if (Rtn != null)
						break;
				}
				if (Rtn != null)
					break;
			}
			return Rtn;
		}
		public void IO_Maker_View()
		{
			Form_SRZ_IO_Maker dlg = new Form_SRZ_IO_Maker();
			dlg.IO_List_DataSet = this;
			dlg.ShowDialog();
		}
	}

	[Serializable]
	public class SRZ_CPU_Struct
	{
		public SRZ_CPU_Struct()
		{
		}
		public List<SRZ_Module_Struct> Module_List = new List<SRZ_Module_Struct>();
	}

	[Serializable]
	public class SRZ_Module_Struct
	{
		public SRZ_Module_Struct()
		{
		}

		public SRZ_IO_TYPE IO_Type = SRZ_IO_TYPE.TIO_8888;
		public List<SRZ_IO> Read_Value = new List<SRZ_IO>(); //TIO, DIO
		public List<SRZ_IO> Set_Value = new List<SRZ_IO>(); //TIO, DIO
		public List<SRZ_IO> Read_Out_Value = new List<SRZ_IO>(); //TIO

		public SRZ_Module_Struct(SRZ_IO_TYPE _Type)
		{
			IO_Type = _Type;
			if (_Type == SRZ_IO_TYPE.TIO_8888)
			{
				for (int i = 0; i < 4; i++)
				{
					Read_Value.Add(new SRZ_IO(_Type));
					Set_Value.Add(new SRZ_IO(_Type));
					Read_Out_Value.Add(new SRZ_IO(_Type));
				}
			}
			else if (_Type == SRZ_IO_TYPE.TIO_VVVV)
			{
				for (int i = 0; i < 4; i++)
				{
					Read_Value.Add(new SRZ_IO(_Type));
				}
			}
			else
			{
				for (int i = 0; i < 8; i++)
				{
					Read_Value.Add(new SRZ_IO(_Type));
					Set_Value.Add(new SRZ_IO(_Type));
				}
			}
		}
		public bool IsDifferent()
		{
			bool bRtn = false;
			for (int i = 0; i < Set_Value.Count; i++)
			{
				if (Set_Value[i].Value != Set_Value[i].CurValue)
				{
					bRtn = true;
					break;
				}
			}
			return bRtn;
		}
	}
}
