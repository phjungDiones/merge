﻿using System;

//using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TBDB_CTC.FileLib
{
    public class fileUtil
    {
        public delegate void DeleException(string strErr);
        public event DeleException deleException = null;

        /// <summary>
        /// 문자열에서 특정문자 갯수 확인
        /// </summary>
        /// <param name="strTotal"></param>
        /// <param name="strFind"></param>
        /// <returns></returns>
        public int GetStringcount(string strTotal, string strFind)
        {
            System.Text.RegularExpressions.Regex cntStr = new System.Text.RegularExpressions.Regex(strFind);
            return int.Parse(cntStr.Matches(strTotal, 0).Count.ToString());
        }

        /// <summary>
        /// clipboard to image
        /// </summary>
        /// <param name="TargetFileName"></param>
        /// <returns></returns>
        public bool SaveClipboardImg(string TargetFileName)
        {
            try
            {
                IDataObject ob = null;
                PictureBox pic = new PictureBox();

                ob = Clipboard.GetDataObject();
                pic.Image = (Image)ob.GetData(DataFormats.Bitmap);
                Image Im = pic.Image;

                if (Im == null)
                    return false;

                FileStream fs = new FileStream(TargetFileName, FileMode.Create, FileAccess.ReadWrite);
                Im.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                if (deleException != null)
                    deleException(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 문자열 안에서 숫자만 뽑기
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string parseInt(string str)
        {
            string strRet = "";
            strRet = Regex.Replace(str, @"\D", ""); // 닷넷 정규식에서 \d는 숫자. \D는 숫자가 아닌 문자를 의미
            return strRet;
        }

        /// <summary>
        /// 문자열 안에서 숫자를 제외한 문자만 뽑기
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string parseStr(string str)
        {
            string strRet = "";
            strRet = Regex.Replace(str, @"\d", ""); // 닷넷 정규식에서 \d는 숫자. \D는 숫자가 아닌 문자를 의미
            return strRet;
        }

        /// <summary>
        /// 파일경로 에서 파일명 얻기
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        public string getFileName(string strFile)
        {
            string[] nameArr = strFile.Split('\\');
            //마지막으로 스플릿된 문자열이 파일이름
            string fileName = nameArr[nameArr.Length - 1];
            return fileName;
        }

        /// <summary>
        /// 파일경로 에서 확장자 얻기
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        public string getFileExtension(string strFile)
        {
            string[] extArr = strFile.Split('.');
            string fileExt = extArr[extArr.Length - 1];
            return fileExt;
        }

        /// <summary>
        /// 두 날짜의 차이 구하기 (날짜 포맷 : 2015-04-30)
        /// </summary>
        /// <param name="strSDate"></param>
        /// <param name="strEDate"></param>
        /// <returns></returns>
        public int getBetweenDaybyDay(string strSDate, string strEDate)
        {
            DateTime T1 = DateTime.Parse(strSDate);
            DateTime T2 = DateTime.Parse(strEDate);
            TimeSpan TS = T2 - T1;
            int nCnt = Convert.ToInt32(TS.TotalDays.ToString());
            return nCnt;
        }

        /// <summary>
        /// 파일내용 안에 같은 문자열의 존재 확인
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="strCompareString"></param>
        /// <returns></returns>
        public bool isExistsStringInFile(string strFilePath, string strCompareString)
        {
            StreamReader streamReader = new StreamReader(strFilePath);
            string text = streamReader.ReadToEnd();
            streamReader.Close();

            return text.Contains(strCompareString);
        }

        /// <summary>
        /// 현재 exe실행 경로얻기
        /// </summary>
        /// <returns></returns>
        public string getExecutablePath()
        {
            return System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        }

        /// <summary>
        /// File size 확인
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public bool getFileSize(string strPath, ref int size)
        {
            size += 244;
            return true;
        }

        /// <summary>
        /// 로그 파일 삭제 - ??월 미만에 만들어진 파일 삭제
        /// </summary>
        /// <param name="keepMonth">유지 월(그 이상은 삭제)</param>
        public void deleteLogByMonth(string folderPath, int keepMonth)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Extension != ".log") continue;

                // 한달 이전 로그를 삭제.
                //if (fi.CreationTime <= DateTime.Now.AddMonths(-1))
                if (fi.CreationTime <= DateTime.Now.AddDays(-keepMonth))
                {
                    fi.Delete();
                }
            }
        }
    }
}