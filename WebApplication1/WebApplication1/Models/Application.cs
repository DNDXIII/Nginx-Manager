using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text;
using WebApplication1.DataAccess;

namespace WebApplication1.Models
{
    public class Application:MongoObject
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Locations")]
        public List<string> Locations { get; set; } 
        [BsonElement("UpstreamId")]
        public string UpstreamId { get; set; }
        
       
        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            foreach(var l in Locations)
            {
                strb.AppendLine(allRep.LocationRep.GetById(l).GenerateConfig(allRep, UpstreamId));
            }

            return strb.ToString();
        }
    }
}
