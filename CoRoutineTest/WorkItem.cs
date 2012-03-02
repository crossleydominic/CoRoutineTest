using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoRoutineTest
{
	public class WorkItem
	{
		private Coroutine _coroutine;
		private IEnumerator<CoroutineState> _workStepper;

		public WorkItem(Coroutine coroutine)
		{
			_coroutine = coroutine;
			_workStepper = coroutine.GetEnumerator();
		}

		public CoroutineState DoWork()
		{
			if (_workStepper.MoveNext())
			{
				return _workStepper.Current;
			}
			else
			{
				return CoroutineState.Finished;
			}
		}

		public Coroutine Coroutine { get { return _coroutine; } }
	}
}
