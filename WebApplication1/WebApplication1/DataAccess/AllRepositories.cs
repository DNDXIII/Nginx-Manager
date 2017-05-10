﻿using System;
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
        public IRepository<VirtualServer> VirtualServerRep { get; }

        public AllRepositories(ServersDataAccess s, UpstreamDataAccess u, ProxyTypeDataAccess p, VirtualServerDataAccess vs)
        {
            ServerRep = s;
            UpstreamRep = u;
            ProxyTypeRep = p;
            VirtualServerRep = vs;
        }
    }
}