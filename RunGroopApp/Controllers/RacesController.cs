using Microsoft.AspNetCore.Mvc;

namespace RunGroopApp.Controllers
{
    public class RacesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
