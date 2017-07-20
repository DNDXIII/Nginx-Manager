using MongoDB.Bson.Serialization.Attributes;
using System.Text;
using WebApplication1.DataAccess;


namespace WebApplication1.Models
{
    public class Location 
    {
        [BsonElement("URI")]//URI that it matches to
        public string URI { get; set; }
        [BsonElement("PassType")]//proxy pass...
        public string PassType { get; set; }
        [BsonElement("MatchType")]// ~* ...
        public string MatchType { get; set; }
        [BsonElement("FreeText")]
        public string FreeText { get; set; }

        public string GenerateConfig(AllRepositories allRep, string upstreamId, string Protocol)
        {
            var strb = new StringBuilder();

            var s = MatchType != null ? MatchType : "";

            strb.AppendLine("\t\tlocation " + s + " " + URI + " {");

            var up = allRep.UpstreamRep.GetById(upstreamId);

            if (PassType!=null && PassType!="" && up!=null)
                strb.AppendLine("\t\t\t" + PassType + " " +Protocol+ up.Name.Replace(" ", "_") + ";");

            if (FreeText != null && FreeText!="")
                strb.AppendLine("\t\t\t" + FreeText);

            strb.AppendLine("\t\t}");


            return strb.ToString();
        }
    }
}
