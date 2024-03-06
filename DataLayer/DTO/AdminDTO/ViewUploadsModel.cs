using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.AdminDTO
{
    public class ViewUploadsModel
    {
       public List<FileData> filedata;
        public string name {  get; set; }
        public int reqid { get; set; }
       
    }
    public class FileData
    {
        public string file { get; set; }
        public int docid { get; set; }
        public DateTime createdate { get; set; }
    }
}
