﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TaskManager.DataAccess.Repositories.Abstractions
{
    public interface IRepositoryBase<T>
    {
        List<T> FindAll();
        T FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        public void Save();
    }
}
