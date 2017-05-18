using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DataAccess;

namespace WebApplication1.Models
{
    public class GeneralConfig:MongoObject
    {
        [BsonElement("Text")]
        public string Text { get; set;}
        [BsonElement("Name")]
        public string Name { get; set; }

        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            strb.Append(Text);

            foreach (var up in allRep.UpstreamRep.GetAll())
            {
                strb.AppendLine(up.GenerateConfig(allRep));
            }

            foreach (var vs in allRep.VirtualServerRep.GetAll())
            {
                strb.AppendLine(vs.GenerateConfig(allRep));
            }

            strb.AppendLine("}");
            return strb.ToString();
        }

    }
}
