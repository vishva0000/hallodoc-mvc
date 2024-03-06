using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;

namespace BusinessLayer.Repository.Implementation
{
    public class ViewUploads : IViewUploads
    {
        public HallodocContext db;
        public ViewUploads(HallodocContext context) 
        {
            this.db = context;
        }
        public ViewUploadsModel Uploadedfilesdata(int viewid)
        {
            ViewUploadsModel details = new ViewUploadsModel();

            List<FileData> files = new();

            details.name = db.Requests.Where(a => a.RequestId == viewid).FirstOrDefault().FirstName + " " + db.Requests.Where(a => a.RequestId == viewid).FirstOrDefault().LastName;
            details.reqid = viewid;
            var data = db.RequestWiseFiles.Where(a => a.RequestId == viewid && a.IsDeleted==null);
            foreach (var item in data)
            {
                FileData fileup = new FileData();

                //fileup.file = item.FileName.Split('\\').Last();
                fileup.file = item.FileName;
                fileup.createdate = item.CreatedDate;
                fileup.docid = item.RequestWiseFileId;
                files.Add(fileup);
            }
            details.filedata = files;

            return details;
        }
    }
}
