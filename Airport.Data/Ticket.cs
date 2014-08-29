using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Data
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public DateTime TravelingDate { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Destination Destination { get; set; }
        public virtual Company Company { get; set; }
    }
}
