using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Essentials;
using ConsultApp.Helpers.Interfaces;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace ConsultApp.Dialogs.ViewModels
{
    public class LocationErrorViewModel : BindableBase
    {
        //private ILocation location = DependencyService.Get<ILocation>();
        public LocationErrorViewModel()
        {
            CloseDialog = new DelegateCommand(async() => await PopupNavigation.Instance.PopAsync(true));
            RequestPermissionCommand = new DelegateCommand(async () => await RequestPermissions());
        }

        #region Properties

        public DelegateCommand CloseDialog { get; set; }
        public DelegateCommand RequestPermissionCommand { get; set; }

        #endregion

        #region Methods

        private async Task RequestPermissions()
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
                await DependencyService.Get<ILocation>().DisplayLocationSettingsRequest();
            }).ContinueWith(async x => await SetLocation());

            //if (DeviceInfo.Platform.Equals(DevicePlatform.Android))
            //{
            //    await DependencyService.Get<ILocation>().DisplayLocationSettingsRequest();

            //    App.CurrentLcoation = await Geolocation.GetLocationAsync(new GeolocationRequest
            //    {
            //        DesiredAccuracy = GeolocationAccuracy.High,
            //        Timeout = TimeSpan.FromSeconds(10),
            //    });
            //    await PopupNavigation.Instance.PopAsync(true);
            //        //App.CurrentLcoation = new Location(loc.Result.Latitude, loc.Result.Longitude);

            //    //else TODO: FOR OTHER PLATFORMS
            //    //{
            //    //    App.CurrentLcoation = await Geolocation.GetLocationAsync(new GeolocationRequest
            //    //    {
            //    //        DesiredAccuracy = GeolocationAccuracy.High,
            //    //        Timeout = TimeSpan.FromSeconds(10),
            //    //    });
            //    //}
            //}
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
