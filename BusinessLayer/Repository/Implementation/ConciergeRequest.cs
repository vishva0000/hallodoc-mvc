using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;

using DataLayer.DTO;
using DataLayer.Models;
using System.Collections;
using BusinessLayer.Utility;


namespace BusinessLayer.Repository.Implementation
{
    public  class ConciergeRequest : IConciergeRequest
    {
        public HallodocContext db;
   
        IEmailSender _EmailSender;

        public ConciergeRequest(HallodocContext context, IEmailSender emailSender)
        {
            this.db = context;
         
            _EmailSender = emailSender;
        }

        public void ConciergeRequestData(ConciergeRequestModel model)
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
           db.Requests.Add(insertrequest);

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
            db.Concierges.Add(insertconcierge);
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
                IntYear = year,
                 Request = insertrequest,
            };
            db.RequestClients.Add(insertrequestclient);
           db.SaveChanges();
            _EmailSender.SendEmailAsync("tatva.dotnet.vishvarami@outlook.com", "Hello", "Please <a href=\"https://localhost:44301/Patient/CreatePatient\">login</a>");

        }
    }
}
