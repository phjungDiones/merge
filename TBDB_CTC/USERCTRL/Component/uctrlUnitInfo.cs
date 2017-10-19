using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBDB_CTC.UserCtrl
{
    public partial class uctrlUnitInfo : UserControl
    {
        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string UnitName
        {
            get { return lbUnitName.Text; }
            set
            {
                if (lbUnitName.Text == value) return;

                lbUnitName.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string RecipeName
        {
            get { return lbRecipeName.Text; }
            set
            {
                if (lbRecipeName.Text == value) return;

                lbRecipeName.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string StepName
        {
            get { return lbStepName.Text; }
            set
            {
                if (lbStepName.Text == value) return;

                lbStepName.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string RecipeMsg
        {
            get { return lbRecipeMsg.Text; }
            set
            {
                if (lbRecipeMsg.Text == value) return;

                lbRecipeMsg.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string StepMsg
        {
            get { return lbStepMsg.Text; }
            set
            {
                if (lbStepMsg.Text == value) return;

                lbStepMsg.Text = value;
                this.Invalidate();
            }
        }


        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string StepTime
        {
            get { return lbStepTime.Text; }
            set
            {
                if (lbStepTime.Text == value) return;

                lbStepTime.Text = value;
                this.Invalidate();
            }
        }

        [Category("Advanced")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string RecipeTime
        {
            get { return lbRecipeTime.Text; }
            set
            {
                if (lbRecipeTime.Text == value) return;

                lbRecipeTime.Text = value;
                this.Invalidate();
            }
        }


//         [Category("Advanced")]
//         [Browsable(true)]
//         [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
//         [EditorBrowsable(EditorBrowsableState.Always)]
//         public ProgressBar ProgrsProc
//         {
//             get { return prgProcess}
//             set
//             {
//                 prgProcess.Value = value;
//                 this.Invalidate();
//             }
//         }




        public uctrlUnitInfo()
        {
            InitializeComponent();
        }
    }
}
