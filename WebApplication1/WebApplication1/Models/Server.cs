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
    public class Server : MongoObject
    {
        private const int DEFAULT_MAX_FAILS = 1;
        private const int DEFAULT_FAIL_TIMEOUT = 10;

        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("Port")]
        public int Port { get; set; }
        [BsonElement("MaxFails")]
        public int MaxFails { get; set; }
        [BsonElement("FailTimeout")]
        public int FailTimeout { get; set; }

        public Server()
        {
            this.MaxFails = DEFAULT_MAX_FAILS;
            this.FailTimeout = DEFAULT_FAIL_TIMEOUT;
        }

        public string GenerateConfig()
        {
            var strb = new StringBuilder();

            strb.Append("   server " + Address + ":" + Port);
            if (FailTimeout != DEFAULT_FAIL_TIMEOUT)
                strb.Append(" fail_timeout=" + FailTimeout);
            if (MaxFails != DEFAULT_MAX_FAILS)
                strb.Append(" max_fails=" + MaxFails);
            strb.AppendLine(";");

            return strb.ToString();
        }

    }


}
