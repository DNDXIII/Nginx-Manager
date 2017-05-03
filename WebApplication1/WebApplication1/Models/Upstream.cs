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
        public const int DEFAULT_MAX_FAILS = 1;
        public const int DEFAULT_FAIL_TIMEOUT = 10;


        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id{ get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("ProxyTypeId")]
        public string ProxyTypeId { get; set; }
        [BsonElement("ServerIds")]
        public List<string> ServerIds { get; set; }
        [BsonElement("MaxFails")]
        public int[] MaxFails { get; set; }
        [BsonElement("FailTimeout")]
        public int[] FailTimeout { get; set; }
        [BsonElement("Weights")]
        public int[] Weights { get; set; }
        [BsonElement("FreeText")]
        public string FreeText { get; set; }
        
        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();
            var pType = allRep.ProxyTypeRep.GetById(ProxyTypeId);

            strb.AppendLine("upstream " + Name.Replace(" ", "_") + " {");
            if(pType.ProxyValue != "")
                strb.AppendLine("   " + pType.ProxyValue + ";");            

            for(int i = 0; i < ServerIds.Count; i++)
            {
                strb.Append(allRep.ServerRep.GetById(ServerIds[i]).GenerateConfig());

                if (pType.Name == "Weighted")
                    strb.Append(" weight=" + Weights[i]);
                if(FailTimeout!=null && FailTimeout[i]!=DEFAULT_FAIL_TIMEOUT)
                    strb.Append(" fail_timeout=" + FailTimeout[i]);
                if(MaxFails!=null && MaxFails[i]!=DEFAULT_MAX_FAILS)
                    strb.Append(" max_fails=" + MaxFails[i]);

                 strb.AppendLine(";");
            }

            if(FreeText!=null)
                strb.AppendLine("   " + FreeText);

            strb.AppendLine("}");

            return strb.ToString();
        }

    }
}
