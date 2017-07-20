﻿using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text;
using WebApplication1.DataAccess;

namespace WebApplication1.Models
{
    public class VirtualServer : MongoObject
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Domain")]
        public string Domain { get; set; }
        [BsonElement("Listen")]
        public int Listen { get; set; }
        [BsonElement("Applications")]
        public List<string> Applications { get; set; }//applications id
        [BsonElement("ApplicationsUpstreamId")]
        public List<string> ApplicationsUpstreamId { get; set; }// upstream to use inside the application
        [BsonElement("SSL")]
        public string SSL { get; set; }
        [BsonElement("Whitelist")]
        public string Whitelist { get; set; }
        [BsonElement("FreeText")]
        public string FreeText { get; set; }
        [BsonElement("Priority")]
        public int Priority { get; set; }

        public string GenerateConfig(AllRepositories allRep)
        {
            var strb = new StringBuilder();

            strb.AppendLine("\tserver {");
            strb.AppendLine("\t\tlisten " + Listen + ";");
            strb.AppendLine("\t\tserver_name " + Domain + ";\n");

            if (SSL != null && SSL != "")
                strb.Append(allRep.SSLRep.GetById(SSL).GenerateConfig());

            if (FreeText != null)
                strb.AppendLine("\t" + FreeText);

            for(int i=0;i< Applications.Count;i++)
                strb.Append(allRep.ApplicationRep.GetById(Applications[i]).GenerateConfig(allRep, ApplicationsUpstreamId[i]));

            if (Whitelist != null)
                strb.AppendLine(allRep.WhitelistRep.GetById(Whitelist).GenerateConfig());
            
            strb.AppendLine("\t}");

            return strb.ToString();
        }
    }
}
