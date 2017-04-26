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
    public class VirtualServer
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Listen")]
        public int Listen { get; set; }

        [BsonElement("Locations")]
        public List<Location> Locations { get; set; }

        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            strb.AppendLine("server {");
            strb.AppendLine("   listen " + Listen +";");
            strb.AppendLine("   server_name " + Name.Replace(" ", "_") + ";\n");

            foreach(var l in Locations)
            {
                strb.AppendLine(l.GenerateConfig(allRep));
            }

            strb.AppendLine("}");

            return strb.ToString();
        }
    }
}
