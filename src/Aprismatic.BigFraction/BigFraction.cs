using System;
using System.Numerics;

namespace Aprismatic
{
    public struct BigFraction
    {
        //Paramaters Numerator / Denominator
        public BigInteger Numerator { get; private set; }
        public BigInteger Denominator { get; private set; }

        private static readonly BigInteger MAX_DECIMAL = new BigInteger(decimal.MaxValue);
        private static readonly BigInteger MIN_DECIMAL = new BigInteger(decimal.MinValue);

        public static readonly BigFraction MinusOne = new BigFraction(BigInteger.MinusOne, BigInteger.One);
        public static readonly BigFraction Zero = new BigFraction(BigInteger.Zero, BigInteger.One);
        public static readonly BigFraction One = new BigFraction(BigInteger.One, BigInteger.One);

        // FIELDS
        public int Sign =>
            Numerator.IsZero
            ? 0
            : Numerator.Sign == Denominator.Sign
                ? 1
                : -1;

        public bool IsZero => Numerator.IsZero;

        public bool IsOne => Numerator == Denominator;


        //CONSTRUCTORS

        //Fractional constructor
        public BigFraction(BigInteger num, BigInteger den)
        {
            Numerator = num;
            Denominator = den;
        }

        public BigFraction(int num, int den)
        {
            Numerator = new BigInteger(num);
            Denominator = new BigInteger(den);
        }

        public BigFraction(long num, long den)
        {
            Numerator = new BigInteger(num);
            Denominator = new BigInteger(den);
        }

        //BigInteger constructor
        public BigFraction(BigInteger num)
        {
            Numerator = num;
            Denominator = BigInteger.One;
        }

        //Decimal constructor
        public BigFraction(decimal dec)
        {
            int count = BitConverter.GetBytes(decimal.GetBits(dec)[3])[2];  //count decimal places
            Numerator = new BigInteger(dec * (Decimal)Math.Pow(10, count));
            Denominator = new BigInteger(Math.Pow(10, count));
        }

        //Double constructor
        public BigFraction(double dou, double accuracy = 1e-15)
        {
            BigFraction f = FromDouble(dou, accuracy);
            Numerator = f.Numerator;
            Denominator = f.Denominator;
        }

        //Long constructor
        public BigFraction(long i)
        {
            Numerator = new BigInteger(i);
            Denominator = BigInteger.One;
        }

        //Integer constructor
        public BigFraction(int i)
        {
            Numerator = new BigInteger(i);
            Denominator = BigInteger.One;
        }

        //OPERATORS

        //User-defined conversion from BigInteger to BigFraction
        public static implicit operator BigFraction(BigInteger integer) => new BigFraction(integer);

        //User-defined conversion from Decimal to BigFraction
        public static implicit operator BigFraction(decimal dec) => new BigFraction(dec);

        //User-defined conversion from Double to BigFraction
        public static implicit operator BigFraction(double d) => new BigFraction(d);

        //User-defined conversion from Long to BigFraction
        public static implicit operator BigFraction(long l) => new BigFraction(l);

        //User-defined conversion from Integer to BigFraction
        public static implicit operator BigFraction(int i) => new BigFraction(i);

        //Unary minus
        public static BigFraction operator -(BigFraction value) => new BigFraction(-value.Numerator, value.Denominator);

        //Operator %
        public static BigFraction operator %(BigFraction r, BigInteger mod)
        {
            BigInteger modmulden = r.Denominator * mod;
            BigInteger remainder = r.Numerator % modmulden;
            BigFraction answer = new BigFraction(remainder, r.Denominator);
            return answer;
        }

        //Operator >
        public static Boolean operator >(BigFraction r1, BigFraction r2)
        {
            BigInteger r1compare = r1.Numerator * r2.Denominator;
            BigInteger r2compare = r2.Numerator * r1.Denominator;
            if (r1compare.CompareTo(r2compare) == 1) { return true; }
            else { return false; }
        }

        //Operator <
        public static Boolean operator <(BigFraction r1, BigFraction r2)
        {
            BigInteger r1compare = r1.Numerator * r2.Denominator;
            BigInteger r2compare = r2.Numerator * r1.Denominator;
            return r1compare.CompareTo(r2compare) == -1;
        }

        //Operator ==
        public static Boolean operator ==(BigFraction r1, BigFraction r2)
        {
            BigInteger r1compare = r1.Numerator * r2.Denominator;
            BigInteger r2compare = r2.Numerator * r1.Denominator;
            return r1compare.CompareTo(r2compare) == 0;
        }

        //Operator !=
        public static Boolean operator !=(BigFraction r1, BigFraction r2)
        {
            BigInteger r1compare = r1.Numerator * r2.Denominator;
            BigInteger r2compare = r2.Numerator * r1.Denominator;
            return !(r1compare.CompareTo(r2compare) == 0);
        }

        //Operator <=
        public static Boolean operator <=(BigFraction r1, BigFraction r2)
        {
            BigInteger r1compare = r1.Numerator * r2.Denominator;
            BigInteger r2compare = r2.Numerator * r1.Denominator;
            return r1compare.CompareTo(r2compare) == -1 || r1compare.CompareTo(r2compare) == 0;
        }

        //Operator >=
        public static Boolean operator >=(BigFraction r1, BigFraction r2)
        {
            BigInteger r1compare = r1.Numerator * r2.Denominator;
            BigInteger r2compare = r2.Numerator * r1.Denominator;
            return r1compare.CompareTo(r2compare) == 1 || r1compare.CompareTo(r2compare) == 0;
        }

        //Operator -
        public static BigFraction operator -(BigFraction a, BigFraction b)
        {
            return new BigFraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator,
                a.Denominator * b.Denominator);
        }

        //Operator +
        public static BigFraction operator +(BigFraction a, BigFraction b)
        {
            return new BigFraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator,
                a.Denominator * b.Denominator);
        }

        //Operator *
        public static BigFraction operator *(BigFraction a, BigFraction b)
        {
            return new BigFraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        //Operator /
        public static BigFraction operator /(BigFraction a, BigFraction b)
        {
            return new BigFraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        //Override Equals
        public override bool Equals(object obj)
        {
            if (!(obj is BigFraction comparebigfrac)) return false;

            if (IsZero && comparebigfrac.IsZero) return true;

            return Numerator * comparebigfrac.Denominator == comparebigfrac.Numerator * Denominator;
        }

        //Override GetHashCode
        public override int GetHashCode()
        {
            return Numerator.GetHashCode() / Denominator.GetHashCode();
        }

        //Override ToString
        public override string ToString()
        {
            return "(" + Numerator.ToString() + "/" + Denominator.ToString() + ")";
        }

        //MISC

        public void Simplify()
        {
            BigInteger quotient = Numerator / Denominator;  //Separate quotient from the number for faster calculation
            BigInteger remainder = Numerator % Denominator;
            BigInteger gcd = BigInteger.GreatestCommonDivisor(remainder, Denominator);
            remainder = remainder / gcd;

            Denominator = Denominator / gcd;
            Numerator = (quotient * Denominator) + remainder;
        }

        //NOTE: ALWAYS use this method when converting from BigFraction to BigInteger.
        public BigInteger ToBigInteger() => Numerator / Denominator;

        public float ToFloat() => (float)Numerator / (float)Denominator;

        public double ToDouble() => (double)Numerator / (double)Denominator;

        public decimal ToDecimal()
        {
            if (IsZero)
                return 0;

            if (Numerator <= MAX_DECIMAL && Numerator >= MIN_DECIMAL &&
                Denominator <= MAX_DECIMAL && Denominator >= MIN_DECIMAL)
                return (decimal)Numerator / (decimal)Denominator;

            var intPart = Numerator / Denominator;

            if (intPart != 0)
                return (decimal)intPart + (this - intPart).ToDecimal();

            var thisinverse = new BigFraction(Denominator, Numerator); // == 1 / this
            return 1 / thisinverse.ToDecimal();
        }

        //Conversion from double to fraction
        //Accuracy is used to convert recurring decimals into fractions (eg. 0.166667 -> 1/6)
        public static BigFraction FromDouble(double value, double accuracy)
        {
            if (accuracy <= 0.0 || accuracy >= 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(accuracy), "must be > 0 and < 1");
            }

            var sign = Math.Sign(value);
            var signbi =
                sign == -1 ? BigInteger.MinusOne :
                sign == 0 ? BigInteger.Zero :
                BigInteger.One;

            if (sign == -1)
                value = Math.Abs(value);

            // Accuracy is the maximum relative error; convert to absolute maxError
            var maxError = sign == 0 ? accuracy : value * accuracy;

            var n = new BigInteger(value);
            value -= Math.Floor(value);

            if (value < maxError)
                return new BigFraction(signbi * n, BigInteger.One);

            if (1 - maxError < value)
                return new BigFraction(signbi * (n + BigInteger.One), BigInteger.One);

            // The lower fraction is 0/1
            var lower_n = 0;
            var lower_d = 1;

            // The upper fraction is 1/1
            var upper_n = 1;
            var upper_d = 1;

            while (true)
            {
                // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
                var middle_n = lower_n + upper_n;
                var middle_d = lower_d + upper_d;

                if (middle_d * (value + maxError) < middle_n)
                {
                    // real + error < middle : middle is our new upper
                    upper_n = middle_n;
                    upper_d = middle_d;
                }
                else if (middle_n < (value - maxError) * middle_d)
                {
                    // middle < real - error : middle is our new lower
                    lower_n = middle_n;
                    lower_d = middle_d;
                }
                else
                {
                    // Middle is our best fraction
                    var middle_d_bi = new BigInteger(middle_d);
                    return new BigFraction((n * middle_d_bi + middle_n) * signbi, middle_d_bi);
                }
            }
        }
    }
}
