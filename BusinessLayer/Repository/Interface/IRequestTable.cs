using BusinessLayer.Repository.IRepository;
using DataLayer.DTO.AdminDTO;


namespace BusinessLayer.Repository.Interface
{
    public interface IRequestTable
    {
        void AssignCase(int assign_req_id, string phy_region, string phy_id, string assignNote);
        void BlockCase(int block_req_id, string blocknote);
        void CancelCase(cancelcase model);
        void ClearCase(int clear_req_id);
        List<RequestTableData> requestTableData(int status, int requesttype, string search);
        //List<RequestTableData> searchintable(int status, int requesttype, string search);
        void SendAgreementMail(int arg_req_id, string argPhone, string argEmail);
        void TransferCase(int transfer_req_id, string phy_region, string phy_id, string transferNote);

        public PagedList<RequestTableData> GetData(int reqStaus, int requesttype, string searchin, int page, out int Count);
    }
}