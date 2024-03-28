using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{
    public interface IUserAccess
    {
        List<UserAccessData> getData(string search);
    }
}