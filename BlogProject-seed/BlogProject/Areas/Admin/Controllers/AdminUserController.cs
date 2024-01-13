using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BlogProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminUserController : Controller
    {
        Context c = new Context();
        public IActionResult Index(int page=1)
        {
            var users = c.Users.ToList().ToPagedList(page, 9);
            return View(users);
        }
    }
}
