namespace HalloDoc.DTO.AdminDTO
{
    public class RequestTableData
    {
        public int? RequestId { get; set; }
        public int RequestTypeId { get; set; }
        public string? Name { get; set; }   
        public DateTime Dob { get; set; }
        public string? Requestor { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? DateOfService { get; set; }
        public string? Phone { get; set;}
        public string? Email { get; set;}
        public string? Physician { get; set;}
        public string? Address { get; set;}
        public string? Notes { get; set;}
        public string? ChatWith { get; set;}

        public int? statusdb; //  field to store the actual db status value
        public int? RegionId { get; set; }

        public int Status
        {
            get
            {
                switch (statusdb) // Use the db status here
                {
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 5;
                    case 7:
                        return 5;
                    case 8:
                        return 5;
                    case 4:
                        return 3;
                    case 5:
                        return 3;
                    case 6:
                        return 4;
                    case 9:
                        return 6;
                    default:
                        return 0;
                }
            }
            set { statusdb = value; } // Set the db status here
        }
    }
}
