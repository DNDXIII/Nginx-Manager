using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Reflection;


namespace WebApplication1.Models
{
    public class Server
    {
        public ObjectId Id { get; set; }
        [BsonElement("RestId")]
        public int RestId { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Ip")]
        public string Ip { get; set; }
        [BsonElement("Port")]
        public int Port { get; set; }
       
    }
}
