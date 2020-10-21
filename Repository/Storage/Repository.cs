using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Storages
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly CityRouteContext _context;
        protected readonly DbSet<T> _entity;
        public Repository(CityRouteContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public void Add(T obj)
        {
            obj.CreateOnUTC = DateTimeOffset.Now;
            if (!_context.Entry<T>(obj).IsKeySet)
            {
                _context.Add(obj);
            }
            else
            {
                _context.Update(obj);
            }
            _context.SaveChanges();
        }

        public T Get(Guid id)
        {
            return _entity.SingleOrDefault(p => p.Id == id);
        }

        public T Update(T obj)
        {
            obj.UpdatedOnUTC = DateTimeOffset.Now;
            _context.Update(obj);
            return obj;
        }
        public bool Delete(Guid id)
        {
            var obj = _entity.Single(p => p.Id == id);
            if (obj is null)
            {
                return false;
            }

            _context.Remove(obj);

            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable();
        }
    }
}
