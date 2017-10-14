using System.Runtime.InteropServices;
using System.Text;

namespace TBDB_CTC.FileLib
{
    public class iniUtil
    {
        private string iniPath;

        public iniUtil(string path)
        {
            this.iniPath = path;
        }

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath);

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(  // SetIniValue를 위해
            string section,
            string key,
            string val,
            string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileStringW(
            string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileStringW(  // SetIniValue를 위해
            string section,
            string key,
            string val,
            string filePath);

        /// <summary>
        /// Read ini
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetIniValue(string Section, string Key, string Default, bool replaceBlank = true)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, Default, temp, 255, iniPath);
            string Value = temp.ToString();
            if (replaceBlank) Value = Value.Replace("&nbsp;", " ");
            return Value;
        }

        /// <summary>
        /// Write ini
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void SetIniValue(string Section, string Key, string Value, bool replaceBlank = true)
        {
            if (replaceBlank) Value = Value.Replace(" ", "&nbsp;");

            WritePrivateProfileString(Section, Key, Value, iniPath);
        }

        /// <summary>
        /// Read ini
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetIniValueW(string Section, string Key, string Default, bool replaceBlank = true)
        {
            StringBuilder temp = new StringBuilder(4096);
            int i = GetPrivateProfileStringW(Section, Key, Default, temp, 4096, iniPath);
            string Value = temp.ToString();
            if (replaceBlank) Value = Value.Replace("&nbsp;", " ");
            return Value;
        }

        /// <summary>
        /// Write ini
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void SetIniValueW(string Section, string Key, string Value, bool replaceBlank = true)
        {
            if (replaceBlank) Value = Value.Replace(" ", "&nbsp;");

            WritePrivateProfileStringW(Section, Key, Value, iniPath);
        }
    }
}