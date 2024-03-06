using Microsoft.AspNetCore.Mvc;
using RunGroopApp.Interfaces;
using RunGroopApp.Services;

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

        public async Task<IActionResult> Detail(int id)
        {
            var race = await _raceService.GetRaceByIdAsync(id);
            return View(race);
        }

    }
}
