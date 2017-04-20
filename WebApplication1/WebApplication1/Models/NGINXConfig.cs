using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class NGINXConfig
    {
        private IDictionary<string, Server> _servers;
        private IDictionary<string, Upstream> _upstreams;
        private IDictionary<string, ProxyType> _proxyTypes;
       // private string pathString = @"../stuff/coise.txt";

        public NGINXConfig(IDictionary<string, Server> servers, IDictionary<string, Upstream> upstreams, IDictionary<string, ProxyType> proxyTypes)
        {
            _servers = servers;
            _upstreams = upstreams;
            _proxyTypes = proxyTypes;
        }

        public string GenerateConfig()
        {

            var strb = new StringBuilder();


            foreach (var up in _upstreams.Values)
            {
                var i = 0;//to count wich of the upstream's servers im on
                strb.AppendLine("upstream " + up.Name + " {");

                var pType = _proxyTypes[up.ProxyTypeId];

                strb.AppendLine(pType.ProxyValue);

                foreach(var svId in up.ServerIds)
                {
                    var sv = _servers[svId];
                    strb.Append("   server " + sv.Ip + ":" + sv.Port );

                    if (pType.Name == "Weighted Load Balancing")
                        strb.Append(" weight=" + up.Weights[i]);

                    strb.AppendLine(";");
                    i++;
                }

                strb.AppendLine("}");
            }

            foreach (var server in servers)
            {
                strb.AppendLine(server.GenerateConfig());
            }

            strb.AppendLine(" } ");

            return strb.ToString();
        }

    }
}
