using HalloDoc.DTO;
using HalloDoc.DTO.AdminDTO;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public IActionResult NewState(int reqStaus)
        {
            List<RequestTableData> data = RequestsTable(1);
            return PartialView("_NewTable", data);
        }
        public IActionResult PendingState(int reqStaus) 
        {
            List<RequestTableData> data = RequestsTable(2);
            return PartialView("_PendingTable", data);

        }
        public IActionResult ActiveState(int reqStaus) 
        {
            List<RequestTableData> data = RequestsTable(3);
            return PartialView("_ActiveTable", data);

        } 
        public IActionResult ConcludeState(int reqStaus) 
        {
            List<RequestTableData> data = RequestsTable(4);
            return PartialView("_ConcludeTable", data);

        } 
        public IActionResult ToCloseState(int reqStaus) 
        {
            List<RequestTableData> data = RequestsTable(5);
            return PartialView("_ToCloseTable", data);

        }
        public IActionResult UnpaidState(int reqStaus) 
        {
            List<RequestTableData> data = RequestsTable(6);
            return PartialView("_UnpaidTable", data);

        }
        public List<RequestTableData> RequestsTable(int status)
        {
            List<Request> r;
           if (status == 1)
            {
                r = db.Requests.Where(a => a.Status == 1).ToList();
            }
           else if(status == 2)
            {
                r = db.Requests.Where(a => a.Status == 2).ToList();
            }
           else if(status == 3)
            {
                r = db.Requests.Where(a => a.Status == 5 || a.Status == 6).ToList();
            }
           else if(status == 4)
            {
                r = db.Requests.Where(a => a.Status == 8).ToList();
            }
           else if(status == 5)
            {
                r = db.Requests.Where(a => a.Status == 13 || a.Status == 3 || a.Status==8).ToList();
            }
           else if(status == 6)
            {
                r = db.Requests.Where(a => a.Status == 8).ToList();
            }
            else
            {
                r = db.Requests.Where(a => a.Status == status).ToList();
            }
            List<RequestTableData> data = new();
             
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
                request.RequestedDate = item.CreatedDate;
                data.Add(request);
            }

            return data;
        }
        [HttpGet]
        public ActionResult ViewCase(int reqId)
        {

            TempData["requestid"] = reqId;

            var request = db.Requests.Where(a => a.RequestId == reqId).FirstOrDefault();
            var data = db.RequestClients.Where(a => a.RequestId == reqId).FirstOrDefault();
                ViewCaseData details = new ViewCaseData();
            details.RequestTypeId = request.RequestTypeId;
            details.Symp = data.Notes;
            details.Firstname = data.FirstName;
            details.Lastname = data.LastName;
            details.Email = data.Email;
            details.Phone = data.PhoneNumber;
            details.Street = data.Street;
            details.City = data.City;
            details.State = data.State;
                return View(details);
        }

        [HttpPost]
        public ActionResult ViewCase(ViewCaseData model)
        {
            var id = (int)TempData["requestid"];
            var data = db.RequestClients.Where(a => a.RequestId == id).FirstOrDefault();

            data.Notes = model.Symp;
            data.Street = model.Street;
            data.City = model.City;
            data.State = model.State;
            data.FirstName = model.Firstname;
            data.LastName = model.Lastname;
            data.Email = model.Email;
            data.Location = model.Room;

            db.RequestClients.Update(data);
            db.SaveChanges();
            return RedirectToAction("AdminDashboard");
        }

        [HttpGet]
        public ActionResult ViewNotes(int Requestid)
        {
            var data = db.RequestNotes.Where(a => a.RequestId == Requestid).FirstOrDefault();


            if (data != null)
            {
                ViewNotesData notesdata = new ViewNotesData();
                
                notesdata.adminNotes = data.AdminNotes;
                notesdata.physicianNotes = data.PhysicianNotes;
            }
            return View(data);
        }
    }
}
