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
        [BsonElement("Password")]
        public string Password;

        public void setPassword(string pass)
        {
            Password = Encryption.EncryptString(pass, "49GOVPZ61AMJYIGXVKQSPEWXFNQZT05N");
        }

        public string getPassword()
        {
            return Encryption.DecryptString(Password, "49GOVPZ61AMJYIGXVKQSPEWXFNQZT05N");
        }
/*


        public DeploymentServer(string Password)
        {
            this.Password = Encryption.EncryptString(Password, "49GOVPZ61AMJYIGXVKQSPEWXFNQZT05N");
        }*/
    }
}
