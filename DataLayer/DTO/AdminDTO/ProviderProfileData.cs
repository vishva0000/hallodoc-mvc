using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.AdminDTO
{
    public class ProviderProfileData
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public int ProviderId { get; set; }
        public int Roleid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MedicalLicense { get; set; }
        public string NPINumber { get; set; }
        public string syncEmail { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string MailPhone { get; set; }
        public string BusinessName { get; set; }
        public string BusinessWebsite { get; set; }
        public string Photo { get; set; }
        public string Sign { get; set; }
        public string AdminNote { get; set; }
        public List<int> Selectedregions { get; set; }
        public List<ProviderRegion> ProviderServiceRegion { get; set; }
        public List<AllRoles> RolesList { get; set; }

        public class ProviderRegion
        {
            public string? Name { get; set; }
            public bool? Ischecked { get; set; }
            public int? RegionId { get; set; }
        }

        public class AllRoles
        {
            public string Rolename { get; set; }
            public int Roleid { get; set; }
        }
    }
}
