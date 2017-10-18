using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJ_Controls.Vision.BaseData
{
	public enum DATA_FORMAT
	{
		NONE,
		INT,
		DOUBLE,
		PROPERTY,
		JMP,
		STRING,
		BOOL,
		OBJECT,
		ADDRESS,
		INTERNAL
	};

	public enum COM_CODE
	{
		CALC,
		INTERFACE,
		INTERNAL
	};

	public class XCode : X
	{
		protected COM_CODE m_Code;
		public DATA_FORMAT m_Format;
		public XCalc m_Calc;
		public XInterface m_Interface;

		public XCode(XCalc _Calc, DATA_FORMAT Format)
		{
			m_Code = COM_CODE.CALC;
			m_Calc = _Calc;
			m_Format = Format;
			m_Interface = null;
		}
		public XCode(XInterface _Interface, DATA_FORMAT Format)
		{
			m_Code = COM_CODE.INTERFACE;
			m_Calc = null;
			m_Format = Format;
			m_Interface = _Interface;
		}

	}

	class XCom
	{
		protected XCode m_CodeSoc;
		protected XCode m_CodeDst;
		protected string m_sMsg;
		public XCom()
		{
			m_CodeSoc = null;
			m_CodeDst = null;
			m_sMsg = null;
			//            this.Run += new System.EventHandler(this.Running);
		}
		protected int RunSoc()
		{
			int nResult = 0;
			/*
			  switch (m_CodeSoc.m_Code)
			  {
				  case COM_CODE.CC_CALC:
					  nResult = m_Calc.Calculation();
					  m_Object[ID] = nResult;
					  break;
				  case COM_CODE.CC_FCALC: m_Object[ID] = m_FCalc.Calculation(); break;
				  case COM_CODE.CC_PROPERTY: m_Object[ID] = m_CodeSoc.m_Property.XValue; break;
				  case COM_CODE.CC_INTERNAL: m_Object[ID] = m_Object[m_CodeSoc.m_Index]; break;
			  }
			*/
			return nResult;
		}
		protected int RunDst(int nResult)
		{/*
            switch (m_CodeDst.m_Format)
            {
                case DATA_FORMAT.DF_INT: break;
                case DATA_FORMAT.DF_JMP: return nResult;
                case DATA_FORMAT.DF_DOUBLE: break;
                case DATA_FORMAT.DF_OBJECT: break;
                case DATA_FORMAT.DF_ADDRESS:
                    if (m_Dest != 0)
                    {
                        unsafe
                        {
                            int* pPoint = (int*)m_Dest;
                            *pPoint = nResult;
                        }
                    } break;
                case DATA_FORMAT.DF_PROPERTY: m_CodeDst.m_Property.XValue = m_Object[ID]; break;
                case DATA_FORMAT.DF_INTERNAL: m_Object[m_CodeDst.m_Index] = m_Object[ID]; break;
            }
          */
			return 0;
		}
		private void Running(object sender)
		{
			/*
			try
			{
				if (m_CodeSoc != null)
				{
					nData = RunSoc();
				}
				if (m_CodeDst != null)
				{
					return RunDst(nData);
				}
			}
			catch// ()//Exception pe
			{
				m_sMsg = "Formula Error!\n";
				return -1;
			}
			 */
			//return 0;
		}
	}
}
