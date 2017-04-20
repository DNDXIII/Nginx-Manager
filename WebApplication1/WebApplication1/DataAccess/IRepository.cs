using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.DataAccess
{
    public interface IRepository<E>
    {
        E Add(E e);
        IEnumerable<E> GetAll();
        E GetById(string id);
        void Delete(string id);
        void Update(string id, E e);
        IEnumerable<E> GetList(string sort, string order, int start, int end);
    }
}
