using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GryPlanszowe
{
    public abstract class Osoba
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public Osoba(string imie, string nazwisko)
        {
            Imie = imie;
            Nazwisko = nazwisko;
        }

        // Wirtualna metoda do wyświetlania informacji o osobie
        public virtual void WyswietlInformacje()
        {
            Console.WriteLine($"Imię: {Imie}, Nazwisko: {Nazwisko}");
        }
    }
}
