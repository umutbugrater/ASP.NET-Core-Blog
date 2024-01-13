using EntityLayer.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    public class WriterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            var usermail = User.Identity.Name;  //username ile usermail değerleri aynı
            var values = await _userManager.FindByNameAsync(usermail);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.namesurname = values.NameSurname;
            model.email = values.Email;
            model.about = values.About;
            model.jobtitle = values.JobTitle;
            ViewBag.image = values.ImageUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel p, IFormFile img)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            if (img != null)
            {
                string uzanti = Path.GetExtension(img.FileName);
                string resimAdi = Guid.NewGuid() + uzanti;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/UserImages/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                values.ImageUrl = resimAdi;
            }
            values.Email = p.email;
            values.UserName = p.email;
            values.NormalizedUserName = p.email;
            values.About = p.about;
            values.JobTitle = p.jobtitle;
            values.NameSurname = p.namesurname;
            if (!p.ChangePassword)
            {
                values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, p.password);
                var result2 = await _userManager.UpdateAsync(values);
                return RedirectToAction("LogOut", "Login");

            }
            var result = await _userManager.UpdateAsync(values);
            return RedirectToAction("WriterEditProfile", "Writer");
        }
    }
}
