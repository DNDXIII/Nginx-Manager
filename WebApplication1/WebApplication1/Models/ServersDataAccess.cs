using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication1.Models
{
    public class ServersDataAccess : IServersRepository
    {

        private IMongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<Server> _serverCollection;

        public ServersDataAccess()
        {
            _client = new MongoClient();
            _db = _client.GetDatabase("NginxDB");
            _serverCollection = _db.GetCollection<Server>("Servers");

        }

        public Server Add(Server sv)
        {
            AddAsync(sv);
            return sv;
        }

        private async void AddAsync(Server sv)
        {
            await _serverCollection.InsertOneAsync(sv);

        }

        public void Delete(int id)
        {
            DeleteAsync(id);
        }

        private async void DeleteAsync(int id)
        {
            var filter = Builders<Server>.Filter.Eq("RestId", id);
            await _serverCollection.DeleteOneAsync(filter);
        }
        public IEnumerable<Server> GetAll()
        {
            return GetAllAsync().Result;
        }

        private async Task<IEnumerable<Server>> GetAllAsync()
        {
            var filter = new BsonDocument();

            return (await _serverCollection.Find(filter).ToListAsync());
        }

        public Server GetById(int id)
        {
            var res = GetByIdAsync(id).Result;
            if (res.Count() == 0)
                return null;
            return res.First();

        }

        private async Task<IEnumerable<Server>> GetByIdAsync(int id)
        {
            var filter = Builders<Server>.Filter.Eq("RestId", id);
            return (await _serverCollection.Find(filter).ToListAsync());
        }

        public void Update(Server sv)
        {
            UpdateAsync(sv);
        }

        private async void UpdateAsync(Server sv)
        {
            var filter = Builders<Server>.Filter.Eq("RestId", sv.RestId);

            sv.Id = GetById(sv.RestId).Id;

            await _serverCollection.ReplaceOneAsync(filter, sv);
        }

        public IEnumerable<Server> GetList(string sort, string order, int start, int end)
        {
            List<Server> svs = GetListAsync(sort,order).Result.ToList();

            if (svs.Count() - start > end - start)
                return svs.GetRange(start, (end - start));
            else
                if (svs.Count() > start)
                  return svs.GetRange(start, (svs.Count - start));
                else
                  return null;
        }

        private async Task<IEnumerable<Server>> GetListAsync(string sort, string order)
        {
            var filter = new BsonDocument();
            if(order=="ASC")
                return (await _serverCollection.Find(filter).Sort(Builders<Server>.Sort.Ascending(order)).ToListAsync());
            else
                return (await _serverCollection.Find(filter).Sort(Builders<Server>.Sort.Descending(order)).ToListAsync());
        }


        /*
        private static string FirstCharToUpper(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }*/
    }
}
