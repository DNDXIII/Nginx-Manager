using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Reflection;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Text;

namespace WebApplication1.Models
{
    public class Server { 
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("Port")]
        public int Port { get; set; }

        public string GenerateConfig(Upstream up, string pType, int i)//TODO
        {
            var strb = new StringBuilder();

            strb.Append("   server " + Address + ":" + Port);

            if (pType == "Weighted Load Balancing")
                strb.Append(" weight=" + up.Weights[i]);

            strb.AppendLine(";");

            return strb.ToString();
        }

    }

    
}
