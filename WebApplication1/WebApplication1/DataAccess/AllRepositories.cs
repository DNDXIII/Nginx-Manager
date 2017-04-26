using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class AllRepositories
    {
        public IRepository<Server> ServerRep { get; }
        public IRepository<Upstream> UpstreamRep { get; }
        public IRepository<ProxyType> ProxyTypeRep { get; }

        public AllRepositories(IRepository<Server> svRep, IRepository<Upstream> upRep, IRepository<ProxyType> proxyRep )
        {
            ServerRep = svRep;
            UpstreamRep = upRep;
            ProxyTypeRep = proxyRep;
        }
    }
}
