using System;
using System.Linq;
using System.Threading.Tasks;

namespace Parallelisering
{
    class Program
    {
        static void Main(string[] args)
        {
            var quicksort = new Quicksort();
            var plinq = new Plinq();
            quicksort.QuicksortExample();
            //plinq.PlinqQuery();
            //ForExample();
            //ForEachExample();
            //Console.ReadKey();
        }


        public static void ForExample()
        {
            const int size = 20;
            var result = new int[size];
            Parallel.For(0, size, i =>
            {
                result[i] = i*i;
                Console.WriteLine(result[i]);
            });
            Console.WriteLine("Completed ForExample");
        }

        public static void ForEachExample()
        {
            var list = Enumerable.Range(0, 20);
            var counterLock = new object();
            var counter = 0;
            Parallel.ForEach(list, i =>
            {
                lock (counterLock)
                {
                    counter++;
                }
                Console.WriteLine(i);
            });
            Console.WriteLine("Completed ForEachExample, counter: "+counter);
        }
    }
}
