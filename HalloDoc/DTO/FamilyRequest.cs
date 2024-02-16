namespace HalloDoc.DTO
{
    public class FamilyRequest
    {
        //Familyrequestdto Friend 

        public string F_Firstname { get; set; }
        public string F_Lastname { get; set; }
        public string F_Phone { get; set; }
        public string F_Email { get; set; }
        public string F_relation { get; set; }

        //Patient Info

        public string P_symp { get; set; }
        public DateTime dob { get; set; }
        public string P_Firstname { get; set; }
        public string P_Lastname { get; set; }
        public string P_Phone { get; set; }
        public string P_Email { get; set; }
        public string P_Street { get; set; }
        public string P_State { get; set; }
        public string P_City { get; set; }
        public string P_Zipcode { get; set; }
        public string P_Location { get; set; }
        public IFormFile P_File { get; set; }
    }
}
