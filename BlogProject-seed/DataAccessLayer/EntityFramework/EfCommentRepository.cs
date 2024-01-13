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
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetCommentListWithUser()
        {
            using (var c= new Context())
            {
                return c.Comments.Include(x=>x.AppUser).ToList();
            }
        }

        public List<Comment> GetCommentListWithUserAndBlog()
        {
            using(var c= new Context())
            {
                return c.Comments.Include(x => x.AppUser).Include(x => x.Blog).ToList();
            }
        }
    }
}
