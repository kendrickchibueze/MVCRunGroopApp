using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
    public interface IRaceService
    {
        Task<IReadOnlyList<Race>> GetAllRacesAsync();

        Task<Race> GetRaceByIdAsync(int id);

        Task<IEnumerable<Race>> GetAllRacesByCity(string city); 

        bool AddRace(Race race);
        bool UpdateRace(Race race);
        bool DeleteRace(int id);

    }
}
