using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJ_Controls.Vision.BaseData;
using System.Xml.Serialization;
using System.IO;



namespace CJ_Controls.Vision.Aligner
{
	public class XAlign
	{
		public enum MODE
		{
			ALIGN = 0,
			CONFIRM = 1,
			CONTACT = 2,
			MAX_ALIGN_MODE
		};
		public const int MAX_CAM = 32;
		public const int MAX_OFFSET = 10;

		public int m_nAlignCount;
		protected int m_nCamCount;
		protected XCam[] m_xCam = new XCam[MAX_CAM];
		public XVector[] m_xTolerance = new XVector[(int)MODE.MAX_ALIGN_MODE];
		public XVector m_xDeviation;
		public XVector m_xTwist;
		public XVector m_xShift;
		public XVector[] m_xOffset = new XVector[MAX_OFFSET];
		public int m_nCamPoseMode;
		public XVector m_xMoveCalibration;
		public XTable m_xTable;
		public XVector[] m_xOffsetData = new XVector[MAX_OFFSET];
		public XVector[] m_xOriginData = new XVector[MAX_OFFSET];
		public XVector[] m_xContactData = new XVector[MAX_OFFSET];
		public double m_dE = 0;
		public double m_dTolE = 100;

		public int[] m_nLight = new int[MAX_CAM];

		public double m_dLength = 100;


		public XVector[] m_xGain = new XVector[MAX_CAM];// new XVector(0, 300, 128);

		public double m_dTolScale = 1000;



		public XAlign()
		{
			m_nAlignCount = 0;
			m_nCamCount = 2;
			for (int n = 0; n < (int)MODE.MAX_ALIGN_MODE; n++)
			{
				m_xTolerance[n] = new XVector();
			}

			m_xDeviation = new XVector();
			m_xTwist = new XVector();
			m_xShift = new XVector();
			for (int n = 0; n < MAX_OFFSET; n++)
			{
				m_xOffset[n] = new XVector();
				m_xOffsetData[n] = new XVector();
				m_xOriginData[n] = new XVector();
				m_xContactData[n] = new XVector();
			}


			for (int n = 0; n < MAX_CAM; n++)
			{
				m_xCam[n] = new XCam();
				m_nLight[n] = 100;
				m_xGain[n] = new XVector(0, 300, 128);

			}
			m_nCamPoseMode = 0xF;

			m_xTable = new XTable();
			m_xMoveCalibration = new XVector();


		}

		public int CamCount
		{
			get
			{
				return m_nCamCount;
			}
			set
			{
				m_nCamCount = value;
				m_xTable.CamCount = m_nCamCount;

			}
		}

		public int AlignCount
		{
			get
			{
				return m_nAlignCount;
			}
			set
			{
				m_nAlignCount = value;
			}
		}

		public XCam[] Cam
		{
			get
			{
				return m_xCam;
			}
			set
			{
				m_xCam = value;
			}
		}

		public bool SetAlign(XVector xDeviation, MODE Mode, int Offset)
		{
			m_xShift.Set(0, 0, 0);
			m_xDeviation.Set(xDeviation);
			//m_xDeviation.Set(m_xDeviation - m_xShift);
			m_xDeviation.Set(m_xDeviation - m_xTwist);
			if (Mode == MODE.ALIGN)
			{
				m_xDeviation.Set(m_xDeviation - m_xOffset[Offset]);
			}
			return true;
		}

		public bool Align(MODE Mode, bool Course, int Offset, int CameraMode)
		{
			for (int n = 0; n < m_nCamCount; n++)
			{
				m_xCam[n].CalPoint(Course);
			}

			GetAngle(CameraMode);
			GetDelta(CameraMode);
			m_xDeviation.Set(m_xDeviation - m_xShift);
			m_xDeviation.Set(m_xDeviation - m_xTwist);
			if (Mode == MODE.ALIGN)
			{
				m_xDeviation.Set(m_xDeviation - m_xOffset[Offset]);
			}

			for (int n = 0; n < m_nCamCount; n++)
			{
				m_xCam[n].CalScale(m_xDeviation);
			}

			double dMarkL = 0;
			double dCoorL = 0;
			double dR = 0;
			if (m_nCamCount == 2)
			{
				dMarkL = (double)(m_xCam[0].Mark - m_xCam[1].Mark);
				dCoorL = (double)(m_xCam[0].Coordinate - m_xCam[1].Coordinate);
				if (Math.Abs(dCoorL - dMarkL) > m_dLength)
				{
					return false;
				}
				dR = dMarkL / 2;
			}

			if (m_nCamCount == 4)
			{
				dMarkL = (double)(m_xCam[0].Mark - m_xCam[3].Mark);
				dCoorL = (double)(m_xCam[0].Coordinate - m_xCam[3].Coordinate);
				if (Math.Abs(dCoorL - dMarkL) > m_dLength)
				{
					return false;
				}

				dR += dMarkL;

				dMarkL = (double)(m_xCam[1].Mark - m_xCam[2].Mark);
				dCoorL = (double)(m_xCam[1].Coordinate - m_xCam[2].Coordinate);
				if (Math.Abs(dCoorL - dMarkL) > m_dLength)
				{
					return false;
				}
				dR += dMarkL;
				dR /= 4;
			}

			m_dE = (double)m_xDeviation + Math.Abs(dR * Math.Tan(m_xDeviation.T));

			return true;// Judge(Mode);
		}

		double GetAngle(int a, int b)
		{

			double dCoorAngle = (m_xCam[b].Coordinate - m_xCam[a].Coordinate).Angle();
			double dMarkAngle = (m_xCam[b].Mark - m_xCam[a].Mark).Angle();
			if (dCoorAngle < 0) dCoorAngle = Math.PI + dCoorAngle;
			if (dMarkAngle < 0) dMarkAngle = Math.PI + dMarkAngle;
			double dAngle = dCoorAngle - dMarkAngle;
			if (dAngle > (Math.PI / 2)) dAngle = dAngle - Math.PI;
			return dAngle;
		}
		private void GetAngle(int nCameraMode)
		{

			switch (m_nCamCount)
			{
				case 1: m_xDeviation.T = m_xCam[0].Dist.Angle(); break;
				case 2: m_xDeviation.T = GetAngle(0, 1); break;
				case 3:
					switch (m_nCamPoseMode)
					{
						case 7:
							switch (nCameraMode)
							{
								case 3: m_xDeviation.T = GetAngle(0, 1); break;
								case 5: m_xDeviation.T = GetAngle(2, 0); break;
								case 6: m_xDeviation.T = GetAngle(2, 1); break;
								case 7:
									{
										double dAngleSum = 0;
										dAngleSum += GetAngle(0, 1);
										dAngleSum += GetAngle(2, 1);
										m_xDeviation.T = dAngleSum / 2;
									} break;
							} break;
						case 11: break;
						case 12: break;
						case 13: break;
						case 14: break;
					} break;
				case 4:
					switch (nCameraMode)
					{
						case 3: m_xDeviation.T = GetAngle(0, 1); break;
						case 5: m_xDeviation.T = GetAngle(2, 0); break;
						case 10: m_xDeviation.T = GetAngle(3, 1); break;
						case 12: m_xDeviation.T = GetAngle(2, 3); break;
						case 7:
							{
								double dAngleSum = 0;
								dAngleSum += GetAngle(0, 1);
								dAngleSum += GetAngle(2, 1);
								m_xDeviation.T = dAngleSum / 2;
							} break;
						case 11:
							{
								double dAngleSum = 0;
								dAngleSum += GetAngle(0, 1);
								dAngleSum += GetAngle(3, 1);
								m_xDeviation.T = dAngleSum / 2;
							} break;
						case 13:
							{
								double dAngleSum = 0;
								dAngleSum += GetAngle(2, 0);
								dAngleSum += GetAngle(2, 3);
								m_xDeviation.T = dAngleSum / 2;
							} break;
						case 14:
							{
								double dAngleSum = 0;
								dAngleSum += GetAngle(3, 1);
								dAngleSum += GetAngle(2, 1);
								m_xDeviation.T = dAngleSum / 2;
							} break;
						case 15:
							{
								double dAngleSum = 0;
								dAngleSum = GetAngle(0, 1);
								dAngleSum = GetAngle(2, 3);
								dAngleSum = GetAngle(2, 0);
								dAngleSum = GetAngle(3, 1);
								m_xDeviation.T = dAngleSum / 4;
							} break;
					} break;
			}
		}

		private XScalar GetDelta(int a, int b)
		{
			XScalar xCenter = m_xCam[a].Mark + m_xCam[b].Mark;
			if (xCenter.X < 1 && xCenter.Y < 1)
			{
				return (m_xCam[a].Dist + m_xCam[b].Dist) / 2;
			}
			else
			{
				double dT = m_xDeviation.T;
				XScalar A = new XScalar(m_xCam[a].Coordinate);
				XScalar B = new XScalar(m_xCam[b].Coordinate);
				A.Rotate(-dT);
				B.Rotate(-dT);
				return ((A - m_xCam[a].Mark) + (B - m_xCam[b].Mark)) / 2;
			}
		}
		private void GetDelta(int nCameraMode)
		{
			switch (m_nCamCount)
			{
				case 1:
					{
						m_xDeviation.Set(m_xCam[0].Dist);
					} break;
				case 2:
					{
						m_xDeviation.Set(GetDelta(0, 1));
					} break;
				case 3:
					switch (m_nCamPoseMode)
					{
						case 7:
							switch (nCameraMode)
							{
								case 3: m_xDeviation.Set(GetDelta(0, 1)); break;
								case 5: m_xDeviation.Set(GetDelta(2, 0)); break;
								case 6: m_xDeviation.Set(GetDelta(2, 1)); break;
								case 7:
									{
										XScalar dDeviationSum = new XScalar();
										dDeviationSum.Set(dDeviationSum + GetDelta(0, 1));
										dDeviationSum.Set(dDeviationSum + GetDelta(2, 1));
										m_xDeviation.Set(dDeviationSum / 2);
									} break;
							} break;
						case 11: break;
						case 12: break;
						case 13: break;
						case 14: break;
					} break;
				case 4:
					switch (nCameraMode)
					{
						case 3: m_xDeviation.Set(GetDelta(0, 1)); break;
						case 5: m_xDeviation.Set(GetDelta(2, 0)); break;
						case 10: m_xDeviation.Set(GetDelta(3, 1)); break;
						case 12: m_xDeviation.Set(GetDelta(2, 3)); break;
						case 7:
							{
								XScalar dDeviationSum = new XScalar();
								dDeviationSum.Set(dDeviationSum + GetDelta(0, 1));
								dDeviationSum.Set(dDeviationSum + GetDelta(2, 1));
								m_xDeviation.Set(dDeviationSum / 2);
							} break;
						case 11:
							{
								XScalar dDeviationSum = new XScalar();
								dDeviationSum.Set(dDeviationSum + GetDelta(0, 1));
								dDeviationSum.Set(dDeviationSum + GetDelta(3, 1));
								m_xDeviation.Set(dDeviationSum / 2);
							} break;
						case 13:
							{
								XScalar dDeviationSum = new XScalar();
								dDeviationSum.Set(dDeviationSum + GetDelta(2, 0));
								dDeviationSum.Set(dDeviationSum + GetDelta(2, 3));
								m_xDeviation.Set(dDeviationSum / 2);
							} break;
						case 14:
							{
								XScalar dDeviationSum = new XScalar();
								dDeviationSum.Set(dDeviationSum + GetDelta(3, 1));
								dDeviationSum.Set(dDeviationSum + GetDelta(2, 1));
								m_xDeviation.Set(dDeviationSum / 2);
							} break;
						case 15:
							{
								XScalar dDeviationSum = new XScalar();
								dDeviationSum.Set(dDeviationSum + GetDelta(0, 1));
								dDeviationSum.Set(dDeviationSum + GetDelta(2, 3));
								dDeviationSum.Set(dDeviationSum + GetDelta(2, 0));
								dDeviationSum.Set(dDeviationSum + GetDelta(3, 1));
								m_xDeviation.Set(dDeviationSum / 4);
							} break;
					} break;
			}
		}

		public bool Judge(MODE eMode)
		{
			XVector xTolerance = m_xTolerance[(int)eMode];
			if (Math.Abs(m_xDeviation.X) <= xTolerance.X
			 && Math.Abs(m_xDeviation.Y) <= xTolerance.Y
			 && Math.Abs(m_xDeviation.T) <= xTolerance.T)
			//&& Math.Abs(m_dE)<= m_dTolE )
			{
				if (eMode == MODE.ALIGN)
				{
					return true;
				}
				else
				{
					for (int n = 0; n < m_nCamCount; n++)
					{
						XVector xScale = m_xCam[n].Scale;
						if ((double)xScale > m_dTolScale)
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		public bool SaveAlign(string sPath)
		{

			if (sPath != null && sPath != "")
			{
				try
				{
					XmlSerializer serializer = new XmlSerializer(typeof(XAlign));
					TextWriter writer = new StreamWriter(sPath);
					serializer.Serialize(writer, this);
					writer.Close();
					return true;
				}
				catch (System.Exception e)
				{
					string sMsg = e.ToString();
				}
			}

			return false;
		}
		public bool LoadAlign(string sPath)
		{
			if (sPath != null && sPath != "")
			{

				XmlSerializer serializer = new XmlSerializer(typeof(XAlign));
				try
				{
					FileStream fs = new FileStream(sPath, FileMode.Open);
					if (fs != null)
					{
						XAlign xAlign = (XAlign)serializer.Deserialize(fs);
						fs.Close();

						m_nAlignCount = xAlign.AlignCount;
						m_nCamCount = xAlign.CamCount;
						m_xTable.CamCount = m_nCamCount;
						for (int n = 0; n < MAX_CAM; n++)
						{
							m_xCam[n].Set(xAlign.Cam[n]);
							m_xTable.m_xCamPosition[n].Set(xAlign.m_xTable.m_xCamPosition[n]);
							m_nLight[n] = xAlign.m_nLight[n];
							m_xGain[n].Set(xAlign.m_xGain[n]);

						}
						for (int n = 0; n < (int)MODE.MAX_ALIGN_MODE; n++)
						{
							m_xTolerance[n].Set(xAlign.m_xTolerance[n]);
						}
						m_xDeviation.Set(xAlign.m_xDeviation);
						m_xTwist.Set(xAlign.m_xTwist);
						m_xShift.Set(xAlign.m_xShift);
						for (int n = 0; n < MAX_OFFSET; n++)
						{
							m_xOffset[n].Set(xAlign.m_xOffset[n]);
							m_xOffsetData[n].Set(xAlign.m_xOffsetData[n]);
							m_xOriginData[n].Set(xAlign.m_xOriginData[n]);
							m_xContactData[n].Set(xAlign.m_xContactData[n]);
						}
						m_nCamPoseMode = xAlign.m_nCamPoseMode;
						m_xMoveCalibration.Set(xAlign.m_xMoveCalibration);
						m_xTable.m_xMotorDirection.Set(xAlign.m_xTable.m_xMotorDirection);
						m_xTable.m_xMoveLimit.Set(xAlign.m_xTable.m_xMoveLimit);
						m_dLength = xAlign.m_dLength;

						//m_xGain0.Set(xAlign.m_xGain0);
						//m_xGain1.Set(xAlign.m_xGain1);
						//m_xGain2.Set(xAlign.m_xGain2);

						m_dE = xAlign.m_dE;
						m_dTolE = xAlign.m_dTolE;
						m_dTolScale = xAlign.m_dTolScale;
					}
				}
				catch (Exception pe)
				{
					string sMsg = pe.ToString();
				}

				return true;
			}

			return false;
		}

		public void CalX(int nCam, XScalar xStartPos, XScalar xEndPos)
		{
			if (m_xMoveCalibration.X != 0 && 0 <= nCam && nCam < MAX_CAM && xStartPos != null && xEndPos != null)
			{
				m_xCam[nCam].Cal.X = m_xMoveCalibration.X / (xEndPos.X - xStartPos.X);
			}
		}
		public void CalY(int nCam, XScalar xStartPos, XScalar xEndPos)
		{
			if (m_xMoveCalibration.Y != 0 && 0 <= nCam && nCam < MAX_CAM && xStartPos != null && xEndPos != null)
			{
				m_xCam[nCam].Cal.Y = m_xMoveCalibration.Y / (xEndPos.Y - xStartPos.Y);
			}
		}
		public void CalT(int nCam, XScalar xStartPos, XScalar xEndPos)
		{
			if (0 <= nCam && nCam < MAX_CAM && xStartPos != null && xEndPos != null)
			{
				m_xTable.CalT(nCam, m_xMoveCalibration.T, xStartPos, xEndPos, m_xCam[nCam].Cal);
			}
		}

		public XVector GetCalT(int nCam, XScalar xStartPos, XScalar xEndPos)
		{
			if (0 <= nCam && nCam < MAX_CAM && xStartPos != null && xEndPos != null)
			{
				return m_xTable.GetCalT(nCam, m_xMoveCalibration.T, xStartPos, xEndPos, m_xCam[nCam].Cal);
			}
			return null;
		}
	}
}
