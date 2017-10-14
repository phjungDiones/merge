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

    public delegate void ChangeCell(Control sender);
    public delegate void ChangeText(Control sender, int nRow, int nCol);

    public partial class XSheet : Control
    {
        public event ChangeCell ChangeCell;
        public event ChangeText ChangeText;
        private static int WM_KEYDOWN = 0x0100;

        private Point m_pFix;
        private int m_nOriginX;
        private int m_nOriginY;
        private float m_fZoom;

        private XGrid m_Grid = new XGrid();
        
        private int m_nCols;
        private int m_nRows;

        private int m_nRow;
        private int m_nCol;
        private int m_nRowSelectCount;
        private int m_nColSelectCount;
        private int m_nEditPosCol;
        private int m_nEditPosRow;
        private int m_lDefaultWidth;
        private int m_lDefaultHeight;

        private Color m_cBorderColor = Color.Black;
        private Color m_cSelectColor = Color.Blue;

        private Bitmap m_BackDC;
        private Bitmap m_MapDC;


        private XArray m_xBackup = new XArray();

        private StringFormat m_TextAlign = new StringFormat();

        private bool m_bEditEnable;

        public XSheet()
        {
            m_bEditEnable = true;
            m_TextAlign.Alignment = StringAlignment.Center;
            m_TextAlign.LineAlignment = StringAlignment.Center;

            m_nOriginX = 0;
            m_nOriginY = 0;
            m_nRow = 0;
            m_nCol = 0;
            m_nRowSelectCount = 0;
            m_nColSelectCount = 0;
            m_nEditPosCol = 0;
            m_nEditPosRow = 0;
            m_lDefaultWidth = 80;
            m_lDefaultHeight = 22;
            m_nRows = 2;
            m_nCols = 2;
            InitializeComponent();


            txtEdit = new TextBox();
            this.txtEdit.Location = new System.Drawing.Point(0, 0);
            this.txtEdit.Multiline = true;
            this.txtEdit.Name = "txtEdit";
            this.txtEdit.Size = new System.Drawing.Size(82, 21);
            this.txtEdit.TabIndex = 1;
            this.txtEdit.Visible = false;   
            this.txtEdit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEdit_KeyUp);
            this.txtEdit.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtEdit_PreviewKeyDown);



            this.Controls.Add(this.txtEdit);

            m_fZoom = 1;
            m_Grid.NewMatrix(m_nRows, m_nCols, m_lDefaultWidth, m_lDefaultHeight);
            //m_Grid.NewMatrix(200, 100, 60, 20);

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

        public int [] GridHeight
        {
            get
            {
                return m_Grid.m_pHeight;
            }
        }
        public int[] GridWidth
        {
            get
            {
                return m_Grid.m_pWidth;
            }
        }

        public bool EditEnable
        {
            get
            {
                return m_bEditEnable;
            }
            set
            {
                m_bEditEnable = value;
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
                m_Grid.NewMatrix(m_nRows, m_nCols, m_lDefaultWidth, m_lDefaultHeight);
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
                m_Grid.NewMatrix(m_nRows, m_nCols, m_lDefaultWidth, m_lDefaultHeight);
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

        public void MoveGrid(int nOriginX, int nOriginY)
        {
            m_Grid.MoveGrid(nOriginX, nOriginY, Width, Height, m_fZoom);
        }
/*
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
        */

        public XCell[,] Cell
        {
            get
            {
                return m_Grid.m_pCell;
            }
        }

        public XCell GetCell(int lRow, int lCol)
        {
            if (m_Grid != null && 0 <= lRow && lRow < m_nRows && 0 <= lCol && lCol < m_nCols)
            {
                return m_Grid.GetCell(lRow, lCol);
            }
            return null;
        }



        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            m_MapDC = new Bitmap(Width, Height);
            m_Grid.MoveGrid(0, 0, Width, Height, m_fZoom);
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Graphics gp = pevent.Graphics;
            if (m_MapDC != null && m_BackDC == null)
            {
                m_BackDC = new Bitmap(Width, Height);
                Graphics mGp = Graphics.FromImage(m_BackDC);

                SolidBrush BrushBack = new SolidBrush(BackColor);
                mGp.FillRectangle(BrushBack, 0, 0, Width, Height);
                BrushBack.Dispose();
                gp.DrawImage(m_MapDC, 0, 0);
                mGp.Dispose();
            }

        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics gp = pe.Graphics;
            if (m_MapDC!=null)
            {
                
                Graphics mGp = Graphics.FromImage(m_MapDC);
                mGp.DrawImage(m_BackDC, 0, 0);
                //m_Grid.DrawGrid(mGp, ForeColor);
                m_Grid.DrawSelect(mGp, Color.FromArgb(50, SelectColor));
                m_Grid.DrawCell(mGp, m_fZoom, m_TextAlign, ForeColor, this.Font);
                m_Grid.DrawGrid(mGp, m_cBorderColor);
                gp.DrawImage(m_MapDC, 0, 0);
                mGp.Dispose();
            }

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            //Refresh();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Focused == false) Focus();
            int nMX = e.X;
            int nMY = e.Y;

            if ((e.Button & MouseButtons.Middle) != 0 || (ModifierKeys == Keys.Control && (e.Button & MouseButtons.Left) != 0))
            {
                m_pFix.X = nMX - m_nOriginX;
                m_pFix.Y = nMY - m_nOriginY;
            }
            else if ((e.Button & MouseButtons.Left) != 0)
            {

                if (txtEdit.Visible == true) UpdateEdit();
                //if(m_dlgMenu.m_hWnd)m_dlgMenu.ShowWindow(SW_HIDE);

                //int fwKeys = wParam;        // key flags 
                int nSelRow=0;
                int nSelCol=0;
                if (m_Grid.GetPos(ref nSelRow, ref nSelCol, nMX, nMY))
                {
                    if (0 <= nSelCol && nSelCol < m_Grid.m_nCols && 0 <= nSelRow && nSelRow < m_Grid.m_nRows)
                    {
                        m_nRow = nSelRow;
                        m_nCol = nSelCol;
                        m_nRowSelectCount = 0;
                        m_nColSelectCount = 0;
                        m_Grid.SetSelect(m_nRow, m_nCol, m_nRowSelectCount, m_nColSelectCount);
                        if (ChangeCell != null) ChangeCell(this);
                        Refresh(false);
                    }
                }

            }

        }
        public void Refresh(bool bBack)
        {
            if (bBack)
            {
                if (m_BackDC != null)
                {
                    m_BackDC.Dispose();
                    m_BackDC = null;
                }
            }
            Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {

            int nMX = e.X;
            int nMY = e.Y;

    
            if ((e.Button & MouseButtons.Middle) != 0 || (ModifierKeys == Keys.Control && (e.Button & MouseButtons.Left) != 0))
            {

                m_nOriginX = nMX - m_pFix.X;
                m_nOriginY = nMY - m_pFix.Y;

                //int lMinX = Width - m_Grid.m_nWidth - 1;
                //if (m_nOriginX < lMinX) m_nOriginX = lMinX;
                //if (m_nOriginX > 0) m_nOriginX = 0;

                //int lMinY = Height - m_Grid.m_nHeight - 1;
                //if (m_nOriginY < lMinY) m_nOriginY = lMinY;
                //if (m_nOriginY > 0) m_nOriginY = 0;

                m_Grid.MoveGrid(m_nOriginX, m_nOriginY,Width,Height,m_fZoom);
                
                Refresh(true);

            }
            else if ((e.Button & MouseButtons.Left) != 0)
            {
                int nSelCol = 0;
                int nSelRow = 0;
                m_Grid.GetPos(ref nSelRow, ref nSelCol, nMX, nMY);
                //if (pSel != Point.Empty)
                {
                    //Point pSel = pGetPos[0];
                    //if ((m_pAbsColWidth[nSelRow] + m_nOriginX - nMX) < (m_pAbsColWidth[nSelRow]/2)) nSelRow--;
                    //if ((nMY - Rect.Top) < 3) nSelRow--;
                    //if ((Rect.Right - nMX) < 3) nSelRow++;
                    //if ((Rect.Bottom - nMY) < 3) nSelRow++;

                    int nColSelectCount = nSelCol - m_nCol;
                    int nRowSelectCount = nSelRow - m_nRow;
                    if (m_nColSelectCount != nColSelectCount || m_nRowSelectCount != nRowSelectCount)
                    {
                        SetSelect(m_nRow, m_nCol, nRowSelectCount, nColSelectCount);
                    }
                }
                  
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (Focused == false) Focus();
            
            
            if (txtEdit.Visible == true) UpdateEdit();
            if (ModifierKeys == Keys.Control)
            {

                float fMinW =   m_Grid.m_nWidth * 4;
                float fMinH =   m_Grid.m_nHeight * 4;

                if (e.Delta > 0 || (fMinW > Width && fMinH > Height))
                {
                    PointF point = new PointF(e.Location.X - m_nOriginX, e.Location.Y - m_nOriginY);

                    float opx = point.X / m_fZoom;
                    float opy = point.Y / m_fZoom;


                    m_fZoom += e.Delta / 1000.0f;

                    float npx = point.X / m_fZoom;
                    float npy = point.Y / m_fZoom;

                    m_nOriginX += (int)((npx - opx) * m_fZoom);
                    m_nOriginY += (int)((npy - opy) * m_fZoom);
                }
                
            }

            if (ModifierKeys == Keys.Shift)
            {
                m_nOriginX += e.Delta / 10;
            }
            else if (ModifierKeys == Keys.None) 
            {
                m_nOriginY += e.Delta / 10;
                int lMinY = Height - m_Grid.m_nHeight - 1;
            }

            m_Grid.MoveGrid(m_nOriginX, m_nOriginY, Width, Height,m_fZoom);
            Refresh(true);
        }

        private void UpdateEdit()
        {
            if (txtEdit.Visible == true)
            {
                //Backup(m_nEditPosRow, m_nEditPosCol, 0, 0);
                XCell xCell = m_Grid.GetCell(m_nEditPosRow, m_nEditPosCol);
                string sText = xCell.Text;
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
                            xCell.Text = sText;
                            Focus();
                            if (ChangeText != null) ChangeText(this, m_nEditPosRow, m_nEditPosCol);
                        }
                    }

                }
            }
        }


        private void ColtrolCellSize(int x, int y)
        {
            long lRow, lCol;
            int nMinViewRow = 0;
            int nMinViewCol = 0;
            int nMaxViewRow = 0;
            int nMaxViewCol = 0;

            m_Grid.GetSelect(ref nMinViewRow, ref nMinViewCol, ref nMaxViewRow, ref nMaxViewCol);

            int[] pWidth = m_Grid.m_pWidth;
            int[] pHeight = m_Grid.m_pHeight;
            if (pWidth != null && pHeight != null)
            {
                if (x != 0)
                {
                    for (lCol =nMinViewCol;lCol <=nMaxViewCol; lCol++)
                    {
                        pWidth[lCol] += x;
                        if (pWidth[lCol] < 0) pWidth[lCol] = 0;
                    }
                }
                if (y != 0)
                {
                    for (lRow =nMinViewRow; lRow <= nMaxViewRow; lRow++)
                    {
                        pHeight[lRow] += y;
                        if (pHeight[lRow] < 0) pHeight[lRow] = 0;
                    }
                }
                m_Grid.MoveGrid(m_nOriginX, m_nOriginY, Width, Height, m_fZoom);

                Refresh(true);

            }

        }


        public void GetSelect(ref int nMinViewRow, ref int nMinViewCol, ref int nMaxViewRow, ref int nMaxViewCol)
        {
            m_Grid.GetSelect(ref nMinViewRow, ref nMinViewCol, ref nMaxViewRow, ref nMaxViewCol);
        }

        public void SetSelect(int nRow, int nCol, int nRowSelectCount, int nColSelectCount)
        {

            m_nRowSelectCount = nRowSelectCount;
            m_nColSelectCount = nColSelectCount;

            Rectangle OldSelectRect = m_Grid.m_DrawRect;
            m_Grid.SetSelect(m_nRow, m_nCol, nRowSelectCount, nColSelectCount);
            Rectangle NewSelectRect = m_Grid.m_DrawRect;
            if (OldSelectRect.X < NewSelectRect.X) NewSelectRect.X = OldSelectRect.X;
            if (OldSelectRect.Y < NewSelectRect.Y) NewSelectRect.Y = OldSelectRect.Y;
            if (OldSelectRect.Right > NewSelectRect.Right) NewSelectRect.Width = OldSelectRect.Right - NewSelectRect.X;
            if (OldSelectRect.Bottom > NewSelectRect.Bottom) NewSelectRect.Height = OldSelectRect.Bottom - NewSelectRect.Y;
            Invalidate(NewSelectRect);
            
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
            if (m_bEditEnable && ModifierKeys == Keys.Control)
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
            else if (m_bEditEnable && ModifierKeys == Keys.Shift)
            {/*
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
                */
            }
            else
            {
                int nCols = m_Grid.m_nCols;
                int nRows = m_Grid.m_nRows;
             
                switch (keyConde)
                {
                    case Keys.Home: SetSelect(m_nRow, 0, 0, 0); break;
                    case Keys.Left: SetSelect(m_nRow, --m_nCol, 0, 0); Refresh(false); break;
                    case Keys.PageUp:
                        {
                            int nSpRow = 0, nSpCol = 0;
                            int nEpRow = 0, nEpCol = 0;
                            m_Grid.GetPos(ref nSpRow, ref nSpCol,0, 0);
                            m_Grid.GetPos(ref nEpRow, ref nEpCol, 0, Height);

                            int nPageUp = nSpRow - nEpRow + nSpRow;
                            if (nPageUp < 0) nPageUp = 0;
                            SetSelect(nPageUp, m_nCol, 0, 0);
                        } break;
                    case Keys.Up: SetSelect(--m_nRow, m_nCol, 0, 0); Refresh(false); break;
                    case Keys.End: SetSelect(m_nRow, nCols-1, 0, 0); Refresh(false); break;
                    case Keys.Right: SetSelect(m_nRow, ++m_nCol, 0, 0); Refresh(false); break;
                    case Keys.PageDown:
                        {
                            int nSpRow = 0, nSpCol = 0;
                            int nEpRow = 0, nEpCol = 0;
                            m_Grid.GetPos(ref nSpRow, ref nSpCol, 0, 0);
                            m_Grid.GetPos(ref nEpRow, ref nEpCol, 0, Height);
                            int nPageDn = nSpRow - nEpRow + nSpRow;
                            if (nPageDn >= nRows) nPageDn = nRows - 1;
                            SetSelect(nPageDn, m_nCol, 0, 0);
                        } break;
                    case Keys.Down: SetSelect(++m_nRow, m_nCol, 0, 0); Refresh(false); break;
                    case Keys.Delete: Delete(); break;
                    case Keys.Back: Delete(); SetSelect(m_nRow, m_nCol - 1, 0, 0); break;
                    case Keys.Enter: ShowEdit(m_nRow, m_nCol); break;
                    default: return false;
                }
                 
                return true;
            }
            return false;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            int nSelRow = 0;
            int nSelCol = 0;
            
            m_Grid.GetPos(ref nSelRow, ref nSelCol, e.X, e.Y);
            ShowEdit(nSelRow, nSelCol);
        }


        private void ShowEdit(int nEditPosRow,int nEditPosCol)
        {
            if (m_bEditEnable && 0 <= nEditPosCol && nEditPosCol < m_Grid.m_nCols && 0 <= nEditPosRow && nEditPosRow < m_Grid.m_nRows)
            {
                XCell xCell = m_Grid.GetCell(nEditPosRow, nEditPosCol);
                txtEdit.Text = xCell.Text; ;
               

                txtEdit.Left = m_Grid.m_pAbsPosX[nEditPosCol] + 1;
                txtEdit.Top = m_Grid.m_pAbsPosY[nEditPosRow] + 1;
                txtEdit.Width = m_Grid.m_pAbsPosX[nEditPosCol + 1] - txtEdit.Left;
                txtEdit.Height = m_Grid.m_pAbsPosY[nEditPosRow+1] - txtEdit.Top;

                

                Font fFont = new Font(Font.FontFamily, Font.Size * m_fZoom);
                txtEdit.Font = fFont;

                txtEdit.Visible = true;
                txtEdit.Focus();
                txtEdit.SelectAll();
                m_nEditPosRow = nEditPosRow;
                m_nEditPosCol = nEditPosCol;
            }
        }

        private void CopyCell()
        {
            /*
            Point[] pSel = GetSelect(m_nRow, m_nCol, m_nRowSelectCount, m_nColSelectCount);
            if (pSel != null)
            {
                m_xClip = new CellUnit(pSel[(int)SEL_POS.SP].Y, pSel[(int)SEL_POS.SP].X, pSel[(int)SEL_POS.EP].Y, pSel[(int)SEL_POS.EP].X);
                m_xClip.Copy(m_cItem);
            }
            */
        }
        private void PasteCell()
        {/*
            if (m_bEditEnable && m_xClip != null && m_cItem != null)
            {
                m_xClip.Paste(m_cItem, m_nRow, m_nCol, m_nRows, m_nCols);
            }
          */ 
        }

        private void Copy()
        {

            int nMinViewRow = 0;
            int nMinViewCol = 0;
            int nMaxViewRow = 0;
            int nMaxViewCol = 0;
            m_Grid.GetSelect(ref nMinViewRow, ref nMinViewCol, ref nMaxViewRow, ref nMaxViewCol);
            XCell[,] pCell = m_Grid.m_pCell;
            if (pCell != null)
            {
                string sText = "", sTab = "\t", sEnd = "\r\n";

                for (long nRow = nMinViewRow; nRow <= nMaxViewRow; nRow++)
                {
                    for (long nCol = nMinViewCol; nCol <= nMaxViewCol; nCol++)
                    {
                        sText += pCell[nRow,nCol].Text;
                        if (nCol < nMaxViewCol) sText += sTab;
                    }
                    sText += sEnd;
                }
                Clipboard.SetDataObject(sText);
            }

        }

        private void Paste()
        {

            if (m_bEditEnable)
            {
                int nCols = m_Grid.m_nCols;
                int nRows = m_Grid.m_nRows;

               

                string sClipText;
                IDataObject iData = Clipboard.GetDataObject();
                if (true == iData.GetDataPresent(DataFormats.Text))
                {
                    sClipText = (string)(iData.GetData(DataFormats.Text));
                    sClipText = sClipText.Replace("\n", "");
                    string[] sRowText = sClipText.Split('\r');
                    int nRowLength = sRowText.Length - 1;
                    if (nRowLength > 0)
                    {
                        XCell[,] pCell = m_Grid.m_pCell;
                        if ((m_nRow + nRowLength) >= nRows) nRowLength = nRows - m_nRow;

                        string[] sColText = sRowText[0].Split('\t');
                        int nColLength = sColText.Length;
                        if (nColLength > 0)
                        {
                            if ((m_nCol + nColLength) >= nCols) nColLength = nCols - m_nCol;

                            Backup(m_nRow, m_nCol, nRowLength - 1, nColLength - 1);
                            for (int nRow = 0; nRow < nRowLength; nRow++)
                            {
                                int nRowPos = m_nRow + nRow;
                                string[] sText = sRowText[nRow].Split('\t');
                                for (int nCol = 0; nCol < nColLength; nCol++)
                                {
                                    pCell[nRowPos, m_nCol + nCol].Text = sText[nCol];
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
            if (m_bEditEnable == true)
            {

                XCell[,] pCell = m_Grid.m_pCell;

                if (pCell != null)
                {
                    //Backup(m_nRow, m_nCol, m_nRowSelectCount, m_nColSelectCount);

                    int nMinViewRow = 0;
                    int nMinViewCol = 0;
                    int nMaxViewRow = 0;
                    int nMaxViewCol = 0;


                    m_Grid.GetSelect(ref nMinViewRow, ref nMinViewCol, ref nMaxViewRow, ref nMaxViewCol);


                    for (long nRow = nMinViewRow; nRow <= nMaxViewRow; nRow++)
                    {
                        for (long nCol = nMinViewCol; nCol <= nMaxViewCol; nCol++)
                        {

                            pCell[nRow,nCol].Text = "";
                            //			        if(m_cItem[nRow,nCol].Text="")
                            //			        {
                            //				        SysFreeString(m_cItem[nRow,nCol].Text);
                            //				        m_cItem[nRow,nCol].Text=NULL;
                            //			        }					
                        }
                    }
                    Refresh(false);
                }
            }
          
        }


        public void Backup(int lRow, int lCol, int lRowSelectCount, int lColSelectCount)
        {/*
            if (m_bEditEnable == true && m_cItem != null && m_nCols > 0 && m_nRows > 0)
            {
                Point[] pSel = GetSelect(lRow, lCol, lRowSelectCount, lColSelectCount);
                if (pSel != null)
                {
                    CellUnit cUnit = new CellUnit(pSel[(int)SEL_POS.SP].Y, pSel[(int)SEL_POS.SP].X, pSel[(int)SEL_POS.EP].Y, pSel[(int)SEL_POS.EP].X);
                    cUnit.Copy(m_cItem);
                    m_xBackup.Add(cUnit);
                }
            }
            */
        }

        private void Recovery()
        {/*
            if (m_xBackup.Count > 0 && m_bEditEnable == true && m_cItem != null && m_nCols > 0 && m_nRows > 0)
            {
                CellUnit cUnit = (CellUnit)m_xBackup.Last();
                if (cUnit != null)
                {
                    cUnit.Paste(m_cItem);
                    m_xBackup.Del();
                }
                Refresh();
            }
          */ 
        }

        private void txtEdit_KeyUp(object sender, KeyEventArgs e)
        {
            /*
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
             */

        }

        private void txtEdit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            /*
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
             */

        }




    }

    public class XCell
    {
        protected XCell Sub;

        public int Param;
        public Color ForeColor;
        public Color BackColor;
        public Image ForeImage;
        public Image BackImage;
        public StringFormat Align;
        public Font Font;
        public string Text;

        public XCell()
        {
            Reset();
        }
        public void Copy(XCell cItem)
        {
            Sub = cItem.Sub;
            Param = cItem.Param;
            ForeColor = cItem.ForeColor;
            BackColor = cItem.BackColor;
            ForeImage = cItem.ForeImage;
            BackImage = cItem.BackImage;
            Align = cItem.Align;
            Font = cItem.Font;
            Text = cItem.Text;
        }
        public void Reset()
        {
            Sub = null;
            Param = 0;
            ForeColor = Color.Empty;
            BackColor = Color.Empty;
            ForeImage = null;
            BackImage = null;
            Align = null;
            Font = null;
            Text = "";
        }

        public void DrawCell(Graphics gp, Rectangle CellRect)
        {

            if (BackImage != null)
            {
                try
                {
                    float fDspW = BackImage.Width;
                    float fDspH = BackImage.Height;
                    float fScrW = CellRect.Width;
                    float fScrH = CellRect.Height;
                    float fZoom = 1.0f;

                    float fZw = (fScrW + 1) / fDspW;
                    float fZh = (fScrH + 1) / fDspH;
                    if (fZw < fZh)
                    {
                        fZoom = fZw;
                    }
                    else
                    {
                        fZoom = fZh;
                    }
                    
                    fDspW *= fZoom;
                    fDspH *= fZoom;

                    gp.DrawImage(BackImage, new RectangleF(CellRect.X + (fScrW - fDspW) / 2.0f, CellRect.Y + (fScrH - fDspH) / 2.0f, fDspW, fDspH));
                }
                catch (System.Exception e)
                {
                    string sMsg = e.ToString();
                }
            }
            else
            {             
                if (BackColor.A != 0)
                {
                    SolidBrush drawBrushBack = new SolidBrush(BackColor);
                    gp.FillRectangle(drawBrushBack, CellRect);
                    drawBrushBack.Dispose();
                }
            }

        }


        public void DrawText(Graphics gp, float fZoom, Rectangle CellRect, StringFormat DefaultAlign, Color DefaultColor, Font DefaultFont)
        {

            if (Text != null && Text != "")
            {

                StringFormat sTextAlign;
                if (Align == null) sTextAlign = DefaultAlign;             
                else sTextAlign = Align;                

                Color cForeColor;
                if (ForeColor.A == 0)cForeColor = DefaultColor;
                else cForeColor = ForeColor;
                

                Font fFont;
                if (Font == null) fFont = new Font(DefaultFont.FontFamily, DefaultFont.Size * fZoom);
                else fFont = new Font(Font.FontFamily, Font.Size * fZoom);
                
                SolidBrush drawBrushFore = new SolidBrush(cForeColor);
                gp.DrawString(Text, fFont, drawBrushFore, CellRect, sTextAlign);
                drawBrushFore.Dispose();
                fFont.Dispose();
            }
        }


    }

    internal class XGrid : XCellSelect
    {
        public XCell[,] m_pCell;

        public int m_nCols;
        public int m_nRows;

        public int[] m_pWidth;
        public int[] m_pHeight;

        public int[] m_pAbsPosX;
        public int[] m_pAbsPosY;

        private Point[] m_pDrawH;
        private Point[] m_pDrawV;
        public int m_nMinViewRow;
        public int m_nMinViewCol;

        public int m_nMaxViewRow;
        public int m_nMaxViewCol;




        public int m_nWidth;
        public int m_nHeight;


        public XGrid()
        {
            m_pCell = null;

            m_nCols = 0;
            m_nRows = 0;

            m_pWidth = null;
            m_pHeight = null;

            m_pAbsPosX = null;
            m_pAbsPosY = null;

            m_pDrawH = null;
            m_pDrawV = null;

            m_nWidth = 0;
            m_nHeight = 0;
        }


        public void NewMatrix(int nRows, int nCols, int nDefaultWidth, int nDefaultHeight)
        {

            m_nRows = nRows;
            m_nCols = nCols;

            if (nRows > 0 && nCols > 0)
            {
                m_pHeight = new int[nRows];
                m_pWidth = new int[nCols];
                m_pCell = new XCell[nRows, nCols];

                if (nRows > 0 && nCols > 0)
                {
                    for (int nRow = 0; nRow < m_nRows; nRow++)
                    {
                        m_pHeight[nRow] = nDefaultHeight;
                        for (int nCol = 0; nCol < nCols; nCol++)
                        {
                            m_pCell[nRow,nCol] = new XCell();
                        }
                    }

                    for (int nCol = 0; nCol < m_nCols; nCol++)
                    {
                        m_pWidth[nCol] = nDefaultWidth;
                    }

                    m_pAbsPosY = new int[m_nRows + 3];
                    m_pAbsPosX = new int[m_nCols + 3];

                    m_pDrawH = new Point[m_nRows * 2 + 5];
                    m_pDrawV = new Point[m_nCols * 2 + 5];

                    MoveGrid(0, 0, 0, 0,1);

                }
            }

        }
        


        public void MoveGrid(int nOriginX, int nOriginY, int nWidth, int nHeight, float fZoom)
        {

            if (m_nCols > 0 && m_nRows > 0)
            {
                int nRow, nCol;
                

                m_pAbsPosY[0] = nOriginY;
                for (nRow = 0; nRow < m_nRows; nRow++)
                {
                    m_pAbsPosY[nRow + 1] = m_pAbsPosY[nRow] + (int)(m_pHeight[nRow] * fZoom);
                }
                m_nHeight = m_pAbsPosY[m_nRows] - nOriginY;

                m_pAbsPosX[0] = nOriginX;
                for (nCol = 0; nCol < m_nCols; nCol++)
                {
                    m_pAbsPosX[nCol + 1] = m_pAbsPosX[nCol] + (int)(m_pWidth[nCol] * fZoom);
                }
                m_nWidth = m_pAbsPosX[m_nCols] - nOriginX;

                for (nRow = 0; nRow < m_nRows+1; nRow += 2)
                {
                    int lR = nRow * 2;
                    int sx = m_pAbsPosX[0];
                    int ex = m_pAbsPosX[m_nCols];
                    m_pDrawH[lR].X = sx;
                    m_pDrawH[lR].Y = m_pAbsPosY[nRow];
                    lR++;
                    m_pDrawH[lR].X = ex;
                    m_pDrawH[lR].Y = m_pAbsPosY[nRow];
                    lR++;
                    m_pDrawH[lR].X = ex;
                    m_pDrawH[lR].Y = m_pAbsPosY[nRow + 1];
                    lR++;
                    m_pDrawH[lR].X = sx;
                    m_pDrawH[lR].Y = m_pAbsPosY[nRow + 1];
                    lR++;
                    m_pDrawH[lR].X = sx;
                    m_pDrawH[lR].Y = m_pAbsPosY[nRow + 2];
                }

                int nRowH = m_nRows * 2 + 1;
                for (nRow = nRowH; nRow < m_pDrawH.Length; nRow++)
                {
                    m_pDrawH[nRow].X = m_pAbsPosX[0];
                    m_pDrawH[nRow].Y = m_pAbsPosY[m_nRows];

                }


                for (nCol = 0; nCol < m_nCols+1; nCol += 2)
                {
                    int lC = nCol * 2;
                    int sy = m_pAbsPosY[0];
                    int ey = m_pAbsPosY[m_nRows];

                    m_pDrawV[lC].Y = sy;
                    m_pDrawV[lC].X = m_pAbsPosX[nCol];
                    lC++;
                    m_pDrawV[lC].Y = ey;
                    m_pDrawV[lC].X = m_pAbsPosX[nCol];
                    lC++;
                    m_pDrawV[lC].Y = ey;
                    m_pDrawV[lC].X = m_pAbsPosX[nCol + 1];
                    lC++;
                    m_pDrawV[lC].Y = sy;
                    m_pDrawV[lC].X = m_pAbsPosX[nCol + 1];
                    lC++;
                    m_pDrawV[lC].Y = sy;
                    m_pDrawV[lC].X = m_pAbsPosX[nCol + 2];
                }

                int nColH = m_nCols * 2 + 1;
                for (nCol = nColH; nCol < m_pDrawV.Length; nCol++)
                {
                    m_pDrawV[nCol].X = m_pAbsPosX[m_nCols];
                    m_pDrawV[nCol].Y = m_pAbsPosY[0];

                }
                int nMinX = 0;
                int nMinY = 0;
                if (m_pAbsPosX[0] > 0) nMinX = m_pAbsPosX[0];
                if (m_pAbsPosY[0] > 0) nMinY = m_pAbsPosY[0];
                GetPos(ref m_nMinViewRow, ref m_nMinViewCol, nMinX, nMinY);
                GetPos(ref m_nMaxViewRow, ref m_nMaxViewCol, nWidth, nHeight);

                if (m_nMaxViewCol < m_nCols) m_nMaxViewCol++;
                if (m_nMaxViewRow < m_nRows) m_nMaxViewRow++;
                MoveSelect(m_pAbsPosX, m_pAbsPosY);
            }

        }

        public void DrawGrid(Graphics gp, Color BolderColor)
        {
            if (m_pDrawH != null  && m_pDrawV != null)
            {
                Pen BolderPen = new Pen(BolderColor);
                gp.DrawRectangle(BolderPen, m_pAbsPosX[0], m_pAbsPosY[0], m_nWidth, m_nHeight);
                gp.DrawLines(BolderPen, m_pDrawH);
                gp.DrawLines(BolderPen, m_pDrawV);
                BolderPen.Dispose();

            }
        }

        public void DrawCell(Graphics gp, float fZoom ,StringFormat DefaultAlign, Color DefaultColor, Font DefaultFont)
        {
            Rectangle CellRect = new Rectangle();
            int nMinX = m_nMinViewCol;
            int nMinY = m_nMinViewRow;
            int nMaxX = m_nMaxViewCol;
            int nMaxY = m_nMaxViewRow;
            if (nMaxX == 0) nMaxX = m_nCols;
            if (nMaxY == 0) nMaxY = m_nRows;

            for (int nRow = nMinY; nRow < nMaxY; nRow++)
            {
                for (int nCol = nMinX; nCol < nMaxX; nCol++)
                {
                    CellRect.X = m_pAbsPosX[nCol];
                    CellRect.Y = m_pAbsPosY[nRow];
                    CellRect.Width = m_pAbsPosX[nCol + 1] - CellRect.X;
                    CellRect.Height = m_pAbsPosY[nRow + 1] - CellRect.Y;
                    m_pCell[nRow, nCol].DrawCell(gp, CellRect);
                    m_pCell[nRow, nCol].DrawText(gp,fZoom, CellRect, DefaultAlign, DefaultColor, DefaultFont);
                }
            }
        }

        public void SetSelect(int nRow, int nCol, int nRowSelectCount, int nColSelectCount)
        {
            SetSelect(nRow, nCol, nRowSelectCount, nColSelectCount, m_nRows, m_nCols);
            MoveSelect(m_pAbsPosX, m_pAbsPosY);
        }

        public bool GetPos(ref int nRow, ref int nCol, int nMX, int nMY)
        {
            if (m_pAbsPosX != null && m_pAbsPosY != null)
            {

                for (nCol = 0; nCol < m_nCols; nCol++)
                {
                    if (m_pAbsPosX[nCol] <= nMX && nMX < m_pAbsPosX[nCol + 1]) break;
                }

                for (nRow = 0; nRow < m_nRows; nRow++)
                {
                    if (m_pAbsPosY[nRow] <= nMY && nMY < m_pAbsPosY[nRow + 1]) break;
                }
                return true;
            }
            return false;
        }

        public XCell GetCell(int nRow, int nCol)
        {
            if(0 <= nRow && nRow < m_nRows && 0 <= nCol && nCol < m_nCols)
            {
                return m_pCell[nRow, nCol];
            }
            return null;
        }

    }
    
    internal class XCellSelect
    {
        public int m_nMinSelRow;
        public int m_nMinSelCol;
        public int m_nMaxSelRow;
        public int m_nMaxSelCol;
        public int m_nSelectRow;
        public int m_nSelectCol;

        public Rectangle m_DrawRect = new Rectangle(); 
        public XCellSelect()
        {
            m_nMinSelRow = 0;
            m_nMinSelCol = 0;
            m_nMaxSelRow = 0;
            m_nMaxSelCol = 0;
            m_nSelectRow = 0;
            m_nSelectCol = 0;

        }

        public void GetSelect(ref int nMinViewRow, ref int nMinViewCol, ref int nMaxViewRow, ref int nMaxViewCol)
        {
            nMinViewRow = m_nMinSelRow;
            nMinViewCol = m_nMinSelCol;
            nMaxViewRow = m_nMaxSelRow;
            nMaxViewCol = m_nMaxSelCol;
        }

        protected void MoveSelect(int[] pAbsPosX, int[] pAbsPosY)
        {
            try
            {
                m_DrawRect.X = pAbsPosX[m_nMinSelCol];
                m_DrawRect.Y = pAbsPosY[m_nMinSelRow];
                m_DrawRect.Width = pAbsPosX[m_nMaxSelCol + 1] - m_DrawRect.X;
                m_DrawRect.Height = pAbsPosY[m_nMaxSelRow + 1] - m_DrawRect.Y;
            }
            catch(Exception pe)
            {
                string sMsg = pe.ToString();
            }
        }
        public void DrawSelect(Graphics gp, Color SelColor)
        {

            SolidBrush SelBrush = new SolidBrush(SelColor);
            gp.FillRectangle(SelBrush, m_DrawRect);
            SelBrush.Dispose();
        }
  
        protected void SetSelect(int nRow, int nCol, int nRowSelectCount, int nColSelectCount, int nRows, int nCols)
        {
            int nCellX = nCol + nColSelectCount;
            int nCellY = nRow + nRowSelectCount;

            if ( 0 <= nCellY && nCellY < nRows && 0 <= nCellX && nCellX < nCols)
            {

                m_nSelectRow = nRow;
                m_nSelectCol = nCol;
                    

                if (nRowSelectCount > 0)
                {
                    m_nMinSelRow = nRow;
                    m_nMaxSelRow = nRow + nRowSelectCount;
                }
                else
                {
                    m_nMinSelRow = nRow + nRowSelectCount;
                    m_nMaxSelRow = nRow;
                }

                if (nColSelectCount > 0)
                {
                    m_nMinSelCol = nCol;
                    m_nMaxSelCol = nCol + nColSelectCount;
                }
                else
                {
                    m_nMinSelCol = nCol + nColSelectCount;
                    m_nMaxSelCol = nCol;
                }

                if (m_nMinSelCol > m_nMaxSelCol)
                {
                    m_nMinSelCol = m_nMaxSelCol = nCol;
                }
                if (m_nMinSelRow > m_nMaxSelRow)
                {
                    m_nMinSelRow = m_nMaxSelRow = nRow;
                }

                if (m_nMinSelCol < 0) m_nMinSelCol = 0;
                if (m_nMinSelRow < 0) m_nMinSelRow = 0;
                if (m_nMaxSelCol > nCols) m_nMaxSelCol = nCols;
                if (m_nMaxSelRow > nRows) m_nMaxSelRow = nRows;
            }



        }
        
    }

}
