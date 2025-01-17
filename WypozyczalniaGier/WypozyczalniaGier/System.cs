using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WypozyczalniaGier
{
    internal class System
    {
        //samo przez sie - tworze listy obiektów
        private List<Gra> gry = new List<Gra>();
        private List<Klient> klienci = new List<Klient>();
        private List<Rezerwacja> rezerwacje = new List<Rezerwacja>();

        public List<Klient> Klienci { get => klienci; private set => klienci = value; }

        //chcieliśmy wyjątki, to mamy wyjątki - miały być w pliku program, ale tu będzie czytelniej, wiec bum:
        public class BrakEgzemplarzyException : Exception
        {
            public BrakEgzemplarzyException(string message) : base(message) {}  
        }

        public class ObiektNieznalezionyException : Exception
        {
            public ObiektNieznalezionyException(string message) : base(message) {}
        }

        public class NiedostepnaAkcjaException : Exception
        {
            public NiedostepnaAkcjaException(string message) : base(message) {} 
        }

        /* No i teraz tak - my chcemy jakieś akcje mieć w zwiążku z tym systemem:
         * 1. Dodaj grę (do bazy)
         * 2. Dodaj klienta (do bazy)
         * 3. Dodaj rezerwację (do bazy)
         * 4. Usuniecie rezerwacji = zwrot gry na podstawie ID rezerwacji
         * nie wywalamy z bazy tylko zmieniamy date i dostepnosc gry znów dodajemy
         * 5. Znajdź klienta - robi find
         * 6. Znajdz gre - jak wyżej
         * 7. Wyświetl gry - foreach każda gra w liscie: gra.ShowInfo()
         * 8. Wyświetl klientów - jak wyżej
         * 9. Wyświetl rezerwacje - jak wyżej
         * 10. Zapis danych do .json (możemy podać ścieżkę, jak nie to domyślnie nazwie go plik.json)
         * 11. Wczytanie dane z .json (jak wyżej, domyślnie pobierze z plik.json)
         */

        //1
        public void DodajGre(Gra gra)
        {
            gry.Add(gra);
        }

        //2
        public void DodajKlienta(Klient klient)
        {
            Klienci.Add(klient);
        }

        //3
        public void DodajRezerwacje(Rezerwacja rezerwacja)
        {
            if (rezerwacja.GraR.Dostepnosc > 0)
            {
                rezerwacje.Add(rezerwacja);
                rezerwacja.GraR.Dostepnosc--; //zmniejszam dostepne gry
                Console.WriteLine($"Rezerwacja nr {rezerwacja.IdRezerwacji} dodana.");
            }
            else
            {
                throw new BrakEgzemplarzyException("Brak dostępnych egzemplarzy tej gry.");
            }
        }

        //4
        public void ZwrocGre(int id)
        {
            if (rezerwacje[id].DataZ.HasValue)
            {
                throw new NiedostepnaAkcjaException("Próbujesz oddać oddaną grę.");
            }
            if (id > rezerwacje.Count)
            {
                throw new NiedostepnaAkcjaException("Próbujesz zakończyć nieistniejące zamówienie.");
            }
            rezerwacje[id].DataZ = DateTime.Now; //ustawiam date zwrotu na teraz
            rezerwacje[id].GraR.Dostepnosc++; //zwiekszam dostepnosc o sztuke
            //zwrot zaksiegowany, stan magazynowy na plus, rekord w tabeli pozostaje - sigmastycznie
        }

        //5
        public Klient ZnajdzKlienta(int idKlienta)
        {
            var klient = Klienci.Find(k => k.IdKlienta == idKlienta);
            if(klient == null)
            {
                throw new ObiektNieznalezionyException("Klient o podanym ID nie został znaleziony.");
            }
            return klient;
        }

        //6
        public Gra ZnajdzGre(string tytul) //szukanie gier po tytule, może wpadne na coś lepszego potem
        {
            var gra = gry.Find(g => g.Tytul.Equals(tytul, StringComparison.OrdinalIgnoreCase));
            if (gra == null)
            {
                throw new ObiektNieznalezionyException("Nie znaleziono gry o podanym tytule.");
            }
            return gra;
        }

        //7
        public void WyswietlGry()
        {
            Console.WriteLine("Dostępne gry:");
            foreach (var gra in gry)
            {
                gra.ShowInfo();
            }
        }

        //8
        public void WyswietlKlientow()
        {
            Console.WriteLine("Zarejestrowani klienci:");
            foreach (var klient in Klienci)
            {
                klient.ShowInfo();
            }
        }

        //9
        public void WyswietlRezerwacje()
        {
            Console.WriteLine("Lista rezerwacji:");
            foreach (var rezerwacja in rezerwacje)
            {
                rezerwacja.ShowInfo();
            }
        }

        //10 DŻEJSON MOMOA TO NAPRRRRRAWDE TY?
        public void ZapiszDane(string? sciezka)
        {
            //zabawa jest taka jak wczesniej opisalem -
            //jak jest pusty lub null to ustawiam domyslnie,
            //jak nie to jest śigmą i działa sam
            if (string.IsNullOrEmpty(sciezka))
            {
                sciezka = "plik.json";
            }
            var dane = new { Gry = gry, Klienci = Klienci, Rezerwacje = rezerwacje };
            File.WriteAllText(sciezka, JsonSerializer.Serialize(dane));
            Console.WriteLine("Dane zapisane do pliku.");
        }

        //11 TAK TO JEST DŻEJSON MOMOA CHODŹCIE WSZYSCY
        public void WczytajDane(string? sciezka)
        {
            if (string.IsNullOrEmpty(sciezka)) // to samo - sprawdza pustotę lub null
            {
                sciezka = "plik.json";
            }
            if (File.Exists(sciezka)) 
                //jesli wybrany json istnieje to czyta z niego dane
                //jak nie to skibidi ohio komunikat wychodzi na wierzch i sieje postrach
                //(nie istnieje plik do pobrania danych - zrobiłeś literówke cepie)
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
