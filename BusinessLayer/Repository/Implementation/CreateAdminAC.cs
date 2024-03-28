using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;
using NPOI.SS.Formula.PTG;
using static DataLayer.DTO.AdminDTO.ProviderProfileData;

namespace BusinessLayer.Repository.Implementation
{
    public class CreateAdminAC : ICreateAdminAC
    {
        public HallodocContext db;

        public CreateAdminAC(HallodocContext context) 
        {
            this.db = context;

        }

        public AdminAccountData getAllList()
        {
            AdminAccountData data = new AdminAccountData();

            List<States> states = new();
            var allregion = db.Regions.ToList();

            foreach (var item in allregion)
            {
                States d = new States()
                {
                    stateName = item.Name,
                    regid = item.RegionId
                };
                states.Add(d);
            }
            data.stateList = states;

            List<AllRole> allroles = new List<AllRole>();

            var roles = db.Roles.Where(a=>a.AccountType==1).ToList();
            foreach (var item in roles) 
            {
                AllRole d = new AllRole()
                {
                    Roleid = item.RoleId,
                    Rolename = item.Name
                };
                allroles.Add(d);
            }
            data.RolesList = allroles;
            List<ProviderRegions> regions = new List<ProviderRegions>();
            foreach (var region in allregion)
            {
                ProviderRegions reg = new ProviderRegions();

                reg.RegionId = region.RegionId;
                reg.Name = region.Name;
                reg.Ischecked = null;
                regions.Add(reg);
            }
            data.ProviderServiceRegion = regions;

            return data;
        }

        public void CreateAccount(AdminAccountData model, string email)
        {
            AspNetUser aspuser = new AspNetUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PasswordHash = model.Password,
                Email = model.Email,
                PhoneNumber = model.Phone,
                CreatedDate = DateTime.Now,

            };
            db.AspNetUsers.Add(aspuser);

            var role = db.AspNetRoles.FirstOrDefault(a => a.Name == "admin");
            if (role != null)
            {
                aspuser.Roles.Add(role);

            }

            Admin addadmin = new Admin()
            {
                AspNetUser = aspuser,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                Email = model.Email,
                Mobile = model.Phone,
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                RegionId = model.StateId,
                Zip = model.Zip,
                AltPhone = model.AltPhone,
                CreatedBy = db.AspNetUsers.Where(a => a.Email == email).FirstOrDefault().Id,
                CreatedDate = DateTime.Now,
                RoleId = model.Roleid,

            };

            db.Admins.Add(addadmin);

            foreach (var item in model.Selectedregions)
            {
                AdminRegion d = new AdminRegion()
                {
                    Admin = addadmin,
                    RegionId = item
                };
                db.AdminRegions.Add(d);
            }

            db.SaveChanges();

        }
    }
}
