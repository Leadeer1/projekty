using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier
{
    public class Klient : Osoba
    {
        private static int nextId = 1;
        public int liczbaWypozyczen = 1;

        public int IdKlienta { get; private set; }
        public bool IsActive { get; set; }
        

        public Klient(string imie, string nazwisko) : base(imie, nazwisko)
        {
            IsActive = true;
            IdKlienta = nextId;
            nextId++;
        }

        public override void ShowInfo()
        {
            string aktywnosc = IsActive ? "Tak" : "Nie";
            Console.Write($"Klient - ID: {IdKlienta}, Imię: {Imie}, Nazwisko: {Nazwisko}, Aktywne wypożyczenia: {aktywnosc}");
            if (IsActive) { Console.WriteLine($" ({liczbaWypozyczen}) "); } 
            //placeholder żeby tutaj wyświetlało potem liczbę tych wypożyczeń,
            //tylko najpierw system wypożyczalni trzeba zrobić xD
        }
    }
}
