using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Concurrent;
using System.Collections;

namespace CoRoutineTest
{
	class Program
	{
		static void Main(string[] args)
		{
			IScheduler scheduler = SchedulerFactory.Create(SchedulerType.RoundRobin);

			foreach (string s in Directory.GetFiles(@"C:\Downloads"))
			{
				scheduler.Add(new CopyFileAsync(s, @"C:\Downloads\COPYTEST\COPYDEST"));
			}

			scheduler.CoroutineComplete += new CoroutineCompleteHandler(scheduler_CoroutineComplete);
			scheduler.Run();

			Console.ReadKey();
		}

		static void scheduler_CoroutineComplete(object sender, CoroutineCompleteEventArgs args)
		{
			Console.WriteLine("Finished " + args.Coroutine.GetType().Name);
		}
	}
}
