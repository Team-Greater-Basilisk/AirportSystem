using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBController
{
    public class MongoDataInserter
    {
        private MongoDatabase database;

        public MongoDataInserter()
        {
            var connection = new MongoDBConnector("mongodb://localhost/");
            this.database = connection.GetDatabase("AirportSystem");
        }

        public void AddComapny(Company company)
        {
            var companies = this.database.GetCollection("Companies");
            companies.Insert(company);
        }

        public void AddCustomer(Customer customer)
        {
            var customers = this.database.GetCollection("Customers");
            customers.Insert(customer);
        }

        public void AddDestination(Destination destination)
        {
            var destinations = this.database.GetCollection("Destinations");
            destinations.Insert(destination);
        }
        public void AddCompanyInfo(CompanyInfo companyInfo)
        {
            var companyInfoCollection = this.database.GetCollection("CompanyInfo");
            companyInfoCollection.Insert(companyInfo);
        }
    }
}