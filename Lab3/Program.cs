using System;

namespace ComplexNumbersDemo
{
    // Zadanie 2: interfejs IModular
    public interface IModular
    {
        double Module();
    }

    // Zadania 1 i 3: klasa ComplexNumber
    public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular
    {
        // prywatne pola
        private double re;
        private double im;

        // publiczne właściwości
        public double Re
        {
            get => re;
            set => re = value;
        }

        public double Im
        {
            get => im;
            set => im = value;
        }

        // konstruktor
        public ComplexNumber(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        // ToString: zapis "a + bi" lub "a - bi"
        public override string ToString()
        {
            string sign = im >= 0 ? " + " : " - ";
            double absIm = Math.Abs(im);
            return $"{re}{sign}{absIm}i";
        }

        // operator + (dodawanie liczb zespolonych)
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Argument of + operator is null.");

            return new ComplexNumber(a.re + b.re, a.im + b.im);
        }

        // operator - (odejmowanie liczb zespolonych)
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Argument of - operator is null.");

            return new ComplexNumber(a.re - b.re, a.im - b.im);
        }

        // operator * (mnożenie liczb zespolonych)
        // (a+bi)(c+di) = (ac−bd) + (ad+bc)i
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Argument of * operator is null.");

            double real = a.re * b.re - a.im * b.im;
            double imag = a.re * b.im + a.im * b.re;
            return new ComplexNumber(real, imag);
        }

        // unarny operator - (sprzężenie liczby zespolonej): -(a+bi) = a - bi
        public static ComplexNumber operator -(ComplexNumber z)
        {
            if (z is null)
                throw new ArgumentNullException(nameof(z));

            return new ComplexNumber(z.re, -z.im);
        }

        // ICloneable
        public object Clone()
        {
            // płytka kopia jest wystarczająca (tylko typy proste)
            return new ComplexNumber(this.re, this.im);
        }

        // IModular
        // |Z| = sqrt(Re^2 + Im^2)
        public double Module()
        {
            return Math.Sqrt(re * re + im * im);
        }

        // IEquatable<ComplexNumber>
        public bool Equals(ComplexNumber other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return re == other.re && im == other.im;
        }

        // override Equals(object)
        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
                return Equals(other);

            return false;
        }

        // override GetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + re.GetHashCode();
                hash = hash * 23 + im.GetHashCode();
                return hash;
            }
        }

        // operator ==
        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        // operator !=
        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !(a == b);
        }
    }

    // Zadanie 4: klasa Program z Main
    public class Program
    {
        public static void Main(string[] args)
        {
            // tworzenie kilku liczb zespolonych
            ComplexNumber z1 = new ComplexNumber(6, 7);
            ComplexNumber z2 = new ComplexNumber(3, -2);
            ComplexNumber z3 = new ComplexNumber(-1.5, 4.2);

            Console.WriteLine("Liczby zespolone:");
            Console.WriteLine($"z1 = {z1}");
            Console.WriteLine($"z2 = {z2}");
            Console.WriteLine($"z3 = {z3}");
            Console.WriteLine();

            // testowanie operatorów +, -, *
            ComplexNumber sum = z1 + z2;
            ComplexNumber diff = z1 - z2;
            ComplexNumber prod = z1 * z2;

            Console.WriteLine("Operatory binarne:");
            Console.WriteLine($"z1 + z2 = {sum}");
            Console.WriteLine($"z1 - z2 = {diff}");
            Console.WriteLine($"z1 * z2 = {prod}");
            Console.WriteLine();

            // sprzężenie (unarne -)
            ComplexNumber conj = -z1;
            Console.WriteLine("Sprzężenie:");
            Console.WriteLine($"-z1 = {conj}");
            Console.WriteLine();

            // moduł (IModular)
            Console.WriteLine("Moduły liczb zespolonych:");
            Console.WriteLine($"|z1| = {z1.Module()}");
            Console.WriteLine($"|z2| = {z2.Module()}");
            Console.WriteLine($"|z3| = {z3.Module()}");
            Console.WriteLine();

            // klonowanie (ICloneable)
            ComplexNumber clone = (ComplexNumber)z1.Clone();
            Console.WriteLine("Klonowanie:");
            Console.WriteLine($"clone = {clone}");
            Console.WriteLine($"Referencje równe? {ReferenceEquals(z1, clone)}");
            Console.WriteLine();

            // porównywanie (Equals, ==, !=)
            ComplexNumber z1Copy = new ComplexNumber(6, 7);

            Console.WriteLine("Porównywanie:");
            Console.WriteLine($"z1.Equals(z1Copy) = {z1.Equals(z1Copy)}");
            Console.WriteLine($"z1 == z1Copy      = {z1 == z1Copy}");
            Console.WriteLine($"z1 != z2          = {z1 != z2}");
            Console.WriteLine();

            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }
    }
}