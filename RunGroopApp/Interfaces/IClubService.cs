using RunGroopApp.Models;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Interfaces
{
    public interface IClubService
    {
       
        Task<IReadOnlyList<Club>> GetAllClubsAsync();
        Task<Club> GetClubByIdAsync(int id);
        Task<IEnumerable<Club>> GetClubByCity(string city);

        Task<bool> AddClub(Club club);
        Task UpdateClub(int id, Club club);
        bool DeleteClub(int id);


    }
}
