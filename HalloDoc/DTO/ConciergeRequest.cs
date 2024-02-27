using System.ComponentModel.DataAnnotations;

namespace HalloDoc.DTO
{
    public class ConciergeRequest
    {
        [Required(ErrorMessage = "This field is required")]
        public string C_Firstname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string C_Lastname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string C_Phone { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string C_Email { get; set; }

      
        public string? C_Street { get; set; }
        public string? C_State { get; set; }

        public string? C_Location { get; set; }
        public string? C_City { get; set; }
        public string? C_Zipcode { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string P_Firstname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string P_Lastname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime P_dob { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string P_symp { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string P_phone { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string P_email { get; set; }

        public string? P_Location { get; set; }
    }
}
