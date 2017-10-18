using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TBDB_Handler.GLOBAL
{
    class CfgManager
    {
        private static CfgManager instance = null;
        private static readonly object padlock = new object();

        // 파일 이름
        private string CfgFileName = string.Empty;

        CfgManager()
        {
        }

        public static CfgManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new CfgManager();

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
            CfgFileName = PathManager.Instance.PATH_CONFIG_PARAM;
        }

        public void LoadConfigFile()
        {
            Config cfg = Deserialize<Config>(CfgFileName);
            if (cfg == null)
            {
                cfg = new Config();
                // 초기화
            }

            GlobalVariable.cfg = cfg;
        }

        public void SaveConfigFile()
        {
            if (!Serialize<Config>(CfgFileName, GlobalVariable.cfg))
            {
                // 실패
            }
        }

        /// <summary>
        /// 파일에 xml형태로 정보를 저장합니다.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>성공 여부</returns>
        protected bool Serialize<T>(string path, Config cfg)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (StreamWriter wr = new StreamWriter(path))
                {
                    xmlSerializer.Serialize(wr, cfg);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                //if (deleException != null)
                //    deleException(ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// xml형태의 파일에서 정보를 불러옵니다.
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <returns>반환 객체</returns>
        protected T Deserialize<T>(string path) where T : Config
        {
            T portInfo = null;

            if (!File.Exists(path))
            {
                return null;
            }

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (StreamReader rd = new StreamReader(path))
                {
                    portInfo = (T)xmlSerializer.Deserialize(rd);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString()    );
                //if (deleException != null)
                //    deleException(ex.ToString());
                return null;
            }

            return portInfo;
        }
    }
}
