using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using ConsultApp.Helpers.Interfaces;
using ConsultApp.Droid.Helpers.Dependencies;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(AskLocation))]
namespace ConsultApp.Droid.Helpers.Dependencies
{
    public class AskLocation : ILocation
    {
        public async Task DisplayLocationSettingsRequest()
        {
            var activity = Xamarin.Essentials.Platform.CurrentActivity;
            LocationSettingsResponse locationSettingsResponse;

            try
            {
                var locationRequest = LocationRequest.Create();
                locationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
                locationRequest.SetInterval(2000);
                locationRequest.SetFastestInterval(1250);

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
        }
    }
}