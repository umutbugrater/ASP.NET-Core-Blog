using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void TAdd(T t);
        void TDelete(T t);
        void TUpdate(T t);
        T TGetByID(int id);
        List<T> TGetList();
        List<T> TGetList(Expression<Func<T, bool>> filter);
    }
}
