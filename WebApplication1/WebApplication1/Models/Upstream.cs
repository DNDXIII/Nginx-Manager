using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Text;
using WebApplication1.DataAccess;

namespace WebApplication1.Models
{
    public class Upstream
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id{ get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("ProxyTypeId")]
        public string ProxyTypeId { get; set; }
        [BsonElement("ServerIds")]
        public List<string> ServerIds { get; set; }
        [BsonElement("MaxFails")]
        public int MaxFails { get; set; }
        [BsonElement("FailTimeout")]
        public int FailTimeout { get; set; }
        [BsonElement("Weights")]
        public int[] Weights { get; set; }
        private AllRepositories allRep;

        public Upstream()
        {
            Weights = new int[] { };
        }

        public string GenerateConfig()
        {
            var strb = new StringBuilder();
            var pType = allRep._proxyRep.GetById(ProxyTypeId);

            strb.AppendLine("upstream " + Name + " {");
            strb.AppendLine(pType.ProxyValue);            

            for(int i = 0; i < ServerIds.Count; i++)
            {
                strb.Append(allRep._svRep.GetById(ServerIds[i]).GenerateConfig(this, pType.Name));
            }

            strb.AppendLine("}");

            return strb.ToString();
        }

    }
}
