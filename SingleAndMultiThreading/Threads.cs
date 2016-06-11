using System;

namespace SingleAndMultiThreading
{
    internal class Threads
    {
        private static void Main(string[] args)
        {

            long numOfObjCreated;
            int numberOfTests;

            while (true)
            {
                try
                {
                    Console.Write("Number of objects to create: ");
                    numOfObjCreated = Convert.ToInt64(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input.");
                }
            }


            while (true)
            {
                try
                {
                    Console.Write("Number of tests to run: ");
                    numberOfTests = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input.");
                }
            }


            CalculateResults(numOfObjCreated, numberOfTests);

            Console.ReadKey();

        }


        private static void CalculateResults(long numOfObjCreated, int numberOfTests)
        {
            double totalPercentages = 0;
            for (var i = 0; i < numberOfTests; i++)
            {
                totalPercentages += CompleteTest(numOfObjCreated);
            }

            var accuracy = totalPercentages / numberOfTests;

            if ((int)accuracy == 0)
            {
                Console.WriteLine("\nIn this case, neither single threading or multithreading is faster.\n" +
                                  "They both run equally well under these conditions.\n");
                return;
            }

            if (accuracy < 0)
            {
                Console.WriteLine("\nIn this case with {0} objects being created, single threading is faster!\n",
                    string.Format("{0:#,###0}", numOfObjCreated));
                return;
            }

            Console.WriteLine("\nFrom {0} test(s), {1}% was the average percentage of increased speed in multithreading.\n",
                string.Format("{0:#,###0}", numberOfTests), string.Format("{0:#,###0}", accuracy));
        }

        private static double CompleteTest(long numOfObjCreated)
        {
            Console.WriteLine("Computing...");

            var numOfCores = Environment.ProcessorCount;

            var timeForSingleThread = SingleThread.Run(numOfObjCreated);
            var timeForMultiThread = MultiThread.Run(numOfObjCreated, numOfCores);

            var percentFaster = ((timeForSingleThread / timeForMultiThread) * 100) - 100;

            //note: .NET does its part in assigning a certian thread to its own core

            Console.WriteLine("Using all {0} cores, creating {1} objects is {2}% faster.",
                numOfCores, string.Format("{0:#,###0}", numOfObjCreated), string.Format("{0:#,###0}", percentFaster));

            return percentFaster;
        }
    }
}