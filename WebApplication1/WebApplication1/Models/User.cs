using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography;

namespace WebApplication1.Models
{
    public class User : MongoObject
    {
        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("Salt")]
        public byte[] Salt { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }

    }
}
