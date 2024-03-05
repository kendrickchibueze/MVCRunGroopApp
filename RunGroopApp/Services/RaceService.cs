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
    }
}
