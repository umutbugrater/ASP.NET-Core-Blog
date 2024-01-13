using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents.Blog
{
    public class SameCategoryBlogList : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IViewComponentResult Invoke(int id)
        {
            int catID = bm.TGetByID(id).CategoryID;
            var values = bm.TGetList().Where(x => x.CategoryID == catID).ToList();
            var gelenBlog = bm.TGetByID(id);
            values.RemoveAll(x => x.BlogID == id);  //Sayfada gözüken blog, yandaki gösterilen aynı kategoriler içerisinde gözükmemesi için
            return View(values);
        }
    }
}
