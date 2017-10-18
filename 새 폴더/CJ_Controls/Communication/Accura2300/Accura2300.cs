using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace CJ_Controls.Communication.Accura2300
{
	public class Accura2300
	{
		private ushort _TransactionID = 0;
		private Socket _AccuraSocket;
		private byte[] _RecvData = new byte[125];
		private DateTime _RecvTime = DateTime.MaxValue;
		private VoltageValue _VoltageValueStruct = new VoltageValue();
		private AllVoltageValue _AllVoltageValueStruct = new AllVoltageValue();

		public AllVoltageValue AllVoltageValueStruct
		{
			get { return _AllVoltageValueStruct; }
		}

		public VoltageValue VoltageValueStruct
		{
			get { return _VoltageValueStruct; }
		}

		public Accura2300()
		{
			_AccuraSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		public bool ConnectSocket(string ip, int portNo)
		{
			IPAddress ipAddress = IPAddress.Parse(ip);
			try
			{
				_AccuraSocket.Connect(ipAddress, portNo);
			}
			catch (Exception e)
			{
				string ex = e.ToString();
			}
			return _AccuraSocket.Connected;
		}

		public bool IsConnect
		{
			get { return _AccuraSocket.Connected; }
		}

		private ushort SetTransactionID()
		{
			if (_TransactionID == 0xFFFF)
			{
				_TransactionID = 0;
			}
			else
			{
				_TransactionID++;
			}
			return _TransactionID;
		}

		private byte[] UShortToByte(ushort data)
		{
			byte[] value = new byte[2];
			value[1] = (byte)(data & 0xff);
			value[0] = (byte)((data >> 8) & 0xff);
			return value;
		}

		private ReadHoldingResponse GetResponseData(byte[] buf)
		{
			ReadHoldingResponse responseData = new ReadHoldingResponse();

			return responseData;
		}

		private long GetElapseTime(DateTime dtTime)
		{
			return ((DateTime.Now.Ticks - dtTime.Ticks) / 10000);
		}

		private byte[] getBytes(object requestData)
		{
			int size = Marshal.SizeOf(requestData);
			byte[] arr = new byte[size];
			IntPtr ptr = Marshal.AllocHGlobal(size);

			Marshal.StructureToPtr(requestData, ptr, true);
			Marshal.Copy(ptr, arr, 0, size);
			Marshal.FreeHGlobal(ptr);

			//Array.Reverse(arr);
			return arr;
		}

		private VoltageValue VoltageValuefromBytes(byte[] arr, int size)
		{
			VoltageValue str = new VoltageValue();
			IntPtr ptr = Marshal.AllocHGlobal(size);
			SwapIt(typeof(VoltageValue), arr, 0);
			Marshal.Copy(arr, 0, ptr, size);
			str = (VoltageValue)Marshal.PtrToStructure(ptr, str.GetType());
			Marshal.FreeHGlobal(ptr);

			//str.VoltageVan = ChangeEndian_32Bit(str.VoltageVan);
			//str.VoltageVbn = ChangeEndian_32Bit(str.VoltageVbn);
			//str.VoltageVcn = ChangeEndian_32Bit(str.VoltageVcn);
			//str.VoltageVavg = ChangeEndian_32Bit(str.VoltageVavg);

			return str;
		}

		private AllVoltageValue AllVoltageValuefromBytes(byte[] arr, int size)
		{
			AllVoltageValue str = new AllVoltageValue();
			IntPtr ptr = Marshal.AllocHGlobal(size);
			SwapIt(typeof(VoltageValue), arr, 0);
			Marshal.Copy(arr, 0, ptr, size);
			str = (AllVoltageValue)Marshal.PtrToStructure(ptr, str.GetType());
			Marshal.FreeHGlobal(ptr);

			//str.VoltageVan = ChangeEndian_32Bit(str.VoltageVan);
			//str.VoltageVbn = ChangeEndian_32Bit(str.VoltageVbn);
			//str.VoltageVcn = ChangeEndian_32Bit(str.VoltageVcn);
			//str.VoltageVavg = ChangeEndian_32Bit(str.VoltageVavg);

			return str;
		}

		private ReadHoldingError ErrorfromBytes(byte[] arr, int size)
		{
			ReadHoldingError str = new ReadHoldingError();
			IntPtr ptr = Marshal.AllocHGlobal(size);
			Marshal.Copy(arr, 0, ptr, size);
			str = (ReadHoldingError)Marshal.PtrToStructure(ptr, str.GetType());
			Marshal.FreeHGlobal(ptr);

			return str;
		}
		public static void SwapIt(Type type, byte[] recvbyte, int offset)
		{
			foreach (System.Reflection.FieldInfo fi in type.GetFields())
			{
				int index = Marshal.OffsetOf(type, fi.Name).ToInt32() + offset;
				if (fi.FieldType == typeof(int))
				{
					Array.Reverse(recvbyte, index, sizeof(int));
				}
				if (fi.FieldType == typeof(short))
				{
					Array.Reverse(recvbyte, index, sizeof(short));
				}
				if (fi.FieldType == typeof(ushort))
				{
					Array.Reverse(recvbyte, index, sizeof(ushort));
				}
				else if (fi.FieldType == typeof(float))
				{
					Array.Reverse(recvbyte, index, sizeof(float));
					//int n = sizeof(float);
					//byte b1 = recvbyte[index];
					//byte b2 = recvbyte[index + 1];
					//recvbyte[index] = recvbyte[index + 2];
					//recvbyte[index + 1] = recvbyte[index + 3];
					//recvbyte[index + 2] = b1;
					//recvbyte[index + 3] = b2;

				}
				else if (fi.FieldType == typeof(double))
				{
					Array.Reverse(recvbyte, index, sizeof(double));
				}
			}
		}
		private UInt16 ChangeEndian_16Bit(UInt16 value)
		{
			return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
		}
		private UInt32 ChangeEndian_32Bit(UInt32 value)
		{
			return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
					(value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
		}
		public static UInt64 ChangeEndian_64Bit(UInt64 value)
		{
			return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
				   (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
				   (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
				   (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
		}
		private ReadHoldingResponse fromBytes(byte[] arr, int size)
		{
			ReadHoldingResponse str = new ReadHoldingResponse();
			IntPtr ptr = Marshal.AllocHGlobal(size);
			Marshal.Copy(arr, 0, ptr, size);
			str = (ReadHoldingResponse)Marshal.PtrToStructure(ptr, str.GetType());
			Marshal.FreeHGlobal(ptr);
			str.TransactionID = ChangeEndian_16Bit(str.TransactionID);
			str.Length = ChangeEndian_16Bit(str.Length);
			return str;
		}

		public int ReadHoldingRegister(ReadHoldingRequest requestData)
		{
			int result = 0;
			ReadHoldingResponse responseData = new ReadHoldingResponse();
			int responseDataSize = System.Runtime.InteropServices.Marshal.SizeOf(responseData);
			byte[] responseByte = new byte[responseDataSize];
			byte[] sendByte = new byte[12];//getBytes(requestData);

			int nIndex = 0;
			sendByte[nIndex++] = (byte)((requestData.TransactionID & 0xFF00) >> 8);
			sendByte[nIndex++] = (byte)(requestData.TransactionID & 0x00FF);
			sendByte[nIndex++] = (byte)((requestData.ProtocolID & 0xFF00) >> 8);
			sendByte[nIndex++] = (byte)(requestData.ProtocolID & 0x00FF);
			sendByte[nIndex++] = (byte)((requestData.Length & 0xFF00) >> 8);
			sendByte[nIndex++] = (byte)(requestData.Length & 0x00FF);
			sendByte[nIndex++] = requestData.UnitID;
			sendByte[nIndex++] = requestData.FunctionCode;
			sendByte[nIndex++] = (byte)((requestData.StartAddr & 0xFF00) >> 8);
			sendByte[nIndex++] = (byte)(requestData.StartAddr & 0x00FF);
			sendByte[nIndex++] = (byte)((requestData.QuantityReg & 0xFF00) >> 8);
			sendByte[nIndex++] = (byte)(requestData.QuantityReg & 0x00FF);

			_AccuraSocket.Send(sendByte);
			SetTransactionID();
			int recvCount = _AccuraSocket.Available;
			result = _AccuraSocket.Receive(responseByte, responseDataSize, SocketFlags.None);
			if (result > 0)
			{

				responseData = (ReadHoldingResponse)fromBytes(responseByte, Marshal.SizeOf(responseData));
				ReadHoldingError errorData = new ReadHoldingError();
				errorData = (ReadHoldingError)ErrorfromBytes(responseByte, Marshal.SizeOf(errorData));

				if (responseData.TransactionID == _TransactionID - 1 &&
					responseData.ProtocolID == 0 &&
					responseData.UnitID == 1 &&
				responseData.FunctionCode == 3)
				{
					InitRecvData();
					result = _AccuraSocket.Receive(_RecvData, responseData.ByteCount, SocketFlags.None);

				}
			}

			return result;
		}

		private void InitRecvData()
		{
			for (int i = 0; i < _RecvData.Length; i++)
			{
				_RecvData[i] = 0;
			}
		}

		public int ReadVoltageValue()
		{
			int result = 0;
			result = ReadHoldingRegister(new ReadHoldingRequest(_TransactionID, 0, 6, 1, 3, 11044, 1));
			if (result < 0)
				return -1;

			result = ReadHoldingRegister(new ReadHoldingRequest(_TransactionID, 0, 6, 1, 3, 11291, 2));
			if (result > 0)
				_AllVoltageValueStruct = (AllVoltageValue)AllVoltageValuefromBytes(_RecvData, Marshal.SizeOf(_AllVoltageValueStruct));
			else
				return -2;

			result = ReadHoldingRegister(new ReadHoldingRequest(_TransactionID, 0, 6, 1, 3, 11101, 8));
			if (result > 0)
				_VoltageValueStruct = (VoltageValue)VoltageValuefromBytes(_RecvData, Marshal.SizeOf(_VoltageValueStruct));
			else
				return -3;

			#region 테스트 시 작성됨
			//result = ReadHoldingRegister(new ReadHoldingRequest(_TransactionID, 0, 6, 1, 3, 1, 1));
			//if (result > 0)
			//{
			//    //result = ReadHoldingRegister(new ReadHoldingRequest(_TransactionID, 0, 6, 1, 3, 11001, 1));
			//    //result = ReadHoldingRegister(new ReadHoldingRequest(_TransactionID, 0, 6, 1, 3, 11002, 1));
			//    result = ReadHoldingRegister(new ReadHoldingRequest(_TransactionID, 0, 6, 1, 3, 11044, 1));
			//    result = ReadHoldingRegister(new ReadHoldingRequest(_TransactionID, 0, 6, 1, 3, 11045, 1));
			//    if (result > 0)
			//    {
			//        result = ReadHoldingRegister(new ReadHoldingRequest(_TransactionID, 0, 6, 1, 3, 11291, 2));
			//        if (result > 0)
			//        {
			//            _AllVoltageValueStruct = (AllVoltageValue)AllVoltageValuefromBytes(_RecvData, Marshal.SizeOf(_AllVoltageValueStruct));
			//        }
			//        result = ReadHoldingRegister(new ReadHoldingRequest(_TransactionID, 0, 6, 1, 3, 11101, 8));

			//        if (result > 0)
			//        {
			//            _VoltageValueStruct = (VoltageValue)VoltageValuefromBytes(_RecvData, Marshal.SizeOf(_VoltageValueStruct));
			//        }
			//        else
			//        {
			//            result = -2;
			//        }
			//    }
			//    else
			//    {
			//        result = -1;
			//    }
			//}
			//else
			//{
			//    result = -3;
			//}
			#endregion
			return result;
		}
	}

	public struct VoltageValue
	{
		public float VoltageVan;
		public float VoltageVbn;
		public float VoltageVcn;
		public float VoltageVavg;
	}

	public struct AllVoltageValue
	{
		public int AllVoltageValueData;

	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ReadHoldingRequest
	{
		public ushort TransactionID;
		public ushort ProtocolID;
		public ushort Length;
		public byte UnitID;
		public byte FunctionCode;
		public ushort StartAddr;
		public ushort QuantityReg;
		public ReadHoldingRequest(ushort transactionID, ushort protocolID, ushort length, byte unitID, byte functionCode, ushort startAddr, ushort quantityReg)
		{
			TransactionID = transactionID;
			ProtocolID = protocolID;
			Length = length;
			UnitID = unitID;
			FunctionCode = functionCode;
			StartAddr = (ushort)(startAddr - 1);
			QuantityReg = quantityReg;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ReadHoldingResponse
	{
		public ushort TransactionID;
		public ushort ProtocolID;
		public ushort Length;
		public byte UnitID;
		public byte FunctionCode;
		public byte ByteCount;
	}
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ReadHoldingError
	{
		public ushort TransactionID;
		public ushort ProtocolID;
		public ushort Length;
		public byte UnitID;
		public byte ErrorCode;
		public byte ExceptionCode;
	}
}
