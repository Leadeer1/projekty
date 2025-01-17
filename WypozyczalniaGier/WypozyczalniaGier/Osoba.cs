using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier
{
    public abstract class Osoba
    {
        string imie;
        string nazwisko;

        public string Imie { get => imie;  set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }

        public Osoba(string imie, string nazwisko)
        {
            Imie = imie;
            Nazwisko = nazwisko;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Imię: {Imie}, Nazwisko: {Nazwisko}");
        }
    }
}
