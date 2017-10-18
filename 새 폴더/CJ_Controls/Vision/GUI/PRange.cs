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
    public partial class PRange : Control
    {
        public event ChangeData Change;

        protected double m_dMinL;
        protected double m_dMaxL;
        protected double m_dMinH;
        protected double m_dMaxH;
        protected double m_dCalL;
        protected double m_dCalH;

        protected XRange m_xRange;
        protected Color m_cBolderColor;
        protected Color m_cValueColor;
        protected string m_sFormatL;
        protected string m_sFormatH;

        public int m_nSelect;

        public PRange()
        {
            m_nSelect = 0;
            m_dCalL = 1.0;
            m_dCalH = 1.0;

            m_dMinL = -100000.0;
            m_dMaxL = 100000.0;
            m_dMinH = -100000.0;
            m_dMaxH = 100000.0;

            m_sFormatL = "{0:F1}";
            m_sFormatH = "{0:F1}";
            m_cBolderColor = Color.DarkBlue;
            m_cValueColor = Color.White;
            InitializeComponent();
            m_xRange = new XRange();

        }


        public double CalL
        {
            get
            {
                return m_dCalL;
            }
            set
            {
                m_dCalL = value;
                Refresh();
            }
        }
        public double CalH
        {
            get
            {
                return m_dCalH;
            }
            set
            {
                m_dCalH = value;
                Refresh();
            }
        }

        public double MinL
        {
            get
            {
                return m_dMinL;
            }
            set
            {
                m_dMinL = value;
                Refresh();
            }
        }
        public double MaxL
        {
            get
            {
                return m_dMaxL;
            }
            set
            {
                m_dMaxL = value;
                Refresh();
            }
        }


        public string FormatL
        {
            get
            {
                return m_sFormatL;
            }
            set
            {
                m_sFormatL = value;
                Refresh();
            }
        }


        public double MinH
        {
            get
            {
                return m_dMinH;
            }
            set
            {
                m_dMinH = value;
                Refresh();
            }
        }
        public double MaxH
        {
            get
            {
                return m_dMaxH;
            }
            set
            {
                m_dMaxH = value;
                Refresh();
            }
        }


        public string FormatH
        {
            get
            {
                return m_sFormatH;
            }
            set
            {
                m_sFormatH = value;
                Refresh();
            }
        }

        public XRange Range
        {
            get
            {
                return m_xRange;
            }
            set
            {
                m_xRange = value;
                if (Change != null) Change(null);
                Refresh();
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
            float fWidth = Width / 3.0f - 1.0f;
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
            pe.Graphics.DrawRectangle(BolderPen, fWidth, 0, fWidth, fHeight);

            pe.Graphics.FillRectangle(drawBrushValue, fWidth * 2, 0, Width - fWidth * 2 - 1.0f, fHeight);
            pe.Graphics.DrawRectangle(BolderPen, fWidth * 2, 0, Width - fWidth * 2 - 1.0f, fHeight);


            BolderPen.Dispose();
            drawBrushValue.Dispose();

            if (m_xRange != null)
            {
                {//X
                    RectangleF drawRectValue = new RectangleF(fWidth, 0, fWidth, fHeight);
                    double dVal = m_xRange.L;
                    string sVal = String.Format(m_sFormatL, dVal * m_dCalL);
                    if (m_dMinL <= dVal && dVal <= m_dMaxL)
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
                {//Y
                    RectangleF drawRectValue = new RectangleF(fWidth * 2, 0, fWidth, fHeight);
                    double dVal = m_xRange.H;
                    string sVal = String.Format(m_sFormatH, dVal * m_dCalH);
                    if (m_dMinH <= dVal && dVal <= m_dMaxH)
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


            }

            drawFormat.Dispose();
            drawBrushFore.Dispose();
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (m_xRange != null)
            {
                int nWidth = Width / 3;
                int nIndex = (int)(e.X / nWidth);
                m_nSelect = nIndex;
                if (nIndex == 1)
                {
                    if (m_dCalL != 0)
                    {
                        frmInput fInput = new frmInput();
                        fInput.Value = m_xRange.L * m_dCalL;
                        fInput.Min = m_dMinL;
                        fInput.Max = m_dMaxL;
                        fInput.Format = m_sFormatL;
                        if (DialogResult.OK == fInput.ShowDialog(this))
                        {
                            m_xRange.L = fInput.Value / m_dCalL;
                            if (Change != null) Change(this);
                            Refresh();
                        }
                        fInput.Dispose();
                    }
                }
                if (nIndex == 2)
                {
                    if (m_dCalH != 0)
                    {
                        frmInput fInput = new frmInput();
                        fInput.Value = m_xRange.H * m_dCalH;
                        fInput.Min = m_dMinH;
                        fInput.Max = m_dMaxH;
                        fInput.Format = m_sFormatH;
                        if (DialogResult.OK == fInput.ShowDialog(this))
                        {
                            m_xRange.H = fInput.Value / m_dCalH;
                            if (Change != null) Change(this);
                            Refresh();
                        }
                        fInput.Dispose();
                    }
                }



            }
        }
    }
}
