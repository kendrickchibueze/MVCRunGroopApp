using RunGroopApp.Data.Enum;
using RunGroopApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RunGroopApp.ViewModels
{
    public class CreateRaceViewModel
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }  
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}
