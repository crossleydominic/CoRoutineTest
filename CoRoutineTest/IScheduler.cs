using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoRoutineTest
{
	public delegate void CoroutineCompleteHandler(object sender, CoroutineCompleteEventArgs args);

	public interface IScheduler
	{
		event CoroutineCompleteHandler CoroutineComplete;

		void Add(Coroutine coroutine);
		void Run();
	}
}
