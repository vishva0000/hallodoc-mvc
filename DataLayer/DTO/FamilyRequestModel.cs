using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.DTO
{
    public class FamilyRequestModel
    {
        //Familyrequestdto Friend 
        [Required(ErrorMessage = "This field is required")]
        public string F_Firstname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string F_Lastname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string F_Phone { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string F_Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string F_relation { get; set; }

        //Patient Info

        [Required(ErrorMessage = "This field is required")]
        public string P_symp { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime dob { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string P_Firstname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string P_Lastname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string P_Phone { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string P_Email { get; set; }


        public string? P_Street { get; set; }
        public string? P_State { get; set; }
        public string? P_City { get; set; }
        public string? P_Zipcode { get; set; }
        public string? P_Location { get; set; }
        public List<IFormFile>? P_File { get; set; }
    }
}
