using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJ_Controls.Vision.BaseData
{
	internal class Opp
	{
		public int id;
		public XCalc calc;
		public Opp[] mult;
		public Opp[] logic;
		public Opp[] judge;
		public decimal val;
		public char opp;

		public Opp(int _id, char _opp, decimal _val)
		{
			id = _id;
			mult = null;
			logic = null;
			judge = null;
			calc = null;
			opp = _opp;
			val = _val;
		}
		public Opp(Opp _opp)
		{
			id = _opp.id;
			calc = _opp.calc;
			opp = _opp.opp;
			val = _opp.val;
		}
	}


	public class XCalc : X
	{
		private int m_nOppCount;
		private Opp[] m_Opp;

		private int m_nSequenceCount;
		private Opp[] m_Sequence;

		public XCalc()
		{
			m_nOppCount = -1;
			m_nSequenceCount = -1;
		}

		public bool Sequence()
		{
			if (m_nOppCount >= 0)
			{
				m_nSequenceCount = -1;
				int nOppCount = m_nOppCount + 1;
				m_Sequence = new Opp[nOppCount];
				int nMulCount = 0;
				int nLogicCount = 0;
				int nJudgeCount = 0;
				int m = 0;
				for (m = 0; (m < nOppCount && m_Opp[m] != null); m++)
				{
					char opp = m_Opp[m].opp;
					if (opp == '=' || opp == '-' || opp == '+')
					{
						if (nMulCount > 0)
						{
							m_Sequence[m_nSequenceCount].mult = new Opp[nMulCount];
							for (int n = 0; n < nMulCount; n++)
							{
								m_Sequence[m_nSequenceCount].mult[n] = new Opp(m_Opp[m - nMulCount + n]);
							}
						}
						if (nLogicCount > 0)
						{
							m_Sequence[m_nSequenceCount].logic = new Opp[nLogicCount];
							for (int n = 0; n < nLogicCount; n++)
							{
								m_Sequence[m_nSequenceCount].logic[n] = new Opp(m_Opp[m - nLogicCount + n]);
							}
						}

						if (nJudgeCount > 0)
						{
							m_Sequence[m_nSequenceCount].judge = new Opp[nJudgeCount];
							for (int n = 0; n < nJudgeCount; n++)
							{
								m_Sequence[m_nSequenceCount].judge[n] = new Opp(m_Opp[m - nJudgeCount + n]);
							}
						}

						m_Sequence[++m_nSequenceCount] = new Opp(m_Opp[m]);
						nMulCount = 0;
						nLogicCount = 0;
						nJudgeCount = 0;
					}

					if (opp == '%' || opp == '*' || opp == '/')
					{
						nMulCount++;
					}
					if (opp == '~' || opp == '&' || opp == '^' || opp == '|')
					{
						nLogicCount++;
					}
					if (opp == '?' || opp == ':')
					{
						nJudgeCount++;
					}

				}


				if (nMulCount > 0)
				{
					m_Sequence[m_nSequenceCount].mult = new Opp[nMulCount];
					for (int n = 0; n < nMulCount; n++)
					{
						m_Sequence[m_nSequenceCount].mult[n] = new Opp(m_Opp[m - nMulCount + n]);
					}
				}

				if (nLogicCount > 0)
				{
					m_Sequence[m_nSequenceCount].logic = new Opp[nLogicCount];
					for (int n = 0; n < nLogicCount; n++)
					{
						m_Sequence[m_nSequenceCount].logic[n] = new Opp(m_Opp[m - nLogicCount + n]);
					}
				}
				if (nJudgeCount > 0)
				{
					m_Sequence[m_nSequenceCount].judge = new Opp[nJudgeCount];
					for (int n = 0; n < nJudgeCount; n++)
					{
						m_Sequence[m_nSequenceCount].judge[n] = new Opp(m_Opp[m - nJudgeCount + n]);
					}
				}


				return true;
			}
			return false;
		}
		public bool Formula(string sFormula)
		{
			m_nOppCount = 0;
			string sSubFormula = string.Empty;
			int nSubCount = 0;
			sFormula = sFormula.Trim();
			int nLen = sFormula.Length;
			if (0 < nLen && sFormula[0] == '=')
			{
				decimal dPoint = 0;
				m_Opp = new Opp[nLen];
				m_nOppCount = -1;
				for (int n = 0; n < nLen; n++)
				{
					char ch = sFormula[n];
					if (nSubCount == 0)
					{
						switch (ch)
						{
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								if (dPoint == 0)
								{
									m_Opp[m_nOppCount].val = m_Opp[m_nOppCount].val * 10 + ch - '0';
								}
								else
								{
									m_Opp[m_nOppCount].val = m_Opp[m_nOppCount].val + (ch - '0') / dPoint; dPoint *= 10;
								}
								break;

							case '?':
							case ':':
							case '~':
							case '!':
							case '&':
							case '^':
							case '|':
							case '/':
							case '*':
							case '%':
							case '-':
							case '+':
							case '=': m_Opp[++m_nOppCount] = new Opp(m_nOppCount, ch, 0); dPoint = 0; break;
							case '[': break;
							case ']': break;
							case '(': sSubFormula = "="; nSubCount++; m_Opp[m_nOppCount].calc = new XCalc(); dPoint = 0; break;
							case ')': return false;
							case '.': dPoint = 10; break;

							default: return false;
						}
					}
					else
					{
						switch (ch)
						{
							case '(': nSubCount++; break;
							case ')': nSubCount--; break;
						}
						if (nSubCount == 0)
						{
							m_Opp[m_nOppCount].calc.Formula(sSubFormula);
						}
						else
						{
							sSubFormula += ch;
						}
					}
				}
				return Sequence();
			}
			return false;
		}

		public bool Cal(ref decimal val)
		{
			if (m_nOppCount >= 0 && m_nSequenceCount >= 0)
			{
				decimal nVal = 0;
				int nSequenceCount = m_nSequenceCount + 1;
				for (int n = 0; n < nSequenceCount; n++)
				{
					Opp Seq = m_Sequence[n];
					Seq.val = m_Opp[Seq.id].val;
					if (Seq.calc != null) Seq.calc.Cal(ref Seq.val);
					if (Seq.mult != null) CalMul(Seq.mult, ref Seq.val);
					if (Seq.logic != null) CalLogic(Seq.logic, ref Seq.val);
					if (Seq.judge != null) CalJudge(Seq.judge, ref Seq.val);
					switch (Seq.opp)
					{
						case '=': nVal = Seq.val; break;
						case '-': nVal -= Seq.val; break;
						case '+': nVal += Seq.val; break;
					}
				}
				val = nVal;
				return true;
			}
			return false;
		}
		private bool CalMul(Opp[] Opp, ref decimal val)
		{
			int nCount = Opp.Length;
			for (int n = 0; n < nCount; n++)
			{
				Opp Seq = Opp[n];
				Seq.val = m_Opp[Seq.id].val;
				if (Seq.calc != null) Seq.calc.Cal(ref Seq.val);
				switch (Seq.opp)
				{
					case '%': val %= Seq.val; break;
					case '/': val /= Seq.val; break;
					case '*': val *= Seq.val; break;
				}
			}
			return true;
		}
		private bool CalLogic(Opp[] Opp, ref decimal val)
		{
			int nCount = Opp.Length;
			int nVal = (int)val;
			for (int n = 0; n < nCount; n++)
			{
				Opp Seq = Opp[n];
				Seq.val = m_Opp[Seq.id].val;
				if (Seq.calc != null) Seq.calc.Cal(ref Seq.val);
				int nSeq = (int)Seq.val;
				switch (Seq.opp)
				{
					case '&': nVal &= nSeq; break;
					case '^': nVal ^= nSeq; break;
					case '|': nVal |= nSeq; break;
				}
			}
			val = nVal;
			return true;
		}
		private bool CalJudge(Opp[] Opp, ref decimal val)
		{
			int nCount = Opp.Length;
			int nVal = (int)val;
			bool bJudge = false;
			if (nCount == 2 && Opp[0].opp == '?' && Opp[1].opp == ':')
			{
				for (int n = 0; n < nCount; n++)
				{
					Opp Seq = Opp[n];
					Seq.val = m_Opp[Seq.id].val;
					if (Seq.calc != null) Seq.calc.Cal(ref Seq.val);
					int nSeq = (int)Seq.val;
					switch (Seq.opp)
					{
						case '?': if (nVal != 0)
							{
								nVal = nSeq;
								bJudge = true;
							}
							else
							{
								bJudge = false;
							} break;

						case ':': if (bJudge == false) nVal = nSeq; break;
					}
				}
				val = nVal;
				return true;
			}
			return false;
		}

	}
}
