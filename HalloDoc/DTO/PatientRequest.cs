
using System.ComponentModel.DataAnnotations;

namespace HalloDoc.DTO
{
    public class PatientRequest
    {

        [Required(ErrorMessage = "This field is required")]
        public string Symptoms { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Zipcode { get; set; }
        public string? Room { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime dob { get; set; }

        public List<IFormFile>? File { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }

    }
}
