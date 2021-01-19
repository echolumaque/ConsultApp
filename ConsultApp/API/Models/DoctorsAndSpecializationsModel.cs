using System;

namespace ConsultApp.API.Models
{
    public class DoctorsAndSpecializationsModel
    {
        public string Doctor { get; set; }
        public string Specialization { get; set; }
        public string DaysAvailable { get; set; }
        public string Hospital { get; set; }
        public bool IsAvailable => DaysAvailable.Contains(DateTime.Now.DayOfWeek.ToString()) ? true : false;
    }
}
