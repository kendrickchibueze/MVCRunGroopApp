using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
    public interface IClubService
    {
       
        Task<IReadOnlyList<Club>> GetAllClubsAsync();
        Task<Club> GetClubByIdAsync(int id);
        Task<IEnumerable<Club>> GetClubByCity(string city);

        Task<bool> AddClub(Club club);
        bool UpdateClub(Club club);
        bool DeleteClub(int id);


    }
}
