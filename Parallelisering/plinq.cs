﻿using System;
using System.Linq;

namespace Parallelisering
{
    public class Plinq
    {

        public void PlinqQuery()
        {
            var source = Enumerable.Range(100, 20000);

            // Result sequence might be out of order. 
            var parallelQuery = from num in source.AsParallel()
                where num % 10 == 0
                select num;

            // Process result sequence in parallel
            parallelQuery.ForAll((e) => DoSomething(e));

            // You can also use ToArray, ToList, etc as with LINQ to Objects. 
            var parallelQuery2 = (from num in source.AsParallel()
                where num % 10 == 0
                select num).ToArray();

            // Method syntax is also supported 
            var parallelQuery3 = source.AsParallel().Where(n => n % 10 == 0).Select(n => n);

            Console.ReadKey();
        }

        static void DoSomething(int i)
        {
        
        }
    }
}