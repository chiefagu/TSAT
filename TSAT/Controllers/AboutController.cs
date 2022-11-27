using Microsoft.AspNetCore.Mvc;

namespace TSAT.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
