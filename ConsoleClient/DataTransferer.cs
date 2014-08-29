using Airport.Model;
using MongoDBController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public static class DataTransferer
    {
        public static void TransferDataFromMongoToMsSql()
        {
            using (var msDb = new AirportDbContext())
            {
                MongoDataReader reader = new MongoDataReader();
                var comapnies = reader.GetCompanies();
                foreach (var company in comapnies)
                {
                    var newCompany = new Airport.Data.Company();
                    newCompany.Name = company.Name;
                    msDb.Companies.Add(newCompany);
                }
                var customers = reader.GetCustomers();
                foreach (var customer in customers)
                {
                    var newCustomer = new Airport.Data.Customer();
                    newCustomer.FirstName = customer.FirstName;
                    newCustomer.LastName = customer.LastName;
                    msDb.Customers.Add(newCustomer);
                }
                var destinations = reader.GetDestinations();
                foreach (var dest in destinations)
                {
                    var destination = new Airport.Data.Destination();
                    destination.Name = dest.Name;
                    msDb.Destinations.Add(destination);
                }

                msDb.SaveChanges();
            }
        } 
    }
}
