﻿
using DataLayer.Models;
using DataLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using BusinessLayer.Utility;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System.Globalization;
using BusinessLayer.Repository.Interface;

namespace HalloDoc.Controllers
{
    public class PatientController : Controller
    {
        public HallodocContext context;
        public readonly IHostingEnvironment _environment;
        public readonly IPatientRequest patientRequestService;
        public readonly IFamilyRequest familyRequestService;
        public readonly IBusinessRequest businessRequestService;
        public readonly IConciergeRequest conciergeRequestService;
        public readonly IRequestForMe requestForMeService;
        const string CookieUserEmail = "UserId";
        const string emailforreset = "EmailId";

        IEmailSender _EmailSender;



        public PatientController(HallodocContext context, IHostingEnvironment environment, IEmailSender emailSender, IPatientRequest patientRequest, IFamilyRequest familyRequest, IBusinessRequest businessRequest, IConciergeRequest conciergeRequest, IRequestForMe requestForMe)
        {
            this.context = context;
            this.patientRequestService = patientRequest;
            this.familyRequestService = familyRequest;
            _environment = environment;
            _EmailSender = emailSender;
            this.businessRequestService = businessRequest;
            this.conciergeRequestService = conciergeRequest;
            this.requestForMeService = requestForMe;
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
        public IActionResult PatientRequest(PatientRequestModel model)
        {
            patientRequestService.PatientRequestData(model);
           
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
        public IActionResult FamilyRequest(FamilyRequestModel model)
        {
            familyRequestService.FamilyRequestData(model);
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult BusinessRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BusinessRequest(BusinessRequestModel model)
        {
            businessRequestService.BusinessRequestData(model);  
            return RedirectToAction("Index", "Home");
            
        }
        [HttpGet]
        public IActionResult ConciergeRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ConciergeRequest(ConciergeRequestModel model)
        {
            conciergeRequestService.ConciergeRequestData(model);
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
        public IActionResult RequestForMe(PatientRequestModel model)
        {
            string? UserEmail = Request.Cookies[CookieUserEmail];
            requestForMeService.Requestforme(model, UserEmail);
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
