using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Storages
{
    public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        private readonly CityRouteContext _context;
        private readonly DbSet<T> _entity;
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
            if (typeof(T).Name == "Map")
            { 
                DbSet<Map> entity = _context.Set<Map>();
                return entity.Include(p => p.Image).SingleOrDefault(p => p.Id == id) as T;
            } else {
                return _entity.SingleOrDefault(p => p.Id == id);
            }
        }

        public T Update(T obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
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
            _context.SaveChanges();

            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable();
        }
    }
}
