using Xamarin.Forms;
using System.Collections.Generic;
using ConsultApp.API.Models;
using ConsultApp.ViewModels;
using System;

namespace ConsultApp.Views
{
    public partial class DoctorsSchedule : ContentPage
    {
        public DoctorsSchedule()
        {
            InitializeComponent();
        }

        private void calendar_OnMonthCellLoaded(object sender, Syncfusion.SfCalendar.XForms.MonthCellLoadedEventArgs e)
        {
            List<string> customDayLabels = new List<string>()
            {
                "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
            };
            calendar.CustomDayLabels = customDayLabels;

            //var vm = BindingContext as DoctorsScheduleViewModel;

            //var blackoutDays = new List<DateTime>();

            //if (e.Date.DayOfWeek != vm.DayAvailable)
            //{
            //    blackoutDays.Add(e.Date);
            //    calendar.BlackoutDates = blackoutDays;
            //}
        }
    }
}
