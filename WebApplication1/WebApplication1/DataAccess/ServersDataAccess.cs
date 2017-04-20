using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication1.Models
{
    public class ServersDataAccess : AbstractDataAccess<Server>
    {
        public ServersDataAccess() : base("Servers") { }
    }
}
