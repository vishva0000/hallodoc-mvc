using BusinessLayer.Repository.Implementation;
using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{
    public interface IProviderData
    {
        List<ProviderDetails> getProviderData(string search);
    }
}