using System;
using System.Numerics;
using Aprismatic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace OptimizationsBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Operators>();
        }
    }

    public class Operators
    {
        private readonly BigFraction bf;
        private readonly BigInteger bi;
        private readonly Random rnd = new();

        public Operators()
        {
            bf = new BigFraction(123456.3456);
            bi = new BigInteger(43765);
        }

        [Benchmark]
        public BigFraction AddOld() => bf + new BigFraction(bi);

        [Benchmark]
        public BigFraction AddNew() => bf + bi;

        [Benchmark]
        public BigFraction SubOld() => bf - new BigFraction(bi);

        [Benchmark]
        public BigFraction SubNew() => bf - bi;

        [Benchmark]
        public BigFraction DivOld() => bf / new BigFraction(bi);

        [Benchmark]
        public BigFraction DivNew() => bf / bi;

        [Benchmark]
        public BigFraction MulOld() => bf * new BigFraction(bi);

        [Benchmark]
        public BigFraction MulNew() => bf * bi;

        [Benchmark]
        public bool EqOld() => bf == new BigFraction(bi);

        [Benchmark]
        public bool EqNew() => bf == bi;

        [Benchmark]
        public bool NeqOld() => bf != new BigFraction(bi);

        [Benchmark]
        public bool NeqNew() => bf != bi;

        [Benchmark]
        public bool GtOld() => bf > new BigFraction(bi);

        [Benchmark]
        public bool GtNew() => bf > bi;
    }
}
