using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
    public interface IRaceService
    {
        Task<IReadOnlyList<Race>> GetAllRacesAsync();

    }
}
