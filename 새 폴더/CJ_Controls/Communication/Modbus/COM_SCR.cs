using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.ComponentModel;
using CJ_Controls;

namespace CJ_Controls.Communication
{
	public class COM_SCR : Component
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

		public COM_SCR()
		{
			m_ModusBus.MessageEvent += new Class_Modbus.MessageEventHandler(this.SCR_Modusbus_MessageEvent);

			m_Thread = new Thread(new ThreadStart(RunSequnece_Modusbus));
			m_Thread.IsBackground = true;
		}

		Thread m_Thread = null;
		Class_Modbus m_ModusBus = new Class_Modbus();
		[Category("SCR Port Setting"), Description("Port Name"), DefaultValue(false)]
		public string PortName
		{
			get { return m_ModusBus.ModbusPort.PortName; }
			set { m_ModusBus.ModbusPort.PortName = value; }
		}
		[Category("SCR Port Setting"), Description("Baudrate"), DefaultValue(false)]
		public int BaudRate
		{
			get { return m_ModusBus.ModbusPort.BaudRate; }
			set { m_ModusBus.ModbusPort.BaudRate = value; }
		}
		[Category("Modbus Mode"), Description("ASCII 모드로 사용"), DefaultValue(false)]
		public bool ModbusMode_ASCII
		{
			get { return bModbusMode_ASCII; }
			set { bModbusMode_ASCII = value; }
		}
		public void Open()
		{
			m_SeqNumModbus = 0;
			m_ModusBus.Open();
		}
		public void Close()
		{
			m_ModusBus.Close();
			m_SeqNumModbus = 0;
		}
		public void Start()
		{
			m_Thread.Start();
		}
		private void SCR_Modusbus_MessageEvent(object sender, CJ_Controls.MessageEventArgs args)
		{
			LogTextOut(args.Text);
		}
		private void RunSequnece_Modusbus()
		{
			while (true)
			{
				Thread.Sleep(100);
				SequenceModbus();
			}
		}

		private bool bModbusMode_ASCII = true;
		private int m_SeqNumModbus = 0;
		private ushort[] m_ModbusGetData = new ushort[7];
		private ushort[,] m_MODBUS_DATA = new ushort[16, 7];

		//m_MODBUS_DATA 설명 : ZONE 16개
		//7개의 데이터 => 0:상태, 1:4-20ma, 2:전압, 3:전류(단상:3상), 4:R상(3상), 5:S상(3상), 6:T상(3상)
		private byte m_Modbus_Index = 0;
		public byte GetModbus_CurIndex()
		{
			return m_Modbus_Index;
		}
		public ushort[,] GetSCR_Data()
		{
			return m_MODBUS_DATA;
		}
		private void SequenceModbus()
		{
			int nRet = 0;
			int nSeqNum = m_SeqNumModbus;
			switch (nSeqNum)
			{
				case 0:
					{
						if (m_ModusBus.IsOpen())
						{
							nSeqNum = 100;
						}
					}break;
				case 100:
					{
						if (bModbusMode_ASCII)
							nRet = m_ModusBus.SeqReadData_ASCII((byte)(m_Modbus_Index + 1), 0, 7, ref m_ModbusGetData);
						else
							nRet = m_ModusBus.SeqReadData((byte)(m_Modbus_Index + 1), 0, 7, ref m_ModbusGetData);

						if (nRet > 0)
						{
							for (int i = 0; i < m_ModbusGetData.Length; i++)
							{
								m_MODBUS_DATA[m_Modbus_Index, i] = m_ModbusGetData[i];
							}
							nSeqNum = 200;
						}
						else if (nRet < 0)
						{
							for (int i = 0; i < m_ModbusGetData.Length; i++)
							{
								m_MODBUS_DATA[m_Modbus_Index, i] = ushort.MaxValue;
							}
							nSeqNum = 200;
						}
					} break;
				case 200:
					{
						m_Modbus_Index += 1;
						if (m_Modbus_Index >= 16)
							m_Modbus_Index = 0;

						nSeqNum = 0;
					}break;
				default:
					break;
			}
			m_SeqNumModbus = nSeqNum;
		}
	}
}
