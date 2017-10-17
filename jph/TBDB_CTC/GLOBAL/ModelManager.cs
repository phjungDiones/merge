using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using TBDB_Handler.GLOBAL;

namespace TBDB_Handler.GLOBAL
{
    class ModelManager
    {
        private static ModelManager instance = null;
        private static readonly object padlock = new object();

        private string pathExeFile = string.Empty;
        private string pathName = string.Empty;
        private string FileName = "Loadlock.xml";
        private string dirName = "Setting";

        ModelManager()
        {
        }

        public static ModelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new ModelManager();

                            instance.Init();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 초기화
        /// </summary>
        void Init()
        {
            pathExeFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            pathName = GetPath(dirName);
        }

        private string GetPath(string pathName)
        {
            string path = string.Format("{0}\\{1}\\", pathExeFile, pathName);

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            return path;
        }


        /// <summary>
        /// 파일에 xml형태로 정보를 저장합니다.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>성공 여부</returns>
        protected bool Serialize<T>(string path, WorkModel info)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (StreamWriter wr = new StreamWriter(path))
                {
                    xmlSerializer.Serialize(wr, info);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// xml형태의 파일에서 정보를 불러옵니다.
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <returns>반환 객체</returns>
        public T Deserialize<T>(string path) where T : WorkModel
        {
            T info = null;

            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (StreamReader rd = new StreamReader(path))
                {
                    info = (T)xmlSerializer.Deserialize(rd);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                return null;
            }

            return info;
        }


        public void SaveLoadlockData()
        {
            string fullfilename = string.Format("{0}\\{1}", pathExeFile, FileName);
            Serialize<WorkModel>(fullfilename, GlobalVariable.model);
        }

        public void LoadLoadlockData()
        {
            string fullfilename = string.Format("{0}\\{1}", pathExeFile, FileName);

            GlobalVariable.model = Deserialize<WorkModel>(fullfilename);
            if (GlobalVariable.model == null)
            {
                GlobalVariable.model = new WorkModel();
                SaveLoadlockData();
            }
        }
    }
}
