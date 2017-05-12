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
    public class Application:MongoObject
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Locations")]
        public List<string> Locations { get; set; } //TODO 
        
       
        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            foreach(var l in Locations)
            {
                strb.AppendLine(allRep.LocationRep.GetById(l).GenerateConfig(allRep));
            }

            return strb.ToString();
        }
    }
}
