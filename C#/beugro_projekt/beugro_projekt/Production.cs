using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beugro_projekt
{
    public class Production
    {
        public int Pcb_id { get; set; }
        public int Quantity { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public Production(int Pcb_id, int Quantity, DateTime startDate, DateTime endDate)
        {
            this.Pcb_id = Pcb_id;
            this.Quantity = Quantity;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}
