using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class CommentController : Controller
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        Context c = new Context();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CommentAddByBlog(Comment p, int id)
        {
            var usermail = User.Identity.Name;
            var user = c.Users.Where(x=>x.Email==usermail).FirstOrDefault();
            p.AppUserID = user.Id;
            p.CommentDate = DateTime.Now;
            p.BlogID = id;
            commentManager.TAdd(p);
            return RedirectToAction("BlogReadAll", "Blog", new { id = id });
        }
    }
}
