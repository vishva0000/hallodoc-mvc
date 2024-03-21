using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Repository.Implementation
{
    public class ProviderData: IProviderData
    {
        public HallodocContext db;

        public ProviderData(HallodocContext context) 
        {
            this.db = context;

        }

        public List<ProviderDetails> getProviderData(string search)
        {
            
            var providers = db.Physicians.ToList();
            var notification = db.PhysicianNotifications.ToList();
            List<ProviderDetails> data = new();

            if (search != null)
            {
                int seid = int.Parse(search);
                var providerselect = providers.Where(a =>
                      search.IsNullOrEmpty() ||
                      a.RegionId == seid);
                foreach (var item in providerselect)
                {
                    ProviderDetails dto = new ProviderDetails();
                    dto.Phyid = item.PhysicianId;
                    dto.ProviderName = item.FirstName + " " + item.LastName;
                    dto.Role = "Provider";
                    dto.OnCallStatus = "Not Available";
                    dto.Status = "Pending";
                    if (notification.Where(a => a.PhysicianId == item.PhysicianId).FirstOrDefault().IsNotificationStopped != null)
                    {
                        dto.IsNotificationStopped = true;
                    }
                    else
                    {
                        dto.IsNotificationStopped = false;
                    }
                    data.Add(dto);
                }
            }
            else
            {
                foreach (var item in providers)
                {
                    ProviderDetails dto = new ProviderDetails();
                    dto.Phyid = item.PhysicianId;
                    dto.ProviderName = item.FirstName + " " + item.LastName;
                    dto.Role = "Provider";
                    dto.OnCallStatus = "Not Available";
                    dto.Status = "Pending";
                    if (notification.Where(a => a.PhysicianId == item.PhysicianId).FirstOrDefault().IsNotificationStopped != null)
                    {
                        dto.IsNotificationStopped = true;
                    }
                    else
                    {
                        dto.IsNotificationStopped = false;
                    }
                    data.Add(dto);
                }
            }       
                                 
            return data;
        }
    }
}
