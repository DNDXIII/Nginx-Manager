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
    public class VirtualServer : MongoObject
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Domain")]
        public string Domain { get; set; }
        [BsonElement("Listen")]
        public int Listen { get; set; }
        [BsonElement("Locations")]
        public List<string> Locations { get; set; }
        [BsonElement("Applications")]
        public List<string> Applications { get; set; }
        [BsonElement("SSL")]
        public string SSL { get; set; }
        [BsonElement("FreeText")]
        public string FreeText { get; set; }

        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            strb.AppendLine("server {");
            strb.AppendLine("   listen " + Listen + ";");
            strb.AppendLine("   server_name " + Domain + ";\n");

            if (SSL != null && SSL != "")
                strb.Append(allRep.SSLRep.GetById(SSL).GenerateConfig());
            if (Applications != null && Applications.Count!=0)
                foreach (string app in Applications)
                    strb.Append(allRep.ApplicationRep.GetById(app).GenerateConfig(allRep));

            if (Locations != null && Locations.Count!=0)
                foreach (var l in Locations)
                    strb.Append(allRep.LocationRep.GetById(l).GenerateConfig(allRep, null));



            if (FreeText != null)
                strb.AppendLine("   " + FreeText);


            strb.AppendLine("}");

            return strb.ToString();
        }
    }
}
