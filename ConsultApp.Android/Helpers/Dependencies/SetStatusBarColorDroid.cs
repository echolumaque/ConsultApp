using Android.Views;
using ConsultApp.Helpers.Interfaces;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

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