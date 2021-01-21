using System;
using Xamarin.Forms;
using Prism.Events;
using ConsultApp.Helpers.Events;
using System.Collections.ObjectModel;

namespace ConsultApp.Views
{
    public partial class DoctorsAvailability : ContentPage
    {
        private string availableDay;
        public DoctorsAvailability(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            eventAggregator.GetEvent<PassAvailableDay>().Subscribe(AvailableDay);
        }

        private void SfCalendar_OnMonthCellLoaded(object sender, Syncfusion.SfCalendar.XForms.MonthCellLoadedEventArgs e)
        {
            var blackoutDays = new ObservableCollection<DateTime>();
            var dayOfWeek = e.Date.DayOfWeek;

            if (!dayOfWeek.Equals(DateTime.Parse(availableDay).DayOfWeek))
            {
                blackoutDays.Add(e.Date);
                calendar.BlackoutDates = blackoutDays;
            }
        }

        private void AvailableDay(string day) => availableDay = day;
    }
}
