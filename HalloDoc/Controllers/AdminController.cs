using BusinessLayer.Repository.Interface;
using DataLayer.DTO;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;
using BusinessLayer.Utility;
using BusinessLayer.Repository.Implementation;

namespace HalloDoc.Controllers
{
    [Auth("admin")]
    public class AdminController : Controller
    {
        public HallodocContext db;
        public readonly IHostingEnvironment _environment;
        private readonly IAdminDashboard adminDashboardService;
        private readonly IRequestTable requestTableService;
        private readonly IViewUploads viewUploadsService;
        private readonly ICreateRequest createRequestService;
        private readonly IEmailSender emailSenderService;


        public AdminController(HallodocContext context, 
            IHostingEnvironment environment, 
            IAdminDashboard adminDashboard, 
            IRequestTable requestTable, 
            IViewUploads viewUploads, 
            IEmailSender emailSender, 
            ICreateRequest createRequest)
        {
            this.db = context;
            this.adminDashboardService = adminDashboard;
            _environment = environment;
            this.requestTableService = requestTable;
            this.viewUploadsService = viewUploads;
            this.emailSenderService = emailSender;
            this.createRequestService = createRequest;
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
        [HttpGet]
        public IActionResult CreateRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRequest(CreateRequestData model)
        {
            if (ModelState.IsValid)
            {
                createRequestService.AdminRequest(model);

            }
            return View();
            //return RedirectToAction("AdminDashboard", "Admin");
        }
        public void CancelCase(cancelcase model)
        {
            requestTableService.CancelCase(model);
           
        }
        
        public void AssignCase(int assign_req_id, string phy_region, string phy_id, string assignNote)
        {
            requestTableService.AssignCase(assign_req_id, phy_region, phy_id, assignNote);
          
           
        }
        
        public void TransferCase(int transfer_req_id, string phy_region, string phy_id, string transferNote)
        {

            requestTableService.TransferCase(transfer_req_id, phy_region, phy_id, transferNote);
           
        }
        public void BlockCase( int block_req_id, string blocknote)
        {
            requestTableService.BlockCase(block_req_id, blocknote);           
        }
         public void ClearCase( int clear_req_id)
         {
            requestTableService.ClearCase(clear_req_id);           
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
        
        public IActionResult FetchProfession()
        {
            var profession = db.HealthProfessionalTypes.Select(r => new
            {
                typeid = r.HealthProfessionalId,
                name = r.ProfessionName
            }).ToList();

            return Ok(profession);
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
        
        public IActionResult FetchBusiness(int businessid)
        {
            var business = db.HealthProfessionals.Where(r=>r.Profession == businessid).Select(r => new
            {
                businessid = r.VendorId,
                name = r.VendorName
            }).ToList();

            return Ok(business);
        }
        public IActionResult BusinessDetails(int businessid)
        {
            var business = db.HealthProfessionals.Where(r=>r.Profession == businessid).Select(r => new
            {
                businessid = r.VendorId,
                name = r.VendorName,
                contact = r.BusinessContact,
                email = r.Email,
                fax= r.FaxNumber
            }).ToList();

            return Ok(business);
        }


        [HttpGet]
        public IActionResult ViewUploads(int viewid)
        {
            ViewUploadsModel details = viewUploadsService.Uploadedfilesdata(viewid);

           
            return View(details);
        }
        public IActionResult Downloadfile(int docid)
        {
            var data = db.RequestWiseFiles.Where(a => a.RequestWiseFileId == docid).FirstOrDefault();
            string filePath = data.FileName;

            if (filePath == null)
            {
                return RedirectToAction("PatientDashboard", "Patient");
            }

            return PhysicalFile(filePath, MimeTypes.GetMimeType(filePath), Path.GetFileName(filePath));
        }
        public void DeleteFile(int fileid)
        {
            var data = db.RequestWiseFiles.Where(a => a.RequestWiseFileId == fileid).FirstOrDefault();
            data.IsDeleted = new BitArray(new bool[1] { true });

            db.RequestWiseFiles.Update(data);
            db.SaveChanges();

        }
        public void SendDocumentEmail(int id, List<string> files)
        {
            emailSenderService.SendEmailAsync("vishva.rami@etatvasoft.com", "files", "message", files);
        }

        [HttpPost]
        public void addFiles(List<IFormFile> file, int reqid)
        {

            foreach(var item in file)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, item.FileName);


                item.CopyTo(new FileStream(filePath, FileMode.Create));
                RequestWiseFile insertfile = new RequestWiseFile()
                {
                    RequestId = reqid,
                    FileName = filePath,
                    CreatedDate = DateTime.Now                   


                };
                db.RequestWiseFiles.Add(insertfile);
            }
           
            db.SaveChanges();
            RedirectToAction("ViewUploads", reqid);
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
        [HttpGet]
        public ActionResult AdminOrders(int ordreqid)
        {
            TempData["orderReqId"] = ordreqid;
            return View();
        }
        [HttpPost]
        public ActionResult AdminOrders(OrderData model)
        {
           

            OrderDetail data = new OrderDetail();
            
            data.VendorId = model.VendorId;
            data.RequestId = (int)TempData["orderReqId"];
            data.FaxNumber = model.Fax;
            data.Email = model.Email;
            data.BusinessContact = model.Contact;
            data.Prescription = model.Prescription;
            data.CreatedDate = DateTime.Now;
            data.NoOfRefill = int.Parse(model.NoOfRefill);

            db.OrderDetails.Add(data);
            db.SaveChanges();

            return RedirectToAction("AdminDashboard");
        }
    }
}
