using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;
using Microsoft.Extensions.Hosting;
using static DataLayer.DTO.AdminDTO.ProviderProfileData;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BusinessLayer.Repository.Implementation
{
    public class CreateProviderAC : ICreateProviderAC
    {
        public HallodocContext db;
        public readonly IHostingEnvironment _environment;
        public CreateProviderAC(HallodocContext context, IHostingEnvironment environment) 
        {
            this.db = context;
            _environment = environment;

        }

        public ProviderProfileData getallList()
        {
            ProviderProfileData data = new ProviderProfileData();

            var allregion = db.Regions.ToList();
            List<state> states = new List<state>();


            foreach (var item in allregion)
            {
                state d = new state()
                {
                    stateName = item.Name,
                    regid = item.RegionId
                };
                states.Add(d);
            }
            data.stateList = states;

            List<AllRoles> allroles = new List<AllRoles>();

            var roles = db.Roles.Where(a=>a.AccountType==2).ToList();
            foreach (var item in roles)
            {
                AllRoles d = new AllRoles()
                {
                    Roleid = item.RoleId,
                    Rolename = item.Name
                };
                allroles.Add(d);
            }
            data.RolesList = allroles;
            List<ProviderRegion> regions = new List<ProviderRegion>();
            foreach (var region in allregion)
            {
                ProviderRegion reg = new ProviderRegion();

                reg.RegionId = region.RegionId;
                reg.Name = region.Name;
                reg.Ischecked = null;
                regions.Add(reg);
            }
            data.ProviderServiceRegion = regions;

            return data;

        }

        public void CreateAccount(ProviderProfileData model, string email)
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


            var role = db.AspNetRoles.FirstOrDefault(a => a.Name == "physician");
            if (role != null)
            {
                aspuser.Roles.Add(role);

            }

            Physician addphy = new Physician()
            {
                AspNetUser = aspuser,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                Email = model.Email,
                Mobile =  model.Phone,
                MedicalLicense = model.MedicalLicense,
                //Photo = model.Photo,
                AdminNotes = model.AdminNote,
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                RegionId = model.StateId,
                Zip = model.Zip,
                AltPhone = model.MailPhone,
                CreatedBy = db.AspNetUsers.Where(a=>a.Email== email).FirstOrDefault().Id,
                CreatedDate = DateTime.Now,
                BusinessName =  model.BusinessName,
                BusinessWebsite = model.BusinessWebsite,
                RoleId = model.Roleid,
                Npinumber = model.NPINumber,

            };


            foreach (var item in model.Selectedregions)
            {
                PhysicianRegion d = new PhysicianRegion()
                {
                    Physician = addphy,
                    RegionId = item
                };
                db.PhysicianRegions.Add(d);
            }

            var phyid = db.Physicians.OrderByDescending(m => m.PhysicianId).FirstOrDefault().PhysicianId+1;
            if (model.photo != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads/Physician");
                var filename = model.photo.Name+"_"+ phyid.ToString() + Path.GetExtension(model.photo.FileName);
                var filePath = Path.Combine(uploads,filename);
                var file = model.photo;
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                //addphy.Photo = filePath;
            }
            
            if (model.IndependentContract != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads/Physician");
                var filename = model.IndependentContract.Name+"_"+ phyid.ToString() +Path.GetExtension(model.IndependentContract.FileName);
                var filePath = Path.Combine(uploads,filename);
                var file = model.IndependentContract;
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                addphy.IsAgreementDoc = new BitArray(new bool[1] { true });
            }

            if (model.BackgroundCheck != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads/Physician");
                var filename = model.BackgroundCheck.Name+"_"+ phyid.ToString() + Path.GetExtension(model.BackgroundCheck.FileName);
                var filePath = Path.Combine(uploads,filename);
                var file = model.BackgroundCheck;
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                addphy.IsBackgroundDoc = new BitArray(new bool[1] { true });

            }

            if (model.HIPAAC != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads/Physician");
                var filename = model.HIPAAC.Name+"_"+ phyid.ToString()  + Path.GetExtension(model.HIPAAC.FileName);
                var filePath = Path.Combine(uploads,filename);
                var file = model.HIPAAC;
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                //addphy.IsLicenseDoc = new BitArray(new bool[1] { true });

            }
            if (model.NonDisclouser != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads/Physician");
                var filename = model.NonDisclouser.Name+"_"+ phyid.ToString().ToString()+Path.GetExtension(model.NonDisclouser.FileName);
                var filePath = Path.Combine(uploads,filename);
                var file = model.NonDisclouser;
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                addphy.IsNonDisclosureDoc = new BitArray(new bool[1] { true });

            }

            db.Physicians.Add(addphy);

            db.SaveChanges();

        }
    }
}
