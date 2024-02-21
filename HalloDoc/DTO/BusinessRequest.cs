using System.ComponentModel.DataAnnotations;

namespace HalloDoc.DTO
{
    public class BusinessRequest
    {
        //[Required(ErrorMessage = "First name is required")]
        public string B_Firstname { get; set; }
        //[Required(ErrorMessage = "Last name is required")]
        public string B_Lastname { get;  set; }
        //[Required(ErrorMessage = "Email is required")]
        public string B_Email { get; set;}
        //[Required(ErrorMessage = "Phone number is required")]
        public string B_Phone { get; set;}
        public string B_Hotel { get; set;}
        public string B_caseNo { get; set;}

        //[Required(ErrorMessage = "Please enter symptoms")]
        public string P_symp {  get; set;}
        //[Required(ErrorMessage = "First name is required")]
        public string P_Firstname { get; set;}
        //[Required(ErrorMessage = "Last name is required")]
        public string P_Lastname { get; set;}
        //[Required(ErrorMessage = "Email name is required")]
        public string P_Email { get; set;}
        //[Required(ErrorMessage = "Phone number is required")]
        public string P_Phone { get; set;}
        public string P_Street { get; set;}
        public string P_City { get; set;}
        public string P_State { get; set;}
        public string P_ZipCode { get; set;}

        public string P_Room { get; set;}
        //[Required(ErrorMessage = "Date of birth is required")]
        public DateTime P_dob {  get; set;}

    }
}
