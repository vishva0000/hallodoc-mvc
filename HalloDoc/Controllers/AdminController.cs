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
            AdminDashboard data = new AdminDashboard();
            data.New = db.Requests.Where(a => a.Status == 1).Count();
            data.Pending = db.Requests.Where(a => a.Status == 2).Count();
            data.Active = db.Requests.Where(a => a.Status == 5 || a.Status == 6).Count();
            data.Conclude = db.Requests.Where(a => a.Status == 8).Count();
            data.ToClose = db.Requests.Where(a => a.Status == 13 || a.Status == 3 || a.Status == 8).Count();
            data.UnPaid = db.Requests.Where(a => a.Status == 8).Count();

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
            List<RequestTableData> data = new();

            if (status == 1)
            {
                r = db.Requests.Where(a => a.Status == 1).ToList();
              

                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();

                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
           else if(status == 2)
            {
                r = db.Requests.Where(a => a.Status == 2).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();

                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
           else if(status == 3)
            {
                r = db.Requests.Where(a => a.Status == 5 || a.Status == 6).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();

                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
           else if(status == 4)
            {
                r = db.Requests.Where(a => a.Status == 8).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();

                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
           else if(status == 5)
            {
                r = db.Requests.Where(a => a.Status == 13 || a.Status == 3 || a.Status==8).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();

                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
           else if(status == 6)
            {
                r = db.Requests.Where(a => a.Status == 8).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();

                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
            else
            {
                r = db.Requests.Where(a => a.Status == status).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();

                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
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

            TempData["requestid"]=Requestid;
            if (data != null)
            {
                ViewNotesData notesdata = new ViewNotesData();
                
                notesdata.adminNotes = data.AdminNotes;
                notesdata.physicianNotes = data.PhysicianNotes;

                return View(notesdata);
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult ViewNotes(ViewNotesData model)
        {
            var requid = (int)TempData["requestid"];
            var data = db.RequestNotes.Where(a=>a.RequestId == requid).FirstOrDefault();
            data.AdminNotes = model.Notes;
            db.RequestNotes.Update(data);
            db.SaveChanges();
            return View();
        }
    }
}
