using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJ_Controls.Vision.BaseData;
using System.Xml.Serialization;
using System.IO;

namespace CJ_Controls.Vision.Aligner
{
	public class XTable
	{
		public const int MAX_CAM = 32;

		private int m_nCamCount;

		public XVector m_xMotorDirection;
		public XVector m_xMoveLimit;
		public XVector[] m_xCamPosition = new XVector[MAX_CAM];
		public XTable()
		{
			m_xMotorDirection = new XVector();
			m_xMoveLimit = new XVector();

			for (int n = 0; n < MAX_CAM; n++)
			{
				m_xCamPosition[n] = new XVector();
			}
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
			}
		}

		public void GetLocal(ref XVector xMove, int nLocal)
		{
			double dTx = 0, dTy = 0;

			XVector tPoint = m_xCamPosition[nLocal];
			RotateMethod(tPoint.X - xMove.X, tPoint.Y - xMove.Y, xMove.T, ref dTx, ref dTy);
			xMove.X += dTx;
			xMove.Y += dTy;
		}

		public void GetLocal(ref XVector xMove)
		{
			double dSTx = 0, dSTy = 0;
			double dTx = 0, dTy = 0;

			for (long n = 0; n < m_nCamCount; n++)
			{
				XVector tPoint = m_xCamPosition[n];
				RotateMethod(tPoint.X - xMove.X, tPoint.Y - xMove.Y, xMove.T, ref dTx, ref dTy);
				dSTx += dTx;
				dSTy += dTy;
			}

			xMove.X += dSTx / m_nCamCount;
			xMove.Y += dSTy / m_nCamCount;
		}

		public bool GetMotion(ref XVector xMove)
		{
			GetLocal(ref xMove);

			xMove.X *= m_xMotorDirection.X;
			xMove.Y *= m_xMotorDirection.Y;
			xMove.T *= m_xMotorDirection.T * 180000 / Math.PI;
			double dLimitT = m_xMoveLimit.T * 180000 / Math.PI;
			if (m_xMoveLimit.X > Math.Abs(xMove.X) && m_xMoveLimit.Y > Math.Abs(xMove.Y) && dLimitT > Math.Abs(xMove.T))
			{
				return true;
			}
			return false;
		}

		public bool GetMotionTable(ref XVector xMove, int nLocal, bool bLocal)
		{
			if (bLocal == true)
			{
				GetLocal(ref xMove, nLocal);
			}
			xMove.X *= m_xMotorDirection.X;
			xMove.Y *= m_xMotorDirection.Y;
			xMove.T *= m_xMotorDirection.T * 180000 / Math.PI;
			double dLimitT = m_xMoveLimit.T * 180000 / Math.PI;
			if (m_xMoveLimit.X > Math.Abs(xMove.X) && m_xMoveLimit.Y > Math.Abs(xMove.Y) && dLimitT > Math.Abs(xMove.T))
			{
				return true;
			}
			return false;
		}

		public bool GetMotionUVW(ref XVector xMove, ref XScalar xU, ref XScalar xV, ref XScalar xW)
		{
			GetLocal(ref xMove);

			double dTx = 0, dTy = 0;
			{//U
				RotateMethod(63640, 63640, xMove.T, ref dTx, ref dTy);
				xU.Set(dTx, dTy);
			}
			{//V
				RotateMethod(-63640, -63640, xMove.T, ref dTx, ref dTy);
				xV.Set(dTx, dTy);
			}
			{//W
				RotateMethod(63640, -63640, xMove.T, ref dTx, ref dTy);
				xW.Set(dTx, dTy);
			}

			xMove.X *= m_xMotorDirection.X;
			xMove.Y *= m_xMotorDirection.Y;
			xMove.T *= m_xMotorDirection.T * 180000 / Math.PI;
			double dLimitT = m_xMoveLimit.T * 180000 / Math.PI;
			if (m_xMoveLimit.X > Math.Abs(xMove.X) && m_xMoveLimit.Y > Math.Abs(xMove.Y) && dLimitT > Math.Abs(xMove.T))
			{
				return true;
			}
			return false;
		}

		public bool GetMotionUVWX(XVector xMove, ref XScalar xU, ref XScalar xV, ref XScalar xW, ref XScalar xX)
		{
			xMove.X *= m_xMotorDirection.X;
			xMove.Y *= m_xMotorDirection.Y;
			xMove.T *= m_xMotorDirection.T;

			double dTx = 0, dTy = 0;
			{//U
				RotateMethod(-1005000, 755000, xMove.T, ref dTx, ref dTy);
				xU.Set(dTx, dTy);
			}
			{//V
				RotateMethod(1005000, 755000, xMove.T, ref dTx, ref dTy);
				xV.Set(dTx, dTy);
			}
			{//W
				RotateMethod(-1005000, -755000, xMove.T, ref dTx, ref dTy);
				xW.Set(dTx, dTy);
			}

			{//W
				RotateMethod(1005000, -755000, xMove.T, ref dTx, ref dTy);
				xX.Set(dTx, dTy);
			}

			xMove.T *= 180000 / Math.PI;

			double dLimitT = m_xMoveLimit.T * 180000 / Math.PI;
			if (m_xMoveLimit.X > Math.Abs(xMove.X) && m_xMoveLimit.Y > Math.Abs(xMove.Y) && dLimitT > Math.Abs(xMove.T))
			{
				return true;
			}
			return false;
		}

		public bool GetMoveCalibration(ref XVector xMove)
		{
			xMove.X *= m_xMotorDirection.X;
			xMove.Y *= m_xMotorDirection.Y;
			xMove.T *= m_xMotorDirection.T * 180000 / Math.PI;
			double dLimitT = m_xMoveLimit.T * 180000 / Math.PI;
			if (m_xMoveLimit.X > Math.Abs(xMove.X) && m_xMoveLimit.Y > Math.Abs(xMove.Y) && dLimitT > Math.Abs(xMove.T))
			{
				return true;
			}
			return false;
		}

		public void RotateMethod(double x0, double y0, double dt, ref double tx, ref double ty)
		{
			double x1 = x0 * Math.Cos(dt) + y0 * Math.Sin(dt);
			double y1 = -x0 * Math.Sin(dt) + y0 * Math.Cos(dt);

			tx = x1 - x0;
			ty = y1 - y0;
		}

		public void CalT(int nCam, double dMoveT, XScalar xStartPos, XScalar xEndPos, XScalar xCal)
		{
			XVector xMarkPos = new XVector();
			XScalar xDelta = (xEndPos - xStartPos) * xCal;
			double dDn = 1.0 + Math.Sin(dMoveT) * Math.Sin(dMoveT) / (1.0 - Math.Cos(dMoveT)) - Math.Cos(dMoveT);
			double dUp = xDelta.X * Math.Sin(dMoveT) / (1.0 - Math.Cos(dMoveT)) - xDelta.Y;
			xMarkPos.Y = dUp / dDn;
			xMarkPos.X = (xMarkPos.Y * Math.Sin(dMoveT) - xDelta.X) / (1.0 - Math.Cos(dMoveT));
			m_xCamPosition[nCam].Set(xMarkPos);
		}

		public XVector GetCalT(int nCam, double dMoveT, XScalar xStartPos, XScalar xEndPos, XScalar xCal)
		{
			XVector xMarkPos = new XVector();
			XScalar xDelta = (xEndPos - xStartPos) * xCal;
			double dDn = 1.0 + Math.Sin(dMoveT) * Math.Sin(dMoveT) / (1.0 - Math.Cos(dMoveT)) - Math.Cos(dMoveT);
			double dUp = xDelta.X * Math.Sin(dMoveT) / (1.0 - Math.Cos(dMoveT)) - xDelta.Y;
			xMarkPos.Y = dUp / dDn;
			xMarkPos.X = (xMarkPos.Y * Math.Sin(dMoveT) - xDelta.X) / (1.0 - Math.Cos(dMoveT));
			return xMarkPos;
		}
	}
}
