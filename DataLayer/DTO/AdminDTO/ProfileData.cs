using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.AdminDTO
{
    public class ProfileData
    {
        public int id {  get; set; }
        public string aspid { get; set; }
        public string Username { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string MailNo { get; set; }
        public List<int>? regionmodified { get; set; }
        public List<ViewServiceRegion>? serviceregions { get; set; }
        public ViewAdminProfileList? viewadminlist { get; set; }

        public class ViewServiceRegion
        {
            public string? Name { get; set; }
            public bool? Ischecked { get; set; }
            public int? RegionId { get; set; }
        }
        public class ViewAdminProfileList
        {
            public List<Role>? roles { get; set; }
        }
    }


}
