using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents.Blog
{
    public class MostCommentedTakeBlogList : ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        public IViewComponentResult Invoke()
        {
            var group = commentManager.TGetList();

            var bloglar = group.GroupBy(x => x.BlogID)
                .Select(blog => new { blogId = blog.Key, Sayisi = blog.Count() })
                .OrderByDescending(x => x.Sayisi);

            int[] BlogIndex = new int[bloglar.Count()];
            int[] CommentCount = new int[bloglar.Count()];  
            int i = 0;
            foreach (var Satir in bloglar)
            {
                BlogIndex[i] = Satir.blogId;
                CommentCount[i] = Satir.Sayisi;
                i++;
            }

            ViewBag.MostCommentsOrderByBlogId = BlogIndex;
            ViewBag.CommentCount = CommentCount;    
            return View(blogManager.TGetList());
        }
    }
}
