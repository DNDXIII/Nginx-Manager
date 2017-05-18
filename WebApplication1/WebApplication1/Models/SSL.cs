using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DataAccess;

namespace WebApplication1.Models
{
    public class SSL:MongoObject
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Certificate")]
        public string Certificate { get; set; }
        [BsonElement("TrstCertificate")]
        public string TrstCertificate { get; set; }
        [BsonElement("Key")]
        public string Key { get; set; }
        [BsonElement("Protocols")]
        public string Protocols { get; set; }
        [BsonElement("DHParam")]
        public string DHParam { get; set; }
        [BsonElement("Ciphers")]
        public string Ciphers { get; set; }
        [BsonElement("PreferServerCiphers")]
        public bool PreferServerCiphers { get; set; }
        [BsonElement("SessionCache")]
        public string SessionCache { get; set; }
        [BsonElement("SessionTimeout")]
        public string SessionTimeout { get; set; }
        [BsonElement("BufferSize")]
        public int BufferSize { get; set; }
        [BsonElement("Stapling")]
        public bool Stapling { get; set; }
        [BsonElement("StaplingVerify")]
        public bool StaplingVerify { get; set; }



        public string GenerateConfig()
        {
            var strb = new StringBuilder();
            string res;//to help create some strings
            
            strb.AppendLine("\t\tssl on;");
            strb.AppendLine("\t\tssl_certificate "+ Certificate + ";");
            strb.AppendLine("\t\tssl_trusted_certificate "+ TrstCertificate + ";");
            strb.AppendLine("\t\tssl_certificate_key "+ Key + ";");
            strb.AppendLine("\t\tssl_protocols " + Protocols + ";");
            strb.AppendLine("\t\tssl_dhparam "+ DHParam + ";");
            strb.AppendLine("\t\tssl_ciphers "+ Ciphers + ";");

            res= (PreferServerCiphers ? "on":"off");
            strb.AppendLine("\t\tssl_prefer_server_ciphers " + res + ";");
            
            strb.AppendLine("\t\tssl_session_cache "+ SessionCache + ";");
            strb.AppendLine("\t\tssl_session_timeout "+ SessionTimeout + ";");
            strb.AppendLine("\t\tssl_buffer_size "+ BufferSize + ";");
            
            res= (Stapling ? "on":"off");
            strb.AppendLine("\t\tssl_stapling "+ res + ";");   
            
            res= (StaplingVerify ? "on":"off");
            strb.AppendLine("\t\tssl_stapling_verify "+ res + ":\n");
            
            return strb.ToString();
        }
    }
}
