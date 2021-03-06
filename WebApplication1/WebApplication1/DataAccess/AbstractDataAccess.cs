﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication1.DataAccess
{

    public class AbstractDataAccess<E> : IRepository<E>  
    {
       

        protected IMongoDatabase _db;
        protected IMongoCollection<E> _collection;

       
        public AbstractDataAccess(string name, string connectionString)
        {
            var databaseName = connectionString.Split('/').Last();

            var _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(databaseName);
            _collection = _db.GetCollection<E>(name);
        }

        public E Add(E e)
        { 
            try{
                AddAsync(e);
                return e;   
            }catch{
                throw;
            }
        }

        private async void AddAsync(E e) 
        {
            try{
                await _collection.InsertOneAsync(e);
            }catch{
                throw ;
            }

        }

        public virtual bool Delete(string id)
        {
            DeleteAsync(id);
            return true;
        }

        public virtual async void DeleteAsync(string id)
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
                  return new List<E>();//empty list 
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
