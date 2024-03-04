using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.Models;
using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Implementation
{
    public class AdminDashboard : IAdminDashboard
    {
        public HallodocContext db;
        public AdminDashboard(HallodocContext context)
        {
            this.db = context;
        }
        public AdminDashboarddata countrequest()
        {
            AdminDashboarddata data = new AdminDashboarddata();
            data.New = db.Requests.Where(a => a.Status == 1).Count();
            data.Pending = db.Requests.Where(a => a.Status == 2).Count();
            data.Active = db.Requests.Where(a => a.Status == 5 || a.Status == 4).Count();
            data.Conclude = db.Requests.Where(a => a.Status == 6).Count();
            data.ToClose = db.Requests.Where(a => a.Status == 7 || a.Status == 3 || a.Status == 8).Count();
            data.UnPaid = db.Requests.Where(a => a.Status == 9).Count();
            return data;

        }

    }
}
