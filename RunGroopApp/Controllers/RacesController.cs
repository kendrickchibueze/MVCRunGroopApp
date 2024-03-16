using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;
using RunGroopApp.Services;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Controllers
{
    public class RacesController : Controller
    {
        private readonly IRaceService _raceService;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public RacesController(IRaceService raceService, IPhotoService photoService, IMapper mapper)
        {
            _raceService = raceService;
            _photoService = photoService;
            _mapper = mapper;
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
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = _mapper.Map<Race>(raceVM);
                race.Image = result.Url.ToString();
                await _raceService.AddRace(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(raceVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceService.GetRaceByIdAsync(id);
            if (race == null)
            {
                return NotFound();
            }
            var editRaceVM = _mapper.Map<EditRaceViewModel>(race);
            return View(editRaceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel editRaceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View(editRaceVM);
            }

            var race = await _raceService.GetRaceByIdAsync(id);
            if (race == null)
            {
                return NotFound();
            }
            try
            {
                if (editRaceVM.Image != null)
                {
                    await _photoService.DeletePhotoAsync(race.Image);
                    var uploadResult = await _photoService.AddPhotoAsync(editRaceVM.Image);
                    race.Image = uploadResult.Url.ToString();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete or upload photo");
                return View(editRaceVM);
            }
            _mapper.Map(editRaceVM, race);
            await _raceService.UpdateRace(id, race);
            return RedirectToAction("Index");
        }


    }
}
