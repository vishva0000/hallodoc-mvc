using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{
    public interface ICreateAdminAC
    {
        public void CreateAccount(AdminAccountData model, string email);
        AdminAccountData getAllList();
    }
}