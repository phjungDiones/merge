using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CJ_Controls.Vision.BaseData;
namespace CJ_Controls.Vision.GUI
{
    public partial class frmInput : Form
    {

        protected double m_dMin;
        protected double m_dMax;
        protected double m_dValue;
        protected string m_sFormat;

        public frmInput()
        {
            InitializeComponent();
        }

        public string Format
        {
            get
            {
                return m_sFormat;
            }
            set
            {
                m_sFormat = value;

            }
        }

        public double Min
        {
            get
            {
                return m_dMin;
            }
            set
            {
                m_dMin = value;

            }
        }
        public double Max
        {
            get
            {
                return m_dMax;
            }
            set
            {
                m_dMax = value;

            }
        }

        public double Value
        {
            get
            {
                return m_dValue;
            }
            set
            {
                m_dValue = value;

            }
        }



        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_dValue = Convert.ToDouble(txtValue.Text);
                if (m_dMin <= m_dValue && m_dValue <= m_dMax)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("This value is over range. Please! Retry input.");
                }
            }
            catch (FormatException fe)
            {

                MessageBox.Show(fe.Message + " Please! Retry input.");

            }
        }

        private void Input(string sIn)
        {
            int nSelectStart = txtValue.SelectionStart;
            int nSelectLength = txtValue.SelectionLength;

            if (nSelectLength != 0)
            {
                string sText = txtValue.Text;
                string sSelect = sText.Substring(nSelectStart, nSelectLength);
                txtValue.Text = sText.Replace(sSelect, sIn);
            }
            else
            {
                txtValue.Text += sIn;
            }
        }
        private void cmd00_Click(object sender, EventArgs e)
        {
            Input("0");
        }

        private void cmd01_Click(object sender, EventArgs e)
        {
            Input("1");
        }

        private void cmd02_Click(object sender, EventArgs e)
        {
            Input("2");
        }

        private void cmd03_Click(object sender, EventArgs e)
        {
            Input("3");
        }

        private void cmd04_Click(object sender, EventArgs e)
        {
            Input("4");
        }

        private void cmd05_Click(object sender, EventArgs e)
        {
            Input("5");
        }

        private void cmd06_Click(object sender, EventArgs e)
        {
            Input("6");
        }

        private void cmd07_Click(object sender, EventArgs e)
        {
            Input("7");
        }

        private void cmd08_Click(object sender, EventArgs e)
        {
            Input("8");
        }

        private void cmd09_Click(object sender, EventArgs e)
        {
            Input("9");
        }

        private void cmdP_Click(object sender, EventArgs e)
        {
            Input(".");
        }

        private void cmdM_Click(object sender, EventArgs e)
        {
            Input("-");
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            labMin.Text = String.Format(m_sFormat, m_dMin);
            labMax.Text = String.Format(m_sFormat, m_dMax);
            txtValue.Text = String.Format(m_sFormat, m_dValue);


        }

        private void cmdDel_Click(object sender, EventArgs e)
        {
            txtValue.Text = "";
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            int nLen = txtValue.Text.Length;
            if (nLen > 0)
            {
                txtValue.Text = txtValue.Text.Substring(0, nLen - 1);
            }
        }


        private void frmInput_Shown(object sender, EventArgs e)
        {
            txtValue.Select();
            txtValue.SelectAll();
        }


    }
}
