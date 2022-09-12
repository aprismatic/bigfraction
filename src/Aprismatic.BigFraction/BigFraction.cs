using System;
using System.Numerics;

namespace Aprismatic
{
    /// <summary>
    /// A struct that represents a a fraction (BigInteger / BigInteger).
    /// </summary>
    public readonly struct BigFraction
    {
        // Parameters Numerator / Denominator
        /// <summary>
        /// The numerator of the fraction.
        /// </summary>
        public readonly BigInteger Numerator;

        /// <summary>
        /// The denominator of the fraction.
        /// </summary>
        public readonly BigInteger Denominator;

        private static readonly BigInteger MAX_DECIMAL = new BigInteger(decimal.MaxValue);
        private static readonly BigInteger MIN_DECIMAL = new BigInteger(decimal.MinValue);

        /// <summary>
        /// Predefined constant of a BigFraction that represents –1.
        /// </summary>
        public static readonly BigFraction MinusOne = new BigFraction(BigInteger.MinusOne, BigInteger.One);

        /// <summary>
        /// Predefined constant of a BigFraction that represents 0.
        /// </summary>
        public static readonly BigFraction Zero = new BigFraction(BigInteger.Zero, BigInteger.One);

        /// <summary>
        /// Predefined constant of a BigFraction that represents 1.
        /// </summary>
        public static readonly BigFraction One = new BigFraction(BigInteger.One, BigInteger.One);

        // FIELDS
        /// <summary>
        /// Returns the sign of the fraction (-1, 0, or 1).
        /// </summary>
        public int Sign =>
            Numerator.IsZero                   ? 0 :
            Numerator.Sign == Denominator.Sign ? 1 :
                                                -1 ;

        /// <summary>
        /// Returns true if fraction equals to zero (i.e., its numerator is zero).
        /// </summary>
        public bool IsZero => Numerator.IsZero;

        /// <summary>
        /// Return true if fraction equals to one (i.e., its numerator equals its denominator).
        /// </summary>
        public bool IsOne => Numerator == Denominator;


        #region Constructors
        // CONSTRUCTORS

        // Fractional constructor
        /// <summary>
        /// Create a fraction from a numerator and a denominator
        /// </summary>
        /// <param name="num">Numerator</param>
        /// <param name="den">Denominator</param>
        public BigFraction(BigInteger num, BigInteger den)
        {
            Numerator = num;
            Denominator = den;
        }

        /// <summary>
        /// Create a fraction from a numerator and a denominator
        /// </summary>
        /// <param name="num">Numerator</param>
        /// <param name="den">Denominator</param>
        public BigFraction(int num, int den)
        {
            Numerator = new BigInteger(num);
            Denominator = new BigInteger(den);
        }

        /// <summary>
        /// Create a fraction from a numerator and a denominator
        /// </summary>
        /// <param name="num">Numerator</param>
        /// <param name="den">Denominator</param>
        public BigFraction(long num, long den)
        {
            Numerator = new BigInteger(num);
            Denominator = new BigInteger(den);
        }

        // BigInteger constructor
        /// <summary>
        /// Create a fraction from BigInteger (denominator is assumed to be 1)
        /// </summary>
        /// <param name="num">Numerator</param>
        public BigFraction(BigInteger num)
        {
            Numerator = num;
            Denominator = BigInteger.One;
        }

        // Decimal constructor
        /// <summary>
        /// Convert a decimal to a fraction with exact precision
        /// </summary>
        /// <param name="dec">Decimal to convert</param>
        public BigFraction(decimal dec)
        {
            int count = BitConverter.GetBytes(decimal.GetBits(dec)[3])[2];  //count decimal places
            Numerator = new BigInteger(dec * (Decimal)Math.Pow(10, count));
            Denominator = new BigInteger(Math.Pow(10, count));
        }

        // Double constructor
        /// <summary>
        /// Create a fraction from a double within a given precision
        /// </summary>
        /// <param name="dou">Double to convert to fraction</param>
        /// <param name="accuracy">Convert within this accuracy window</param>
        public BigFraction(double dou, double accuracy = 1e-15)
        {
            var f = FromDouble(dou, accuracy);
            Numerator = f.Numerator;
            Denominator = f.Denominator;
        }

        // Long constructor
        /// <summary>
        /// Convert long to fraction
        /// </summary>
        /// <param name="i">Long to convert</param>
        public BigFraction(long i)
        {
            Numerator = new BigInteger(i);
            Denominator = BigInteger.One;
        }

        // Integer constructor
        /// <summary>
        /// Convert int to fraction
        /// </summary>
        /// <param name="i">Int to convert</param>
        public BigFraction(int i)
        {
            Numerator = new BigInteger(i);
            Denominator = BigInteger.One;
        }
        #endregion

        #region Operators
        // OPERATORS

        // BigInteger to BigFraction
        /// <summary>
        /// Implicitly convert BigInteger to BigFraction
        /// </summary>
        /// <param name="integer">BigInteger to convert</param>
        public static implicit operator BigFraction(BigInteger integer) => new BigFraction(integer);

        // Decimal to BigFraction
        /// <summary>
        /// Implicitly convert decimal to BigFraction
        /// </summary>
        /// <param name="dec">Decimal to convert</param>
        public static implicit operator BigFraction(decimal dec) => new BigFraction(dec);

        // Double to BigFraction
        /// <summary>
        /// Implicitly convert double to BigFraction
        /// </summary>
        /// <param name="d">Double to convert</param>
        public static implicit operator BigFraction(double d) => new BigFraction(d);

        // Long to BigFraction
        /// <summary>
        /// Implicitly convert long to BigFraction
        /// </summary>
        /// <param name="l">Long to convert</param>
        public static implicit operator BigFraction(long l) => new BigFraction(l);

        // Integer to BigFraction
        /// <summary>
        /// Implicitly convert int to BigFraction
        /// </summary>
        /// <param name="i">Int to convert</param>
        public static implicit operator BigFraction(int i) => new BigFraction(i);

        // Unary minus
        /// <summary>
        /// Negate a fraction
        /// </summary>
        /// <param name="value">Fraction to negate</param>
        public static BigFraction operator -(BigFraction value) => new BigFraction(-value.Numerator, value.Denominator);

        /// <summary>
        /// Operator "greater than"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator >(BigFraction r1, BigFraction r2)
        {
            var r1compare = r1.Numerator * r2.Denominator;
            var r2compare = r2.Numerator * r1.Denominator;
            var res = r1compare.CompareTo(r2compare);
            return r1.Denominator.Sign == r2.Denominator.Sign ? res > 0 : res < 0;
        }

        /// <summary>
        /// Operator "greater than"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator >(BigFraction r1, BigInteger r2)
        {
            var r1compare = r1.Numerator;
            var r2compare = r2 * r1.Denominator;
            var res = r1compare.CompareTo(r2compare);
            return r1.Denominator.Sign == 1 ? res > 0 : res < 0;
        }

        /// <summary>
        /// Operator "greater than"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator >(BigInteger r1, BigFraction r2)
        {
            var r1compare = r1 * r2.Denominator;
            var r2compare = r2.Numerator;
            var res = r1compare.CompareTo(r2compare);
            return r2.Denominator.Sign == 1 ? res > 0 : res < 0;
        }

        // Operator <
        /// <summary>
        /// Operator "less than"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator <(BigFraction r1, BigFraction r2)
        {
            var r1compare = r1.Numerator * r2.Denominator;
            var r2compare = r2.Numerator * r1.Denominator;
            var res = r1compare.CompareTo(r2compare);
            return r1.Denominator.Sign == r2.Denominator.Sign ? res < 0 : res > 0;
        }

        /// <summary>
        /// Operator "less than"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator <(BigFraction r1, BigInteger r2)
        {
            var r1compare = r1.Numerator;
            var r2compare = r2 * r1.Denominator;
            var res = r1compare.CompareTo(r2compare);
            return r1.Denominator.Sign == 1 ? res < 0 : res > 0;
        }

        /// <summary>
        /// Operator "less than"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator <(BigInteger r1, BigFraction r2)
        {
            var r1compare = r1 * r2.Denominator;
            var r2compare = r2.Numerator;
            var res = r1compare.CompareTo(r2compare);
            return r2.Denominator.Sign == 1 ? res < 0 : res > 0;
        }

        // Operator ==
        /// <summary>
        /// Operator "equals"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator ==(BigFraction r1, BigFraction r2)
        {
            var r1compare = r1.Numerator * r2.Denominator;
            var r2compare = r2.Numerator * r1.Denominator;
            return r1compare.CompareTo(r2compare) == 0;
        }

        /// <summary>
        /// Operator "equals"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator ==(BigFraction r1, BigInteger r2)
        {
            var r1compare = r1.Numerator;
            var r2compare = r2 * r1.Denominator;
            return r1compare.CompareTo(r2compare) == 0;
        }

        /// <summary>
        /// Operator "equals"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator ==(BigInteger r1, BigFraction r2)
        {
            var r1compare = r1 * r2.Denominator;
            var r2compare = r2.Numerator;
            return r1compare.CompareTo(r2compare) == 0;
        }

        // Operator !=
        /// <summary>
        /// Operator "not equals"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator !=(BigFraction r1, BigFraction r2)
        {
            var r1compare = r1.Numerator * r2.Denominator;
            var r2compare = r2.Numerator * r1.Denominator;
            return r1compare.CompareTo(r2compare) != 0;
        }

        /// <summary>
        /// Operator "not equals"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator !=(BigFraction r1, BigInteger r2)
        {
            var r1compare = r1.Numerator;
            var r2compare = r2 * r1.Denominator;
            return r1compare.CompareTo(r2compare) != 0;
        }

        /// <summary>
        /// Operator "not equals"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator !=(BigInteger r1, BigFraction r2)
        {
            var r1compare = r1 * r2.Denominator;
            var r2compare = r2.Numerator;
            return r1compare.CompareTo(r2compare) != 0;
        }

        // Operator <=
        /// <summary>
        /// Operator "less than or equals to"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator <=(BigFraction r1, BigFraction r2)
        {
            var r1compare = r1.Numerator * r2.Denominator;
            var r2compare = r2.Numerator * r1.Denominator;
            var res = r1compare.CompareTo(r2compare);
            return r1.Denominator.Sign == r2.Denominator.Sign ? res <= 0 : res >= 0;
        }

        /// <summary>
        /// Operator "less than or equals to"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator <=(BigFraction r1, BigInteger r2)
        {
            var r1compare = r1.Numerator;
            var r2compare = r2 * r1.Denominator;
            var res = r1compare.CompareTo(r2compare);
            return r1.Denominator.Sign == 1 ? res <= 0 : res >= 0;
        }

        /// <summary>
        /// Operator "less than or equals to"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator <=(BigInteger r1, BigFraction r2)
        {
            var r1compare = r1 * r2.Denominator;
            var r2compare = r2.Numerator;
            var res = r1compare.CompareTo(r2compare);
            return r2.Denominator.Sign == 1 ? res <= 0 : res >= 0;
        }

        // Operator >=
        /// <summary>
        /// Operator "greater than or equals to"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator >=(BigFraction r1, BigFraction r2)
        {
            var r1compare = r1.Numerator * r2.Denominator;
            var r2compare = r2.Numerator * r1.Denominator;
            var res = r1compare.CompareTo(r2compare);
            return r1.Denominator.Sign == r2.Denominator.Sign ? res >= 0 : res <= 0;
        }

        /// <summary>
        /// Operator "greater than or equals to"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator >=(BigFraction r1, BigInteger r2)
        {
            var r1compare = r1.Numerator;
            var r2compare = r2 * r1.Denominator;
            var res = r1compare.CompareTo(r2compare);
            return r1.Denominator.Sign == 1 ? res >= 0 : res <= 0;
        }

        /// <summary>
        /// Operator "greater than or equals to"
        /// </summary>
        /// <param name="r1">First operand</param>
        /// <param name="r2">Second operand</param>
        public static bool operator >=(BigInteger r1, BigFraction r2)
        {
            var r1compare = r1 * r2.Denominator;
            var r2compare = r2.Numerator;
            var res = r1compare.CompareTo(r2compare);
            return r2.Denominator.Sign == 1 ? res >= 0 : res <= 0;
        }

        // Operator -
        /// <summary>
        /// Operator "minus"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator -(BigFraction a, BigFraction b) =>
            new BigFraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator,
                a.Denominator * b.Denominator);

        /// <summary>
        /// Operator "minus"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator -(BigFraction a, BigInteger b) =>
            new BigFraction(a.Numerator - b * a.Denominator,
                a.Denominator);

        /// <summary>
        /// Operator "minus"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator -(BigInteger a, BigFraction b) =>
            new BigFraction(a * b.Denominator - b.Numerator,
                b.Denominator);

        // Operator +
        /// <summary>
        /// Operator "plus"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator +(BigFraction a, BigFraction b) =>
            new BigFraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator,
                a.Denominator * b.Denominator);

        /// <summary>
        /// Operator "plus"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator +(BigFraction a, BigInteger b) =>
            new BigFraction(a.Numerator + b * a.Denominator,
                a.Denominator);

        /// <summary>
        /// Operator "plus"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator +(BigInteger a, BigFraction b) =>
            new BigFraction(a * b.Denominator + b.Numerator,
                b.Denominator);

        // Operator *
        /// <summary>
        /// Operator "multiply"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator *(BigFraction a, BigFraction b) => new BigFraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

        /// <summary>
        /// Operator "multiply"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator *(BigFraction a, BigInteger b) => new BigFraction(a.Numerator * b, a.Denominator);

        /// <summary>
        /// Operator "multiply"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator *(BigInteger a, BigFraction b) => new BigFraction(a * b.Numerator, b.Denominator);

        // Operator /
        /// <summary>
        /// Operator "divide"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator /(BigFraction a, BigFraction b) => new BigFraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);

        /// <summary>
        /// Operator "divide"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator /(BigFraction a, BigInteger b) => new BigFraction(a.Numerator, a.Denominator * b);

        /// <summary>
        /// Operator "divide"
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        public static BigFraction operator /(BigInteger a, BigFraction b) => new BigFraction(a * b.Denominator, b.Numerator);
        #endregion

        // Override Equals
        /// <summary>
        /// Deep check for equality of two BigFraction objects
        /// </summary>
        /// <param name="obj">Object to compare to</param>
        public override bool Equals(object obj)
        {
            if (!(obj is BigFraction comparebigfrac)) return false;

            if (IsZero && comparebigfrac.IsZero) return true;

            return Numerator * comparebigfrac.Denominator == comparebigfrac.Numerator * Denominator;
        }

        // Override GetHashCode
        /// <summary>
        /// Override GetHashCode
        /// </summary>
        public override int GetHashCode() // TODO: this will produce the same hash code for all fractions between 0 and 1 which might be undesirable
        {
            var denhc = Denominator.GetHashCode();
            if (denhc == 0)
                denhc = 1;
            var numhc = Numerator.GetHashCode();
            return numhc / denhc;
        }

        // Override ToString
        /// <summary>
        /// Convert fraction to a string representation
        /// </summary>
        public override string ToString() => "(" + Numerator.ToString() + "/" + Denominator.ToString() + ")";

        // MISC

        /// <summary>
        /// Returns a BigFraction that is equal to <c>this</c> but with numerator and denominator divided by their GCD
        /// </summary>
        public BigFraction Simplify()
        {
            var quotient = Numerator / Denominator; // Separate the quotient from the number for faster calculation
            var quot_x_denom = quotient * Denominator;
            var remainder = Numerator - quot_x_denom;
            var gcd = BigInteger.GreatestCommonDivisor(remainder, Denominator);
            remainder /= gcd;

            var newDenominator = Denominator / gcd;
            var newNumerator = quotient * newDenominator + remainder;
            return new BigFraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Converts a BigFraction to a BigInteger (truncates the fractional part).
        /// NOTE: ALWAYS use this method when converting from BigFraction to BigInteger.
        /// </summary>
        public BigInteger ToBigInteger() => Numerator / Denominator;

        // TODO: improve this to handle large numerators and denominators
        /// <summary>
        /// Converts a BigFraction to a float
        /// </summary>
        public float ToFloat() => (float)Numerator / (float)Denominator;

        // TODO: improve this to handle large numerators and denominators
        /// <summary>
        /// Convert a BigFraction to a double
        /// </summary>
        public double ToDouble() => (double)Numerator / (double)Denominator;

        /// <summary>
        /// Convert a BigFraction to a decimal
        /// </summary>
        public decimal ToDecimal()
        {
            if (IsZero)
                return 0m;

            if (Numerator <= MAX_DECIMAL && Numerator >= MIN_DECIMAL &&
                Denominator <= MAX_DECIMAL && Denominator >= MIN_DECIMAL)
                return (decimal)Numerator / (decimal)Denominator;

            var quotient = Numerator / Denominator;

            if (quotient != 0)
                return (decimal)quotient + (this - quotient).ToDecimal();

            var thisInverse = new BigFraction(Denominator, Numerator); // == 1 / this
            return 1m / thisInverse.ToDecimal();
        }

        /// <summary>
        /// Conversion from double to fraction
        /// </summary>
        /// <param name="value">Double to convert</param>
        /// <param name="accuracy">Accuracy is used to convert recurring decimals into fractions (eg. 0.166667 -> 1/6)</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if accuracy ≤ 0 or accuracy ≥ 1</exception>
        public static BigFraction FromDouble(double value, double accuracy)
        {
            if (accuracy <= 0.0 || accuracy >= 1.0)
                throw new ArgumentOutOfRangeException(nameof(accuracy), "must be > 0 and < 1");

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

        public static BigFraction Abs(BigFraction value) => value < 0 ? -value : value;
    }
}
