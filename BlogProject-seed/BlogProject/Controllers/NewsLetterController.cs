using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class NewsLetterController : Controller
    {
        NewsLetterManager nm = new NewsLetterManager(new EfNewsLetterRepository());
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubscribeNewsLetter(NewsLetter p)
        {
            if (ModelState.IsValid)
            {
                nm.TAdd(p);
                return RedirectToAction("Index", "Blog");
            }
            return View();
        }
    }
}
