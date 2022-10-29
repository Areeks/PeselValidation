using System;
using System.Collections.Generic;
using System.Text;

namespace PeselValidation
{
    class Osoba
    {
        private string _imie, _nazwisko, _pesel;

        public string Imie
        {
            get
            {
                return _imie;
            }
            set
            { 
                    if (CheckNazwa(value))
                    {
                        _imie = char.ToUpper(value[0]) + value.Substring(1);
                    }
            }
        }
        public string Nazwisko
        {
            get
            {
                return _nazwisko;
            }
            set
            {
                if (CheckNazwa(value))
                {
                    _nazwisko = char.ToUpper(value[0]) + value.Substring(1);
                }
            }
        }
        public string Pesel
        {
            get
            {
                return _pesel;
            }
            set
            {
                if(sprawdzPesel(value))
                {
                    _pesel = value;
                }
            }
        }
        /// <summary>
        /// Dodaje uzytkownika
        /// </summary>
        /// <param name="user">Imie, Nazwisko, Pesel</param>
        /// <returns>Zwraca usera</returns>
        public static bool DodajUzytkownika ()
        {
            Osoba user = new Osoba();
            Console.Write("Podaj imie: ");
            user.Imie = Console.ReadLine();
            Console.Write("Podaj Nazwisko: ");
            user.Nazwisko = Console.ReadLine();
            Console.Write("Podaj PESEL: ");
            user.Pesel = Console.ReadLine();
            if (string.IsNullOrEmpty(user.Imie) || string.IsNullOrEmpty(user.Nazwisko) || string.IsNullOrEmpty(user.Pesel))
            {
                Console.WriteLine("Wprowadziłeś błędne dane!");
                Console.ReadKey();
                return false;
            }
            MySQL.DodajUzytkownika(user.Imie, user.Nazwisko, user.Pesel);
            Console.WriteLine("Użytkownik dodany pomyślnie!");
            Console.ReadKey();
            return true;
        }
        

        /// <summary>
        /// Funkcja sluzaca do wyswietlania uzytkownika
        /// </summary>
        /// <param name="user">Osoba</param>
        /// <returns>Osoba</returns>
        public Osoba WyswietlUzytkownika (Osoba user)
        {
            Console.Write("Imie: " + user.Imie);
            return user;
        }
        /// <summary>
        /// Sprawdza czy nazwa posiada tylko litery i czy jest dluzsza niz 3 znaki
        /// </summary>
        /// <param name="nazwa">String Nazwa</param>
        /// <returns>Zwraca true jesli poprawna nazwa i false jesli niepoprawna</returns>
        bool CheckNazwa(string nazwa)
        {
            if (!string.IsNullOrEmpty(nazwa) && nazwa.Length >= 3)
            {
                for (int i = 0; i < nazwa.Length; i++)
                {
                    if (!char.IsLetter(nazwa[i]))
                    {
                        return false;
                    }
                }
                return true;
            } 
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Sprawdza czy pesel jest poprawny wyliczajac sume kontrolna
        /// </summary>
        /// <param name="peseltext">Numer pesel jako string</param>
        /// <returns>Zwraca prawde gdy poprawny badz falsz gdy niepoprawny</returns>
       public bool sprawdzPesel (string peseltext)
        {
            if (peseltext.Length == 11)
            {
                char kontrolna = peseltext[10];
                int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
                int suma = 0;
                int wynikloop = 0;
                for (int i = 0; i < 10; i++)
                {
                    wynikloop = (int)(char.GetNumericValue(peseltext, i) * wagi[i]);
                    suma += wynikloop % 10;

                }
                suma = suma % 10;
                suma = 10 - suma;
                if ((int)char.GetNumericValue(kontrolna) == suma) return true;
                else return false;
            } 
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Na podstawie numeru pesel generuje date urodzenia
        /// </summary>
        /// <param name="peseltext">Pesel jako string</param>
        public static DateTime DataUrodzenia(string peseltext)
        {
                int kontrolna = (int)char.GetNumericValue(peseltext[2]);
                if ( kontrolna == 2 || kontrolna == 3)
                {
                    string syear, smonth, sday;
                    syear = "20" + peseltext[0] + peseltext[1];
                    smonth = "" + peseltext[2] + peseltext[3];
                    sday = "" + peseltext[4] + peseltext[5];
                DateTime dataUrodzenia = new DateTime(int.Parse(syear), int.Parse(smonth) - 20, int.Parse(sday));
                return dataUrodzenia;
            }
                else
                {
                    string syear, smonth, sday;
                    syear = "19" + peseltext[0] + peseltext[1];
                    smonth = "" + peseltext[2] + peseltext[3];
                    sday = "" + peseltext[4] + peseltext[5];

                    DateTime dataUrodzenia = new DateTime(int.Parse(syear), int.Parse(smonth), int.Parse(sday));
                return dataUrodzenia;
                }
        }
    }
}
