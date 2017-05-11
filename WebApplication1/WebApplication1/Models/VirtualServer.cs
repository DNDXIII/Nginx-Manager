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


        [BsonElement("Locations")]//TODO necessario ? talvez para os wildcards?
        public List<Location> Locations { get; set; }//TODO mudar para list string com os ids das locations ? 


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
                allRep.SSLRep.GetById(SSL).GenerateConfig();

            allRep.ApplicationRep.GetById(Application).GenerateConfig(allRep);   

            foreach(var l in Locations)
            {
                strb.AppendLine(l.GenerateConfig(allRep));
            }

               
           if(FreeText!=null)
                strb.AppendLine(FreeText);


            strb.AppendLine("}");

            return strb.ToString();
        }
    }
}
