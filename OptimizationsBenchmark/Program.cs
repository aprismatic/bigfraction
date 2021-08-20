using System;
using Aprismatic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace OptimizationsBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<FromDouble>();
        }
    }

    public class FromDouble
    {
        private readonly double val;
        private readonly Random rnd = new();

        public FromDouble()
        {
            val = rnd.NextDouble() * rnd.Next() * (-1);
        }

        //[Benchmark]
        //public BigFraction Old() => BigFraction.FromDoubleOld(val, 1e-15);

        [Benchmark]
        public BigFraction New() => BigFraction.FromDouble(val, 1e-15);
    }
}
