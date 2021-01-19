using System;
using System.Threading.Tasks;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Xamarin.Essentials;
using ConsultApp.Helpers.Interfaces;
using ConsultApp.Helpers;
using Xamarin.Forms;

namespace ConsultApp.Dialogs.ViewModels
{
    public class LocationErrorViewModel : BindableBase, IDialogAware
    {
        private readonly ILocation location;
        private readonly ISetStatusBarColor setStatusBarColor;
        public LocationErrorViewModel(ILocation location, ISetStatusBarColor setStatusBarColor)
        {
            this.location = location;

            this.setStatusBarColor = setStatusBarColor;
            this.setStatusBarColor.SetStatusBarColor(Color.White);

            CloseDialog = new DelegateCommand(() => RequestClose(null));
            RequestPermissionCommand = new DelegateCommand(async () => await RequestPermissions());
        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }
        

        public void OnDialogOpened(IDialogParameters parameters)
        {

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

            if (DeviceInfo.Platform.Equals(DevicePlatform.Android))
                await location.DisplayLocationSettingsRequest();

            App.CurrentLcoation = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.High,
                Timeout = TimeSpan.FromSeconds(10),
            });
        }
        #endregion
    }
}
