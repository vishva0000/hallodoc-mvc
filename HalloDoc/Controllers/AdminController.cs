using HalloDoc.DTO.AdminDTO;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Controllers
{
    public class AdminController : Controller
    {
        public HallodocContext db;

        public AdminController(HallodocContext context)
        {
            this.db = context;


        }
        public IActionResult AdminDashboard()
        {
            List<RequestTableData> data = new();
            var details = db.RequestClients.Include(a => a.Request);
            foreach (var item in details)
            {
                RequestTableData request = new RequestTableData()
                {
                    Name = item.FirstName + " " + item.LastName,
                    Requestor = item.Request.FirstName,
                    Phone = item.PhoneNumber,
                    Address = item.Street + " " + item.City + " " + item.State,


                };
                data.Add(request);
            }

            //return PartialView("_Table");
            return View(data);
           
        }
        public IActionResult test()
        {
            return View();
        }
        public IActionResult Table(int reqStaus)
        {
            List<RequestTableData> data = RequestsTable(1);
            return PartialView("Table", data);
        }
       public List<RequestTableData> RequestsTable(int status)
        {
           
            List<RequestTableData> data = new();
            List<Request> r = db.Requests.Where(a => a.Status == status).ToList();
            var details = db.Requests;

            foreach (var item in r)
            {

                RequestTableData request = new RequestTableData();
            
                request.RequestId = item.RequestId;
                request.RequestTypeId = item.RequestTypeId;
                request.Requestor = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                request.Name = item.FirstName + " " + item.LastName;
                request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                request.Phone = item.PhoneNumber;
                data.Add(request);
            }

            return data;
        }

        public ActionResult ViewCase()
        {
            return View();
        }
    }
}
