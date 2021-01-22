using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ConsultApp.API.Models
{
    public class PendingConsultationsModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public DayOfWeek Availability { get; set; }
        public string Hospital { get; set; }
        public double Distance { get; set; }
        public object Image { get; set; }
    }
}
