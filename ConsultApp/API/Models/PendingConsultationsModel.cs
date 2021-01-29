using System;
using SQLite;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using Prism.Commands;

namespace ConsultApp.API.Models
{
    public class PendingConsultationsModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public string Name { get; set; }
        [Indexed]
        public string Specialization { get; set; }
        [Indexed]
        public string AvailableDay { get; set; }
        [Indexed]
        public string Hospital { get; set; }
        [Indexed]
        public double Distance { get; set; }
        [Indexed]
        public string SelectedDay { get; set; }
        [Indexed]
        public bool IsAvailable => AvailableDay.Equals(DateTime.Now.DayOfWeek.ToString()) ? true : false;
        [Indexed]
        public string CreationDate { get; set; }
        [Indexed]
        public double Latitude { get; set; }
        [Indexed]
        public double Longitude { get; set; }
        [Indexed]
        public string Label { get; set; }
        [Indexed]
        public string Address { get; set; }
        [Ignore]
        public ObservableCollection<Pins> Pins { get; set; }
        [Ignore]
        public DelegateCommand<PendingConsultationsModel> RemoveConsultation { get; set; }
    }
    public class Pins
    {
        [Ignore]
        public string Label { get; set; }
        [Ignore]
        public string Address { get; set; }
        [Ignore]
        public double Latitude { get; set; }
        [Ignore]
        public double Longitude { get; set; }
        [Ignore]
        public Position Position { get; set; }
    }
}