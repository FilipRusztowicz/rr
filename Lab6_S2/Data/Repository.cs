using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        
        public Repository(DbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            if (entity != null)
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable(); // Teraz zwraca IQueryable
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
        }
    }
}
