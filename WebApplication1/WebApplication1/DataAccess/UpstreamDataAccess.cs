using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication1.Models
{
    public class UpstreamDataAccess : AbstractDataAccess<Upstream>
    {
        public UpstreamDataAccess() : base("Upstreams") { }     
    }
}
