using BlogProject.Models;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace BlogProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<Context>();           //Identity

            builder.Services.AddIdentity<AppUser, AppRole>(x =>   //Validationdakilere uygun olarak ayarlad�m.
            {
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<Context>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddMvc(config =>                               //Proje seviyesinde Authorize i�lemi kullanabilecez. 
            {                                                                    //Yani t�m sayfalar� g�rebilmek i�in giri� yapmal�. Hi�bir sayfay� g�remiyoruz
                var policy = new AuthorizationPolicyBuilder()                          // AllowAnonymous komutu sayesinde sayfalar� g�rebiliyoruz
                                     .RequireAuthenticatedUser()
                                     .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            builder.Services.ConfigureApplicationCookie(options => 
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(115);
                options.AccessDeniedPath = new PathString("/RegisterORLogin/AccessDenied");  //Yetkisiz eri�im yerine gerince geliyo
                options.LoginPath = "/RegisterORLogin/Index";
                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Index", "?code={0}");


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Blog}/{action=Index}/{id?}");

			using (var scope = app.Services.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

				var roles = new[] { "Admin", "Writer", "Member", "Moderator" };

				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role))
					{
						await roleManager.CreateAsync(new AppRole
						{
							Name = role
						});
					}
				}
			}


			using (var scope = app.Services.CreateScope())
			{
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

				string email = "admin@admin.com";
				string password = "Umut.2001";
				
				if (await userManager.FindByEmailAsync(email) == null)
				{
					var user = new AppUser();
					user.UserName = email;
					user.Email = email;
					user.NameSurname = "Umut Bu�ra TER";
					user.ImageUrl = "751b677c-21a3-4cee-a25b-1b7a72ca5211.jpg";
					user.About = "Merhaba. Ben Umut Bu�ra. Bilgisayar M�hendisiyim. Bal�kesir �niversitesi mezunuyum. Yeni mezun oldum. Kendimi geli�tirebilece�im ve i�imi en iyi �ekilde yapacak bir i� yeri ar�yorum.Merhaba. Ben Umut Bu�ra. Bilgisayar M�hendisiyim. Bal�kesir �niversitesi mezunuyum. Yeni mezun oldum. Kendimi geli�tirebilece�im ve i�imi en iyi �ekilde yapacak bir i� yeri ar�yorum.Merhaba. Ben Umut Bu�ra. Bilgisayar M�hendisiyim. Bal�kesir �niversitesi mezunuyum. Yeni mezun oldum. Kendimi geli�tirebilece�im ve i�imi en iyi �ekilde yapacak bir i� yeri ar�yorum.Merhaba. Ben Umut Bu�ra. Bilgisayar M�hendisiyim. Bal�kesir �niversitesi mezunuyum. Yeni mezun oldum. Kendimi geli�tirebilece�im ve i�imi en iyi �ekilde yapacak bir i� yeri ar�yorum";
					user.JobTitle = "Computer Engineer";
					await userManager.CreateAsync(user, password);
					await userManager.AddToRoleAsync(user, "Admin");
				}

                string email2 = "test@hotmail.com";
                string password2 = "123456a";
                if (await userManager.FindByEmailAsync(email2) == null)
				{
					var user = new AppUser();
					user.UserName = email2;
					user.Email = email2;
					user.NameSurname = "Eren Utku";
					user.ImageUrl = "no-image.jpg";
					await userManager.CreateAsync(user, password2);
					await userManager.AddToRoleAsync(user, "Writer");
				}
			}
            SeedData.EnsurePopulated(app);

			app.Run();
		}
    }
}