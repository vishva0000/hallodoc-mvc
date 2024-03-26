using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using static DataLayer.DTO.AdminDTO.ProfileData;
using static DataLayer.DTO.AdminDTO.ProviderProfileData;

namespace BusinessLayer.Repository.Implementation
{
    public class ProviderProfileEditByAdmin : IProviderProfileEditByAdmin
    {
        public HallodocContext db;

        public ProviderProfileEditByAdmin(HallodocContext context)
        {
            this.db = context;

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
            return data;

        }
    }
}
