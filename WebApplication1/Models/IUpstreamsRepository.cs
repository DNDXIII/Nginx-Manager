using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public interface IUpstreamsRepository
    {
        Upstream Add(Upstream up);
        IEnumerable<Upstream> GetAll();
        Upstream GetById(int id);
        void Delete(int id);
        void Update(Upstream up);
    }
}
