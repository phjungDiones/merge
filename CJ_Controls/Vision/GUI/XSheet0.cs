using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using CJ_Controls.Vision.BaseData;
namespace CJ_Controls.Vision.GUI
{

    public partial class XSheet0 : Control
    {
        public event ChangeCell ChangeCell;
        public event ChangeText ChangeText;
        private static int WM_KEYDOWN = 0x0100;
//        private static int WM_KEYUP = 0x0101;

        public enum SEL_POS
        {
            SP = 0,
            EP = 1,
            MAX_SEL_POS
        }

        //  private Bitmap m_iBmp;
        private string m_sPath;
        private int m_nCols;
        private int m_nRows;
        private int[] m_pColWidth;
        private int[] m_pRowHeight;
        private int[] m_pGroup;
        private int[] m_pGroupCheck;

        private XCell[][] m_cItem;

        private Color m_cBorderColor;
        private Color m_cSelectColor;

        private int m_nCol;
        private int m_nRow;
        private int m_nRowSelectCount;
        private int m_nColSelectCount;
        private Point[] m_pSelPos = new Point[(int)SEL_POS.MAX_SEL_POS];

        private int[] m_pAbsColWidth;
        private int[] m_pAbsRowHeight;
        private int m_lRoundWidth;
        private int m_lRoundHeight;

        private int m_lOriginX;
        private int m_lOriginY;
        private int m_lPointH;
        private Point[] m_pDrawH;
        private int m_lPointV;
        private Point[] m_pDrawV;
        private Point m_pEditPos = new Point();
        private Point m_pFix;

        private float m_dZoom;


        private int m_lWidth;
        private int m_lHeight;
        private int m_lDefaultWidth;
        private int m_lDefaultHeight;
        private StringFormat m_TextAlign = new StringFormat();


        private bool m_bTextSelect;

        private bool m_bPopupMenu;

        private XArray m_xBackup = new XArray();
        private CellUnit m_xClip;
        public XSheet0()
        {
            m_xClip = null;
            m_bPopupMenu = false;
            m_bTextSelect = false;

 //           m_nRound = 6;
            m_cBorderColor = Color.AliceBlue;
            m_cSelectColor = Color.OrangeRed;
            m_pSelPos[(int)SEL_POS.SP] = new Point();
            m_pSelPos[(int)SEL_POS.EP] = new Point();
            m_lDefaultWidth = 80;
            m_lDefaultHeight = 22;

            m_dZoom = 0;

            m_TextAlign.Alignment = StringAlignment.Center;
            m_TextAlign.LineAlignment = StringAlignment.Center;

            InitializeComponent();
            // 
            // txtEdit
            // 
            txtEdit = new TextBox();
            this.txtEdit.Location = new System.Drawing.Point(0, 0);
            this.txtEdit.Multiline = true;
            this.txtEdit.Name = "txtEdit";
            this.txtEdit.Size = new System.Drawing.Size(82, 21);
            this.txtEdit.TabIndex = 17;
            this.txtEdit.Visible = false;
            this.txtEdit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEdit_KeyUp);
            this.txtEdit.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtEdit_PreviewKeyDown);



            this.Controls.Add(this.txtEdit);
            //txtEdit.Show();
            //           NewMatrix(m_nRows, m_nCols);

        }

        public Point[] Find(int lRow, int lCol, int lRowSelectCount, int lColSelectCount, string sFind)
        {
            Point[] FindPos = null;
            Point[] pSel = GetSelect(lRow, lCol, lRowSelectCount, lColSelectCount);
            if (pSel != null)
            {
                for (int nRow = pSel[(int)SEL_POS.SP].Y; nRow < pSel[(int)SEL_POS.EP].Y; nRow++)
                {
                    for (int nCol = pSel[(int)SEL_POS.SP].X; nCol < pSel[(int)SEL_POS.EP].X; nCol++)
                    {
                        if (m_cItem[nRow][nCol].Text == sFind)
                        {
                            FindPos = new Point[1];
                            FindPos[0] = new Point(nCol, nRow);
                            return FindPos;
                        }
                    }
                }

            }

            return FindPos;
        }


        public XCell[][] GetItem()
        {
            return m_cItem;
        }

        public XCell GetCell(int lRow, int lCol)
        {
            if (m_cItem != null && 0 <= lRow && lRow < m_nRows && 0 <= lCol && lCol < m_nCols)
            {
                return m_cItem[lRow][lCol];
            }
            return null;
        }
        public void SetCell(int lRow, int lCol, XCell XCell)
        {
            if (m_cItem != null && 0 <= lRow && lRow < m_nRows && 0 <= lCol && lCol < m_nCols)
            {
                m_cItem[lRow][lCol] = XCell;
            }
        }

        public string GetMatrixText(int lRow, int lCol)
        {
            if (m_cItem != null && 0 <= lRow && lRow < m_nRows && 0 <= lCol && lCol < m_nCols)
            {
                return m_cItem[lRow][lCol].Text;
            }
            return null;
        }
        public string GetText()
        {
            if (m_cItem != null && 0 <= m_nRow && m_nRow < m_nRows && 0 <= m_nCol && m_nCol < m_nCols)
            {
                return m_cItem[m_nRow][m_nCol].Text;
            }
            return null;
        }

        public void SetMatrixText(int lRow, int lCol, string sText)
        {
            if (m_cItem != null && 0 <= lRow && lRow < m_nRows && 0 <= lCol && lCol < m_nCols)
            {
                m_cItem[lRow][lCol].Text = sText;
            }
        }
        public void SetText(string sText)
        {
            if (m_cItem != null && 0 <= m_nRow && m_nRow < m_nRows && 0 <= m_nCol && m_nCol < m_nCols)
            {
                m_cItem[m_nRow][m_nCol].Text = sText;
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

        public bool PopupMenu
        {
            get
            {
                return m_bPopupMenu;
            }
            set
            {
                m_bPopupMenu = value;
            }
        }

        private string Path
        {
            get
            {
                return m_sPath;
            }
            set
            {
                m_sPath = value;
            }
        }
        public float Zoom
        {
            get
            {
                return m_dZoom;
            }
            set
            {
                m_dZoom = value;
            }
        }

        public Color SelectColor
        {
            get
            {
                return m_cSelectColor;
            }
            set
            {
                m_cSelectColor = value;
            }
        }

        public Color BorderColor
        {
            get
            {
                return m_cBorderColor;
            }
            set
            {
                m_cBorderColor = value;
            }
        }


        public int DefaultWidth
        {
            get
            {
                return m_lDefaultWidth;
            }
            set
            {
                m_lDefaultWidth = value;
            }
        }
        public int DefaultHeight
        {
            get
            {
                return m_lDefaultHeight;
            }
            set
            {
                m_lDefaultHeight = value;
            }
        }

        public int Col
        {
            get
            {
                return m_nCol;
            }
            set
            {
                m_nCol = value;
            }
        }
        public int Row
        {
            get
            {
                return m_nRow;
            }
            set
            {
                m_nRow = value;
            }
        }
        public int RowSelectCount
        {
            get
            {
                return m_nRowSelectCount;
            }
            set
            {
                m_nRowSelectCount = value;
            }
        }
        public int ColSelectCount
        {
            get
            {
                return m_nColSelectCount;
            }
            set
            {
                m_nColSelectCount = value;
            }
        }

        public int Cols
        {
            get
            {
                return m_nCols;
            }
            set
            {
                m_nCols = value;
                NewMatrix(m_nRows, m_nCols);
            }
        }
        public int Rows
        {
            get
            {
                return m_nRows;
            }
            set
            {
                m_nRows = value;
                NewMatrix(m_nRows, m_nCols);
            }
        }

        public int[] GetColWidth()
        {
            return m_pColWidth;
        }
        public void SetColWidth(int[] ColWidth)
        {
            m_pColWidth = ColWidth;
            NewGrid();

            Refresh();

        }

        public int[] GetRowHeight()
        {
            return m_pRowHeight;
        }
        public void SetRowHeight(int[] RowHeight)
        {
            m_pRowHeight = RowHeight;
            NewGrid();

            Refresh();

        }



        private void NewMatrix(int lRows, int lCols)
        {
            //            if (m_cItem != null && m_nRows == lRows && m_nCols == lCols) return;

            m_xBackup.Empty();

            m_nRows = lRows;
            m_nCols = lCols;

            if (lRows > 0 && lCols > 0)
            {
                m_pRowHeight = new int[lRows];
                m_pGroup = new int[lRows];
                m_pGroupCheck = new int[lRows];
                m_pColWidth = new int[lCols];
                m_cItem = new XCell[lRows][];

                if (lRows > 0 && lCols > 0)
                {
                    m_lHeight = 0;
                    for (int lRow = 0; lRow < m_nRows; lRow++)
                    {
                        m_cItem[lRow] = new XCell[lCols];
                        m_pGroup[lRow] = 0;
                        m_pGroupCheck[lRow] = 0;
                        m_pRowHeight[lRow] = m_lDefaultHeight;
                        m_lHeight += m_pRowHeight[lRow];
                        for (int lCol = 0; lCol < lCols; lCol++)
                        {
                            m_cItem[lRow][lCol] = new XCell();
                        }
                    }

                    m_lWidth = 0;
                    for (int lCol = 0; lCol < m_nCols; lCol++)
                    {
                        m_pColWidth[lCol] = m_lDefaultWidth;
                        m_lWidth += m_pColWidth[lCol];
                    }

                    NewGrid();

                    Refresh();
                }
            }


        }

        private void NewGrid()
        {

            if (m_nCols > 0 && m_nRows > 0)
            {
                m_pAbsRowHeight = new int[m_nRows + 3];
                m_pAbsColWidth = new int[m_nCols + 3];


                m_lPointH = m_nRows * 2 + 2;
                m_pDrawH = new Point[m_lPointH + 5];
                m_lPointV = m_nCols * 2 + 2;
                m_pDrawV = new Point[m_lPointV + 5];
                MoveGrid(0, 0);

            }

        }

        private void MoveGrid(int lOriginX, int lOriginY)
        {

            if (m_nCols > 0 && m_nRows > 0)
            {
                int lRow, lCol;

                m_pAbsRowHeight[0] = lOriginY;
                for (lRow = 0; lRow < m_nRows; lRow++)
                {
                    m_pAbsRowHeight[lRow + 1] = m_pAbsRowHeight[lRow] + m_pRowHeight[lRow];
                }
                if (lOriginY == 0) m_lRoundHeight = m_pAbsRowHeight[m_nRows];
                m_pAbsColWidth[0] = lOriginX;
                for (lCol = 0; lCol < m_nCols; lCol++)
                {
                    m_pAbsColWidth[lCol + 1] = m_pAbsColWidth[lCol] + m_pColWidth[lCol];
                }
                if (lOriginX == 0) m_lRoundWidth = m_pAbsColWidth[m_nCols];


                for (lRow = 0; lRow < m_nRows + 1; lRow += 2)
                {
                    int lR = lRow * 2;
                    int sx = m_pAbsColWidth[0];
                    int ex = m_pAbsColWidth[m_nCols];
                    m_pDrawH[lR].X = sx;
                    m_pDrawH[lR].Y = m_pAbsRowHeight[lRow];
                    lR++;
                    m_pDrawH[lR].X = ex;
                    m_pDrawH[lR].Y = m_pAbsRowHeight[lRow];
                    lR++;
                    m_pDrawH[lR].X = ex;
                    m_pDrawH[lR].Y = m_pAbsRowHeight[lRow + 1];
                    lR++;
                    m_pDrawH[lR].X = sx;
                    m_pDrawH[lR].Y = m_pAbsRowHeight[lRow + 1];
                    lR++;
                    m_pDrawH[lR].X = sx;
                    m_pDrawH[lR].Y = m_pAbsRowHeight[lRow + 2];
                }

                for (lCol = 0; lCol < m_nCols + 1; lCol += 2)
                {
                    int lC = lCol * 2;
                    int sy = m_pAbsRowHeight[0];
                    int ey = m_pAbsRowHeight[m_nRows];
                    m_pDrawV[lC].Y = sy;
                    m_pDrawV[lC].X = m_pAbsColWidth[lCol];
                    lC++;
                    m_pDrawV[lC].Y = ey;
                    m_pDrawV[lC].X = m_pAbsColWidth[lCol];
                    lC++;
                    m_pDrawV[lC].Y = ey;
                    m_pDrawV[lC].X = m_pAbsColWidth[lCol + 1];
                    lC++;
                    m_pDrawV[lC].Y = sy;
                    m_pDrawV[lC].X = m_pAbsColWidth[lCol + 1];
                    lC++;
                    m_pDrawV[lC].Y = sy;
                    m_pDrawV[lC].X = m_pAbsColWidth[lCol + 2];
                }

            }

        }

        public Point[] GetSelect()
        {
            Point[] PP = new Point[2];

            if (m_nRowSelectCount > 0)
            {
                PP[(int)SEL_POS.SP].Y = m_nRow;
                PP[(int)SEL_POS.EP].Y = m_nRow + m_nRowSelectCount;
            }
            else
            {
                PP[(int)SEL_POS.SP].Y = m_nRow + m_nRowSelectCount;
                PP[(int)SEL_POS.EP].Y = m_nRow;
            }

            if (m_nColSelectCount > 0)
            {
                PP[(int)SEL_POS.SP].X = m_nCol;
                PP[(int)SEL_POS.EP].X = m_nCol + m_nColSelectCount;
            }
            else
            {
                PP[(int)SEL_POS.SP].X = m_nCol + m_nColSelectCount;
                PP[(int)SEL_POS.EP].X = m_nCol;
            }
            if (PP[(int)SEL_POS.SP].X > PP[(int)SEL_POS.EP].X)
                return null;
            if (PP[(int)SEL_POS.SP].Y > PP[(int)SEL_POS.EP].Y)
                return null;
            if (PP[(int)SEL_POS.SP].X < 0)
                PP[(int)SEL_POS.SP].X = 0;
            if (PP[(int)SEL_POS.SP].Y < 0)
                PP[(int)SEL_POS.SP].Y = 0;
            if (PP[(int)SEL_POS.EP].X > m_nCols)
                PP[(int)SEL_POS.EP].X = m_nCols;
            if (PP[(int)SEL_POS.EP].Y > m_nRows)
                PP[(int)SEL_POS.EP].Y = m_nRows;

            return PP;

        }
        private Point[] GetSelect(int lRow, int lCol, int lRowSelectCount, int lColSelectCount)
        {
            Point[] PP = new Point[2];

            if (lRowSelectCount > 0)
            {
                PP[(int)SEL_POS.SP].Y = lRow;
                PP[(int)SEL_POS.EP].Y = lRow + lRowSelectCount;
            }
            else
            {
                PP[(int)SEL_POS.SP].Y = lRow + lRowSelectCount;
                PP[(int)SEL_POS.EP].Y = lRow;
            }

            if (lColSelectCount > 0)
            {
                PP[(int)SEL_POS.SP].X = lCol;
                PP[(int)SEL_POS.EP].X = lCol + lColSelectCount;
            }
            else
            {
                PP[(int)SEL_POS.SP].X = lCol + lColSelectCount;
                PP[(int)SEL_POS.EP].X = lCol;
            }
            if (PP[(int)SEL_POS.SP].X > PP[(int)SEL_POS.EP].X)
                return null;
            if (PP[(int)SEL_POS.SP].Y > PP[(int)SEL_POS.EP].Y)
                return null;
            if (PP[(int)SEL_POS.SP].X < 0)
                PP[(int)SEL_POS.SP].X = 0;
            if (PP[(int)SEL_POS.SP].Y < 0)
                PP[(int)SEL_POS.SP].Y = 0;
            if (PP[(int)SEL_POS.EP].X > m_nCols)
                PP[(int)SEL_POS.EP].X = m_nCols;
            if (PP[(int)SEL_POS.EP].Y > m_nRows)
                PP[(int)SEL_POS.EP].Y = m_nRows;

            return PP;

        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            Graphics gp = pe.Graphics;
            if (0 < m_nCols && 0 < m_nRows)
            {
                DrawCell(gp);
                DrawText(gp);
                DrawGrid(gp);
                DrawSelect(gp);
                //                if (m_bFix) DrawControl(gp);
            }


        }

        protected void DrawControl(Graphics gp)
        {

        }
        protected void DrawGrid(Graphics gp)
        {
            if (m_pDrawH != null && m_lPointH != 0 && m_pDrawV != null && m_lPointV != 0)
            {
                Pen BolderPen = new Pen(this.BorderColor);
                gp.DrawLines(BolderPen, m_pDrawH);
                gp.DrawLines(BolderPen, m_pDrawV);
                BolderPen.Dispose();
            }
        }


        protected void DrawCell(Graphics gp)
        {

            int lRow, lCol;

            if (m_pAbsColWidth != null && m_pAbsRowHeight != null)
            {
                for (lRow = 0; lRow < m_nRows; lRow++)
                {

                    for (lCol = 0; lCol < m_nCols; lCol++)
                    {
                        XCell pItem = m_cItem[lRow][lCol];

                        if (pItem.BackImage != null)
                        {
                            float dZoom = m_dZoom;
                            if (dZoom == 0)
                            {
                                float dW = (float)m_pColWidth[lCol] / pItem.BackImage.Width;
                                float dH = (float)m_pRowHeight[lRow] / pItem.BackImage.Height;
                                if (dW > dH) dZoom = dH;
                                else dZoom = dW;

                            }
                            float fWidth = pItem.BackImage.Width * dZoom;
                            float fHeight = pItem.BackImage.Height * dZoom;
                            float fX = m_pAbsColWidth[lCol] + (m_pColWidth[lCol] - fWidth) / 2;
                            float fY = m_pAbsRowHeight[lRow] + (m_pRowHeight[lRow] - fHeight) / 2;

                            RectangleF rRect = new RectangleF
                                  (
                                      fX,
                                      fY,
                                      fWidth,
                                      fHeight

                                  );
                            gp.DrawImage(pItem.BackImage, rRect);

                        }
                        else
                        {
                            Color cBackColor = pItem.BackColor;
                            if (cBackColor.A != 0)
                            {
                                Rectangle rRect = new Rectangle
                                    (
                                        m_pAbsColWidth[lCol] + 1,
                                        m_pAbsRowHeight[lRow] + 1,
                                        m_pColWidth[lCol] - 1,
                                        m_pRowHeight[lRow] - 1
                                    );

                                    SolidBrush drawBrushBack = new SolidBrush(cBackColor);
                                    gp.FillRectangle(drawBrushBack, rRect);
                                    drawBrushBack.Dispose();


                            }
                        }

                    }
                    if (m_pGroup != null && m_pGroup[lRow] > 0)
                    {
                        Pen BorderPen = new Pen(Color.Blue);
                        Rectangle rRect = new Rectangle
                            (
                                m_pAbsColWidth[0] + 1,
                                m_pAbsRowHeight[lRow] + m_pRowHeight[lRow] / 2 - 5,
                                10,
                                10

                            );
                        gp.DrawRectangle(BorderPen, rRect);
                        gp.DrawLine(BorderPen, new Point(rRect.Left, rRect.Top + 5), new Point(rRect.Right, rRect.Top + 5));
                        if (m_pGroupCheck != null && m_pGroupCheck[lRow] != 0)
                        {
                            gp.DrawLine(BorderPen, new Point(rRect.Left + 5, rRect.Top), new Point(rRect.Left + 5, rRect.Bottom));
                        }
                        BorderPen.Dispose();

                    }
                    else if (m_pGroupCheck != null && m_pGroupCheck[lRow] != 0)
                    {
                        Pen BorderPen = new Pen(Color.Blue);
                        gp.DrawLine(BorderPen, new Point(m_pAbsColWidth[0] + 6, m_pAbsRowHeight[lRow]), new Point(m_pAbsColWidth[0] + 6, m_pAbsRowHeight[lRow + 1]));
                        BorderPen.Dispose();
                    }
                }
            }

        }


        private void DrawRound(Graphics gp, Color cFillColor, Rectangle Rect)
        {
            /*
            SolidBrush drawBrushBack = new SolidBrush(BackColor);
            gp.FillRectangle(drawBrushBack, Rect);
            drawBrushBack.Dispose();


            IntPtr hdc = gp.GetHdc();
            int nColor = GDI.RGB(cFillColor.R, cFillColor.G, cFillColor.B);
            IntPtr hBrush = GDI.CreateSolidBrush(nColor);
            IntPtr oBrush = GDI.SelectObject(hdc, hBrush);

            //IntPtr hPen = GDI.CreatePen((int)GDI.PEN_STYLE.PS_SOLID, 1, GDI.RGB(m_cBorderColor.R, m_cBorderColor.G, m_cBorderColor.B));
            Color cRoundColor = Color.Navy;
            IntPtr hPen = GDI.CreatePen((int)GDI.PEN_STYLE.PS_SOLID, 1, GDI.RGB(cRoundColor.R, cRoundColor.G, cRoundColor.B));
            IntPtr oPen = GDI.SelectObject(hdc, hPen);

            GDI.RoundRect(hdc, Rect.Left, Rect.Top, Rect.Right, Rect.Bottom, m_nRound, m_nRound);
            GDI.SelectObject(hdc, oPen);
            GDI.DeleteObject(hPen);


            GDI.SelectObject(hdc, oBrush);
            GDI.DeleteObject(hBrush);
            gp.ReleaseHdc(hdc);


            int nSpace = m_nRound / 2;

            Pen BolderPen = new Pen(Color.FromArgb((byte)(BackColor.R / 1.2), (byte)(BackColor.G / 1.2), (byte)(BackColor.B / 1.2)));
            gp.DrawLine(BolderPen, Rect.Left + nSpace, Rect.Top + 1, Rect.Right - nSpace, Rect.Top + 1);
            gp.DrawLine(BolderPen, Rect.Right - 2, Rect.Top + nSpace, Rect.Right - 2, Rect.Bottom - nSpace);
            gp.DrawLine(BolderPen, Rect.Left + 1, Rect.Top + nSpace, Rect.Left + 1, Rect.Bottom - nSpace);
            gp.DrawLine(BolderPen, Rect.Left + nSpace, Rect.Bottom - 2, Rect.Right - nSpace, Rect.Bottom - 2);
            BolderPen.Dispose();
            */
        }


        protected void DrawText(Graphics gp)
        {
            try
            {
                int lRow, lCol;

                if (m_pAbsColWidth != null && m_pAbsRowHeight != null)
                {
                    for (lRow = 0; lRow < m_nRows; lRow++)
                    {
                        if (m_pRowHeight[lRow] > 1)
                        {
                            for (lCol = 0; lCol < m_nCols; lCol++)
                            {
                                XCell pItem = m_cItem[lRow][lCol];


                                string sText = pItem.Text;

                                if (sText != null && sText != "")
                                {
                                    Rectangle rRect = new Rectangle
                                        (
                                            m_pAbsColWidth[lCol] + 1,
                                            m_pAbsRowHeight[lRow] + 1,
                                            m_pColWidth[lCol] - 1,
                                            m_pRowHeight[lRow] - 1
                                        );

                                    StringFormat stTextAlign = pItem.Align;
                                    if (pItem.Align == null)
                                    {
                                        stTextAlign = m_TextAlign;
                                    }

                                    Color cForeColor = pItem.ForeColor;
                                    if (cForeColor.A == 0)
                                    {
                                        cForeColor = ForeColor;
                                    }

                                    Font fFont = pItem.Font;
                                    if (fFont == null)
                                    {
                                        fFont = this.Font;
                                    }


                                    SolidBrush drawBrushFore = new SolidBrush(cForeColor);
                                    gp.DrawString(sText, fFont, drawBrushFore, rRect, stTextAlign);
                                    drawBrushFore.Dispose();

                                }

                            }
                        }

                    }
                }
            }
            catch (Exception pe)
            {
                string sMsg = pe.ToString();
            }

        }

        public void DrawFastText(int nStartX, int nStartY, int nEndX, int nEndY, int nUpdateCol)
        {
            Point sp = GetPos(0, 0);
            if (nStartY < sp.Y) nStartY = sp.Y;
            Point ep = GetPos(0, Height);
            if (nEndY > ep.Y) nEndY = ep.Y;

            if (0 <= nStartX && nStartX <= m_nCols
             && 0 <= nStartY && nStartY <= m_nRows
             && 0 <= nEndX && nEndX <= m_nCols
             && 0 <= nEndY && nEndY <= m_nRows
             && nStartX < nEndX && nStartY < nEndY)
            {


                if (Visible == true)
                {
                    Graphics gp = CreateGraphics();
                    if (gp != null)
                    {
                        if (m_pAbsColWidth != null && m_pAbsRowHeight != null)
                        {
                            for (int lRow = nStartY; lRow < nEndY; lRow++)
                            {
                                if (m_pRowHeight[lRow] > 1)
                                {
                                    for (int lCol = nStartX; lCol < nEndX; lCol++)
                                    {
                                        XCell pItem = m_cItem[lRow][lCol];
                                        if (nUpdateCol >= 0 && m_cItem[lRow][nUpdateCol].Param != 0)
                                        {
                                            if (nUpdateCol >= 0) m_cItem[lRow][nUpdateCol].Param = 0;
                                            string sText = pItem.Text;

                                            if (sText != null && sText != "")
                                            {
                                                Rectangle rRect = new Rectangle
                                                    (
                                                        m_pAbsColWidth[lCol] + 1,
                                                        m_pAbsRowHeight[lRow] + 1,
                                                        m_pColWidth[lCol] - 1,
                                                        m_pRowHeight[lRow] - 1
                                                    );


                                                Color cBackColor = pItem.BackColor;
                                                if (cBackColor.A == 0)
                                                {
                                                    cBackColor = BackColor;
                                                }
                                                SolidBrush drawBrushBack = new SolidBrush(cBackColor);
                                                gp.FillRectangle(drawBrushBack, rRect);
                                                drawBrushBack.Dispose();


                                                StringFormat stTextAlign = pItem.Align;
                                                if (pItem.Align == null)
                                                {
                                                    stTextAlign = m_TextAlign;
                                                }

                                                Color cForeColor = pItem.ForeColor;
                                                if (cForeColor.A == 0)
                                                {
                                                    cForeColor = ForeColor;
                                                }

                                                Font fFont = pItem.Font;
                                                if (fFont == null)
                                                {
                                                    fFont = this.Font;
                                                }


                                                SolidBrush drawBrushFore = new SolidBrush(cForeColor);
                                                gp.DrawString(sText, fFont, drawBrushFore, rRect, stTextAlign);
                                                drawBrushFore.Dispose();
                                            }
                                        }

                                    }
                                }

                            }
                        }
                        gp.Dispose();
                    }

                }
            }

        }

        public void DrawFastParam(int nStartX, int nStartY, int nEndX, int nEndY)
        {
            if (0 <= nStartX && nStartX <= m_nCols
             && 0 <= nStartY && nStartY <= m_nRows
             && 0 <= nEndX && nEndX <= m_nCols
             && 0 <= nEndY && nEndY <= m_nRows
             && nStartX < nEndX && nStartY < nEndY)
            {
                if (Visible == true)
                {
                    Graphics gp = CreateGraphics();
                    if (gp != null)
                    {
                        if (m_pAbsColWidth != null && m_pAbsRowHeight != null)
                        {
                            for (int lRow = nStartY; lRow < nEndY; lRow++)
                            {
                                if (m_pRowHeight[lRow] > 1)
                                {

                                    for (int lCol = nStartX; lCol < nEndX; lCol++)
                                    {
                                        XCell pItem = m_cItem[lRow][lCol];

                                        string sText = pItem.Param.ToString();
                                        if (sText != pItem.Text)
                                        {
                                            pItem.Text = sText;

                                            if (sText != null && sText != "")
                                            {
                                                Rectangle rRect = new Rectangle
                                                    (
                                                        m_pAbsColWidth[lCol] + 1,
                                                        m_pAbsRowHeight[lRow] + 1,
                                                        m_pColWidth[lCol] - 1,
                                                        m_pRowHeight[lRow] - 1
                                                    );


                                                Color cBackColor = pItem.BackColor;
                                                if (cBackColor.A == 0)
                                                {
                                                    cBackColor = BackColor;
                                                }
                                                SolidBrush drawBrushBack = new SolidBrush(cBackColor);
                                                gp.FillRectangle(drawBrushBack, rRect);
                                                drawBrushBack.Dispose();


                                                StringFormat stTextAlign = pItem.Align;
                                                if (pItem.Align == null)
                                                {
                                                    stTextAlign = m_TextAlign;
                                                }

                                                Color cForeColor = pItem.ForeColor;
                                                if (cForeColor.A == 0)
                                                {
                                                    cForeColor = ForeColor;
                                                }

                                                Font fFont = pItem.Font;
                                                if (fFont == null)
                                                {
                                                    fFont = this.Font;
                                                }


                                                SolidBrush drawBrushFore = new SolidBrush(cForeColor);
                                                gp.DrawString(sText, fFont, drawBrushFore, rRect, stTextAlign);
                                                drawBrushFore.Dispose();

                                            }
                                        }
                                    }
                                }

                            }
                        }
                        gp.Dispose();
                    }

                }
            }

        }
        public void ResetItem()
        {
            for (int nRow = 0; nRow < m_nRows; nRow++)
            {
                for (int nCol = 0; nCol < m_nCols; nCol++)
                {
                    m_cItem[nRow][nCol].Reset();
                }
            }
            Refresh();
        }
        public void RefreshSelect()
        {
            Rectangle Rect = new Rectangle();
            if (m_pAbsColWidth != null && m_pAbsRowHeight != null)
            {
                Rect.X = m_pAbsColWidth[m_pSelPos[(int)SEL_POS.SP].X];
                Rect.Y = m_pAbsRowHeight[m_pSelPos[(int)SEL_POS.SP].Y];
                Rect.Width = m_pAbsColWidth[m_pSelPos[(int)SEL_POS.EP].X + 1] - Rect.X;
                Rect.Height = m_pAbsRowHeight[m_pSelPos[(int)SEL_POS.EP].Y + 1] - Rect.Y;
                Invalidate(Rect, true); // Window based
                Point[] pSel = GetSelect();
                if (pSel != null)
                {
                    Rect.X = m_pAbsColWidth[pSel[(int)SEL_POS.SP].X];
                    Rect.Y = m_pAbsRowHeight[pSel[(int)SEL_POS.SP].Y];
                    Rect.Width = m_pAbsColWidth[pSel[(int)SEL_POS.EP].X + 1] - Rect.X;
                    Rect.Height = m_pAbsRowHeight[pSel[(int)SEL_POS.EP].Y + 1] - Rect.Y;
                }
                Invalidate(Rect, true); // Window based
            }
        }

        protected void DrawSelect(Graphics gp)
        {
            int lRow, lCol;
            Point[] poSel = new Point[5];
            Point[] pSel = GetSelect();
            if (pSel != null && m_pAbsColWidth != null && m_pAbsRowHeight != null)
            {
                if (0 <= pSel[(int)SEL_POS.SP].X && pSel[(int)SEL_POS.SP].X < m_nCols &&
                    0 <= pSel[(int)SEL_POS.SP].Y && pSel[(int)SEL_POS.SP].Y < m_nRows &&
                    0 <= pSel[(int)SEL_POS.EP].X && pSel[(int)SEL_POS.EP].X < m_nCols &&
                    0 <= pSel[(int)SEL_POS.EP].Y && pSel[(int)SEL_POS.EP].Y < m_nRows)
                {
                    Pen BolderPen = new Pen(m_cSelectColor);

                    for (lRow = pSel[(int)SEL_POS.SP].Y; lRow <= pSel[(int)SEL_POS.EP].Y; lRow++)
                    {
                        for (lCol = pSel[(int)SEL_POS.SP].X; lCol <= pSel[(int)SEL_POS.EP].X; lCol++)
                        {

                            XCell pItem = m_cItem[lRow][lCol];
                                poSel[3].X = poSel[4].X = poSel[0].X = m_pAbsColWidth[lCol] + 1;
                                poSel[1].Y = poSel[4].Y = poSel[0].Y = m_pAbsRowHeight[lRow] + 1;

                                poSel[1].X = poSel[2].X = m_pAbsColWidth[lCol + 1] - 1;
                                poSel[3].Y = poSel[2].Y = m_pAbsRowHeight[lRow + 1] - 1;
                                gp.DrawLines(BolderPen, poSel);
                            //Polyline(hDC,poSel,5);
                        }
                    }
                    Array.Copy(pSel, m_pSelPos, (int)SEL_POS.MAX_SEL_POS);
                    BolderPen.Dispose();
                }
            }
        }
        public void SetSelect(int lRow, int lCol, int lRowSelectCount, int lColSelectCount)
        {
            if (txtEdit.Visible == true) UpdateEdit();

            int lCellX = lCol + lColSelectCount;
            int lCellY = lRow + lRowSelectCount;

            if (m_cItem != null && 0 <= lCellY && lCellY < m_nRows && 0 <= lCellX && lCellX < m_nCols)
            {
                int oRow = m_nRow;
                int oCol = m_nCol;
                m_nRow = lRow;
                m_nCol = lCol;
                m_nRowSelectCount = lRowSelectCount;
                m_nColSelectCount = lColSelectCount;
                if (ChangeCell != null) ChangeCell(this);
                Rectangle Rect = new Rectangle(0, 0, Width, Height);
                bool bRefreshSelect = true;
                if ((m_pAbsColWidth[lCellX]) < 0 || (m_pAbsColWidth[lCellX + 1]) > Rect.Width)
                {
                    bRefreshSelect = false;
                    m_lOriginX += (m_pAbsColWidth[oCol] - m_pAbsColWidth[lCol]);
                }
                if ((m_pAbsRowHeight[lCellY]) < 0 || (m_pAbsRowHeight[lCellY + 1]) > Rect.Height)
                {
                    bRefreshSelect = false;
                    m_lOriginY += (m_pAbsRowHeight[oRow] - m_pAbsRowHeight[lRow]);
                }

                if (bRefreshSelect == true)
                {

                    RefreshSelect();
                }
                else
                {
                    if (lCellX == 0) m_lOriginX = 0;
                    int nMinOriginX = Rect.Width - m_lRoundWidth;
                    if (m_lOriginX < nMinOriginX) m_lOriginX = nMinOriginX;
                    if (m_lOriginX > 0) m_lOriginX = 0;
                    if (lCellY == 0) m_lOriginY = 0;
                    int nMinOriginY = Rect.Height - m_lRoundHeight;
                    if (m_lOriginY < nMinOriginY) m_lOriginY = nMinOriginY;
                    if (m_lOriginY > 0) m_lOriginY = 0;
                    //else if (lCellY == (m_nRows - 1)) m_lOriginY = (Rect.Bottom - Rect.Top) - m_lRoundHeight - 1;

                    MoveGrid(m_lOriginX, m_lOriginY);
                    Refresh();
                }
            }

        }

        private void ColtrolCellSize(int x, int y)
        {
            long lRow, lCol;
            Point[] pSel = GetSelect();
            if (pSel != null && m_pAbsColWidth != null && m_pAbsRowHeight != null)
            {
                if (x != 0)
                {
                    for (lCol = pSel[(int)SEL_POS.SP].X; lCol <= pSel[(int)SEL_POS.EP].X; lCol++)
                    {
                        m_pColWidth[lCol] += x;
                        if (m_pColWidth[lCol] < 0) m_pColWidth[lCol] = 0;
                    }
                }
                if (y != 0)
                {
                    for (lRow = pSel[(int)SEL_POS.SP].Y; lRow <= pSel[(int)SEL_POS.EP].Y; lRow++)
                    {
                        m_pRowHeight[lRow] += y;
                        if (m_pRowHeight[lRow] < 0) m_pRowHeight[lRow] = 0;
                    }
                }
                NewGrid();

                Refresh();

            }

        }

        private Point GetPos(int nMX, int nMY)
        {

            if (m_pAbsColWidth != null && m_pAbsRowHeight != null)
            {
                if (0 <= nMX && nMX < Width && 0 <= nMY && nMY <= Height)
                {
                    if (nMX > m_pAbsColWidth[m_nCols]) nMX = m_pAbsColWidth[m_nCols];
                    if (nMY > m_pAbsRowHeight[m_nRows]) nMY = m_pAbsRowHeight[m_nRows];
                    int lCol, lRow;

                    for (lCol = 0; lCol < m_nCols; lCol++)
                    {
                        if (m_pAbsColWidth[lCol] <= nMX && nMX < m_pAbsColWidth[lCol + 1]) break;
                    }
                    for (lRow = 0; lRow < m_nRows; lRow++)
                    {
                        if (m_pAbsRowHeight[lRow] <= nMY && nMY < m_pAbsRowHeight[lRow + 1]) break;
                    }

                    return new Point(lCol, lRow);
                }

            }

            return Point.Empty;
        }

        private bool GetPosGroup(int nMx, int nMy)
        {

            if (m_pAbsColWidth != null && m_pAbsRowHeight != null)
            {
                if (m_pAbsColWidth[0] < nMx && nMx < (m_pAbsColWidth[0] + 12)) return true;
            }

            return false;
        }


        public override bool PreProcessMessage(ref Message msg)
        {
            if (msg.HWnd == Handle)
            {
                if (msg.Msg == WM_KEYDOWN)
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
            if (Enabled && ModifierKeys == Keys.Control)
            {
                switch (keyConde)
                {
                    case Keys.C: Copy(); break;
                    case Keys.V: Paste(); break;
                    case Keys.X: Cut(); break;
                    case Keys.Z: Recovery(); break;
                    case Keys.Left: ColtrolCellSize(-1, 0); break;
                    case Keys.Up: ColtrolCellSize(0, -1); break;
                    case Keys.Right: ColtrolCellSize(1, 0); break;
                    case Keys.Down: ColtrolCellSize(0, 1); break;

                }
            }
            else if (Enabled && ModifierKeys == Keys.Shift)
            {
                switch (keyConde)
                {
                    case Keys.Home: SetSelect(m_nRow, m_nCol, m_nRowSelectCount, -m_nCol); break;
                    case Keys.Left: SetSelect(m_nRow, m_nCol, m_nRowSelectCount, m_nColSelectCount - 1); return true;
                    case Keys.PageUp: SetSelect(m_nRow, m_nCol, -m_nRow, m_nColSelectCount); break;
                    case Keys.Up: SetSelect(m_nRow, m_nCol, m_nRowSelectCount - 1, m_nColSelectCount); return true;
                    case Keys.End: SetSelect(m_nRow, m_nCol, m_nRowSelectCount, m_nCols - m_nCol - 1); break;
                    case Keys.Right: SetSelect(m_nRow, m_nCol, m_nRowSelectCount, m_nColSelectCount + 1); return true;
                    case Keys.PageDown: SetSelect(m_nRow, m_nCol, m_nRows - m_nRow - 1, m_nColSelectCount); break;
                    case Keys.Down: SetSelect(m_nRow, m_nCol, m_nRowSelectCount + 1, m_nColSelectCount); return true;
                    case Keys.C: CopyCell(); Refresh(); break;
                    case Keys.V: PasteCell(); Refresh(); break;

                }

            }
            else
            {
                switch (keyConde)
                {
                    case Keys.Home: SetSelect(m_nRow, 0, 0, 0); break;
                    case Keys.Left: SetSelect(m_nRow, m_nCol - 1, 0, 0); break;
                    case Keys.PageUp:
                        {
                            Point sp = GetPos(0, 0);
                            Point ep = GetPos(0, Height);
                            int nPageUp = sp.Y - ep.Y + sp.Y;
                            if (nPageUp < 0) nPageUp = 0;
                            SetSelect(nPageUp, m_nCol, 0, 0);
                        } break;
                    case Keys.Up: SetSelect(m_nRow - 1, m_nCol, 0, 0); break;
                    case Keys.End: SetSelect(m_nRow, m_nCols - 1, 0, 0); break;
                    case Keys.Right: SetSelect(m_nRow, m_nCol + 1, 0, 0); break;
                    case Keys.PageDown:
                        {
                            Point sp = GetPos(0, 0);
                            Point ep = GetPos(0, Height);
                            int nPageDn = ep.Y + ep.Y - sp.Y;
                            if (nPageDn >= m_nRows) nPageDn = m_nRows - 1;
                            SetSelect(nPageDn, m_nCol, 0, 0);
                        } break;
                    case Keys.Down: SetSelect(m_nRow + 1, m_nCol, 0, 0); break;
                    case Keys.Delete: Delete(); break;
                    case Keys.Back: Delete(); SetSelect(m_nRow, m_nCol - 1, 0, 0); break;
                    case Keys.Enter: ShowEdit(new Point(m_nCol, m_nRow)); break;
                    default: return false;
                }
                return true;
            }
            return false;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            int nMX = e.X;
            int nMY = e.Y;
            Point pGetPos = GetPos(nMX, nMY);
            if (m_pAbsColWidth != null && m_pAbsRowHeight != null)
            {
                ShowEdit(pGetPos);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Focused == false) Focus();
            int nMX = e.X;
            int nMY = e.Y;

            if ((e.Button & MouseButtons.Middle) != 0 || (ModifierKeys == Keys.Control && (e.Button & MouseButtons.Left) != 0))
            {
                m_pFix.X = nMX - m_lOriginX;
                m_pFix.Y = nMY - m_lOriginY;
            }
            else if ((e.Button & MouseButtons.Left) != 0)
            {

                if (txtEdit.Visible == true) UpdateEdit();
                //if(m_dlgMenu.m_hWnd)m_dlgMenu.ShowWindow(SW_HIDE);

                //int fwKeys = wParam;        // key flags 
                Point pSel = GetPos(nMX, nMY);
                //if (pSel != Point.Empty)
                {
                    m_nRowSelectCount = 0;
                    m_nColSelectCount = 0;
                    if (0 <= pSel.X && pSel.X < m_nCols && 0 <= pSel.Y && pSel.Y < m_nRows)
                    {
                        m_nRow = pSel.Y;
                        m_nCol = pSel.X;
                        if (ChangeCell != null) ChangeCell(this);


                        if (m_nCol == 0)
                        {
                            int nGroupCount = m_pGroup[m_nRow];
                            if (nGroupCount > 0)
                            {
                                if (GetPosGroup(nMX, nMY))
                                {
                                    if (m_pGroupCheck[m_nRow] == 0)
                                    {
                                        m_pGroupCheck[m_nRow] = 1;
                                        for (int n = 1; n <= nGroupCount; n++)
                                        {
                                            m_pRowHeight[m_nRow + n] = 0;
                                            m_pGroupCheck[m_nRow + n] = 0;
                                        }
                                    }
                                    else
                                    {
                                        m_pGroupCheck[m_nRow] = 0;
                                        for (int n = 1; n <= nGroupCount; n++)
                                        {
                                            m_pRowHeight[m_nRow + n] = m_pRowHeight[m_nRow];
                                            m_pGroupCheck[m_nRow + n] = 1;
                                        }


                                    }
                                    MoveGrid(m_lOriginX, m_lOriginY);
                                    Refresh();
                                }
                            }
                        }

                        RefreshSelect();
                    }
                }
                //else
                //{
                //    m_nRow = 0;
                //    m_nRow = 0;
                //}

            }

        }
        protected override void OnMouseUp(MouseEventArgs e)
        {


            if ((e.Button & MouseButtons.Right) != 0)
            {

                if (txtEdit.Visible == true) UpdateEdit();

                if (m_bPopupMenu && ModifierKeys == Keys.Control)
                {
                    /*
                    GridControl_Prop proGridControl = new GridControl_Prop();
                    proGridControl.XSheet = this;
                    if (proGridControl.ShowDialog() == DialogResult.OK)
                    {
                        Refresh();
                    }
                     */

                }
                else
                {
                    if (0 == m_nRowSelectCount && 0 == m_nColSelectCount)
                    {

                        int nMX = e.X;
                        int nMY = e.Y;

                        Point pSel = GetPos(nMX, nMY);
                        //if (pSel != Point.Empty)
                        {
                            m_nRowSelectCount = 0;
                            m_nColSelectCount = 0;

                            if (0 <= pSel.X && pSel.X < m_nCols && 0 <= pSel.Y && pSel.Y < m_nRows)
                            {
                                m_nRow = pSel.Y;
                                m_nCol = pSel.X;
                                if (ChangeCell != null) ChangeCell(this);
                                RefreshSelect();
                            }
                        }
                    }
                    Backup(m_nRow, m_nCol, m_nRowSelectCount, m_nColSelectCount);
                    /*
                    GridItem_Prop proCellItem = new GridItem_Prop();
                    proCellItem.XCell = m_cItem;
                    proCellItem.SetDefault(this.ForeColor, this.BackColor, m_TextAlign, this.Font);
                    proCellItem.SetGroup(m_pGroup);
                    proCellItem.SetRowHeight(m_pRowHeight);
                    proCellItem.SetSelect(m_nRow, m_nCol, m_nRowSelectCount, m_nColSelectCount);
                    if (m_bPopupMenu && proCellItem.XCell != null)
                    {
                        if (proCellItem.ShowDialog() == DialogResult.OK)
                        {
                            Refresh();
                        }
                    }
                    */
                }
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {

            int nMX = e.X;
            int nMY = e.Y;

            Rectangle Rect = new Rectangle(0, 0, Width, Height);

            if ((e.Button & MouseButtons.Middle) != 0 || (ModifierKeys == Keys.Control && (e.Button & MouseButtons.Left) != 0))
            {

                m_lOriginX = nMX - m_pFix.X;
                m_lOriginY = nMY - m_pFix.Y;

                int lMinX = (Rect.Right - Rect.Left) - m_lRoundWidth - 1;
                if (m_lOriginX < lMinX) m_lOriginX = lMinX;
                if (m_lOriginX > 0) m_lOriginX = 0;

                int lMinY = (Rect.Bottom - Rect.Top) - m_lRoundHeight - 1;
                if (m_lOriginY < lMinY) m_lOriginY = lMinY;
                if (m_lOriginY > 0) m_lOriginY = 0;

                MoveGrid(m_lOriginX, m_lOriginY);
                Refresh();

            }
            else if ((e.Button & MouseButtons.Left) != 0)
            {
                Point pSel = GetPos(nMX, nMY);
                //if (pSel != Point.Empty)
                {
                    //Point pSel = pGetPos[0];
                    //if ((m_pAbsColWidth[pSel.Y] + m_lOriginX - nMX) < (m_pAbsColWidth[pSel.Y]/2)) pSel.Y--;
                    //if ((nMY - Rect.Top) < 3) pSel.Y--;
                    //if ((Rect.Right - nMX) < 3) pSel.Y++;
                    //if ((Rect.Bottom - nMY) < 3) pSel.Y++;

                    int lColSelectCount = pSel.X - m_nCol;
                    int lRowSelectCount = pSel.Y - m_nRow;
                    if (m_nColSelectCount != lColSelectCount || m_nRowSelectCount != lRowSelectCount)
                    {
                        SetSelect(m_nRow, m_nCol, lRowSelectCount, lColSelectCount);
                    }
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {

            Rectangle Rect = new Rectangle(0, 0, Width, Height);

            if (txtEdit.Visible == true) UpdateEdit();
            if (ModifierKeys == Keys.Shift)
            {
                m_lOriginX += e.Delta / 10;

                int lMinX = (Rect.Right - Rect.Left) - m_lRoundWidth - 1;
                if (m_lOriginX < lMinX) m_lOriginX = lMinX;
                if (m_lOriginX > 0) m_lOriginX = 0;
            }
            else
            {
                m_lOriginY += e.Delta / 10;

                int lMinY = (Rect.Bottom - Rect.Top) - m_lRoundHeight - 1;
                if (m_lOriginY < lMinY) m_lOriginY = lMinY;
                if (m_lOriginY > 0) m_lOriginY = 0;
            }
            MoveGrid(m_lOriginX, m_lOriginY);
            Refresh();
        }


        private void CopyCell()
        {
            Point[] pSel = GetSelect(m_nRow, m_nCol, m_nRowSelectCount, m_nColSelectCount);
            if (pSel != null)
            {
                m_xClip = new CellUnit(pSel[(int)SEL_POS.SP].Y, pSel[(int)SEL_POS.SP].X, pSel[(int)SEL_POS.EP].Y, pSel[(int)SEL_POS.EP].X);
                m_xClip.Copy(m_cItem);
            }

        }
        private void PasteCell()
        {
            if (Enabled && m_xClip != null && m_cItem != null)
            {
                m_xClip.Paste(m_cItem, m_nRow, m_nCol, m_nRows, m_nCols);
            }
        }

        private void Copy()
        {

            Point[] pSel = GetSelect();
            if (pSel != null)
            {
                string sText = "", sTab = "\t", sEnd = "\r\n";

                for (long nRow = pSel[(int)SEL_POS.SP].Y; nRow <= pSel[(int)SEL_POS.EP].Y; nRow++)
                {
                    for (long nCol = pSel[(int)SEL_POS.SP].X; nCol <= pSel[(int)SEL_POS.EP].X; nCol++)
                    {
                        sText += m_cItem[nRow][nCol].Text;
                        if (nCol < pSel[(int)SEL_POS.EP].X) sText += sTab;
                    }
                    sText += sEnd;
                }
                Clipboard.SetDataObject(sText);
            }

        }

        private void Paste()
        {

            if (Enabled)
            {

                string sClipText;
                IDataObject iData = Clipboard.GetDataObject();
                if (true == iData.GetDataPresent(DataFormats.Text))
                {
                    sClipText = (string)(iData.GetData(DataFormats.Text));
                    sClipText = sClipText.Replace("\n", "");
                    string[] sRowText = sClipText.Split('\r');
                    int nRows = sRowText.Length - 1;
                    if (nRows > 0)
                    {
                        if ((m_nRow + nRows) >= m_nRows) nRows = m_nRows - m_nRow;

                        string[] sColText = sRowText[0].Split('\t');
                        int nCols = sColText.Length;
                        if (nCols > 0)
                        {
                            if ((m_nCol + nCols) >= m_nCols) nCols = m_nCols - m_nCol;

                            Backup(m_nRow, m_nCol, nRows - 1, nCols - 1);
                            for (int nRow = 0; nRow < nRows; nRow++)
                            {
                                int nRowPos = m_nRow + nRow;
                                string[] sText = sRowText[nRow].Split('\t');
                                for (int nCol = 0; nCol < nCols; nCol++)
                                {
                                    SetMatrixText(nRowPos, m_nCol + nCol, sText[nCol]);
                                }
                            }
                        }
                    }

                    Refresh();
                }

            }

        }

        private void Cut()
        {
            Copy();
            Delete();

        }
        private void Delete()
        {
            if (Enabled == true)
            {
                Backup(m_nRow, m_nCol, m_nRowSelectCount, m_nColSelectCount);

                Point[] pSel = GetSelect();

                for (long nRow = pSel[(int)SEL_POS.SP].Y; nRow <= pSel[(int)SEL_POS.EP].Y; nRow++)
                {
                    for (long nCol = pSel[(int)SEL_POS.SP].X; nCol <= pSel[(int)SEL_POS.EP].X; nCol++)
                    {

                        m_cItem[nRow][nCol].Text = "";
                        //			        if(m_cItem[nRow,nCol].Text="")
                        //			        {
                        //				        SysFreeString(m_cItem[nRow,nCol].Text);
                        //				        m_cItem[nRow,nCol].Text=NULL;
                        //			        }					
                    }
                }
                RefreshSelect();
            }
        }


        public void Backup(int lRow, int lCol, int lRowSelectCount, int lColSelectCount)
        {
            if (Enabled == true && m_cItem != null && m_nCols > 0 && m_nRows > 0)
            {
                Point[] pSel = GetSelect(lRow, lCol, lRowSelectCount, lColSelectCount);
                if (pSel != null)
                {
                    CellUnit cUnit = new CellUnit(pSel[(int)SEL_POS.SP].Y, pSel[(int)SEL_POS.SP].X, pSel[(int)SEL_POS.EP].Y, pSel[(int)SEL_POS.EP].X);
                    cUnit.Copy(m_cItem);
                    m_xBackup.Add(cUnit);
                }
            }

        }

        private void Recovery()
        {
            if (m_xBackup.Count > 0 && Enabled == true && m_cItem != null && m_nCols > 0 && m_nRows > 0)
            {
                CellUnit cUnit = (CellUnit)m_xBackup.Last();
                if (cUnit != null)
                {
                    cUnit.Paste(m_cItem);
                    m_xBackup.Del();
                }
                Refresh();
            }
        }

        private void txtEdit_KeyUp(object sender, KeyEventArgs e)
        {

            switch (e.KeyData)
            {
                case Keys.Home: UpdateEdit(); SetSelect(m_nRow, 0, 0, 0); break;
                case Keys.Left:
                    {
                        if (m_bTextSelect == true)
                        {
                            m_bTextSelect = false;
                            UpdateEdit();
                            SetSelect(m_nRow, m_nCol - 1, 0, 0);
                        }
                        else
                        {
                            return;
                        }
                    } break;
                case Keys.Prior: UpdateEdit(); SetSelect(0, m_nCol, 0, 0); break;
                case Keys.Up: UpdateEdit(); SetSelect(m_nRow - 1, m_nCol, 0, 0); break;
                case Keys.End: UpdateEdit(); SetSelect(m_nRow, m_nCols - 1, 0, 0); break;
                case Keys.Right:
                    {
                        if (m_bTextSelect == true)
                        {
                            m_bTextSelect = false;
                            UpdateEdit();
                            SetSelect(m_nRow, m_nCol + 1, 0, 0);
                        }
                        else
                        {
                            return;
                        }

                    } break;
                case Keys.Next: UpdateEdit(); SetSelect(m_nRows - 1, m_nCol, 0, 0); break;
                case Keys.Down: UpdateEdit(); SetSelect(m_nRow + 1, m_nCol, 0, 0); break;
                case Keys.Enter: break;
                case Keys.Escape: txtEdit.Visible = false; Focus(); return;
                default: return;
            }
            ShowEdit(new Point(m_nCol, m_nRow));

        }

        private void txtEdit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            switch (e.KeyData)
            {
                case Keys.Enter:
                    {
                        UpdateEdit();
                        SetSelect(m_nRow + 1, m_nCol, 0, 0);
                        ShowEdit(new Point(m_nCol, m_nRow));

                    } break;
                case Keys.Left:
                    {
                        if (txtEdit.SelectionLength == 0 && txtEdit.TextLength > 0)
                            return;
                        m_bTextSelect = true;
                    } break;
                case Keys.Right:
                    {
                        if (txtEdit.SelectionLength == 0 && txtEdit.TextLength > 0)
                            return;
                        m_bTextSelect = true;

                    } break;
                default: return;
            }

        }

        private void UpdateEdit()
        {
            if (txtEdit.Visible == true)
            {
                Backup(m_pEditPos.Y, m_pEditPos.X, 0, 0);
                string sText = GetMatrixText(m_pEditPos.Y, m_pEditPos.X);
                txtEdit.Visible = false;
                if (sText != txtEdit.Text)
                {
                    sText = txtEdit.Text;
                    sText = sText.Replace("\n", "");
                    if (sText != null && sText != "")
                    {
                        sText = sText.Replace("\r", "");
                        if (sText != null && sText != "")
                        {
                            SetMatrixText(m_pEditPos.Y, m_pEditPos.X, sText);
                            Focus();
                            if (ChangeText != null) ChangeText(this, m_pEditPos.Y, m_pEditPos.X);
                        }
                    }

                }
            }
        }

        private void ShowEdit(Point pEditPos)
        {
            if (Enabled && 0 <= pEditPos.X && pEditPos.X < m_nCols && 0 <= pEditPos.Y && pEditPos.Y < m_nRows)
            {
                string sText = GetMatrixText(pEditPos.Y, pEditPos.X);
                txtEdit.Text = sText;

                txtEdit.Left = m_pAbsColWidth[pEditPos.X] + 1;
                txtEdit.Top = m_pAbsRowHeight[pEditPos.Y] + 1;
                txtEdit.Width = m_pColWidth[pEditPos.X] - 1;
                txtEdit.Height = m_pRowHeight[pEditPos.Y] - 1;

                txtEdit.Font = this.Font;
                txtEdit.Visible = true;
                txtEdit.Focus();
                txtEdit.SelectAll();
                m_pEditPos = pEditPos;
            }
        }

        public bool Load(string path)
        {

            if (path != null && path != "") m_sPath = path;
            if (m_sPath != null && m_sPath != "")
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GridItem));
                serializer.UnknownNode += new
                XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnknownAttribute += new
                XmlAttributeEventHandler(serializer_UnknownAttribute);

                // A FileStream is needed to read the XML document.
                try
                {
                    FileStream fs = new FileStream(path, FileMode.Open);
                    if (fs != null)
                    {
                        // Declares an object variable of the type to be deserialized.
                        GridItem gItem = (GridItem)serializer.Deserialize(fs);
                        fs.Close();

                        NewMatrix(gItem.m_nRows, gItem.m_nCols);
                        m_pColWidth = gItem.m_pColWidth;
                        m_pRowHeight = gItem.m_pRowHeight;
                        m_pGroup = gItem.m_pGroup;
                        m_pGroupCheck = gItem.m_pGroupCheck;
                        gItem.OutFont(ref m_cItem);
                        gItem.GetItem(ref m_cItem);
                        MoveGrid(0, 0);
                        Refresh();
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


        public bool Save(string path)
        {
            if (path != null && path != "") m_sPath = path;
            if (m_sPath != null && m_sPath != "")
            {

                XmlSerializer serializer = new XmlSerializer(typeof(GridItem));
                TextWriter writer = new StreamWriter(path);
                GridItem gItem = new GridItem();
                gItem.m_pColWidth = m_pColWidth;
                gItem.m_pRowHeight = m_pRowHeight;
                gItem.m_pGroup = m_pGroup;
                gItem.m_pGroupCheck = m_pGroupCheck;
                gItem.SetItem(m_nRows, m_nCols, m_cItem);
                gItem.InFont(m_cItem);
                serializer.Serialize(writer, gItem);
                writer.Close();
                return true;
            }
            return false;
        }
        public void AddLine(int nAddLine)
        {
            string sTempPath = "c:/grid_temp.tmp";
            Save(sTempPath);

            XmlSerializer serializer = new XmlSerializer(typeof(GridItem));
            serializer.UnknownNode += new
            XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new
            XmlAttributeEventHandler(serializer_UnknownAttribute);

            // A FileStream is needed to read the XML document.
            try
            {
                FileStream fs = new FileStream(sTempPath, FileMode.Open);
                if (fs != null)
                {
                    // Declares an object variable of the type to be deserialized.
                    GridItem gItem = (GridItem)serializer.Deserialize(fs);
                    fs.Close();
                    int nRows = gItem.m_nRows + nAddLine;
                    NewMatrix(nRows, gItem.m_nCols);
                    m_pColWidth = gItem.m_pColWidth;
                    //m_pRowHeight = gItem.m_pRowHeight;

                    for (int nRow = 0; nRow < gItem.m_nRows; nRow++)
                    {
                        for (int nCol = 0; nCol < gItem.m_nCols; nCol++)
                        {
                            // m_cItem[nRow][nCol] = gItem.m_cItem[nRow][nCol];
                        }
                    }
                    MoveGrid(0, 0);
                    Refresh();
                }
            }
            catch (Exception pe)
            {
                string sMsg = pe.ToString();
            }

        }
        public void DelLine(int nDelLine)
        {
            string sTempPath = "c:/grid_temp.tmp";
            Save(sTempPath);

            XmlSerializer serializer = new XmlSerializer(typeof(GridItem));
            serializer.UnknownNode += new
            XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new
            XmlAttributeEventHandler(serializer_UnknownAttribute);

            // A FileStream is needed to read the XML document.
            try
            {
                FileStream fs = new FileStream(sTempPath, FileMode.Open);
                if (fs != null)
                {
                    // Declares an object variable of the type to be deserialized.
                    GridItem gItem = (GridItem)serializer.Deserialize(fs);
                    fs.Close();
                    int nRows = gItem.m_nRows - nDelLine;
                    NewMatrix(nRows, gItem.m_nCols);
                    m_pColWidth = gItem.m_pColWidth;
                    //m_pRowHeight = gItem.m_pRowHeight;

                    for (int nRow = 0; nRow < nRows; nRow++)
                    {
                        for (int nCol = 0; nCol < gItem.m_nCols; nCol++)
                        {
                            //m_cItem[nRow][nCol] = gItem.m_cItem[nRow][nCol];
                        }
                    }
                    MoveGrid(0, 0);
                    Refresh();
                }
            }
            catch (Exception pe)
            {
                string sMsg = pe.ToString();
            }
        }


        protected void serializer_UnknownNode
        (object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        protected void serializer_UnknownAttribute
        (object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }

        private void txtEdit_Enter(object sender, EventArgs e)
        {
            int a = 0;
            int b = a;
        }







    }

    public class CellUnit : X
    {
        protected XCell[][] m_cItem;
        protected int m_lMinRow;
        protected int m_lMinCol;
        protected int m_lMaxRow;
        protected int m_lMaxCol;

        protected int m_nRows;
        protected int m_nCols;

        public CellUnit(int lMinRow, int lMinCol, int lMaxRow, int lMaxCol)
        {
            m_lMinRow = lMinRow;
            m_lMinCol = lMinCol;
            m_lMaxRow = lMaxRow;
            m_lMaxCol = lMaxCol;
            m_nRows = lMaxRow - lMinRow + 1;
            m_nCols = lMaxCol - lMinCol + 1;
            m_cItem = new XCell[m_nRows][];
            for (int nRow = 0; nRow < m_nRows; nRow++)
            {
                m_cItem[nRow] = new XCell[m_nCols];
                for (int nCol = 0; nCol < m_nCols; nCol++)
                {
                    m_cItem[nRow][nCol] = new XCell();
                }

            }
        }
        public void Copy(XCell[][] cItem)
        {
            for (int nRow = 0; nRow < m_nRows; nRow++)
            {
                for (int nCol = 0; nCol < m_nCols; nCol++)
                {
                    m_cItem[nRow][nCol].Copy(cItem[m_lMinRow + nRow][m_lMinCol + nCol]);
                }
            }
        }
        public void Paste(XCell[][] cItem)
        {
            for (int nRow = 0; nRow < m_nRows; nRow++)
            {
                for (int nCol = 0; nCol < m_nCols; nCol++)
                {
                    cItem[m_lMinRow + nRow][m_lMinCol + nCol].Copy(m_cItem[nRow][nCol]);
                }
            }
        }
        public void Paste(XCell[][] cItem, int lMinRow, int lMinCol, int lMaxRow, int lMaxCol)
        {
            for (int nRow = 0; nRow < m_nRows; nRow++)
            {
                int lRowPos = lMinRow + nRow;
                for (int nCol = 0; nCol < m_nCols; nCol++)
                {
                    int lColPos = lMinCol + nCol;
                    if (lRowPos < lMaxRow && lColPos < lMaxCol)
                    {
                        cItem[lRowPos][lColPos].Copy(m_cItem[nRow][nCol]);
                    }
                }
            }
        }
    }




    [XmlRootAttribute("GridItem", Namespace = "http://www.fatkorea.com", IsNullable = false)]
    public class GridItem
    {
        public int m_nFonts;
        public FontStyle[] m_FontStyle;
        public string[] m_FontName;
        public float[] m_FontSize;
        public int[][] m_nFont;

        public int m_nCols;
        public int m_nRows;
        public int[] m_pColWidth;
        public int[] m_pRowHeight;
        public int[] m_pGroup;
        public int[] m_pGroupCheck;


        public int[][] m_Param;
        public int[][] m_ForeColor;
        public int[][] m_BackColor;
        public StringFormat[][] m_Align;
        public string[][] m_Text;

        public GridItem()
        {
            m_nFonts = 0;
            m_FontStyle = null;
            m_nFont = null;
            m_FontName = null;
            m_FontSize = null;

            m_nRows = 0;
            m_nCols = 0;


            m_Param = null;

            m_ForeColor = null;
            m_BackColor = null;
            m_Align = null;
            m_Text = null;

        }
        public void SetItem(int nRows, int nCols, XCell[][] cItem)
        {
            m_nRows = nRows;
            m_nCols = nCols;

            m_Param = new int[nRows][];
            m_ForeColor = new int[nRows][];
            m_BackColor = new int[nRows][];
            m_Align = new StringFormat[nRows][];
            m_Text = new string[nRows][];
            for (int nRow = 0; nRow < nRows; nRow++)
            {
                m_Param[nRow] = new int[nCols];
                m_ForeColor[nRow] = new int[nCols];
                m_BackColor[nRow] = new int[nCols];
                m_Align[nRow] = new StringFormat[nCols];
                m_Text[nRow] = new string[nCols];
                for (int nCol = 0; nCol < nCols; nCol++)
                {
                    XCell cCell = cItem[nRow][nCol];
                    m_Param[nRow][nCol] = cCell.Param;
                    m_ForeColor[nRow][nCol] = cCell.ForeColor.ToArgb();
                    m_BackColor[nRow][nCol] = cCell.BackColor.ToArgb();
                    m_Align[nRow][nCol] = cCell.Align;
                    m_Text[nRow][nCol] = cCell.Text;

                }

            }



        }
        public void GetItem(ref XCell[][] cItem)
        {
            if (m_nCols > 0 && m_nRows > 0)
            {
                if (m_Param != null && m_ForeColor != null && m_BackColor != null && m_Align != null  && m_Text != null)
                {
                    for (int nRow = 0; nRow < m_nRows; nRow++)
                    {
                        for (int nCol = 0; nCol < m_nCols; nCol++)
                        {
                            XCell cCell = cItem[nRow][nCol];
                            cCell.Param = m_Param[nRow][nCol];
                            cCell.ForeColor = Color.FromArgb(m_ForeColor[nRow][nCol]);
                            cCell.BackColor = Color.FromArgb(m_BackColor[nRow][nCol]);
                            cCell.Align = m_Align[nRow][nCol];
                            cCell.Text = m_Text[nRow][nCol];

                        }

                    }
                }
            }

        }

        public void InFont(XCell[][] cItem)
        {
            if (cItem != null && m_nRows > 0 && m_nCols > 0)
            {
                m_nFonts = 0;
                m_nFont = new int[m_nRows][];

                Font[] AddFont = new Font[256];
                string[] addFontName = new string[256];
                float[] addFontSize = new float[256];
                FontStyle[] addFontStyle = new FontStyle[256];
                for (int nRow = 0; nRow < m_nRows; nRow++)
                {
                    m_nFont[nRow] = new int[m_nCols];

                    for (int nCol = 0; nCol < m_nCols; nCol++)
                    {
                        Font Font = cItem[nRow][nCol].Font;
                        if (Font != null)
                        {
                            int nPos = 0;
                            for (int n = 1; n <= m_nFonts; n++)
                            {
                                if (AddFont[n] != null && Font == AddFont[n])
                                {
                                    nPos = n;
                                    m_nFont[nRow][nCol] = nPos;
                                    break;
                                }
                            }
                            if (nPos == 0)
                            {
                                m_nFonts++;
                                AddFont[m_nFonts] = Font;
                                addFontName[m_nFonts] = Font.FontFamily.Name;
                                addFontSize[m_nFonts] = Font.Size;
                                addFontStyle[m_nFonts] = Font.Style;
                                m_nFont[nRow][nCol] = m_nFonts;

                            }
                        }
                    }
                }
                if (m_nFonts > 0)
                {
                    int nFontLength = m_nFonts + 1;
                    m_FontName = new string[nFontLength];
                    m_FontSize = new float[nFontLength];
                    m_FontStyle = new FontStyle[nFontLength];
                    for (int n = 1; n <= m_nFonts; n++)
                    {
                        m_FontName[n] = addFontName[n];
                        m_FontSize[n] = addFontSize[n];
                        m_FontStyle[n] = addFontStyle[n];
                    }
                }
            }
        }
        public void OutFont(ref XCell[][] cItem)
        {
            if (cItem != null && m_nFonts > 0 && m_FontName != null && m_FontSize != null && m_nFont != null)
            {
                for (int nRow = 0; nRow < m_nRows; nRow++)
                {
                    for (int nCol = 0; nCol < m_nCols; nCol++)
                    {
                        int nFont = m_nFont[nRow][nCol];
                        if (nFont > 0 && m_FontName[nFont] != null && m_FontName[nFont] != "")
                        {
                            cItem[nRow][nCol].Font = new Font(m_FontName[nFont], m_FontSize[nFont], m_FontStyle[nFont]);
                        }
                    }
                }
            }
        }
    }




}
