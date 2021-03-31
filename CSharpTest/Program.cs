using System;
using System.Collections.Generic;
using System.Threading;

namespace CSharpTest
{
    class Program
    {
        static Semaphore fork = new Semaphore(5, 5);

        static void Main(string[] args)
        {
            List<Philosopher> philosophers = PhilosophersBirth();

            ThreadInit(philosophers, fork);
        }

        private static List<Philosopher> PhilosophersBirth()
        {
            Random rnd = new Random();

            List<Philosopher> philosophers = new List<Philosopher>();
            for (int i = 0; i < 5; i++)
            {
                philosophers.Add(new Philosopher(rnd.Next(3, 8) + rnd.NextDouble(),
                                                rnd.Next(2, 6) + rnd.NextDouble(),
                                                i));
            }

            return philosophers;
        }

        private static void ThreadInit(List<Philosopher> philosophers, Semaphore fork)
        {
            List<Thread> thrs = new List<Thread>();

            for (int i = 0; i < 5; i++)
            {
                thrs.Add(new Thread(new ParameterizedThreadStart(Cycle)));
                thrs[i].Start(philosophers[i]);
            }
        }

        private static void Cycle(object obj)
        {
            Philosopher philosopher = (Philosopher)obj;
            
            philosopher.MainCycle(fork);
        }
    }
}