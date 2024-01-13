using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.ViewComponents.Blog
{
    public class MostScoredTakeBlogList : ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IViewComponentResult Invoke()
        {
            var comments = commentManager.TGetList();
            var grouped = comments.GroupBy(x => x.BlogID)
                 .Select(x => new { blogId = x.Key, blogCountById = x.Count(), blogScore= x.Sum(y=>y.BlogScore)})
                 .OrderByDescending(x=>(x.blogScore)/(x.blogCountById));
            
            int[] BlogIndex = new int[grouped.Count()];
            double[] PuanSıralı = new double[grouped.Count()];
            int i = 0;
            foreach (var blog in grouped)
            {
                BlogIndex[i] = blog.blogId;
                PuanSıralı[i] = Math.Round((double)blog.blogScore / blog.blogCountById, 2);
                i++;
            }
            ViewBag.MostScoredOrderByBlogId = BlogIndex;
            ViewBag.Scores = PuanSıralı;
            return View(bm.TGetList());
        }
    }
}
