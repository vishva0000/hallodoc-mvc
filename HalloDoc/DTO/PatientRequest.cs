
namespace HalloDoc.DTO
{
    public class PatientRequest
    {

        public string Symptoms { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Room { get; set; }

        public DateTime dob { get; set; }

        public IFormFile File { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
