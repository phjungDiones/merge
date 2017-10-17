﻿using CJ_Controls;
using CJ_Controls.DeviceNet;
using System.Threading;

namespace TBDB_CTC.GLOBAL
{
    public class IOManager
    {

        //         private const string I_HPgateOpen = "HP Gate Slot Valve Open Status";
        //         private const string I_HPgateClose = "HP Gate Slot Valve Close Status";
        // 
        //         private const string O_HPslot = "HP Slot Valve Open";
        //         private const string C_HPslot = "HP Slot Valve Close";
        // 
        //         private const string I_LLSlotOpen = "L/L Slot Valve Open Status";
        //         private const string I_LLSlotClose = "L/L Slot Valve Close Status";
        // 
        //         private const string O_LLgate = "L/L Gate Slot Valve Open";
        //         private const string C_LLgate = "L/L Gate Slot Valve Close";
        // 
        //         private const string I_BDgateOpen = "Chamber BD  Slit Gate open Status";
        //         private const string I_BDgateClose = "Chamber BD  Slit Gate Close Status";
        // 
        //         private const string I_TMgateOpen = "Chamber TM  Slit Gate open Status";
        //         private const string I_TMgateClose = "Chamber TM  Slit Gate Close Status";
        // 
        //         private const string O_TMPump_Slow = "VAC TM PUMP Slow Valve Open";
        //         private const string O_TMPump_Fast = "VAC TM PUMP Fast Valve Open";
        // 
        //         private const string O_LLPump_Slow = "Load Lock PUMP  Slow Valve Open";
        //         private const string O_LLPump_Fast = "Load Lock PUMP Fast Valve Open";
        // 
        //         private const string O_EQPressure = "EQ Pressure Valve Open";
        //         private const string C_EQPressure = "EQ Pressure Valve Close";
        // 
        //         private const string I_LLDryPumpRun = "L/L Dry Pump Run Status";
        //         private const string I_LLDryPumpWarning = "L/L Dry Pump Warning Status";
        //         private const string I_LLDryPumpAlarm = "L/L Dry Pump Warning Status";
        // 
        //         private const string S_LLATM = "Load Lock ATM Swtich #1";
        //         private const string S_LLVAC = "Load Lock VAC Swtich #2";
        // 
        //         private const string O_ChamberBDGate = "Chamber BD측 Gate  Valve Open";
        //         private const string C_ChamberBDGate = "Chamber BD측 Gate  Valve Close";
        // 
        //         private const string O_ChamberTMGate = "Chamber TM측 Gate  Valve Open";
        //         private const string C_ChamberTMGate = "Chamber TM측 Gate  Valve Close";
        // 
        //         private const string O_LLPumpingValve_Slow = "Load Lock Slow Pumping Valve Open";
        //         private const string O_LLPumpingValve_Fast = "Load Lock Fast Pumping Valve Open";
        // 
        //         private const string O_LLVentValve_Slow = "Load Lock Slow Vent Valve Open";
        //         private const string O_LLVentValve_Fast = "Load LockFast Vent Valve Open";
        // 
        //         private const string O_ChamberVentValve_Fast = "Chamber Fast Vent Valve Open";
        //         private const string O_ChamberVentValve_Slow = "Chamber Slow Vent Valve Open";
        // 
        //         private const string O_ChamberEqualPressureValve = "Chamber Equal Pressure Valve Open";
        //         private const string O_ChambrPumpValve_Slow = "Chamber Slow Pump Valve Open";
        //         private const string O_ChambrPumpValve_Fast = "Chamber Fast Pump Valve Open";



        //Pumping,Venting 관련 Input
        public string I_VacuumTm_ConvectronRy_1 = "Vacuum TM Convectron Ry-1";
        public string I_VacuumTm_ConvectronRy_2 = "Vacuum TM Convectron Ry-2";

        public string I_TmMainPump_ValueOpen = "TM Main PUMP Valve Open";
        public string I_TmMainPump_ValueClose = "TM Main PUMP Valve Close";

        public string I_BonderGate_OpenStatus = "Bonder  Gate Open Status";
        public string I_BonderGate_CloseStatus = "Bonder Gate Close Status";

        public string I_LoadlockTmGate_OpenStatus = "Load lock TM  Gate Open Status";
        public string I_LoadlockTmGate_CloseStatus = "Load lock TM  Gate Close Status";

        public string I_ChamberTopLID_OpenStatus = "Chamber Top LID Open Status";
        public string I_VacuumTm_AtmSwitch = "Vacuum TM ATM Swtich #1";

        public string I_TMMainPUMP_ValveOpen = "TM Main PUMP Valve Open";
        public string I_TMMainPUMP_ValveClose = "TM Main PUMP Valve Close";

        public string I_VacuumTM_ATMSwtich1 = "Vacuum TM ATM Swtich #1";

        public string I_VacuumTMConvectron_Ry1 = "Vacuum TM Convectron Ry-1";
        public string I_VacuumTMConvectrion_Ry2 = "Vacuum TM Convectron Ry-2";

        public string I_SlowVentGN2_PressureSwitch_1 = "Slow Vent G-N2 Pressure Switch #1";

        public string I_LoadLockConvectronRy_1 = "Load Lock Convectron Ry-1";
        public string I_LoadLockConvectrionRy_2 = "Load Lock Convectron Ry-2";

        public string I_LoadLockSlotValve_OpenStatus = "L/L Slot Valve Open Status";
        public string I_LoadLockSlotValve_CloseStatus = "L/L Slot Valve Close Status";

        public string I_LoadLockTopLID_OpenStatus = "Load Lock Top LID Open Status";

        public string I_LoadLock_ATMSwtich_1 = "Load Lock ATM Swtich #1";

        public string I_LoadLockDryPump_AlarmStatus = "L/L Dry Pump Alarm Status";
        public string I_LoadLockDryPump_RunStatus = "L/L Dry Pump Run Status";


        public string I_LoadLockMainPUMP_ValveOpen = "Load Lock Main PUMP Valve Open";
        public string I_LoadLockMainPUMP_ValveClose = "Load Lock Main PUMP Valve Close";

        //Pumping,Venting 관련 Output
        public string O_ChamberEqualPressure_VV = "Chamber Equal Pressure V/V";
        public string O_VacuumTMSlowPump_VV = "Vacuum TM Slow Pump V/V";
        public string O_VacuumTmmainPump_VV = "Vacuum TM Main Pump V/V";
        public string O_LoadLockVent_VV = "L/L Vent V/V";
        public string O_LoadLockDry_pumpOn = "Load Lock Dry pump On";
        public string O_LoadLockSlowPumping_VV = "L/L Slow Pumping V/V";
        public string O_LoadLockFastPumping_VV = "L/L Fast Pumping V/V";
        public string O_VacuumTmVent_VV = "Vacuum TM Vent V/V";


        public string O_VtmArmExt_1 = "Extend Enable Station #1";
        public string O_VtmArmExt_2 = "Extend Enable Station #2";
        public string O_VtmArmExt_3 = "Extend Enable Station #3";
        public string O_VtmArmExt_4 = "Extend Enable Station #4";



        private COM_DeviceNet_IO_List cOM_DeviceNet_IO_List = new COM_DeviceNet_IO_List();
        //private static COM_DeviceNet cOM_DeviceNet = new COM_DeviceNet();
        private static COM_DeviceNet cOM_DeviceNet = COM_DeviceNet.Instance;

        private Ctrl_DNet_IO_List_View IO_List_View = new Ctrl_DNet_IO_List_View();
        //private static Ctrl_DNet_IO_List_View IO_List_View;

        Thread m_Thread_Read = null;




        public void StartReadIO(Ctrl_DNet_IO_List_View Iolist)
        {
            cOM_DeviceNet.DNet_IO_List = cOM_DeviceNet_IO_List;            
            cOM_DeviceNet.Open();

            //IO_List_View.SetDeviceNet(cOM_DeviceNet);
            Iolist.SetDeviceNet(cOM_DeviceNet);

            m_Thread_Read = new Thread(new ThreadStart(ReadAll));
            m_Thread_Read.IsBackground = true;
            m_Thread_Read.Start();
        }


        private void ReadAll()
        {
            while (true)
            {
                try
                {
                    cOM_DeviceNet.ReadAll_and_Matching();
                }
                catch// (Exception ex)
                {
                    //Console.WriteLine(string.Format("IO View Thread Exception..{0}", ex.Message));
                }

                Thread.Sleep(100);
            }
        }


        private bool Read(string strIOname, DNET_IO_TYPE type)
        {
            DeviceNetIO IO = cOM_DeviceNet_IO_List.GetLinkData(strIOname, type);
            return cOM_DeviceNet.ReadBit(IO);
        }

        private int Write(string strIOname, DNET_IO_TYPE type, bool bOn)
        {
            DeviceNetIO IO = cOM_DeviceNet_IO_List.GetLinkData(strIOname, type);
            return cOM_DeviceNet.WriteBit(IO, bOn);
        }

        public bool ReadInput(string strIOname)
        {
            return Read(strIOname, DNET_IO_TYPE.D_INPUT);
        }

        public bool ReadInput_A(string strIOname)
        {
            return Read(strIOname, DNET_IO_TYPE.A_INPUT);
        }

        public bool ReadOutput(string strIOname)
        {
            return Read(strIOname, DNET_IO_TYPE.D_OUTPUT);
        }

        public bool ReadOutput_A(string strIOname)
        {
            return Read(strIOname, DNET_IO_TYPE.A_OUTPUT);
        }

        public int WriteOutput(string strIOname, bool bOn)
        {
            return Write(strIOname, DNET_IO_TYPE.D_OUTPUT, bOn);
        }

        public int WriteOutput_A(string strIOname, bool bOn)
        {
            return Write(strIOname, DNET_IO_TYPE.A_OUTPUT, bOn);
        }


        public int VtmArmExt(int nStation, bool bOn)
        {
            int nRet = 0;
            if (nStation == 0)
                nRet = Write(O_VtmArmExt_1, DNET_IO_TYPE.D_OUTPUT, bOn);
            else if (nStation == 1)
                nRet = Write(O_VtmArmExt_2, DNET_IO_TYPE.D_OUTPUT, bOn);
            else if (nStation == 2)
                nRet = Write(O_VtmArmExt_3, DNET_IO_TYPE.D_OUTPUT, bOn);
            else if (nStation == 3)
                nRet = Write(O_VtmArmExt_4, DNET_IO_TYPE.D_OUTPUT, bOn);
            else
                return nRet = - 1;

            return nRet;
        }

        public bool ReadFlagTest()
        {
            return cOM_DeviceNet.bTestFalg;
        }





    }
}