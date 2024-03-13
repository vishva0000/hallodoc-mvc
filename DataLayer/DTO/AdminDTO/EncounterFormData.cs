using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO.AdminDTO
{
    public class EncounterFormData
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Location { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateOfService {  get; set; }
        public  string Phone { get; set; }
        public string Email { get; set; }
        public string? HistoryOfIllness { get; set; }
        public string? MedicalHistroy { get; set; }
        public string? Medications {  get; set; }
        [Required]
        public string Allergies { get; set; }
        public string? Temp { get; set; }
        public string? HR { get; set; }
        public string? RR { get; set; }
        public string? BloodPressureSystolic { get; set; }
        public string? BloodPressureDiastolic { get; set; }
        public string? O2 { get; set; }
        public string? Pain { get; set; }
        public string? Heent { get; set; }
        public string? CV { get; set;}
        public string? Chest { get; set;}
        public string? ABD { get; set;}
        public string? Extr { get; set;}
        public string? Skin { get; set;}
        public string? Neuro { get; set;}
        public string? Other { get; set;}
        [Required]
        public string Diagnosis { get; set;}
        [Required]
        public string TreatmentPlan { get; set;}
        [Required]
        public string MedicationDispensed { get; set; }
        [Required]
        public string Procedures { get; set; }
        [Required]
        public string FollowUp { get; set;}
        


    }
}
