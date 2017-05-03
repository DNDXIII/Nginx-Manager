using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication1.DataAccess
{
    public class VirtualServerDataAccess :AbstractDataAccess<VirtualServer>
    {   
        public VirtualServerDataAccess() : base("VirtualServers"){ 
        }
    }
}
