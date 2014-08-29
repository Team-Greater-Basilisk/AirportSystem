using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBController
{
    internal class MongoDBConnector
    {
        private MongoServer server;
        public MongoDBConnector(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            this.server = mongoClient.GetServer();
        }

        public MongoDatabase GetDatabase(string databaseName)
        {
            return this.server.GetDatabase(databaseName);
        }
    }
}
