using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Data
{
    public class Destination
    {
        public Destination()
        {
            this.Tickets = new HashSet<Ticket>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}