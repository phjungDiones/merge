using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace CJ_Controls.Vision.BaseData
{

	internal class Win32
	{
		// public static readonly IntPtr InvalidHandleValue = new IntPtr(-1);
		public const uint FILE_MAP_WRITE = 2;
		public const uint PAGE_READWRITE = 0x04;
		public const uint OPEN_ALWAYS = 4;
		public const uint GENERIC_READ = 0x80000000;
		public const uint GENERIC_WRITE = 0x40000000;
		public const uint FILE_SHARE_READ = 0x00000001;
		public const uint FILE_SHARE_WRITE = 0x00000002;
		public const uint FILE_FLAG_OVERLAPPED = 0x40000000;
		//        
		//        const uint OPEN_EXISTING = 3;


		[DllImport("kernel32")]
		public static extern IntPtr CreateFile
		(
			string FileName,          // file name
			uint DesiredAccess,       // access mode
			uint ShareMode,           // share mode
			uint SecurityAttributes,  // Security Attributes
			uint CreationDisposition, // how to create
			uint FlagsAndAttributes,  // file attributes
			int hTemplateFile         // handle to template file
		);

		[DllImport("Kernel32")]
		public static extern IntPtr CreateFileMapping(IntPtr hFile,
			IntPtr pAttributes, UInt32 flProtect,
			UInt32 dwMaximumSizeHigh, UInt32 dwMaximumSizeLow, String pName);

		[DllImport("Kernel32")]
		public static extern IntPtr OpenFileMapping(UInt32 dwDesiredAccess,
			Boolean bInheritHandle, String name);

		[DllImport("Kernel32")]
		public static extern Boolean CloseHandle(IntPtr handle);

		[DllImport("Kernel32")]
		public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject,
			UInt32 dwDesiredAccess,
			UInt32 dwFileOffsetHigh, UInt32 dwFileOffsetLow,
			IntPtr dwNumberOfBytesToMap);

		[DllImport("Kernel32")]
		public static extern Boolean UnmapViewOfFile(IntPtr address);

		[DllImport("Kernel32")]
		public static extern Boolean DuplicateHandle(IntPtr hSourceProcessHandle,
			IntPtr hSourceHandle,
			IntPtr hTargetProcessHandle, ref IntPtr lpTargetHandle,
			UInt32 dwDesiredAccess, Boolean bInheritHandle, UInt32 dwOptions);
		public const UInt32 DUPLICATE_CLOSE_SOURCE = 0x00000001;
		public const UInt32 DUPLICATE_SAME_ACCESS = 0x00000002;

		[DllImport("Kernel32")]
		public static extern IntPtr GetCurrentProcess();


		[DllImport("Kernel32")]
		public static extern bool WriteFile(
		  IntPtr hFile,                    // handle to file to write to
		  IntPtr lpBuffer,                // pointer to data to write to file
		  UInt32 nNumberOfBytesToWrite,     // number of bytes to write
		  ref UInt32 lpNumberOfBytesWritten,  // pointer to number of bytes written
		  IntPtr lpOverlapped        // pointer to structure for overlapped I/O
		);



	}


	// This class wraps memory that can be simultaneously 
	// shared by multiple AppDomains and Processes.
	[Serializable]
	public sealed class XMapping : ISerializable, IDisposable
	{
		// The handle and string that identify 
		// the Windows file-mapping object.
		private IntPtr m_hFile;
		private IntPtr m_hFileMap = IntPtr.Zero;
		private String m_sName;

		// The address of the memory-mapped file-mapping object.
		private IntPtr m_pMap;

		public IntPtr Map
		{
			get { return m_pMap; }
		}

		// The constructors.

		public bool Write()
		{
			uint test = 0;
			return Win32.WriteFile(m_hFile, m_pMap, 512, ref test, IntPtr.Zero);

		}


		public XMapping(Int32 size) : this(size, null) { }

		public XMapping(Int32 size, String sName)
		{
			m_hFile = Win32.CreateFile(sName, Win32.GENERIC_READ | Win32.GENERIC_WRITE,
												Win32.FILE_SHARE_READ | Win32.FILE_SHARE_WRITE,
												0,
												Win32.OPEN_ALWAYS,
												Win32.FILE_FLAG_OVERLAPPED,
												0);

			if (m_hFile == IntPtr.Zero) m_hFile = new IntPtr(-1);
			m_hFileMap = Win32.CreateFileMapping(m_hFile,
				IntPtr.Zero, Win32.PAGE_READWRITE,
				0, unchecked((UInt32)size), sName);
			if (m_hFileMap == IntPtr.Zero)
			{
				if (m_hFile != IntPtr.Zero && m_hFile != (IntPtr)(-1)) Win32.CloseHandle(m_hFile);
				throw new Exception("Could not create memory-mapped file.");
			}
			m_sName = sName;
			m_pMap = Win32.MapViewOfFile(m_hFileMap, Win32.FILE_MAP_WRITE,
				0, 0, IntPtr.Zero);
		}

		// The cleanup methods.
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Dispose(true);
		}

		private void Dispose(Boolean disposing)
		{
			Win32.CloseHandle(m_hFile);
			Win32.UnmapViewOfFile(m_pMap);
			Win32.CloseHandle(m_hFileMap);
			m_hFile = IntPtr.Zero;
			m_pMap = IntPtr.Zero;
			m_hFileMap = IntPtr.Zero;
		}

		~XMapping()
		{
			Dispose(false);
		}

		// Private helper methods.
		private static Boolean AllFlagsSet(Int32 flags, Int32 flagsToTest)
		{
			return (flags & flagsToTest) == flagsToTest;
		}

		private static Boolean AnyFlagsSet(Int32 flags, Int32 flagsToTest)
		{
			return (flags & flagsToTest) != 0;
		}


		// The security attribute demands that code that calls  
		// this method have permission to perform serialization.
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// The context's State member indicates
			// where the object will be deserialized.

			// A Mapping object cannot be serialized 
			// to any of the following destinations.
			const StreamingContextStates InvalidDestinations =
					  StreamingContextStates.CrossMachine |
					  StreamingContextStates.File |
					  StreamingContextStates.Other |
					  StreamingContextStates.Persistence |
					  StreamingContextStates.Remoting;
			if (AnyFlagsSet((Int32)context.State, (Int32)InvalidDestinations))
				throw new SerializationException("The Mapping object " +
					"cannot be serialized to any of the following streaming contexts: " +
					InvalidDestinations);

			const StreamingContextStates DeserializableByHandle =
					  StreamingContextStates.Clone |
				// The same process.
					  StreamingContextStates.CrossAppDomain;
			if (AnyFlagsSet((Int32)context.State, (Int32)DeserializableByHandle))
				info.AddValue("hFileMap", m_hFileMap);

			const StreamingContextStates DeserializableByName =
				// The same computer.
					  StreamingContextStates.CrossProcess;
			if (AnyFlagsSet((Int32)context.State, (Int32)DeserializableByName))
			{
				if (m_sName == null)
					throw new SerializationException("The Mapping object " +
						"cannot be serialized CrossProcess because it was not constructed " +
						"with a String name.");
				info.AddValue("name", m_sName);
			}
		}


		// The security attribute demands that code that calls  
		// this method have permission to perform serialization.
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		private XMapping(SerializationInfo info, StreamingContext context)
		{
			// The context's State member indicates 
			// where the object was serialized from.

			const StreamingContextStates InvalidSources =
					  StreamingContextStates.CrossMachine |
					  StreamingContextStates.File |
					  StreamingContextStates.Other |
					  StreamingContextStates.Persistence |
					  StreamingContextStates.Remoting;
			if (AnyFlagsSet((Int32)context.State, (Int32)InvalidSources))
				throw new SerializationException("The Mapping object " +
					"cannot be deserialized from any of the following stream contexts: " +
					InvalidSources);

			const StreamingContextStates SerializedByHandle =
					  StreamingContextStates.Clone |
				// The same process.
					  StreamingContextStates.CrossAppDomain;
			if (AnyFlagsSet((Int32)context.State, (Int32)SerializedByHandle))
			{
				try
				{
					Win32.DuplicateHandle(Win32.GetCurrentProcess(),
						(IntPtr)info.GetValue("hFileMap", typeof(IntPtr)),
						Win32.GetCurrentProcess(), ref m_hFileMap, 0, false,
						Win32.DUPLICATE_SAME_ACCESS);
				}
				catch (SerializationException)
				{
					throw new SerializationException("A Mapping was not serialized " +
						"using any of the following streaming contexts: " +
						SerializedByHandle);
				}
			}

			const StreamingContextStates SerializedByName =
				// The same computer.
					  StreamingContextStates.CrossProcess;
			if (AnyFlagsSet((Int32)context.State, (Int32)SerializedByName))
			{
				try
				{
					m_sName = info.GetString("name");
				}
				catch (SerializationException)
				{
					throw new SerializationException("A Mapping object was not " +
						"serialized using any of the following streaming contexts: " +
						SerializedByName);
				}
				m_hFileMap = Win32.OpenFileMapping(Win32.FILE_MAP_WRITE, false, m_sName);
			}
			if (m_hFileMap != IntPtr.Zero)
			{
				m_pMap = Win32.MapViewOfFile(m_hFileMap, Win32.FILE_MAP_WRITE,
					0, 0, IntPtr.Zero);
			}
			else
			{
				throw new SerializationException("A Mapping object could not " +
					"be deserialized.");
			}
		}
	}
}
