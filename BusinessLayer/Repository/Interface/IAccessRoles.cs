using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{
    public interface IAccessRoles
    {
        public List<RoleData> getAllRoles();
        public void CreateRole(string RoleName, string AccountType, List<int> selectedMenu);
    }
}