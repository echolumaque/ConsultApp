using ConsultApp.API.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using ConsultApp.Helpers;
using Xamarin.Forms;
using ConsultApp.Helpers.Interfaces;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;
using System;
using ConsultApp.Dialogs.Views;

namespace ConsultApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private ISetStatusBarColor setStatusBarColor { get; }
        private ILocation location { get; }

        public HomePageViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor, ILocation location) : base(navigationService)
        {
            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.FromHex("E6EDFF"));
            this.location = location;

            this.navigationService = navigationService;

            Buttons = new ObservableCollection<ButtonsModel>()
            {
                new ButtonsModel
                {
                    Title = "Diagnostic",
                    FontIcon = "\uf0f1",
                    Commands = new DelegateCommand(async() => await this.navigationService.NavigateAsync("ConsultPage")),
                },
                new ButtonsModel
                {
                    Title = "Shots",
                    FontIcon = "\uf48e",
                    Commands = new DelegateCommand(async () => await this.navigationService.NavigateAsync("")),
                },
                new ButtonsModel
                {
                    Title = "Consultation",
                    FontIcon = "\uf095",
                    Commands = new DelegateCommand(async() => await navigationService.NavigateAsync(""))
                },
                new ButtonsModel
                {
                    Title = "Ambulance",
                    FontIcon = "\uf0f9",
                    Commands = new DelegateCommand(async() => await navigationService.NavigateAsync(""))
                },
                new ButtonsModel
                {
                    Title = "Nurse",
                    FontIcon = "\uf0f0",
                    Commands = new DelegateCommand(async() => await navigationService.NavigateAsync(""))
                },
                new ButtonsModel
                {
                    Title = "Lab Work",
                    FontIcon = "\uf0f8",
                    Commands = new DelegateCommand(async() => await navigationService.NavigateAsync(""))
                },
            };
        }

        public override void OnNavigatedTo(INavigationParameters parameters) => setStatusBarColor.SetStatusBarColor(Color.FromHex("E6EDFF"));

        public override async void Initialize(INavigationParameters parameters) => await GetLocation();
      
        #region Properties

        private ObservableCollection<ButtonsModel> buttons;
        public ObservableCollection<ButtonsModel> Buttons
        {
            get { return buttons; }
            set { SetProperty(ref buttons, value); }
        }

        #endregion

        #region Methods
        private async Task GetLocation()
        {
            try
            {
                await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
                await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                await Permissions.CheckStatusAsync<Permissions.NetworkState>();
                await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

                await Permissions.RequestAsync<Permissions.LocationAlways>();
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                await Permissions.RequestAsync<Permissions.NetworkState>();
                await Permissions.RequestAsync<Permissions.StorageWrite>();

                await Task.Run(async () =>
                {
                    await location.DisplayLocationSettingsRequest();
                }).ContinueWith(async x => await SetLocation());

                //if (location.DisplayLocationSettingsRequest().IsCompleted)
                //{
                //    var loc =  Geolocation.GetLocationAsync(new GeolocationRequest
                //    {
                //        DesiredAccuracy = GeolocationAccuracy.High,
                //        Timeout = TimeSpan.FromSeconds(10)
                //    });
                //    App.CurrentLcoation = new Location(loc.Result.Latitude, loc.Result.Longitude);
                //}    
                //else
                //{
                //    await PopupNavigation.Instance.PushAsync(new LocationError(), true);
                //}
            }
            catch (Exception)
            {
                await PopupNavigation.Instance.PushAsync(new LocationError(), true);
            }
        }

        private async Task SetLocation()
        {
            App.CurrentLocation = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.High,
                Timeout = TimeSpan.FromSeconds(10)
            });
        }
        #endregion
    }
}