using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GryPlanszowe
{
    public class Rezerwacja
    {
        public Gra Gra { get; set; }
        public Klient Klient { get; set; }
        public DateTime DataRezerwacji { get; set; }

        public Rezerwacja(Gra gra, Klient klient, DateTime dataRezerwacji)
        {
            Gra = gra;
            Klient = klient;
            DataRezerwacji = dataRezerwacji;
        }

        public void WyswietlSzczegoly()
        {
            Console.WriteLine($"Rezerwacja: Gra - {Gra.Tytul}, Klient - {Klient.Imie} {Klient.Nazwisko}, Data: {DataRezerwacji.ToShortDateString()}");
        }
    }
}
