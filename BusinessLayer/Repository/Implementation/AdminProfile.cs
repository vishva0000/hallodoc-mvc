using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;
using static DataLayer.DTO.AdminDTO.ProfileData;

namespace BusinessLayer.Repository.Implementation
{
    public class AdminProfile :IAdminProfile
    {
        public HallodocContext db;
        public AdminProfile(HallodocContext context)
        {
            this.db = context;

        }

        public ProfileData AdminProfileDetails(string email)
        {
            ProfileData data = new ProfileData();
            var admin = db.Admins.Where(a=>a.Email == email).FirstOrDefault();
            var allregion = db.Regions.ToList();
            var servicereg = db.AdminRegions.Where(a => a.AdminId == admin.AdminId).ToList();
            data.Username = db.AspNetUsers.Where(a=>a.Id== admin.AspNetUserId).FirstOrDefault().UserName;
            data.id = admin.AdminId;
            data.Firstname = admin.FirstName;
            data.Lastname = admin.LastName;
            data.Email = admin.Email;
            data.ConfirmEmail = admin.Email;
            data.Phone = admin.Mobile;
            data.MailNo = admin.AltPhone;
            data.Address1 = admin.Address1;
            data.Address2 = admin.Address2;
            data.City = admin.City;
            data.Zipcode = admin.Zip;

            List<ViewServiceRegion> availableregion = new();

            foreach (var region in allregion)
            {
                ViewServiceRegion reg = new ViewServiceRegion();

                reg.RegionId = region.RegionId;
                reg.Name = region.Name;
                reg.Ischecked = servicereg.Any(a=>a.RegionId==region.RegionId);
                availableregion.Add(reg);
            }
            data.serviceregions = availableregion;
            return data;
        }

        public void editMail(ProfileData model)
        {
            var admin = db.Admins.Where(a => a.AdminId == model.id).FirstOrDefault();

            admin.Address1 = model.Address1;
            admin.Address2 = model.Address2;
            admin.City = model.City;
            admin.Zip = model.Zipcode;
            admin.AltPhone = model.MailNo;
            db.Admins.Update(admin);
            db.SaveChanges();
        }

        public void adminDetailsEdit(ProfileData model)
        {
            var admin = db.Admins.Where(a => a.AdminId == model.id).FirstOrDefault();
            List<AdminRegion> adregion = db.AdminRegions.Where(a => a.AdminId == model.id).ToList();
            admin.FirstName = model.Firstname;
            admin.LastName = model.Lastname;
            admin.Email = model.ConfirmEmail;
            admin.Mobile = model.Phone;

            foreach (var item in adregion)
            {
                if (!model.regionmodified.Contains(item.RegionId))
                {
                    db.AdminRegions.Remove(item);

                }
                model.regionmodified.Remove(item.RegionId);
            }

            foreach (var item in model.regionmodified)
            {
                AdminRegion addnew = new()
                {
                    AdminId = model.id,
                    RegionId = item
                };
                db.AdminRegions.Add(addnew);

            }
            db.Admins.Update(admin);
            db.SaveChanges();

        }

        public void resetAdminPass(string Username, string Password)
        {
            var asp = db.AspNetUsers.Where(a => a.UserName == Username).FirstOrDefault();
            asp.PasswordHash = Password;
            db.AspNetUsers.Update(asp);
            db.SaveChanges();
        }
    }
}
