using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DataAccess;

namespace WebApplication1.Models
{
    public class DeploymentServer:MongoObject
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; } 
        [BsonElement("Port")]
        public int Port { get; set; }
        [BsonElement("Active")]
        public bool Active { get; set; }
    }
}
