using Android;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using ConsultApp.Droid.Helpers;
using ConsultApp.Droid.Helpers.Dependencies;
using ConsultApp.Helpers;
using ConsultApp.Helpers.Interfaces;
using Prism;
using Prism.Ioc;
using Prism.Services.Dialogs;

namespace ConsultApp.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize,
              ScreenOrientation = ScreenOrientation.Portrait, ResizeableActivity = false)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Android.Glide.Forms.Init(this);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            FFImageLoading.Forms.Platform.CachedImageRenderer.InitImageViewHandler();
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
            {
                Window.SetStatusBarColor(Color.Rgb(230, 237, 255));
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
                Window.AddFlags(WindowManagerFlags.LayoutInScreen);
                Window.ClearFlags(WindowManagerFlags.Fullscreen);
            }

            InitFontScale();

            LoadApplication(new App(new AndroidInitializer()));
        }

        protected override void OnStart()
        {
            base.OnStart();

            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                {
                    RequestPermissions(LocationPermissions, RequestLocationId);
                }
            }
        }

        public override void OnBackPressed() => Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);


        public override Resources Resources
        {
            get
            {
                Configuration config = new Configuration();
                config.SetToDefaults();

                return CreateConfigurationContext(config).Resources;
            }
        }

        private void InitFontScale()
        {
            Configuration configuration = Resources.Configuration;
            configuration.FontScale = (float)0.85;
            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);

            metrics.ScaledDensity = configuration.FontScale * metrics.Density;

            BaseContext.ApplicationContext.CreateConfigurationContext(configuration);
            BaseContext.Resources.DisplayMetrics.SetTo(metrics);
        }

        private const int RequestLocationId = 0;
        private readonly string[] LocationPermissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.LocationHardware,
            Manifest.Permission.Internet,
            Manifest.Permission.WriteExternalStorage,
        };

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ISetStatusBarColor, SetStatusBarColorDroid>();
            containerRegistry.Register<ILocation, AskLocation>();
        }
    }
}

