using System;

namespace CSharpTest
{
    [Serializable]
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
    }
}
