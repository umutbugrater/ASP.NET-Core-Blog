using BlogProject.Areas.Admin.Models;
using BusinessLayer.Concrete;
using ClosedXML.Excel;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminDynamicRaporController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DynamicExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                //var worksheet = workbook.AddWorksheet("Blog Listesi");
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Name";
                worksheet.Cell(1, 3).Value = "Category Name";
                worksheet.Cell(1, 4).Value = "Writer Name Surname";

                int BlogRowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.BlogID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogTitle;
                    worksheet.Cell(BlogRowCount, 3).Value = item.CategoryName;
                    worksheet.Cell(BlogRowCount, 4).Value = item.WriterName;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BlogListesi.xlsx");
                }
            }
        }

        public List<BlogModel> BlogTitleList()
        {
            List<BlogModel> bm = new List<BlogModel>();
            BlogManager blogManager = new BlogManager(new EfBlogRepository());
            bm = blogManager.GetBlogListWithCategoryAndWriter().Select(x => new BlogModel
            {
                BlogID = x.BlogID,
                BlogTitle = x.BlogTitle,
                CategoryName = x.Category.CategoryName,
                WriterName = x.AppUser.NameSurname
            }).ToList();

            return bm;
        }

        public FileContentResult DynamicExcelUserList()
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Kullanıcı Listesi");
            worksheet.Cell(1, 1).Value = "User ID";
            worksheet.Cell(1, 2).Value = "User Name Surname";
            worksheet.Cell(1, 3).Value = "User Email";
            worksheet.Cell(1, 4).Value = "User About";
            worksheet.Cell(1, 5).Value = "User Job";

            int UserRowCount = 2;
            foreach (var item in UserList())
            {
                worksheet.Cell(UserRowCount, 1).Value = item.UserID;
                worksheet.Cell(UserRowCount, 2).Value = item.NameSurname;
                worksheet.Cell(UserRowCount, 3).Value = item.Email;
                worksheet.Cell(UserRowCount, 4).Value = item.About;
                worksheet.Cell(UserRowCount, 5).Value = item.Job;
                UserRowCount++;
            }

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "KullanıcıListesi.xlsx");
        }

        public List<UserModel> UserList()
        {
            List<UserModel> um = new List<UserModel>();
            Context c = new Context();
            um = c.Users.ToList().Select(x=>new UserModel()
            {
                UserID = x.Id,
                NameSurname = x.NameSurname,
                Email = x.Email,
                About = x.About,
                Job = x.JobTitle
            }).ToList();
            return um;
        }

    }
}
