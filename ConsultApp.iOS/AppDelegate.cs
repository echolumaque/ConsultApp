using ConsultApp.Helpers;
using ConsultApp.Helpers.Interfaces;
using ConsultApp.iOS.Helpers;
using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;
using Syncfusion.SfCalendar.XForms.iOS;

namespace ConsultApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.InitImageSourceHandler();
            new Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer();
            SfCalendarRenderer.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
            Xamarin.FormsMaps.Init();
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ISetStatusBarColor, SetStatusBarColoriOS>();
            containerRegistry.Register<IToast, Toast>();
        }
    }
}
