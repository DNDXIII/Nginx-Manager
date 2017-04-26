using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Location
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [BsonElement("Path")]
        public string Path { get; set; }
        [BsonElement("Pass")]
        public string Pass { get; set; }
        [BsonElement("PassType")]
        public string PassType { get; set; }


        public Location()
        {
            PassType = "proxy_pass";
        }
    }
}
