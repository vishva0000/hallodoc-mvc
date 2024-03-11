using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.AdminDTO
{
    public class CreateRequestData
    {
        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string State {  get; set; }
        public string? Zipcode { get; set; }

        public string? Room { get; set; }
        public string? Adminnotes {  get; set; }

    }
}
