using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{
    public interface ICreateProviderAC
    {
        ProviderProfileData getallList();
        public void CreateAccount(ProviderProfileData model, string email);
    }
}