using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TBDB_Handler.GLOBAL;

namespace TBDB_Handler
{
    public class RecipeMgr
    {
        private static volatile RecipeMgr instance;
        private static object syncRoot = new Object();

        public Recipe Rcp;
        public Recipe TempRcp;

        public static RecipeMgr Inst
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RecipeMgr();
                    }
                }

                return instance;
            }
        }

        public void Load()
        {
            string strPath = string.Format("{0}{1}", PathManager.Instance[PathManager.ePathInfo.eWorkFile], LAST_MODEL_NAME);
            Rcp = Recipe.Deserialize(strPath);
            if (Rcp == null)
            {
                Rcp = new Recipe();
            }
            CheckValidation(Rcp);
            Rcp.Serialize(strPath);

            TempRcp = Recipe.Deserialize(strPath);
            if (TempRcp == null)
            {
                TempRcp = new Recipe();
            }
        }

        void CheckValidation(Recipe rcp)
        {
            //
        }

        public void Save()
        {
            //string strPath = PathManager.Instance.FILE_PATH_TEMP_RECIPE;
            string strPath = string.Format("{0}{1}", PathManager.Instance[PathManager.ePathInfo.eWorkFile], LAST_MODEL_NAME);
            Rcp.Serialize(strPath);
        }

        public void CopyTempRcp()
        {
            TempRcp = CreateDeepCopy(Rcp);
        }

        public void SaveTempRcp()
        {
            Rcp = CreateDeepCopy(TempRcp);
        }

        private Recipe CreateDeepCopy(Recipe inputcls)
        {
            MemoryStream m = new MemoryStream();
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(m, inputcls);
            m.Position = 0;
            return (Recipe)b.Deserialize(m);
        }

        public void ChangeModelFile(string ChnageFileName)
        {
            if (!ChnageFileName.Contains(".rcp"))
                ChnageFileName = ChnageFileName + ".rcp";

            LAST_MODEL_NAME = ChnageFileName;
            ProcessMgr.Inst.Load(); 
            RecipeMgr.Inst.Load();
        }

        public void DeleteModelFile(string NewFileName)
        {
            if (!NewFileName.Contains(".rcp"))
                NewFileName = NewFileName + ".rcp";

            if (LAST_MODEL_NAME != NewFileName)
            {
                string delFile = string.Format("{0}{1}", PathManager.Instance[PathManager.ePathInfo.eWorkFile], NewFileName);
                System.IO.File.Delete(delFile);


                // Patternfile 삭제
                //System.IO.Directory.Delete(PathManager.Instance[PathManager.ePathInfo.ePattern] + NewFileName, true);
            }
        }
        public bool CopyToModel(string sDestFileName)
        {
            string sourceFile = string.Format("{0}{1}", PathManager.Instance[PathManager.ePathInfo.eWorkFile], LAST_MODEL_NAME);
            string destFile = string.Format("{0}{1}", PathManager.Instance[PathManager.ePathInfo.eWorkFile], sDestFileName);

            try
            {
                if (System.IO.File.Exists(sourceFile))
                {
                    System.IO.File.Copy(sourceFile, destFile, true);
                }
            }
            catch (Exception ex)
            {
                //
                return false;
            }

            return true;
        }

        public string LAST_MODEL_NAME
        {
            set
            {
                TBDB_CTC.FileLib.iniUtil ini = new TBDB_CTC.FileLib.iniUtil(PathManager.Instance.FILE_LAST_MODEL_NAME);
                ini.SetIniValue("LAST_MDDEL", "LAST_MODEL_NAME", value);
            }
            get
            {
                TBDB_CTC.FileLib.iniUtil ini = new TBDB_CTC.FileLib.iniUtil(PathManager.Instance.FILE_LAST_MODEL_NAME);
                return ini.GetIniValue("LAST_MDDEL", "LAST_MODEL_NAME", "");
            }
        }
    }
}
