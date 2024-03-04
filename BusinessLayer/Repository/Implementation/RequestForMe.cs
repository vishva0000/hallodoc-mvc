using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using DataLayer.DTO;
using DataLayer.Models;
using System.Collections;
using BusinessLayer.Utility;

namespace BusinessLayer.Repository.Implementation
{
    public   class RequestForMe : IRequestForMe
    {
        public HallodocContext db;
        public readonly IHostingEnvironment _environment;
        IEmailSender _EmailSender;
        public RequestForMe(HallodocContext context, IHostingEnvironment environment, IEmailSender emailSender)
        {
            this.db = context;
            _environment = environment;
            _EmailSender = emailSender;
        }

        public void Requestforme(PatientRequestModel model, string UserEmail)
        {
           

            var dob = model.dob;
            int day = dob.Day;
            var mon = dob.ToString("MMMM");
            var year = dob.Year;
            var email = model.Email;

            var userid = db.Requests.Where(a => a.Email == UserEmail).FirstOrDefault();

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
            db.Requests.Add(insertrequest);

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
            db.RequestClients.Add(insertrequestclient);

            if (model.File != null)
            {
                foreach (var item in model.File)
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
                    db.RequestWiseFiles.Add(insertfile);
                }


            }
            db.RequestClients.Add(insertrequestclient);
            db.SaveChanges();
        }
    }
}
