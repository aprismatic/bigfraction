using Aprismatic;
using System;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;

namespace ConstructorTest
{
    public class ConstructorTest
    {
        private readonly ITestOutputHelper output;

        public ConstructorTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "Integer")]
        public void IntConstructor()
        {
            var a = new BigFraction(-10);
            var expected = new BigFraction(new BigInteger(-10), BigInteger.One);
            Assert.Equal(expected, a);
        }

        [Fact(DisplayName = "Long")]
        public void LongConstructor()
        {
            var a = new BigFraction(12147483647);
            var expected = new BigFraction(new BigInteger(12147483647), BigInteger.One);
            Assert.Equal(expected, a);
        }

        [Fact(DisplayName = "Double")]
        public void DouConstructor()
        {
            var a = new BigFraction(6545.99);
            var expected = new BigFraction(new BigInteger(654599), new BigInteger(100));
            Assert.Equal(expected, a);
        }

        [Fact(DisplayName = "Decimal")]
        public void DecConstructor()
        {
            var a = new BigFraction(new decimal(8984.65));
            var expected = new BigFraction(new BigInteger(898465), new BigInteger(100));
            Assert.Equal(expected, a);
        }

        [Fact(DisplayName = "BigInteger")]
        public void BigIntConstructor()
        {
            var a = new BigFraction(new BigInteger(33));
            var expected = new BigFraction(new BigInteger(33), new BigInteger(1));
            Assert.Equal(expected, a);
        }

        [Fact(DisplayName = "Fractions")]
        public void FracConstructor()
        {
            var bibi = new BigFraction(new BigInteger(3300), new BigInteger(9900));
            var expected = new BigFraction(new BigInteger(33), new BigInteger(99));
            Assert.Equal(new BigInteger(3300), bibi.Numerator);
            Assert.Equal(new BigInteger(9900), bibi.Denominator);
            Assert.Equal(expected, bibi);

            var inin = new BigFraction(132, 26);
            expected = new BigFraction(new BigInteger(66), new BigInteger(13));
            Assert.Equal(new BigInteger(132), inin.Numerator);
            Assert.Equal(new BigInteger(26), inin.Denominator);
            Assert.Equal(expected, inin);

            var lolo = new BigFraction(222L, 50L);
            expected = new BigFraction(new BigInteger(111), new BigInteger(25));
            Assert.Equal(new BigInteger(222), lolo.Numerator);
            Assert.Equal(new BigInteger(50), lolo.Denominator);
            Assert.Equal(expected, lolo);
        }
    }
}

namespace ImplicitConversion
{
    public class ImplicitConversion
    {
        [Fact(DisplayName = "BigInteger->BigFraction")]
        public void BigInttoBigFrac()
        {
            BigInteger a = new BigInteger(1000);
            BigFraction afrac = a;
            BigFraction expected = new BigFraction(new BigInteger(1000), new BigInteger(1));
            Assert.Equal(expected, afrac);
        }

        [Fact(DisplayName = "Decimal->BigFraction")]
        public void DectoBigFrac()
        {
            Decimal a = new Decimal(1000.01);
            BigFraction afrac = a;
            BigFraction expected = new BigFraction(new BigInteger(100001), new BigInteger(100));
            Assert.Equal(expected, afrac);
        }

        [Fact(DisplayName = "Double->BigFraction")]
        public void DoubletoBigFrac()
        {
            double a = 1000.01;
            BigFraction afrac = a;
            BigFraction expected = new BigFraction(new BigInteger(100001), new BigInteger(100));
            Assert.Equal(expected, afrac);
        }

        [Fact(DisplayName = "Long->BigFraction")]
        public void LongtoBigFrac()
        {
            long a = 10000;
            BigFraction afrac = a;
            BigFraction expected = new BigFraction(new BigInteger(10000), new BigInteger(1));
            Assert.Equal(expected, afrac);
        }

        [Fact(DisplayName = "Integer->BigFraction")]
        public void InttoBigFrac()
        {
            int a = 1000;
            BigFraction afrac = a;
            BigFraction expected = new BigFraction(new BigInteger(1000), new BigInteger(1));
            Assert.Equal(expected, afrac);
        }
    }
}

namespace OperatorTest
{
    public class BigFractionOperatorBigFraction
    {
        private readonly ITestOutputHelper output;

        public BigFractionOperatorBigFraction(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "Unary minus")]
        public void Unaryminus()
        {
            BigFraction a = new BigFraction(1000);
            a = -a;
            BigFraction expected = new BigFraction(-1000);
            Assert.Equal(expected, a);
        }

        [Fact(DisplayName = "+")]
        public void Addition()
        {
            BigFraction a = new BigFraction(1000.25);
            BigFraction b = new BigFraction(1000.25);
            BigFraction expected = new BigFraction(2000.5);
            Assert.Equal(expected, a + b);
        }

        [Fact(DisplayName = "-")]
        public void Subtraction()
        {
            BigFraction a = new BigFraction(2000.5);
            BigFraction b = new BigFraction(1000.25);
            BigFraction expected = new BigFraction(1000.25);
            Assert.Equal(expected, a - b);
        }

        [Fact(DisplayName = "*")]
        public void Mulplication()
        {
            BigFraction a = new BigFraction(-5.25);
            BigFraction b = new BigFraction(10.1);
            BigFraction expected = new BigFraction(-53.025);
            Assert.Equal(expected, a * b);
        }

        [Fact(DisplayName = "/")]
        public void Division()
        {
            BigFraction a = new BigFraction(5.25);
            BigFraction b = new BigFraction(1.25);
            BigFraction expected = new BigFraction(4.20);
            Assert.Equal(expected, a / b);
        }

        [Fact(DisplayName = ">")]
        public void Greater()
        {
            var a = new BigFraction(5.25);
            var b = new BigFraction(4.20);
            Assert.True(a > b);
            Assert.False(b > a);
            Assert.True(a > -b);
            Assert.True(b > -a);
            Assert.False(-a > b);
            Assert.False(-a > -b);
            Assert.False(a > a);
            Assert.False(b > b);

            Assert.True(BigFraction.One > BigFraction.Zero);
            Assert.True(BigFraction.One > BigFraction.MinusOne);
            Assert.True(BigFraction.Zero > BigFraction.MinusOne);

            Assert.False(BigFraction.One > BigFraction.One);
            Assert.False(BigFraction.Zero > BigFraction.Zero);
            Assert.False(BigFraction.MinusOne > BigFraction.MinusOne);

            Assert.False(BigFraction.MinusOne > BigFraction.Zero);
            Assert.False(BigFraction.Zero > BigFraction.One);
            Assert.False(BigFraction.MinusOne > BigFraction.One);

            Assert.True(BigFraction.Zero > new BigFraction(1, -1));
            Assert.True(BigFraction.One > new BigFraction(1, -1));
            Assert.False(BigFraction.MinusOne > new BigFraction(1, -1));

            Assert.True(BigInteger.Zero > new BigFraction(1, -1));
            Assert.True(BigInteger.One > new BigFraction(1, -1));
            Assert.False(BigInteger.MinusOne > new BigFraction(1, -1));

            Assert.True(new BigFraction(1, -1) > new BigFraction(-2));
            Assert.False(new BigFraction(1,-1) > BigFraction.MinusOne);
            Assert.False(new BigFraction(1, -1) > BigFraction.Zero);

            Assert.True(new BigFraction(1, -1) > new BigInteger(-2));
            Assert.False(new BigFraction(1,-1) > BigInteger.MinusOne);
            Assert.False(new BigFraction(1, -1) > BigInteger.Zero);
        }

        [Fact(DisplayName = "<")]
        public void Smaller()
        {
            BigFraction a = new BigFraction(-251.15);
            BigFraction b = new BigFraction(4.20);
            Assert.True(a < b);
            Assert.False(b < a);
            Assert.True(a < -b);
            Assert.True(b < -a);
            Assert.False(-a < b);
            Assert.False(-a < -b);
            Assert.False(a < a);
            Assert.False(b < b);

            Assert.True(BigFraction.MinusOne < BigFraction.Zero);
            Assert.True(BigFraction.MinusOne < BigFraction.One);
            Assert.True(BigFraction.Zero < BigFraction.One);

            Assert.False(BigFraction.MinusOne < BigFraction.MinusOne);
            Assert.False(BigFraction.Zero < BigFraction.Zero);
            Assert.False(BigFraction.One < BigFraction.One);

            Assert.False(BigFraction.One < BigFraction.Zero);
            Assert.False(BigFraction.Zero < BigFraction.MinusOne);
            Assert.False(BigFraction.One < BigFraction.MinusOne);

            Assert.True(new BigFraction(-2) < new BigFraction(1, -1));
            Assert.False(BigFraction.Zero < new BigFraction(1, -1));
            Assert.False(BigFraction.MinusOne < new BigFraction(1, -1));

            Assert.True(new BigInteger(-2) < new BigFraction(1, -1));
            Assert.False(BigInteger.MinusOne < new BigFraction(1, -1));
            Assert.False(BigInteger.Zero < new BigFraction(1, -1));

            Assert.True(new BigFraction(1, -1) < BigFraction.One);
            Assert.True(new BigFraction(1, -1) < BigFraction.Zero);
            Assert.False(new BigFraction(1, -1) < BigFraction.MinusOne);

            Assert.True(new BigFraction(1, -1) < BigInteger.One);
            Assert.True(new BigFraction(1, -1) < BigInteger.Zero);
            Assert.False(new BigFraction(1, -1) < BigInteger.MinusOne);
        }

        [Fact(DisplayName = ">=")]
        public void GreaterEqual()
        {
            BigFraction a = new BigFraction(251.15);
            BigFraction b = new BigFraction(4.20);
            Assert.True(a >= b);
        }

        [Fact(DisplayName = "<=")]
        public void SmallerEqual()
        {
            BigFraction a = new BigFraction(-251.15);
            BigFraction b = new BigFraction(4.20);
            Assert.True(a <= b);
        }

        [Fact(DisplayName = "==")]
        public void Equal()
        {
            BigFraction a = new BigFraction(-251.15);
            BigFraction b = new BigFraction(-251.15);
            Assert.True(a == b);
        }
    }

    public class BigFractionOperatorOthers
    {
        private readonly ITestOutputHelper output;

        public BigFractionOperatorOthers(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "+")]
        public void Addition()
        {
            var a = new BigFraction(1000.25);
            var c = a + 1000.25;
            var d = a + new decimal(1000.25);
            var e = a + BigInteger.One;
            var f = BigInteger.One + a;
            var expected = new BigFraction(2000.5);
            var expected2 = new BigFraction(1001.25);
            Assert.Equal(expected, c);
            Assert.Equal(expected, d);
            Assert.Equal(expected2, e);
            Assert.Equal(expected2, f);
            Assert.Equal(a, a + BigFraction.Zero);
            Assert.Equal(a, BigFraction.Zero + a);
        }

        [Fact(DisplayName = "-")]
        public void Subtraction()
        {
            var a = new BigFraction(2000.5);
            var c = a - 1000.25;
            var d = a - new decimal(1000.25);
            var e = a - BigInteger.One;
            var f = BigInteger.One - a;
            var expected = new BigFraction(1000.25);
            var expected2 = new BigFraction(1999.5);
            var expected3 = new BigFraction(-1999.5);
            Assert.Equal(expected, c);
            Assert.Equal(expected, d);
            Assert.Equal(expected2, e);
            Assert.Equal(expected3, f);
            Assert.Equal(a, a - BigInteger.Zero);
            Assert.Equal(-a, BigInteger.Zero - a);
        }

        [Fact(DisplayName = "*")]
        public void Mulplication()
        {
            var a = new BigFraction(-5.25);
            var c = a * 10.1;
            var d = a * new decimal(10.1);
            var e = a * new BigInteger(2);
            var f = new BigInteger(2) * a;
            var expected = new BigFraction(-53.025);
            var expected2 = new BigFraction(-10.5);
            Assert.Equal(expected, c);
            Assert.Equal(expected, d);
            Assert.Equal(expected2, e);
            Assert.Equal(expected2, f);
            Assert.True((a * BigInteger.Zero).IsZero);
            Assert.True((BigInteger.Zero * a).IsZero);
            Assert.Equal(a, a * BigInteger.One);
            Assert.Equal(a, BigInteger.One * a);
        }

        [Fact(DisplayName = "/")]
        public void Division()
        {
            var a = new BigFraction(-5.25);
            var c = a / -1.25;
            var d = a / new decimal(-1.25);
            var e = a / new BigInteger(2);
            var f = new BigInteger(21) / a;
            var expected = new BigFraction(4.20);
            var expected2 = new BigFraction(-2.625);
            var expected3 = new BigFraction(-4);
            Assert.Equal(expected, c);
            Assert.Equal(expected, d);
            Assert.Equal(expected2,e);
            Assert.Equal(expected3, f);
            Assert.Equal(BigFraction.Zero, BigInteger.Zero / a);
            Assert.Equal(a, a / BigInteger.One);
        }

        /*[Fact(DisplayName = "%")]
        public void Modulus()
        {
            var a = new BigFraction(5.25);
            var c = a % 5;
            var expected = new BigFraction(0.25);
            Assert.Equal(expected, c);
        }*/

        [Fact(DisplayName = ">")]
        public void Greater()
        {
            var a = new BigFraction(5.25);
            Assert.True(a > 4.2);
            Assert.False(a > 6.8);
            Assert.True(a > BigInteger.One);
            Assert.False(-a > BigInteger.One);
            Assert.False(BigInteger.MinusOne > a);
            Assert.True(BigInteger.MinusOne > -a);
        }

        [Fact(DisplayName = "<")]
        public void Smaller()
        {
            var a = new BigFraction(-251.15);
            Assert.True(a < 4.2);
            Assert.False(a < -300);
            Assert.False(a > BigInteger.One);
            Assert.True(-a > BigInteger.One);
            Assert.True(BigInteger.MinusOne > a);
            Assert.False(BigInteger.One > -a);
        }

        [Fact(DisplayName = ">=")]
        public void GreaterEqual()
        {
            var a = new BigFraction(251.15);
            Assert.True(a >= 251.15);
            Assert.True(a >= 4.2);
            Assert.False(a >= 300.1);
            Assert.True(-a >= -251.15);
            Assert.False(-a >= 4.2);
            Assert.True(-a >= -300.1);

            var b = new BigFraction(100);
            Assert.True(b >= new BigInteger(100));
            Assert.True(b >= BigInteger.One);
            Assert.True(b >= BigInteger.MinusOne);
            Assert.False(b >= new BigInteger(120));
            Assert.True(-b >= new BigInteger(-100));
            Assert.False(-b >= BigInteger.One);
            Assert.False(-b >= BigInteger.MinusOne);
            Assert.True(-b >= new BigInteger(-120));

            Assert.True(new BigInteger(100) >= b);
            Assert.False(BigInteger.One >= b);
            Assert.False(BigInteger.MinusOne >= b);
            Assert.True(new BigInteger(120) >= b);
            Assert.True(new BigInteger(-100) >= -b);
            Assert.True(BigInteger.One >= -b);
            Assert.True(BigInteger.MinusOne >= -b);
            Assert.False(new BigInteger(-120) >= -b);
        }

        [Fact(DisplayName = "<=")]
        public void SmallerEqual()
        {
            var a = new BigFraction(-251.15);
            Assert.True(a <= 4.2);
            Assert.True(a <= -251.15);
            Assert.False(a <= -300.1);
            Assert.True(-a <= 251.15);
            Assert.False(-a <= -4.2);
            Assert.True(-a <= 300.1);

            var b = new BigFraction(-100);
            Assert.True(b <= new BigInteger(-100));
            Assert.True(b <= BigInteger.One);
            Assert.True(b <= BigInteger.MinusOne);
            Assert.False(b <= new BigInteger(-120));
            Assert.True(-b <= new BigInteger(100));
            Assert.False(-b <= BigInteger.One);
            Assert.False(-b <= BigInteger.MinusOne);
            Assert.True(-b <= new BigInteger(120));

            Assert.True(new BigInteger(-100) <= b);
            Assert.False(BigInteger.One <= b);
            Assert.False(BigInteger.MinusOne <= b);
            Assert.True(new BigInteger(-120) <= b);
            Assert.True(new BigInteger(100) <= -b);
            Assert.True(BigInteger.One <= -b);
            Assert.True(BigInteger.MinusOne <= -b);
            Assert.False(new BigInteger(120) <= -b);
        }

        [Fact(DisplayName = "==")]
        public void Equal()
        {
            var a = new BigFraction(-251.15);
            Assert.True(a == -251.15);
            Assert.False(a == 123);

            var b = new BigFraction(40, 20);
            Assert.True(b == new BigInteger(2));
            Assert.True(new BigInteger(2) == b);
            Assert.False(b == new BigInteger(3));
            Assert.False(new BigInteger(3) == b);
        }

        [Fact(DisplayName = "!=")]
        public void NotEqual()
        {
            var a = new BigFraction(51.45);
            Assert.True(a != -25);
            Assert.False(a != 51.45);

            var b = new BigFraction(40, 20);
            Assert.False(b != new BigInteger(2));
            Assert.False(new BigInteger(2) != b);
            Assert.True(b != new BigInteger(3));
            Assert.True(new BigInteger(3) != b);
        }
    }
}

namespace OverrideTest
{
    public class OverrideTest
    {
        private readonly ITestOutputHelper output;

        public OverrideTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "ToString")]
        public void tostring()
        {
            BigFraction a = new BigFraction(-10);
            output.WriteLine(a.ToString());
            a = new BigFraction(21321.12);
            output.WriteLine(a.ToString());
            a = new BigFraction(new decimal(451.50));
            output.WriteLine(a.ToString());
            a = new BigFraction(new BigInteger(10), new BigInteger(20));
            output.WriteLine(a.ToString());
        }

        [Fact(DisplayName = "Equals")]
        public void equals()
        {
            BigFraction a = new BigFraction(new BigInteger(1000), new BigInteger(100));
            BigFraction b = new BigFraction(new BigInteger(100), new BigInteger(10));
            Assert.Equal(a, b);

            BigFraction c = new BigFraction(new BigInteger(1001), new BigInteger(100));
            BigFraction d = new BigFraction(new BigInteger(100), new BigInteger(10));
            Assert.NotEqual(c, d);
        }

        [Fact(DisplayName = "GetHashCode")]
        public void gethashcode()
        {
            BigFraction a = new BigFraction(new BigInteger(1000), new BigInteger(100));
            BigFraction b = new BigFraction(new BigInteger(100), new BigInteger(10));
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }
    }
}

namespace MiscTest
{
    public class MiscTest
    {
        [Fact(DisplayName = "Simplify")]
        public void simplify()
        {
            {
                BigFraction a = new BigFraction(new BigInteger(1000), new BigInteger(100));
                var b = a.Simplify();
                var expected = new BigFraction(10, 1);
                Assert.Equal(expected, b);
                Assert.Equal(expected.Numerator, b.Numerator);
                Assert.Equal(expected.Denominator, b.Denominator);
            }

            {
                BigFraction a = new BigFraction(new BigInteger(20), new BigInteger(100));
                var b = a.Simplify();
                var expected = new BigFraction(1, 5);
                Assert.Equal(expected, b);
                Assert.Equal(expected.Numerator, b.Numerator);
                Assert.Equal(expected.Denominator, b.Denominator);
            }
        }

        [Fact(DisplayName = "ToBigInteger")]
        public void tobiginteger()
        {
            BigFraction a = new BigFraction(new BigInteger(1000), new BigInteger(100));
            BigInteger aint = a.ToBigInteger();
            BigInteger expected = new BigInteger(10);
            Assert.Equal(expected, aint);
        }

        [Fact(DisplayName = "DoubleToFraction")]
        public void doubletofraction()
        {
            double a = 0.5;
            BigFraction b = BigFraction.FromDouble(a, 1e-15);
            BigFraction expected = new BigFraction(new BigInteger(1), new BigInteger(2));
            Assert.Equal(expected, b);
        }

        [Fact(DisplayName = "Abs")]
        public void AbsTest()
        {
            Assert.Equal(BigFraction.One, BigFraction.Abs(new BigFraction(1)));
            Assert.Equal(BigFraction.One, BigFraction.Abs(new BigFraction(-1)));
            Assert.Equal(BigFraction.One, BigFraction.Abs(new BigFraction(1, -1)));
            Assert.Equal(BigFraction.One, BigFraction.Abs(new BigFraction(-1, -1)));
            Assert.Equal(BigFraction.One, BigFraction.Abs(new BigFraction(-1, 1)));
            Assert.Equal(BigFraction.One, BigFraction.Abs(new BigFraction(1, 1)));
            Assert.Equal(BigFraction.Zero, BigFraction.Abs(new BigFraction(0)));
            Assert.Equal(BigFraction.Zero, BigFraction.Abs(new BigFraction(0, -1)));
            Assert.Equal(BigFraction.Zero, BigFraction.Abs(new BigFraction(0, 1)));
        }
    }
}

namespace DecimalTests
{
    public class DecimalScaleTest
    {
        [Fact(DisplayName = "Simple cases")]
        public void SimpleCases()
        {
            Assert.Equal(1m, BigFraction.One.ToDecimal());
            Assert.Equal(0m, BigFraction.Zero.ToDecimal());
            Assert.Equal(-1m, BigFraction.MinusOne.ToDecimal());
        }

        [Fact(DisplayName = "( > MAX ) / ( < MAX )")]
        public void Case1()
        {
            var bigA = new BigFraction(new BigInteger(decimal.MaxValue) * 10, new BigInteger(decimal.MaxValue) - 1);
            var a = bigA.ToDecimal();
            Assert.Equal(10m, a);
        }

        [Fact(DisplayName = "( < MAX ) / ( > MAX )")]
        public void Case2()
        {
            var bigA = new BigFraction(new BigInteger(decimal.MaxValue) / 2, new BigInteger(decimal.MaxValue) * 10);
            var a = bigA.ToDecimal();
            Assert.Equal(0.05m, a);
        }

        [Fact(DisplayName = "( > MAX ) / ( > MAX )")]
        public void Case3()
        {
            var bigA = new BigFraction(new BigInteger(decimal.MaxValue) * 100, new BigInteger(decimal.MaxValue) * 10);
            var a = bigA.ToDecimal();
            Assert.Equal(10m, a);
        }

        [Fact(DisplayName = "( < MIN ) / ( < MAX )")]
        public void Case4()
        {
            var bigA = new BigFraction(new BigInteger(decimal.MinValue) * 10, new BigInteger(decimal.MaxValue) / 2);
            var a = bigA.ToDecimal();
            Assert.Equal(-20m, a);
        }

        [Fact(DisplayName = "( < MIN ) / ( > MAX )")]
        public void Case5()
        {
            var bigA = new BigFraction(new BigInteger(decimal.MinValue) * 10, new BigInteger(decimal.MaxValue) * 20);
            var a = bigA.ToDecimal();
            Assert.Equal(-0.5m, a);
        }

        [Fact(DisplayName = "( < MIN ) / ( < MIN )")]
        public void Case6()
        {
            var bigA = new BigFraction(new BigInteger(decimal.MinValue) * 10, new BigInteger(decimal.MinValue) / 2);
            var a = bigA.ToDecimal();
            Assert.Equal(20m, a);
        }

        [Fact(DisplayName = "( < MAX ) / ( < MIN )")]
        public void Case7()
        {
            var bigA = new BigFraction(new BigInteger(decimal.MaxValue) / 2, new BigInteger(decimal.MinValue) * 5);
            var a = bigA.ToDecimal();
            Assert.Equal(-0.1m, a);
        }

        [Fact(DisplayName = "( > MAX ) / ( < MIN )")]
        public void Case8()
        {
            var bigA = new BigFraction(new BigInteger(decimal.MaxValue) * 10, new BigInteger(decimal.MinValue) * 5);
            var a = bigA.ToDecimal();
            Assert.Equal(-2m, a);
        }


        [Fact(DisplayName = "( = MAX ) / ( = MIN )")]
        public void Case9()
        {
            var bigA = new BigFraction(new BigInteger(decimal.MaxValue), new BigInteger(decimal.MinValue));
            var a = bigA.ToDecimal();
            Assert.Equal(-1m, a);
        }
    }
}

namespace FieldTest
{
    public class FieldTest
    {
        [Fact(DisplayName = "Sign")]
        public void SignTest()
        {
            Assert.Equal(1, new BigFraction(0.5).Sign);
            Assert.Equal(0, new BigFraction(0).Sign);
            Assert.Equal(-1, new BigFraction(-2.5).Sign);

            Assert.Equal(1, new BigFraction(new BigInteger(5), new BigInteger(7)).Sign);
            Assert.Equal(1, new BigFraction(new BigInteger(-9), new BigInteger(-8)).Sign);
            Assert.Equal(-1, new BigFraction(new BigInteger(-5), new BigInteger(7)).Sign);
            Assert.Equal(-1, new BigFraction(new BigInteger(5), new BigInteger(-7)).Sign);
            Assert.Equal(-1, new BigFraction(new BigInteger(9), new BigInteger(-8)).Sign);
            Assert.Equal(-1, new BigFraction(new BigInteger(-9), new BigInteger(8)).Sign);
            Assert.Equal(0, new BigFraction(BigInteger.Zero, BigInteger.One).Sign);
            Assert.Equal(0, new BigFraction(BigInteger.Zero, BigInteger.MinusOne).Sign);
        }

        [Fact(DisplayName = "IsOne")]
        public void IsOneTest()
        {
            Assert.True(new BigFraction(BigInteger.One, BigInteger.One).IsOne);
            Assert.True(new BigFraction(BigInteger.MinusOne, BigInteger.MinusOne).IsOne);
            Assert.False(new BigFraction(BigInteger.One, BigInteger.MinusOne).IsOne);
            Assert.False(new BigFraction(BigInteger.MinusOne, BigInteger.One).IsOne);
            Assert.False(new BigFraction(BigInteger.Zero, BigInteger.One).IsOne);
            Assert.False(new BigFraction(BigInteger.Zero, BigInteger.MinusOne).IsOne);
        }

        [Fact(DisplayName = "IsZero")]
        public void IsZeroTest()
        {
            Assert.False(new BigFraction(BigInteger.One, BigInteger.One).IsZero);
            Assert.False(new BigFraction(BigInteger.MinusOne, BigInteger.MinusOne).IsZero);
            Assert.False(new BigFraction(BigInteger.One, BigInteger.MinusOne).IsZero);
            Assert.False(new BigFraction(BigInteger.MinusOne, BigInteger.One).IsZero);
            Assert.True(new BigFraction(BigInteger.Zero, BigInteger.One).IsZero);
            Assert.True(new BigFraction(BigInteger.Zero, BigInteger.MinusOne).IsZero);
        }

        [Fact(DisplayName = "Pre-defined")]
        public void PreDefinedTest()
        {
            Assert.True(BigFraction.One.IsOne);
            Assert.True(BigFraction.Zero.IsZero);
            Assert.True((-BigFraction.MinusOne).IsOne);
            Assert.Equal(new BigFraction(1), BigFraction.One);
            Assert.Equal(new BigFraction(0), BigFraction.Zero);
            Assert.Equal(new BigFraction(-1), BigFraction.MinusOne);
        }
    }
}
