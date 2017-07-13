using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class ApplicationDataAccess :AbstractDataAccess<Application>
    {   
        public ApplicationDataAccess(string connectionString) : base("Applications", connectionString)
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


            //check if there is any reference to this object before deletion
            if ((upCollection.Find(new BsonDocument()).ToList()).Where(vs => vs.Applications.Contains(id)).Count() > 0)
                return false;
            return true;
        }
    }
}
