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
        DbContext context;
        public Repository(DbContext dbContext) { 
        context = dbContext;
        }

        public void Add(T dowolny)
        {
            if (dowolny != null)
            {
                context.Add(dowolny);
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {

            var entity = context.Set<T>().Find(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }

        }
        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update(T dowolny)
        {
            context.Set<T>().Update(dowolny);
            context.SaveChanges();
        }
    }
}
