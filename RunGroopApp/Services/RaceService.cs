using Microsoft.EntityFrameworkCore;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;

namespace RunGroopApp.Services
{
    public class RaceService : IRaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Race> _raceRepo;
        private readonly ILoggerManager _logger;


        public RaceService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _raceRepo = _unitOfWork.GetRepository<Race>();

        }
        public async Task<IReadOnlyList<Race>> GetAllRacesAsync()
        {
            var races = await _raceRepo.ListAllAsync();
            return races;
        }

        public async Task<Race> GetRaceByIdAsync(int id)
        {
            var club = await _raceRepo.GetFirstOrDefaultAsync(p => p.Id == id, include: query => query.Include(x => x.Address));
            if (club == null) _logger.LogError($"Failed to retrieve race with an {id} with an address");
            return club;
        }

        public async Task<IEnumerable<Race>> GetAllRacesByCity(string city)
        {
            var raceByCity = await _raceRepo.GetAllAsync(x => x.Address.City.Contains(city));
            if (raceByCity == null) _logger.LogError($"Failed to retrieve race with city name {city}");
            return raceByCity;
        }

        public bool AddRace(Race race)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRace(Race race)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRace(int id)
        {
            throw new NotImplementedException();
        }
    }
}
