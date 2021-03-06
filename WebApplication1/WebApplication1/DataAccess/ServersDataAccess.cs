﻿using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Models;
using System.Linq;

namespace WebApplication1.DataAccess
{
    public class ServersDataAccess : AbstractDataAccess<Server>
    {
        public ServersDataAccess(string connectionString) : base("Servers", connectionString)
        {
            
        }
        public override bool Delete(string id)
        {
            if (!CanDelete(id))
                return false;
            
            return base.Delete(id);
        }

        private bool CanDelete(string id)
        {
            var upCollection = _db.GetCollection<Upstream>("Upstreams");


            //check if there is any upstream that references this server before deletion
            if ((upCollection.Find(new BsonDocument()).ToList()).Where(u=> u.ServerIds.Contains(id)).Count() > 0)
                return false;
            return true;
        }
    }    


}
