using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Models
{
    public class Blacklist : MongoObject
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("IPs")]
        public List<string> Ips { get; set; }

        public string GenerateConfig()
        {
            var strb = new StringBuilder();

            if (Ips.Count > 0)
            {
                foreach (var b in Ips)
                {
                    strb.AppendLine("allow " + b + ";");
                }
                strb.AppendLine("deny all;");
            }
            return strb.ToString();
        }
    }
}
