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

        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            foreach (var up in allRep.UpstreamRep.GetAll())
            {
                strb.AppendLine(up.GenerateConfig(allRep));
            }

            foreach (var vs in allRep.VirtualServerRep.GetAll())
            {
                strb.AppendLine(vs.GenerateConfig(allRep));
            }

            return Text.Replace("{{config}}", strb.ToString());
        }

    }
}
