using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{
    public interface IProviderProfileEditByAdmin
    {
        void EditAccountInfo(ProviderProfileData model);
        void EditPhyMailInfo(ProviderProfileData model);
        void EditPhyProfileInfo(ProviderProfileData model);
        void EditPhysicianInfo(ProviderProfileData model);
        ProviderProfileData getProviderProfileData(int Providerid);
    }
}