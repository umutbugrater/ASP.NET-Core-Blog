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
    public class BlogManager : IBlogService
    {
        IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public List<Blog> GetBlogListWithCategoryAndWriter()
        {
            return _blogDal.GetBlogListWithCategoryAndWriter();
        }

        public List<Blog> GetBlogListWithCategoryByWriter(int id)
        {
           return _blogDal.GetBlogListWithCategoryByWriter(id);
        }

        public void TAdd(Blog t)
        {
           _blogDal.TAdd(t);
        }

        public void TDelete(Blog t)
        {
          _blogDal.TDelete(t);
        }

        public Blog TGetByID(int id)
        {
           return _blogDal.TGetByID(id);
        }

        public List<Blog> TGetList()
        {
            return _blogDal.TGetList();
        }

        public List<Blog> TGetList(Expression<Func<Blog, bool>> filter)
        {
            return _blogDal.TGetList(filter);
        }

        public void TUpdate(Blog t)
        {
           _blogDal.TUpdate(t);
        }
    }
}
