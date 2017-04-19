using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication1.Models
{
    public class UpstreamDataAccess : IUpstreamsRepository
    {

        private IMongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<Upstream> _upstreamCollection;

        public UpstreamDataAccess()
        {
            _client = new MongoClient();
            _db = _client.GetDatabase("NginxDB");
            _upstreamCollection = _db.GetCollection<Upstream>("Upstreams");

        }

        public Upstream Add(Upstream up)
        {
            AddAsync(up);
            return up;
        }

        private async void AddAsync(Upstream up)
        {
            await _upstreamCollection.InsertOneAsync(up);
            
        }
        
        public void Delete(int id)
        {
            DeleteAsync(id);
        }

        private async void DeleteAsync(int id)
        {
            var filter = Builders<Upstream>.Filter.Eq("RestId", id);
            await _upstreamCollection.DeleteOneAsync(filter);
        }
        public IEnumerable<Upstream> GetAll()
        {
            return GetAllAsync().Result;
        }

        private async Task<IEnumerable<Upstream>> GetAllAsync()
        {
            var filter = new BsonDocument();

            return (await _upstreamCollection.Find(filter).ToListAsync());
        }

        public Upstream GetById(int id)
        {
            var res = GetByIdAsync(id).Result;
            if (res.Count() == 0)
                return null;
            return res.First();
            
        }

        private async Task<IEnumerable<Upstream>> GetByIdAsync(int id)
        {
            var filter = Builders<Upstream>.Filter.Eq("RestId", id);
            return (await _upstreamCollection.Find(filter).ToListAsync());
        }

        public void Update(Upstream up)
        {
            UpdateAsync(up);
        }

        private async void UpdateAsync(Upstream up)
        {
            var filter = Builders<Upstream>.Filter.Eq("RestId", up.RestId);

            up.Id = GetById(up.RestId).Id;

            await _upstreamCollection.ReplaceOneAsync(filter, up);
        } 
        
    }
}
