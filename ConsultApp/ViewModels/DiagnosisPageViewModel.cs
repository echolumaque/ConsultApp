using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ConsultApp.API.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using ConsultApp.Helpers;
using Xamarin.Essentials;
using System;
using Rg.Plugins.Popup.Services;
using ConsultApp.Dialogs.Views;
using ConsultApp.Helpers.Interfaces;

namespace ConsultApp.ViewModels
{
    public partial class DiagnosisPageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private ISetStatusBarColor setStatusBarColor { get; }
        private ILocation location { get; }

        public DiagnosisPageViewModel(INavigationService navigationService, ISetStatusBarColor setStatusBarColor, ILocation location) : base(navigationService)
        {
            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.White);

            this.location = location;

            this.navigationService = navigationService;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            var items = parameters["Diseases"] as ObservableCollection<DiagnosisModel>;
            Diseases = new ObservableCollection<DiagnosisModel>(items);

            foreach (var item in Diseases)
            {
                item.DoctorsPageCommand = new DelegateCommand<string>(async (specialty) => await DoctorsPage(specialty));
            }
        }

        #region Properties

        private ObservableCollection<DiagnosisModel> diseases;
        public ObservableCollection<DiagnosisModel> Diseases
        {
            get { return diseases; }
            set { SetProperty(ref diseases, value); }
        }
        #endregion

        #region Methods
        private async Task DoctorsPage(string specialty)
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

                var parameters = new NavigationParameters
                {
                    { "Major", specialty }
                };
                await navigationService.NavigateAsync("DoctorsPage", parameters);
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
