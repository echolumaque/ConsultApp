using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using ConsultApp.Droid.Helpers.Dependencies;
using ConsultApp.Helpers.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(AskLocation))]
namespace ConsultApp.Droid.Helpers.Dependencies
{
    public class AskLocation : ILocation
    {
        public async Task DisplayLocationSettingsRequest()
        {
            LocationSettingsResponse locationSettingsResponse;
            var activity = Xamarin.Essentials.Platform.CurrentActivity;
            try
            {
                //var googleApiClient = new GoogleApiClient.Builder(activity).AddApi(LocationServices.API).Build();
                //googleApiClient.Connect();
                var locationRequest = LocationRequest.Create();
                locationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
                locationRequest.SetInterval(10000);
                locationRequest.SetFastestInterval(5000);

                var locationSettingsRequestBuilder = new LocationSettingsRequest.Builder().AddLocationRequest(locationRequest);
                locationSettingsRequestBuilder.SetAlwaysShow(false);
                locationSettingsResponse = await LocationServices.GetSettingsClient(activity).CheckLocationSettingsAsync(locationSettingsRequestBuilder.Build());
            }
            catch (ApiException ex)
            {
                switch (ex.StatusCode)
                {
                    case CommonStatusCodes.ResolutionRequired:
                        var resolvable = (ResolvableApiException)ex;
                        resolvable.StartResolutionForResult(activity, 0x1);
                        break;
                    default:
                        break;
                }
            }

            //var googleApiClient = new GoogleApiClient.Builder(this).AddApi(LocationServices.API).Build();
            //googleApiClient.Connect();

            //var locationRequest = LocationRequest.Create();
            //locationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
            //locationRequest.SetInterval(10000);
            //locationRequest.SetFastestInterval(10000 / 2);

            //var builder = new LocationSettingsRequest.Builder().AddLocationRequest(locationRequest);
            //builder.SetAlwaysShow(true);

            //var result = LocationServices.SettingsApi.CheckLocationSettings(googleApiClient, builder.Build());
            //result.SetResultCallback((LocationSettingsResult callback) =>
            //{
            //    switch (callback.Status.StatusCode)
            //    {
            //        case CommonStatusCodes.Success:
            //        {
            //            //DoStuffWithLocation();
            //            break;
            //        }
            //        case CommonStatusCodes.ResolutionRequired:
            //        {
            //            try
            //            {
            //                callback.Status.StartResolutionForResult(this, REQUEST_CHECK_SETTINGS);
            //            }
            //            catch (IntentSender.SendIntentException e)
            //            {
            //            }

            //            break;
            //        }
            //        default:
            //        {
            //            // If all else fails, take the user to the android location settings
            //            StartActivity(new Intent(Android.Provider.Settings.ActionLocationSourceSettings));
            //            break;
            //        }
            //    }
            //});
        }
    }
}