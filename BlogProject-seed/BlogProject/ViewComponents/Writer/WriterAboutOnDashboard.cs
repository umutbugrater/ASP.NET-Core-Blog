using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents.Writer
{
    public class WriterAboutOnDashboard : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            Context context = new Context();
            var usermail = User.Identity.Name;
            var writer = context.Users.Where(x=>x.Email == usermail).FirstOrDefault();
            return View(writer);
        }
    }
}
