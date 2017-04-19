using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WebApplication1.Models
{
    public class Upstream
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id{ get; set; }
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
