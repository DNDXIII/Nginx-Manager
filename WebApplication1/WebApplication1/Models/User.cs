using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    public class User : MongoObject
    {
        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("Password")]
        public string Password{ get; set; }
        
    }
}
