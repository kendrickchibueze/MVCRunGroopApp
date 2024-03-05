using RunGroopApp.Models;

namespace RunGroopApp.Interfaces
{
    public interface IClubService
    {
       
        Task<IReadOnlyList<Club>> GetAllClubsAsync();
        Task<Club> GetClubByIdAsync(int id);
      
    }
}
