using System.ComponentModel.DataAnnotations;

namespace RunGroopApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email Address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }
}
