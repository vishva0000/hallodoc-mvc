namespace DataLayer.DTO.AdminDTO
{
    public class RequestTableData
    {
        
        public int? RequestId { get; set; }
        public int RequestTypeId { get; set; }
        public string? Name { get; set; }   
        public DateTime Dob { get; set; }
      
        public string PhysicianName { get; set; }
        public string? Requestor { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime DateOfService { get; set; }
        public List<string> Region {  get; set; }
        public string? PhoneP { get; set;}
        public string? PhoneO { get; set;}
        public string? Email { get; set;}
        public string? Physician { get; set;}
        public string? Address { get; set;}
        public string? Notes { get; set;}
        public string? ChatWith { get; set;}

        public int? statusdb; //  field to store the actual db status value
        public int? RegionId { get; set; }

       public int status { get; set; }
    }
    
    public class cancelcase
    {
        public int req_id { get; set; }
        public string cancelNote  { get; set;}
    }

    
}
