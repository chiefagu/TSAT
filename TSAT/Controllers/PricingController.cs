using Microsoft.AspNetCore.Mvc;

namespace TSAT.Controllers
{
	public class PricingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
