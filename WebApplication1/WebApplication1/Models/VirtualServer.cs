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
    public class VirtualServer:MongoObject
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Listen")]
        public int Listen { get; set; }
        [BsonElement("Locations")]
        public List<string> Locations { get; set; }
        [BsonElement("Application")]
        public string Application { get; set; }
        [BsonElement("SSL")]
        public string SSL { get; set; }
        [BsonElement("FreeText")]
        public string FreeText { get; set; }
       
        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            strb.AppendLine("server {");
            strb.AppendLine("   listen " + Listen +";");
            strb.AppendLine("   server_name " + Name.Replace(" ", "_") + ";\n");

            if(SSL!=null)
                strb.Append(allRep.SSLRep.GetById(SSL).GenerateConfig());
            if(Application!=null)
                strb.Append(allRep.ApplicationRep.GetById(Application).GenerateConfig(allRep));   

            if(Locations!=null)
                foreach(var l in Locations)
                {
                    strb.Append(allRep.LocationRep.GetById(l).GenerateConfig(allRep));
                }

               
           if(FreeText!=null)
                strb.AppendLine("   " + FreeText);


            strb.AppendLine("}");

            return strb.ToString();
        }
    }
}
