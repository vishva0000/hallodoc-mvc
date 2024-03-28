using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;

namespace BusinessLayer.Repository.Implementation
{
    public class UserAccess:IUserAccess
    {
        public HallodocContext db;
        public UserAccess(HallodocContext context)
        {
            this.db = context;

        }

        public List<UserAccessData> getData(string search)
        {
            int seid = int.Parse(search);
            var admin = db.Admins.ToList();
            var physician = db.Physicians.ToList();

            List<UserAccessData> data = new();
            if (seid == 0)
            {
                foreach (var item in admin)
                {
                    UserAccessData dataItem = new UserAccessData();
                    dataItem.Accounttype = "admin";
                    dataItem.AccountPOC = item.FirstName + ", " + item.LastName;
                    dataItem.Phone = item.Mobile;
                    data.Add(dataItem);
                }

                foreach (var item in physician)
                {
                    UserAccessData dataItem = new UserAccessData();
                    dataItem.Accounttype = "physician";
                    dataItem.AccountPOC = item.FirstName + ", " + item.LastName;
                    dataItem.Phone = item.Mobile;

                    data.Add(dataItem);
                }
            }
            else if (seid == 1)
            {
                foreach (var item in admin)
                {
                    UserAccessData dataItem = new UserAccessData();
                    dataItem.Accounttype = "admin";
                    dataItem.AccountPOC = item.FirstName + ", " + item.LastName;
                    dataItem.Phone = item.Mobile;

                    data.Add(dataItem);
                }

            }
            else if (seid == 2)
            {
                foreach (var item in physician)
                {
                    UserAccessData dataItem = new UserAccessData();
                    dataItem.Accounttype = "physician";
                    dataItem.AccountPOC = item.FirstName + ", " + item.LastName;
                    dataItem.Phone = item.Mobile;

                    data.Add(dataItem);
                }
            }
            
            return data;
        }
    }
}
