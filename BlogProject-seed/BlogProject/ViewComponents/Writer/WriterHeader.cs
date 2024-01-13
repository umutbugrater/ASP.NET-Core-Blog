using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents.Writer
{
    public class WriterHeader : ViewComponent
    {
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            var usermail = User.Identity.Name;
            var values = c.Users.Where(x=>x.Email == usermail).Select(x=>x.ImageUrl).FirstOrDefault();
            ViewBag.image = values;
            return View();
        }
    }
}
