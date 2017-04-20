using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication1.Models
{
    public class AbstractDataAccess<E> : IRepository<E>
    {

        private IMongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<E> _collection;

        public AbstractDataAccess(string name)
        {
            _client = new MongoClient();
            _db = _client.GetDatabase("NginxDB");
            _collection = _db.GetCollection<E>(name);

        }

        public E Add(E e)
        {
            AddAsync(e);
            return e;
        }

        private async void AddAsync(E e)
        {
            await _collection.InsertOneAsync(e);

        }

        public void Delete(string id)
        {
            DeleteAsync(id);
        }

        private async void DeleteAsync(string id)
        {
            var filter = Builders<E>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
        public IEnumerable<E> GetAll()
        {
            return GetAllAsync().Result;
        }

        private async Task<IEnumerable<E>> GetAllAsync()
        {
            var filter = new BsonDocument();

            return (await _collection.Find(filter).ToListAsync());
        }

        public E GetById(string id)
        {
            var res = GetByIdAsync(id).Result;
            if (res.Count() == 0)
                return default(E);
            return res.First();

        }

        private async Task<IEnumerable<E>> GetByIdAsync(string id)
        {
            var filter = Builders<E>.Filter.Eq("Id", id);
            return (await _collection.Find(filter).ToListAsync());
        }

        public void Update(string id, E e)
        {
            UpdateAsync(id, e);
        }

        private async void UpdateAsync(string id, E e)
        {
            var filter = Builders<E>.Filter.Eq("Id", id);

            await _collection.ReplaceOneAsync(filter, e);
        }

        public IEnumerable<E> GetList(string sort, string order, int start, int end)
        {
            List<E> es = GetListAsync(sort,order).Result.ToList();

            if (es.Count() - start > end - start)
                return es.GetRange(start, (end - start));
            else
                if (es.Count() > start)
                  return es.GetRange(start, (es.Count - start));
                else
                  return null;
        }

        private async Task<IEnumerable<E>> GetListAsync(string sort, string order)
        {
            var filter = new BsonDocument();
            if(order=="ASC")
                return (await _collection.Find(filter).Sort(Builders<E>.Sort.Ascending(order)).ToListAsync());
            else
                return (await _collection.Find(filter).Sort(Builders<E>.Sort.Descending(order)).ToListAsync());
        }
    }
}
