using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository.Interface;
using DataLayer.DTO.AdminDTO;
using DataLayer.Models;

namespace BusinessLayer.Repository.Implementation
{
    public class Encounterform: IEncounterform
    {
        public HallodocContext db;
        public Encounterform(HallodocContext context) 
        {
            this.db = context;
        }  

        public EncounterFormData encounterformdata(int encreqid)
        {
            var form = db.EncounterForms.Where(a => a.RequestId == encreqid).FirstOrDefault();
            if (form != null)
            {
                EncounterFormData model = new EncounterFormData();
                var request = db.RequestClients.Where(a => a.RequestId == encreqid).FirstOrDefault();
                model.Firstname = request.FirstName;
                model.Lastname = request.LastName;
                model.Location = request.Street + " " + request.City + " " + request.State;
                model.Email = request.Email;
                model.Phone = request.PhoneNumber;
                if (request.IntYear != null && request.IntDate != null && request.StrMonth != null)
                {
                    int month = DateTime.ParseExact(request.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                    model.DOB = new DateTime((int)request.IntYear, month, (int)request.IntDate);
                }
                model.HistoryOfIllness = form.HistoryOfPresentIllnessOrInjury;
                model.MedicalHistroy = form.MedicalHistory;
                model.Medications = form.Medications;
                model.Allergies = form.Allergies;
                model.Temp = form.Temp;
                model.HR = form.Hr;
                model.RR = form.Rr;
                model.BloodPressureDiastolic = form.BloodPressureDiastolic;
                model.BloodPressureSystolic = form.BloodPressureSystolic;
                model.O2 = form.O2;
                model.Pain = form.Pain;
                model.Heent = form.Heent;
                model.CV = form.Cv;
                model.Chest = form.Chest;
                model.ABD = form.Abd;
                model.Extr = form.Extremeties;
                model.Skin = form.Skin;
                model.Neuro = form.Neuro;
                model.Other = form.Other;
                model.Diagnosis = form.Diagnosis;
                model.TreatmentPlan = form.TreatmentPlan;
                model.MedicationDispensed = form.MedicationsDispensed;
                model.Procedures = form.Procedures;
                model.FollowUp = form.FollowUp;


                return model;

            }
            else
            {
                EncounterFormData model = new EncounterFormData();
                var request = db.RequestClients.Where(a => a.RequestId == encreqid).FirstOrDefault();
                model.Firstname = request.FirstName;
                model.Lastname = request.LastName;
                model.Location = request.Street + " " + request.City + " " + request.State;
                model.Email = request.Email;
                model.Phone = request.PhoneNumber;
                if (request.IntYear != null && request.IntDate != null && request.StrMonth != null)
                {
                    int month = DateTime.ParseExact(request.StrMonth, "MMMM", CultureInfo.CurrentCulture).Month;
                    model.DOB = new DateTime((int)request.IntYear, month, (int)request.IntDate);
                }
                return model;
            }
          
        }

        public void encounterSaveChanges(EncounterFormData model, int id)
        {
          
            var form = db.EncounterForms.Where(a => a.RequestId == id).FirstOrDefault();
            if (form == null)
            {
                EncounterForm encounterForm = new EncounterForm()
                {
                    RequestId = id,
                    HistoryOfPresentIllnessOrInjury = model.HistoryOfIllness,
                    MedicalHistory = model.MedicalHistroy,
                    Medications = model.Medications,
                    Allergies = model.Allergies,
                    Temp = model.Temp,
                    Hr = model.HR,
                    Rr = model.RR,
                    BloodPressureDiastolic = model.BloodPressureDiastolic,
                    BloodPressureSystolic = model.BloodPressureSystolic,
                    O2 = model.O2,
                    Pain = model.Pain,
                    Heent = model.Heent,
                    Cv = model.CV,
                    Chest = model.Chest,
                    Abd = model.ABD,
                    Extremeties = model.Extr,
                    Skin = model.Skin,
                    Neuro = model.Neuro,
                    Other = model.Other,
                    Diagnosis = model.Diagnosis,
                    TreatmentPlan = model.TreatmentPlan,
                    MedicationsDispensed = model.MedicationDispensed,
                    Procedures = model.Procedures,
                    FollowUp = model.FollowUp

                };
                db.EncounterForms.Add(encounterForm);
            }
            else
            {
                form.RequestId = id;
                form.HistoryOfPresentIllnessOrInjury = model.HistoryOfIllness;
                form.MedicalHistory = model.MedicalHistroy;
                form.Medications = model.Medications;
                form.Allergies = model.Allergies;
                form.Temp = model.Temp;
                form.Hr = model.HR;
                form.Rr = model.RR;
                form.BloodPressureDiastolic = model.BloodPressureDiastolic;
                form.BloodPressureSystolic = model.BloodPressureSystolic;
                form.O2 = model.O2;
                form.Pain = model.Pain;
                form.Heent = model.Heent;
                form.Cv = model.CV;
                form.Chest = model.Chest;
                form.Abd = model.ABD;
                form.Extremeties = model.Extr;
                form.Skin = model.Skin;
                form.Neuro = model.Neuro;
                form.Other = model.Other;
                form.Diagnosis = model.Diagnosis;
                form.TreatmentPlan = model.TreatmentPlan;
                form.MedicationsDispensed = model.MedicationDispensed;
                form.Procedures = model.Procedures;
                form.FollowUp = model.FollowUp;

                db.EncounterForms.Update(form);

            }

            db.SaveChanges();

        }
    }
}
