using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CJ_Controls.Vision.BaseData
{
	public enum E_ACT_STATE
	{
		STOP = 0,
		RUN = 1,
		PAUSE = 2,
		DONE = 3,
		ERROR = 4,
		USER = 5,
	}

	public class XActor : X
	{
		public event EventHandler Run;
		private EventWaitHandle m_hEvent;
		private E_ACT_STATE m_eState;
		public int ScanTime;
		private Thread m_tThread;

		public XActor()
		{
			ScanTime = 1;
			m_eState = E_ACT_STATE.STOP;
			m_hEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
		}

		public void Start()
		{
			if (m_tThread == null)
			{
				m_tThread = new Thread(new ParameterizedThreadStart(Loop));
				m_tThread.Start(this);
			}
			else if (m_eState == E_ACT_STATE.PAUSE)
			{
				m_eState = E_ACT_STATE.RUN;
			}
		}

		public void Sleep(int nMilliseconds)
		{
			Thread.Sleep(nMilliseconds);
		}

		public void Pause()
		{
			if (m_eState == E_ACT_STATE.RUN)
			{
				m_eState = E_ACT_STATE.PAUSE;
			}
		}

		public void Stop()
		{
			m_eState = E_ACT_STATE.STOP;
		}
		public void Abort()
		{
			if (m_tThread != null) m_tThread.Abort();
			Run = null;
			m_eState = E_ACT_STATE.ERROR;
		}


		private static void Loop(object actor)
		{
			XActor pActor = (XActor)actor;

			pActor.m_eState = E_ACT_STATE.RUN;

			do
			{
				pActor.m_hEvent.WaitOne(pActor.ScanTime);
				{
					if (pActor.m_eState == E_ACT_STATE.RUN && pActor.Run != null)
					{
						pActor.Run(pActor, null);
						Thread.Sleep(0);
					}
					else
					{
						pActor.m_hEvent.WaitOne(100);
					}
				}
			} while (pActor.m_eState == E_ACT_STATE.RUN || pActor.m_eState == E_ACT_STATE.PAUSE);

			if (pActor.m_eState != E_ACT_STATE.ERROR) pActor.m_eState = E_ACT_STATE.DONE;
			pActor.m_tThread = null;

		}
	}
}
