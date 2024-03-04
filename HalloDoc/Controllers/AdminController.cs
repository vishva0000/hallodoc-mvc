﻿using HalloDoc.DTO;
using HalloDoc.DTO.AdminDTO;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HalloDoc.Controllers
{
    public class AdminController : Controller
    {
        public HallodocContext db;

        public AdminController(HallodocContext context)
        {
            this.db = context;


        }
        public IActionResult AdminDashboard()
        {
            AdminDashboard data = new AdminDashboard();
            data.New = db.Requests.Where(a => a.Status == 1).Count();
            data.Pending = db.Requests.Where(a => a.Status == 2).Count();
            data.Active = db.Requests.Where(a => a.Status == 5 || a.Status == 6).Count();
            data.Conclude = db.Requests.Where(a => a.Status == 8).Count();
            data.ToClose = db.Requests.Where(a => a.Status == 13 || a.Status == 3 || a.Status == 8).Count();
            data.UnPaid = db.Requests.Where(a => a.Status == 8).Count();

            return View(data);
           
        }
        public IActionResult test()
        {
            return View();
        }
        public IActionResult NewState(int reqStaus)
        {
            List<RequestTableData> data = RequestsTable(reqStaus);
            return PartialView("_NewTable", data);
        }
        public IActionResult filters(int status, int requesttype) 
        { 
            List<RequestTableData> data = FilterRequestsTable(status, requesttype);
            return PartialView("_NewTable", data);
        }
       
        public List<RequestTableData> RequestsTable(int status)
        {
            List<Request> r;
            List<RequestTableData> data = new();
            var phy = db.Physicians;
           
            if (status == 1)
            {
                r = db.Requests.Where(a => a.Status == 1).ToList();
              

                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 1;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
           else if(status == 2)
            {
                r = db.Requests.Where(a => a.Status == 2).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    if(item.PhysicianId != null)
                    {
                        var phyid = item.PhysicianId;
                        request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName +" "+ db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().LastName;

                    }
                    request.status = 2;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
           else if(status == 3)
            {
                r = db.Requests.Where(a => a.Status == 5 || a.Status == 6).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 3;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
           else if(status == 4)
            {
                r = db.Requests.Where(a => a.Status == 8).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 4;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
           else if(status == 5)
            {
                r = db.Requests.Where(a => a.Status == 13 || a.Status == 3 || a.Status==8).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 5;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
           else if(status == 6)
            {
                r = db.Requests.Where(a => a.Status == 8).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 6;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
            else
            {
                r = db.Requests.Where(a => a.Status == status).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = status;
                    request.RequestId= item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
            

            return data;
        }

        public List<RequestTableData> FilterRequestsTable(int status, int requesttype)
        {
            List<Request> r;
            List<RequestTableData> data = new();

            if (status == 1)
            {
                r = db.Requests.Where(a => a.Status == 1 && a.RequestTypeId == requesttype).ToList();


                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 1;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
            else if (status == 2)
            {
                r = db.Requests.Where(a => a.Status == 2 && a.RequestTypeId == requesttype).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 2;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
            else if (status == 3)
            {
                r = db.Requests.Where(a => a.Status == 5 || a.Status == 6 && a.RequestTypeId == requesttype).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 3;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
            else if (status == 4 )
            {
                r = db.Requests.Where(a => a.Status == 8 && a.RequestTypeId == requesttype).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 4;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
            else if (status == 5)
            {
                r = db.Requests.Where(a => a.Status == 13 || a.Status == 3 || a.Status == 8 && a.RequestTypeId == requesttype).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 5;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
            else if (status == 6)
            {
                r = db.Requests.Where(a => a.Status == 8 && a.RequestTypeId == requesttype).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = 6;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }
            else
            {
                r = db.Requests.Where(a => a.Status == status && a.RequestTypeId == requesttype).ToList();
                var details = db.Requests;

                foreach (var item in r)
                {

                    RequestTableData request = new RequestTableData();
                    request.status = status;
                    request.RequestId = item.RequestId;
                    request.RequestTypeId = item.RequestTypeId;
                    request.Requestor = item.FirstName + " " + item.LastName;
                    request.Name = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().FirstName + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().LastName;
                    request.Address = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Location + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().Street + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().City + " " + db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().State;
                    request.Phone = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhoneNumber;
                    request.RequestedDate = item.CreatedDate;
                    //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                    //var phyid = item.PhysicianId;
                    //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                    //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                    data.Add(request);
                }
            }


            return data;
        }
        
        public void CancelCase(cancelcase model)
        {
            RequestStatusLog data = new RequestStatusLog();
            data.RequestId = model.req_id;
            data.Notes = model.cancelNote;
            data.Status = 3;
            data.CreatedDate = DateTime.Now;

            var requestTuple = db.Requests.Where(a => a.RequestId == model.req_id).FirstOrDefault();
            requestTuple.Status = 3;

            db.Requests.Update(requestTuple);
            db.RequestStatusLogs.Add(data);

            db.SaveChanges();
           
        }
        
        public void AssignCase(int assign_req_id, string phy_region, string phy_id, string assignNote)
        {
            var phyid = int.Parse(phy_id);
            RequestStatusLog data = new RequestStatusLog();
            data.RequestId = assign_req_id;
            data.Notes = assignNote;
            data.Status = 2;
            data.CreatedDate = DateTime.Now;
            data.PhysicianId = phyid;


            var requestTuple = db.Requests.Where(a => a.RequestId == assign_req_id).FirstOrDefault();
            requestTuple.Status = 2;
            requestTuple.PhysicianId = phyid;
            db.Requests.Update(requestTuple);
            db.RequestStatusLogs.Add(data);

            db.SaveChanges();
           
        }
        public void BlockCase( int block_req_id, string blocknote)
        {
            
            RequestStatusLog data = new RequestStatusLog();
            data.RequestId = block_req_id;
            data.Notes = blocknote;
            data.Status = 8;
            data.CreatedDate = DateTime.Now;
        


            var requestTuple = db.Requests.Where(a => a.RequestId == block_req_id).FirstOrDefault();
            requestTuple.Status = 8;

            
            BlockRequest blockrequest = new BlockRequest();
            blockrequest.RequestId = block_req_id.ToString();
            blockrequest.PhoneNumber = db.RequestClients.Where(a => a.RequestId == block_req_id).FirstOrDefault().PhoneNumber;
            blockrequest.Email = db.RequestClients.Where(a => a.RequestId == block_req_id).FirstOrDefault().Email;
            blockrequest.Reason = blocknote;
            blockrequest.CreatedDate = DateTime.Now;

            db.Requests.Update(requestTuple);
            db.RequestStatusLogs.Add(data);
            db.BlockRequests.Add(blockrequest);

            db.SaveChanges();
           
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
        
        public IActionResult FetchPhysician(int selectregionid)
        {
            var physicians = db.Physicians.Where(r=>r.RegionId == selectregionid).Select(r => new
            {
                physicianid = r.PhysicianId,
                name = r.FirstName + " " + r.LastName
            }).ToList();

            return Ok(physicians);
        }
        
        public IActionResult ViewUploads()
        {
            return View();
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
    }
}
