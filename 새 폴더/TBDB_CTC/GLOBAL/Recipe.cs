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
    public class Recipe
    {
        //public bool[] bIsUseSlot = new bool[MCDF.MAX_SLOT_COUNT];
        public int nMaxUseSlot = 0;
        public RUN_MODE eRunMode = RUN_MODE.FULL;

        public string strPreAlignCarrier = "";
        public string strPreAlignDevice = "";
        public string strPostAlignCarrier = "";
        public string strLamiCondition = "";
        public string strBondCondition = "";

        public double dHpTime = 0.0;

        public bool bUsePreAlignCarrier;
        public bool bUsePreAlignDevice;
        public bool bUsePostAlignCarrier;
        public bool bUseHP;

        #region Serialization
        public bool Serialize(string path)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Recipe));
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

        public static Recipe Deserialize(string path)
        {
            Recipe inst = null;

            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Recipe));
                using (StreamReader rd = new StreamReader(path))
                {
                    inst = (Recipe)xmlSerializer.Deserialize(rd);
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
