using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
    public interface IRaceService
    {
        Task<IReadOnlyList<Race>> GetAllRacesAsync();

        Task<Race> GetRaceByIdAsync(int id);

        Task<IEnumerable<Race>> GetAllRacesByCity(string city);
        Task<bool> AddRace(Race race);
        Task UpdateRace(int id, Race race);
        Task<Race> DeleteRace(int id);

    }
}
