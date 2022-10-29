using MySqlConnector;
using System;
using System.Collections.Generic;

namespace PeselValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Osoba> uzytkownicy = new List<Osoba>();

            string choose;
            do
            {
                Console.WriteLine("Baza osob!\n");
                Console.WriteLine("1. Lista uzytkownikow");
                Console.WriteLine("2. Dodaj uzytkownika");
                Console.WriteLine("0. Zakoncz program\n");
                Console.Write("Wybierz opcje: ");
                choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        Console.Clear();
                        Tabela.PrintLine();
                        Tabela.PrintRow("ID", "Imie", "Nazwisko", "PESEL", "Data Urodzenia");
                        Tabela.PrintLine();
                        MySqlDataReader rdr = MySQL.WyswietlDane("SELECT * FROM baza");
                        while (rdr.Read())
                        {
                            Tabela.PrintRow(rdr.GetInt32(0).ToString(), rdr.GetString(1),
                                    rdr.GetString(2), rdr.GetString(3), (Osoba.DataUrodzenia(rdr.GetString(3)).ToShortDateString()).ToString());
                        }
                        Tabela.PrintLine();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        Osoba.DodajUzytkownika();
                        Console.Clear();
                        break;
                    case "3":
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            } while (choose != "0");



        }
    }
}
