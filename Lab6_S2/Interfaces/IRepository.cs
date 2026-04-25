using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRepository<T> 
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T dowolny);
        void Update(T dowolny);
        void Delete(int id);

    }
}
