using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BlogProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminCategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        public IActionResult Index(int page=1)
        {
            var values = categoryManager.TGetList().ToPagedList(page, 7);
            return View(values);
        }

        [HttpGet]
        public IActionResult CategoryAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryAdd(Category p)
        {
            if (ModelState.IsValid)
            {
                categoryManager.TAdd(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public IActionResult CategoryDelete(int id)
        {
            var value = categoryManager.TGetByID(id);
            categoryManager.TDelete(value);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CategoryUpdate(int id)
        {
            var value = categoryManager.TGetByID(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult CategoryUpdate(Category p)
        {
            if (ModelState.IsValid)
            {
                categoryManager.TUpdate(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }
    }
}
