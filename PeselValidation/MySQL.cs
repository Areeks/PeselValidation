using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeselValidation
{
   public static class MySQL
    {
        static string cs = @"server=localhost;userid=root;password=;database=userdata";

        public static MySqlDataReader WyswietlDane(string query)
        {

            var con = new MySqlConnection(cs);
            con.Open();

            string sql = query;
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }
        public static void DodajUzytkownika(string imie, string nazwisko, string pesel)
        {
            var con = new MySqlConnection(cs);
            con.Open();

            var cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "INSERT INTO baza(imie, nazwisko, pesel) VALUES(\"" + imie + "\", \"" + nazwisko + "\", " + pesel + ");";
            cmd.ExecuteNonQuery();
        }
    }
}
