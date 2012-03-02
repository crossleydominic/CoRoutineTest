using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoRoutineTest
{
	public class CoroutineCompleteEventArgs : EventArgs
	{
		public Coroutine Coroutine { get; set; }
	}
}
