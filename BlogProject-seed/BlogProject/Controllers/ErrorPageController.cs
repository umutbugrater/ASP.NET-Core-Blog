using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
	public class ErrorPageController : Controller
	{
		public IActionResult Index(int code)
		{
			return View();
		}
	}
}
