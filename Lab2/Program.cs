using System;

namespace Cwiczenie2
{
    abstract class Pracownik
    {
        public abstract void Pracuj();
    }

    class Piekarz : Pracownik
    {
        public override void Pracuj()
        {
            Console.WriteLine("Trwa pieczenie...");
        }
    }

    class Zwierze
    {
        protected string nazwa;

        public Zwierze(string nazwa)
        {
            this.nazwa = nazwa;
        }

        public virtual void daj_glos()
        {
            Console.WriteLine("...");
        }
    }

    class Pies : Zwierze
    {
        public Pies(string nazwa) : base(nazwa) { }

        public override void daj_glos()
        {
            Console.WriteLine($"{nazwa} robi woof woof!");
        }
    }

    class Kot : Zwierze
    {
        public Kot(string nazwa) : base(nazwa) { }

        public override void daj_glos()
        {
            Console.WriteLine($"{nazwa} robi miau miau!");
        }
    }

    class Waz : Zwierze
    {
        public Waz(string nazwa) : base(nazwa) { }

        public override void daj_glos()
        {
            Console.WriteLine($"{nazwa} robi ssssssss!");
        }
    }

    class A
    {
        public A()
        {
            Console.WriteLine("To jest konstruktor A");
        }
    }

    class B : A
    {
        public B() : base()
        {
            Console.WriteLine("To jest konstruktor B");
        }
    }

    class C : B
    {
        public C() : base()
        {
            Console.WriteLine("To jest konstruktor C");
        }
    }

    class Program
    {
        static void powiedz_cos(Zwierze z)
        {
            z.daj_glos();
        }

        public static void Main()
        {
            Zwierze z = new Zwierze("Zwierzak");
            Pies p = new Pies("Azor");
            Kot k = new Kot("Mruczek");
            Waz w = new Waz("Kaa");

            powiedz_cos(z);
            Console.WriteLine($"Typ obiektu przekazanego jako parametr: {z.GetType().Name}");
            powiedz_cos(p);
            Console.WriteLine($"Typ obiektu przekazanego jako parametr: {p.GetType().Name}");
            powiedz_cos(k);
            Console.WriteLine($"Typ obiektu przekazanego jako parametr: {k.GetType().Name}");
            powiedz_cos(w);
            Console.WriteLine($"Typ obiektu przekazanego jako parametr: {w.GetType().Name}");

            Piekarz piekarz = new Piekarz();
            piekarz.Pracuj();

            try
            {
                var obj = Activator.CreateInstance(typeof(Pracownik));
                Console.WriteLine($"Utworzono instancję Pracownik: {obj}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Próba utworzenia instancji klasy abstrakcyjnej Pracownik zakończona błędem: {ex.Message}");
            }

            A a = new A();
            B b = new B();
            C c = new C();
        }
    }
}