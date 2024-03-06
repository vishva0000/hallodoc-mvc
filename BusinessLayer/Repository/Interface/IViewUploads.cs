using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{
    public interface IViewUploads
    {
        ViewUploadsModel Uploadedfilesdata(int viewid);
    }
}