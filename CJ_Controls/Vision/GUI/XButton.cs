using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.Vision.GUI
{
    public partial class XButton : System.Windows.Forms.PictureBox
    {
        
        public enum STATE
        {
            OFF = 0,
            ON = 1,
            MAX_BUTTON_STATE
        };

        private bool m_bToggle;
        private STATE m_eMouseUp;
        private STATE m_eValue;

        private Color m_cForeColor; 
        private Font  m_fFont;
        private string m_sText;
        private StringFormat m_TextAlign = new StringFormat();
        private Image m_MouseUpImage;
        public XButton()
        {
            m_bToggle = false;
            m_eMouseUp = STATE.OFF;
            m_eValue = STATE.OFF;
     
            m_TextAlign.Alignment = StringAlignment.Center;
            m_TextAlign.LineAlignment = StringAlignment.Center;
//            m_eMouseUp = STATE.OFF;
 //           m_eValue = STATE.OFF;

            InitializeComponent();
        }

        public STATE Value
        {
            get
            {
                return m_eValue;
            }
            set
            {
                m_eValue = value;
                Refresh();
            }
        }

       public bool Toggle
        {
            get
            {
                return m_bToggle;
            }
            set
            {
                m_bToggle = value;
            }
        }


        public Font XFont
        {
            get
            {
                return m_fFont;
            }
            set
            {
                m_fFont = value;
            }
        }

        public Color XForeColor
        {
            get
            {
                return m_cForeColor;
            }
            set
            {
                m_cForeColor = value;
            }
        }
        public string XText
        {
            get
            {
                return m_sText;
            }
            set
            {
                m_sText = value;
            }
        }

        public Image MouseUpImage
        {
            get
            {
                return m_MouseUpImage;
            }
            set
            {
                m_MouseUpImage = value;
                Refresh();
            }
        }

        public ContentAlignment TextAlign
        {
            get
            {
                return (ContentAlignment)((1 << ((int)m_TextAlign.LineAlignment * 4)) * (1 << (int)m_TextAlign.Alignment));
            }
            set
            {
                switch (value)
                {
                    case ContentAlignment.TopLeft: m_TextAlign.LineAlignment = StringAlignment.Near; m_TextAlign.Alignment = StringAlignment.Near; break;
                    case ContentAlignment.TopCenter: m_TextAlign.LineAlignment = StringAlignment.Near; m_TextAlign.Alignment = StringAlignment.Center; break;
                    case ContentAlignment.TopRight: m_TextAlign.LineAlignment = StringAlignment.Near; m_TextAlign.Alignment = StringAlignment.Far; break;
                    case ContentAlignment.MiddleLeft: m_TextAlign.LineAlignment = StringAlignment.Center; m_TextAlign.Alignment = StringAlignment.Near; break;
                    case ContentAlignment.MiddleCenter: m_TextAlign.LineAlignment = StringAlignment.Center; m_TextAlign.Alignment = StringAlignment.Center; break;
                    case ContentAlignment.MiddleRight: m_TextAlign.LineAlignment = StringAlignment.Center; m_TextAlign.Alignment = StringAlignment.Far; break;
                    case ContentAlignment.BottomLeft: m_TextAlign.LineAlignment = StringAlignment.Far; m_TextAlign.Alignment = StringAlignment.Near; break;
                    case ContentAlignment.BottomCenter: m_TextAlign.LineAlignment = StringAlignment.Far; m_TextAlign.Alignment = StringAlignment.Center; break;
                    case ContentAlignment.BottomRight: m_TextAlign.LineAlignment = StringAlignment.Far; m_TextAlign.Alignment = StringAlignment.Far; break;
                }
                Refresh();
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            Refresh();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle drawRect =new Rectangle(0, 0, Width, Height);
 
            OnPaintImage(pe.Graphics, drawRect);
          
            if (m_sText != null && m_sText != "")
            {
                OnPaintText(pe.Graphics, drawRect);
            }

        }

        private void OnPaintImage(Graphics gp, Rectangle Rect)
        {

            if (m_eValue == STATE.ON)
            {
                if (this.Image != null) gp.DrawImage(this.Image, Rect);
            }
            else
            {
                if (m_eMouseUp == STATE.ON)
                {
                    if (m_MouseUpImage != null) gp.DrawImage(m_MouseUpImage, Rect);
                }
            }
        }

        private void OnPaintText(Graphics gp, Rectangle Rect)
        {
            SolidBrush drawBrushFore = new SolidBrush(m_cForeColor);
            if (m_sText != null)
            {
                gp.DrawString(m_sText, m_fFont, drawBrushFore, Rect, m_TextAlign);
            }
            drawBrushFore.Dispose();
        }

        protected override void OnClick(EventArgs e)
        {

        }
        protected override void OnMouseDown(MouseEventArgs e)
        {

            if (Enabled == true)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Focus();
                    if (m_bToggle == true)
                    {
                        if (m_eValue == STATE.ON)
                        {
                            Value = STATE.OFF;
                        }
                        else
                        {
                            Value = STATE.ON;
                        }
                        base.OnClick(e);
                    }
                    else
                    {
                        Focus();
                        Value = STATE.ON;
                        base.OnClick(e);
                     }
                }
            }

        }
        protected override void OnLostFocus(EventArgs e)
        {
            if (Enabled == true)
            {
               
                if (m_eValue == STATE.ON && m_bToggle==false)
                {
                    Value = STATE.OFF;
                }

            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            if (Enabled == true)
            {
                m_eMouseUp = STATE.ON;
                Refresh();
            }
        }



        protected override void OnMouseLeave(EventArgs e)
        {
            if (Enabled == true)
            {
                m_eMouseUp = STATE.OFF;
                Refresh();
            }
        }



        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Enabled == true)
            {
                if (m_bToggle == false)
                {
                    Value = STATE.OFF;

                }
                Refresh();
            }
        }



    }
}
