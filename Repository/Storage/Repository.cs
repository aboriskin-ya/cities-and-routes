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

        public T Get(int id)
        {
            return _entity.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable();
        }
    }
}
