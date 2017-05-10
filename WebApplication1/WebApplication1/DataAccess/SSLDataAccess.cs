using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class SSLDataAccess : AbstractDataAccess<SSL>
    {
        public SSLDataAccess() : base("SSLs"){
           
        }
    }    
}
