using System;
using ThuleSignal.App.Services.Infrastructure;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var benchmark = new CollectionBenchmarkService();
            benchmark.RunBenchmark(50000);
        }
    }
}