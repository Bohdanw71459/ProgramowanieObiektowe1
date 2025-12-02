using System;
using System.Collections.Generic;
using System.Linq;

namespace ComplexCollectionsDemo
{
    public interface IModular
    {
        double Module();
    }

   
    public class ComplexNumber : ICloneable,
                                 IEquatable<ComplexNumber>,
                                 IModular,
                                 IComparable<ComplexNumber>,
                                 IComparable
    {
        private double re;
        private double im;

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

        public ComplexNumber(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        public override string ToString()
        {
            string sign = im >= 0 ? "+" : "-";
            return $"{re} {sign} {Math.Abs(im)}i";
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.re + b.re, a.im + b.im);

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.re - b.re, a.im - b.im);

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.re * b.re - a.im * b.im, a.re * b.im + a.im * b.re);

        public static ComplexNumber operator -(ComplexNumber a)
            => new ComplexNumber(a.re, -a.im);

        public object Clone()
            => new ComplexNumber(re, im);

        public bool Equals(ComplexNumber other)
        {
            if (other == null) return false;
            return re == other.re && im == other.im;
        }

        public override bool Equals(object obj)
            => obj is ComplexNumber other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(re, im);

        public static bool operator ==(ComplexNumber a, ComplexNumber b)
            => a?.Equals(b) ?? b is null;

        public static bool operator !=(ComplexNumber a, ComplexNumber b)
            => !(a == b);

        public double Module()
            => Math.Sqrt(re * re + im * im);

        // --- IComparable / IComparable<ComplexNumber> ---

        // porównywanie po module liczby zespolonej
        public int CompareTo(ComplexNumber other)
        {
            if (other is null) return 1;
            return Module().CompareTo(other.Module());
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj is null) return 1;
            if (obj is ComplexNumber other)
                return CompareTo(other);

            throw new ArgumentException("Object is not a ComplexNumber", nameof(obj));
        }
    }

    public static class Program
    {
        private static void PrintCollection(IEnumerable<ComplexNumber> numbers, string title)
        {
            Console.WriteLine(title);
            foreach (var z in numbers)
            {
                Console.WriteLine($"  {z}  | |z| = {z.Module():F3}");
            }
            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            // Przydatne liczby z poprzednich zadań:
            ComplexNumber z1 = new ComplexNumber(6, 7);
            ComplexNumber z2 = new ComplexNumber(1, 2);
            ComplexNumber z3 = new ComplexNumber(6, 7);   // taka sama jak z1
            ComplexNumber z4 = new ComplexNumber(1, -2);
            ComplexNumber z5 = new ComplexNumber(-5, 9);

            // Zad 2 tablica

            ComplexNumber[] array = new[]
            {
                z1,
                z2,
                new ComplexNumber(3, -4),
                z4,
                z5
            };

            // 2a. Wypisz je wykorzystując pętlę foreach
            PrintCollection(array, "Zadanie 2a – tablica (foreach):");

            // 2b. Posortuj w oparciu o moduł liczby zespolonej i wypisz jeszcze raz
            Array.Sort(array); // używa IComparable<ComplexNumber>
            PrintCollection(array, "Zadanie 2b – tablica posortowana po module:");

            // 2c. Wypisz minimum i maksimum tablicy
            ComplexNumber minArray = array.Min(); // po module
            ComplexNumber maxArray = array.Max();

            Console.WriteLine("Zadanie 2c – minimum i maksimum tablicy:");
            Console.WriteLine($"  Min: {minArray}  | |z| = {minArray.Module():F3}");
            Console.WriteLine($"  Max: {maxArray}  | |z| = {maxArray.Module():F3}");
            Console.WriteLine();

            // 2d. Odfiltruj z tablicy liczby z ujemną częścią urojoną i wypisz jeszcze raz
            ComplexNumber[] filteredArray =
                array.Where(z => z.Im >= 0).ToArray();

            PrintCollection(filteredArray,
                "Zadanie 2d – tablica bez liczb z ujemną częścią urojoną:");

            // Zadanie 3 lista

            var list = new List<ComplexNumber>
            {
                z1,
                z2,
                new ComplexNumber(3, -4),
                z4,
                z5,
                new ComplexNumber(10, 0)
            };

            // Pokaż, że można wykonać te same operacje co na tablicy:

            PrintCollection(list, "Zadanie 3 – lista (stan początkowy):");

            // sortowanie listy po module
            list.Sort();
            PrintCollection(list, "Zadanie 3 – lista posortowana po module:");

            // minimum, maksimum
            ComplexNumber minList = list.Min();
            ComplexNumber maxList = list.Max();

            Console.WriteLine("Zadanie 3 – min i max listy:");
            Console.WriteLine($"  Min: {minList}  | |z| = {minList.Module():F3}");
            Console.WriteLine($"  Max: {maxList}  | |z| = {maxList.Module():F3}");
            Console.WriteLine();

            // filtrowanie – usunięcie elementów z ujemną częścią urojoną
            var filteredList = list.Where(z => z.Im >= 0).ToList();
            PrintCollection(filteredList,
                "Zadanie 3 – lista bez liczb z ujemną częścią urojoną:");

            // 3a. Usuń drugi element z listy i wypisz
            if (list.Count > 1)
            {
                list.RemoveAt(1); // indeks 1 = drugi element
            }
            PrintCollection(list, "Zadanie 3a – lista po usunięciu drugiego elementu:");

            // 3b. Usuń najmniejszy element z listy i wypisz
            if (list.Count > 0)
            {
                ComplexNumber minToRemove = list.Min();
                list.Remove(minToRemove);
            }
            PrintCollection(list, "Zadanie 3b – lista po usunięciu najmniejszego elementu:");

            // 3c. Usuń wszystkie elementy z listy i wypisz
            list.Clear();
            PrintCollection(list, "Zadanie 3c – lista po wyczyszczeniu:");

            // Zadanie 4 hashset

            var set = new HashSet<ComplexNumber>
            {
                z1, // 6 + 7i
                z2, // 1 + 2i
                z3, // 6 + 7i – duplikat z1, nie wejdzie do zbioru
                z4, // 1 - 2i
                z5  // -5 + 9i
            };

            // 4a. Sprawdź zawartość zbioru wypisując wszystkie wartości
            PrintCollection(set, "Zadanie 4a – zawartość zbioru HashSet:");

            // 4b. Sprawdź możliwość wykonania operacji:
            // minimum, maksimum, sortowanie, filtrowanie

            if (set.Count > 0)
            {
                ComplexNumber minSet = set.Min(); // po module
                ComplexNumber maxSet = set.Max();

                Console.WriteLine("Zadanie 4b – min i max dla HashSet:");
                Console.WriteLine($"  Min: {minSet}  | |z| = {minSet.Module():F3}");
                Console.WriteLine($"  Max: {maxSet}  | |z| = {maxSet.Module():F3}");
                Console.WriteLine();
            }

            var sortedSet = set.OrderBy(z => z); // używa IComparable
            PrintCollection(sortedSet, "Zadanie 4b – HashSet posortowany po module:");

            var filteredSet = set.Where(z => z.Im >= 0);
            PrintCollection(filteredSet,
                "Zadanie 4b – HashSet bez liczb z ujemną częścią urojoną:");

            // Zadanie 5 dictionary

            var dict = new Dictionary<string, ComplexNumber>
            {
                ["z1"] = z1,
                ["z2"] = z2,
                ["z3"] = z3,
                ["z4"] = z4,
                ["z5"] = z5
            };

            // 5a. Wypisz wszystkie elementy słownika w postaci (klucz, wartość)
            Console.WriteLine("Zadanie 5a – słownik (klucz, wartość):");
            foreach (var kv in dict)
            {
                Console.WriteLine($"  ({kv.Key}, {kv.Value})");
            }
            Console.WriteLine();

            // 5b. Wypisz osobno wszystkie klucze i wszystkie wartości
            Console.WriteLine("Zadanie 5b – wszystkie klucze:");
            foreach (var key in dict.Keys)
            {
                Console.WriteLine($"  {key}");
            }
            Console.WriteLine();

            Console.WriteLine("Zadanie 5b – wszystkie wartości:");
            foreach (var value in dict.Values)
            {
                Console.WriteLine($"  {value}");
            }
            Console.WriteLine();

            // 5c. Sprawdź, czy w słowniku istnieje element o kluczu z6
            Console.WriteLine("Zadanie 5c – czy istnieje klucz \"z6\"?");
            Console.WriteLine($"  ContainsKey(\"z6\") = {dict.ContainsKey("z6")}");
            Console.WriteLine();

            // 5d. Wykonaj na słowniku zadania 2c i 2d
            // 2c – min i max, 2d – filtracja po części urojonej < 0
            if (dict.Count > 0)
            {
                ComplexNumber minDictValue = dict.Values.Min();
                ComplexNumber maxDictValue = dict.Values.Max();

                Console.WriteLine("Zadanie 5d – min i max wartości w słowniku:");
                Console.WriteLine($"  Min: {minDictValue}  | |z| = {minDictValue.Module():F3}");
                Console.WriteLine($"  Max: {maxDictValue}  | |z| = {maxDictValue.Module():F3}");
                Console.WriteLine();
            }

            var dictFiltered = dict.Where(kv => kv.Value.Im >= 0);
            Console.WriteLine("Zadanie 5d – słownik bez wartości z ujemną częścią urojoną:");
            foreach (var kv in dictFiltered)
            {
                Console.WriteLine($"  ({kv.Key}, {kv.Value})");
            }
            Console.WriteLine();

            // 5e. Usuń ze słownika element o kluczu „z3”
            Console.WriteLine("Zadanie 5e – usuwanie elementu o kluczu \"z3\":");
            dict.Remove("z3");
            foreach (var kv in dict)
            {
                Console.WriteLine($"  ({kv.Key}, {kv.Value})");
            }
            Console.WriteLine();

            // 5f. Usuń drugi element ze słownika
            // (przyjmujemy drugi element w kolejności enumeracji)
            if (dict.Count > 1)
            {
                string secondKey = dict.ElementAt(1).Key;
                dict.Remove(secondKey);
            }

            Console.WriteLine("Zadanie 5f – słownik po usunięciu drugiego elementu:");
            foreach (var kv in dict)
            {
                Console.WriteLine($"  ({kv.Key}, {kv.Value})");
            }
            Console.WriteLine();

            // 5g. Wyczyść słownik
            dict.Clear();
            Console.WriteLine("Zadanie 5g – słownik po wyczyszczeniu:");
            Console.WriteLine($"  Count = {dict.Count}");
            Console.WriteLine();

            Console.WriteLine("Koniec demonstracji. Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }
    }
}