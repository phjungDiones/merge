using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBDB_CTC.Comm.Lami_PLC;
using TBDB_CTC.Data;

namespace TBDB_Handler.MOTION
{
    public class MotionLaminator
    {
        private MainData _Main = null;

        public MotionLaminator()
        {
            _Main = MainData.Instance;
        }

        public short ReadAddrData(LAMI_PLC_ADDR _Addr)
        {
            return _Main.GetLaminatorData().Laminator.Read_Lami_Bit(_Addr);
        }

        public void WriteAddrData(CTC_PLC_ADDR _Addr, bool bOnOff)
        {
            _Main.GetLaminatorData().Laminator.Send_CTC_To_Lami(_Addr, bOnOff);
        }

        public void WriteRecipeData(RECIPE_PLC_ADDR rcp, short sVal)
        {
            _Main.GetLaminatorData().Laminator.SetRecipe(rcp, sVal);
        }

        public void SendRcp(bool bAct)
        {
            _Main.GetLaminatorData().Laminator.WriteRecipe_Start = bAct;
        }
    }
}
