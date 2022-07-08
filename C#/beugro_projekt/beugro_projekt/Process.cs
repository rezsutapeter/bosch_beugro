using System;
using System.Collections.Generic;
using System.IO;
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
        string password = "";

        //Lists
        List<Products> products = new List<Products>();
        List<Production> productions = new List<Production>();
        List<int> realrandomhelper = new List<int>();
        List<string> removelist = new List<string>();

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
            //I EQUALS 10 to generate exactly 10
            for (int i = 0; i < 10; i++)
            {
                    index = r.Next(1, 52);
                    //IF "index" is already in the list the for loops back and generates another index.
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

        public void CreatePuffer()
        {
            using (StreamWriter opfile = new StreamWriter("Puffer.txt"))
            {
                //Filling puffer.txt with the datas seperated by pipelines
                for (int i = 0; i < productions.Count; i++)
                {
                    opfile.WriteLine(productions[i].Pcb_id.ToString() + "|" + productions[i].Quantity.ToString() + "|" + productions[i].startDate.ToString() + "|" + productions[i].endDate.ToString());
                }
            }
        }

        public void ReadPuffer()
        {
            removelist = File.ReadAllLines("Puffer.txt").ToList();
            //4th object removal
            removelist.RemoveAt(3);
            string s = "";
            //A substring investigates the list to delete ID 4 if it exists.
            for (int i = 0; i < removelist.Count; i++)
            {
                s = removelist[i].Substring(0, 2);
                if (s=="4|")
                {
                    removelist.RemoveAt(i);
                }
            }
            Console.WriteLine("\nDatas read from Puffer.txt (The 4th object is deleted and ID:4 no longer contained)\n");
            for (int i = 0; i < removelist.Count; i++)
            {
                Console.WriteLine(removelist[i]);
            }
        }

        public void DecryptData()
        {
            //temporary array to fill back datas to productions
            string[] str = new string[removelist.Count];
            int rmlast = 0;
            for (int i = 0; i < removelist.Count; i++)
            {
                str = removelist[i].Split('|');
                productions[i].Pcb_id = int.Parse(str[0]);
                productions[i].Quantity = int.Parse(str[1]);
                productions[i].startDate = DateTime.Parse(str[2]);
                productions[i].endDate = DateTime.Parse(str[3]);
                productions[i].Pcb_id = int.Parse(str[0]);
                rmlast = i;
            }
            //rmlast is the index of the last object from productions. productions list was already filled and we changed only 9 object from 10 so we have to remove the last one to avoid a duplicate.
            productions.RemoveAt(rmlast);
            Console.WriteLine("\nDecrypted datas from Puffer.txt\n");
            productions.ForEach(i => Console.WriteLine("pcb_id: {0} \nquanity: {1} \nstartDate: {2} \nendDate: {3}\n", i.Pcb_id, i.Quantity, i.startDate, i.endDate));
        }

        public void insertProduction()
        {
            string constring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";
            MySqlConnection conn = new MySqlConnection(constring);
            conn.Open();
            //Inserting into cs_beugro.production
            for (int i = 0; i < productions.Count; i++)
            {
                string update = "INSERT INTO production (pcb_id,quantity,startDate,endDate) VALUES('" + productions[i].Pcb_id + "','" + productions[i].Quantity + "','" + productions[i].startDate.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + productions[i].endDate.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";
                MySqlCommand cmd2 = new MySqlCommand(update, conn);
                cmd2.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
}
