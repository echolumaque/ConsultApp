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

            await App.CreateDatabaseTable<PendingConsultationsModel>().ConfigureAwait(false);
            await App.CreateDatabaseTable<Pins>().ConfigureAwait(false);
        }

        #region Properties
        private double latitude;
        private double longitude;
        private string label;
        private string address;
        private string consultationDate;

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

        #endregion

        #region Methods

        private void SelectDate(CalendarTappedEventArgs selectedDate) => consultationDate = selectedDate.DateTime.ToString("MMMM dd, yyyy");

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
                };

                var pins = new Pins()
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    Label = label,
                    Address = address,
                };

                //bad request solved using polly, map distances
                var findDuplicate = await App.ConnectionString.QueryAsync<PendingConsultationsModel>
                    ("SELECT SelectedDay FROM PendingConsultationsModel GROUP BY SelectedDay HAVING COUNT(*) > 1");

                if (findDuplicate.Count > 1)
                {
                    toast.ShowToast($"You already have scheduled consultation with {Doctor} on {consultationDate}.");
                }
                else
                {
                    await App.ConnectionString.InsertAsync(pendingConsultation);
                    await App.ConnectionString.InsertAsync(pins);
                    toast.ShowToast("Successfully scheduled consultation!");
                    await navigationService.GoBackToRootAsync();
                }
            }
            catch (Exception)
            {
                 var findDuplicate = await App.ConnectionString.QueryAsync<PendingConsultationsModel>
                    ("SELECT SelectedDay FROM PendingConsultationsModel GROUP BY SelectedDay HAVING COUNT(*) > 1");

                if (findDuplicate.Count > 1)
                {
                    toast.ShowToast($"You already have scheduled consultation with {Doctor} on {consultationDate}.");
                }

            }
        }
        #endregion
    }
}
