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
using Castle.Core.Smtp;
using System.Globalization;
using NPOI.SS.Formula.Functions;
using static BusinessLayer.Utility.Export;
using NPOI.POIFS.Properties;
using NPOI.Util;
using Microsoft.AspNetCore.Components.Forms;
using NPOI.SS.Formula.Eval;
using System.Security.Policy;
using NPOI.POIFS.Crypt.Dsig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        private readonly BusinessLayer.Utility.IEmailSender emailSenderService;
        private readonly IEncounterform encounterformService;
        private readonly ICloseCase closecaseService;
        private readonly IAdminProfile adminprofileService;
        private readonly IProviderData providerDataService;
        private readonly IAccessRoles accessRolesService;

       

        public AdminController(HallodocContext context,
            IHostingEnvironment environment,
            IAdminDashboard adminDashboard,
            IRequestTable requestTable,
            IViewUploads viewUploads,
            BusinessLayer.Utility.IEmailSender emailSender,
            ICreateRequest createRequest,
            IEncounterform encounterform,
            ICloseCase closeCase,
            IAdminProfile adminProfile,
            IProviderData providerData,
            IAccessRoles accessRoles)
        {
            this.db = context;
            this.adminDashboardService = adminDashboard;
            _environment = environment;
            this.requestTableService = requestTable;
            this.viewUploadsService = viewUploads;
            this.emailSenderService = emailSender;
            this.createRequestService = createRequest;
            this.encounterformService = encounterform;
            this.closecaseService = closeCase;
            this.adminprofileService = adminProfile;
            this.providerDataService = providerData;
            this.accessRolesService = accessRoles;
        }
        public IActionResult AdminDashboard()
        {
            AdminDashboarddata data = adminDashboardService.countrequest();
            return View(data);

        }


        public IActionResult NewState(int reqStaus, int requesttype, string searchin)
        {
            List<RequestTableData> data = requestTableService.requestTableData(reqStaus, requesttype, searchin);
            //List<RequestTableData> data = getPaginatedData(reqStaus, requesttype, searchin, page);
            
            return PartialView("_NewTable", data);
        }
        [AllowAnonymous]
        public IActionResult NewState1(int reqStaus, int requesttype, string searchin,int page)
        {
            //ViewBag.Count = 5;
            //    List<RequestTableData> data = requestTableService.requestTableData(reqStaus, requesttype, searchin);
            List<RequestTableData> data = getPaginatedData(reqStaus, requesttype, searchin, page);
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
        [HttpGet]
        public IActionResult Profile()
        {
            string? UserEmail = Request.Cookies["UserId"];

            ProfileData data = adminprofileService.AdminProfileDetails(UserEmail);
            return View(data);
        }
        public void SendLink(string emailAdd)
        {

            emailSenderService.SendEmailAsync("vishva.rami@etatvasoft.com", "Create Request", "Please <a href=\"https://localhost:44301/Patient/PatientRequest\">Create Request</a>");
        }
        public FileResult ExportCurrent(int reqStaus, int requesttype, int page)
        {
            List<RequestTableData> data = getPaginatedData(reqStaus, requesttype, null, page);
            //List<RequestTableData> data = requestTableService.requestTableData(reqStaus, requesttype, null);
            var file = ExcelHelper.CreateFile(data);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "requests.xlsx");
        }

        public FileResult ExportALLReq()
        {
            List<RequestTableData> data = requestTableService.requestTableData(0, 0, null);

            var file = ExcelHelper.CreateFile(data);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "requests.xlsx");
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
        public void BlockCase(int block_req_id, string blocknote)
        {
            requestTableService.BlockCase(block_req_id, blocknote);
        }
        public void ClearCase(int clear_req_id)
        {
            requestTableService.ClearCase(clear_req_id);
        }
        //public void SendAgreement()
        public void SendAgreement(int arg_req_id, string argPhone, string argEmail)
        {
            string url = "https://localhost:44301/Patient/ReviewAgreement/" + arg_req_id;
            //_EmailSender.SendEmailAsync("vishva.rami@etatvasoft.com", "ResetPassword", ResetPassLink);
            //emailSenderService.SendEmailAsync("vishva.rami@etatvasoft.com", "Review Agreement", "Please <a href=\"https://localhost:44301/Patient/ReviewAgreement\">Review Agreement</a>");
            emailSenderService.SendEmailAsync("vishva.rami@etatvasoft.com", "Review Agreement", url);
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
            var physicians = db.Physicians.Where(r => r.RegionId == selectregionid).Select(r => new
            {
                physicianid = r.PhysicianId,
                name = r.FirstName + " " + r.LastName
            }).ToList();

            return Ok(physicians);
        }

        public IActionResult FetchBusiness(int businessid)
        {
            var business = db.HealthProfessionals.Where(r => r.Profession == businessid).Select(r => new
            {
                businessid = r.VendorId,
                name = r.VendorName
            }).ToList();

            return Ok(business);
        }
        public IActionResult BusinessDetails(int businessid)
        {
            var business = db.HealthProfessionals.Where(r => r.Profession == businessid).Select(r => new
            {
                businessid = r.VendorId,
                name = r.VendorName,
                contact = r.BusinessContact,
                email = r.Email,
                fax = r.FaxNumber
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
            emailSenderService.SendFileAsync("vishva.rami@etatvasoft.com", "files", "message", files);
        }

        [HttpPost]
        public void addFiles(List<IFormFile> file, int reqid)
        {

            foreach (var item in file)
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
        //[Route("[controller]/[action]/{reqId:int}")]
        public IActionResult ViewCase(int reqId)
        {
                  
            var request = db.Requests.Where(a => a.RequestId == reqId).FirstOrDefault();
            var data = db.RequestClients.Where(a => a.RequestId == reqId).FirstOrDefault();
            
                ViewCaseData details = new ViewCaseData();
            
            if (data.IntYear != null && data.IntDate != null && data.StrMonth != null)
            {
                int month = DateTime.ParseExact(data.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                details.Dob = new DateTime((int)data.IntYear, month, (int)data.IntDate);
            }
            details.RequestTypeId = request.RequestTypeId;
            details.RequestId = request.RequestId;
            details.Symp = data.Notes;
            details.Firstname = data.FirstName;
            details.Lastname = data.LastName;
            details.Email = data.Email;
            details.Phone = data.PhoneNumber;
            details.Street = data.Street;
            details.City = data.City;
            details.State = data.State;
            details.Room = data.Location;
            details.status = request.Status;
                return View(details);
        }

        [HttpPost]
        public ActionResult ViewCase(ViewCaseData model, int reqId)
        {
           
            var data = db.RequestClients.Where(a => a.RequestId == reqId).FirstOrDefault();

            data.Notes = model.Symp;
            data.Street = model.Street;
            data.PhoneNumber = model.Phone;
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

            var statustrans = db.RequestStatusLogs.Where(a => a.RequestId == Requestid && a.Status==2).ToList();

            List<string> tnotes = new();

            foreach(var item in statustrans)
            {
                
                string phy = db.Physicians.Where(a => a.PhysicianId == item.PhysicianId).FirstOrDefault().FirstName +" "+ db.Physicians.Where(a => a.PhysicianId == item.PhysicianId).FirstOrDefault().LastName;
                string assnote = item.Notes;
                string note = "Request is assigned to " + phy + ": " +assnote;
                tnotes.Add(note);
            }

            ViewNotesData notes = new ViewNotesData();
            if (data != null){
                notes.transfernote = tnotes;
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

        [HttpGet]
        public ActionResult Encounter(int encreqid)
        {
            TempData["encreqid"] = encreqid;
            EncounterFormData model = encounterformService.encounterformdata(encreqid);

            return View(model);

        }
        [HttpPost]
        public ActionResult Encounter(EncounterFormData model)
        {

            var id = (int)TempData["encreqid"];

            encounterformService.encounterSaveChanges(model, id);

            return View();
        }
        
        
        [HttpGet]
        [Route("[controller]/[action]/{closeid:int}")]
        public IActionResult Closecase(int closeid)
        {
         
            ViewUploadsModel data = closecaseService.Closecasefiles(closeid);
            return View(data);
        }


       
        [HttpPost]
        public void Closecasesubmit(int closeid)
        {
            //closecaseService.Closingcase(closeid);
            //return Redirect(Url.Action("AdminDashboard", "Admin"));
        }

        public void CloseCaseUpdateByAdmin(int id, string email, string phone)
        {
            RequestClient r = db.RequestClients.Where(u => u.RequestId == id).FirstOrDefault();
            r.Email = email;
            r.PhoneNumber = phone;
            db.SaveChanges();
        }
        public void ClosecasePatientedit()
        {

        }
        [HttpPost]
        public IActionResult ProfileMailEdit(ProfileData model)
        {
            var admin = db.Admins.Where(a => a.AdminId == model.id).FirstOrDefault();

            admin.Address1 = model.Address1;
            admin.Address2 = model.Address2;
            admin.City = model.City;
            admin.Zip = model.Zipcode;
            admin.AltPhone = model.MailNo;
            db.Admins.Update(admin);
            db.SaveChanges();
            return RedirectToAction("Profile", "Admin");
        }

        [HttpPost]
        public IActionResult AdminDetailsEdit(ProfileData model)
        {
            var admin = db.Admins.Where(a=>a.AdminId == model.id).FirstOrDefault();
            List<AdminRegion> adregion = db.AdminRegions.Where(a => a.AdminId == model.id).ToList();
            admin.FirstName = model.Firstname;
            admin.LastName = model.Lastname;
            admin.Email = model.ConfirmEmail;
            admin.Mobile = model.Phone;

            foreach(var item in adregion)
            {
                if (!model.regionmodified.Contains(item.RegionId))
                {
                    db.AdminRegions.Remove(item);
                    
                }
                model.regionmodified.Remove(item.RegionId);
            }

            foreach(var item in model.regionmodified)
            {
                AdminRegion addnew = new()
                {
                    AdminId = model.id,
                    RegionId = item
                };
                db.AdminRegions.Add(addnew); 
               
            }
            db.Admins.Update(admin);
            db.SaveChanges();
            return RedirectToAction("Profile", "Admin");
        }
        
        [HttpPost]
        public IActionResult ResetAdminPass(string Username,string Password)
        {
            var asp = db.AspNetUsers.Where(a => a.UserName == Username).FirstOrDefault();
            asp.PasswordHash = Password;
            db.AspNetUsers.Update(asp);
            db.SaveChanges();
            return RedirectToAction("Profile", "Admin");
        }

        public IActionResult Providers()
        {
            return View();
        }

        public IActionResult ProviderDataTable(string search)
        {
            List<ProviderDetails> data = providerDataService.getProviderData(search);

            return PartialView("_ProviderTable", data);
        }

        public IActionResult ContactProvider(int physicianid, string Message)
        {
            emailSenderService.SendEmailAsync("vishva.rami@etatvasoft.com", "Messgae sent by Admin", Message);


            return RedirectToAction("Profile", "Admin");
        }

        public IActionResult ManageAccess()
        {
            List<RoleData> data= accessRolesService.getAllRoles();
            return View(data);
        }  
        
        public IActionResult CreateRoleAll()
        {
            return View();
        }

        public void CreateRole(string RoleName, string AccountType, List<int> selectedMenu)
        {
            accessRolesService.CreateRole(RoleName,AccountType,selectedMenu);
        }

        public IActionResult GetMenus(string actype)
        {
            int type = int.Parse(actype);
            if (type == 0)
            {
                
                var data = db.Menus.Select(r => new
                {
                    id = r.MenuId,
                    name = r.Name
                }).ToList();
                return Ok(data);
            }
            else
            {
                var data = db.Menus.Where(a => a.AccountType == type).Select(r => new
                {
                    id = r.MenuId,
                    name = r.Name
                }).ToList();
                return Ok(data);
            }       

          
        }
        [AllowAnonymous]
        public List<RequestTableData> getPaginatedData(int reqStaus, int requesttype, string searchin,int page)
        {
            List<RequestTableData> data = requestTableService.GetData(reqStaus, requesttype,searchin,page,out int Count);
            ViewBag.Count = Count;
            return (data);

        }
    }
}
