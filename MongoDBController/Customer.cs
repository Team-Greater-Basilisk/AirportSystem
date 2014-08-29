using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBController
{
    public class Customer
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [BsonConstructor]
        public Customer(string firstName , string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
