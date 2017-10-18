using System;
using System.IO;
using System.Text;

namespace TBDB_CTC.FileLib
{
    public class LogWrite
    {
        public string Filepath { get; set; }
        private static object locker = new Object();

        public void WriteToFile(StringBuilder text)
        {
            lock (locker)
            {
                using (FileStream file = new FileStream(Filepath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                {
                    writer.Write(text.ToString() + "\r\n");
                }
            }
        }
    }
}