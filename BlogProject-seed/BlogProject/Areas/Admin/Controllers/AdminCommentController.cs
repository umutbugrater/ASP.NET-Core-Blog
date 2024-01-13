using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BlogProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminCommentController : Controller
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        public IActionResult Index(int page=1)
        {
            var values = commentManager.GetCommentListWithUserAndBlog().ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult CommentDetails(int id)
        {
            var value = commentManager.GetCommentListWithUserAndBlog().Find(x=>x.CommentID == id);
            return View(value);

        }
    }
}
