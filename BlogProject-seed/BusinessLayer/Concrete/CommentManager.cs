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
    public class CommentManager : ICommentService
    {
         ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public List<Comment> GetCommentListWithUser()
        {
           return _commentDal.GetCommentListWithUser();
        }

        public List<Comment> GetCommentListWithUserAndBlog()
        {
           return _commentDal.GetCommentListWithUserAndBlog();
        }

        public void TAdd(Comment t)
        {
            _commentDal.TAdd(t);
        }

        public void TDelete(Comment t)
        {
            throw new NotImplementedException();
        }

        public Comment TGetByID(int id)
        {
            return _commentDal.TGetByID(id);
        }

        public List<Comment> TGetList()
        {
            return _commentDal.TGetList();
        }

        public List<Comment> TGetList(Expression<Func<Comment, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Comment t)
        {
            throw new NotImplementedException();
        }
    }
}
