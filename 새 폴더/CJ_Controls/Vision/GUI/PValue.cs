using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using CJ_Controls.Vision.BaseData;
namespace CJ_Controls.Vision.GUI
{
    public partial class PValue : Control
    {

        public event ChangeData Change;

        private string m_sType;
        private object m_Object;
        private FieldInfo m_Field;

        protected double m_dCal;
        protected double m_dMin;
        protected double m_dMax;
        protected Color m_cBolderColor;
        protected Color m_cValueColor;
        protected string m_sFormat;
        protected double m_dNom;

        public PValue()
        {
            m_dCal = 1.0;
            m_dNom = 0.0;
            m_dMin = -100000.0;
            m_dMax = 100000.0;
            m_sFormat = "{0:F0}";
            m_cBolderColor = Color.DarkBlue;
            m_cValueColor = Color.White;
            InitializeComponent();
        }
        public double Cal
        {
            get
            {
                return m_dCal;
            }
            set
            {
                m_dCal = value;
                Refresh();
            }
        }
        public double Nom
        {
            get
            {
                return m_dNom;
            }
            set
            {
                m_dNom = value;
                Refresh();
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
                Refresh();
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
                Refresh();
            }
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
                Refresh();
            }
        }

        public void SetField(object _Class, string sFieldName)
        {
            if(_Class != null)
            {
                Type Type = _Class.GetType();
                m_Field = Type.GetField(sFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
                if (m_Field != null)
                {
                    m_sType = m_Field.FieldType.Name;
                    m_Object = _Class;
                }
            }
            Refresh();
        }



        public object Value
        {
            get
            {
                if (m_Field != null && m_Object != null)
                {
                    return m_Field.GetValue(m_Object);
                }
                return 0.0;
            }
            set
            {
                if (m_Field != null && m_Object != null)
                {
                    m_Field.SetValue(m_Object, value);
                    if (Change != null) Change(null);
                    Refresh();
                }
            }



        }



        public Color BolderColor
        {
            get
            {
                return m_cBolderColor;
            }
            set
            {
                m_cBolderColor = value;
                Refresh();
            }
        }
        public Color ValueColor
        {
            get
            {
                return m_cValueColor;
            }
            set
            {
                m_cValueColor = value;
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);


            // Create font and brush.
            SolidBrush drawBrushFore = new SolidBrush(this.ForeColor);

            // Create rectangle for drawing.
            float fWidth = Width / 2.0f - 1.0f;
            float fHeight = Height - 1.0f;

            RectangleF drawRectCaption = new RectangleF(0, 0, fWidth, fHeight);

            Pen BolderPen = new Pen(m_cBolderColor);
            pe.Graphics.DrawRectangle(BolderPen, 0, 0, fWidth, fHeight);

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            pe.Graphics.DrawString(this.Text, this.Font, drawBrushFore, drawRectCaption, drawFormat);

            SolidBrush drawBrushValue = new SolidBrush(m_cValueColor);
            pe.Graphics.FillRectangle(drawBrushValue, fWidth, 0, fWidth, fHeight);
            pe.Graphics.DrawRectangle(BolderPen, fWidth, 0, Width - fWidth - 1.0f, fHeight);

            BolderPen.Dispose();
            drawBrushValue.Dispose();

            RectangleF drawRectValue = new RectangleF(fWidth, 0, fWidth, fHeight);
            if (m_Object != null)
            {
                double dVal = 0;
                string sVal = "";

                switch (m_sType)
                {
                    case "Int32":
                        {
                            int nVal = (int)Value;
                            dVal = nVal;
                            sVal = String.Format(m_sFormat, (int)(dVal * m_dCal));
                        } break;

                    case "Double":
                        {
                            dVal = (double)Value;
                            sVal = String.Format(m_sFormat, dVal * m_dCal);
                        } break;
                }
                if (m_dMin <= dVal && dVal <= m_dMax)
                {
                    pe.Graphics.DrawString(sVal, this.Font, drawBrushFore, drawRectValue, drawFormat);
                }
                else
                {
                    SolidBrush drawBrushError = new SolidBrush(Color.Red);
                    pe.Graphics.DrawString(sVal, this.Font, drawBrushError, drawRectValue, drawFormat);
                    drawBrushError.Dispose();
                }
            }

            drawFormat.Dispose();
            drawBrushFore.Dispose();
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (m_Object != null)
            {
                if (m_dCal != 0)
                {
                    int nWidth = Width / 2;
                    if ((e.X / nWidth) == 1)
                    {

                        frmInput fInput = new frmInput();

                        switch (m_sType)
                        {
                            case "Int16":
                                {
                                    int nVal = (int)Value;
                                    fInput.Value = nVal * m_dCal;
                                } break;

                            case "Int32":
                                {
                                    int nVal = (int)Value;
                                    fInput.Value = nVal * m_dCal;
                                } break;

                            case "Double":
                                {
                                    double dVal = (double)Value;
                                    fInput.Value = dVal * m_dCal;
                                } break;
                        }


                        fInput.Min = m_dMin;
                        fInput.Max = m_dMax;
                        fInput.Format = m_sFormat;
                        if (DialogResult.OK == fInput.ShowDialog(this))
                        {
                            double dVal = fInput.Value / m_dCal;

                            switch (m_sType)
                            {
                                case "Int16":
                                    {
                                        int nVal = (int)dVal;
                                        Value = nVal;
                                    } break;
                                case "Int32":
                                    {
                                        int nVal = (int)dVal;
                                        Value = nVal;
                                    } break;

                                case "Double":
                                    {
                                        Value = dVal;
                                    } break;
                            }
                            if (Change != null) Change(this);
                            Refresh();
                        }
                        fInput.Dispose();
                    }
                    else
                    {
                        string sMsg;
                        sMsg = string.Format("Set = " + m_sFormat + " Are you sure?", m_dNom * m_dCal);
                        if (DialogResult.OK == MessageBox.Show(sMsg, Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                        {
                            switch (m_sType)
                            {
                                case "Int16":
                                    {
                                        int nVal = (int)m_dNom;
                                        Value = nVal;
                                    } break;
                                case "Int32":
                                    {
                                        int nVal = (int)m_dNom;
                                        Value = nVal;
                                    } break;

                                case "Double":
                                    {
                                        Value = m_dNom;
                                    } break;
                            }

                            if (Change != null) Change(this);
                            Refresh();
                        }
                    }
                }
            }
        }
    }
}
