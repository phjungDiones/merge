using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBDB_CTC.POPWND.Error.Global
{
    public class Cause_action
    {
        public string message;
        public string cause;
        public string action;
        public Cause_action(string m,string c,string a)
        {
            message = m;
            cause = c;
            action = a;
        }
    }
}
