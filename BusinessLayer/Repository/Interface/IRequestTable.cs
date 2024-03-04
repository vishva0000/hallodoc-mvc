using DataLayer.DTO.AdminDTO;


namespace BusinessLayer.Repository.Interface
{
    public interface IRequestTable
    {
        void AssignCase(int assign_req_id, string phy_region, string phy_id, string assignNote);
        void BlockCase(int block_req_id, string blocknote);
        void CancelCase(cancelcase model);
        List<RequestTableData> requestTableData(int status, int requesttype);
    }
}