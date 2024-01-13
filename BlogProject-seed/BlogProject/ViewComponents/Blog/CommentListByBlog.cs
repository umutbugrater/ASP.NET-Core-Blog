using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents.Blog
{
    public class CommentListByBlog : ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());

        public IViewComponentResult Invoke(int  id)
        {
            var values = commentManager.GetCommentListWithUser().Where(x => x.BlogID == id).ToList();
            return View(values);
        }
    }
}
