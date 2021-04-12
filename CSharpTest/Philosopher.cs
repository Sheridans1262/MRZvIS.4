using System;
using System.Threading;
using System.IO.MemoryMappedFiles;

namespace CSharpTest
{
    public class Philosopher
    {
        public double Eating_time { get; set; }
        public double Philosophing_time { get; set;}
        public int Number { get; set; }

        public Philosopher(double eating_time, double philosophing_time, int number)
        {
            Eating_time = eating_time;
            Philosophing_time = philosophing_time;
            Number = number + 1;
        }


        public void MainCycle(SemaphoreSlim fork, MemoryMappedViewAccessor accessor)
        {
            while (true)
            {
                accessor.Write(0, fork.CurrentCount);

                fork.Wait();
                fork.Wait();

                EatingTime();

                fork.Release();
                fork.Release();

                PhilosophingTime();
            }
        }

        public void EatingTime()
        {
            Console.WriteLine($"{Number} philosopher eating.");

            Thread.Sleep((int)Eating_time * 1000);
        }

        public void PhilosophingTime()
        {
            Console.WriteLine($"{Number} philosopher philosophing.");

            Thread.Sleep((int)Philosophing_time * 1000);

            Console.WriteLine($"{Number} philosopher is hungry.");
        }
    }
}
