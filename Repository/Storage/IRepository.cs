using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Storages
{
    public interface IRepository<T> where T: BaseEntity
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Add(T obj);
        T Update(T obj);
        bool Delete(Guid id);
    }
}
