using BusinessLayer.Repository.Interface;
using DataLayer.DTO;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HalloDoc.Controllers
{
    public class AdminController : Controller
    {
        public HallodocContext db;
        private readonly IAdminDashboard adminDashboardService;
        private readonly IRequestTable requestTableService;


        public AdminController(HallodocContext context, IAdminDashboard adminDashboard, IRequestTable requestTable)
        {
            this.db = context;
            this.adminDashboardService = adminDashboard;
            this.requestTableService = requestTable;

        }
        public IActionResult AdminDashboard()
        {
            AdminDashboarddata data = adminDashboardService.countrequest();
            return View(data);
           
        }
       
        public IActionResult NewState(int reqStaus, int requesttype)
        {
            List<RequestTableData> data = requestTableService.requestTableData(reqStaus, requesttype);
            return PartialView("_NewTable", data);
        }
        public void CancelCase(cancelcase model)
        {
            requestTableService.CancelCase(model);
           
        }
        
        public void AssignCase(int assign_req_id, string phy_region, string phy_id, string assignNote)
        {
            requestTableService.AssignCase(assign_req_id, phy_region, phy_id, assignNote);
          
           
        }
        public void BlockCase( int block_req_id, string blocknote)
        {
            requestTableService.BlockCase(block_req_id, blocknote);           
        }

        public IActionResult FetchRegions()
        {
            var regions = db.Regions.Select(r => new
            {
                regionid = r.RegionId,
                name = r.Name
            }).ToList();

            return Ok(regions);
        }
        
        public IActionResult FetchPhysician(int selectregionid)
        {
            var physicians = db.Physicians.Where(r=>r.RegionId == selectregionid).Select(r => new
            {
                physicianid = r.PhysicianId,
                name = r.FirstName + " " + r.LastName
            }).ToList();

            return Ok(physicians);
        }
        
        public IActionResult ViewUploads()
        {
            return View();
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
            TempData["Requestid"] = Requestid;
            var data = db.RequestNotes.Where(a=>a.RequestId == Requestid).FirstOrDefault();
            ViewNotesData notes = new ViewNotesData();
            if (data != null)
            {
               
                notes.physicianNotes = data.PhysicianNotes;
                notes.adminNotes = data.AdminNotes;
            }
            return View(notes);
            
        }

        [HttpPost]
        public ActionResult ViewNotes(ViewNotesData model)
        {
            var requid = (int)TempData["Requestid"];
            var data = db.RequestNotes.Where(a=>a.RequestId == requid).FirstOrDefault();
            if(data != null)
            {
                data.AdminNotes = model.Additional;
                db.RequestNotes.Update(data);
            }
            else
            {
                RequestNote reqnote = new RequestNote();
                reqnote.RequestId = requid;
                reqnote.CreatedBy = "admin";
                reqnote.CreatedDate = DateTime.Now;
                reqnote.AdminNotes = model.Additional;
                db.RequestNotes.Add(reqnote);
            }
            db.SaveChanges();
            return RedirectToAction("AdminDashboard", "Admin");
        }
    }
}
