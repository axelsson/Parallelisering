using System;

namespace Parallelisering
{
    class Program
    {
        static void Main(string[] args)
        {
            var quicksort = new Quicksort();
            var plinq = new Plinq();
            for (int i = 0; i < 5; i++)
            {
                plinq.PlinqQuery();
//                quicksort.QuicksortExample();
            }
            Console.ReadKey();
        }
    }
}
