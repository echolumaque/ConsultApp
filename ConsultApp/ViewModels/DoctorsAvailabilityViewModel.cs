using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ConsultApp.API.Models;
using ConsultApp.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using ConsultApp.Dialogs.Views;

namespace ConsultApp.ViewModels
{
    public class DoctorsAvailabilityViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private ISetStatusBarColor setStatusBarColor { get; }

        public DelegateCommand<int> AddToPendingCommand { get; }
        public DelegateCommand SelectedDateChanged { get; set; }

        public DoctorsAvailabilityViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.navigationService = navigationService;

            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.White);

            AddToPendingCommand = new DelegateCommand<int>(async (day) => await AddToPendings(day));
            Enabled = false;

            SelectedDateChanged = new DelegateCommand(() => Enabled = true);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            var passedDoctor = parameters["doctor"] as ObservableCollection<DoctorsAndSpecializationsModel>;
            Doctor = passedDoctor[0].Doctor;
            Specialization = passedDoctor[0].Specialization;
            DaysAvailable = passedDoctor[0].DaysAvailable;
            Hospital = passedDoctor[0].Hospital;
            IsAvailable = passedDoctor[0].IsAvailable;
            Distance = passedDoctor[0].Distance;
        }

        #region Properties


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

        private DayOfWeek daysAvailable;
        public DayOfWeek DaysAvailable
        {
            get { return daysAvailable; }
            set { SetProperty(ref daysAvailable, value); }
        }

        private string hospital;
        public string Hospital
        {
            get { return hospital; }
            set { SetProperty(ref hospital, value); }
        }

        private bool isAvailable;
        public bool IsAvailable
        {
            get { return isAvailable; }
            set { SetProperty(ref isAvailable, value); }
        }

        private double distance;
        public double Distance
        {
            get { return distance; }
            set { SetProperty(ref distance, value); }
        }

        private int selectedDate;
        public int SelectedDate
        {
            get { return selectedDate; }
            set { SetProperty(ref selectedDate, value); }
        }

        private bool enable;
        public bool Enabled
        {
            get { return enable; }
            set { SetProperty(ref enable, value); }
        }
        #endregion

        #region Methods

        private async Task AddToPendings(int selectedDay)
        {
            await PopupNavigation.Instance.PushAsync(new ConfirmConsultation(), true).ContinueWith(async x => await navigationService.GoBackToRootAsync());
        }
        #endregion
    }
}
