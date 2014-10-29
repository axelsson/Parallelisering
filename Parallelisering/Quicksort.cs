using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallelisering
{
    class Quicksort
    {
        private static int threshold = 500;

        static void QuickSort<T>(T[] data, int fromInclusive, int toExclusive)
            where T : IComparable<T>
        {
                int pivotPos = Partition(data, fromInclusive, toExclusive);
                if (toExclusive - fromInclusive <= threshold)
                {
                    QuickSort(data, fromInclusive, pivotPos);             
                    QuickSort(data, pivotPos, toExclusive);         
                }         
                else Parallel.Invoke(
                    () => QuickSort(data, fromInclusive, pivotPos),
                    () => QuickSort(data, pivotPos, toExclusive));
        }

        public void WithoutFalseSharing(){
            int[] results1, results2;
            Parallel.Invoke(
                () => { Random rand1 = new Random();
                results1 = new int[20000000];
                for (int i = 0; i < results1.Length; i++)
                    results1[i] = rand1.Next(); 
                },
                () => { Random rand2 = new Random();
                    results2 = new int[20000000];
                    for (int i = 0; i < results2.Length; i++)
                        results2[i] = rand2.Next(); }); 
        } 
    }
}
