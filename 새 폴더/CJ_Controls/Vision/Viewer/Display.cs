using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.Vision.Viewer
{
    public delegate void ChangeControlMode(Control sender, View.E_VIEW_CONTROL_MODE eMode);

    public partial class Display : UserControl
    {

        public event ChangeControlMode ChangeControlMode;
        public event ChangeSelect ChangeSelect;
        public event ChangeDraw ChangeDraw;

        public Display()
        {
            InitializeComponent();
        }

        private void Vision_Load(object sender, EventArgs e)
        {

        }

        public View View
        {
            get
            {
                return c_View;
            }
        }

        private void cmdPoint_Click(object sender, EventArgs e)
        {
            c_View.ControlMode = View.E_VIEW_CONTROL_MODE.VCM_POINT;
            cmdPan.Value = GUI.Button.STATE.OFF;
            cmdPoint.Value = GUI.Button.STATE.ON;
            cmdZoomIn.Value = GUI.Button.STATE.OFF;
            cmdZoomOut.Value = GUI.Button.STATE.OFF;
            if (ChangeControlMode != null) ChangeControlMode(this, c_View.ControlMode);
        }

        private void cmdPan_Click(object sender, EventArgs e)
        {
            c_View.ControlMode = View.E_VIEW_CONTROL_MODE.VCM_PAN;
            cmdPan.Value = GUI.Button.STATE.ON;
            cmdPoint.Value = GUI.Button.STATE.OFF;
            cmdZoomIn.Value = GUI.Button.STATE.OFF;
            cmdZoomOut.Value = GUI.Button.STATE.OFF;
            if (ChangeControlMode != null) ChangeControlMode(this, c_View.ControlMode);
        }

        private void cmdZoomIn_Click(object sender, EventArgs e)
        {
            c_View.ControlMode = View.E_VIEW_CONTROL_MODE.VCM_ZOOM_IN;
            cmdPan.Value = GUI.Button.STATE.OFF;
            cmdPoint.Value = GUI.Button.STATE.OFF;
            cmdZoomIn.Value = GUI.Button.STATE.ON;
            cmdZoomOut.Value = GUI.Button.STATE.OFF;
            if (ChangeControlMode != null) ChangeControlMode(this, c_View.ControlMode);
        }

        private void cmdZoomOut_Click(object sender, EventArgs e)
        {
            c_View.ControlMode = View.E_VIEW_CONTROL_MODE.VCM_ZOOM_OUT;
            cmdPan.Value = GUI.Button.STATE.OFF;
            cmdPoint.Value = GUI.Button.STATE.OFF;
            cmdZoomIn.Value = GUI.Button.STATE.OFF;
            cmdZoomOut.Value = GUI.Button.STATE.ON;
            if (ChangeControlMode != null) ChangeControlMode(this, c_View.ControlMode);
        }

        private void cmdFit_Click(object sender, EventArgs e)
        {
            c_View.Zoom = 0;
            c_View.Refresh();
            if (ChangeControlMode != null) ChangeControlMode(this, View.E_VIEW_CONTROL_MODE.VCM_FIT);
        }

        private void cmdZoom100_Click(object sender, EventArgs e)
        {
            c_View.Zoom = 1;
            c_View.Refresh();
            if (ChangeControlMode != null) ChangeControlMode(this, View.E_VIEW_CONTROL_MODE.VCM_ZOOM_100);

        }




        private void c_View_Click(object sender, EventArgs e)
        {
            InvokeOnClick(this, e);
        }

        private void c_View_ChangeDraw(Graphics pe)
        {
            if (ChangeDraw != null) ChangeDraw(pe);
        }

        private void c_View_ChangeSelect(Control sender)
        {
            if(ChangeSelect!=null) ChangeSelect(sender);

        }

        private void Display_Resize(object sender, EventArgs e)
        {
            c_View.Left = 0;
            c_View.Top = 0;
            c_View.Width = Width;
            c_View.Height = Height - panPattern.Height;
        }

        private void c_View_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

  








    }
}
