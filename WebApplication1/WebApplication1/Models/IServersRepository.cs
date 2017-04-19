using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Models
{
    public interface IServersRepository
    {
        Server Add(Server sv);
        IEnumerable<Server> GetAll();
        Server GetById(int id);
        void Delete(int sv);
        void Update(Server sv);
        IEnumerable<Server> GetList(string sort, string order, int start, int end);
    }
}
