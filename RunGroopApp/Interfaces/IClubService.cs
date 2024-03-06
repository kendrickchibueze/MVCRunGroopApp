using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
    public interface IClubService
    {
       
        Task<IReadOnlyList<Club>> GetAllClubsAsync();
        Task<Club> GetClubByIdAsync(int id);
        Task<IEnumerable<Club>> GetClubByCity(string city);

        bool AddClub(Race race);
        bool UpdateClub(Race race);
        bool DeleteClub(int id);


    }
}
