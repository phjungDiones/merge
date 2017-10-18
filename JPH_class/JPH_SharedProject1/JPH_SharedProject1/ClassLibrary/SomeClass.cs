using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JPH_SharedProject1.ClassLibrary
{
    class FileClass
    {
        private static volatile FileClass instance;
        private static object syncRoot = new Object();

        private FileClass() { }
        /// <summary>DoWork is a method in the TestClass class.
        /// <para>Here's how you could make a second paragraph in a description. <see cref="System.Console.WriteLine(System.String)"/> for information about output statements.</para>
        /// <seealso cref="TestClass.Main"/>
        /// </summary>
        public static FileClass Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new FileClass();
                    }
                }

                return instance;
            }
        }


        /// <summary>파일이 존재하는지 여부를 bool 값으로 리턴합니다.
        /// <para>EX) SomeClass.Instance.IsFile(@"C:\Users\jph\Desktop\새 폴더 (7)\WireProtector2\WireProtector2.sln")</para>
        /// </summary>
        public bool IsFile(string _strFile)
        {
            FileInfo _finfo = new FileInfo(_strFile);
            return _finfo.Exists;
        }
        /// <summary>폴더가 존재하는지 여부를 bool 값으로 리턴합니다.
        /// <para>EX) SomeClass.Instance.IsFile(@"C:\Users\jph\Desktop\새 폴더 (7)\WireProtector2")</para>
        /// </summary>
        public bool isFolder(string _strFolder)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(_strFolder);
            return di.Exists;
        }


        /// <summary>현재 작업중인 폴더의 경로를 반환합니다.
        /// <para>EX) SomeClass.Instance.GetCurrentDirectory()</para>
        /// </summary>
        public string GetCurrentDirectory()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }


/// <summary>
/// 파일이 다른 프로세스에 의해 사용중인지 점검
/// </summary>
/// <param name="filePath">파일경로</param>
/// <param name="secondsToWait">사용중이면 잠시 기다릴 시간</param>
/// <returns>사용중이면 true</returns>
public static bool IsFileLocked(string filePath, int secondsToWait)
        {
            bool isLocked = true;
            int i = 0;
            while (isLocked && ((i < secondsToWait) || (secondsToWait == 0)))
            {
                try
                {
                    using (File.Open(filePath, FileMode.Open)) { }
                    return false;
                }
                catch (IOException e)
                {
                    var errorCode = System.Runtime.InteropServices.Marshal.GetHRForException(e) & ((1 << 16) - 1);
                    isLocked = errorCode == 32 || errorCode == 33;
                    i++;
                    if (secondsToWait != 0)
                        new System.Threading.ManualResetEvent(false).WaitOne(1000);
                }
            }

            return isLocked;
        }

        /// <summary>
        /// 파일을 삭제한다.
        /// </summary>
        public static void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;
            if (File.Exists(filePath) == false) return;
            if (IsFileLocked(filePath, 1)) return;

            File.Delete(filePath);
        }

    }
}
