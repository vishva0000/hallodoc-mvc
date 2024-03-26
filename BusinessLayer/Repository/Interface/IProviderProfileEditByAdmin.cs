using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{
    public interface IProviderProfileEditByAdmin
    {
        ProviderProfileData getProviderProfileData(int Providerid);
    }
}