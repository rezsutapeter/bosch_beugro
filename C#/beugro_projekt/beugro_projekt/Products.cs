using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beugro_projekt
{
    public class Products
    {
        public int Id { get; set; }
        public string Pcb { get; set; }

        public Products(int Id, string Pcb)
        {
            this.Id = Id;
            this.Pcb = Pcb;
        }
    }
}
