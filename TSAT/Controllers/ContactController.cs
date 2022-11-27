using Microsoft.AspNetCore.Mvc;

namespace TSAT.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
