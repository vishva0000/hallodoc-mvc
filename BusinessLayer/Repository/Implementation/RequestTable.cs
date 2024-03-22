﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using BusinessLayer.Repository.IRepository;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NPOI.SS.Formula.Functions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLayer.Repository.Implementation
{
    public class RequestTable : IRequestTable
    {
        public HallodocContext db;
        public RequestTable(HallodocContext context)
        {
            this.db = context;
        }

        public List<RequestTableData> requestTableData(int status, int requesttype, string search)
        {
            List<Request> r;
            List<RequestTableData> data = new();
            var phy = db.Physicians;
            if (requesttype == 0)
            {
                if (status == 1)
                {
                    //r = db.Requests.Where(a => a.Status == 1).ToList();
                    var my = db.Requests.Where(a => a.Status == 1).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                    foreach (var item in mydata)
                    {
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();

                        RequestTableData request = new RequestTableData();

                        request.status = 1;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;

                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;

                        //string month = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().StrMonth;
                        //var day = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().IntDate;
                        //var year = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault().IntYear;
                        //var formattedDate = $"{month} {day}, {year}";
                        //request.dateofbirth = formattedDate;

                        data.Add(request);
                    }
                }
                else if (status == 2)
                {
                    //r = db.Requests.Where(a => a.Status == 2).ToList();
                    //var details = db.Requests;
                    var my = db.Requests.Where(a => a.Status == 2).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        if (item.PhysicianId != null)
                        {
                            var phyid = item.PhysicianId;
                            request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName + " " + db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().LastName;

                        }
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 2;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        //var lastassphy = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId && a.Status == 2).OrderByDescending(b => b.CreatedDate).FirstOrDefault().PhysicianId;

                        //request.Notes = "Assigned to " + db.Physicians.Where(a => a.PhysicianId == lastassphy).FirstOrDefault().FirstName + " " + db.Physicians.Where(a => a.PhysicianId == lastassphy).FirstOrDefault().LastName;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;


                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
                else if (status == 3)
                {
                    //r = db.Requests.Where(a => a.Status == 4 || a.Status == 5).ToList();
                    //var details = db.Requests;

                    var my = db.Requests.Where(a => a.Status == 4 || a.Status == 5).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));


                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 3;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;

                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
                else if (status == 4)
                {
                    //r = db.Requests.Where(a => a.Status == 6).ToList();
                    //var details = db.Requests;
                    var my = db.Requests.Where(a => a.Status == 6).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));


                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 4;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;

                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
                else if (status == 5)
                {
                    //r = db.Requests.Where(a => a.Status == 3 || a.Status == 7 || a.Status == 8).ToList();
                    var my = db.Requests.Where(a => a.Status == 3 || a.Status == 7 || a.Status == 8).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                    foreach (var item in mydata)
                    {
                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (item.PhysicianId != null)
                        {
                            var phyid = item.PhysicianId;
                            request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName + " " + db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().LastName;

                        }
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 5;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.Email = patient.Email;

                        data.Add(request);
                    }

                }
                else if (status == 6)
                {
                    //r = db.Requests.Where(a => a.Status == 9).ToList();
                    //var details = db.Requests;
                    var my = db.Requests.Where(a => a.Status == 9).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));


                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (item.PhysicianId != null)
                        {
                            var phyid = item.PhysicianId;
                            request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName + " " + db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().LastName;

                        }
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 6;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;

                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
                else
                {
                    var my = db.Requests.Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                    //r = db.Requests.ToList();
                    //var details = db.Requests;

                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = item.Status;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;

                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
            }
            else
            {

                if (status == 1)
                {
                    //r = db.Requests.Where(a => a.Status == 1 && a.RequestTypeId == requesttype).ToList();
                    //var details = db.Requests;

                    var my = db.Requests.Where(a => a.Status == 1 && a.RequestTypeId == requesttype).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));


                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 1;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + " " + item.LastName;
                        request.Name = patient.FirstName + " " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;

                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
                else if (status == 2)
                {
                    //r = db.Requests.Where(a => a.Status == 2 && a.RequestTypeId == requesttype).ToList();
                    //var details = db.Requests;
                    var my = db.Requests.Where(a => a.Status == 2 && a.RequestTypeId == requesttype).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 2;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;

                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
                else if (status == 3)
                {
                    //r = db.Requests.Where(a => (a.Status == 4 || a.Status == 5) && a.RequestTypeId == requesttype).ToList();
                    //var details = db.Requests;
                    var my = db.Requests.Where(a => (a.Status == 4 || a.Status == 5) && a.RequestTypeId == requesttype).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));


                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 3;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;

                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
                else if (status == 4)
                {
                    //r = db.Requests.Where(a => a.Status == 6 && a.RequestTypeId == requesttype).ToList();
                    //var details = db.Requests;
                    var my = db.Requests.Where(a => a.Status == 6 && a.RequestTypeId == requesttype).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 4;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;

                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
                else if (status == 5)
                {


                    //r = db.Requests.Where(a => (a.Status==3 || a.Status == 7 || a.Status == 8) && a.RequestTypeId == requesttype).ToList();
                    //var details = db.Requests;
                    var my = db.Requests.Where(a => (a.Status == 3 || a.Status == 7 || a.Status == 8) && a.RequestTypeId == requesttype).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 5;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;


                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
                else if (status == 6)
                {
                    //r = db.Requests.Where(a => a.Status == 9 && a.RequestTypeId == requesttype).ToList();
                    //var details = db.Requests;
                    var my = db.Requests.Where(a => a.Status == 9 && a.RequestTypeId == requesttype).Include(b => b.RequestClients).ToList();
                    var mydata = my.Where(a =>
                        search.IsNullOrEmpty() ||
                        a.RequestClients.FirstOrDefault()!.FirstName!.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                        a.RequestClients.FirstOrDefault()!.LastName!.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                    foreach (var item in mydata)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = 6;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;

                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }
                else
                {
                    r = db.Requests.Where(a => a.Status == status && a.RequestTypeId == requesttype).Include(b => b.RequestClients).ToList();
                    var details = db.Requests;

                    foreach (var item in r)
                    {

                        RequestTableData request = new RequestTableData();
                        var patient = db.RequestClients.Where(a => a.RequestId == item.RequestId).FirstOrDefault();
                        if (patient.IntYear != null && patient.IntDate != null && patient.StrMonth != null)
                        {
                            int month = DateTime.ParseExact(patient.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                            request.Dob = new DateTime((int)patient.IntYear, month, (int)patient.IntDate);
                        }
                        request.status = status;
                        request.RequestId = item.RequestId;
                        request.RequestTypeId = item.RequestTypeId;
                        request.Requestor = item.FirstName + ", " + item.LastName;
                        request.Name = patient.FirstName + ", " + patient.LastName;
                        request.Address = patient.Location + " " + patient.Street + " " + patient.City + " " + patient.State;
                        request.PhoneP = patient.PhoneNumber;
                        request.PhoneO = item.PhoneNumber;
                        request.RequestedDate = item.CreatedDate;
                        request.Email = patient.Email;

                        //request.Notes = db.RequestNotes.Where(a => a.RequestId == item.RequestId).FirstOrDefault().PhysicianNotes;
                        //var phyid = item.PhysicianId;
                        //request.PhysicianName = db.Physicians.Where(a => a.PhysicianId == phyid).FirstOrDefault().FirstName;
                        //request.DateOfService = db.RequestStatusLogs.Where(a => a.RequestId == item.RequestId).FirstOrDefault().CreatedDate;
                        data.Add(request);
                    }
                }

            }



            return data;
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

        public void BlockCase(int block_req_id, string blocknote)
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

        public void TransferCase(int transfer_req_id, string phy_region, string phy_id, string transferNote)
        {
            var phyid = int.Parse(phy_id);
            RequestStatusLog data = new RequestStatusLog();
            data.RequestId = transfer_req_id;
            data.Notes = transferNote;
            data.Status = 2;
            data.CreatedDate = DateTime.Now;
            data.PhysicianId = phyid;


            var requestTuple = db.Requests.Where(a => a.RequestId == transfer_req_id).FirstOrDefault();
            requestTuple.Status = 2;
            requestTuple.PhysicianId = phyid;
            db.Requests.Update(requestTuple);
            db.RequestStatusLogs.Add(data);

            db.SaveChanges();
        }

        public void ClearCase(int clear_req_id)
        {
            var data = db.Requests.Where(a => a.RequestId == clear_req_id).FirstOrDefault();

            data.Status = 10;

            RequestStatusLog requstatuslog = new RequestStatusLog();

            requstatuslog.Status = 10;
            requstatuslog.RequestId = clear_req_id;
            requstatuslog.PhysicianId = data.PhysicianId;
            requstatuslog.CreatedDate = DateTime.Now;


            db.Requests.Update(data);
            db.RequestStatusLogs.Add(requstatuslog);
            db.SaveChanges();
        }

        public void SendAgreementMail(int arg_req_id, string argPhone, string argEmail)
        {

        }

        public PagedList<RequestTableData> GetData(int reqStaus, int requesttype, string searchin, int page,out int Count)
        {
            var query = requestTableData(reqStaus,requesttype,searchin);
            var count = query.Count();
            Count = count;
            var items = query.Skip((page) * 5).Take(5).ToList();
            return PagedList<RequestTableData>.Create(query, page);



        }
    }
}
