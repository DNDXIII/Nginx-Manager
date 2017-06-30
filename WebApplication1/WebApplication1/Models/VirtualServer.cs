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
        public string Applications { get; set; }//applications id
        [BsonElement("ApplicationsUpstreamId")]
        public string ApplicationsUpstreamId { get; set; }// upstream to use inside the application
        [BsonElement("SSL")]
        public string SSL { get; set; }
        [BsonElement("Blacklist")]
        public string Blacklist { get; set; }
        [BsonElement("FreeText")]
        public string FreeText { get; set; }
        [BsonElement("Priority")]
        public int Priority { get; set; }

        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            strb.AppendLine("\tserver {");
            strb.AppendLine("\t\tlisten " + Listen + ";");
            strb.AppendLine("\t\tserver_name " + Domain + ";\n");

            if (SSL != null && SSL != "")
                strb.Append(allRep.SSLRep.GetById(SSL).GenerateConfig());
            if (Applications != null)//&& Applications.Count!=0
                    strb.Append(allRep.ApplicationRep.GetById(Applications).GenerateConfig(allRep, ApplicationsUpstreamId));

            if (Locations != null && Locations.Count!=0)
                foreach (var l in Locations)
                    strb.Append(allRep.LocationRep.GetById(l).GenerateConfig(allRep, null));



            if (FreeText != null)
                strb.AppendLine("\t"+ FreeText);

            if (Blacklist != null)
                strb.AppendLine(allRep.BlacklistRep.GetById(Blacklist).GenerateConfig());
            



            strb.AppendLine("\t}");

            return strb.ToString();
        }
    }
}
