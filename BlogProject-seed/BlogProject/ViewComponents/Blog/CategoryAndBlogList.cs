using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents.Blog
{
    public class CategoryAndBlogList : ViewComponent
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IViewComponentResult Invoke()
        {
            var blogList = bm.GetBlogListWithCategoryAndWriter();
            var blogCategoriesID = blogList.GroupBy(x => x.CategoryID).Select(x => new { id = x.Key, sayi = x.Count() });
            string[] catName = new string[blogCategoriesID.Count()];
            int[] catCount = new int[blogCategoriesID.Count()];
            int i = 0;
            foreach (var category in blogCategoriesID)
            {
                catName[i] = categoryManager.TGetByID(category.id).CategoryName;
                catCount[i] = category.sayi;
                i++;
            }
            ViewBag.catCount = catCount;
            ViewBag.categories = catName;
            ViewBag.categoriesCount = catName.Count();
            
            var allCategories = categoryManager.TGetList().Select(x=>x.CategoryName).ToList();
            var blogCategoriesName = blogList.DistinctBy(x => x.CategoryID).Select(x => x.Category.CategoryName).ToList();
            var bosKategoriler = allCategories.Except(blogCategoriesName).ToList();
            ViewBag.bosKategoriler = bosKategoriler;
            ViewBag.bosKategorilerCount = bosKategoriler.Count();

            return View(blogList);
        }
    }
}
