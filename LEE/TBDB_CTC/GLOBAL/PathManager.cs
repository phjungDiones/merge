using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TBDB_Handler.GLOBAL;

namespace TBDB_Handler.GLOBAL
{
    class PathManager
    {
        private static PathManager instance = null;
        private static readonly object padlock = new object();

        // 폴더 이름
        private string[] dirRefName = new string[] { "Setting", "WorkFile", "Config", "Error", "Log", "Proc" };

        //하위 폴더
        private string[] dirRefLogName = new string[] { "Debug", "Event", "Logging", "LotRun", "WaferRun", "LotProcess", "WaferProcess"};

        // 파일 이름
        private const string filenameSettingAjin = "motor.mot";

        private const string filenameSettingDio  = "Dio.ini";
        private const string filenameProgramInfo = "PgmInfo.ini";
        private const string filenameLfCfgSetting = "McLfCfg.xml";
        private const string filenameRtCfgSetting = "McRtCfg.xml";
        private const string filenameFuncSetting = "McFunc.xml";

        private const string filenameCfgParam = "CfgParam.xml";

        private const string filenameErrDefined = "ErrorDef.ini";

        private const string filenameLastModel = "LastModel.ini";

        //Log File
        private const string filenameLogDebugging = "Debugging.log";

        // 임시 레시피 이름 (확장자 rcp로 찾아야함)
        private const string filenameTempRecipe = "temp.rcp";
        // Process 리스트 정보 파일 이름
        private const string filenameProc = "list.prc";
        private const string filenameProcAligner = "aligner.prc";
        private const string filenameProcLami = "lami.prc";
        private const string filenameProcBond = "bond.prc";

        private string pathExeFile = string.Empty;
        private string[] pathRefName;

        public enum ePathInfo
        {
            eSetting = 0,
            eWorkFile,
            eConfig,
            eError,
            eLog,
            eProc,
        }

        public enum eLogType
        {
            eDebug,
            eEvent,
            eLogging,
            eLotRun,
            eWaferRun,
            eLotProcData,
            eWaferProcData,
        }

        PathManager()
        {
            pathRefName = new string[Enum.GetNames(typeof(ePathInfo)).Length];
        }

        public static PathManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new PathManager();

                            instance.Init();
                        }
                    }
                }
                return instance;
            }
        }

        public string this[ePathInfo index]
        {
            get
            {
                if (instance.pathRefName == null) return null;

                return instance.pathRefName[(int)index];
            }
        }

        /// <summary>
        /// 초기화
        /// </summary>
        void Init()
        {
            pathExeFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            for (int nPathCount = 0; nPathCount < pathRefName.Length; nPathCount++)
            {
                pathRefName[nPathCount] = GetPath(dirRefName[nPathCount]);    
            }
        }

        /// <summary>
        /// 현재 실행 위치에서 pathName을 더한 경로를 반환한다
        /// 경로에 폴더가 없다면 생성한다
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        private string GetPath(string pathName)
        {
            string path = string.Format("{0}\\{1}\\", pathExeFile, pathName);

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            return path;
        }


        public string PATH_CONFIG_PARAM
        {
            get { return string.Format("{0}{1}", pathRefName[(int)ePathInfo.eConfig], filenameCfgParam); }
        }

        public string PATH_LOG_DEBUG
        {
            get { return string.Format("{0}{1}\\{2}", pathRefName[(int)ePathInfo.eLog], dirRefLogName[(int)eLogType.eDebug], filenameLogDebugging); }
        }

        public string FILE_PATH_TEMP_RECIPE
        {
            get { return string.Format("{0}{1}", pathRefName[(int)ePathInfo.eWorkFile], filenameTempRecipe); }
        }

        public string FILE_PATH_PROC
        {
            get { return string.Format("{0}{1}", pathRefName[(int)ePathInfo.eProc], filenameProc); }
        }

        public string FILE_LAST_MODEL_NAME
        {
            get { return string.Format("{0}{1}", pathRefName[(int)ePathInfo.eWorkFile], filenameLastModel); }
        }

        public string FILE_LAST_ERROR_DEF
        {
            get { return string.Format("{0}{1}", pathRefName[(int)ePathInfo.eWorkFile], filenameErrDefined); }
        }
    }
}
