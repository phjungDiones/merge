using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using CJ_Controls;

namespace CJ_Controls.DeviceNet
{
	public class COM_DeviceNet_IO_List : Component
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

		public COM_DeviceNet_IO_List()
		{
			LoadDeviceNetIOList();
		}

		private ushort _Max_Input = 0;
		private ushort _Max_Output = 0;
		public ushort GetMaxInputCnt()
		{
			return _Max_Input;
		}
		public ushort GetMaxOutputCnt()
		{
			return _Max_Output;
		}
		private string IO_FILE_NAME = "DeviceNet_IO.xml";
		private List<DNet_Adapter_Struct> _Adapter = new List<DNet_Adapter_Struct>();
		internal List<DNet_Adapter_Struct> DNet_Adapter
		{
			get { return _Adapter; }
			set { _Adapter = value; }
		}
		private void LoadDeviceNetIOList()
		{
			try
			{
				using (FileStream fs = new FileStream(IO_FILE_NAME, FileMode.Open))
				{
					XmlSerializer xs = new XmlSerializer(typeof(List<DNet_Adapter_Struct>));
					_Adapter = xs.Deserialize(fs) as List<DNet_Adapter_Struct>;
					RefreshAddress(ref _Max_Input, ref _Max_Output);
					LogTextOut("DNet IO List Load Success!");
				}
			}
			catch (System.Exception ex)
			{
				LogTextOut(ex.Message);
			}
		}
		public void SaveDeviceNetIOList()
		{
			try
			{
				if (File.Exists(IO_FILE_NAME))
					File.Delete(IO_FILE_NAME);

				using (FileStream fs = new FileStream(IO_FILE_NAME, FileMode.Create))
				{
					XmlSerializer xs = new XmlSerializer(typeof(List<DNet_Adapter_Struct>));
					xs.Serialize(fs, _Adapter);
					LogTextOut("DNet IO List Save Success!");
				}
			}
			catch (System.Exception ex)
			{
				LogTextOut(ex.Message);
			}
		}
		int nSpare = 1;
		public void RefreshAddress(ref ushort usInputMax, ref ushort usOutputMax)
		{
			//ushort nInputAddr = 0;
            ushort nInputAddr = 1;
            //ushort nInputAddr = 0;
			ushort nOutputAddr = 0;
			nSpare = 1;
			for (int nCntAdapter = 0; nCntAdapter < DNet_Adapter.Count; nCntAdapter++)
			{
				//nInputAddr += 2; //디지털 인풋은 아답터 정보때매 2Word를 차지한다.
				for (int nCntModule = 0; nCntModule < DNet_Adapter[nCntAdapter].Module_List.Count; nCntModule++)
				{

					//Todo
					if (DNet_Adapter[nCntAdapter].Module_List[nCntModule].IO_Type == DNET_IO_TYPE.D_INPUT
						|| DNet_Adapter[nCntAdapter].Module_List[nCntModule].IO_Type == DNET_IO_TYPE.D_OUTPUT)
					{//Digital 신호
						DNET_IO_TYPE _CurIO_Type = DNet_Adapter[nCntAdapter].Module_List[nCntModule].IO_Type;
						for (int nCntIO = 0; nCntIO < DNet_Adapter[nCntAdapter].Module_List[nCntModule].IO_List.Count; nCntIO++)
						{
							DeviceNetIO _CurIO = DNet_Adapter[nCntAdapter].Module_List[nCntModule].IO_List[nCntIO];


							//_CurIO.SubAddr = (ushort)(1 << nCntIO);

							if (_CurIO.IO_Name == "" || _CurIO.IO_Name.IndexOf("SPARE_") >= 0)
							{
								_CurIO.IO_Name = string.Format("SPARE_{0:d04}", nSpare++);
							}
						}
						//nInputAddr += 2;
						//nOutputAddr += 2;
                        
					}
					else
					{//Analog 신호
						//아날로그 모듈은, 인풋이 9개, 아웃풋이 6개로,,, 각각 2워드를 차지한다.
						DNET_IO_TYPE _CurIO_Type = DNet_Adapter[nCntAdapter].Module_List[nCntModule].IO_Type;
						for (int nCntIO = 0; nCntIO < DNet_Adapter[nCntAdapter].Module_List[nCntModule].IO_List.Count; nCntIO++)
						{
							DeviceNetIO _CurIO = DNet_Adapter[nCntAdapter].Module_List[nCntModule].IO_List[nCntIO];
							//_CurIO.Address = nInputAddr;
							//_CurIO.SubAddr = nOutputAddr;

							if (_CurIO.IO_Name == "" || _CurIO.IO_Name.IndexOf("SPARE_") >= 0)
							{
								_CurIO.IO_Name = string.Format("SPARE_{0:d04}", nSpare++);
							}

							//nInputAddr += 2;
						}
						//nOutputAddr += 12; //아날로그 아웃풋 영역은 6개니깐,, 고정으로 12워드를 차지한다. 단 첫번째 어드래스는,, 각 인풋영역의 셋팅값으로 활용된다.
					}
				}
			}
			usInputMax = nInputAddr;
			usOutputMax = nOutputAddr;
		}
		public void IO_Maker_View()
		{
			Form_IO_List_Maker dlg = new Form_IO_List_Maker();
			dlg.IO_List_DataSet = this;
			dlg.ShowDialog();
		}

		// IO 정보 리턴.
		public DeviceNetIO GetLinkData(string strIoName, DNET_IO_TYPE type)
		{
			DeviceNetIO Rtn = null;
			for(int nAdapter = 0; nAdapter < DNet_Adapter.Count; nAdapter++)
			{
				for(int nModule = 0; nModule < DNet_Adapter[nAdapter].Module_List.Count; nModule++)
				{
					if (DNet_Adapter[nAdapter].Module_List[nModule].IO_Type != type)
						continue;

					for(int nIO = 0; nIO < DNet_Adapter[nAdapter].Module_List[nModule].IO_List.Count; nIO++)
					{
						DeviceNetIO _IO = DNet_Adapter[nAdapter].Module_List[nModule].IO_List[nIO];
						if (_IO.IO_Name.Equals(strIoName) && _IO.IO_Type.Equals(type))
						{
							Rtn = _IO;
							break;
						}
					}
					if(Rtn != null)
						break;
				}
				if(Rtn != null)
					break;
			}
			return Rtn;
		}
		public DeviceNetIO GetLinkData(string strIoName, int nAdapterIndex, DNET_IO_TYPE type)
		{
			DeviceNetIO Rtn = null;

			for (int nAdapter = 0; nAdapter < DNet_Adapter.Count; nAdapter++)
			{
				if (nAdapterIndex != nAdapter) //속도때매,, 원하는 아답터에서만 검색.
					continue;

				for (int nModule = 0; nModule < DNet_Adapter[nAdapter].Module_List.Count; nModule++)
				{
					if (DNet_Adapter[nAdapter].Module_List[nModule].IO_Type != type)
						continue;

					for (int nIO = 0; nIO < DNet_Adapter[nAdapter].Module_List[nModule].IO_List.Count; nIO++)
					{
						DeviceNetIO _IO = DNet_Adapter[nAdapter].Module_List[nModule].IO_List[nIO];
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
	}

	[Serializable]
	public class DNet_Adapter_Struct
	{
		public DNet_Adapter_Struct()
		{
		}
		public List<DNet_Module_Struct> Module_List = new List<DNet_Module_Struct>();
	}

	[Serializable]
	public class DNet_Module_Struct
	{
		public DNet_Module_Struct()
		{
		}
		public DNet_Module_Struct(DNET_IO_TYPE type)
		{
			IO_Type = type;
			if (type == DNET_IO_TYPE.D_INPUT || type == DNET_IO_TYPE.D_OUTPUT)
			{//디지털 신호
				for (int i = 0; i < 16; i++)
				{
					IO_List.Add(new DeviceNetIO(type));
				}
			}
			else
			{//아날로그 신호
				for (int i = 0; i < 9; i++)
				{
					IO_List.Add(new DeviceNetIO(type));
				}
			}
		}
		public DNET_IO_TYPE IO_Type = DNET_IO_TYPE.D_INPUT;
		public List<DeviceNetIO> IO_List = new List<DeviceNetIO>();
	}
}
