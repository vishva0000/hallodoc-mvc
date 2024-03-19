using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{

    public interface ICloseCase
    {
        ViewUploadsModel Closecasefiles(int reqid);
        void Closingcase(int reqid);
    }
}