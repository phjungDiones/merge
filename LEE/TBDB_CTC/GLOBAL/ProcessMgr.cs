using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TBDB_Handler.GLOBAL;

namespace TBDB_Handler
{
    public class ProcessMgr
    {
        private static volatile ProcessMgr instance;
        private static object syncRoot = new Object();

        public ProcessInfo Pinfo;
        public ProcessInfo TempPinfo;

        public static ProcessMgr Inst
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ProcessMgr();
                    }
                }

                return instance;
            }
        }

        public void Load()
        {
            string strPath = PathManager.Instance.FILE_PATH_PROC;
            Pinfo = ProcessInfo.Deserialize(strPath);
            if (Pinfo == null)
            {
                Pinfo = new ProcessInfo();
            }
            Pinfo.Serialize(strPath);

            TempPinfo = ProcessInfo.Deserialize(strPath);
            if (TempPinfo == null)
            {
                TempPinfo = new ProcessInfo();
            }
        }

        public void Save()
        {
            string strPath = PathManager.Instance.FILE_PATH_PROC;
            Pinfo.Serialize(strPath);
        }

        public void CopyTempRcp()
        {
            TempPinfo = CreateDeepCopy(Pinfo);
        }

        public void SaveTempRcp()
        {
            Pinfo = CreateDeepCopy(TempPinfo);
        }

        private ProcessInfo CreateDeepCopy(ProcessInfo inputcls)
        {
            MemoryStream m = new MemoryStream();
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(m, inputcls);
            m.Position = 0;
            return (ProcessInfo)b.Deserialize(m);
        }
    }
}
