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
            var summary = BenchmarkRunner.Run<ToDecimal>();
        }
    }

    public class ToDecimal
    {
        private readonly BigFraction val_lt1, val_gt1;
        private readonly Random rnd = new();

        public ToDecimal()
        {
            val_lt1 = new BigFraction(new BigInteger(decimal.MaxValue), new BigInteger(decimal.MaxValue)) *
                      new BigFraction(BigInteger.One, new BigInteger(2));
            val_gt1 = new BigFraction(new BigInteger(decimal.MaxValue), new BigInteger(decimal.MaxValue)) * 2;

            Console.WriteLine(val_lt1);
        }

        [Benchmark]
        public decimal Old_LT1() => val_lt1.ToDecimal();

        //[Benchmark]
        //public decimal New_LT1() => val_lt1.ToDecimalNew();

        [Benchmark]
        public decimal Old_GT1() => val_gt1.ToDecimal();

        //[Benchmark]
        //public decimal New_GT1() => val_gt1.ToDecimalNew();
    }
}
