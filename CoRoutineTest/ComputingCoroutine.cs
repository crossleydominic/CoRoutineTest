using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoRoutineTest
{
	public abstract class ComputingCoroutine<T> : Coroutine
	{
		public T Result { get; protected set; }
	}
}
