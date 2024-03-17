using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Services
{
    public class ClubService : IClubService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Club> _clubRepo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ClubService(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _clubRepo = _unitOfWork.GetRepository<Club>();

        }
        public async Task<IReadOnlyList<Club>> GetAllClubsAsync()
        {
            var clubs = await _clubRepo.ListAllAsync();
            return clubs;
        }
 
        public async  Task<Club> GetClubByIdAsync(int id)
        {           
            var club = await _clubRepo.GetFirstOrDefaultAsync(predicate: p => p.Id == id, include: query => query.Include(x => x.Address));
            if (club == null) _logger.LogError($"Failed to retrieve club with an {id} with an address");  
            return club;
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            IEnumerable<Club> clubByCity = await _clubRepo.GetAllAsync(filter:x=>x.Address.City == city);        
            if (clubByCity == null) _logger.LogError($"Failed to retrieve club with city name {city}");
            return clubByCity.ToList();
        }

        public async  Task<bool>  AddClub(Club club)
        {
            var addedClub = await  _clubRepo.AddAsync(club);
            if (addedClub == null) 
            {
                _logger.LogError("unable to add a new club");
                return false;
            }
            else
            {
                 _logger.LogInfo("New club added successfully");
                  return true;
            }
        }

        public async Task UpdateClub(int id, Club club)
        {
            Club singleClub = await _clubRepo.GetSingleByAsync(predicate:x => x.Id == id);
            if (singleClub == null)
                throw new InvalidOperationException("club with the Id does not exit");
            Club newClub = _mapper.Map(club, singleClub);
            await _clubRepo.UpdateAsync(newClub);
        }

        public async Task<Club> DeleteClub(int id)
        {
            var club = await _clubRepo.GetSingleByAsync(predicate:x=>x.Id==id);
            if (club == null)
                throw new InvalidOperationException($"club with the ${id} does not exist");
            await _clubRepo.DeleteByIdAsync(id);
            return club;
        }
    }
}
