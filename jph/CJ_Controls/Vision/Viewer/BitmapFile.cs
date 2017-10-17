using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Xml.Serialization;
using System.IO;

namespace CJ_Controls.Vision.Viewer
{
    public class BitmapFile
    {
        public static Bitmap Load(string sPath)
        {
            try
            {

                Bitmap LoadBitmap = new Bitmap(sPath);
                Bitmap ReturnBitmap = new Bitmap(LoadBitmap);
                LoadBitmap.Dispose();
                return ReturnBitmap;
            }

            catch (Exception pe)
            {
                string sMsg = pe.ToString();
                return null;
            }
        }

        public static bool Save(string sPath, Bitmap Bitmap)
        {
            try
            {

                if (File.Exists(sPath))
                {
                    File.Delete(sPath);
                }
                Bitmap SaveBitmap = new Bitmap(Bitmap);
                SaveBitmap.Save(sPath);
                SaveBitmap.Dispose();
                return true;
            }
            catch (Exception pe)
            {
                string sMsg = pe.ToString();
                return false;
            }
        }


        public static bool Save(string sPath, Bitmap SocBitmap, ImageFormat iFormat)
        {
            try
            {

                if (File.Exists(sPath))
                {
                    File.Delete(sPath);
                }

                int nWidth = SocBitmap.Width;
                int nHeight = SocBitmap.Height;

                Rectangle Rect = new Rectangle(0, 0, nWidth, nHeight);
                BitmapData SocBitmapData = SocBitmap.LockBits(Rect
                                    , ImageLockMode.ReadOnly, SocBitmap.PixelFormat);

                Bitmap DstBitmap = new Bitmap(nWidth, nHeight, SocBitmapData.Stride, PixelFormat.Format32bppRgb, SocBitmapData.Scan0);

                SocBitmap.UnlockBits(SocBitmapData);

                DstBitmap.Save(sPath, iFormat);
                
                DstBitmap.Dispose();
                return true;
            }
            catch (Exception pe)
            {
                string sMsg = pe.ToString();
                return false;
            }
        }


    }
}
