using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.IO.MemoryMappedFiles;

namespace CSharpTest
{
    class Program
    {
        readonly static SemaphoreSlim fork = new SemaphoreSlim(5, 5);

        static void Main(string[] args)
        {
            List<Philosopher> philosophers = PhilosophersBirth();

            
            ThreadInit(philosophers);
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

        private static void ThreadInit(List<Philosopher> philosophers)
        {
            List<Thread> thrs = new List<Thread>();

            var mmFile = MemoryMappedFile.CreateFromFile(
                @"C:\Users\Spon'k\source\repos\CSharpTest\CSharpTest\forks.txt",
                System.IO.FileMode.Create, "fileHandle", 1024);
            var myAccessor = mmFile.CreateViewAccessor();

            for (int i = 0; i < 5; i++)
            {
                object[] obj = new object[2] { philosophers[i], myAccessor };
                thrs.Add(new Thread(new ParameterizedThreadStart(Cycle)));
                thrs[i].Start(obj);
            }
        }

        private static void Cycle(object obj)
        {
            Array arr = new object[2];
            arr = (Array)obj;

            Philosopher philosopher = (Philosopher)arr.GetValue(0);
            var accessor = (MemoryMappedViewAccessor)arr.GetValue(1);
            
            philosopher.MainCycle(fork, accessor);
        }
    }
}