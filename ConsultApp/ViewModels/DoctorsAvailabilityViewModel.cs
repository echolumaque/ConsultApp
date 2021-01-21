using System;
using System.Collections.ObjectModel;
using System.Linq;
using ConsultApp.API.Models;
using ConsultApp.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace ConsultApp.ViewModels
{
    public class DoctorsAvailabilityViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private ISetStatusBarColor setStatusBarColor { get; }
        public DoctorsAvailabilityViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.navigationService = navigationService;

            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.White);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            passedDoctor = parameters["doctor"] as ObservableCollection<DoctorsAndSpecializationsModel>;
            Doctor = passedDoctor[0].Doctor;
            Specialization = passedDoctor[0].Specialization;
            DaysAvailable = passedDoctor[0].DaysAvailable;
            Hospital = passedDoctor[0].Hospital;
            IsAvailable = passedDoctor[0].IsAvailable;
            Distance = passedDoctor[0].Distance;
        }

        #region Properties
        private ObservableCollection<DoctorsAndSpecializationsModel> passedDoctor;


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

        private string daysAvailable;
        public string DaysAvailable
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

        #endregion

        #region Methods

        #endregion
    }
}
