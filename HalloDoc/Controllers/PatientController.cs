using HalloDoc.DTO;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using HalloDoc.Utility;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System.Globalization;

namespace HalloDoc.Controllers
{
    public class PatientController : Controller
    {
        public HallodocContext context;
        public readonly IHostingEnvironment _environment;
        const string CookieUserEmail = "UserId";
        const string emailforreset = "EmailId";

        IEmailSender _EmailSender;



        public PatientController(HallodocContext context, IHostingEnvironment environment, IEmailSender emailSender)
        {
            this.context = context;
            _environment = environment;
            _EmailSender = emailSender;

        }

        [HttpGet]
        public IActionResult PatientLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PatientLogin(PatientLogin model)
        {
            if (ModelState.IsValid)
            {



                var email = model.Email;
                var password = model.Password;
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);

                Response.Cookies.Append(CookieUserEmail, model.Email, options);

                if (email != null || password != null)
                {
                    var user = context.AspNetUsers.Where(a => a.Email == email).FirstOrDefault();

                    if (user != null)
                    {
                        if (user.PasswordHash == password)
                        {
                            return RedirectToAction("PatientDashboardPage", "Patient");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Wrong Password");
                        }

                    }
                    else
                    {

                        ModelState.AddModelError("", "Failed to login");
                        return View(model);


                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(ForgetPassword model)
        {
         
            string ResetPassLink = "https://localhost:44301/Patient/ResetPassword?email="+model.email;
            _EmailSender.SendEmailAsync("vishva.rami@etatvasoft.com", "ResetPassword", ResetPassLink);

            return RedirectToAction("PatientLogin", "Patient");
        }
        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            TempData["email"] = email;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPassword model)
        {
            var email = TempData["email"];
            var data = context.AspNetUsers.Where(a => a.Email == email).FirstOrDefault();
            data.PasswordHash = model.Password;
            context.Update(data);
            context.SaveChanges();
            return RedirectToAction("PatientLogin", "Patient");
        }
        [HttpGet]
        public IActionResult PatientRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PatientRequest(PatientRequest model)
        {
            var dob = model.dob;
            int day = dob.Day;
            var mon = dob.ToString("MMMM");
            var year = dob.Year;
            var email = model.Email;


            User insertuser = new User();
            AspNetUser insertAspNetUser = new AspNetUser();
            var user = context.Users.Where(a => a.Email == email).FirstOrDefault();
            if (user == null)
            {
                Guid obj = Guid.NewGuid();
                insertAspNetUser.Id = obj.ToString();
                insertAspNetUser.Email = email;
                insertAspNetUser.PasswordHash = model.Password;
                insertAspNetUser.UserName = model.Firstname;
                insertAspNetUser.CreatedDate = DateTime.Now;

                context.AspNetUsers.Add(insertAspNetUser);

                insertuser.AspNetUser = insertAspNetUser;
                insertuser.FirstName = model.Firstname;
                insertuser.LastName = model.Lastname;
                insertuser.Email = model.Email;
                insertuser.Mobile = model.Phone;
                insertuser.Street = model.Street;
                insertuser.City = model.City;
                insertuser.State = model.State;
                insertuser.ZipCode = model.Zipcode;
                insertuser.IntDate = day;
                insertuser.StrMonth = mon;
                insertuser.IntYear = year;
                insertuser.CreatedBy = "Patient";
                insertuser.CreatedDate = DateTime.Now;

                context.Users.Add(insertuser);
            }
            

            Request insertrequest = new Request()
            {
                RequestTypeId = 2,
                User = insertuser,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                PhoneNumber = model.Phone,
                Email = model.Email,
                Status = 1,
                CreatedDate = DateTime.Now,
                IsUrgentEmailSent = new BitArray(new bool[1] { false })

            };
            context.Requests.Add(insertrequest);

            RequestClient insertrequestclient = new RequestClient()
            {
                Request = insertrequest,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                PhoneNumber = model.Phone,
                Email = model.Email,
                IntDate = day,
                StrMonth = mon,
                IntYear = year,
                Street = model.Street,
                State = model.State,
                City = model.City,
                ZipCode = model.Zipcode

            };
            context.RequestClients.Add(insertrequestclient);

            if (model.File != null)
            {
                //int lastRecord = context.Requests.OrderByDescending(m => m.RequestId).FirstOrDefault().RequestId;
                //string path = _environment.WebRootPath;
                //string filePath = "content/" + model.File.FileName;
                //string fullPath = Path.Combine(path, filePath);

                //IFormFile file1 = model.File;
                //FileStream stream = new FileStream(fullPath, FileMode.Create);
                //file1.CopyTo(stream);

                //Request? request = context.Requests.FirstOrDefault(i => i.Email == model.Email);

                //var fileName = model.File?.FileName;
                //var doctType = model.File?.ContentType;

                //var file = new RequestWiseFile()
                //{
                //    Request = insertrequest,
                //    FileName = fullPath,
                //    Ip = doctType,
                //};
                //context.Add(file);
                foreach(var item in model.File)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads/Patient");
                    var filePath = Path.Combine(uploads, item.FileName);
                    var file = item;

                    file.CopyTo(new FileStream(filePath, FileMode.Create));
                    RequestWiseFile insertfile = new RequestWiseFile()
                    {
                        Request = insertrequest,
                        FileName = filePath,
                        CreatedDate = DateTime.Now

                    };
                    context.RequestWiseFiles.Add(insertfile);
                }
               

            }
            context.RequestClients.Add(insertrequestclient);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public Boolean IsPatientPresent(string email)
        {
            var data = context.AspNetUsers.Where(a => a.Email == email).FirstOrDefault();
            if (data == null)
            {
                return false;
            }
            else
            {
                return true;

            }
        }
        [HttpGet]
        public IActionResult FamilyRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FamilyRequest(FamilyRequest model)
        {
            Request insertrequest = new()
            {
                RequestTypeId = 3,
                FirstName = model.F_Firstname,
                LastName = model.F_Lastname,
                PhoneNumber = model.F_Phone,
                Email = model.F_Email,
                RelationName = model.F_relation,
                Status = 1,
                CreatedDate = DateTime.Now,
                IsUrgentEmailSent = new BitArray(new bool[1] { false })

            };
            context.Requests.Add(insertrequest);
            var dob = model.dob;
            int day = dob.Day;
            var mon = dob.ToString("MMMM");
            var year = dob.Year;

            RequestClient insertrequestclient = new RequestClient()
            {
                Request = insertrequest,
                FirstName = model.P_Firstname,
                LastName = model.P_Lastname,
                PhoneNumber = model.P_Phone,
                Email = model.P_Email,
                Notes = model.P_symp,
                Location = model.P_Location,
                IntDate = day,
                StrMonth = mon,
                IntYear = year,
                Street = model.P_Street,
                State = model.P_State,
                City = model.P_City

            };
            context.RequestClients.Add(insertrequestclient);
            if (model.P_File != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads/Family");
                var filePath = Path.Combine(uploads, model.P_File.FileName);
                var file = model.P_File;

                file.CopyTo(new FileStream(filePath, FileMode.Create));
                RequestWiseFile insertfile = new RequestWiseFile()
                {
                    Request = insertrequest,
                    FileName = filePath,
                    CreatedDate = DateTime.Now

                };
                context.RequestWiseFiles.Add(insertfile);
            }



            context.SaveChanges();
            _EmailSender.SendEmailAsync("vishva.rami@etatvasoft.com", "CreateAccount", "Please <a href=\"https://localhost:44301/Patient/CreatePatient\">login</a>");

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult BusinessRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BusinessRequest(BusinessRequest model)
        {
            
            Request insertrequest = new Request()
            {
                RequestTypeId = 1,
                FirstName = model.B_Firstname,
                LastName = model.B_Lastname,
                PhoneNumber = model.B_Phone,
                Email = model.B_Email,
                Status = 1,
                CreatedDate = DateTime.Now,
                IsUrgentEmailSent = new BitArray(new bool[1] { false })
            };
            context.Requests.Add(insertrequest);

            var dob = model.P_dob;
            int day = dob.Day;
            var mon = dob.ToString("MMMM");
            var year = dob.Year;

            RequestClient insertrequestclient = new RequestClient()
            {
                Request = insertrequest,
                FirstName = model.P_Firstname,
                LastName = model.P_Lastname,
                PhoneNumber = model.P_Phone,
                Email = model.P_Email,
                Notes = model.P_symp,
                Location = model.P_Room,
                IntDate = day,
                StrMonth = mon,
                IntYear = year,
                Street = model.P_Street,
                State = model.P_State,
                City = model.P_City
               

            };
            context.RequestClients.Add(insertrequestclient);

            Business insertbusiness = new Business()
            {
                Name = model.B_Hotel,
                PhoneNumber = model.B_Phone,
                CreatedDate = DateTime.Now

            };
            context.Businesses.Add(insertbusiness);

            RequestBusiness insertrequestbusiness = new RequestBusiness()
            {
                Business = insertbusiness,
                Request = insertrequest


            };
            context.RequestBusinesses.Add(insertrequestbusiness);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
            
        }
        [HttpGet]
        public IActionResult ConciergeRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ConciergeRequest(ConciergeRequest model)
        {
            Request insertrequest = new Request()
            {
                RequestTypeId = 4,
                FirstName = model.C_Firstname,
                LastName = model.C_Lastname,
                PhoneNumber = model.C_Phone,
                Status = 1,
                CreatedDate = DateTime.Now,
                IsUrgentEmailSent = new BitArray(new bool[1] { false })

            };
            context.Requests.Add(insertrequest);

            var dob = model.P_dob;
            int day = dob.Day;
            var mon = dob.ToString("MMMM");
            var year = dob.Year;


            var name = model.C_Firstname + " " + model.C_Lastname;
            Concierge insertconcierge = new Concierge()
            {
                ConciergeName = name,
                Address = model.C_Location,
                Street = model.C_Street,
                State = model.C_State,
                City = model.C_City,
                ZipCode = model.C_Zipcode,
                CreatedDate = DateTime.Now,
            };
            context.Concierges.Add(insertconcierge);
            RequestClient insertrequestclient = new RequestClient()
            {
                FirstName = model.P_Firstname,
                LastName = model.P_Lastname,
                PhoneNumber = model.P_phone,
                Email = model.P_email,
                Notes = model.P_symp,
                Location = model.P_Location,
                IntDate = day,
                StrMonth = mon,
                IntYear = year




            };
            context.RequestClients.Add(insertrequestclient);
            context.SaveChanges();
            _EmailSender.SendEmailAsync("tatva.dotnet.vishvarami@outlook.com", "Hello", "Please <a href=\"https://localhost:44301/Patient/CreatePatient\">login</a>");

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult CreatePatient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePatient(CreatePatient model)
        {
            if (ModelState.IsValid)
            {


                AspNetUser insertuser = new AspNetUser()
                {
                    Email = model.Email,
                    PasswordHash = model.Password
                };
                context.AspNetUsers.Add(insertuser);
                context.SaveChanges();
            }
            return View();
        }
        public IActionResult PatientDashboardPage()
        {
            string? UserEmail = Request.Cookies[CookieUserEmail];
            List<PatientDashboard> data = new();
            var details = context.Requests.Where(a => a.Email == UserEmail).Include(a => a.RequestWiseFiles);

            foreach (var item in details)
            {
                PatientDashboard dashboard = new PatientDashboard()
                {
                    CreatedDate = item.CreatedDate,
                    status = item.Status,
                    count = item.RequestWiseFiles.Count,
                    RequestID = item.RequestId
                };
                data.Add(dashboard);
            }
            return View(data);
            //return View();
        }
        [HttpGet]
        public IActionResult PatientProfile()
        {
            string? UserEmail = Request.Cookies[CookieUserEmail];
            PatientProfile model = new PatientProfile();
            var details = context.Users.Where(a => a.Email == UserEmail).Include(a => a.Requests).FirstOrDefault();

            //string month = details.StrMonth;
            //int day = (int)details.IntDate;
            //int year = (int)details.IntYear;

            //DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            //int monthInNum = dtfi.MonthNames.ToList().IndexOf(month) + 1;
            //string dateOfBirth = $"{day}-{monthInNum}-{year}";
            //DateTime DOB = DateTime.ParseExact(dateOfBirth, "dd-MM-yyyy", CultureInfo.InvariantCulture);


            model.Firstname = details.FirstName;
            model.Lastname = details.LastName;
            model.phone = details.Mobile;
            model.email = details.Email;
            model.street = details.Street;
            model.city = details.City;
            model.state = details.State;
            model.zipcode = details.ZipCode;
            //model.dob = DOB;

            return View(model);
        }
        [HttpPost]
        public IActionResult PatientProfile(PatientProfile model)
        {
            string? UserEmail = Request.Cookies[CookieUserEmail];

            var details = context.Users.Where(a => a.Email == UserEmail).FirstOrDefault();
            var dob = model.dob;
            int day = dob.Day;
            var mon = dob.ToString("MMMM");
            var year = dob.Year;

            details.FirstName = model.Firstname;
            details.LastName = model.Lastname;
            details.Mobile = model.phone;
            details.Email = model.email;
            details.Street = model.street;
            details.City = model.city;
            details.State = model.state;
            details.ZipCode = model.zipcode;
            details.StrMonth = mon;
            details.IntDate = day;
            details.IntYear = year;

            context.Users.Update(details);
            context.SaveChanges();


            return View();
        }

        [HttpGet]
        public IActionResult ViewDocument(int RequestID)
        {
            var file = context.RequestWiseFiles.Where(a => a.RequestId == RequestID);
            var req = context.Requests.Where(a => a.RequestId == RequestID).FirstOrDefault();
            var name = context.RequestClients.Where(a => a.RequestId == RequestID).FirstOrDefault();
            List<FileData> data = new();

            foreach (var files in file)
            {
                FileData FileDataList = new()
                {
                    FileName = files.FileName,
                    CreatedBy = name.FirstName,
                    CreatedDate = files.CreatedDate,
                    DocumentId = files.RequestWiseFileId

                };
                data.Add(FileDataList);
            }
            ViewDocumentList doc = new()
            {
                Name = name.FirstName,
                ConfirmationNumber = req.ConfirmationNumber,
                Document = data,
                RequestId = RequestID

            };
            return View(doc);
        }
        public IActionResult Download(int Download)
        {
            var data = context.RequestWiseFiles.Where(a => a.RequestWiseFileId == Download).FirstOrDefault();
            string filePath = data.FileName;

            if (filePath == null)
            {
                return RedirectToAction("PatientDashboard", "Patient");
            }

            return PhysicalFile(filePath, MimeTypes.GetMimeType(filePath), Path.GetFileName(filePath));
        }

        [HttpPost]
        public void addDocument(IFormFile file, int requestid)
        {


            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads, file.FileName);


            file.CopyTo(new FileStream(filePath, FileMode.Create));
            RequestWiseFile insertfile = new RequestWiseFile()
            {
                RequestId = requestid,
                FileName = filePath,
                CreatedDate = DateTime.Now

            };
            context.RequestWiseFiles.Add(insertfile);
            context.SaveChanges();
            RedirectToAction("ViewDocument", requestid);
        }

        public IActionResult ReviewAgreement()
        {
            return View();
        }
        [HttpGet]
        public IActionResult RequestForMe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RequestForMe(PatientRequest model)
        {
            string? UserEmail = Request.Cookies[CookieUserEmail];

            var dob = model.dob;
            int day = dob.Day;
            var mon = dob.ToString("MMMM");
            var year = dob.Year;
            var email = model.Email;

            var userid = context.Requests.Where(a => a.Email == UserEmail).FirstOrDefault();

            Request insertrequest = new Request()
            {
                RequestTypeId = 2,
                UserId = userid.UserId,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                PhoneNumber = model.Phone,
                Email = model.Email,
                Status = 1,
                CreatedDate = DateTime.Now,
                IsUrgentEmailSent = new BitArray(new bool[1] { false })

            };
            context.Requests.Add(insertrequest);

            RequestClient insertrequestclient = new RequestClient()
            {
                Request = insertrequest,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                PhoneNumber = model.Phone,
                Email = model.Email,
                IntDate = day,
                StrMonth = mon,
                IntYear = year,
                Street = model.Street,
                State = model.State,
                City = model.City,
                ZipCode = model.Zipcode

            };
            context.RequestClients.Add(insertrequestclient);

            if (model.File != null)
            {
                foreach(var item in model.File)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads/Patient");
                    var filePath = Path.Combine(uploads, item.FileName);
                    var file = item;

                    file.CopyTo(new FileStream(filePath, FileMode.Create));
                    RequestWiseFile insertfile = new RequestWiseFile()
                    {
                        Request = insertrequest,
                        FileName = filePath,
                        CreatedDate = DateTime.Now

                    };
                    context.RequestWiseFiles.Add(insertfile);
                }
             

            }
            context.RequestClients.Add(insertrequestclient);
            context.SaveChanges();

            return RedirectToAction("PatientDashboardPage", "Patient");

        }

        [HttpGet]
        public IActionResult RequestForSomeoneElse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RequestForSomeoneElse(RequestForSomeoneElse model)
        {
            var dob = model.dob;
            int day = dob.Day;
            var mon = dob.ToString("MMMM");
            var year = dob.Year;
            var email = model.Email;

            Request insertrequest = new Request()
            {
                RequestTypeId = 3,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                PhoneNumber = model.Phone,
                Email = model.Email,
                Status = 1,
                CreatedDate = DateTime.Now,
                IsUrgentEmailSent = new BitArray(new bool[1] { false })

            };
            context.Requests.Add(insertrequest);

            RequestClient insertrequestclient = new RequestClient()
            {
                Request = insertrequest,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                PhoneNumber = model.Phone,
                Email = model.Email,
                IntDate = day,
                StrMonth = mon,
                IntYear = year,
                Street = model.Street,
                State = model.State,
                City = model.City,
                ZipCode = model.Zipcode

            };
            context.RequestClients.Add(insertrequestclient);

            if (model.File != null)
            {

                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, model.File.FileName);
                var file = model.File;

                file.CopyTo(new FileStream(filePath, FileMode.Create));
                RequestWiseFile insertfile = new RequestWiseFile()
                {
                    Request = insertrequest,
                    FileName = filePath,
                    CreatedDate = DateTime.Now

                };
                context.RequestWiseFiles.Add(insertfile);

            }
            context.RequestClients.Add(insertrequestclient);
            context.SaveChanges();

            return View();
        }
    }
}
