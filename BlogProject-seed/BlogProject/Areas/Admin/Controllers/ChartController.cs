using BlogProject.Areas.Admin.Models;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ChartController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        CategoryManager catManager = new CategoryManager(new EfCategoryRepository());
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CategoryChart()
        {
            var blogList = bm.TGetList();
            var catList = catManager.TGetList();
            var grouped = blogList.GroupBy(x => x.CategoryID);
            var value = grouped.Select(x => new { catId = x.Key, sayisi = x.Count() }).OrderBy(x=>x.catId);


            List<ChartModel> model = new List<ChartModel>();

            var categorList = catManager.TGetList().Select(x => x.CategoryID).ToList();

            var blogDistincList = blogList.DistinctBy(x => x.CategoryID).Select(x => x.CategoryID).ToList();

            var a = categorList.Except(blogDistincList).ToArray();  //Kategori listesinde olup, blog listesinde olmayan ıd leri getiriyor.

            //chartlarda 0 değeri gözükmediği için bunu yapmamızın bi anlamı yok
            for (int i = 0; i < a.Count(); i++)
            {
                model.Add(new ChartModel
                {
                    categoryname = catList.Where(x => x.CategoryID == a[i]).FirstOrDefault().CategoryName,
                    categoryblogcount = 0
                });
            }

            foreach (var item in value)
            {
                model.Add(new ChartModel
                {
                    categoryname = catList.Where(x => x.CategoryID == item.catId).FirstOrDefault().CategoryName,
                    categoryblogcount = item.sayisi
                });
            }
            return Json(new { jsonlist = model });
        }
    }
}
