using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class WriterManager : IWriterService
    {
        IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public void TAdd(Writer t)
        {
           _writerDal.TAdd(t);
        }

        public void TDelete(Writer t)
        {
           _writerDal.TDelete(t);
        }

        public Writer TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Writer> TGetList()
        {
            throw new NotImplementedException();
        }

        public List<Writer> TGetList(Expression<Func<Writer, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Writer t)
        {
            throw new NotImplementedException();
        }
    }
}
