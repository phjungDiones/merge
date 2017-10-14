using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using CJ_Controls.Vision.BaseData;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;

namespace CJ_Controls.Vision.Viewer
{
	public delegate void ChangeSelect(Control sender);
	public delegate void ChangeDraw(Graphics pe);

	public partial class View : Control
	{
		public event ChangeSelect ChangeSelect;
		public event ChangeDraw ChangeDraw;

		[DllImport("kernel32")]
		public static extern unsafe void CopyMemory(void* Destination,  // pointer to address of copy destination
											 void* Source, // pointer to address of block to copy
											 int Length        // size, in bytes, of block to copy
											  );

		[DllImport("kernel32")]
		public static extern unsafe void FillMemory(void* Destination,
											  int Length,
											  Int32 Fill
											);

		public enum WINDOW_EVENT
		{
			WM_KEYDOWN = 0x0100,
			WM_KEYUP = 0x0101
		}

		public enum E_VIEW_CONTROL_MODE
		{
			VCM_POINT,
			VCM_PAN,
			VCM_ZOOM_IN,
			VCM_ZOOM_OUT,
			VCM_FIT,
			VCM_ZOOM_100,
			MAX_VCM
		}


		E_VIEW_CONTROL_MODE m_eControlMode;


		//{Draw Image;

		private Bitmap m_Back;
		private Bitmap m_Bitmap;
		protected int m_nActBitmap;
		private Bitmap[] m_Buffer = new Bitmap[2];
		//       private int m_MapWidth;
		//       private int m_MapHeight;
		private float m_fZoom;
		private float m_fZ;
		private PointF m_pOrigin = new PointF(0, 0);
		private PointF m_pStartPan = new PointF(0, 0);
		private RectangleF m_srcRect = new RectangleF();
		//}

		//{Draw Overlay;
		private XDimension m_xSelect = new XDimension();
		private XRegion m_xSelectRegion = new XRegion();
		private XArray m_xOverlay;
		private XRegion m_xActiveRegion;
		//}

		//{Draw Static;
		private XArray m_xStatic;
		//}

		//{

		//       private float m_oZ = 0;
		private PointF m_oOrigin = new PointF(0, 0);

		//        private bool m_bRefresh = false;
		//        private bool m_bRefreshImage = false;
		//}
// 		Cursor m_cPan = new Cursor("C:/Xn/Resources/Pan.cur");
// 		Cursor m_cZoomIn = new Cursor("C:/Xn/Resources/ZoomIn.cur");
// 		Cursor m_cZoomOut = new Cursor("C:/Xn/Resources/ZoomOut.cur");
//		private int m_nTransparency;

		private bool m_bMouseMove = false;
		private bool m_bOverlayControl = false;

		public View()
		{
			//            m_MapWidth = 0;
			//            m_MapHeight = 0;
			m_nActBitmap = 0;
			//m_nTransparency = 0xFF;
			m_eControlMode = E_VIEW_CONTROL_MODE.VCM_POINT;
			m_fZ = m_fZoom = 0.0f;
			m_xSelectRegion.BorderStyle = DashStyle.Dot;
			m_xSelectRegion.BorderColor = Color.LightGray;
			InitializeComponent();


		}

		public PointF Origin
		{
			get
			{
				return m_pOrigin;
			}
		}
		public int Transparency
		{

			get
			{
				return 0;
			}
			set
			{
				//m_nTransparency = value;

			}
		}

		public bool OverlayControl
		{

			get
			{
				return m_bOverlayControl;
			}
			set
			{
				m_bOverlayControl = value;

			}
		}


		public new XDimension Select
		{

			get
			{
				return m_xSelect;
			}

		}



		public XRegion ActiveRegion
		{

			get
			{
				return m_xActiveRegion;
			}

		}
		public XArray Overlay
		{

			get
			{
				return m_xOverlay;
			}
			set
			{
				m_xOverlay = value;

			}
		}
		public XArray Static
		{

			get
			{
				return m_xStatic;
			}
			set
			{
				m_xStatic = value;

			}
		}

		public Bitmap GetImage()
		{
			Bitmap ImageDC = new Bitmap(m_Bitmap);
			Graphics iGp = Graphics.FromImage(ImageDC);
// 			if (iGp != null)
// 			{
// 				DrawFillOverlay(iGp, m_nTransparency, 0, 0, 1);
// 				iGp.Dispose();
// 			}
			return ImageDC;
		}

		public Bitmap GetOverlayImage()
		{
			try
			{
				Bitmap ImageDC = new Bitmap(m_Bitmap.Width, m_Bitmap.Height);
				Graphics iGp = Graphics.FromImage(ImageDC);
				if (iGp != null)
				{
					DrawFillOverlay(iGp, 0xFF, 0, 0, 1);
					iGp.Dispose();
					return ImageDC;
				}
			}
			catch (Exception pe)
			{
				string sMsg = pe.ToString();
			}
			return null;
		}

		public void FillBitmap(Bitmap Dst, Rectangle Rect, Color cFillColor)
		{
			BitmapData DstData = Dst.LockBits(Rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			if (DstData != null)
			{
				unsafe
				{
					//int nDstStride = DstData.Stride>>2;
					Int32* iDst = (Int32*)DstData.Scan0.ToPointer();

					FillMemory(iDst, DstData.Width * DstData.Height, cFillColor.ToArgb());

				}
				Dst.UnlockBits(DstData);
			}
		}

		public void BitmapFrom32To32(Bitmap Soc32, Bitmap Dst32)
		{

			int nWidth = Soc32.Width;
			int nHeight = Soc32.Height;
			Rectangle Rect = new Rectangle(0, 0, nWidth, nHeight);
			BitmapData SocData = Soc32.LockBits(Rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
			BitmapData DstData = Dst32.LockBits(Rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
			if (SocData != null && DstData != null)
			{
				unsafe
				{
					int nSocStride = SocData.Stride;
					int nDstStride = DstData.Stride;
					Int32* iSoc = (Int32*)SocData.Scan0.ToPointer();
					Int32* iDst = (Int32*)DstData.Scan0.ToPointer();

					CopyMemory(iDst, iSoc, nDstStride * nHeight);

				}

			}
			if (SocData != null) Soc32.UnlockBits(SocData);
			if (DstData != null) Dst32.UnlockBits(DstData);
		}
		public void BitmapFrom8To32(Bitmap Soc8, Bitmap Dst32)
		{
			int nWidth = Soc8.Width;
			int nHeight = Soc8.Height;
			Rectangle Rect = new Rectangle(0, 0, nWidth, nHeight);
			BitmapData SocData = Soc8.LockBits(Rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			BitmapData DstData = Dst32.LockBits(Rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			if (SocData != null && DstData != null)
			{
				unsafe
				{
					int nSocStride = SocData.Stride;
					int nDstStride = DstData.Stride;
					Int32* iSoc = (Int32*)SocData.Scan0.ToPointer();
					Int32* iDst = (Int32*)DstData.Scan0.ToPointer();

					if (nSocStride == nDstStride)
					{
						CopyMemory(iDst, iSoc, nDstStride * nHeight);
					}
					else
					{
						for (int n = 0; n < nHeight; n++)
						{
							CopyMemory(iDst, iSoc, nSocStride);
							iSoc += nSocStride;
							iDst += nDstStride;
						}
					}
				}

			}
			if (SocData != null) Soc8.UnlockBits(SocData);
			if (DstData != null) Dst32.UnlockBits(DstData);
		}

		public Bitmap Bitmap
		{
			get
			{
				return m_Bitmap;
			}
			set
			{
				try
				{
					if (value != null)
					{
						if (m_Bitmap == null)
						{
							m_Buffer[0] = new Bitmap(value);
							m_Buffer[1] = new Bitmap(value);
							m_Bitmap = m_Buffer[1];
							m_nActBitmap = 0;
							Zoom = 0;
							Invalidate(new Rectangle(0, 0, Width, Height));
						}
						else
						{
							if (m_Buffer[m_nActBitmap] != null) m_Buffer[m_nActBitmap].Dispose();
							m_Buffer[m_nActBitmap] = new Bitmap(value);

							if (m_nActBitmap == 0) m_nActBitmap = 1;
							else m_nActBitmap = 0;

							m_Bitmap = m_Buffer[m_nActBitmap];
						}
						Invalidate(new Rectangle(0, 0, Width, Height));
					}
					else
					{
						if (m_Buffer[0] != null) m_Buffer[0].Dispose();
						if (m_Buffer[1] != null) m_Buffer[1].Dispose();
						m_Bitmap = null;
						Invalidate(new Rectangle(0, 0, Width, Height));
					}
				}
				catch (System.Exception e)
				{
					string sMsg = e.ToString();
				}

			}
		}


		public float Zoom
		{
			get
			{
				return m_fZoom;
			}
			set
			{
				m_fZoom = value;
				if (m_Bitmap != null && (m_fZoom == 0 || m_fZoom == 1 || m_fZ == 0))
				{
					try
					{
						CalcScreen();
						m_pOrigin.X = (Width - m_Bitmap.Width * m_fZ) / 2.0f;
						m_pOrigin.Y = (Height - m_Bitmap.Height * m_fZ) / 2.0f;
						//Refresh();
					}
					catch (System.Exception e)
					{
						string sMsg = e.ToString();
					}

				}

			}

		}
		public float GetZoom()
		{
			return m_fZ;
		}
		private void CalcScreen()
		{
			try
			{
				if (m_Bitmap == null)
				{
					if (m_fZoom == 0) m_fZ = 1.0f;
					else m_fZ = m_fZoom;
					return;
				}

				if (m_fZoom == 1.0)
				{
					m_fZ = m_fZoom;
					m_srcRect.X = m_pOrigin.X;
					m_srcRect.Y = m_pOrigin.Y;
					m_srcRect.Width = m_Bitmap.Width;
					m_srcRect.Height = m_Bitmap.Height;

				}
				else
				{
					float fDspW = m_Bitmap.Width;
					float fDspH = m_Bitmap.Height;
					float fScrW = Width;
					float fScrH = Height;
					m_fZ = m_fZoom;
					if (m_fZ <= 0.0f)
					{
						float fZw = (fScrW + 0.5f) / fDspW;
						float fZh = (fScrH + 0.5f) / fDspH;

						//float fZw = fScrW  / fDspW;
						//float fZh = fScrH   / fDspH;
						if (fZw < fZh)
						{
							m_fZ = fZw;
						}
						else
						{
							m_fZ = fZh;
						}
					}
					fDspW *= m_fZ;
					fDspH *= m_fZ;
					m_srcRect.Width = fDspW;
					m_srcRect.Height = fDspH;
					m_srcRect.X = m_pOrigin.X;
					m_srcRect.Y = m_pOrigin.Y;
				}

			}
			catch (System.Exception e)
			{
				string sMsg = e.ToString();
			}
		}
		public E_VIEW_CONTROL_MODE ControlMode
		{
			get
			{
				return m_eControlMode;
			}
			set
			{

				m_eControlMode = value;
				switch (m_eControlMode)
				{
// 					case E_VIEW_CONTROL_MODE.VCM_POINT: Cursor = Cursors.Arrow; break;
// 					case E_VIEW_CONTROL_MODE.VCM_PAN: Cursor = m_cPan; break;
// 					case E_VIEW_CONTROL_MODE.VCM_ZOOM_IN: Cursor = m_cZoomIn; break;
// 					case E_VIEW_CONTROL_MODE.VCM_ZOOM_OUT: Cursor = m_cZoomOut; break;
				}
			}

		}
		protected override void OnResize(EventArgs e)
		{
			if (m_Back != null) m_Back.Dispose();

			m_Back = new Bitmap(Width, Height);


			base.OnResize(e);
		}
		protected override void OnPaintBackground(PaintEventArgs pe)
		{
			if (m_Bitmap == null)//|| m_bRefresh == true
			{
				base.OnPaintBackground(pe);
			}
			else if (m_Back != null)
			{
				CalcScreen();
				float srcRectT = m_srcRect.Top;
				float srcRectL = m_srcRect.Left;
				float srcRectB = m_srcRect.Bottom;
				float srcRectR = m_srcRect.Right;
				Graphics Gp = Graphics.FromImage(m_Back);
				if (Gp != null)
				{
					SolidBrush brushBack = new SolidBrush(BackColor);
					float nSpX = 0;
					float nEpW = Width;
					if (0 < srcRectL && srcRectL < Width) nSpX = srcRectL;
					if (0 < srcRectR && srcRectR < Width) nEpW = srcRectR - nSpX;

					if (0 < srcRectT)
					{
						Gp.FillRectangle(brushBack, nSpX, 0, nEpW, srcRectT + 1.0f);
					}
					if (srcRectB < Height)
					{
						Gp.FillRectangle(brushBack, nSpX, srcRectB - 1.0f, nEpW, Height - srcRectB + 2.0f);
					}

					if (0 < srcRectL)
					{
						Gp.FillRectangle(brushBack, 0, 0, srcRectL + 1.0f, Height);
					}
					if (srcRectR < Width)
					{
						Gp.FillRectangle(brushBack, srcRectR - 1.0f, 0, Width - srcRectR + 2.0f, Height);
					}

					Gp.Dispose();
				}
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			if (m_Bitmap != null && m_Bitmap != null)
			{
				try
				{
					Graphics mGp = Graphics.FromImage(m_Back);
					if (mGp != null)
					{

						DrawImage(mGp, m_Bitmap);
						if (m_bMouseMove == false)
						{
							//DrawFillOverlay(mGp, m_nTransparency, m_pOrigin.X, m_pOrigin.Y, m_fZ);
						}
						DrawOverlay(mGp);

						if (m_xSelectRegion.ControlSelect == REGION_CONTROL_STYLES.BR)
						{
							m_xSelectRegion.Draw(mGp, m_pOrigin.X, m_pOrigin.Y, m_fZ, m_fZ);
						}
						if (ChangeDraw != null) ChangeDraw(mGp);

						Graphics Gp = pe.Graphics;
						if (Gp != null)
						{
							Gp.DrawImage(m_Back, 0, 0);
							mGp.Dispose();
						}
						//m_bRefreshImage = false;
						//m_bRefresh = false;
					}
					else
					{
						if (mGp != null) Refresh();
					}
				}
				catch (System.Exception e)
				{
					string sMsg = e.ToString();
				}

			}
		}

		private void DrawFillOverlay(Graphics gp, int nTransparency, float fOx, float fOy, float fZoom)
		{

			if (m_Bitmap != null)
			{

				if (m_xOverlay != null)
				{
					XRegion iRegion;
					int nMaxID = m_xOverlay.FindMaxID();
					for (int nID = 1; nID <= nMaxID; nID++)
					{
						iRegion = (XRegion)m_xOverlay.Find(nID);
						if (iRegion != null && (iRegion.FillColor.ToArgb() != 0 || iRegion.FillImage != null))
						{
							iRegion.DrawFill(gp, fOx, fOy, fZoom, fZoom, nTransparency);
						}
					}
				}

			}

		}

		private void DrawImage(Graphics gp, Bitmap drawBitmap)
		{


			if (drawBitmap != null)
			{

				if (m_fZoom == 1.0f)
				{
					gp.DrawImage(drawBitmap, m_pOrigin.X, m_pOrigin.Y);
				}
				else
				{
					gp.DrawImage(drawBitmap, m_srcRect);
				}
			}

		}
		private void DrawOverlay(Graphics gp)
		{
			if (m_xOverlay != null)
			{
				XRegion xRegion = (XRegion)m_xOverlay.First();
				for (int n = 0; n < m_xOverlay.Count; n++)
				{
					if (xRegion != null)
					{
						xRegion.Draw(gp, m_pOrigin.X, m_pOrigin.Y, m_fZ, m_fZ);
						xRegion = (XRegion)m_xOverlay.Next();
					}
				}
			}
		}

		private void StaticStatic(Graphics gp)
		{
			if (m_xStatic != null)
			{
				XRegion xRegion = (XRegion)m_xStatic.First();
				for (int n = 0; n < m_xStatic.Count; n++)
				{
					if (xRegion != null)
					{
						xRegion.Draw(gp, m_pOrigin.X, m_pOrigin.Y, m_fZ, m_fZ);
						xRegion = (XRegion)m_xStatic.Next();
					}
				}

			}
		}

		public void ExpandSelect(float fX, float fY)
		{
			XRegion xRegion = (XRegion)m_xSelect.First();
			if (xRegion != null)
			{
				for (int n = 0; n < m_xSelect.Count; n++)
				{
					if (xRegion != null)
					{
						xRegion.W += fX;
						xRegion.H += fY;
						xRegion = (XRegion)m_xSelect.Next();
					}
				}
				Refresh();
			}
		}
		public void MoveSelect(float fX, float fY)
		{
			XRegion xRegion = (XRegion)m_xSelect.First();
			if (xRegion != null)
			{
				for (int n = 0; n < m_xSelect.Count; n++)
				{
					if (xRegion != null)
					{
						xRegion.X += fX;
						xRegion.Y += fY;
						xRegion = (XRegion)m_xSelect.Next();
					}
				}
				Refresh();
			}
		}

		public void SelectAll()
		{
			m_xSelect.Empty();

			if (m_xOverlay != null)
			{
				uint nCount = m_xOverlay.Count;
				if (nCount > 0)
				{
					XRegion xRegion = (XRegion)m_xOverlay.First();
					for (int n = 0; n < nCount; n++)
					{
						if (xRegion != null)
						{
							m_xSelect.Add(xRegion);
							xRegion = (XRegion)m_xOverlay.Next();
						}
					}
				}
				Refresh();
			}
		}




		public bool SaveOverlay(string sPath)
		{

			if (sPath != null && sPath != "")
			{
				try
				{
					sPath = sPath.ToLower();
					string sImagePath = sPath.Replace(".xml", "_");
					XmlSerializer serializer = new XmlSerializer(typeof(TRegion[]));

					TextWriter writer = new StreamWriter(sPath);

					uint nCount = m_xOverlay.Count;
					TRegion[] Item = new TRegion[nCount];
					XRegion Region = (XRegion)m_xOverlay.First();
					for (int n = 0; n < nCount; n++)
					{
						if (Region != null)
						{
							Item[n] = new TRegion();
							Item[n].Copy(Region);
							if (Region.FillImage != null)
							{
								string sImageRegionPath = sImagePath + Region.ID.ToString() + ".bmp";


								if (File.Exists(sImageRegionPath))
								{
									File.Delete(sImageRegionPath);
								}
								Region.FillImage.Save(sImageRegionPath);

								Item[n].m_sFillImagePath = sImageRegionPath;


							}
							Region = (XRegion)m_xOverlay.Next();
						}
					}
					serializer.Serialize(writer, Item);
					writer.Close();
					return true;

				}
				catch (Exception pe)
				{
					string sMsg = pe.ToString();
				}
			}
			return false;
		}
		public bool LoadOverlay(string sPath)
		{


			if (sPath != null && sPath != "")
			{
				uint uCount = m_xOverlay.Count;
				XRegion Region = (XRegion)m_xOverlay.First();
				for (int n = 0; n < uCount; n++)
				{
					if (Region != null)
					{
						if (Region.FillImage != null) Region.FillImage.Dispose();
						Region = (XRegion)m_xOverlay.Next();
					}
				}
				m_xOverlay.Empty();

				XmlSerializer serializer = new XmlSerializer(typeof(TRegion[]));
				FileStream fs = null;
				try
				{
					fs = new FileStream(sPath, FileMode.Open);
					if (fs != null)
					{
						TRegion[] pItem = (TRegion[])serializer.Deserialize(fs);
						fs.Close();
						if (pItem != null)
						{
							int n;
							int nCount = pItem.Length;
							for (n = 0; n < nCount; n++)
							{
								AddRegion(pItem[n]);
							}

							for (n = 0; n < nCount; n++)
							{
								int nTargetID = pItem[n].m_TargetRegionID;
								if (nTargetID > 0)
								{
									XRegion xRegion = (XRegion)m_xOverlay.Find(pItem[n].m_nID);
									if (xRegion != null)
									{
										XRegion xTargetRegion = (XRegion)m_xOverlay.Find(nTargetID);
										if (xTargetRegion != null)
										{
											xRegion.SetAlign(xTargetRegion, pItem[n].m_eAlignTarget, pItem[n].m_eAlignObject);
										}
									}
								}
							}
						}
						Refresh();
						return true;
					}
				}
				catch (Exception pe)
				{
					if (fs != null) fs.Close();
					string sMsg = pe.ToString();
					Refresh();
				}
			}
			return false;
		}


		private bool AddRegion(TRegion Region)
		{
			if (m_xOverlay != null)
			{
				float[] p = Region.p;
				XRegion xRegion = new XRegion(Region.m_eRShape, p[0], p[1], p[2], p[3], p[4]);
				m_xOverlay.Add(xRegion);
				xRegion.ID = Region.m_nID;
				xRegion.BorderColor = Color.FromArgb(Region.m_cBorderColor);
				xRegion.FillColor = Color.FromArgb(Region.m_cFillColor);
				xRegion.BorderStyle = Region.m_dBorderStyle;
				xRegion.ControlStyle = (REGION_CONTROL_STYLES)Region.m_RCS;
				xRegion.Caption = Region.m_sCaption;

				string sImagePath = Region.m_sFillImagePath;
				if (sImagePath != null && sImagePath != "")
				{
					Image FillImage = Image.FromFile(sImagePath);
					if (xRegion.FillImage != null) xRegion.FillImage.Dispose();
					xRegion.FillImage = new Bitmap(FillImage);
					FillImage.Dispose();
				}
				return true;
			}
			return false;
		}

		public override bool PreProcessMessage(ref Message msg)
		{
			if (msg.HWnd == Handle)
			{
				if (msg.Msg == (int)WINDOW_EVENT.WM_KEYDOWN)
				{
					IntPtr nVirtKey = msg.WParam;    // virtual-key code 
					if (OnKeyDown((Keys)nVirtKey) == true)
					{
						return true;
					}
				}
			}
			return base.PreProcessMessage(ref msg);
		}
		protected bool OnKeyDown(Keys keyConde)
		{
			if (ModifierKeys == Keys.Control)
			{
				if (m_fZ != 0)
				{
					switch (keyConde)
					{
						case Keys.Left:
							ExpandSelect(-1.0f / m_fZ, 0);
							return true;
						case Keys.Right:
							ExpandSelect(1.0f / m_fZ, 0);
							return true;
						case Keys.Up:
							ExpandSelect(0, -1.0f / m_fZ);
							return true;
						case Keys.Down:
							ExpandSelect(0, 1.0f / m_fZ);
							return true;
					}
				}
			}
			if (ModifierKeys == Keys.Shift)
			{
				if (m_fZ != 0)
				{
					switch (keyConde)
					{
						case Keys.Left:
							MoveSelect(-1.0f / m_fZ, 0);
							return true;
						case Keys.Right:
							MoveSelect(1.0f / m_fZ, 0);
							return true;
						case Keys.Up:
							MoveSelect(0, -1.0f / m_fZ);
							return true;
						case Keys.Down:
							MoveSelect(0, 1.0f / m_fZ);
							return true;
					}
				}
			}
			return false;
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (Focused == false) Focus();

			if ((e.Button & MouseButtons.Left) != 0)
			{
				PointF point = new PointF(e.X - m_pOrigin.X, e.Y - m_pOrigin.Y);
				switch (m_eControlMode)
				{
					case E_VIEW_CONTROL_MODE.VCM_POINT:
						if (m_bOverlayControl == true)
						{
							m_bMouseMove = true;
							LButtonDown(point);
						} break;
					case E_VIEW_CONTROL_MODE.VCM_PAN:
						m_pStartPan = point;
						break;
					case E_VIEW_CONTROL_MODE.VCM_ZOOM_IN:
						{
							CalcScreen();

							if (m_fZ == 0)
								return;
							float opx = point.X / m_fZ;
							float opy = point.Y / m_fZ;
							Zoom = m_fZ *= 1.1f;

							if (m_fZ == 0)
								return;
							float npx = point.X / m_fZ;
							float npy = point.Y / m_fZ;

							m_pOrigin.X += (npx - opx) * m_fZ;
							m_pOrigin.Y += (npy - opy) * m_fZ;

							Refresh();
						} break;

					case E_VIEW_CONTROL_MODE.VCM_ZOOM_OUT:
						{
							CalcScreen();

							if (m_fZ == 0)
								return;
							float opx = point.X / m_fZ;
							float opy = point.Y / m_fZ;
							Zoom = m_fZ *= 0.9f;

							if (m_fZ == 0)
								return;
							float npx = point.X / m_fZ;
							float npy = point.Y / m_fZ;
							m_pOrigin.X += (npx - opx) * m_fZ;
							m_pOrigin.Y += (npy - opy) * m_fZ;

							Refresh();
						} break;
				}
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != 0)
			{
				PointF point = new PointF(e.X - m_pOrigin.X, e.Y - m_pOrigin.Y);
				switch (m_eControlMode)
				{
					case E_VIEW_CONTROL_MODE.VCM_POINT:
						{
							if (ModifierKeys == Keys.Alt)
							{
								REGION_CONTROL_STYLES HitRCS = REGION_CONTROL_STYLES.NONE;
								XRegion xRegion = (XRegion)m_xOverlay.First();
								for (int n = 0; n < m_xOverlay.Count; n++)
								{
									if (xRegion != null)
									{
										HitRCS = xRegion.HitTest(point.X / m_fZ, point.Y / m_fZ, xRegion.Spaces / m_fZ, ModifierKeys);
										if (HitRCS != REGION_CONTROL_STYLES.NONE)
										{
											if (m_xActiveRegion != null && xRegion != null && m_xActiveRegion != xRegion)
											{
												m_xActiveRegion.SetAlign(xRegion, HitRCS);
												break;
											}
										}
										else
										{
											xRegion.SetAlign(null, HitRCS);
										}
										xRegion = (XRegion)m_xOverlay.Next();
									}
								}
							}
							else if (m_xSelectRegion.ControlSelect == REGION_CONTROL_STYLES.BR)
							{
								SelectControl();
								m_xSelectRegion.ControlSelect = REGION_CONTROL_STYLES.NONE;
								Refresh();
							}
							if (m_bMouseMove == true)
							{
								m_bMouseMove = false;
								Refresh();
							}
							InvokeOnClick(this, null);
						} break;
					case E_VIEW_CONTROL_MODE.VCM_PAN:
						{
							m_pOrigin.X = e.X - m_pStartPan.X;
							m_pOrigin.Y = e.Y - m_pStartPan.Y;
							Refresh();
						} break;
				}
			}
		}

		public void SelectControl()
		{
			if (m_xOverlay != null)
			{
				XRegion xRegion = (XRegion)m_xOverlay.First();
				for (int n = 0; n < m_xOverlay.Count; n++)
				{
					if (xRegion != null)
					{
						if (xRegion.InnerRegion(m_xSelectRegion.X, m_xSelectRegion.Y, m_xSelectRegion.W, m_xSelectRegion.H) == true)
						{
							xRegion.ControlSelect = REGION_CONTROL_STYLES.MC;
							AddSelect(xRegion);
						}
						xRegion = (XRegion)m_xOverlay.Next();
					}
				}
			}
		}
		private void LButtonDown(PointF point)
		{

			if (m_xOverlay != null)
			{
				m_xActiveRegion = null;
				XRegion xRegion = (XRegion)m_xOverlay.First();
				for (int n = 0; n < m_xOverlay.Count; n++)
				{
					if (xRegion != null)
					{
						if (REGION_CONTROL_STYLES.NONE != xRegion.SelectControl(point.X / m_fZ, point.Y / m_fZ, xRegion.Spaces / m_fZ, ModifierKeys))
						{
							m_xActiveRegion = xRegion;
							break;
						}
						else if (REGION_CONTROL_STYLES.NONE != xRegion.ControlSelect)
						{
							if (m_xActiveRegion == null)
							{
								m_xActiveRegion = xRegion;
							}
							else
							{
								if (m_xActiveRegion.ControlSelect == REGION_CONTROL_STYLES.MC
									&& (xRegion.ControlSelect != REGION_CONTROL_STYLES.MC
									|| xRegion.InnerRegion(m_xActiveRegion) == true))
								{
									m_xActiveRegion.ControlSelect = REGION_CONTROL_STYLES.NONE;
									m_xActiveRegion = xRegion;
								}
								else
								{
									xRegion.ControlSelect = REGION_CONTROL_STYLES.NONE;
								}
							}
						}
						xRegion = (XRegion)m_xOverlay.Next();
					}
				}

				if (ModifierKeys != Keys.Alt)
				{
					if (m_xActiveRegion == null)
					{
						m_xSelect.Empty();
						m_xSelectRegion.Set(point.X / m_fZ, point.Y / m_fZ, 1, 1, 0);
						m_xSelectRegion.SelectControl(point.X / m_fZ, point.Y / m_fZ, m_xSelectRegion.Spaces / m_fZ, ModifierKeys);
						m_xSelectRegion.ControlStyle = REGION_CONTROL_STYLES.BR;
						m_xSelectRegion.ControlSelect = REGION_CONTROL_STYLES.BR;
					}
					else
					{
						if (ModifierKeys == Keys.Control || m_xSelect.Count > 1)
						{
							AddSelect(m_xActiveRegion);
							float fCx = m_xActiveRegion.X;
							float fCy = m_xActiveRegion.Y;
							PointF Fix = m_xActiveRegion.Fix;
							xRegion = (XRegion)m_xSelect.First();
							for (int n = 0; n < m_xSelect.Count; n++)
							{
								if (xRegion != null)
								{
									xRegion.ControlSelect = m_xActiveRegion.ControlSelect;
									if (m_xActiveRegion.ControlSelect == REGION_CONTROL_STYLES.MC)
									{
										PointF FixN = new PointF(Fix.X - fCx + xRegion.X, Fix.Y - fCy + xRegion.Y);
										xRegion.Fix = FixN;
									}
									xRegion = (XRegion)m_xSelect.Next();
								}
							}
						}
						else
						{
							xRegion = (XRegion)m_xOverlay.First();
							for (int n = 0; n < m_xOverlay.Count; n++)
							{
								if (xRegion != null)
								{
									if (m_xActiveRegion != xRegion && m_xSelect.Count == 1)
									{
										xRegion.ControlSelect = REGION_CONTROL_STYLES.NONE;
									}
									xRegion = (XRegion)m_xOverlay.Next();
								}
							}
							m_xSelect.Empty();
							AddSelect(m_xActiveRegion);
						}
						m_xOverlay.Exchange(m_xOverlay.First(), m_xActiveRegion);
					}
					Refresh();
				}
			}
		}

		private void AddSelect(XRegion xRegion)
		{
			if (xRegion != null)
			{
				//if (null == m_xSelect.Find(xRegion.ID))
				{
					m_xSelect.Add(xRegion);
					if (ChangeSelect != null) ChangeSelect(this);
				}
			}
		}
		public void RefreshAlign()
		{
			if (m_xOverlay != null)
			{
				XRegion Region = (XRegion)m_xOverlay.First();
				uint nCount = m_xOverlay.Count;
				for (int n = 0; n < nCount; n++)
				{
					if (Region != null)
					{
						Region.Align();
						Region = (XRegion)m_xOverlay.Next();
					}
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			PointF point = new PointF(e.X - m_pOrigin.X, e.Y - m_pOrigin.Y);
			switch (m_eControlMode)
			{
				case E_VIEW_CONTROL_MODE.VCM_POINT:
					{
						if ((e.Button & MouseButtons.Left) != 0)
						{
							m_bMouseMove = true;

							if (ModifierKeys == Keys.Alt && m_xOverlay != null)
							{
								XRegion xRegion = (XRegion)m_xOverlay.First();
								for (int n = 0; n < m_xOverlay.Count; n++)
								{
									if (xRegion != null)
									{
										xRegion.HitTest(point.X / m_fZ, point.Y / m_fZ, xRegion.Spaces / m_fZ, ModifierKeys);
										xRegion = (XRegion)m_xOverlay.Next();
									}
								}
							}

							if (m_xActiveRegion != null && m_xOverlay != null && m_xOverlay.Count != 0)
							{
								m_xActiveRegion.Control(point.X, point.Y, m_fZ, m_fZ);
								if (m_xSelect.Count > 1)
								{
									if (m_xActiveRegion.ControlSelect == REGION_CONTROL_STYLES.MC)
									{
										XRegion xRegion = (XRegion)m_xSelect.First();
										for (int n = 0; n < m_xSelect.Count; n++)
										{
											if (xRegion != null)
											{
												xRegion.Control(point.X, point.Y, m_fZ, m_fZ);
												xRegion = (XRegion)m_xSelect.Next();
											}
										}
									}
								}
								Refresh();
							}
							else if (m_xSelectRegion.ControlSelect == REGION_CONTROL_STYLES.BR)
							{
								m_xSelectRegion.Control(point.X, point.Y, m_fZ, m_fZ);
								Refresh();
							}
						}
					} break;
				case E_VIEW_CONTROL_MODE.VCM_PAN:
					{
						if ((e.Button & MouseButtons.Left) != 0)
						{
							m_pOrigin.X = e.X - m_pStartPan.X;
							m_pOrigin.Y = e.Y - m_pStartPan.Y;
							CalcScreen();
							Refresh();
						}
					} break;
			}
		}
	}
}
