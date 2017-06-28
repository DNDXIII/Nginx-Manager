using MongoDB.Bson.Serialization.Attributes;
using WebApplication1.Common;

namespace WebApplication1.Models
{
    public class DeploymentServer:MongoObject
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; } 
        [BsonElement("Port")]
        public int Port { get; set; }
        [BsonElement("Active")]
        public bool Active { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        private string _password;
        [BsonElement("Password")]
        public string Password
        {
            set
            {
                _password = Encryption.EncryptString(value, "49GOVPZ61AMJYIGXVKQSPEWXFNQZT05N");
            }
        }

        public string getPassword()
        {
            return Encryption.DecryptString(_password, "49GOVPZ61AMJYIGXVKQSPEWXFNQZT05N");
        }

    }
}
