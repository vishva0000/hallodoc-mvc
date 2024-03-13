using DataLayer.DTO.AdminDTO;

namespace BusinessLayer.Repository.Interface
{
    public interface IEncounterform
    {
        EncounterFormData encounterformdata(int encreqid);
        void encounterSaveChanges(EncounterFormData model, int id);
    }
}