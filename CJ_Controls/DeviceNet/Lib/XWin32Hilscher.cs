using System;
using System.Runtime.InteropServices;

namespace CJ_Controls.DeviceNet
{
	/// <summary>
	/// Summary description for Win32Hilscher.
	/// </summary>
	public class XWin32Hilscher
	{
		public const int MAX_DEV_BOARDS = 4;

		public XWin32Hilscher()
		{
		}

		[StructLayout( LayoutKind.Sequential)] public struct COMSTATE 
		{
			internal ushort usMode;
			internal ushort usStateFlag;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=64 )]
			internal byte[] abState;
		}

		[StructLayout( LayoutKind.Sequential, Pack = 1)] public struct BOARD
		{
			internal ushort usBoardNumber;
			internal ushort usAvailable;
			internal uint ulPhysicalAddress;
			internal ushort usIrqNumber;
		}

		[StructLayout( LayoutKind.Sequential)] public struct BOARD_INFO
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
			internal byte[] abDriverVersion;
			internal BOARD tBoard0;
			internal BOARD tBoard1;
			internal BOARD tBoard2;
			internal BOARD tBoard3;
		}

		[StructLayout( LayoutKind.Sequential)] public struct MSG_STRUC
		{
			internal byte rx;
			internal byte tx;
			internal byte ln;
			internal byte nr;
			internal byte a;
			internal byte f;
			internal byte b;
			internal byte e;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=255)]
			internal byte[] data;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=25)]
			internal byte[] dummy;
		}

		[StructLayout( LayoutKind.Sequential)] public struct TASKPARAM
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
			internal byte[] abTaskParameter;
		}

		[DllImport("Cif32dll.dll")]
		internal static extern short DevOpenDriver(UInt16 usDevNumber);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevCloseDriver(UInt16 usDevNumber);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevGetBoardInfo(UInt16 usDevNumber, UInt16 usSize, out BOARD_INFO tBoardInfo);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevInitBoard(UInt16 usDevNumber, IntPtr pDevAddress);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevExitBoard(UInt16 usDevNumber);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevPutTaskParameter(UInt16 usDevNumber, UInt16 usNumber, UInt16 usSize, IntPtr pvData);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevSetHostState(UInt16 usDevNumber, UInt16 usMode, UInt32 ulTimeout);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevReset(UInt16 usDevNumber, UInt16 usMode, UInt32 ulTimeout);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevGetInfo(UInt16 usDevNumber, UInt16 usInfoArea, UInt16 usSize, IntPtr pvData);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevExchangeIO(UInt16 usDevNumber, UInt16 usSendOffset, UInt16 usSendSize, IntPtr pvSendData, UInt16 usReceiveOffset, UInt16 usReceiveSize, IntPtr pvReceiveData, UInt32  ulTimeout);

		//[DllImport("Cif32dll.dll")]
		//internal static extern Int16 DevExchangeIOErr(UInt16 usDevNumber, UInt16 usSendOffset, UInt16 usSendSize, IntPtr pvSendData, UInt16 usReceiveOffset, UInt16 usReceiveSize, IntPtr pvReceiveData, out COMSTATE ptState, UInt32  ulTimeout);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevExchangeIOErr(ushort usDevNumber, ushort usSendOffset, ushort usSendSize, ushort[] sSendData, ushort usReceiveOffset, ushort usReceiveSize, ushort[] usReceiveData, out COMSTATE ptState, uint  ulTimeout);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevReadSendData(ushort usDevNumber, ushort usOffset, ushort usSize, ushort[] sReadSentData);
		//internal static extern Int16 DevReadSendData(UInt16 usDevNumber, UInt16 usOffset, UInt16 usSize, IntPtr pvData);

		//[DllImport("Cif32dll.dll")]
		//internal static extern Int16 DevGetBoardInfoEx(UInt16 usDevNumber, UInt16 usSize, IntPtr pvData);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevExchangeIOEx(UInt16 usDevNumber, UInt16 usMode, UInt16 usSendOffset, UInt16 usSendSize, IntPtr pvSendData, UInt16 usReceiveOffset, UInt16 usReceiveSize, IntPtr pvReceiveData, UInt32  ulTimeout);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevGetTaskParameter(UInt16 usDevNumber, UInt16 usNumber, UInt16 usSize, IntPtr pvData);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevGetMessage(UInt16 usDevNumber, UInt16 usSize, IntPtr ptrMsgStruct , UInt32 ulTimeout);

		[DllImport("Cif32dll.dll")]
		internal static extern short DevPutMessage(UInt16 usDevNumber, IntPtr ptrMsgStruct , UInt32 ulTimeout);

		// Board Start Mode
		internal const ushort NORMALSTART = 0;
		internal const ushort COLDSTART = 2; // New Initializing
		internal const ushort WARMSTART = 3; // Initializing with parameter
		internal const ushort BOOTSTART = 4; // switches the board into bootstrap loader mode. COM modules uses
		// this mode to store user parameters

		internal const ushort HOST_NOT_READY = 0;
		internal const ushort HOST_READY = 1;

		internal const ushort STATE_MODE_3 = 3;
		internal const ushort STATE_MODE_4 = 4;
		/* ------------------------------------------------------------------------------------ */
		/*  driver errors                                                                       */
		/* ------------------------------------------------------------------------------------ */
		internal const Int16 DRV_NO_ERROR                =  0;      // no error                                            
		internal const Int16 DRV_BOARD_NOT_INITIALIZED   = -1;      // DRIVER Board not initialized                        
		internal const Int16 DRV_INIT_STATE_ERROR        = -2;      // DRIVER Error in internal init state                 
		internal const Int16 DRV_READ_STATE_ERROR        = -3;      // DRIVER Error in internal read state                 
		internal const Int16 DRV_CMD_ACTIVE              = -4;      // DRIVER Command on this channel is activ             
		internal const Int16 DRV_PARAMETER_UNKNOWN       = -5;     // DRIVER Unknown parameter in function occured        
		internal const Int16 DRV_WRONG_DRIVER_VERSION    = -6;      // DRIVER Version is incompatible with DLL             
			                                               
		internal const Int16 DRV_PCI_SET_CONFIG_MODE     = -7;      // DRIVER Error during PCI set run mode                
		internal const Int16 DRV_PCI_READ_DPM_LENGTH     = -8;      // DRIVER Could not read PCI dual port memory length   
		internal const Int16 DRV_PCI_SET_RUN_MODE        = -9;      // DRIVER Error during PCI set run mode                                                                  
			                                            
		internal const Int16 DRV_DEV_DPM_ACCESS_ERROR    = -10;     // DEVICE Dual port ram not accessable(board not found)
		internal const Int16 DRV_DEV_NOT_READY           = -11;     // DEVICE Not ready (ready flag failed)                
		internal const Int16 DRV_DEV_NOT_RUNNING         = -12;     // DEVICE Not running (running flag failed)            
		internal const Int16 DRV_DEV_WATCHDOG_FAILED     = -13;     // DEVICE Watchdog test failed                         
		internal const Int16 DRV_DEV_OS_VERSION_ERROR    = -14;     // DEVICE Signals wrong OS version                     
		internal const Int16 DRV_DEV_SYSERR              = -15;     // DEVICE Error in dual port flags                     
		internal const Int16 DRV_DEV_MAILBOX_FULL        = -16;     // DEVICE Send mailbox is full                         
		internal const Int16 DRV_DEV_PUT_TIMEOUT         = -17;     // DEVICE PutMessage timeout                           
		internal const Int16 DRV_DEV_GET_TIMEOUT         = -18;     // DEVICE GetMessage timeout                           
		internal const Int16 DRV_DEV_GET_NO_MESSAGE      = -19;     // DEVICE No message available                         
		internal const Int16 DRV_DEV_RESET_TIMEOUT       = -20;     // DEVICE RESET command timeout                        
		internal const Int16 DRV_DEV_NO_COM_FLAG         = -21;     // DEVICE COM-flag not set                             
		internal const Int16 DRV_DEV_EXCHANGE_FAILED     = -22;     // DEVICE IO data exchange failed                      
		internal const Int16 DRV_DEV_EXCHANGE_TIMEOUT    = -23;     // DEVICE IO data exchange timeout                     
		internal const Int16 DRV_DEV_COM_MODE_UNKNOWN    = -24;     // DEVICE IO data mode unknown                         
		internal const Int16 DRV_DEV_FUNCTION_FAILED     = -25;     // DEVICE Function call failed                         
		internal const Int16 DRV_DEV_DPMSIZE_MISMATCH    = -26;     // DEVICE DPM size differs from configuration          
		internal const Int16 DRV_DEV_STATE_MODE_UNKNOWN  = -27;     // DEVICE State mode unknown

		// Error from Interface functions
		internal const Int16 DRV_USR_OPEN_ERROR          = -30;     // USER Driver not opened (device driver not loaded)   
		internal const Int16 DRV_USR_INIT_DRV_ERROR      = -31;     // USER Can't connect with device                      
		internal const Int16 DRV_USR_NOT_INITIALIZED     = -32;     // USER Board not initialized (DevInitBoard not called)
		internal const Int16 DRV_USR_COMM_ERR            = -33;     // USER IOCTRL function failed                         
		internal const Int16 DRV_USR_DEV_NUMBER_INVALID  = -34;     // USER Parameter DeviceNumber  invalid                
		internal const Int16 DRV_USR_INFO_AREA_INVALID   = -35;     // USER Parameter InfoArea unknown                     
		internal const Int16 DRV_USR_NUMBER_INVALID      = -36;     // USER Parameter Number invalid                       
		internal const Int16 DRV_USR_MODE_INVALID        = -37;     // USER Parameter Mode invalid                         
		internal const Int16 DRV_USR_MSG_BUF_NULL_PTR    = -38;     // USER NULL pointer assignment                        
		internal const Int16 DRV_USR_MSG_BUF_TOO_SHORT   = -39;     // USER Message buffer too short                       
		internal const Int16 DRV_USR_SIZE_INVALID        = -40;     // USER Parameter Size invalid                         
		internal const Int16 DRV_USR_SIZE_ZERO           = -42;     // USER Parameter Size with zero length               
		internal const Int16 DRV_USR_SIZE_TOO_LONG       = -43;     // USER Parameter Size too long                        
		internal const Int16 DRV_USR_DEV_PTR_NULL        = -44;     // USER Device address null pointer                    
		internal const Int16 DRV_USR_BUF_PTR_NULL        = -45;     // USER Pointer to buffer is a null pointer            
			                                                
		internal const Int16 DRV_USR_SENDSIZE_TOO_LONG   = -46;     // USER Parameter SendSize too long                    
		internal const Int16 DRV_USR_RECVSIZE_TOO_LONG   = -47;     // USER Parameter ReceiveSize too long                 
		internal const Int16 DRV_USR_SENDBUF_PTR_NULL    = -48;     // USER Pointer to send buffer is a null pointer       
		internal const Int16 DRV_USR_RECVBUF_PTR_NULL    = -49;     // USER Pointer to receive buffer is a null pointer 

		internal const Int16 DRV_USR_FILE_OPEN_FAILED    = -100;    // USER file not opend
		internal const Int16 DRV_USR_FILE_SIZE_ZERO      = -101;    // USER file size zero
		internal const Int16 DRV_USR_FILE_NO_MEMORY      = -102;    // USER not enough memory to load file
		internal const Int16 DRV_USR_FILE_READ_FAILED    = -103;    // USER file read failed
		internal const Int16 DRV_USR_INVALID_FILETYPE    = -104;    // USER file type invalid
		internal const Int16 DRV_USR_FILENAME_INVALID    = -105;    // USER file name not valid

		internal const Int16 DRV_RCS_ERROR_OFFSET        = 1000;     // RCS error number start
	}
}

