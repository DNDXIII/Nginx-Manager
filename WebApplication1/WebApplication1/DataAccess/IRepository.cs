using System.Collections.Generic;

namespace WebApplication1.DataAccess
{
    public interface IRepository<E>
    {
        E Add(E e);
        IEnumerable<E> GetAll();
        E GetById(string id);
        bool Delete(string id);
        void Update(string id, E e);
        IEnumerable<E> GetList(string sort, string order, int start, int end);
    }
}
