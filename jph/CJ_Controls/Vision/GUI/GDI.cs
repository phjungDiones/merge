using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJ_Controls.Vision.BaseData;
namespace CJ_Controls.Vision.GUI
{
	internal class GDI
	{
		public enum PEN_STYLE
		{
			PS_SOLID = 0,
			PS_DASH = 1,       /* -------  */
			PS_DOT = 2,       /* .......  */
			PS_DASHDOT = 3,       /* _._._._  */
			PS_DASHDOTDOT = 4,       /* _.._.._  */
			PS_NULL = 5,
			PS_INSIDEFRAME = 6,
			PS_USERSTYLE = 7,
			PS_ALTERNATE = 8,
			PS_STYLE_MASK = 0x0000000F,

			PS_ENDCAP_ROUND = 0x00000000,
			PS_ENDCAP_SQUARE = 0x00000100,
			PS_ENDCAP_FLAT = 0x00000200,
			PS_ENDCAP_MASK = 0x00000F00,

			PS_JOIN_ROUND = 0x00000000,
			PS_JOIN_BEVEL = 0x00001000,
			PS_JOIN_MITER = 0x00002000,
			PS_JOIN_MASK = 0x0000F000,

			PS_COSMETIC = 0x00000000,
			PS_GEOMETRIC = 0x00010000,
			PS_TYPE_MASK = 0x000F0000
		};


		internal static int RGB(byte r, byte g, byte b)
		{
			return r | (g << 8) | (b << 16);
		}

		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		internal static extern bool Rectangle(
		   IntPtr hdc,
		   int ulCornerX, int ulCornerY,
		   int lrCornerX, int lrCornerY);
		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		internal static extern bool RoundRect(
		  IntPtr hdc,
		  int nLeftRect,
		  int nTopRect,
		  int nRightRect,
		  int nBottomRect,
		  int nWidth,
		  int nHeight);

		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		internal static extern IntPtr CreateSolidBrush(int crColor);

		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		internal static extern IntPtr CreatePen(
		  int fnPenStyle,    // pen style
		  int nWidth,        // pen width
		  int crColor   // pen color
		);

		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		internal static extern IntPtr SelectObject(
		  IntPtr hdc,          // handle to device context
		  IntPtr hgdiobj   // handle to object
		);

		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		internal static extern bool DeleteObject(IntPtr hgdiobj);
	}
}
