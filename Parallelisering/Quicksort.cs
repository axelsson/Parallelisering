//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Parallelisering
//{
//    class Quicksort
//    {
//        private static int threshold = 500;

//        static void QuickSort<T>(T[] data, int fromInclusive, int toExclusive)
//            where T : IComparable<T>
//        {
//            int pivotPos = Partition(data, fromInclusive, toExclusive);
//            if (toExclusive - fromInclusive <= threshold)
//            {
//                QuickSort(data, fromInclusive, pivotPos);
//                QuickSort(data, pivotPos, toExclusive);
//            }
//            else Parallel.Invoke(
//                () => QuickSort(data, fromInclusive, pivotPos),
//                () => QuickSort(data, pivotPos, toExclusive));
//        }

//        private static int Partition(IComparable<object> data, int fromInclusive, int toExclusive)
//        {
//            throw new NotImplementedException();
//        }

//        public void WithoutFalseSharing()
//        {
//            int[] results1, results2;
//            Parallel.Invoke(
//                () =>
//                {
//                    Random rand1 = new Random();
//                    results1 = new int[20000000];
//                    for (int i = 0; i < results1.Length; i++)
//                        results1[i] = rand1.Next();
//                },
//                () =>
//                {
//                    Random rand2 = new Random();
//                    results2 = new int[20000000];
//                    for (int i = 0; i < results2.Length; i++)
//                        results2[i] = rand2.Next();
//                });
//        }
//    }
//}

using System;
using System.Diagnostics;

namespace Parallelisering
{
    class Quicksort
    {

        public void QuicksortExample()
        {
            const int size = 1000000;
            var list = new int[size];
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                list[i] = random.Next(size);
            }
            Console.WriteLine("Beginning to sort.");
            var timer = new Stopwatch();
            timer.Start();
            QuicksortExample2(list,0,size-1);
            timer.Stop();
            Console.WriteLine("Time "+timer.ElapsedMilliseconds);
            Console.ReadKey();
        }

        public static void QuicksortExample2(int[] elements, int left, int right)
        {
            int i = left, j = right;
            int pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    int tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                QuicksortExample2(elements, left, j);
            }

            if (i < right)
            {
                QuicksortExample2(elements, i, right);
            }
        }
    }
}
