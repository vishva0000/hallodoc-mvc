

using BusinessLayer.Repository.Implementation;
using DataLayer.DTO;

namespace BusinessLayer.Repository.Interface
{
    public interface IPatientRequest
    {
        void PatientRequestData(PatientRequestModel model);
    }
}