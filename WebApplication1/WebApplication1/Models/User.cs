using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    public class User : MongoObject
    {
        [BsonElement("Username")]
        public string Username { get; set; }
        private string _password;
        [BsonElement("Password")]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = Hash.ComputeHash(value, Username);
            }
        }

        
    }
}
