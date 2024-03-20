namespace DataLayer.DTO.AdminDTO
{
    public class ViewNotesData
    {
       
        public int RequestId { get; set; }
        public string transfer { get; set; }
        public List<string> transfernote { get; set; }
        public string adminNotes { get; set; }
        public string physicianNotes { get; set; }

        public string Additional { get; set; }
        
    }
}
