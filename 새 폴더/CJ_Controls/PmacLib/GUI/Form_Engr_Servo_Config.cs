using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CJ_Controls.PmacLib
{
	public partial class Form_Engr_Servo_Config : Form
	{
		public Form_Engr_Servo_Config()
		{
			InitializeComponent();
		}
		private uint m_BoardNo_1Base = 0;
		private int m_nMaxAxisIndex = (int)SERVO_CNT.MAX_AXIS_COUNT - 1;
		public void SetBoardNo(uint nBoardNo_1Base)
		{
			m_BoardNo_1Base = nBoardNo_1Base;
		}
		private PMacOneBoardCtrl _MotorCtrl = null;
		public PMacOneBoardCtrl MotorCtrl
		{
			get { return _MotorCtrl; }
			set { _MotorCtrl = value; }
		}

		private List<AxisInfo> _AxisList = null;
		private void Form_Engr_Servo_Config_Load(object sender, EventArgs e)
		{
			Label_Board_No.Text = "BOARD NO : " + m_BoardNo_1Base;
			InitAxisList();
		}

		private void ControlDataClear()
		{
			//축 정보
			Combo_Axis_Use.SelectedIndex = 0;
			Combo_Axis_Type.SelectedIndex = 0;
			Text_Axis_Name.Text = "";
			Text_Homming.Text = "";
			Text_HomeEnd.Text = "";
			Num_PlcNo.Value = 0;
			Num_Scale_Value.Value = 0;

			//축의 포지션 데이터
			PositionDataClear();
		}
		private void PositionDataClear()
		{
			//축의 포지션 데이터
			ListBox_Position.Items.Clear();
			Text_Position_Name.Text = "";
			Num_Position_Value.Value = 0;
			Num_Position_speed.Value = 0;
			Num_Position_accel.Value = 0;
		}

		private void InitAxisList()
		{
			ListBox_Axis.Items.Clear();
			_AxisList = MotorCtrl.AxisAllList;

			if (_AxisList == null)
			{
				MessageBox.Show("Servo Data Copy Fail!");
				return;
			}

			m_nMaxAxisIndex = _AxisList.Count - 1;

			for (int nAxis = 0; nAxis < _AxisList.Count; nAxis++)
			{
				ListBox_Axis.Items.Add(string.Format("AXIS #{0}", nAxis + 1));
			}

			if (ListBox_Axis.Items.Count > 0)
				ListBox_Axis.SelectedIndex = 0;
		}

		private void Btn_ServoData_Save_Click(object sender, EventArgs e)
		{
			MotorCtrl.Save_ServoData(m_BoardNo_1Base);
		}

		private void Btn_ServoData_Reload_Click(object sender, EventArgs e)
		{
			MotorCtrl.Load_ServoData(m_BoardNo_1Base);
			InitAxisList();
		}

		private void Btn_Close_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("저장 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				Btn_ServoData_Save.PerformClick();
			}

			if (MessageBox.Show("종료 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				this.Close();
			}
		}

		private void ListBox_Axis_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListBox _List = sender as ListBox;
			int nAxisIndex = _List.SelectedIndex;
			if (nAxisIndex < 0 || nAxisIndex > m_nMaxAxisIndex)
				return;

			ControlDataClear();
			SelectAxis(nAxisIndex);
			DataToGUI_PositionList();
		}

		private void SelectAxis(int nAxis_0Base)
		{
			//Axis Use
			if (_AxisList[nAxis_0Base].UseAxis == true)
				Combo_Axis_Use.SelectedIndex = 1;
			else
				Combo_Axis_Use.SelectedIndex = 0;

			//Axis Type
			Combo_Axis_Type.SelectedIndex = (int)_AxisList[nAxis_0Base].AxisType;

			//Axis Name
			Text_Axis_Name.Text = _AxisList[nAxis_0Base].Name;

			//Axis Home
			Text_Homming.Text = _AxisList[nAxis_0Base].HommingCmd;
			Text_HomeEnd.Text = _AxisList[nAxis_0Base].HomeEndCmd;

			//Plc No
			Num_PlcNo.Value = (decimal)_AxisList[nAxis_0Base].PlcNo;

			//Axis Scale
			Num_Scale_Value.Value = (decimal)_AxisList[nAxis_0Base].Scale;
		}

		private void Combo_AxisType_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox _Cmb = sender as ComboBox;
			int nType = _Cmb.SelectedIndex;
			if (nType < 0)
			{
				ControlDataClear();
				return;
			}

			ControlDataClear();
			DataToGUI_PositionList();
		}

		private void Tab_Type_Value_Setting_SelectedIndexChanged(object sender, EventArgs e)
		{
			int nAxisIndex = ListBox_Axis.SelectedIndex;
			if (nAxisIndex < 0 || nAxisIndex > m_nMaxAxisIndex)
				return;

			//int nType = Combo_AxisType.SelectedIndex;
			//if (nType < 0)
			//	return;

			//TabControl _Tab = sender as TabControl;
			//if (_Tab.SelectedIndex != nType)
			//{
			//	MessageBox.Show("Axis Type is " + ((AXIS_TYPE)nType).ToString());
			//	_Tab.SelectedIndex = nType;
			//}
		}

		private void DataToGUI_PositionList()
		{
			int nAxisIndex = ListBox_Axis.SelectedIndex;
			if (nAxisIndex < 0 || nAxisIndex > m_nMaxAxisIndex)
			{
				return;
			}

			ListBox_Position.Items.Clear();
			for (int nPos = 0; nPos < _AxisList[nAxisIndex].PtList.Count; nPos++)
			{
				ListBox_Position.Items.Add(string.Format("Position #{0}",nPos+1));
			}

			if (ListBox_Position.Items.Count > 0)
				ListBox_Position.SelectedIndex = 0;
		}

		private void ListBox_Position_SelectedIndexChanged(object sender, EventArgs e)
		{
			int nPosIndex = ListBox_Position.SelectedIndex;
			if (nPosIndex < 0)
				return;

			ChangePositionData(nPosIndex);
		}

		private void ChangePositionData(int nPosIndex)
		{
			int nAxisIndex = ListBox_Axis.SelectedIndex;
			if (nAxisIndex < 0 || nAxisIndex > m_nMaxAxisIndex)
				return;

			if (nPosIndex < 0)
				return;

			Text_Position_Name.Text = _AxisList[nAxisIndex].PtList[nPosIndex].Name;
			Num_Position_Value.Value = (decimal)_AxisList[nAxisIndex].PtList[nPosIndex].Position;
			Num_Position_speed.Value = (decimal)_AxisList[nAxisIndex].PtList[nPosIndex].Speed;
			Num_Position_accel.Value = (decimal)_AxisList[nAxisIndex].PtList[nPosIndex].Accel;
		}

		private void Btn_Position_Add_Click(object sender, EventArgs e)
		{
			int nAxisIndex = ListBox_Axis.SelectedIndex;
			if (nAxisIndex < 0 || nAxisIndex > m_nMaxAxisIndex)
				return;

			_AxisList[nAxisIndex].PtList.Add(new PT_Data());

			PositionDataClear();
			DataToGUI_PositionList();
		}

		private void Btn_Position_Delete_Click(object sender, EventArgs e)
		{
			int nAxisIndex = ListBox_Axis.SelectedIndex;
			if (nAxisIndex < 0 || nAxisIndex > m_nMaxAxisIndex)
				return;

			int nPosIndex = ListBox_Position.SelectedIndex;
			if (nPosIndex < 0)
				return;

			_AxisList[nAxisIndex].PtList.RemoveAt(nPosIndex);

			PositionDataClear();
			DataToGUI_PositionList();
		}

		private void Btn_Axis_Data_Set_Click(object sender, EventArgs e)
		{
			AxisDataSave();
		}
		private void AxisDataSave()
		{
			int nAxisIndex = ListBox_Axis.SelectedIndex;
			if (nAxisIndex < 0 || nAxisIndex > m_nMaxAxisIndex)
			{
				MessageBox.Show("Please, Select the Axis.");
				return;
			}

			int nAxisUse = Combo_Axis_Use.SelectedIndex;
			if (nAxisUse < 0)
			{
				MessageBox.Show("Please, Select the Axis Use.");
				return;
			}

			int nAxisType = Combo_Axis_Type.SelectedIndex;
			if (nAxisType < 0)
			{
				MessageBox.Show("Please, Select the Axis Type.");
				return;
			}

			string strAxisName = Text_Axis_Name.Text;
			if (nAxisUse == 1 && strAxisName == "")
			{
				MessageBox.Show("Please, Input the Axis Name.");
				return;
			}

			string strHommingCmd = Text_Homming.Text;
			string strHomeEndCmd = Text_HomeEnd.Text;

			int nPlcNo = (int)Num_PlcNo.Value;
			if (nAxisUse == 1 && nPlcNo == 0)
			{
				MessageBox.Show("Please, Input the Plc No.");
				return;
			}

			double dScale = (double)Num_Scale_Value.Value;
			if (nAxisUse <= 0)
			{
				dScale = 0;
			}

			try
			{
				_AxisList[nAxisIndex].UseAxis = (nAxisUse == 1) ? true : false;
				_AxisList[nAxisIndex].AxisType = (AXIS_TYPE)nAxisType;
				_AxisList[nAxisIndex].Name = strAxisName;
				_AxisList[nAxisIndex].HommingCmd = strHommingCmd;
				_AxisList[nAxisIndex].HomeEndCmd = strHomeEndCmd;
				_AxisList[nAxisIndex].PlcNo = nPlcNo;
				_AxisList[nAxisIndex].Scale = dScale;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Btn_Axis_PositionData_Set_Click(object sender, EventArgs e)
		{
			PositionDataSave();
		}
		private void PositionDataSave()
		{
			int nAxisIndex = ListBox_Axis.SelectedIndex;
			if (nAxisIndex < 0 || nAxisIndex > m_nMaxAxisIndex)
			{
				MessageBox.Show("Please, Select the Axis.");
				return;
			}

			int nPosIndex = ListBox_Position.SelectedIndex;
			if (nPosIndex < 0)
			{
				MessageBox.Show("Please, Select the Position.");
				return;
			}

			string strPosName = Text_Position_Name.Text;
			if (strPosName == "")
			{
				MessageBox.Show("Please, Input the Position Name.");
				return;
			}

			try
			{
				int position = int.Parse(Num_Position_Value.Value.ToString());
				int speed = int.Parse(Num_Position_speed.Value.ToString());
				short accel = short.Parse(Num_Position_accel.Value.ToString());

				_AxisList[nAxisIndex].PtList[nPosIndex].Name = strPosName;
				_AxisList[nAxisIndex].PtList[nPosIndex].Position = position;
				_AxisList[nAxisIndex].PtList[nPosIndex].Speed = speed;
				_AxisList[nAxisIndex].PtList[nPosIndex].Accel = accel;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}