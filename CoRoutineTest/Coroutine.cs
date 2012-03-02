using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CoRoutineTest
{
	public abstract class Coroutine : IEnumerable<CoroutineState>
	{
		public abstract IEnumerator<CoroutineState> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
