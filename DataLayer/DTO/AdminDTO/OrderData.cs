using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.AdminDTO
{
    public class OrderData
    {
        public int VendorId { get; set; } 
        public string BusinessName {  get; set; }

        public string Prescription {  get; set; }
        public string NoOfRefill { get; set; }
        public string Email {  get; set; }
        public string Contact { get; set; }
        public string Fax { get; set; }
        public int RequestId { get; set; }
        public DateTime CreatedDate {  get; set; }
    }
}
