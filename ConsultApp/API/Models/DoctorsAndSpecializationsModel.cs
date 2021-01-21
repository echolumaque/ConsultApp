using System;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;
using Prism.Commands;
using ConsultApp.API.Models;

namespace ConsultApp.API.Models
{
    public class DoctorsAndSpecializationsModel
    {
        public string Doctor { get; set; }
        public string Specialization { get; set; }
        public string DaysAvailable { get; set; }
        public string Hospital { get; set; }
        public bool IsAvailable => DaysAvailable.Contains(DateTime.Now.DayOfWeek.ToString()) ? true : false;
        public double Distance { get; set; }
        public ObservableCollection<MapPinModel> MapPins { get; set; }
        public DelegateCommand DoctorsSchedule { get; set; }
    }
    public class MapPinModel
    {
        public Position Location { get; set; }
        public string Label { get; set; }
        public string Address { get; set; }
    }
}
