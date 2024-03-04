using DataLayer.DTO;

namespace BusinessLayer.Repository.Interface
{
    public interface IRequestForMe
    {
        void Requestforme(PatientRequestModel model, string UserEmail);
    }
}