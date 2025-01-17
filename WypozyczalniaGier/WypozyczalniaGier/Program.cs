using static WypozyczalniaGier.System;

namespace WypozyczalniaGier
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var system = new System();
            TextGUI(system);
        }
        private static void TextGUI(System system)
        {
            while (true)
            {
                Console.WriteLine("\nSystem Wypożyczalni Gier Planszowych");
                Console.WriteLine("1. Wyświetl gry"); //7
                Console.WriteLine("2. Wyświetl klientów"); //8
                Console.WriteLine("3. Wyświetl rezerwacje"); //9
                Console.WriteLine("4. Dodaj nową grę"); //1
                Console.WriteLine("5. Dodaj nową rezerwację"); //3 + 2
                Console.WriteLine("5. Zwróć grę (zakończ rezerwację)"); //4
                Console.WriteLine("6. Zapisz dane do pliku"); //10
                Console.WriteLine("7. Wczytaj dane z pliku"); //11
                Console.WriteLine("8. Wywołanie funkcji dodającej przykładowe dane"); //moja inwencja twórcza xD, na czymś musze testować kod
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
                                //Console.Write("Podaj ID klienta: ");
                                //var idKlienta = int.Parse(Console.ReadLine());
                                //klient = new Klient(imie, nazwisko, idKlienta);
                                klient = new Klient(imie, nazwisko);
                                system.DodajKlienta(klient);
                            }
                            else
                            {
                                /*var id = int.Parse(input);
                                int id_i = system.Klienci[system.Klienci.Count].IdKlienta;
                                klient = system.ZnajdzKlienta(id_i);*/
                                var id = int.Parse(input);
                                klient = system.ZnajdzKlienta(id);

                                if (klient == null)
                                {
                                    Console.WriteLine($"Nie znaleziono klienta o ID: {id}");
                                    break; // lub obsłuż to inaczej
                                }
                            }

                            Console.WriteLine("Wybierz grę (podaj tytuł):");
                            system.WyswietlGry();
                            var tytulGry = Console.ReadLine();
                            var gra = system.ZnajdzGre(tytulGry);

                            system.DodajRezerwacje(new Rezerwacja(gra, klient, DateTime.Now, null));
                            gra.Dostepnosc--;
                            klient.liczbaWypozyczen++;
                            break;
                        case "6":
                            Console.Write("Podaj ścieżkę do zapisu danych: ");
                            string sciezkaZapisu = Console.ReadLine();
                            if(sciezkaZapisu == "") { sciezkaZapisu = "plik.json"; }
                            system.ZapiszDane(sciezkaZapisu);
                            break;
                        case "7":
                            Console.Write("Podaj ścieżkę do odczytu danych: ");
                            string sciezkaOdczytu = Console.ReadLine();
                            if (sciezkaOdczytu == "") { sciezkaOdczytu = "plik.json"; }
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

        private static void TestGier(System system)
        {
            try
            {
                // Dodawanie gier
                system.DodajGre(new Gra("Catan", "Strategia", 3));
                system.DodajGre(new Gra("Dixit", "Imprezowa", 2));

                // Dodawanie klientów
                system.DodajKlienta(new Klient("Jan", "Kowalski"));
                system.DodajKlienta(new Klient("Anna", "Nowak"));

                // Tworzenie rezerwacji
                var gra = new Gra("Catan", "Strategia", 3);
                var klient = system.ZnajdzKlienta(1);
                system.DodajRezerwacje(new Rezerwacja(gra, klient, DateTime.Now, null));

                // Zapis danych
                system.ZapiszDane("dane.json");

                // Wczytanie danych
                system.WczytajDane("dane.json");
            }
            catch (BrakEgzemplarzyException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            catch (ObiektNieznalezionyException ex)
            {
                Console.WriteLine($"Błąd: Nie ma takiego klienta");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieoczekiwany błąd: {ex.Message}");
            }
        }


    }

}
