using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.AdminDTO
{
    public class ProviderDetails
    {
        public int Phyid {get; set;}
        public string ProviderName { get; set; }
        public string Role { get; set; }
        public string OnCallStatus { get; set; }
        public string Status { get; set; }

        public bool IsNotificationStopped { get; set; }

    }
}
