using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GryPlanszowe
{
    public class SystemWypozyczalni
    {
        private List<Gra> gry = new List<Gra>();
        private List<Klient> klienci = new List<Klient>();
        private List<Rezerwacja> rezerwacje = new List<Rezerwacja>();

        public void DodajGre(Gra gra)
        {
            gry.Add(gra);
        }

        public void DodajKlienta(Klient klient)
        {
            klienci.Add(klient);
        }

        public void DodajRezerwacje(Rezerwacja rezerwacja)
        {
            if (rezerwacja.Gra.LiczbaEgzemplarzy > 0)
            {
                rezerwacje.Add(rezerwacja);
                rezerwacja.Gra.LiczbaEgzemplarzy--;
                Console.WriteLine("Rezerwacja została dodana.");
            }
            else
            {
                throw new BrakEgzemplarzyException("Brak dostępnych egzemplarzy tej gry.");
            }
        }

        public Klient ZnajdzKlienta(int idKlienta)
        {
            var klient = klienci.Find(k => k.IdKlienta == idKlienta);
            if (klient == null)
            {
                throw new KlientNieznalezionyException("Klient o podanym ID nie został znaleziony.");
            }
            return klient;
        }

        public Gra ZnajdzGre(string tytul)
        {
            var gra = gry.Find(g => g.Tytul.Equals(tytul, StringComparison.OrdinalIgnoreCase));
            if (gra == null)
            {
                throw new Exception("Nie znaleziono gry o podanym tytule.");
            }
            return gra;
        }

        public void WyswietlGry()
        {
            Console.WriteLine("Dostępne gry:");
            foreach (var gra in gry)
            {
                gra.WyswietlInformacje();
            }
        }

        public void WyswietlKlientow()
        {
            Console.WriteLine("Zarejestrowani klienci:");
            foreach (var klient in klienci)
            {
                klient.WyswietlInformacje();
            }
        }

        public void WyswietlRezerwacje()
        {
            Console.WriteLine("Lista rezerwacji:");
            foreach (var rezerwacja in rezerwacje)
            {
                rezerwacja.WyswietlSzczegoly();
            }
        }

        public void ZapiszDane(string sciezka)
        {
            var dane = new { Gry = gry, Klienci = klienci, Rezerwacje = rezerwacje };
            File.WriteAllText(sciezka, JsonSerializer.Serialize(dane));
            Console.WriteLine("Dane zapisane do pliku.");
        }

        public void WczytajDane(string sciezka)
        {
            if (File.Exists(sciezka))
            {
                var dane = JsonSerializer.Deserialize<dynamic>(File.ReadAllText(sciezka));
                Console.WriteLine("Dane wczytane z pliku.");
                // Deserializacja może być rozwinięta w kolejnych etapach
            }
            else
            {
                Console.WriteLine("Plik nie istnieje.");
            }
        }
    }

}
