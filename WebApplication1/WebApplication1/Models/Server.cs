using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text;

namespace WebApplication1.Models
{
    public class Server : MongoObject
    {

        private const int DEFAULT_MAX_FAILS = 1;
        private const int DEFAULT_FAIL_TIMEOUT = 10;

        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        

        
        public string GenerateConfig(int port, int maxFails, int failTimeout)
        {
            var strb = new StringBuilder();

            strb.Append("\tserver " + Address + ":" + port);
            if (failTimeout != DEFAULT_FAIL_TIMEOUT)
                strb.Append(" fail_timeout=" + failTimeout);
            if (maxFails != DEFAULT_MAX_FAILS)
                strb.Append(" max_fails=" + maxFails);
            strb.Append(";");

            return strb.ToString();
        }
    }


}
