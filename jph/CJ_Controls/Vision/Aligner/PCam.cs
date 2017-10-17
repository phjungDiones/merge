using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CJ_Controls.Vision.BaseData;
namespace CJ_Controls.Vision.Aligner
{
    public partial class PCam : UserControl
    {
        public double E;
        public double Angle;
        private XCam m_xCam;
        public PCam()
        {
            InitializeComponent();
             m_xCam = new XCam();
        }

        public XCam Cam
        {
            get
            {
                return m_xCam;
            }
            set
            {
                m_xCam = value;
                if (m_xCam!=null)
                {
                    c_pInfro.Scalar = new XScalar();
                    c_pTarget.Scalar = m_xCam.Find[XCam.TARGET];
                    c_pObject.Scalar = m_xCam.Find[XCam.OBJECT];
                    c_pDiffer.Scalar = m_xCam.Differ;
                    c_pCal.Scalar = m_xCam.Cal;
                    c_pDist.Scalar = m_xCam.Dist;
                    c_pE.SetField(this, "E");
                    c_pAngle.SetField(this, "Angle");
                }
                Refresh();
            }

        }

        public new void Refresh()
        {
            E = (double)m_xCam.Dist;
            Angle = m_xCam.Dist.T;

            c_pE.Refresh();
            c_pAngle.Refresh();
            c_pTarget.Refresh();
            c_pObject.Refresh();
            c_pDiffer.Refresh();
            c_pCal.Refresh();
            c_pDist.Refresh();
        }

        private void PCam_Resize(object sender, EventArgs e)
        {
            int nHeight = Height/7;

            c_pInfro.Height = nHeight;
            c_pTarget.Height = nHeight;
            c_pObject.Height = nHeight;
            c_pDiffer.Height = nHeight;
            c_pCal.Height = nHeight;
            c_pDist.Height = nHeight;
            c_pE.Height = nHeight;
            c_pAngle.Height = nHeight;
      
            c_pInfro.Width = Width;
            c_pTarget.Width = Width;
            c_pObject.Width = Width;
            c_pDiffer.Width = Width;
            c_pCal.Width = Width;
            c_pDist.Width = Width;
            c_pE.Width = Width/2 -1;
            c_pAngle.Width = Width/2-1;


            c_pInfro.Top = nHeight * 0;
            c_pTarget.Top = nHeight * 1;
            c_pObject.Top = nHeight * 2;
            c_pDiffer.Top = nHeight * 3;
            c_pCal.Top = nHeight * 4;
            c_pDist.Top = nHeight * 5;
            c_pE.Top = c_pAngle.Top = nHeight * 6;


            c_pInfro.Left = 0;
            c_pTarget.Left = 0;
            c_pObject.Left = 0;
            c_pDiffer.Left = 0;
            c_pCal.Left = 0;
            c_pDist.Left = 0;
            c_pE.Left = 0 ;
            c_pAngle.Left = Width/2+1;


        }

    }
}
