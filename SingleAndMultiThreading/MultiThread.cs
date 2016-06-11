using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SingleAndMultiThreading
{
    internal class MultiThread
    {
        public static double Run(long numOfObjCreated, int numOfCores)
        {
            var watch = new Stopwatch();

            var workerObject = new Worker(numOfObjCreated / numOfCores);

            var listOfThreads = new List<Thread>();


            for (long k = 0; k < numOfCores; k++)
            {
                var workerThread = new Thread(workerObject.DoWork);
                listOfThreads.Add(workerThread);
            }

            watch.Start();
            foreach (var thread in listOfThreads)
            {
                thread.Start();
            }

            byte countOfCompletedThreads = 0;

            while (true)
            {
                foreach (var thread in listOfThreads)
                    if (!thread.IsAlive)
                        countOfCompletedThreads++;

                if (countOfCompletedThreads == numOfCores)
                    break;
                countOfCompletedThreads = 0;

            }

            watch.Stop();

            var totalTime = watch.ElapsedTicks;

            Console.WriteLine("The time to create {0} objects utilizing all {1} cores is: {2} ticks.",
                string.Format("{0:#,###0}", numOfObjCreated), numOfCores, string.Format("{0:#,###0}", totalTime));

            return totalTime;

        }
    }
}