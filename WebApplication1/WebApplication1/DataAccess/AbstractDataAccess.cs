using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication1.DataAccess
{

    public class AbstractDataAccess<E> : IRepository<E>  
    {
       

        private IMongoClient _client;
        private IMongoDatabase _db;
        protected IMongoCollection<E> _collection;

       
        public AbstractDataAccess(string name)
        {
            _client = new MongoClient();
            _db = _client.GetDatabase("NginxDB");
            _collection = _db.GetCollection<E>(name);
        }

        public async void GenerateIndexesAsync()//TODO
        {
            var options = new CreateIndexOptions() { Unique = true };
            var field = new StringFieldDefinition<E>("Name");
            var indexDefinition = new IndexKeysDefinitionBuilder<E>().Ascending(field);
            await _collection.Indexes.CreateOneAsync(indexDefinition, options);
        }
    

        public E Add(E e)
        { 
            try{
                AddAsync(e);
                return e;   
            }catch{//TODO
                throw;
            }
        }

        private async void AddAsync(E e) 
        {
            try{//TODO
                await _collection.InsertOneAsync(e);
            }catch{
                throw ;
            }

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

            //to return only the number of items requested
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
            var _sort = FirstCharToUpper(sort);//name=> Name
            var filter = new BsonDocument();
            if(order=="ASC")
                return (await _collection.Find(filter).Sort(Builders<E>.Sort.Ascending(_sort)).ToListAsync());
            else
                return (await _collection.Find(filter).Sort(Builders<E>.Sort.Descending(_sort)).ToListAsync());
        }

        private string FirstCharToUpper(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
