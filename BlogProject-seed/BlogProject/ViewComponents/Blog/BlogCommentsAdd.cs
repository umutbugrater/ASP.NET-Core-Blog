using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents.Blog
{
    public class BlogCommentsAdd : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            ViewBag.i = id;
            return View();
        }
    }
}
