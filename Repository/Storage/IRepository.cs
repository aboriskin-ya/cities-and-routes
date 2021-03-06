﻿using DataAccess;
using System;
using System.Collections.Generic;

namespace Repository.Storage
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Add(T obj);
        T Update(T obj);
        bool Delete(Guid id);
    }
}