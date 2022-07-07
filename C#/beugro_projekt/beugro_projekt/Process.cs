using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace beugro_projekt
{
    public class Process
    {
        //SQL CONNECTION VARIABLES
        string server = "localhost";
        string database = "cs_beugro";
        string username = "root";
        string password = "admin";

        //Lists
        List<Products> products = new List<Products>();
        List<Production> productions = new List<Production>();
        List<int> realrandomhelper = new List<int>();

        public void ReadandFill()
        {
            //CONNECTION
            string constring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";
            MySqlConnection conn = new MySqlConnection(constring);
            conn.Open();
            //QUERY
            string query = "SELECT * FROM products";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            //READ
            MySqlDataReader reader = cmd.ExecuteReader();

            //FILLING PRODUCTS LIST
            while (reader.Read())
            {
                var filling = new Products((int)reader["id"], (string)reader["pcb"]);
                products.Add(filling);
            }
            conn.Close();
            //LIST FILLING TEST
            products.ForEach(i => Console.WriteLine("ID: {0} PCB: {1}",i.Id.ToString(),i.Pcb));
        }

        public void GenerateProductions()
        {

        }
    }
}
