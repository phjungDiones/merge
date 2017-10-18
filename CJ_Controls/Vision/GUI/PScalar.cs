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
    public delegate void ChangeData(Control sender);

    public partial class PScalar : Control
    {
        public event ChangeData Change;
        protected double m_dMinX;
        protected double m_dMaxX;
        protected double m_dMinY;
        protected double m_dMaxY;
        protected double m_dCalX;
        protected double m_dCalY;

        protected XScalar m_xScalar;
        protected Color m_cBolderColor;
        protected Color m_cValueColor;
        protected string m_sFormatX;
        protected string m_sFormatY;

        public int m_nSelect;

        protected double m_dNomX;
        protected double m_dNomY;

        public PScalar()
        {
            m_nSelect = 0;
            m_dCalX = 1.0;
            m_dCalY = 1.0;

            m_dNomX = 0.0;
            m_dNomY = 0.0;

            m_dMinX = -100000.0;
            m_dMaxX = 100000.0;
            m_dMinY = -100000.0;
            m_dMaxY = 100000.0;

            m_sFormatX = "{0:F1}";
            m_sFormatY = "{0:F1}";
            m_cBolderColor = Color.DarkBlue;
            m_cValueColor = Color.White;
            InitializeComponent();

            m_xScalar = new XScalar();

        }


        public double CalX
        {
            get
            {
                return m_dCalX;
            }
            set
            {
                m_dCalX = value;
                Refresh();
            }
        }
        public double CalY
        {
            get
            {
                return m_dCalY;
            }
            set
            {
                m_dCalY = value;
                Refresh();
            }
        }


        public double NomX
        {
            get
            {
                return m_dNomX;
            }
            set
            {
                m_dNomX = value;
                Refresh();
            }
        }
        public double NomY
        {
            get
            {
                return m_dNomY;
            }
            set
            {
                m_dNomY = value;
                Refresh();
            }
        }

        public double MinX
        {
            get
            {
                return m_dMinX;
            }
            set
            {
                m_dMinX = value;
                Refresh();
            }
        }
        public double MaxX
        {
            get
            {
                return m_dMaxX;
            }
            set
            {
                m_dMaxX = value;
                Refresh();
            }
        }


        public string FormatX
        {
            get
            {
                return m_sFormatX;
            }
            set
            {
                m_sFormatX = value;
                Refresh();
            }
        }


        public double MinY
        {
            get
            {
                return m_dMinY;
            }
            set
            {
                m_dMinY = value;
                Refresh();
            }
        }
        public double MaxY
        {
            get
            {
                return m_dMaxY;
            }
            set
            {
                m_dMaxY = value;
                Refresh();
            }
        }


        public string FormatY
        {
            get
            {
                return m_sFormatY;
            }
            set
            {
                m_sFormatY = value;
                Refresh();
            }
        }

        public XScalar Scalar
        {
            get
            {
                return m_xScalar;
            }
            set
            {
                m_xScalar = value;
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

            if (m_xScalar != null)
            {
                {//X
                    RectangleF drawRectValue = new RectangleF(fWidth, 0, fWidth, fHeight);
                    double dVal = m_xScalar.X;
                    string sVal = String.Format(m_sFormatX, dVal * m_dCalX);
                    if (m_dMinX <= dVal && dVal <= m_dMaxX)
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
                    double dVal = m_xScalar.Y;
                    string sVal = String.Format(m_sFormatY, dVal * m_dCalY);
                    if (m_dMinY <= dVal && dVal <= m_dMaxY)
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
            if (m_xScalar != null)
            {
                int nWidth = Width / 3;
                int nIndex = (int)(e.X / nWidth);
                m_nSelect = nIndex;
                if (nIndex == 0)
                {
                    string sMsg = string.Format("Set = {0:F}, {1:F}, Are you sure?", m_dNomX * m_dCalX, m_dNomY * m_dCalY);
                    if (DialogResult.OK == MessageBox.Show(sMsg, Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        m_xScalar.Set(m_dNomX, m_dNomY);
                        if (Change != null) Change(this);
                        Refresh();
                    }

                }

                if (nIndex == 1)
                {
                    if (m_dCalX != 0)
                    {
                        frmInput fInput = new frmInput();
                        fInput.Value = m_xScalar.X * m_dCalX;
                        fInput.Min = m_dMinX;
                        fInput.Max = m_dMaxX;
                        fInput.Format = m_sFormatX;
                        if (DialogResult.OK == fInput.ShowDialog(this))
                        {
                            m_xScalar.X = fInput.Value / m_dCalX;
                            if (Change != null) Change(this);
                            Refresh();
                        }
                        fInput.Dispose();
                    }
                }
                if (nIndex == 2)
                {
                    if (m_dCalY != 0)
                    {
                        frmInput fInput = new frmInput();
                        fInput.Value = m_xScalar.Y * m_dCalY;
                        fInput.Min = m_dMinY;
                        fInput.Max = m_dMaxY;
                        fInput.Format = m_sFormatY;
                        if (DialogResult.OK == fInput.ShowDialog(this))
                        {
                            m_xScalar.Y = fInput.Value / m_dCalY;
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
