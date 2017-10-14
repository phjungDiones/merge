﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TBDB_CTC.FileLib
{
    public class SharedAPI
    {
        string strCurrentServer = "";

        // 구조체 선언
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NETRESOURCE
        {
            public uint dwScope;
            public uint dwType;
            public uint dwDisplayType;
            public uint dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        // API 함수 선언
        [DllImport("mpr.dll", CharSet = CharSet.Auto)]
        public static extern int WNetUseConnection(
                    IntPtr hwndOwner,
                    [MarshalAs(UnmanagedType.Struct)] ref NETRESOURCE lpNetResource,
                    string lpPassword,
                    string lpUserID,
                    uint dwFlags,
                    StringBuilder lpAccessName,
                    ref int lpBufferSize,
                    out uint lpResult);

        // API 함수 선언 (공유해제)
        [DllImport("mpr.dll", EntryPoint = "WNetCancelConnection2", CharSet = CharSet.Auto)]
        public static extern int WNetCancelConnection2A(string lpName, int dwFlags, int fForce);

        // 공유연결
        public int ConnectRemoteServer(string server, string id, string pw)
        {
            strCurrentServer = server;
            int capacity = 64;
            uint resultFlags = 0;
            uint flags = 0;
            System.Text.StringBuilder sb = new System.Text.StringBuilder(capacity);
            NETRESOURCE ns = new NETRESOURCE();
            ns.dwType = 1;              // 공유 디스크
            ns.lpLocalName = null;   // 로컬 드라이브 지정하지 않음
            ns.lpRemoteName = server;
            ns.lpProvider = null;
            int result = 0;
            result = WNetUseConnection(IntPtr.Zero, ref ns, pw, id, flags,
                                                sb, ref capacity, out resultFlags);
            return result;
        }

        // 공유해제
        public void CancelRemoteServer(string server)
        {
            WNetCancelConnection2A(server, 1, 0);
        }

        public void CancelRemoteServer()
        {
            WNetCancelConnection2A(strCurrentServer, 1, 0);
        }
    }
}
