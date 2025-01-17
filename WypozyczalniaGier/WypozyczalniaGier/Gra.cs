using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier
{
    public class Gra
    {
        string tytul;
        string gatunek;
        int dostepnosc;

        public string Tytul { get => tytul; set => tytul = value; }
        public string Gatunek { get => gatunek; set => gatunek = value; }
        public int Dostepnosc { get => dostepnosc; set => dostepnosc = value; }

        public Gra(string tytul, string gatunek, int liczba)
        {
            Tytul = tytul;
            Gatunek = gatunek;
            Dostepnosc = liczba;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Gra: {Tytul}, Gatunek: {Gatunek}, Dostepne egzemplarze {Dostepnosc}");
        }
    }
}
