using Android.App;
using Android.Widget;
using ConsultApp.Helpers.Interfaces;

namespace ConsultApp.Droid.Helpers.Dependencies
{
    public class Toast : IToast
    {
        public void ShowToast(string message)
        {
            Android.Widget.Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}