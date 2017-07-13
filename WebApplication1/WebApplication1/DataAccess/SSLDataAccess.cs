using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class SSLDataAccess : AbstractDataAccess<SSL>
    {
        public SSLDataAccess(string connectionString) : base("SSLs", connectionString)
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
            var upCollection = _db.GetCollection<VirtualServer>("VirtualServers");


            //check if there is any upstream that references this server before deletion
            if ((upCollection.Find(new BsonDocument()).ToList()).Where(vs => vs.SSL.Contains(id)).Count() > 0)
                return false;
            return true;
        }
    }    
}
