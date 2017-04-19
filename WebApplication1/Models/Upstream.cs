using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    public class Upstream
    {
        public ObjectId Id{ get; set; }
        [BsonElement("RestId")]
        public int RestId { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("ProxyTypeId")]
        public int ProxyTypeId { get; set; }
        [BsonElement("ServerIds")]
        public List<int> ServerIds { get; set; }
        [BsonElement("MaxFails")]
        public int MaxFails { get; set; }
        [BsonElement("FailTimeout")]
        public int FailTimeout { get; set; }

    }
}
