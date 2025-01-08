using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GryPlanszowe
{
    public class Gra
    {
        public string Tytul { get; set; }
        public string Gatunek { get; set; }
        public int LiczbaEgzemplarzy { get; set; }

        public Gra(string tytul, string gatunek, int liczbaEgzemplarzy)
        {
            Tytul = tytul;
            Gatunek = gatunek;
            LiczbaEgzemplarzy = liczbaEgzemplarzy;
        }

        public void WyswietlInformacje()
        {
            Console.WriteLine($"Gra: {Tytul}, Gatunek: {Gatunek}, Dostępne egzemplarze: {LiczbaEgzemplarzy}");
        }
    }
}
