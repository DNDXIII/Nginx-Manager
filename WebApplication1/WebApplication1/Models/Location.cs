using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DataAccess;
using WebApplication1.Models;


namespace WebApplication1.Models
{
    public class Location : MongoObject
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("URI")]//URI that it matches to
        public string URI { get; set; }
        [BsonElement("Pass")]//id of the upstream to pass to
        public string Pass { get; set; }
        [BsonElement("PassType")]//proxy pass...
        public string PassType { get; set; }
        [BsonElement("MatchType")]// ~* ...
        public string MatchType { get; set; }
        [BsonElement("FreeText")]
        public string FreeText { get; set; }


        public Location()
        {
            PassType = "proxy_pass";
        }

        public string GenerateConfig(AllRepositories allRep, string upstreamId)//upstreamId is used when trying to override 
                                                                               //the Pass variable, oterwise should be null
        {
            var strb = new StringBuilder();

            var s = MatchType != null ? MatchType : "";

            strb.AppendLine("   location " + s + " " + URI + " {");

            string upstreamToPassTo = upstreamId == null ? Pass : upstreamId;

            strb.AppendLine("       " + PassType + " " + allRep.UpstreamRep.GetById(upstreamToPassTo).Name.Replace(" ", "_") + ";");

            if (FreeText != null)
                strb.AppendLine("       " + FreeText);

            strb.AppendLine("   }");


            return strb.ToString();
        }
    }
}
