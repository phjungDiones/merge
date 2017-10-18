using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBDB_CTC.GLOBAL;
using TBDB_CTC.GUI;
using TBDB_Handler.THREAD;
using TBDB_CTC.POPWND;
using System.Data;

namespace TBDB_Handler.GLOBAL
{
    
    public static class GlobalVariable
    {
        
        public static McStage mcState = new McStage();
        public static ManualInfo manualInfo = new ManualInfo();

        //Loadlock 모터설정
        public static LoadlockMotor Loadlock = new LoadlockMotor();
        public static WorkModel model = new WorkModel();
        public static WaferInfo WaferInfo = new WaferInfo();
        public static IOManager io = new IOManager();
        public static Interlock interlock = new Interlock();

        //Recipe
        public static PMC_RECIPE pmc_Rcp = new PMC_RECIPE();
        public static LAMI_RECIPE lami_Rcp = new LAMI_RECIPE();
        public static ALIGNER_RECIPE aligner_Rcp = new ALIGNER_RECIPE();

        //Config Data
        public static Config cfg = new Config();

        public static SeqShared seqShared = new SeqShared();
    }

    public static class GlobalForm
    {
        public static frmLogIn fLogin = null;
        public static frmMainView fMain = null;
        public static frmSemiAUto fAuto = null;
        public static frmManual fManual = null;
        public static frmConfig fCfg = null;
        public static frmIO fIO = null;
        public static frmAlarm fAlarm = null;
        public static frmHistory fHistory = null;
        public static frmRecipe fRcp = null;
        public static popErrMessage fErr = null;
        public static frmTestForm fTest = null;
        public static frmAlarm fmAlarm = null;
    }

    public static class GlobalSeq
    {
        public static AutoRun autoRun;
        public static ManualRun manualRun = null;
    }

    public static class GlobalDataSet
    {
        //파일경로에 데이터셋이 존재하지않는다면
        public static DataSet dataset = new DataSet("dataset");
        public static DataSet dataset2 = new DataSet("dataset");

    }



}
