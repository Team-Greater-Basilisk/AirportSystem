using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBController
{
    public class MongoDataReader
    {
        private MongoDatabase database;

        public MongoDataReader()
        {
            var connection = new MongoDBConnector("mongodb://localhost/");
            this.database = connection.GetDatabase("Test");
        }

        public IEnumerable<Company> GetCompanies()
        {

            var companies = database.GetCollection("Companies");
            var allComapnies = companies.FindAllAs<Company>();
            var list = allComapnies.ToList();

            return list;
        }

        public IEnumerable<Destination> GetDestinations()
        {

            var companies = database.GetCollection("Destinations");
            var allComapnies = companies.FindAllAs<Destination>();
            var list = allComapnies.ToList();

            return list;
        }

        public IEnumerable<Customer> GetCustomers()
        {

            var companies = database.GetCollection("Customers");
            var allComapnies = companies.FindAllAs<Customer>();
            var list = allComapnies.ToList();

            return list;
        }
    }
}
