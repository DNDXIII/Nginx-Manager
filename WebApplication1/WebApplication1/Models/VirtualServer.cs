﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DataAccess;

namespace WebApplication1.Models
{
    public class VirtualServer
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Listen")]
        public int Listen { get; set; }
        [BsonElement("Locations")]
        public List<Location> Locations { get; set; }
        [BsonElement("FreeText")]
        public string FreeText { get; set; }
        [BsonElement("SSL")]
        public bool SSL { get; set; }
        [BsonElement("SSLCertificate")]
        public string SSLCertificate { get; set; }
        [BsonElement("SSLTrstCertificate")]
        public string SSLTrstCertificate { get; set; }
        [BsonElement("SSLKey")]
        public string SSLKey { get; set; }
        [BsonElement("SSLProtocols")]
        public string[] SSLProtocols { get; set; }
        [BsonElement("SSLDHParam")]
        public string SSLDHParam { get; set; }
        [BsonElement("SSLCiphers")]
        public string SSLCiphers { get; set; }
        [BsonElement("SSLPreferServerCiphers")]
        public bool SSLPreferServerCiphers { get; set; }
        [BsonElement("SSLSessionCache")]
        public string SSLSessionCache { get; set; }
        [BsonElement("SSLSessionTimeout")]
        public string SSLSessionTimeout { get; set; }
        [BsonElement("SSLBufferSize")]
        public int SSLBufferSize { get; set; }
        [BsonElement("SSLStapling")]
        public bool SSLStapling { get; set; }
        [BsonElement("SSLStaplingVerify")]
        public bool SSLStaplingVerify { get; set; }



        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();
            string res;//to help create some strings

            strb.AppendLine("server {");
            strb.AppendLine("   listen " + Listen +";");
            strb.AppendLine("   server_name " + Name.Replace(" ", "_") + ";\n");

            if(SSL){
                strb.AppendLine("   ssl on;");
                strb.AppendLine("   ssl_certificate "+ SSLCertificate + ";");
                strb.AppendLine("   ssl_trusted_certificate "+ SSLTrstCertificate + ";");
                strb.AppendLine("   ssl_certificate_key "+ SSLKey + ";");
                strb.Append("   ssl_protocols");
                foreach(var e in SSLProtocols)
                    strb.Append(" " + e);
                strb.AppendLine(";");
                strb.AppendLine("   ssl_dhparam "+ SSLDHParam + ";");
                strb.AppendLine("   ssl_ciphers "+ SSLCiphers + ";");

                res= (SSLPreferServerCiphers ? "on":"off");
                strb.AppendLine("   ssl_prefer_server_ciphers " + res + ";");
                
                strb.AppendLine("   ssl_session_cache "+ SSLSessionCache + ";");
                strb.AppendLine("   ssl_session_timeout "+ SSLSessionTimeout + ";");
                strb.AppendLine("   ssl_buffer_size "+ SSLBufferSize + ";");
               
                res= (SSLStapling ? "on":"off");
                strb.AppendLine("   ssl_stapling "+ res + ";");   
               
                res= (SSLStaplingVerify ? "on":"off");
                strb.AppendLine("   ssl_stapling_verify "+ res + ":\n");
            }


            foreach(var l in Locations)
            {
                strb.AppendLine(l.GenerateConfig(allRep));
            }

               
           if(FreeText!=null)
                strb.AppendLine(FreeText);


            strb.AppendLine("}");

            return strb.ToString();
        }
    }
}
