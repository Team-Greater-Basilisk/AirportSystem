using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBController
{
    public class CompanyInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string CompanyName { get; set; }

        public string Town { get; set; }

        public int AirplainsCount { get; set; }

        public int EmployeesCount { get; set; }
    }
}