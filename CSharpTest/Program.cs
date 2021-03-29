using System;
using System.Collections.Generic;
using System.Threading;
//using System.Windows.Forms;

namespace CSharpTest
{
    class Program
    {
        static Semaphore fork = new Semaphore(5, 5);

        static void Main(string[] args)
        {
            Random rnd = new Random();

            List<Philosopher> philosophers = new List<Philosopher>();
            for (int i = 0; i < 5; i++)
            {
                philosophers.Add(new Philosopher(rnd.Next(3, 8) + rnd.NextDouble(),
                                                rnd.Next(2, 6) + rnd.NextDouble(),
                                                i));
            }

            List<Thread> thrs = new List<Thread>();
            for (int i = 0; i < 5; i++)
            {
                thrs.Add(new Thread(new ParameterizedThreadStart(PhilFunc)));
                thrs[i].Start(philosophers[i]);
            }
        }

        private static void PhilFunc(object obj)
        {
            Philosopher philosopher = (Philosopher)obj;

            while(true)
            {
                fork.WaitOne();
                fork.WaitOne();

                Console.WriteLine($"{philosopher.Number} philosopher eating.");
                Thread.Sleep((int)philosopher.Eating_time * 1000);

                Console.WriteLine($"{philosopher.Number} philosopher philosophing.");

                fork.Release();
                fork.Release();
                
                Thread.Sleep((int)philosopher.Philosophing_time * 1000);

                Console.WriteLine($"{philosopher.Number} philosopher is hungry.");
            }
        }
    }
}