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
    public class Location
    {
        [BsonElement("Path")]
        public string Path { get; set; }
        [BsonElement("Pass")]
        public string Pass { get; set; }
        [BsonElement("PassType")]
        public string PassType { get; set; }
        [BsonElement("FreeText")]
        public string FreeText { get; set; }


        public Location()
        {
            PassType = "proxy_pass";
        }

        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            strb.AppendLine("   location " + /*adcionar match type*/ Path + " {");

            strb.AppendLine("       " + PassType + " " + allRep.UpstreamRep.GetById(Pass).Name.Replace(" ", "_") + ";");
           
           if(FreeText!=null)
                strb.AppendLine(FreeText);


            strb.AppendLine("   }");


            return strb.ToString();
        }
    }
}
