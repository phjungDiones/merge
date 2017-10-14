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
    public partial class PVector : Control
    {
        public event ChangeData Change;

        protected double m_dMinX;
        protected double m_dMaxX;
        protected double m_dMinY;
        protected double m_dMaxY;
        protected double m_dMinT;
        protected double m_dMaxT;

        protected double m_dCalX;
        protected double m_dCalY;
        protected double m_dCalT;

        protected XVector m_xVector;
        protected Color m_cBolderColor;
        protected Color m_cValueColor;
        protected string m_sFormatX;
        protected string m_sFormatY;
        protected string m_sFormatT;

        public int m_nSelect;


        protected double m_dNomX;
        protected double m_dNomY;
        protected double m_dNomT;
        
        
        
        public PVector()
        {

             m_dNomX = 0.0;
             m_dNomY = 0.0;
             m_dNomT = 0.0;


            m_nSelect = 0;

            m_dCalX = 1.0;
            m_dCalY = 1.0;
            m_dCalT = 1.0;

            m_dMinX = -100000.0;
            m_dMaxX = 100000.0;
            m_dMinY = -100000.0;
            m_dMaxY = 100000.0;
            m_dMinT = -100000.0;
            m_dMaxT = 100000.0;

            m_sFormatX = "{0:F1}";
            m_sFormatY = "{0:F1}";
            m_sFormatT = "{0:F1}";

            m_cBolderColor = Color.DarkBlue;
            m_cValueColor = Color.White;
            InitializeComponent();

            m_xVector = new XVector();

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
        public double CalT
        {
            get
            {
                return m_dCalT;
            }
            set
            {
                m_dCalT = value;
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
        public double NomT
        {
            get
            {
                return m_dNomT;
            }
            set
            {
                m_dNomT = value;
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


        public double MinT
        {
            get
            {
                return m_dMinT;
            }
            set
            {
                m_dMinT = value;
                Refresh();
            }
        }
        public double MaxT
        {
            get
            {
                return m_dMaxT;
            }
            set
            {
                m_dMaxT = value;
                Refresh();
            }
        }


        public string FormatT
        {
            get
            {
                return m_sFormatT;
            }
            set
            {
                m_sFormatT = value;
                Refresh();
            }
        }


        public XVector Vector
        {
            get
            {
                return m_xVector;
            }
            set
            {
                m_xVector = value;
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
            float fWidth = Width / 4.0f - 1.0f;
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

            pe.Graphics.FillRectangle(drawBrushValue, fWidth * 2, 0, fWidth, fHeight);
            pe.Graphics.DrawRectangle(BolderPen, fWidth * 2, 0, fWidth, fHeight);

            pe.Graphics.FillRectangle(drawBrushValue, fWidth * 3, 0, Width - fWidth * 3 - 1.0f, fHeight);
            pe.Graphics.DrawRectangle(BolderPen, fWidth * 3, 0, Width - fWidth * 3 - 1.0f, fHeight);

            BolderPen.Dispose();
            drawBrushValue.Dispose();

            if (m_xVector != null)
            {
                {//X
                    RectangleF drawRectValue = new RectangleF(fWidth, 0, fWidth, fHeight);
                    double dVal = m_xVector.X;
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
                    double dVal = m_xVector.Y;
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

                {//T
                    RectangleF drawRectValue = new RectangleF(fWidth * 3, 0, fWidth, fHeight);
                    double dVal = m_xVector.T;
                    string sVal = String.Format(m_sFormatT, dVal * m_dCalT);
                    if (m_dMinT <= dVal && dVal <= m_dMaxT)
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
            if (m_xVector != null)
            {
                int nWidth = Width / 4;
                int nIndex = (int)(e.X / nWidth);
                m_nSelect = nIndex;
                if (nIndex==0)
                {
                    string sMsg = string.Format("Set = {0:F}, {1:F}, {2:F}, Are you sure?", m_dNomX * m_dCalX, m_dNomY * m_dCalY, m_dNomT * m_dCalT);
                    if (DialogResult.OK == MessageBox.Show(sMsg, Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        m_xVector.Set(m_dNomX,m_dNomY,m_dNomT);
                        if (Change != null) Change(this);
                        Refresh();
                    }

                }
                if (nIndex == 1)
                {
                    if (m_dCalX != 0)
                    {
                        frmInput fInput = new frmInput();
                        fInput.Value = m_xVector.X * m_dCalX;
                        fInput.Min = m_dMinX;
                        fInput.Max = m_dMaxX;
                        fInput.Format = m_sFormatX;
                        if (DialogResult.OK == fInput.ShowDialog(this))
                        {
                            m_xVector.X = fInput.Value / m_dCalX;
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
                        fInput.Value = m_xVector.Y * m_dCalY;
                        fInput.Min = m_dMinY;
                        fInput.Max = m_dMaxY;
                        fInput.Format = m_sFormatY;
                        if (DialogResult.OK == fInput.ShowDialog(this))
                        {
                            m_xVector.Y = fInput.Value / m_dCalY;
                            if (Change != null) Change(this);
                            Refresh();
                        }
                        fInput.Dispose();
                    }
                }

                if (nIndex == 3)
                {
                    if (m_dCalT != 0)
                    {
                        frmInput fInput = new frmInput();
                        fInput.Value = m_xVector.T * m_dCalT;
                        fInput.Min = m_dMinT;
                        fInput.Max = m_dMaxT;
                        fInput.Format = m_sFormatT;
                        if (DialogResult.OK == fInput.ShowDialog(this))
                        {
                            m_xVector.T = fInput.Value / m_dCalT;
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
