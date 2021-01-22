using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using ConsultApp.ViewModels;

namespace ConsultApp.Views
{
    public partial class DoctorsAvailability : ContentPage
    {
        public DoctorsAvailability()
        {
            InitializeComponent();
            ObservableCollection<string> customDayLabels = new ObservableCollection<string>()
            {
                "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
            };
            calendar.CustomDayLabels = customDayLabels;
        }

        private void SfCalendar_OnMonthCellLoaded(object sender, Syncfusion.SfCalendar.XForms.MonthCellLoadedEventArgs e)
        {
            var vm = BindingContext as DoctorsAvailabilityViewModel;

            var blackoutDays = new ObservableCollection<DateTime>();
            var dayOfWeek = e.Date.DayOfWeek;

            if (!dayOfWeek.Equals(vm.DaysAvailable))
                blackoutDays.Add(e.Date);

            calendar.BlackoutDates = blackoutDays;
        }

    }
}
