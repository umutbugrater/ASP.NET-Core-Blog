using EntityLayer.Models;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [AllowAnonymous]
    public class RegisterORLoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public RegisterORLoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserSignInViewModel p)
        {
            UserSignInViewModelValidator uv = new UserSignInViewModelValidator();
            ValidationResult results = uv.Validate(p);
            if (results.IsValid)
            {
                //                        ---,---, çerezlerde hatırlansın mı?, 5 kere yanlış girildiğinde sisteme giriş belli süre(5dk) engellecenek 
                var result = await _signInManager.PasswordSignInAsync(p.email, p.password, false, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(p);

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Blog");
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel p, IFormFile img)
        {
            UserRegisterViewModelValidator rv = new UserRegisterViewModelValidator();
            ValidationResult results = rv.Validate(p);
            if (results.IsValid)
            {
                if (img != null)
                {

                    string uzanti = Path.GetExtension(img.FileName);
                    string resimAdi = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/UserImages/{resimAdi}");
                    using var stream = new FileStream(path, FileMode.Create);
                    img.CopyTo(stream);

                    AppUser user = new AppUser()
                    {
                        Email = p.Mail,
                        UserName = p.Mail,
                        //UserName = p.UserName, normalde böyle olması lazım ama email ile giriş yapmak için bunada mail adresi verdim
                        NameSurname = p.NameSurname,
                        ImageUrl = resimAdi
                    };
                    //identity kütüphanesinde, şifre metot çağrılırken giriliyor
                    var result = await _userManager.CreateAsync(user, p.Password);
                    await _userManager.AddToRoleAsync(user, "Writer");
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", "RegisterORLogin");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View(p);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
