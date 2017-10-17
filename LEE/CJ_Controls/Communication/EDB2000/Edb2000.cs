using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace CJ_Controls.Communication.EDB2000
{

    //----- RS232serial -------------------------------------------------------------------------------
    public enum MotorCmd
    {
        ROR = 1,
        ROL,//  2,
        MST,//  3,
        MVP,//  4,
        SAP,//  5,
        GAP,//  6,
        STAP,// 7,
        RSAP,// 8,
        SGP,//  9,
        GGP,//  10,
        STGP,// 11,
        RSGP,// 12,
        RFS,//  13,
        SIO,//  14,
        GIO,//  15,
        CALC,// 19,
        COMP,// 20,
        JC,//   21,
        JA,//   22,
        CSUB,// 23,
        RSU,//B 24,
        EI,//   25,
        DI,//   26,
        WAIT,// 27,
        STOP,// 28,
        SCO,//  30,
        GCO,//  31,
        CCO,//  32,
        CALX,// 33,
        AAP,//  34,
        AGP,//  35,
        VECT,// 37,
        RETI,// 38,
        ACO,//  39,

        VERSION = 136,
        RTPREVENT = 138,
    }

    public enum AxisParam
    {
        TargetPos = 0,
        ActualPos, // 1
        TargetSpeed,//  2,
        ActualSpeed,//  3,
        MaxPosSpeed,//  4,
        MaxAcc,//  5,
        AbsMaxCurrent,//  6,
        StandByCurrent,// 7,
        PosReached,// 8,
        HomeSWStatus,//  9,
        RightLMStatus,//  10,
        LeftLMStatus,//  11,
        RightLMSWDisable,//  12,
        LeftLMSWDisable,//  13,
    }

    public enum GlobalParam
    {
        EepromMagic = 64,
        RSBaudRate, // 65
    }

    public enum MoveParam
    {
        AbsMove = 0,
        RelMove = 1,
        CoordMove = 2,
    }

    public enum DataBank
    {
        DataBank0 = 0,
        DataBank1 = 1,
        DataBank2 = 2,
    }

    public enum ReplyCode
    {
        EMC_REPLY_OK = 0,
        EMC_REPLY_NOT_READY, // 1
        EMC_REPLY_CHECKSUM_ERROR, // 2
        EMC_REPLY_VERSION, // 3
        EMC_REPLY_TIMEOUT, // 4
    }

    //#define GET 0
    //#define AXIS 1
    //#define GLOBAL 2
    //#define VERSION 3

    public struct shortCmdType
    {
        //public byte Command;
        public byte Type;
        public byte Bank;
        public int Value;
    };

    public struct baseCmdType
    {
        public byte Command;
        public byte Type;
        public byte Bank;
        public int Value;
    };

    public struct byteCmdType
    {
        public byte Command;
        public byte Type;
        public byte Bank;
        public byte Value0;
        public byte Value1;
        public byte Value2;
        public byte Value3;
    };

    public struct shortReplyType
    {
        public byte ModuleAddr;
        public byte Status;
        public byte CmdNumber;
        public int Value;
    };

    public struct baseReplyType
    {
        public byte ReplyAddr;
        public byte ModuleAddr;
        public byte Status;
        public byte CmdNumber;
        public int Value;
    };


    public class Edb2000
    {
        public const int WAIT_TIMEOUT = 100;   // 명령전송후 대기시간
        private SerialPort ComPort = new SerialPort();
        public static int nRecvCnt = 0;
        public static byte[] inByte = new byte[9];
        public static bool bDataReceived = false;

        public Edb2000()
        {

        }

        public void Test()
        {

        }

        public bool OpenComm(string sPort, int nBaudRate)
        {
            try
            {
                CloseComm();

                ComPort.PortName = sPort;
                ComPort.BaudRate = nBaudRate;

                ComPort.Parity = Parity.None;
                ComPort.StopBits = StopBits.One;
                ComPort.DataBits = 8;
                ComPort.Handshake = Handshake.None;
                //ComPort.RtsEnable = true;

                ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                ComPort.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return ComPort.IsOpen;
        }

        public void CloseComm()
        {
            if (ComPort.IsOpen)
            {
                ComPort.Close();
            }
        }

        public void SendData(byte[] byteData)
        {
            if (ComPort.IsOpen)
            {
                nRecvCnt = 0;
                bDataReceived = false;
                ComPort.Write(byteData, 0, byteData.Length);
            }
        }

        public bool GetData(ref byte[] byteData)
        {
            bool bRet = false;

            if (bDataReceived)
            {
#if DEBUG
                Debug.WriteLine("*** Data Received: " + BitConverter.ToString(inByte));
#endif

                inByte.CopyTo(byteData, 0);
                nRecvCnt = 0;
                bDataReceived = false;
                bRet = true;
            }

            return bRet;
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            if (9 <= sp.BytesToRead)
            {
                nRecvCnt += sp.Read(inByte, 0, 9);
                bDataReceived = true;
            }
        }

        //end EraeSerial




        // CmdAgent.cs


        //Edb2000 MotorComm = new Edb2000();


        public void SendCmd(byte[] buffer)
        {
            buffer[8] = CalcCheckSum(buffer);
            SendData(buffer);
        }

        public void SendCmd(byte MotorAddress, baseCmdType tyBCF)
        {
            byte[] TxBuffer = new byte[9];
            SendCmd(MotorAddress, tyBCF.Command, tyBCF.Type, tyBCF.Bank, tyBCF.Value, ref TxBuffer);
        }

        public void SendCmd(byte MotorAddress, baseCmdType tyBCF, ref byte[] TxBuffer)
        {
            SendCmd(MotorAddress, tyBCF.Command, tyBCF.Type, tyBCF.Bank, tyBCF.Value, ref TxBuffer);
        }

        public void SendCmd(byte MotorAddress, byte nCommand, byte nType, byte nBank, int nValue)
        {
            byte[] TxBuffer = new byte[9];
            SendCmd(MotorAddress, nCommand, nType, nBank, nValue, ref TxBuffer);
        }

        public void SendCmd(byte MotorAddress, byte nCommand, byte nType, byte nBank, int nValue, ref byte[] TxBuffer)
        {
            TxBuffer[0] = MotorAddress;
            TxBuffer[1] = nCommand;
            TxBuffer[2] = nType;
            TxBuffer[3] = nBank;
            TxBuffer[4] = (byte)(nValue >> 24);
            TxBuffer[5] = (byte)(nValue >> 16);
            TxBuffer[6] = (byte)(nValue >> 8);
            TxBuffer[7] = (byte)(nValue & 0xff);
            TxBuffer[8] = CalcCheckSum(TxBuffer);

            //Send the data
            SendData(TxBuffer);
        }

        public bool WaitDataRecv(int nWaitTime)
        {
            long nStart = DateTime.Now.Ticks;

            while (!bDataReceived && ((DateTime.Now.Ticks - nStart) / 10000) < nWaitTime)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }

            return bDataReceived; // false for timeout
        }

        public ReplyCode GetReply()
        {
            byte[] RxBuffer = new byte[9];
            return GetReply(ref RxBuffer, false);
        }

        public ReplyCode GetReply(ref int nValue, bool bVersion)
        {
            byte[] RxBuffer = new byte[9];
            byte uchReplyAddr = 0, ucModuleAdr = 0, uchStatus = 0, ucCmdNumber = 0;

            return GetReply(ref uchReplyAddr, ref ucModuleAdr, ref uchStatus, ref ucCmdNumber, ref nValue, ref RxBuffer, bVersion);
        }

        public ReplyCode GetReply(ref byte[] buffer, bool bVersion)
        {
            byte uchReplyAddr = 0, ucModuleAdr = 0, uchStatus = 0, ucCmdNumber = 0;
            int nValue = 0;

            return GetReply(ref uchReplyAddr, ref ucModuleAdr, ref uchStatus, ref ucCmdNumber, ref nValue, ref buffer, bVersion);
        }

        public ReplyCode GetReply(ref baseReplyType reply, bool bVersion)
        {
            byte[] RxBuffer = new byte[9];
            byte uchReplyAddr = 0, ucModuleAdr = 0, uchStatus = 0, ucCmdNumber = 0;
            int nValue = 0;

            return GetReply(ref reply.ReplyAddr, ref reply.ModuleAddr, ref reply.Status, ref reply.CmdNumber, ref reply.Value, ref RxBuffer, bVersion);
        }

        public ReplyCode GetReply(ref byte ucReplyAddr, ref byte ucModuleAdr, ref byte ucStatus, ref byte ucCmdNumber, ref int nValue, ref byte[] buffer, bool bVersion)
        {
            if (WaitDataRecv(WAIT_TIMEOUT))
            {
                GetData(ref buffer);
                ucCmdNumber = buffer[3];

                if (bVersion)  // 버전은 쳌섬이 없다
                {
                    return ReplyCode.EMC_REPLY_VERSION;
                }

                byte Checksum = CalcCheckSum(buffer);
                if (Checksum != buffer[8])
                {
                    return ReplyCode.EMC_REPLY_CHECKSUM_ERROR;
                }
                else
                {
                    ucReplyAddr = buffer[0];
                    ucModuleAdr = buffer[1];
                    ucStatus = buffer[2];
                    ucCmdNumber = buffer[3];
                    nValue = (buffer[4] << 24) | (buffer[5] << 16) | (buffer[6] << 8) | buffer[7];

                    return ReplyCode.EMC_REPLY_OK;
                }
            }
            else
            {
                return ReplyCode.EMC_REPLY_TIMEOUT;
            }
        }

        //end CmdAgent.cs



        //EraseUtill

        public static byte CalcCheckSum(byte[] buffer)
        {
            int i;
            byte cCheckSum = 0;

            if (8 <= buffer.Length)
            {
                for (i = 0; i < 8; i++)
                    cCheckSum += buffer[i];
            }

            return cCheckSum;
        }





    }
}
