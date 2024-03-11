using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;

namespace BusinessLayer.Repository.Implementation
{
    public class CreateRequest : ICreateRequest
    {
        public HallodocContext db;
        public CreateRequest(HallodocContext context) 
        {
            this.db = context;
        }

        public void AdminRequest(CreateRequestData model)
        {
           
            var dob = model.Dob;
            int day = dob.Day;
            var mon = dob.ToString("MMMM");
            var year = dob.Year;

            Request request = new Request()
            {
                RequestTypeId = 2,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Status = 1,
                CreatedDate = DateTime.Now,
                IsUrgentEmailSent = new BitArray(new bool[1] { false })
            };
           
            db.Requests.Add(request);

            RequestClient insertrequestclient = new RequestClient()
            {
                Request = request,
                FirstName = model.FirstName,
                LastName = model.LastName,
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

            RequestNote requestNote = new RequestNote()
            {
                Request = request,
                AdminNotes = model.Adminnotes,
                CreatedBy = "admin",
                CreatedDate = DateTime.Now
            };

            db.RequestNotes.Add(requestNote);

            db.SaveChanges();
        }
    }
}
