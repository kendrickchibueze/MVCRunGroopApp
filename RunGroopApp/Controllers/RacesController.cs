using Microsoft.AspNetCore.Mvc;
using RunGroopApp.Interfaces;

namespace RunGroopApp.Controllers
{
    public class RacesController : Controller
    {
        private readonly IRaceService _raceService;
        public RacesController(IRaceService raceService)
        {
            _raceService = raceService;
        }
        public async Task<IActionResult> Index()
        {
            var races = await _raceService.GetAllRacesAsync();
            return View(races);
        }
    }
}
