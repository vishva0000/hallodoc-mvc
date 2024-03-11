
using DataLayer.DTO;
using DataLayer.Models;
using BusinessLayer.Repository.Interface;
using System.Collections;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BusinessLayer.Repository.Implementation
{
    public class PatientRequest : IPatientRequest
    {
        public HallodocContext db;
        public readonly IHostingEnvironment _environment;
        public PatientRequest(HallodocContext context, IHostingEnvironment environment)
        {
            this.db = context;
            _environment = environment;
        }

        public void PatientRequestData(PatientRequestModel model)
        {
            var dob = model.dob;
            int day = dob.Day;
            var mon = dob.ToString("MMMM");
            var year = dob.Year;
            var email = model.Email;


            User insertuser = new User();
            AspNetUser insertAspNetUser = new AspNetUser();
            var user = db.Users.Where(a => a.Email == email).FirstOrDefault();
            if (user == null)
            {
                Guid obj = Guid.NewGuid();
                insertAspNetUser.Id = obj.ToString();
                insertAspNetUser.Email = email;
                insertAspNetUser.PasswordHash = model.Password;
                insertAspNetUser.UserName = model.Firstname;
                insertAspNetUser.CreatedDate = DateTime.Now;

                db.AspNetUsers.Add(insertAspNetUser);

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

                db.Users.Add(insertuser);
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
                Location = model.Room,
                ZipCode = model.Zipcode

            };
            db.RequestClients.Add(insertrequestclient);

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
         
            db.SaveChanges();
        }
    }
}
