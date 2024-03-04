using System.ComponentModel.DataAnnotations;

namespace DataLayer.DTO
{
    public class BusinessRequestModel
    {
        [Required(ErrorMessage ="This field is required")]
        public string B_Firstname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string B_Lastname { get;  set; }
        [Required(ErrorMessage = "This field is required")]
        public string B_Email { get; set;}
        [Required(ErrorMessage = "This field is required")]
        public string B_Phone { get; set;}
        public string? B_Hotel { get; set;}
        public string? B_caseNo { get; set;}

        [Required(ErrorMessage = "This field is required")]
        public string P_symp {  get; set;}
        [Required(ErrorMessage = "This field is required")]
        public string   P_Firstname { get; set;}
        [Required(ErrorMessage = "This field is required")]
        public string P_Lastname { get; set;}
        [Required(ErrorMessage = "This field is required")]
        public string P_Email { get; set;}
        [Required(ErrorMessage = "This field is required")]
        public string P_Phone { get; set;}
        [Required(ErrorMessage = "This field is required")]
        public string P_Street { get; set;}
        [Required(ErrorMessage = "This field is required")]
        public string P_City { get; set;}
        [Required(ErrorMessage = "This field is required")]
        public string? P_State { get; set;}

        public string? P_ZipCode { get; set;}

        public string?  P_Room { get; set;}
        [Required(ErrorMessage = "This field is required")]
        public DateTime P_dob {  get; set;}

    }
}
