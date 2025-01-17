using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier
{
    public class Rezerwacja
    {
        //skróty R - rezerwacja, Z - zwrot, czyli:
        public Gra GraR { get; set; } //GraRezerwacja
        public Klient KlientR { get; set; } //KlientRezerwacja
        public DateTime DataR {  get; set; } //DataRezerwacji
        public DateTime? DataZ { get; set; } //DataZwrotu - może być nullem, bo jeszcze nie zwrócone cn
                                             //zrobione tylko po to, żeby odróżnić pola klasy Rezerwacja od nazw klas po prostu
        private static int nextId = 1; // identyczna sztuczka co przy Klient.cs, żeby dynamicznie nadawać id, przyda się potem do bazy danych
        public int IdRezerwacji { get; private set; }

        public Rezerwacja(Gra gra, Klient klient, DateTime dataR, DateTime? dataZ)
        {
            GraR = gra;
            KlientR = klient;
            DataR = dataR;
            DataZ = null;
            IdRezerwacji = nextId;
            nextId++;
        }

        public void ShowInfo()
        {
            Console.Write($"Rezerwacja - ID: {IdRezerwacji}, Gra - {GraR.Tytul}, Klient - {KlientR.Imie} {KlientR.Nazwisko}, Data Rezrwacji: {DataR.ToShortDateString()}");
            //z racji że DateTime? DataZ może być nullem - czyli dalej coś jest wypożyczone, to muszę sprawdzić czy ma value czy nie ma value żeby wypisać odpowiedni komunikat
            if (DataZ.HasValue)
            {
                DateTime dataZwrotu = DataZ.Value; // Dostęp do wartości
                Console.WriteLine($", Data Zwrotu - {dataZwrotu.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine();
            }
            
        }
    }
}
