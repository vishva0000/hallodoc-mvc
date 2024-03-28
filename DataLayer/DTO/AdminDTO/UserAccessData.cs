using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.AdminDTO
{
    public class UserAccessData
    {
        public string Accounttype { get; set; }
        public string AccountPOC { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public string OpenRequests { get; set; }
    }
}
