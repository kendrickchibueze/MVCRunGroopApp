using Microsoft.AspNetCore.Mvc;
using RunGroopApp.Interfaces;

namespace RunGroopApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService _clubService;
        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
                
        }
        public async Task<IActionResult> Index()
        {
            var clubs = await _clubService.GetAllClubsAsync();
            return View(clubs);
        }
    }
}
