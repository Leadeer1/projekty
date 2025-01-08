namespace GryPlanszowe
{
    public class BrakEgzemplarzyException : Exception
    {
        public BrakEgzemplarzyException(string message) : base(message) { }
    }

    public class KlientNieznalezionyException : Exception
    {
        public KlientNieznalezionyException(string message) : base(message) { }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var system = new SystemWypozyczalni();
            TextGUI(system);
        }
        private static void TextGUI(SystemWypozyczalni system)
        {
            while (true)
            {
                Console.WriteLine("\nSystem Wypożyczalni Gier Planszowych");
                Console.WriteLine("1. Wyświetl gry");
                Console.WriteLine("2. Wyświetl klientów");
                Console.WriteLine("3. Wyświetl rezerwacje");
                Console.WriteLine("4. Dodaj nową grę");
                Console.WriteLine("5. Dodaj nową rezerwację");
                Console.WriteLine("6. Zapisz dane do pliku");
                Console.WriteLine("7. Wczytaj dane z pliku");
                Console.WriteLine("8. Wywołanie funkcji dodającej przykładowe dane");
                Console.WriteLine("0. Wyjdź");
                Console.Write("Wybierz opcję: ");

                var wybor = Console.ReadLine();

                try
                {
                    switch (wybor)
                    {
                        case "1":
                            system.WyswietlGry();
                            break;
                        case "2":
                            system.WyswietlKlientow();
                            break;
                        case "3":
                            system.WyswietlRezerwacje();
                            break;
                        case "4":
                            Console.Write("Podaj tytuł gry: ");
                            var tytul = Console.ReadLine();
                            Console.Write("Podaj gatunek gry: ");
                            var gatunek = Console.ReadLine();
                            Console.Write("Podaj liczbę egzemplarzy: ");
                            var liczbaEgzemplarzy = int.Parse(Console.ReadLine());
                            system.DodajGre(new Gra(tytul, gatunek, liczbaEgzemplarzy));
                            Console.WriteLine("Gra została dodana.");
                            break;
                        case "5":
                            Console.WriteLine("Wybierz klienta (podaj ID) lub wpisz 'nowy', aby dodać nowego klienta:");
                            system.WyswietlKlientow();
                            var input = Console.ReadLine();

                            Klient klient;
                            if (input.ToLower() == "nowy")
                            {
                                Console.Write("Podaj imię: ");
                                var imie = Console.ReadLine();
                                Console.Write("Podaj nazwisko: ");
                                var nazwisko = Console.ReadLine();
                                Console.Write("Podaj ID klienta: ");
                                var idKlienta = int.Parse(Console.ReadLine());
                                klient = new Klient(imie, nazwisko, idKlienta);
                                system.DodajKlienta(klient);
                            }
                            else
                            {
                                var id = int.Parse(input);
                                klient = system.ZnajdzKlienta(id);
                            }

                            Console.WriteLine("Wybierz grę (podaj tytuł):");
                            system.WyswietlGry();
                            var tytulGry = Console.ReadLine();
                            var gra = system.ZnajdzGre(tytulGry);

                            system.DodajRezerwacje(new Rezerwacja(gra, klient, DateTime.Now));
                            break;
                        case "6":
                            Console.Write("Podaj ścieżkę do zapisu danych: ");
                            var sciezkaZapisu = Console.ReadLine();
                            system.ZapiszDane(sciezkaZapisu);
                            break;
                        case "7":
                            Console.Write("Podaj ścieżkę do odczytu danych: ");
                            var sciezkaOdczytu = Console.ReadLine();
                            system.WczytajDane(sciezkaOdczytu);
                            break;
                        case "8":
                            TestGier(system);
                            break;
                        case "0":
                            Console.WriteLine("Zamykam program...");
                            return;
                        default:
                            Console.WriteLine("Nieprawidłowa opcja, spróbuj ponownie.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }
            }
        }

        private static void TestGier(SystemWypozyczalni system)
        {
            try
            {
                // Dodawanie gier
                system.DodajGre(new Gra("Catan", "Strategia", 3));
                system.DodajGre(new Gra("Dixit", "Imprezowa", 2));

                // Dodawanie klientów
                system.DodajKlienta(new Klient("Jan", "Kowalski", 1));
                system.DodajKlienta(new Klient("Anna", "Nowak", 2));

                // Tworzenie rezerwacji
                var gra = new Gra("Catan", "Strategia", 3);
                var klient = system.ZnajdzKlienta(1);
                system.DodajRezerwacje(new Rezerwacja(gra, klient, DateTime.Now));

                // Zapis danych
                system.ZapiszDane("dane.json");

                // Wczytanie danych
                system.WczytajDane("dane.json");
            }
            catch (BrakEgzemplarzyException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            catch (KlientNieznalezionyException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieoczekiwany błąd: {ex.Message}");
            }
        }

        
    }

    
}
