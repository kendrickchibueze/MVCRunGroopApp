using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService _clubService;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public ClubController(IClubService clubService, IPhotoService photoService, IMapper mapper)
        {
            _clubService = clubService;
            _photoService = photoService;
            _mapper = mapper;
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
        public async Task<IActionResult> Create()
        {       
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if(ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = _mapper.Map<Club>(clubVM);
                club.Image = result.Url.ToString();
                await _clubService.AddClub(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubService.GetClubByIdAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            var editClubVM = _mapper.Map<EditClubViewModel>(club);
            return View(editClubVM);
        }

        [HttpPost] 
        public async Task<IActionResult> Edit(int id, EditClubViewModel editClubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View(editClubVM);
            }

            var club = await _clubService.GetClubByIdAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            try
            {
                if (editClubVM.Image != null)
                {
                    await _photoService.DeletePhotoAsync(club.Image);
                    var uploadResult = await _photoService.AddPhotoAsync(editClubVM.Image);
                    club.Image = uploadResult.Url.ToString();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete or upload photo");
                return View(editClubVM);
            }
            _mapper.Map(editClubVM, club);
            await _clubService.UpdateClub(id, club);
            return RedirectToAction("Index");
        }

    }
}
