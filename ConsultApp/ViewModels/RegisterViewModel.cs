using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Essentials;
using ConsultApp.Helpers.Interfaces;
using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace ConsultApp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private ISetStatusBarColor setStatusBarColor;
        public DelegateCommand RegisterCommand { get; }
        public RegisterViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.setStatusBarColor = setStatusBarColor;
            setStatusBarColor.SetStatusBarColor(Color.White);
            RegisterCommand = new DelegateCommand(async () => await Register());

            NameError = true;
            GenderError = true;

            MaxDate = DateTime.Now;
        }

        public override void OnNavigatedTo(INavigationParameters parameters) => setStatusBarColor.SetStatusBarColor(Color.White);

        #region Properties

        private string name;
        public string Name
        {
            get { return name; }
            set
            { 
                SetProperty(ref name, value);
                if (!string.IsNullOrWhiteSpace(Name))
                    NameError = false;
                else
                    NameError = true;
            }
        }

        private string gender;
        public string Gender
        {
            get { return gender; }
            set
            { 
                SetProperty(ref gender, value);
                if (!string.IsNullOrWhiteSpace(Gender))
                    GenderError = false;
                else
                    GenderError = true;
            }
        }

        private DateTime birth;
        public DateTime Birth
        {
            get { return birth; }
            set 
            { 
                SetProperty(ref birth, value);
            }
        }

        //validations
        private bool nameError;
        public bool NameError
        {
            get { return nameError; }
            set { SetProperty(ref nameError, value); }
        }

        private bool genderError;
        public bool GenderError
        {
            get { return genderError; }
            set { SetProperty(ref genderError, value); }
        }

        //datepicker
        private DateTime maxDate;
        public DateTime MaxDate
        {
            get { return maxDate; }
            set { SetProperty(ref maxDate, value); }
        }
        #endregion

        #region Methods

        private async Task Register()
        {
            if (AreFieldsValid())
            {
                Preferences.Set("name", Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Name));
                Preferences.Set("gender", Gender);
                Preferences.Set("birth", Birth);

                await navigationService.NavigateAsync("/CustomNavigationPage/HomePage");
                Analytics.TrackEvent("Register", new Dictionary<string, string>
                {
                    { "Registered Users", Name }
                });
            }
        }

        //private void ValidationRules()
        //{
        //    IsValidName.Validations.Add(new IsNotNullOrEmpty<string> { ValidationMessage = "Your name is required" });
        //    IsValidAge.Validations.Add(new IsNotNullOrEmpty<string> { ValidationMessage = "Your age is required" });
        //    IsValidGender.Validations.Add(new IsNotNullOrEmpty<string> { ValidationMessage = "Your gender is required" });
        //    IsValidBirth.Validations.Add(new IsNotNullOrEmpty<DateTime> { ValidationMessage = "Your birthday is required" });
        //}

        private bool AreFieldsValid()
        {
            bool isValidName = !string.IsNullOrWhiteSpace(Name); 
            bool isValidGender = !string.IsNullOrWhiteSpace(Gender);
            bool isValidBirth = Birth != null;

            return isValidName && isValidGender && isValidBirth;
        }
        #endregion
    }
}
