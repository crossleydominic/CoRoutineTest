using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoRoutineTest
{
	public class RoundRobinScheduler : IScheduler
	{
		public event CoroutineCompleteHandler CoroutineComplete;

		private Queue<WorkItem> _running = new Queue<WorkItem>();

		public void Add(Coroutine co)
		{
			_running.Enqueue(new WorkItem(co));
		}

		public void Run()
		{
			while (_running.Count > 0)
			{
				//Console.WriteLine(_running.Count.ToString() + " coroutines running");
				WorkItem item = _running.Dequeue();

				if (item.DoWork() == CoroutineState.Running)
				{
					_running.Enqueue(item);
				}
				else
				{
					if (CoroutineComplete != null)
					{
						CoroutineComplete(this, new CoroutineCompleteEventArgs() { Coroutine = item.Coroutine });
					}
				}
			}
		}
	}
}
