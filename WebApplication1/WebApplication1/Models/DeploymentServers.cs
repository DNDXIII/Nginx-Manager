using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography;
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
        [BsonElement("Salt")]
        public byte[] Salt { get; internal set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        private string _password;
        [BsonElement("Password")]
        public string Password
        {
            get
            {
                return Encryption. Decrypt("E546C8DF278CD5931069B522E695D4F2", Salt, _password);
            }
            set
            {
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                Salt = salt;
                _password = Encryption.Encrypt("E546C8DF278CD5931069B522E695D4F2", Salt, value);
            }
        }

     
    }
}
