using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DionesTool.UTIL
{
    /// <summary>
    /// 유틸 관련 함수
    /// </summary>
    public class Util
    {
        /// <summary>
        /// Delay 함수 MS
        /// </summary>
        /// <param name="MS">(단위 : MS)</param>
        ///<returns></returns>
        public static DateTime Delay(int ms)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        /// <summary>
        /// 경로에서 파일 이름만 추출한다
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileNameInPath(string path)
        {
            return System.IO.Path.GetFileName(path);
        }

        /// <summary>
        /// string을 byte array로 변경 (ASCII)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] TranslateStringToByteArrayAscii(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        /// <summary>
        /// byte array를 string으로 변경 (ASCII)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string TranslateByteArrayToStringAscii(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }

        /// <summary>
        /// 이미 실행 중인 프로그램이 있는지 확인
        /// </summary>
        /// <returns></returns>
        public static bool IsAppAlreadyRunning()
        {
            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();

            int ID = currentProcess.Id;
            string Name = currentProcess.ProcessName;

            bool isAlreadyRunnig = false;

            System.Diagnostics.Process[] processess = System.Diagnostics.Process.GetProcesses();

            foreach (System.Diagnostics.Process process in processess)
            {
                if (ID != process.Id)
                {
                    if (Name == process.ProcessName)
                    {
                        isAlreadyRunnig = true;
                        break;
                    }
                }
            }
            return isAlreadyRunnig;
        }

        /// <summary>
        /// 16bit의 Endian을 바꾼다 (little -> big, big -> little)
        /// </summary>
        /// <param name="pInt16Value"></param>
        public static void ConvertEndian(ref short pInt16Value)
        {
            byte[] temp = BitConverter.GetBytes(pInt16Value);
            Array.Reverse(temp);
            pInt16Value = BitConverter.ToInt16(temp, 0);
        }

        /// <summary>
        /// 16bit의 Endian을 바꾼다 (little -> big, big -> little)
        /// </summary>
        /// <param name="pUint16Value"></param>
        public static void ConvertEndian(ref ushort pUint16Value)
        {
            byte[] temp = BitConverter.GetBytes(pUint16Value);
            Array.Reverse(temp);
            pUint16Value = BitConverter.ToUInt16(temp, 0);
        }

        /// <summary>
        /// 32bit의 Endian을 바꾼다 (little -> big, big -> little)
        /// </summary>
        /// <param name="pInt32Value"></param>
        public static void ConvertEndian(ref int pInt32Value)
        {
            byte[] temp = BitConverter.GetBytes(pInt32Value);
            Array.Reverse(temp);
            pInt32Value = BitConverter.ToInt32(temp, 0);
        }

        /// <summary>
        /// 32bit의 Endian을 바꾼다 (little -> big, big -> little)
        /// </summary>
        /// <param name="pUint32Value"></param>
        public static void ConvertEndian(ref uint pUint32Value)
        {
            byte[] temp = BitConverter.GetBytes(pUint32Value);
            Array.Reverse(temp);
            pUint32Value = BitConverter.ToUInt32(temp, 0);
        }

        /// <summary>
        /// 64bit의 Endian을 바꾼다 (little -> big, big -> little)
        /// </summary>
        /// <param name="pInt64Value"></param>
        public static void ConvertEndian(ref long pInt64Value)
        {
            byte[] temp = BitConverter.GetBytes(pInt64Value);
            Array.Reverse(temp);
            pInt64Value = BitConverter.ToInt64(temp, 0);
        }

        /// <summary>
        /// 64bit의 Endian을 바꾼다 (little -> big, big -> little)
        /// </summary>
        /// <param name="pUint64Value"></param>
        public static void ConvertEndian(ref ulong pUint64Value)
        {
            byte[] temp = BitConverter.GetBytes(pUint64Value);
            Array.Reverse(temp);
            pUint64Value = BitConverter.ToUInt64(temp, 0);
        }

        //Process Start (해당 프로세스(exe)의 경로를 받아 실행)
        public static void ProcStart(string strProcPath)
        {
            // open text file in notepad (or another default text editor)
            System.Diagnostics.Process.Start(strProcPath);
        }

        //Screen Capture
        public static void takeScreenShot(string strSavePath)
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                bmp.Save(strSavePath);  // saves the image
            }
        }

        /// <summary>
        /// Listview에 있는 내용을 CSV로 저장
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="filePath"></param>
        /// <param name="includeHidden"></param>
        public static bool ListViewToCSV(ListView listView, string filePath, bool includeHidden)
        {
            //make header string
            StringBuilder result = new StringBuilder();
            WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listView.Columns[i].Text);

            //export data rows
            foreach (ListViewItem listItem in listView.Items)
                WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listItem.SubItems[i].Text);

            try
            {
                System.IO.File.WriteAllText(filePath, result.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        private static void WriteCSVRow(StringBuilder result, int itemsCount, Func<int, bool> isColumnNeeded, Func<int, string> columnValue)
        {
            bool isFirstTime = true;
            for (int i = 0; i < itemsCount; i++)
            {
                if (!isColumnNeeded(i))
                    continue;

                if (!isFirstTime)
                    result.Append(",");
                isFirstTime = false;

                result.Append(String.Format("\"{0}\"", columnValue(i)));
            }
            result.AppendLine();
        }

        /// <summary>
        /// 오래된 파일을 삭제합니다
        /// </summary>
        /// <param name="dirName">폴더를 찾을 위치</param>
        /// <param name="dayOld">삭제 조건 일(Day)</param>
        /// <param name="isCheckWriteTIme">True이면 수정 날짜로 체크, False이면 파일 이름 및 폴더 이름으로 체크</param>
        public static void DeleteOldFile(string dirName, int dayOld, bool isCheckWriteTIme = true)
        {
            if (dirName == "")
                return;

            if (!System.IO.Directory.Exists(dirName))
                return;

            string[] files = System.IO.Directory.GetFiles(dirName);

            foreach (string file in files)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(file);

                bool isDelete = false;
                if (isCheckWriteTIme)
                    isDelete = fi.LastWriteTime < DateTime.Now.AddDays(-dayOld);
                else
                {
                    DateTime dt;
                    if (!DateTime.TryParse(fi.Name, out dt))
                        return;

                    isDelete = dt < DateTime.Now.AddDays(-dayOld);
                }

                if (isDelete)
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 해당 일이 지난 폴더를 삭제합니다
        /// </summary>
        /// <param name="dirName">폴더를 찾을 위치</param>
        /// <param name="dayOld">삭제 조건 일(Day)</param>
        /// <param name="isCheckWriteTIme">True이면 수정 날짜로 체크, False이면 파일 이름 및 폴더 이름으로 체크</param>
        public static void DeleteOldDir(string dirName, int dayOld, bool isCheckWriteTIme = true)
        {
            if (dirName == "")
                return;

            if (!System.IO.Directory.Exists(dirName))
                return;

            string[] dirs = System.IO.Directory.GetDirectories(dirName);

            foreach (string dir in dirs)
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);

                bool isDelete = false;
                if (isCheckWriteTIme)
                    isDelete = di.LastWriteTime < DateTime.Now.AddDays(-dayOld);
                else
                {
                    DateTime dt;
                    if (!DateTime.TryParse(di.Name, out dt))
                        return;

                    isDelete = dt < DateTime.Now.AddDays(-dayOld);
                }

                

                if (isDelete)
                {
                    try
                    {
                        di.Delete(true);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 해당 일이 지난 파일을 지정된 폴더로 이동합니다
        /// </summary>
        /// <param name="dirName">파일들을 찾을 위치</param>
        /// <param name="dirBaseTo">년월로 폴더를 생성할 위치</param>
        /// <param name="dayOld">이동 조건 일(Day)</param>
        /// <param name="isCheckWriteTIme">True이면 수정 날짜로 체크, False이면 파일 이름 및 폴더 이름으로 체크</param>
        public static void MoveToMonthDirOldFile(string dirName, string dirBaseTo, int dayOld, bool isCheckWriteTIme = true)
        {
            if (dirName == "")
                return;

            if (!System.IO.Directory.Exists(dirName))
                return;

            string[] files = System.IO.Directory.GetFiles(dirName);

            string dirMoveTo = "";
            foreach (string file in files)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(file);

                bool isMove = false;
                if (isCheckWriteTIme)
                    isMove = fi.LastWriteTime < DateTime.Now.AddDays(-dayOld);
                else
                {
                    DateTime dt;
                    if (!DateTime.TryParse(fi.Name, out dt))
                        return;

                    isMove = dt < DateTime.Now.AddDays(-dayOld);
                }

                if (isMove)
                {
                    try
                    {
                        dirMoveTo = string.Format("{0}\\{1}", dirBaseTo, fi.LastWriteTime.ToString("yyyy-MM"));
                        if (!System.IO.Directory.Exists(dirMoveTo))
                            System.IO.Directory.CreateDirectory(dirMoveTo);

                        fi.MoveTo(string.Format("{0}\\{1}", dirMoveTo, fi.Name));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 팝업 윈도우를 Show로 띄울 때 부모 창 가운데로 이동
        /// </summary>
        /// <param name="parent">부모 창</param>
        /// <param name="popup">자식 창</param>
        public static void SetPosPopupShowParentCenter(Form parent, Form popup)
        {
            int nWidth = popup.ClientSize.Width;
            int nHeight = popup.ClientSize.Height;

            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point((parent.ClientSize.Width / 2) - (nWidth / 2) + parent.Left, (parent.ClientSize.Height / 2) - (nHeight / 2) + parent.Top);
            popup.ClientSize = new Size(nWidth, nHeight);
        }

        /// <summary>
        /// Design Mode에서 실행 중인지
        /// UserConrol 안에 UserControl을 또 사용한다면 DesignMode가 정상 인식되지 않는다
        /// https://support.microsoft.com/ko-kr/kb/839202
        /// </summary>
        public static bool IsInDesigner
        {
            get { return (System.Reflection.Assembly.GetEntryAssembly() == null); }
        }

        /// <summary>
        /// 폴더를 복사합니다
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool overWrite = true)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            //if (!dir.Exists)
            //{
            //    throw new DirectoryNotFoundException(
            //        "Source directory does not exist or could not be found: "
            //        + sourceDirName);
            //}

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, overWrite);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static void FtpUpload(string ftpPath, string user, string pwd, string inputFile)
        {
            //string ftpPath = "ftp://ftp.test.com/home/myindex.txt";
            //string user = "ftpuser";
            //string pwd = "ftppwd";
            //string inputFile = "index.txt";

            // WebRequest.Create로 Http,Ftp,File Request 객체를 모두 생성할 수 있다.
            System.Net.FtpWebRequest req = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(ftpPath);
            // FTP 업로드한다는 것을 표시
            req.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            // 쓰기 권한이 있는 FTP 사용자 로그인 지정
            req.Credentials = new System.Net.NetworkCredential(user, pwd);

            // 입력파일을 바이트 배열로 읽음
            byte[] data;
            using (StreamReader reader = new StreamReader(inputFile))
            {
                data = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            }

            // RequestStream에 데이타를 쓴다
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
            }

            // FTP Upload 실행
            using (System.Net.FtpWebResponse resp = (System.Net.FtpWebResponse)req.GetResponse())
            {
                // FTP 결과 상태 출력
                Console.WriteLine("Upload: {0}", resp.StatusDescription);
            }
        }
    }
}
