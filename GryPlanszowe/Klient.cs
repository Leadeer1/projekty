using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GryPlanszowe
{
    public class Klient : Osoba
    {
        public int IdKlienta { get; set; }

        public Klient(string imie, string nazwisko, int idKlienta) : base(imie, nazwisko)
        {
            IdKlienta = idKlienta;
        }

        public override void WyswietlInformacje()
        {
            Console.WriteLine($"Klient - ID: {IdKlienta}, Imię: {Imie}, Nazwisko: {Nazwisko}");
        }
    }
}
