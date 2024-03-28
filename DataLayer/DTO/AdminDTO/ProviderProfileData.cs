using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.AdminDTO
{
    public class ProviderProfileData
    {
        [Required(ErrorMessage ="The user name is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
        public int? ProviderId { get; set; }
        public int Roleid { get; set; }
        [Required(ErrorMessage = "The first name is required")]
        public string Firstname { get; set; }
        [Required(ErrorMessage ="The Last name is required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage ="The Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="The Phone n number is required")]
        public string Phone { get; set; }
        public string? MedicalLicense { get; set; }
        public string? NPINumber { get; set; }
        public string? syncEmail { get; set; }
        [Required(ErrorMessage ="The Address1 is required")]
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? MailPhone { get; set; }
        [Required(ErrorMessage ="The Business name is required")]
        public string BusinessName { get; set; }
        [Required(ErrorMessage ="The Business website is required")]
        public string BusinessWebsite { get; set; }
        public string? AdminNote { get; set; }

        public IFormFile? photo { get; set; }
        public IFormFile? signature { get; set; }
        public IFormFile? IndependentContract { get; set; }
        public IFormFile? BackgroundCheck { get; set; }
        public IFormFile? HIPAAC { get; set; }
        public IFormFile? NonDisclouser { get; set; }

        public int? StateId { get; set; }
        [Required(ErrorMessage ="Select the region")]
        public List<int> Selectedregions { get; set; }
        public List<ProviderRegion>? ProviderServiceRegion { get; set; }
        public List<AllRoles>? RolesList { get; set; }
        public List<Accounttype>? AccTypeList { get; set; }

        public List<state>? stateList { get; set; }
        public class state
        {
            public string stateName { get; set; }
            public int regid {get; set;}
        }
        public class Accounttype
        {
            public string AccName { get; set; }
            public string AccTypId { get; set; }
        }

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
