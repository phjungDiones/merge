using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;

using CJ_Controls.Vision.BaseData;

namespace CJ_Controls.Vision.Viewer
{
    public class CHistogram
    {
        public int[] Hist = new int[256];
        public int Start;
        public int Mean;
        public int High;
        public int Average;
        public int End;
        public int Sum;
        public int Count;
    }

    public class XCore : X
    {
        private bool m_bGrey = true;
        private CHistogram m_cHist = new CHistogram();
        private Bitmap m_InputImage = null;
        private Bitmap m_OutputImage = null;

        public void Dispose()
        {
            if(m_InputImage != null)m_InputImage.Dispose();
            if(m_OutputImage != null)m_OutputImage.Dispose();
        }

        public XCore()
        {
        }
        public XCore(Bitmap Input)
        {
            InputImage = Input;
        }

        private bool Grey
        {
            get
            {
                return m_bGrey;
            }
            set
            {
                m_bGrey = value;
            }
        }

        public Bitmap InputImage
        {
            get
            {
                return m_InputImage;
            }
            set
            {
                if (value!=null)
                {
                    if (m_InputImage != null) m_InputImage.Dispose();
                    if (m_OutputImage != null) m_OutputImage.Dispose();
                    m_InputImage = new Bitmap(value);
                    m_OutputImage = new Bitmap(m_InputImage);
                }
            }
        }

        public Bitmap OutputImage
        {
            get
            {
                return m_OutputImage;
            }
        }


        public CHistogram GetHistogram()
        {
            int[] pHist = m_cHist.Hist;
            int nStart = 0, nMax = 0, nMean = 0, nEnd = 0;
            int nHist, nSum = 0;
            int nCount = 0;
            for (int n = 0; n < 256; n++)
            {
                nHist = pHist[n];
                if (nHist != 0)
                {
                    if (nMax < nHist)
                    {
                        nMax = nHist;
                        nMean = n;
                    }
                    if (nCount == 0)
                    {
                        nEnd = nStart = n;
                    }
                    else
                    {
                        nEnd = n;
                    }
                    nCount++;
                    nSum += nHist;
                }

            }
            m_cHist.Start = nStart;
            m_cHist.Mean = nMean;
            m_cHist.Average = nSum / nCount;
            m_cHist.End = nEnd;
            m_cHist.Sum = nSum;
            m_cHist.Count = nCount;

            return m_cHist;
        }

        public bool Histogram()
        {
            int[] pHist = m_cHist.Hist;
            if (m_InputImage != null)
            {
                for (int n = 0; n < 256; n++)
                {
                    pHist[n] = 0;
                }

                int nHeight = InputImage.Height;
                int nWidth = InputImage.Width;
                Rectangle Rect = new Rectangle(0, 0, nWidth, nHeight);
                BitmapData SocBitmapData = InputImage.LockBits(Rect
                                    , ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);

                try
                {
                    if (SocBitmapData != null)
                    {
                        unsafe
                        {

                            byte* Soc = (byte*)SocBitmapData.Scan0.ToPointer();
                            int nStride = SocBitmapData.Stride;

                            for (int nH = 0; nH < nHeight; nH++)
                            {
                                for (int nW = 0; nW < nStride; nW += 8)
                                {
                                    pHist[Soc[nW]]++;
                                    pHist[Soc[nW + 1]]++;
                                    pHist[Soc[nW + 2]]++;
                                    pHist[Soc[nW + 3]]++;
                                    pHist[Soc[nW + 4]]++;
                                    pHist[Soc[nW + 5]]++;
                                    pHist[Soc[nW + 6]]++;
                                    pHist[Soc[nW + 7]]++;

                                }
                                Soc += nStride;
                            }
                        }
                        InputImage.UnlockBits(SocBitmapData);
                    }
                }
                catch (Exception pe)
                {
                    string sMsg = pe.ToString();
                    return false;
                }

            }
            return true;
        }


        public bool Contrast(int nBrightness, int nContrast, int nOrigin)
        {
            if (m_InputImage != null && m_OutputImage != null)
            {
                if (m_bGrey == true)
                {
                    int nLevel;
                    int nR, nG, nB;
                    uint[] lut = new uint[256];
                    float fLevel;
                    for (Int16 n = 0; n < 256; n++)
                    {
                        fLevel = (n - nOrigin) * nContrast / 100.0f + nOrigin + nBrightness;
                        if (fLevel < 0.0f) lut[n] = 0xFF000000;
                        else if (fLevel > 255.0f) lut[n] = 0xFFFFFFFF;
                        else
                        {
                            nB = nG = nR = (int)fLevel;
                            nLevel = (nB << 16) + (nG << 8) + nR;
                            lut[n] = 0xFF000000 | (uint)nLevel;
                        }
                    }

                    int nHeight = m_OutputImage.Height;
                    int nWidth = m_OutputImage.Width;

                    Rectangle Rect = new Rectangle(0, 0, nWidth, nHeight);
                    BitmapData SocBitmapData = m_InputImage.LockBits(Rect
                                        , ImageLockMode.ReadOnly, m_InputImage.PixelFormat);
                    BitmapData DstBitmapData = m_OutputImage.LockBits(Rect
                                       , ImageLockMode.WriteOnly, m_OutputImage.PixelFormat);

                    try
                    {
                        if (DstBitmapData != null)
                        {
                            unsafe
                            {

                                uint* Soc = (uint*)SocBitmapData.Scan0.ToPointer();
                                uint* Dst = (uint*)DstBitmapData.Scan0.ToPointer();
                                int nStride = DstBitmapData.Stride;

                                for (int nH = 0; nH < nHeight; nH++)
                                {
                                    for (int nW = 0; nW < nWidth; nW += 8)
                                    {
                                        Dst[nW] = lut[0xFF & Soc[nW]];
                                        Dst[nW + 1] = lut[0xFF & Soc[nW + 1]];
                                        Dst[nW + 2] = lut[0xFF & Soc[nW + 2]];
                                        Dst[nW + 3] = lut[0xFF & Soc[nW + 3]];
                                        Dst[nW + 4] = lut[0xFF & Soc[nW + 4]];
                                        Dst[nW + 5] = lut[0xFF & Soc[nW + 5]];
                                        Dst[nW + 6] = lut[0xFF & Soc[nW + 6]];
                                        Dst[nW + 7] = lut[0xFF & Soc[nW + 7]];
                                    }
                                    Dst += nWidth;
                                    Soc += nWidth;
                                }

                            }
                            m_InputImage.UnlockBits(SocBitmapData);
                            m_OutputImage.UnlockBits(DstBitmapData);
                        }
                    }
                    catch (Exception pe)
                    {
                        string sMsg = pe.ToString();
                        return false;
                    }
                }
            }
            return true;
        }

        private Bitmap GetGrayImage(Bitmap SocBitmap, int nMask)
        {

            if (SocBitmap != null && SocBitmap.PixelFormat == PixelFormat.Format32bppArgb)
            {
                int nWidth = SocBitmap.Width;
                int nHeight = SocBitmap.Height;

                Bitmap DstBitmap = new Bitmap(nWidth, nHeight, PixelFormat.Format8bppIndexed);

                ColorPalette GrayScaleColorPalette = DstBitmap.Palette;
                for (int n = 0; n < 256; n++)
                {
                    GrayScaleColorPalette.Entries[n] = Color.FromArgb(n, n, n);
                }
                DstBitmap.Palette = GrayScaleColorPalette;
                try
                {
                    BitmapData SocBitmapData = SocBitmap.LockBits(new Rectangle(0, 0, nWidth, nHeight)
                                            , ImageLockMode.ReadOnly, SocBitmap.PixelFormat);

                    BitmapData DstBitmapData = DstBitmap.LockBits(new Rectangle(0, 0, nWidth, nHeight)
                                            , ImageLockMode.WriteOnly, DstBitmap.PixelFormat);

                    unsafe
                    {

                        int* SocPointer = (int*)SocBitmapData.Scan0.ToPointer();
                        byte* DstPointer = (byte*)DstBitmapData.Scan0.ToPointer();
                        int nSocLine = SocBitmapData.Stride >> 2;
                        int nDstLine = DstBitmapData.Stride;
                        int nH, nW;
                        for (nH = 0; nH < nHeight; nH++)
                        {
                            for (nW = 0; nW < nDstLine; nW += 8)
                            {
                                DstPointer[nW] = (byte)(SocPointer[nW] & nMask);
                                DstPointer[nW + 1] = (byte)(SocPointer[nW + 1] & nMask);
                                DstPointer[nW + 2] = (byte)(SocPointer[nW + 2] & nMask);
                                DstPointer[nW + 3] = (byte)(SocPointer[nW + 3] & nMask);
                                DstPointer[nW + 4] = (byte)(SocPointer[nW + 4] & nMask);
                                DstPointer[nW + 5] = (byte)(SocPointer[nW + 5] & nMask);
                                DstPointer[nW + 6] = (byte)(SocPointer[nW + 6] & nMask);
                                DstPointer[nW + 7] = (byte)(SocPointer[nW + 7] & nMask);
                            }
                            SocPointer += nSocLine;
                            DstPointer += nDstLine;

                        }

                    }
                    DstBitmap.UnlockBits(DstBitmapData);
                    SocBitmap.UnlockBits(SocBitmapData);

                    return DstBitmap;
                }
                catch (Exception fe)
                {
                    string sMsg = fe.ToString();
                    return null;

                }

            }
            return null;

        }

        public static Bitmap GetRotateImage(Bitmap Image, float w, float h, double t)
        {

            float dw = w * 2.0f;
            float dh = h * 2.0f;

            Bitmap RotateBmp = new Bitmap((int)(dw + 0.5), (int)(dh + 0.5));
            Graphics gp = Graphics.FromImage(RotateBmp);
            {


                TextureBrush TextureFill = new TextureBrush(Image);

                TextureFill.RotateTransform((float)(t * 180.0 / Math.PI));


                double dx = 0, dy = 0;
                double dPI = Math.PI / 2.0;
                if (0 < t && t < dPI || Math.PI < t && t < (Math.PI + dPI))
                {
                    dx = 0;
                    dy = dh * Math.Cos(-t);
                }
                else if (dPI < t && t < Math.PI || (Math.PI + dPI) < t && t < (Math.PI + Math.PI))
                {
                    dx = dw * Math.Cos(-t);
                    dy = 0;
                }

                TextureFill.TranslateTransform((float)dx, (float)dy);

                gp.FillRectangle(TextureFill, 0, 0, dw, dh);

                TextureFill.Dispose();
                gp.Dispose();
            }
            return RotateBmp;

        }

        public static bool InvertImage(ref Bitmap InputImage)
        {
            if (InputImage != null)
            {


                try
                {
                    
                    BitmapData InputImageData = InputImage.LockBits(new Rectangle(0, 0, InputImage.Width, InputImage.Height)
                                            , ImageLockMode.ReadWrite, InputImage.PixelFormat);
                    if (InputImageData != null)
                    {
                        int nLen = InputImageData.Stride * InputImageData.Height;
                        unsafe
                        {
                            byte* pMap = (byte*)InputImageData.Scan0;
                            for(int n=0; n<nLen;n++)
                            {
                                pMap[n] = Convert.ToByte(255 - pMap[n]);
                            }
                        }
                        InputImage.UnlockBits(InputImageData);
                    }

                }
                catch (Exception pe)
                {
                    string sMsg = pe.ToString();
                }
            }
            return true;
        }

        public static Bitmap GetRegionImage(XRegion xRegion, Bitmap InputImage)
        {
            if (xRegion != null && InputImage != null)
            {
                RectangleF RectF = xRegion.Rect();
                int nLeft = (int)(RectF.Left+0.5f);
                int nTop = (int)(RectF.Top + 0.5f);
                int nRight = (int)(RectF.Right + 0.5f);
                int nBottom = (int)(RectF.Bottom + 0.5f);


                int nWidth = nRight - nLeft;
                int nHeight = nBottom - nTop;

                try
                {

                    BitmapData InputImageData = InputImage.LockBits(new Rectangle(nLeft, nTop, nWidth, nHeight)
                                            , ImageLockMode.ReadOnly, InputImage.PixelFormat);
                    if (InputImageData != null)
                    {
                        Bitmap BoundMap = new Bitmap(nWidth, nHeight, InputImageData.Stride, InputImageData.PixelFormat, InputImageData.Scan0);
                        InputImage.UnlockBits(InputImageData);

                        if (xRegion.T == 0)
                        {
                            return BoundMap;
                        }
                        else
                        {
                            Bitmap RotateMap = GetRotateImage(BoundMap, xRegion.W, xRegion.H, xRegion.T);
                            BoundMap.Dispose();

                            return RotateMap;
                        }
                    }

                }
                catch (Exception pe)
                {
                    string sMsg = pe.ToString();
                }
            }
            return null;
        }

        public Bitmap GetRegionImage(XRegion xRegion, bool bOutputImage)
        {
            if (xRegion != null)
            {
                Bitmap SocBitmap;
                if (bOutputImage == true)
                {
                    SocBitmap = m_OutputImage;
                }
                else
                {
                    SocBitmap = m_InputImage;
                }
                RectangleF RectF = xRegion.Rect();
                int nLeft = (int)(RectF.Left + 0.5f);
                int nTop = (int)(RectF.Top + 0.5f);
                int nRight = (int)(RectF.Right + 0.5f);
                int nBottom = (int)(RectF.Bottom + 0.5f);


                int nWidth = nRight - nLeft;
                int nHeight = nBottom - nTop;
                try
                {

                    BitmapData SocBitmapData = SocBitmap.LockBits(new Rectangle(nLeft, nTop, nWidth, nHeight)
                                            , ImageLockMode.ReadOnly, SocBitmap.PixelFormat);
                    if (SocBitmapData != null)
                    {
                        Bitmap BoundMap = new Bitmap(nWidth, nHeight, SocBitmapData.Stride, SocBitmapData.PixelFormat, SocBitmapData.Scan0);
                        SocBitmap.UnlockBits(SocBitmapData);

                        if (xRegion.T == 0)
                        {
                            return BoundMap;
                        }
                        else
                        {
                            Bitmap RotateMap = GetRotateImage(BoundMap, xRegion.W, xRegion.H, xRegion.T);
                            BoundMap.Dispose();

                            return RotateMap;
                        }
                    }

                }
                catch (Exception pe)
                {
                    string sMsg = pe.ToString();
                }
            }
            return null;
        }

        public static bool Contrast(int nHeight, int nStride, ref int nScan0, int nBrightness, int nContrast, int nOrigin)
        {
            if (nScan0 != 0)
            {
 
                               
                byte[] lut = new byte[256];

                for (Int16 n = 0; n < 256; n++)
                {
                    float fLevel = (n - nOrigin) * nContrast / 100.0f + nOrigin + nBrightness;
                    if (fLevel < 0.0f) lut[n] = 0;
                    else if (fLevel > 255.0f) lut[n] = 255;
                    else
                    {
                        lut[n] = (byte)fLevel;
                    }
                }

                try
                {

                    unsafe
                    {

                        byte* Soc = (byte*)nScan0;
                       
                        for (int nH = 0; nH < nHeight; nH++)
                        {
                            for (int nW = 0; nW < nStride; nW += 8)
                            {
                                Soc[nW] = lut[Soc[nW]];
                                Soc[nW + 1] = lut[Soc[nW + 1]];
                                Soc[nW + 2] = lut[Soc[nW + 2]];
                                Soc[nW + 3] = lut[Soc[nW + 3]];
                                Soc[nW + 4] = lut[Soc[nW + 4]];
                                Soc[nW + 5] = lut[Soc[nW + 5]];
                                Soc[nW + 6] = lut[Soc[nW + 6]];
                                Soc[nW + 7] = lut[Soc[nW + 7]];
                            }
                            Soc += nStride;
                        }

                    }

                  }
                catch (Exception pe)
                {
                    string sMsg = pe.ToString();
                    return false;
                }
                return true;
            }
            return false;
        }

    }
}
