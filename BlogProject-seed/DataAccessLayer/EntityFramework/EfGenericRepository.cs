using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfGenericRepository<T> : IGenericDal<T> where T : class
    {
        public void TAdd(T t)
        {
            using (var c = new Context())
            {
                c.Set<T>().Add(t);
                c.SaveChanges();
            }
        }

        public void TDelete(T t)
        {
            using (var c = new Context())
            {
                c.Set<T>().Remove(t);
                c.SaveChanges();
            }
        }

        public T TGetByID(int id)
        {
            using (var c = new Context())
            {
               return c.Set<T>().Find(id);
            }
        }

        public List<T> TGetList()
        {
            using (var c = new Context()) 
            {
                return c.Set<T>().ToList();
            }
        }

        public List<T> TGetList(Expression<Func<T, bool>> filter)
        {
            using (var c = new Context())
            {
                return c.Set<T>().Where(filter).ToList();
            }
        }

        public void TUpdate(T t)
        {
            using (var c = new Context())
            {
                c.Set<T>().Update(t);
                c.SaveChanges();
            }
        }
    }
}
