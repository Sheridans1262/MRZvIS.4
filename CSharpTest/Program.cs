using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text.Json;

namespace CSharpTest
{
    class Program
    {
        static Semaphore sem = new Semaphore(5, 5);
        //static int count = 5;

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

            /*File.WriteAllText("Philosophers.json", JsonSerializer.Serialize(philosophers));
            List<Philosopher> philosNew =  JsonSerializer.Deserialize<List<Philosopher>>(File.ReadAllText("Philosophers.json"));
            Console.WriteLine(philosNew[0].Eating_time);*/
        }

        private static void PhilFunc(object obj)
        {
            Philosopher philosopher = (Philosopher)obj;

            while(true)
            {
                sem.WaitOne();
                sem.WaitOne();

                Console.WriteLine($"{philosopher.Number} philosopher philosophing.");
                Thread.Sleep((int)philosopher.Philosophing_time * 1000);

                sem.Release();
                sem.Release();

                Console.WriteLine($"{philosopher.Number} philosopher eating.");
                Thread.Sleep((int)philosopher.Eating_time * 1000);

                Console.WriteLine($"{philosopher.Number} philosopher is hungry.");

            }
        }
    }
}