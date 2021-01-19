using Android.Views;
using ConsultApp.Droid.Helpers;
using ConsultApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(SetStatusBarColorDroid))]
namespace ConsultApp.Droid.Helpers
{
    public class SetStatusBarColorDroid : ISetStatusBarColor
    {
        public void SetStatusBarColor(Color color)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var currentWindow = GetCurrentWindows();
                currentWindow.SetStatusBarColor(color.ToAndroid());
            });
        }

        private Window GetCurrentWindows()
        {
            var window = Xamarin.Essentials.Platform.CurrentActivity.Window;
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            return window;
        }
    }
}