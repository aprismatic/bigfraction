using System;
using System.Numerics;

{
    public struct BigFraction
    {
        //Paramaters Numerator / Denominator
        public BigInteger Numerator { get; private set; }
        public BigInteger Denominator { get; private set; }

        //CONSTRUCTORS

        //Fractional number constructor
        public BigFraction(BigInteger num, BigInteger den)
        {
            Numerator = num;
            Denominator = den;
        }

        //Decimal constructor
        public BigFraction(decimal dec)
        {
            int count = BitConverter.GetBytes(decimal.GetBits(dec)[3])[2];  //count decimal places
            Numerator = new BigInteger(dec * (Decimal)Math.Pow(10, count));
            Denominator = new BigInteger(Math.Pow(10, count));
        }

        //Double constructor
        public BigFraction(double dou)
        {
            decimal dec = (decimal)dou;
            int count = BitConverter.GetBytes(decimal.GetBits(dec)[3])[2];  //count decimal places
            Numerator = new BigInteger(dec * (Decimal)Math.Pow(10, count));
            Denominator = new BigInteger(Math.Pow(10, count));
        }

        //Integer constructor
        public BigFraction(int i)
        {
            Numerator = new BigInteger(i);
            Denominator = BigInteger.One;
        }

        //OPERATORS

        //User-defined conversion from BigInteger to BigFraction
        public static implicit operator BigFraction(BigInteger integer)
        {
            return new BigFraction(integer, BigInteger.One);
        }

        //User-defined conversion from Decimal to BigFraction
        public static implicit operator BigFraction(decimal dec)
        {
            return new BigFraction(dec);
        }

        //User-defined conversion from Double to BigFraction
        public static implicit operator BigFraction(double d)
        {
            return new BigFraction(d);
        }

        //User-defined conversion from Integer to BigFraction
        public static implicit operator BigFraction(int i)
        {
            return new BigFraction(i);
        }

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
            if (r1compare.CompareTo(r2compare) == -1) { return true; }
            else { return false; }
        }

        //Operator ==
        public static Boolean operator ==(BigFraction r1, BigFraction r2)
        {
            BigInteger r1compare = r1.Numerator * r2.Denominator;
            BigInteger r2compare = r2.Numerator * r1.Denominator;
            if (r1compare.CompareTo(r2compare) == 0) { return true; }
            else { return false; }
        }

        //Operator !=
        public static Boolean operator !=(BigFraction r1, BigFraction r2)
        {
            BigInteger r1compare = r1.Numerator * r2.Denominator;
            BigInteger r2compare = r2.Numerator * r1.Denominator;
            if (r1compare.CompareTo(r2compare) == 0) { return false; }
            else { return true; }
        }

        //Operator <=
        public static Boolean operator <=(BigFraction r1, BigFraction r2)
        {
            BigInteger r1compare = r1.Numerator * r2.Denominator;
            BigInteger r2compare = r2.Numerator * r1.Denominator;
            if (r1compare.CompareTo(r2compare) == -1 || r1compare.CompareTo(r2compare) == 0) { return true; }
            else { return false; }
        }

        //Operator >=
        public static Boolean operator >=(BigFraction r1, BigFraction r2)
        {
            BigInteger r1compare = r1.Numerator * r2.Denominator;
            BigInteger r2compare = r2.Numerator * r1.Denominator;
            if (r1compare.CompareTo(r2compare) == 1 || r1compare.CompareTo(r2compare) == 0) { return true; }
            else { return false; }
        }

        //Operator -
        public static BigFraction operator -(BigFraction a, BigFraction b)
        {
            a.Numerator = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
            a.Denominator = a.Denominator * b.Denominator;
            return a;
        }

        //Operator +
        public static BigFraction operator +(BigFraction a, BigFraction b)
        {
            a.Numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            a.Denominator = a.Denominator * b.Denominator;
            return a;
        }

        //Operator *
        public static BigFraction operator *(BigFraction a, BigFraction b)
        {
            a.Numerator = a.Numerator * b.Numerator;
            a.Denominator = a.Denominator * b.Denominator;
            return a;
        }

        //Operator /
        public static BigFraction operator /(BigFraction a, BigFraction b)
        {
            a.Numerator = a.Numerator * b.Denominator;
            a.Denominator = a.Denominator * b.Numerator;
            return a;
        }

        //Override Equals
        public override bool Equals(object obj)
        {
            if (obj == null) { return false; } 

            BigFraction comparebigfrac = (BigFraction)obj;
            if(Numerator == 0 && comparebigfrac.Numerator == 0) { return true; }    //If both values are zero

            return Numerator*comparebigfrac.Denominator == comparebigfrac.Numerator*Denominator;
        }

        //Override GetHashCode
        public override int GetHashCode()
        {
            return Numerator.GetHashCode() ^ Denominator.GetHashCode();
        }

        //Override ToString
        public override string ToString()
        {
            return Numerator.ToString() + "/" + Denominator.ToString();
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
        public BigInteger ToBigInteger()
        {
            return Numerator/Denominator;
        }
    }
}
