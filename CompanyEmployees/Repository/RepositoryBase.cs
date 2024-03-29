﻿using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
            //RepositoryContext.SaveChanges();
        }

        public void CreateCollection(IEnumerable<T> entities)
        {
            RepositoryContext.Set<T>().AddRange(entities);
            //RepositoryContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
            //RepositoryContext.SaveChanges();
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ? RepositoryContext.Set<T>().AsNoTracking() : RepositoryContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ? RepositoryContext.Set<T>().Where(expression).AsNoTracking() : RepositoryContext.Set<T>().Where(expression);
        }

        public T GetById(Guid Id, bool trackChanges)
        {
            return RepositoryContext.Set<T>()
                .Find(Id);
        }

        public T GetFirstInclude(Expression<Func<T, bool>> expression, Expression<Func<T, object>> includeExpression, bool trackChanges)
        {
            return !trackChanges ? RepositoryContext.Set<T>().Where(expression).Include(includeExpression).AsNoTracking().FirstOrDefault() :
             RepositoryContext.Set<T>().Where(expression).Include(includeExpression).AsTracking().FirstOrDefault();
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
            //RepositoryContext.SaveChanges();
        }
    }
}