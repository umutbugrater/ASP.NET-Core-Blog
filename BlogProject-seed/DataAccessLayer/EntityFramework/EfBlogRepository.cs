using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfBlogRepository : EfGenericRepository<Blog>, IBlogDal
    {
        public List<Blog> GetBlogListWithCategoryAndWriter()
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(b => b.Category).Include(a=>a.AppUser).ToList();
            }
        }

        public List<Blog> GetBlogListWithCategoryByWriter(int id)
        {
            using (var c = new Context())
            {
               return c.Blogs.Include(x => x.Category).Where(x => x.AppUserId == id).ToList();
            }
        }
    }
}
