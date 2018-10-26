using Xunit;
using System.Numerics;
using Xunit.Abstractions;
using System;
using Aprismatic;

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
            BigFraction a = new BigFraction(-10);
            BigFraction expected = new BigFraction(new BigInteger(-10), BigInteger.One);
            Assert.Equal(expected, a);
        }

        [Fact(DisplayName = "Double")]
        public void DouConstructor()
        {
            BigFraction a = new BigFraction(6545.99);
            BigFraction expected = new BigFraction(new BigInteger(654599), new BigInteger(100));
            Assert.Equal(expected, a);
        }

        [Fact(DisplayName = "Decimal")]
        public void DecConstructor()
        {
            BigFraction a = new BigFraction(new decimal(8984.65));
            BigFraction expected = new BigFraction(new BigInteger(898465), new BigInteger(100));
            Assert.Equal(expected, a);
        }

        [Fact(DisplayName = "BigInteger")]
        public void BigIntConstructor()
        {
            BigFraction a = new BigFraction(new BigInteger(33));
            BigFraction expected = new BigFraction(new BigInteger(33), new BigInteger(1));
            Assert.Equal(expected, a);
        }

        [Fact(DisplayName = "Fractional")]
        public void FracConstructor()
        {
            BigFraction a = new BigFraction(new BigInteger(3300), new BigInteger(9900));
            BigFraction expected = new BigFraction(new BigInteger(33), new BigInteger(99));
            Assert.Equal(expected, a);
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

        [Fact(DisplayName = "%")]
        public void Modulus()
        {
            BigFraction a = new BigFraction(5.25);
            BigInteger b = new BigInteger(5);
            BigFraction expected = new BigFraction(0.25);
            Assert.Equal(expected, a % b);
        }

        [Fact(DisplayName = ">")]
        public void Greater()
        {
            BigFraction a = new BigFraction(5.25);
            BigFraction b = new BigFraction(4.20);
            Assert.True(a > b);
        }

        [Fact(DisplayName = "<")]
        public void Smaller()
        {
            BigFraction a = new BigFraction(-251.15);
            BigFraction b = new BigFraction(4.20);
            Assert.True(a < b);
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
            BigFraction a = new BigFraction(1000.25);
            BigFraction c = a + 1000.25;
            BigFraction d = a + new decimal(1000.25);
            BigFraction expected = new BigFraction(2000.5);
            Assert.Equal(expected, c);
            Assert.Equal(expected, d);
        }

        [Fact(DisplayName = "-")]
        public void Subtraction()
        {
            BigFraction a = new BigFraction(2000.5);
            BigFraction c = a - 1000.25;
            BigFraction d = a - new decimal(1000.25);
            BigFraction expected = new BigFraction(1000.25);
            Assert.Equal(expected, c);
            Assert.Equal(expected, d);
        }

        [Fact(DisplayName = "*")]
        public void Mulplication()
        {
            BigFraction a = new BigFraction(-5.25);
            BigFraction c = a * 10.1;
            BigFraction d = a * new decimal(10.1);
            BigFraction expected = new BigFraction(-53.025);
            Assert.Equal(expected, c);
            Assert.Equal(expected, d);
        }

        [Fact(DisplayName = "/")]
        public void Division()
        {
            BigFraction a = new BigFraction(-5.25);
            BigFraction c = a / -1.25;
            BigFraction d = a / new decimal(-1.25);
            BigFraction expected = new BigFraction(4.20);
            Assert.Equal(expected, c);
            Assert.Equal(expected, d);
        }

        [Fact(DisplayName = "%")]
        public void Modulus()
        {
            BigFraction a = new BigFraction(5.25);
            BigFraction c = a % 5;
            BigFraction expected = new BigFraction(0.25);
            Assert.Equal(expected, c);
        }

        [Fact(DisplayName = ">")]
        public void Greater()
        {
            BigFraction a = new BigFraction(5.25);
            Assert.True(a > 4.2);
        }

        [Fact(DisplayName = "<")]
        public void Smaller()
        {
            BigFraction a = new BigFraction(-251.15);
            Assert.True(a < 4.2);
        }

        [Fact(DisplayName = ">=")]
        public void GreaterEqual()
        {
            BigFraction a = new BigFraction(251.15);
            Assert.True(a >= 251.15);
            Assert.True(a >= 4.2);
        }

        [Fact(DisplayName = "<=")]
        public void SmallerEqual()
        {
            BigFraction a = new BigFraction(-251.15);
            Assert.True(a <= 4.2);
            Assert.True(a <= -251.15);
        }

        [Fact(DisplayName = "==")]
        public void Equal()
        {
            BigFraction a = new BigFraction(-251.15);
            Assert.True(a == -251.15);
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
            BigFraction a = new BigFraction(new BigInteger(1000), new BigInteger(100));
            a.Simplify();
            BigInteger expected = new BigInteger(10);
            Assert.Equal(expected,a);
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
    }
}