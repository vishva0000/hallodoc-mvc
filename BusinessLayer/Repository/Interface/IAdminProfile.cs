using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{
    public interface IAdminProfile
    {
        ProfileData AdminProfileDetails(string email);
    }
}