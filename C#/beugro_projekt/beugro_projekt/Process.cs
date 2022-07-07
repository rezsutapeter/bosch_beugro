﻿using System;
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
            products.ForEach(i => Console.WriteLine("ID: {0} PCB: {1}",i.Id,i.Pcb));
        }

        public void GenerateProductions()
        {
            Random r = new Random();
            Random r2 = new Random();
            Random t1 = new Random();
            Random t2 = new Random();
            int index;
            int count = 0;
            //I EQUALS 10 to generate exactly 10
            for (int i = 0; i < 10; i++)
            {
                    index = r.Next(1, 52);
                    if (!realrandomhelper.Contains(index))
                    {
                        realrandomhelper.Add(index);
                        DateTime dt1 = DateTime.Now.AddMinutes(t1.Next(-20, -1));
                        DateTime dt2 = DateTime.Now.AddMinutes(t1.Next(-15, -1));
                        DateTime fix;

                        //IF THE START DAE IS LATER THAN END DATE IT SWAPS THEM
                        if (dt1<dt2)
                        {
                            var maindata = new Production(products[index].Id, r2.Next(1, 1000), dt1, dt2);
                            productions.Add(maindata);
                        }
                        else
                        {
                            fix = dt1;
                            dt1 = dt2;
                            dt2 = fix;
                            var maindata = new Production(products[index].Id, r2.Next(1, 1000), dt1, dt2);
                            productions.Add(maindata);
                        }
                    }
                    else
                    {
                        i--;
                    }
            }
            //GENERATED DATAS TEST
            Console.WriteLine("\nGenerated Datas: \n");
            productions.ForEach(i => Console.WriteLine("pcb_id: {0}\nquantity: {1}\nstartDate: {2}\nendDate: {3}\n", i.Pcb_id, i.Quantity, i.startDate,i.endDate));
        }
    }
}
