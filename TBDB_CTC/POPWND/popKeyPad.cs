using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBDB_Handler.GLOBAL;

namespace TBDB_CTC.POPWND
{
    public partial class popKeyPad : Form
    {
        bool firstchar;
        string function = "";
        bool _decimal;
        double num1;
        double num2;
        DateTime _start = DateTime.MinValue;
        TimeSpan _Elapse = TimeSpan.Zero;
        string Elapse;
        bool Reverse;
        public popKeyPad()
        {
            InitializeComponent();
        }
        private void Btn_Dec_Click(object sender, EventArgs e)
        {

            if (!_decimal)
            {
                if (Txt_Out.Text == "")
                {
                    Txt_Out.Text = "0.";
                }
                else
                {
                    if (Txt_Out.Text != "0")
                    {
                        Txt_Out.Text += Btn_Dec.Text;
                    }
                    else
                    {
                        Txt_Out.Text = "0.";
                    }
                }
                _decimal = true;
                firstchar = true;
            }
        }
        private void Btn_Eq_Click(object sender, EventArgs e)
        {
            if (Txt_Out.Text.Length != 0)
            {
                if (function != "")
                {
                    Calc();
                }
            }
            else
            {
                Txt_Out.Text = Txt_Out.Text;
            }
        }
        private void Calc()
        {
            num2 = System.Double.Parse(Txt_Out.Text);
            switch (function)
            {
                case "Add":
                    Txt_Out.Text = (num1 + num2).ToString();
                    num1 = num1 + num2;
                    break;

                case "Sub":
                    Txt_Out.Text = (num1 - num2).ToString();
                    num1 = num1 - num2;
                    break;

                case "Mul":
                    Txt_Out.Text = (num1 * num2).ToString();
                    num1 = num2 * num2;
                    break;

                case "Div":
                    Txt_Out.Text = (num1 / num2).ToString();
                    num1 = num1 / num2;
                    break;
                case "SqRoot":
                    Txt_Out.Text = Math.Sqrt(num1).ToString();
                    break;
            }
            firstchar = false;
            function = "";
            int avail = Txt_Out.Text.IndexOf(".");
            if (avail != -1)
            {
                _decimal = true;
            }
            else
            {
                _decimal = false;
            }
        }
        private void Btn_Div_Click(object sender, EventArgs e)
        {
            if (Txt_Out.Text.Length != 0)
            {
                if (function == "")
                {
                    num1 = System.Double.Parse(Txt_Out.Text);
                    Txt_Out.Text = string.Empty;
                }
                else
                {
                    Calc();
                }
                function = "Div";
                _decimal = false;
            }
        }
        private void Btn_Mul_Click(object sender, EventArgs e)
        {
            if (Txt_Out.Text.Length != 0)
            {
                if (function == "")
                {
                    num1 = System.Double.Parse(Txt_Out.Text);
                    Txt_Out.Text = string.Empty;
                }
                else
                {
                    Calc();
                }
                function = "Mul";
                _decimal = false;
            }
        }
        private void Btn_Sub_Click(object sender, EventArgs e)
        {
            if (Txt_Out.Text.Length != 0)
            {
                if (function == "")
                {
                    num1 = System.Double.Parse(Txt_Out.Text);
                    Txt_Out.Text = string.Empty;
                }
                else
                {
                    Calc();
                }
                function = "Sub";
                _decimal = false;
            }
        }
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            if (Txt_Out.Text.Length != 0)
            {
                if (function == "")
                {
                    num1 = System.Double.Parse(Txt_Out.Text);
                    Txt_Out.Text = string.Empty;
                }
                else
                {
                    Calc();
                }
                function = "Add";
                _decimal = false;
            }
        }
        private void Btn_Sqroot_Click(object sender, EventArgs e)
        {
            if (Txt_Out.Text.Length != 0)
            {
                function = "SqRoot";
                num1 = System.Double.Parse(Txt_Out.Text);
                Calc();
            }
        }






















        private void popKeyPad_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void win_GlassButton2_Click(object sender, EventArgs e)
        {
            double.TryParse(Txt_Out.Text, out ReturnNumber.retNum);
            Txt_Out.Text = "";
            this.DialogResult = DialogResult.OK;
            
        }

        private void win_GlassButton1_Click(object sender, EventArgs e)
        {
            double.TryParse("0", out ReturnNumber.retNum);
            Txt_Out.Text = "";
            this.DialogResult = DialogResult.Cancel;
        }

        private void Btn_CE_Click(object sender, EventArgs e)
        {
            Txt_Out.Text = "0";
            _decimal = false;
            function = "";
            num1 = 0;
            num2 = 0;
            //Screensaver_Reset();
            //Run();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Button Btn = (Button)sender;
            if (firstchar)
            {
                Txt_Out.Text += Btn.Text;
            }
            else
            {
                Txt_Out.Text = Btn.Text;
                firstchar = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }
    }
}
