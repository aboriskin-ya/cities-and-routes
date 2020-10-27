using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Storage
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _entity;
        public Repository(CityRouteContext context)
        {
            _entity = context.Set<T>();
        }

        public void Add(T obj)
        {
            obj.CreateOnUTC = DateTimeOffset.Now;
            _entity.Add(obj);
        }

        public T Get(Guid id)
        {
            return _entity.SingleOrDefault(p => p.Id == id);
        }

        public T Update(T obj)
        {
            obj.UpdatedOnUTC = DateTimeOffset.Now;
            _entity.Update(obj);
            return obj;
        }
        public bool Delete(Guid id)
        {
            var obj = _entity.Single(p => p.Id == id);
            if (obj is null)
            {
                return false;
            }

            _entity.Remove(obj);

            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable();
        }
    }
}
