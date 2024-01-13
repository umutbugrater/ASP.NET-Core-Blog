using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents.Blog
{
    public class CategoryListAndBlogCount : ViewComponent
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        public IViewComponentResult Invoke()
        {

            int[] categoryCount = new int[categoryManager.TGetList().OrderByDescending(x => x.CategoryID).FirstOrDefault().CategoryID + 1];
            for (int i = 0; i < categoryCount.Length; i++)
            {
                categoryCount[i] = blogManager.TGetList().Where(x => x.CategoryID == i).Count();
            }

            ViewBag.categoryCount = categoryCount;
            return View(categoryManager.TGetList());
        }
    }
}
