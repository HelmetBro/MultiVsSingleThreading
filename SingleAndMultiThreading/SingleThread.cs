using System;
using System.Diagnostics;

namespace SingleAndMultiThreading
{
    internal class SingleThread
    {
        public static double Run(long numOfObjCreated)
        {
            var watch = new Stopwatch();

            watch.Start();

            for (long i = 0; i < numOfObjCreated; i++)
            {
                new object();
            }

            watch.Stop();

            var totalTime = watch.ElapsedTicks;

            Console.WriteLine("The time to create {0} objects with 1 thread is: {1} ticks.",
                string.Format("{0:#,###0}", numOfObjCreated), string.Format("{0:#,###0}", totalTime));

            return totalTime;

        }
    }
}