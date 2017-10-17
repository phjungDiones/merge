using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TBDB_Handler.GLOBAL;

namespace TBDB_Handler
{
    [Serializable]
    public class ProcessInfo
    {
        public List<ProcInfoAlign> listProcAlign = new List<ProcInfoAlign>();
        public List<ProcInfoLami> listProcLami = new List<ProcInfoLami>();
        public List<ProcInfoBond> listProcBond = new List<ProcInfoBond>();

        #region Serialization
        public bool Serialize(string path)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProcessInfo));
                using (StreamWriter wr = new StreamWriter(path))
                {
                    xmlSerializer.Serialize(wr, this);
                }
            }
            catch (Exception ex)
            {
                //LogMgr.Inst.LogAdd(MCDF.EXCEPT_LOG, ex);
                return false;
            }



            return true;
        }

        public static ProcessInfo Deserialize(string path)
        {
            ProcessInfo inst = null;

            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProcessInfo));
                using (StreamReader rd = new StreamReader(path))
                {
                    inst = (ProcessInfo)xmlSerializer.Deserialize(rd);
                }
            }
            catch (Exception ex)
            {
                //LogMgr.Inst.LogAdd(MCDF.EXCEPT_LOG, ex);
            }

            return inst;
        }
        #endregion
    }
}
