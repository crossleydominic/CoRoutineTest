using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoRoutineTest
{
	public static class SchedulerFactory
	{
		public static IScheduler Create(SchedulerType schedulerType)
		{
			switch (schedulerType)
			{
				case SchedulerType.RoundRobin:
					return new RoundRobinScheduler();
				case SchedulerType.Priority:
					return null;
				default:
					throw new InvalidOperationException("Scheduler type cannot be created by factory");
			}
		}
	}
}
