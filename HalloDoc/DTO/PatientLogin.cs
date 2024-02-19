using System.ComponentModel.DataAnnotations;

namespace HalloDoc.DTO
{
    public class PatientLogin
    {
      
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}
