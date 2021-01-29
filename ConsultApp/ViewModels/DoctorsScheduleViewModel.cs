using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using ConsultApp.API.Models;
using ConsultApp.Helpers.Interfaces;
using Syncfusion.SfCalendar.XForms;

namespace ConsultApp.ViewModels
{
    public class DoctorsScheduleViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private IToast toast { get; }
        public DelegateCommand<CalendarTappedEventArgs> SelectDateCommand { get; }
        public DelegateCommand AddToPendingConsultationCommand { get; }

        public DoctorsScheduleViewModel(INavigationService navigationService, IToast toast) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.toast = toast;

            SelectDateCommand = new DelegateCommand<CalendarTappedEventArgs>((selectedDate) => SelectDate(selectedDate));
            AddToPendingConsultationCommand = new DelegateCommand(async () => await AddToPendingConsultation());
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            Loading = true;
            ViewsLoaded = false;
            var doctor = parameters["doctor"] as ObservableCollection<DoctorsAndSpecializationsModel>;
            Doctor = doctor[0].Doctor;
            Specialization = doctor[0].Specialization;
            Hospital = doctor[0].Hospital;
            Distance = doctor[0].Distance;
            DayAvailable = doctor[0].DaysAvailable;
            //pins
            latitude = doctor[0].MapPins[0].Location.Latitude;
            longitude = doctor[0].MapPins[0].Location.Longitude;
            label = doctor[0].MapPins[0].Label;
            address = doctor[0].MapPins[0].Address;
            Loading = false;
            ViewsLoaded = true;

            Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DoctorSchedulePage", new System.Collections.Generic.Dictionary<string, string>
            {
                    { "Value", "DoctorSchedulePageVisits" }
            });
            await App.RetryPolicy(async () => await App.CreateDatabaseTable<PendingConsultationsModel>().ConfigureAwait(false));
        }

        #region Properties
        private double latitude;
        private double longitude;
        private string label;
        private string address;
        private string consultationDate;
        public bool Online => DayAvailable.Equals(DateTime.Now.DayOfWeek) ? true : false;

        private string doctor;
        public string Doctor
        {
            get { return doctor; }
            set { SetProperty(ref doctor, value); }
        }

        private string specialization;
        public string Specialization
        {
            get { return specialization; }
            set { SetProperty(ref specialization, value); }
        }

        private string hospital;
        public string Hospital
        {
            get { return hospital; }
            set { SetProperty(ref hospital, value); }
        }

        private double distance;
        public double Distance
        {
            get { return distance; }
            set { SetProperty(ref distance, value); }
        }

        private DayOfWeek dayAvailable;
        public DayOfWeek DayAvailable
        {
            get { return dayAvailable; }
            set { SetProperty(ref dayAvailable, value); }
        }

        private bool loading;
        public bool Loading
        {
            get { return loading; }
            set { SetProperty(ref loading, value); }
        }

        private bool viewsLoaded;
        public bool ViewsLoaded
        {
            get { return viewsLoaded; }
            set { SetProperty(ref viewsLoaded, value); }
        }

        #endregion

        #region Methods

        private void SelectDate(CalendarTappedEventArgs selectedDate)
        {
            consultationDate = selectedDate.DateTime.ToString("MMMM dd, yyyy");
            if (DayAvailable != selectedDate.DateTime.DayOfWeek && selectedDate.DateTime < DateTime.Now)
            {
                Prism.PrismApplicationBase.Current.MainPage.DisplayAlert
                    ($"{Doctor} is not available on the selected day",
                    $"{Doctor} is only available every {DayAvailable}. Please select your vacant available day every {DayAvailable} on the future in order to schedule a consultation.",
                    "Okay");
            }
        } 

        private async Task AddToPendingConsultation()
        {
            try
            {
                var pendingConsultation = new PendingConsultationsModel()
                {
                    AvailableDay = DayAvailable.ToString(),
                    Distance = Distance,
                    Hospital = Hospital,
                    Name = Doctor,
                    SelectedDay = consultationDate,
                    Specialization = Specialization,
                    CreationDate = DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt"),
                    Latitude = latitude,
                    Longitude = longitude,
                    Label = label,
                    Address = address,
                };

                if (DayAvailable == DateTime.Parse(consultationDate).DayOfWeek && DateTime.Parse(consultationDate) > DateTime.Now)
                {
                    await App.RetryPolicy(async () => await App.ConnectionString.InsertAsync(pendingConsultation));
                    toast.ShowToast("Successfully scheduled consultation!");
                    await navigationService.GoBackToRootAsync();
                    Xamarin.Essentials.Preferences.Set("navigatable", true);
                }
                else
                {
                    toast.ShowToast($"{Doctor} is only available every {DayAvailable}. Please select your vacant date for every {DayAvailable} in order to schedule a consultation.");
                }
            }
            catch (Exception)
            {
                toast.ShowToast("Please select a date.");
            }
        }
        #endregion
    }
}
