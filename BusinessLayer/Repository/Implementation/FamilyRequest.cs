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
    public class FamilyRequest : IFamilyRequest
    {
        public HallodocContext db;
        public readonly IHostingEnvironment _environment;
        IEmailSender _EmailSender;


        public FamilyRequest(HallodocContext context, IHostingEnvironment environment, IEmailSender emailSender)
        {
            this.db = context;
            _environment = environment;
            _EmailSender = emailSender;
        }

        public void FamilyRequestData(FamilyRequestModel model)
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
            db.Requests.Add(insertrequest);
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
            db.RequestClients.Add(insertrequestclient);
            if (model.P_File != null)
            {
                //var uploads = Path.Combine(_environment.WebRootPath, "uploads/Family");
                //var filePath = Path.Combine(uploads, model.P_File.FileName);
                //var file = model.P_File;

                //file.CopyTo(new FileStream(filePath, FileMode.Create));
                //RequestWiseFile insertfile = new RequestWiseFile()
                //{
                //    Request = insertrequest,
                //    FileName = filePath,
                //    CreatedDate = DateTime.Now

                //};
                //context.RequestWiseFiles.Add(insertfile);
                foreach (var item in model.P_File)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads/Family");
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
            //_EmailSender.SendEmailAsync("vishva.rami@etatvasoft.com", "CreateAccount", "Please <a href=\"https://localhost:44301/Patient/CreatePatient\">login</a>");


        }
    }
}
