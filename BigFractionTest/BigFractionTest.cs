using System;
using Xunit;
using System.Numerics;
using Xunit.Abstractions;
using BigFractionExt;

namespace ConstructorTest
{
    public class ConstructorTest
    {
        private readonly ITestOutputHelper output;

        public ConstructorTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "Constructor for Integer")]
        public void IntConstructor()
        {
            BigFraction a = new BigFraction(-10);
            output.WriteLine(a.ToString());
        }

        [Fact(DisplayName = "Constructor for Double")]
        public void DouConstructor()
        {
            BigFraction a = new BigFraction(6545.99);
            output.WriteLine(a.ToString());
        }

        [Fact(DisplayName = "Constructor for Decimal")]
        public void DecConstructor()
        {
            BigFraction a = new BigFraction(new decimal(8984.65));
            output.WriteLine(a.ToString());
        }

        [Fact(DisplayName = "Constructor for BigInteger")]
        public void BigIntConstructor()
        {
            BigFraction a = new BigFraction(new BigInteger(33), new BigInteger(99));
            output.WriteLine(a.ToString());
        }
    }
}

namespace OpearatorTest
{
    public class BigFractionOperatorBigFraction
    {
        private readonly ITestOutputHelper output;

        public BigFractionOperatorBigFraction(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "Operator +")]
        public void Addition()
        {
            BigFraction a = new BigFraction(1000.25);
            BigFraction b = new BigFraction(1000.25);
            BigFraction c = new BigFraction(2000.5);
            BigFraction d = a + b;
            Assert.Equal(c, d);
        }

        [Fact(DisplayName = "Operator -")]
        public void Subtract()
        {
            BigFraction a = new BigFraction(2000.5);
            BigFraction b = new BigFraction(1000.25);
            BigFraction c = new BigFraction(1000.25);
            BigFraction d = a - b;
            Assert.Equal(c, d);
        }

        [Fact(DisplayName = "Operator *")]
        public void Mulplicate()
        {
            BigFraction a = new BigFraction(-5.25);
            BigFraction b = new BigFraction(10.1);
            BigFraction c = new BigFraction(-53.025);
            BigFraction d = a * b;
            Assert.Equal(c, d);
        }

        [Fact(DisplayName = "Operator /")]
        public void Divide()
        {
            BigFraction a = new BigFraction(5.25);
            BigFraction b = new BigFraction(1.25);
            BigFraction c = new BigFraction(4.20);
            BigFraction d = a / b;
            Assert.Equal(c, d);
        }

        [Fact(DisplayName = "Operator %")]
        public void Module()
        {
            BigFraction a = new BigFraction(5.25);
            BigInteger b = new BigInteger(5);
            BigFraction c = new BigFraction(0.25);
            BigFraction d = a % b;
            Assert.Equal(c, d);
        }

        [Fact(DisplayName = "Operator >")]
        public void Greater()
        {
            BigFraction a = new BigFraction(5.25);
            BigFraction b = new BigFraction(4.20);
            bool isGreater = a > b;
            Assert.True(isGreater);
        }

        [Fact(DisplayName = "Operator <")]
        public void Samller()
        {
            BigFraction a = new BigFraction(-251.15);
            BigFraction b = new BigFraction(4.20);
            bool isSmaller = a < b;
            Assert.True(isSmaller);
        }

        [Fact(DisplayName = "Operator >=")]
        public void GreaterEqual()
        {
            BigFraction a = new BigFraction(251.15);
            BigFraction b = new BigFraction(4.20);
            bool isGreater = a >= b;
            Assert.True(isGreater);
        }

        [Fact(DisplayName = "Operator <=")]
        public void SamllerEqual()
        {
            BigFraction a = new BigFraction(-251.15);
            BigFraction b = new BigFraction(4.20);
            bool isSmaller = a <= b;
            Assert.True(isSmaller);
        }

        [Fact(DisplayName = "Operator ==")]
        public void Equal()
        {
            BigFraction a = new BigFraction(-251.15);
            BigFraction b = new BigFraction(-251.15);
            bool isSmaller = a == b;
            Assert.True(isSmaller);
        }
    }

    public class BigFractionOperatorOthers
    {
        private readonly ITestOutputHelper output;

        public BigFractionOperatorOthers(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "Operator +")]
        public void Addition()
        {
            BigFraction a = new BigFraction(1000.25);
            BigFraction b = new BigFraction(1000.25);
            BigFraction c = new BigFraction(2000.5);
            BigFraction d = a + b;
            Assert.Equal(c, d);
        }

        [Fact(DisplayName = "Operator -")]
        public void Subtract()
        {
            BigFraction a = new BigFraction(2000.5);
            BigFraction b = new BigFraction(1000.25);
            BigFraction c = new BigFraction(1000.25);
            BigFraction d = a - b;
            Assert.Equal(c, d);
        }

        [Fact(DisplayName = "Operator *")]
        public void Mulplicate()
        {
            BigFraction a = new BigFraction(-5.25);
            BigFraction b = new BigFraction(10.1);
            BigFraction c = new BigFraction(-53.025);
            BigFraction d = a * b;
            Assert.Equal(c, d);
        }

        [Fact(DisplayName = "Operator /")]
        public void Divide()
        {
            BigFraction a = new BigFraction(5.25);
            BigFraction b = new BigFraction(1.25);
            BigFraction c = new BigFraction(4.20);
            BigFraction d = a / b;
            Assert.Equal(c, d);
        }

        [Fact(DisplayName = "Operator %")]
        public void Module()
        {
            BigFraction a = new BigFraction(5.25);
            BigInteger b = new BigInteger(5);
            BigFraction c = new BigFraction(0.25);
            BigFraction d = a % b;
            Assert.Equal(c, d);
        }

        [Fact(DisplayName = "Operator >")]
        public void Greater()
        {
            BigFraction a = new BigFraction(5.25);
            BigFraction b = new BigFraction(4.20);
            bool isGreater = a > b;
            Assert.True(isGreater);
        }

        [Fact(DisplayName = "Operator <")]
        public void Samller()
        {
            BigFraction a = new BigFraction(-251.15);
            BigFraction b = new BigFraction(4.20);
            bool isSmaller = a < b;
            Assert.True(isSmaller);
        }

        [Fact(DisplayName = "Operator >=")]
        public void GreaterEqual()
        {
            BigFraction a = new BigFraction(251.15);
            BigFraction b = new BigFraction(4.20);
            bool isGreater = a >= b;
            Assert.True(isGreater);
        }

        [Fact(DisplayName = "Operator <=")]
        public void SamllerEqual()
        {
            BigFraction a = new BigFraction(-251.15);
            BigFraction b = new BigFraction(4.20);
            bool isSmaller = a <= b;
            Assert.True(isSmaller);
        }

        [Fact(DisplayName = "Operator ==")]
        public void Equal()
        {
            BigFraction a = new BigFraction(-251.15);
            BigFraction b = new BigFraction(-251.15);
            bool isSmaller = a == b;
            Assert.True(isSmaller);
        }
    }
}
