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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void TAdd(Category t)
        {
            _categoryDal.TAdd(t);
        }

        public void TDelete(Category t)
        {
            _categoryDal.TDelete(t);
        }

        public Category TGetByID(int id)
        {
            return _categoryDal.TGetByID(id);
        }

        public List<Category> TGetList()
        {
            return _categoryDal.TGetList();
        }

        public List<Category> TGetList(Expression<Func<Category, bool>> filter)
        {
            return _categoryDal.TGetList(filter);
        }

        public void TUpdate(Category t)
        {
            _categoryDal.TUpdate(t);
        }
    }
}
