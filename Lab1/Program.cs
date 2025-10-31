using System;

namespace Cwiczenie1
{
    class Program
    {
        public static void Main()
        {
            void WypiszTekst()
            {
                Console.WriteLine("To jest cwiczenie 1");
            }

            WypiszTekst();

            Zwierze a = new Zwierze();
            Zwierze b = new Zwierze("Filemon", "Kot", 4);
            Zwierze c = new Zwierze(b);

            a.daj_glos();
            b.daj_glos();
            c.daj_glos();

            Console.WriteLine($"Aktualna liczba zwierząt: {Zwierze.GetLiczbaZwierzat()}");
        }
    }

    class Zwierze
    {
        private string nazwa;
        private string gatunek;
        private int liczbaNog;
        private static int liczbaZwierzat = 0;

        public string GetNazwa()
        {
            return nazwa;
        }

        public string GetGatunek()
        {
            return gatunek;
        }

        public int GetLiczbaNog()
        {
            return liczbaNog;
        }

        public void SetNazwa(string nowaNazwa)
        {
            nazwa = nowaNazwa;
        }

        public Zwierze()
        {
            nazwa = "Rex";
            gatunek = "Pies";
            liczbaNog = 4;
            liczbaZwierzat++;
        }

        public Zwierze(string nazwa, string gatunek, int liczbaNog)
        {
            this.nazwa = nazwa;
            this.gatunek = gatunek;
            this.liczbaNog = liczbaNog;
            liczbaZwierzat++;
        }

        public Zwierze(Zwierze other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            this.nazwa = other.nazwa;
            this.gatunek = other.gatunek;
            this.liczbaNog = other.liczbaNog;
            liczbaZwierzat++;
        }

        public void daj_glos()
        {
            if (string.IsNullOrWhiteSpace(gatunek))
            {
                Console.WriteLine($"{nazwa} (nieznany gatunek): ...");
                return;
            }

            switch (gatunek.Trim().ToLowerInvariant())
            {
                case "kot":
                case "kotek":
                case "kotka":
                    Console.WriteLine($"{nazwa} ({gatunek}): Miau");
                    break;
                case "krowa":
                    Console.WriteLine($"{nazwa} ({gatunek}): Muuu!");
                    break;
                case "pies":
                case "piesek":
                case "suczka":
                    Console.WriteLine($"{nazwa} ({gatunek}): Hau!");
                    break;
                case "koń":
                case "kon":
                    Console.WriteLine($"{nazwa} ({gatunek}): Iiii-haaa!");
                    break;
                case "owca":
                    Console.WriteLine($"{nazwa} ({gatunek}): Béé");
                    break;
                case "kura":
                    Console.WriteLine($"{nazwa} ({gatunek}): Ko-ko-ryku!");
                    break;
                default:
                    Console.WriteLine($"{nazwa} ({gatunek}): (brak znanego odgłosu)");
                    break;
            }
        }

        public static int GetLiczbaZwierzat()
        {
            return liczbaZwierzat;
        }
    }
}