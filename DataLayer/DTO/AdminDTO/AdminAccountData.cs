using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.AdminDTO
{
    public class AdminAccountData
    {
        [Required(ErrorMessage ="The username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage ="The password is required")]
        public string Password { get; set; } 
        
        [Required(ErrorMessage ="The First name is required")]
        public string Firstname { get; set; }
         
        [Required(ErrorMessage ="The Last name is required")]
        public string Lastname { get; set; }

        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string AltPhone { get; set; }
        public int Roleid { get; set; }
        public int StateId { get; set; }

        public List<int> Selectedregions { get; set; }
        public List<ProviderRegions>? ProviderServiceRegion { get; set; }

        public List<AllRole>? RolesList { get; set; }
       

        public List<States>? stateList { get; set; }
       
    }
    public class States
    {
        public string stateName { get; set; }
        public int regid { get; set; }
    }

    public class ProviderRegions
    {
        public string? Name { get; set; }
        public bool? Ischecked { get; set; }
        public int? RegionId { get; set; }
    }
    public class AllRole
    {
        public string Rolename { get; set; }
        public int Roleid { get; set; }
    }
}
