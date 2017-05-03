using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class UpstreamDataAccess : AbstractDataAccess<Upstream>
    {
        public UpstreamDataAccess() : base("Upstreams") {
         }     
    }
}
