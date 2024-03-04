namespace DataLayer.DTO.AdminDTO
{
    public class ViewCaseData
    {
        public int RequestId {  get; set; }
        public int RequestTypeId { get; set; }
        public string Symp {  get; set; }
        public string Firstname { get; set;}
        public string Lastname { get; set;}
        public string Email { get; set;}
        public string Phone { get; set;}
        public string Street { get; set;}
        public string City { get; set;}
        public string State { get; set;}
        public string ZipCode { get; set;}
        public string Room { get; set;}
        public DateTime Dob {  get; set;}

    }
}
