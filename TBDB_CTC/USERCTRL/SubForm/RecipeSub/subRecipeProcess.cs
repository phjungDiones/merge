using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBDB_CTC.UserCtrl.SubForm.RecipeSub
{
    public partial class subRecipeProcess : UserControl
    {
        public subRecipeProcess()
        {
            InitializeComponent();
        }

        public void SubMenuChange(int nSubMenuNo)
        {
            tbcProcessRecipe.SelectedIndex = nSubMenuNo;
        }
    }
}
