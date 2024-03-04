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
    public class BusinessRequest : IBusinessRequest
    {
        public HallodocContext db;
        public readonly IHostingEnvironment _environment;
        IEmailSender _EmailSender;
        public BusinessRequest(HallodocContext context, IHostingEnvironment environment, IEmailSender emailSender)
        {
            this.db = context;
            _environment = environment;
            _EmailSender = emailSender;
        }
        public void BusinessRequestData(BusinessRequestModel model)
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
            db.Requests.Add(insertrequest);

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
            db.RequestClients.Add(insertrequestclient);

            Business insertbusiness = new Business()
            {
                Name = model.B_Hotel,
                PhoneNumber = model.B_Phone,
                CreatedDate = DateTime.Now

            };
            db.Businesses.Add(insertbusiness);

            RequestBusiness insertrequestbusiness = new RequestBusiness()
            {
                Business = insertbusiness,
                Request = insertrequest


            };
            db.RequestBusinesses.Add(insertrequestbusiness);
            db.SaveChanges();
        }
    }
}
