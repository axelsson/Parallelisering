﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Parallelisering
{
    class Quicksort
    {
        private const int Size = 10000000;
        private const int Processors = 8;
        private static readonly int ThreadLimit = Environment.ProcessorCount * 2;
        volatile int _activeThreads = 0;

        public void QuicksortExample()
        {
            var list = InitList();
            var timer = new Stopwatch();

            timer.Start();
            RecursiveQuicksort(list, 0, list.Length - 1, Processors);
            timer.Stop();

            Console.WriteLine("Time " + timer.ElapsedMilliseconds+" processorcount: "+Processors+" or threadLimit: "+ThreadLimit);
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

        public void RecursiveQuicksort(int[] array, int left, int right, int maxDepth)
        {
            if (left >= right)
            {
                return;
            }

            SwapElements(array, left, (left + right) / 2);
            var last = left;
            for (var current = left + 1; current <= right; ++current)
            {
                if (array[current].CompareTo(array[left]) < 0)
                {
                    ++last;
                    SwapElements(array, last, current);
                }
            }

            SwapElements(array, left, last);
            if (maxDepth < 1)
            //if (_activeThreads > ThreadLimit)
            {
                RecursiveQuicksort(array, left, last - 1, maxDepth);
                RecursiveQuicksort(array, last + 1, right, maxDepth);
            }
            else
            {
                
                --maxDepth;
                Parallel.Invoke(
                () => RecursiveQuicksort(array, left, last - 1, maxDepth),
                () => RecursiveQuicksort(array, last + 1, right, maxDepth));
                /*
                Interlocked.Increment(ref _activeThreads);
                Parallel.Invoke(
                    () => RecursiveQuicksort(array, left, last - 1, maxDepth),
                    () => RecursiveQuicksort(array, last + 1, right, maxDepth));
                Interlocked.Decrement(ref _activeThreads);
                 */
            }
        }

        static void SwapElements(int[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}

