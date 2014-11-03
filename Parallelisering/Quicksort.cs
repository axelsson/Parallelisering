using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Parallelisering
{
    class Quicksort
    {
        private const int Size = 10000000;
        private const int Processors = 10;

        public void QuicksortExample()
        {
            var list = InitList();
            var timer = new Stopwatch();

            timer.Start();
            RecursiveQuicksort(list, 0, list.Length - 1, Processors);
            timer.Stop();

            Console.WriteLine("Time " + timer.ElapsedMilliseconds+" processorcount: "+Processors);
        }

        private static int[] InitList()
        {
            var list = new int[Size];
            var random = new Random();
            for (var i = 0; i < Size; i++)
            {
                list[i] = random.Next(Size);
            }
            return list;
        }

        private static void RecursiveQuicksort(int[] array, int left, int right, int maxDepth)
        {
            if (left >= right)
            {
                return;
            }

            SwapElements(array, left, (left + right) / 2);
            var last = left;
            for (int current = left + 1; current <= right; ++current)
            {
                if (array[current].CompareTo(array[left]) < 0)
                {
                    ++last;
                    SwapElements(array, last, current);
                }
            }

            SwapElements(array, left, last);
            if (maxDepth < 1)
            {
                RecursiveQuicksort(array, left, last - 1, maxDepth);
                RecursiveQuicksort(array, last + 1, right, maxDepth);
            }
            else
            {       //skapa ny tråd för ena intervallet och låt main fortsätta på andra
                --maxDepth;
                
                Parallel.Invoke(
                    () => RecursiveQuicksort(array, left, last - 1, maxDepth),
                    () => RecursiveQuicksort(array, last + 1, right, maxDepth));
                /*
                var newThread = StartTheThread(array, left, last - 1, maxDepth);
                RecursiveQuicksort(array, last + 1, right, maxDepth);
                 */ 
            }
        }

        public static Thread StartTheThread(int[] array, int left, int right, int maxDepth)
        {
            var t = new Thread(() => RecursiveQuicksort(array, left, right, maxDepth));
            t.Start();
            return t;
        }

        static void SwapElements(int[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}

