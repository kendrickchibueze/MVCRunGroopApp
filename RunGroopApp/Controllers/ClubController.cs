﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Detail(int id)
        {
            var club = await _clubService.GetClubByIdAsync(id);
            return View(club);
        }




    }
}
