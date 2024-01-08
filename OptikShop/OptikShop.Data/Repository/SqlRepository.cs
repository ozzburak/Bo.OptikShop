﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OptikShop.Data.Context;
using OptikShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Data.Repository
{
    public class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly OptikShopContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public SqlRepository(OptikShopContext db)
        {
                _db = db;
            _dbSet = _db.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            
             entity.IsDeleted = true;
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            _db.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is not null ? _dbSet.Where(predicate) : _dbSet;
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            _db.SaveChanges();
        }
    }
}
