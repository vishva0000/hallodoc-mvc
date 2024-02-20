using System.ComponentModel.DataAnnotations;

namespace HalloDoc.DTO
{
    public class CreatePatient
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password is required")]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }
    }
}
