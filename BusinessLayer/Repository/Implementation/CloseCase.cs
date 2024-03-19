using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;
using NPOI.POIFS.Properties;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BusinessLayer.Repository.Implementation
{
    public class CloseCase : ICloseCase
    {
        public HallodocContext db;
        public CloseCase(HallodocContext context) 
        {
            this.db = context;
        }
        public ViewUploadsModel Closecasefiles(int reqid)
        {
            ViewUploadsModel details = new ViewUploadsModel();

            List<FileData> files = new();
            var reqc = db.RequestClients.Where(a => a.RequestId == reqid).FirstOrDefault();
            details.Firstname = reqc.FirstName;
            details.LastName = reqc.LastName;
            details.name = reqc.FirstName +" " + reqc.LastName;
            if (reqc.IntYear != null && reqc.IntDate != null && reqc.StrMonth != null)
            {
                int month = DateTime.ParseExact(reqc.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                details.Dob = new DateTime((int)reqc.IntYear, month, (int)reqc.IntDate);
            }
            details.email = reqc.Email;
            details.phone = reqc.PhoneNumber;
            details.reqid = reqid;
            var data = db.RequestWiseFiles.Where(a => a.RequestId == reqid && a.IsDeleted == null);
            foreach (var item in data)
            {
                FileData fileup = new FileData();

                fileup.file = item.FileName.Split('\\').Last();
                //fileup.file = item.FileName;
                fileup.createdate = item.CreatedDate;
                fileup.docid = item.RequestWiseFileId;
                files.Add(fileup);
            }
            details.filedata = files;

            return details;
        }

        public void Closingcase(int reqid)
        {
            RequestStatusLog data = new RequestStatusLog();
            data.RequestId = reqid;
            data.Status = 9;
            data.CreatedDate = DateTime.Now;

            RequestClosed req = new RequestClosed();
            req.RequestId = reqid;
            req.RequestStatusLog = data;

            var requestTuple = db.Requests.Where(a => a.RequestId == reqid).FirstOrDefault();            
            requestTuple.Status = 9;
            db.Requests.Update(requestTuple);
            db.RequestStatusLogs.Add(data);
            db.RequestCloseds.Add(req);

            db.SaveChanges();
        }
    }
}


