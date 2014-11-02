namespace Parallelisering
{
    class Program
    {
        static void Main(string[] args)
        {
            var quicksort = new Quicksort();
            var plinq = new Plinq();

            quicksort.QuicksortExample();
            //plinq.SimpleQuery();
        }
    }
}
