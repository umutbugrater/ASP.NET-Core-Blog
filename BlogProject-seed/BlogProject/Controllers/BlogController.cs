using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace BlogProject.Controllers
{

    public class BlogController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        Context c = new Context();
        [AllowAnonymous]
        public IActionResult Index(int page=1)
        {
            var values = blogManager.GetBlogListWithCategoryAndWriter().ToPagedList(page, 9);
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            
            ViewBag.i = id;
            var values = blogManager.TGetByID(id);
            
            return View(values);
        }
        [HttpGet]
        public IActionResult BlogAdd()
        {
            //List<SelectListItem> categoryvalues = (from x in categoryManager.TGetList()
            //                                       select new SelectListItem
            //                                       {
            //                                           Text = x.CategoryName,
            //                                           Value = x.CategoryID.ToString(),
            //                                       }).ToList();
            List<SelectListItem> categoryvalues = new List<SelectListItem>();
            categoryvalues.Add(new SelectListItem
            {
                Text = "Bir Kategori seçiniz..",
                Value = null
            });
            foreach (var item in categoryManager.TGetList())
            {
                categoryvalues.Add(new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.CategoryID.ToString(),
                });
            }

            ViewBag.CategoryValues = categoryvalues;
            return View();
        }
        [HttpPost]
        public IActionResult BlogAdd(Blog b, IFormFile img)
        {
            var usermail = User.Identity.Name;
            var writerID = c.Users.Where(x=>x.Email==usermail).Select(x=>x.Id).FirstOrDefault();
            b.AppUserId = writerID;
            if (img != null)
            {
                string uzantı = Path.GetExtension(img.FileName);
                string resimAdi = Guid.NewGuid() + uzantı;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/BlogImages/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                b.BlogImage = resimAdi;
            }
            BlogValidator bv = new BlogValidator();
            ValidationResult results = bv.Validate(b);
            List<SelectListItem> categoryvalues = new List<SelectListItem>();
            categoryvalues.Add(new SelectListItem
            {
                Text = "Bir Kategori seçiniz..",
                Value = null
            });
            foreach (var item in categoryManager.TGetList())
            {
                categoryvalues.Add(new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.CategoryID.ToString(),
                });
            }

            ViewBag.CategoryValues = categoryvalues;
            if (results.IsValid)
            {
                b.BlogCreateDate = DateTime.Parse(DateTime.Now.ToString());
                blogManager.TAdd(b);
                return RedirectToAction("BlogListByWriter","Blog");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public IActionResult BlogListByWriter()
        {
            var usermail = User.Identity.Name;
            var writerID = c.Users.Where(x => x.Email == usermail).Select(x=>x.Id).FirstOrDefault();
            var values = blogManager.GetBlogListWithCategoryByWriter(writerID);
            return View(values);
        }
        public IActionResult BlogListByCategoryId(int id)
        {
            var values = blogManager.GetBlogListWithCategoryAndWriter().Where(x => x.CategoryID == id).ToList();
            var blogID = blogManager.GetBlogListWithCategoryAndWriter().Where(x => x.CategoryID == id).Select(x=>x.BlogID).ToList();
            ViewBag.catName = categoryManager.TGetByID(id).CategoryName;


            int[] blogsCommentCount = new int[blogManager.TGetList().OrderByDescending(x => x.BlogID).FirstOrDefault().BlogID + 1];
            int[] blogsSumScore = new int[blogManager.TGetList().OrderByDescending(x => x.BlogID).FirstOrDefault().BlogID + 1];
            for (int i = 0; i < blogsCommentCount.Length; i++)
            {
                blogsCommentCount[i] = commentManager.TGetList().Where(x => x.BlogID == i).Count();
                blogsSumScore[i] = commentManager.TGetList().Where(x => x.BlogID == i).Sum(x => x.BlogScore);
            }
            ViewBag.blogsCommentCount = blogsCommentCount;
            ViewBag.blogsSumScore = blogsSumScore;
            return View(values);
        }
        public IActionResult BlogDelete(int id)
        {
            var blog = blogManager.TGetByID(id);
            blogManager.TDelete(blog);
            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult BlogEdit(int id)
        {
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var cat in categoryManager.TGetList())
            {
                categoryList.Add(new SelectListItem
                {
                    Text = cat.CategoryName,
                    Value = cat.CategoryID.ToString()
                });
            }
            ViewBag.cat = categoryList;
            var blog = blogManager.TGetByID(id);
            return View(blog);
        }
        [HttpPost]
        public IActionResult BlogEdit(Blog blog,IFormFile img)
        {
            var value = blogManager.TGetByID(blog.BlogID); //sadece oluşturma tarihini almak için kullandık
            if (img != null)
            {
                string uzanti = Path.GetExtension(img.FileName);
                string resimAdi = Guid.NewGuid() + uzanti;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/BlogImages/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                blog.BlogImage = resimAdi;
            }
            else
            {
                blog.BlogImage = value.BlogImage;
            }
            var usermail = User.Identity.Name;
            var writerId = c.Users.Where(x=>x.Email==usermail).Select(x=>x.Id).FirstOrDefault();
            blog.BlogCreateDate = value.BlogCreateDate;
            blog.AppUserId = writerId;
            blogManager.TUpdate(blog);
            return RedirectToAction("BlogListByWriter");

        }
    }
}
