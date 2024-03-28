using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static DataLayer.DTO.AdminDTO.ProfileData;
using static DataLayer.DTO.AdminDTO.ProviderProfileData;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace BusinessLayer.Repository.Implementation
{
    public class ProviderProfileEditByAdmin : IProviderProfileEditByAdmin
    {
        public HallodocContext db;
        public readonly IHostingEnvironment _environment;


        public ProviderProfileEditByAdmin(HallodocContext context, IHostingEnvironment environment)
        {
            this.db = context;
            _environment = environment; 
        }

        public ProviderProfileData getProviderProfileData(int Providerid)
        {
            ProviderProfileData data = new ProviderProfileData();

            var phy = db.Physicians.Where(a => a.PhysicianId == Providerid).Include(a=>a.AspNetUser).FirstOrDefault();
           
            if (phy != null)
            {
                data.Username = phy.AspNetUser.UserName;
                data.ProviderId = Providerid;
                data.Firstname = phy.FirstName;
                data.Lastname = phy.LastName;
                data.Email = phy.Email;
                data.Phone = phy.Mobile;
                data.MedicalLicense = phy.MedicalLicense;
                data.NPINumber = phy.Npinumber;
                data.syncEmail = phy.SyncEmailAddress;
                data.Address1 = phy.Address1;
                data.Address2 = phy.Address2;
                data.City = phy.City;
                data.Zip = phy.Zip;
                data.MailPhone = phy.AltPhone;
                data.BusinessName = phy.BusinessName;
                data.BusinessWebsite = phy.BusinessWebsite;
                if (phy.RoleId != null)
                {
                    data.Roleid = (int)phy.RoleId;

                }
                data.StateId = (int)phy.RegionId;

            }
            var allregion = db.Regions.ToList();
            var servicereg = db.PhysicianRegions.Where(a => a.PhysicianId == Providerid).ToList();
            List<ProviderRegion> regions = new List<ProviderRegion>();
            foreach (var region in allregion)
            {
                ProviderRegion reg = new ProviderRegion();

                reg.RegionId = region.RegionId;
                reg.Name = region.Name;
                reg.Ischecked = servicereg.Any(a => a.RegionId == region.RegionId);
                regions.Add(reg);
            }
            data.ProviderServiceRegion = regions;

            //List<Accounttype> acctype = new List<Accounttype>();

            //var acc = db.AspNetRoles.ToList();
            //foreach(var item in acc)
            //{
            //    Accounttype d = new Accounttype()
            //    {
            //        AccName = item.Name,
            //        AccTypId = item.Id
            //    };
            //    acctype.Add(d);
            //}
            //data.AccTypeList = acctype;  
            
            List<AllRoles> allroles = new List<AllRoles>();

            var roles = db.Roles.Where(a=>a.AccountType==2).ToList();
            foreach(var item in roles)
            {
                AllRoles d = new AllRoles()
                {
                    Roleid = item.RoleId,
                    Rolename = item.Name
                };
                allroles.Add(d);
            }
            data.RolesList = allroles;
            
            List<state> states = new List<state>();

          
            foreach(var item in allregion)
            {
                state d = new state()
                {
                    stateName = item.Name,
                    regid = item.RegionId
                };
                states.Add(d);
            }
            data.stateList = states;
           
            return data;

        }

        public void EditAccountInfo(ProviderProfileData model)
        {
            var phy = db.Physicians.Where(a => a.PhysicianId == model.ProviderId).FirstOrDefault();
            var asp =db.AspNetUsers.Where(a => a.Id == phy.AspNetUserId).FirstOrDefault();
            asp.UserName = model.Username;
            phy.RoleId = model.Roleid;
            //db.AspNetUsers.Update(asp);
            //db.Physicians.Update(phy);
            //db.SaveChanges();
        } 
        
        public void EditPhysicianInfo(ProviderProfileData model)
        {
            var phy = db.Physicians.Where(a => a.PhysicianId == model.ProviderId).FirstOrDefault();
            phy.FirstName = model.Firstname;
            phy.LastName = model.Lastname;
            phy.Email = model.Email;
            phy.MedicalLicense = model.MedicalLicense;
            phy.Npinumber = model.NPINumber;
            phy.SyncEmailAddress = model.syncEmail;
            var phyreg = db.PhysicianRegions.Where(a => a.PhysicianId == model.ProviderId).ToList();
            foreach (var item in phyreg)
            {
                if (!model.Selectedregions.Contains(item.RegionId))
                {
                    //db.PhysicianRegions.Remove(item);

                }
                model.Selectedregions.Remove(item.RegionId);
            }

            foreach (var item in model.Selectedregions)
            {
                PhysicianRegion addnew = new();
                addnew.PhysicianId = (int)model.ProviderId;
                addnew.RegionId = item;
                //db.PhysicianRegions.Add(addnew);

            }
            //db.Physicians.Update(phy);
            //db.SaveChanges();

        }
        public void EditPhyMailInfo(ProviderProfileData model)
        {
            var phy = db.Physicians.Where(a => a.PhysicianId == model.ProviderId).FirstOrDefault();
            phy.Address1 = model.Address1;
            phy.Address2 = model.Address2;
            phy.City = model.City;
            phy.RegionId = model.StateId;
            phy.Zip = model.Zip;
            phy.AltPhone = model.MailPhone;
            //db.Physicians.Update(phy);
            //db.SaveChanges();


        }
        public void EditPhyProfileInfo(ProviderProfileData model)
        {
            var phy = db.Physicians.Where(a => a.PhysicianId == model.ProviderId).FirstOrDefault();
            phy.BusinessName = model.BusinessName;
            phy.BusinessWebsite = model.BusinessWebsite;
            if (model.photo != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads/Physician");
                var filename = model.photo.Name + "_" + model.ProviderId.ToString() + Path.GetExtension(model.photo.FileName);
                var filePath = Path.Combine(uploads, filename);
                var file = model.photo;
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                phy.Photo = filePath;
            } 
            
            if (model.signature != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads/Physician");
                var filename = model.signature.Name + "_" + model.ProviderId.ToString() + Path.GetExtension(model.signature.FileName);
                var filePath = Path.Combine(uploads, filename);
                var file = model.signature;
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                phy.Signature = filePath;

            }
            //db.Physicians.Update(phy);
            //db.SaveChanges();

        }
    }
}
