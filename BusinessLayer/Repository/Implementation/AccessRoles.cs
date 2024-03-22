using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;

namespace BusinessLayer.Repository.Implementation
{
    public class AccessRoles : IAccessRoles
    {
        public HallodocContext db;
        public AccessRoles(HallodocContext context)
        {
            this.db = context;

        }

        public List<RoleData> getAllRoles()
        {
            List<RoleData> allroles = new List<RoleData>();

            var roles = db.Roles.ToList();

            foreach(var item in roles)
            {
                RoleData r = new RoleData();
                r.RoleName = item.Name;
                r.AccType = item.AccountType;
                allroles.Add(r);
            }
            return allroles;
        }

        public void CreateRole(string RoleName, string AccountType, List<int> selectedMenu)
        {
            int type = int.Parse(AccountType);
            Role role = new Role();
            role.Name = RoleName;
            role.AccountType = (short)type;
            role.CreatedBy = "admin";
            role.CreatedDate = DateTime.Now;
            role.IsDeleted = new BitArray(new bool[1] { false });
            db.Roles.Add(role);

            foreach (var item in selectedMenu)
            {
                RoleMenu role1 = new RoleMenu()
                {
                    Role = role,
                    MenuId = item
                };
                db.RoleMenus.Add(role1);
            }

            db.SaveChanges();
        }

    }
}
