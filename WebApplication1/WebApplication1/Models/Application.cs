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
        public List<Location> Locations { get; set; }
        [BsonElement("Protocol")]
        public string Protocol { get; set; }

        public string GenerateConfig(AllRepositories allRep, string UpstreamId)
        {
            var strb = new StringBuilder();

            foreach(var l in Locations)
            {
                strb.AppendLine(l.GenerateConfig(allRep, UpstreamId, Protocol));
            }

            return strb.ToString();
        }
    }
}
