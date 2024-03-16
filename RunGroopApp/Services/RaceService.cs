using AutoMapper;
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
        private readonly IMapper _mapper;

        public RaceService(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
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

        public async Task<bool> AddRace(Race race)
        {
            var addedRace = await _raceRepo.AddAsync(race);
            if (addedRace == null)
            {
                _logger.LogError("unable to add a new Race");
                return false;
            }
            else
            {
                _logger.LogInfo("New Race added successfully");
                return true;
            }
        }
        public async Task UpdateRace(int id, Race race)
        {
            Race singleRace = await _raceRepo.GetSingleByAsync(predicate: x => x.Id == id);
            if (singleRace == null)
                throw new InvalidOperationException("club with the Id does not exit");
            var newRace = _mapper.Map(race, singleRace);
            await _raceRepo.UpdateAsync(newRace);
        }
        public bool DeleteRace(int id)
        {
            throw new NotImplementedException();
        }
    }
}
