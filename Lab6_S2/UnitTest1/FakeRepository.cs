using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
namespace UnitTest1
{
    public class FakeRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _data = new List<T>();

        public void Add(T entity)
        {
            if (entity != null)
            {
                _data.Add(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _data.Remove(entity);
            }
        }

        public IQueryable<T> GetAll()
        {
           
            return _data.AsQueryable();
        }

        public T GetById(int id)
        {
            
            PropertyInfo prop = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty(typeof(T).Name + "Id");

            if (prop != null)
            {
                return _data.FirstOrDefault(e => (int)prop.GetValue(e) == id);
            }

            return null;
        }

        public void Update(T entity)
        {
            
        }
    }
}
