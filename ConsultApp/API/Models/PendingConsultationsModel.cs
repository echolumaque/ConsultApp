using System;
using SQLite;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;

namespace ConsultApp.API.Models
{
    public class PendingConsultationsModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string AvailableDay { get; set; }
        public string Hospital { get; set; }
        public double Distance { get; set; }
        [Unique]
        public string SelectedDay { get; set; }
        public bool IsAvailable => AvailableDay.Equals(DateTime.Now.DayOfWeek.ToString()) ? true : false;
        public string CreationDate { get; set; }
        [Ignore]
        public ObservableCollection<Pins> Pins { get; set; }
    }
    public class Pins
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Label { get; set; }
        public string Address { get; set; }
        [Ignore]
        public Position Position => new Position(Latitude, Longitude);
    }
}