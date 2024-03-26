using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
                r.RoleId = item.RoleId;
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


        public RoleData GetOneRole( int editid)
        {
            var role = db.Roles.Where(a => a.RoleId == editid).FirstOrDefault();
            RoleData data = new RoleData();

            data.RoleId = editid;
            data.RoleName = role.Name;
            data.AccType = role.AccountType;
            return data;
        }

        public void SetOneRole(RoleData model)
        {
            var role = db.Roles.Where(a => a.RoleId == model.RoleId).FirstOrDefault();
            role.Name = model.RoleName;
            role.AccountType = (short)model.AccType;

            db.Roles.Update(role);

            var menu = db.RoleMenus.Where(a => a.RoleId == model.RoleId).ToList();
           
            foreach (var item in menu)
            {
                if (!model.selectedMenu.Contains(item.MenuId))
                {
                    db.RoleMenus.Remove(item);

                }
                model.selectedMenu.Remove(item.MenuId);
            }
            foreach (var item in model.selectedMenu)
            {
                RoleMenu i = new RoleMenu()
                {
                    MenuId = item,
                    RoleId = model.RoleId
                };
                db.RoleMenus.Add(i);

            }

            db.SaveChanges();

        }

        public void deleteRole(int roleid)
        {
            var role = db.Roles.Where(a => a.RoleId == roleid).FirstOrDefault();         

            var menu = db.RoleMenus.Where(a => a.RoleId == roleid).ToList();

            foreach (var item in menu)
            {
                db.RoleMenus.Remove(item);
            }

            db.Roles.Remove(role);

            db.SaveChanges();
        }
    }
}
