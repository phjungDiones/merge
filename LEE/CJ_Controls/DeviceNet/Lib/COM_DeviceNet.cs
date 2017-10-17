using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using System.Threading;
using System.ComponentModel;
using CJ_Controls;

namespace CJ_Controls.DeviceNet
{
	public class COM_DeviceNet : Component
	{
		#region Log 보내기
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public event MessageEventHandler MessageEvent;

		public void LogTextOut(string message)
		{
			if (MessageEvent != null)
				MessageEvent(this, new MessageEventArgs(message));
		}
		#endregion

		COM_DeviceNet_IO_List _DNet_IO_List = null;
		[Category("DeviceNet_Set"), Description("Device Net IO List 컴포넌트 연결"), DefaultValue(false)]
		public COM_DeviceNet_IO_List DNet_IO_List
		{
			get { return _DNet_IO_List; }
			set { _DNet_IO_List = value; }
		}


        #region 싱글톤 멤버

        // 싱글톤 패턴 인스턴스 생성
        public static COM_DeviceNet Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (lockObj)
                    {
                        _Instance = new COM_DeviceNet();
                    }
                }
                return _Instance;
            }
        }
       private static object lockObj = new object();
       private static COM_DeviceNet _Instance = null;

        #endregion

        //테스트
        public bool bTestFalg;

		private XHilscher xHilscher = null;
		public COM_DeviceNet()
		{
			xHilscher = new XHilscher(this);
		}

		public void ReadAll_and_Matching()
		{
			try
			{
				if (IsOpen() == false)
					return;

				ReadAllInput();
				ReadAllOutput();

				if (DNet_IO_List == null)
					return;

				for (int nAdapter = 0; nAdapter < DNet_IO_List.DNet_Adapter.Count; nAdapter++)
				{
					for (int nModule = 0; nModule < DNet_IO_List.DNet_Adapter[nAdapter].Module_List.Count; nModule++)
					{
						for (int nIo = 0; nIo < DNet_IO_List.DNet_Adapter[nAdapter].Module_List[nModule].IO_List.Count; nIo++)
						{
							DeviceNetIO _IO = DNet_IO_List.DNet_Adapter[nAdapter].Module_List[nModule].IO_List[nIo];
							switch(_IO.IO_Type)
							{
								case DNET_IO_TYPE.D_INPUT: 
								{
									ReadBit(_IO);
								}break;
								case DNET_IO_TYPE.D_OUTPUT:
								{
									ReadOutBit(_IO);
								}break;
								case DNET_IO_TYPE.A_INPUT:
								{
									ReadInput(_IO);
								}break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogTextOut(ex.Message);
			}
		}
		private void Write_AnalogIO_ConfigSet()
		{
			for (int nAdapter = 0; nAdapter < DNet_IO_List.DNet_Adapter.Count; nAdapter++)
			{
				for (int nModule = 0; nModule < DNet_IO_List.DNet_Adapter[nAdapter].Module_List.Count; nModule++)
				{
					if (DNet_IO_List.DNet_Adapter[nAdapter].Module_List[nModule].IO_Type != DNET_IO_TYPE.A_INPUT)
						continue; //아날로그 아이오만 하면 된다.

					for (int nIo = 0; nIo < DNet_IO_List.DNet_Adapter[nAdapter].Module_List[nModule].IO_List.Count; nIo++)
					{
						DeviceNetIO _IO = DNet_IO_List.DNet_Adapter[nAdapter].Module_List[nModule].IO_List[nIo];
						//WriteAnalog_Set(_IO.SubAddr);
					}
				}
			}
		}

		public void Open()
		{
			if (DNet_IO_List == null)
				return;

			//if(xHilscher.Open(1000, 1000)>=0)//DNet_IO_List.GetMaxInputCnt(), DNet_IO_List.GetMaxOutputCnt()) >= 0)
            if (xHilscher.Open(1000, 1000) >= 0)//DNet_IO_List.GetMaxInputCnt(), DNet_IO_List.GetMaxOutputCnt()) >= 0)
			{
				Write_AnalogIO_ConfigSet();
			}
		}
		public void Close()
		{
			xHilscher.Close();
		}
		public bool IsOpen()
		{
			return xHilscher.IsOpen();
		}
		public int ReadAllInput()
		{
			return xHilscher.ReadAllInput();
		}
		public int ReadAllOutput()
		{
			return xHilscher.ReadAllOutput();
		}
		public bool ReadBit(ushort usAddress, ushort usReadBit)
		{
			return xHilscher.ReadBit(usAddress, usReadBit);
		}
		public bool ReadBit(DeviceNetIO IO)
		{
			IO.IsOn = xHilscher.ReadBit(IO.Address, IO.SubAddr);
            //IO.IsOn = true;
			return IO.IsOn;
		}
		public bool ReadOutBit(ushort usAddress, ushort usReadData)
		{
			return xHilscher.ReadOutBit(usAddress, usReadData);
		}
		public bool ReadOutBit(DeviceNetIO IO)
		{
			IO.IsOn = xHilscher.ReadOutBit(IO.Address, IO.SubAddr);
            //IO.IsOn = true;
			return IO.IsOn;
		}
		public int WriteBit(ushort usAddress, ushort usWriteData, bool bOn)
		{
			return xHilscher.WriteBit(usAddress, usWriteData, bOn);
		}
		public int WriteBit(DeviceNetIO IO, bool bOn)
		{
			return xHilscher.WriteBit(IO.Address, IO.SubAddr, bOn);
		}
		public int WriteAnalog_Set(ushort usAddress)
		{
			ushort[] usValue = { 0xFFFF };
			return xHilscher.WriteOutput(usAddress, usValue, 0);
		}
		public ushort ReadAnalogOut(ushort usAddress)
		{
			return xHilscher.GetReadOut(usAddress);
		}
		public ushort ReadInput(DeviceNetIO IO)
		{
			ushort[] _Value = new ushort[1];
			xHilscher.ReadInput(IO.Address, _Value, 0);
			IO.AnalogValue = (short)_Value[0];
			return _Value[0];
		}
	}
}
