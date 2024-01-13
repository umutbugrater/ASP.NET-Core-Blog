using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            Context c = new Context();
            var usermail = User.Identity.Name;
            var writerID = c.Users.Where(x=> x.Email == usermail).FirstOrDefault().Id;
            ViewBag.blog = c.Blogs.ToList().Count();
            ViewBag.userBlog = c.Blogs.Where(x => x.AppUserId == writerID).Count();
            ViewBag.category = c.Categories.Count();
            return View();
        }

    }
}
