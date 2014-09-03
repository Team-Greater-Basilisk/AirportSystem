using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport.Data;

namespace Airport.Model
{
    public class AirportDbContext : DbContext
    {
        public AirportDbContext()
            :base("AirportContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<CompanyInfo> CompanyInfo { get; set; }
    }
}
